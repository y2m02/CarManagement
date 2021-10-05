using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CarManagementApi.Helpers;
using CarManagementApi.Models.Entities;
using CarManagementApi.Models.Requests;
using CarManagementApi.Models.Results;
using CarManagementApi.Repositories;
using HelpersLibrary.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CarManagementApi.Services
{
    public interface IAppUserService
    {
        Task<IResult> Register(RegisterAppUserRequest request);
        Task<IResult> SignIn(SignInRequest request);
        Task<IResult> AddToRoles(AddAppUserToRolesRequest request);
    }

    public class AppUserService : IAppUserService
    {
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly IAppUserRepository repository;
        private readonly SignInManager<AppUser> signInManager;

        public AppUserService(
            IMapper mapper,
            IConfiguration configuration,
            IAppUserRepository repository,
            SignInManager<AppUser> signInManager
        )
        {
            this.mapper = mapper;
            this.configuration = configuration;
            this.repository = repository;
            this.signInManager = signInManager;
        }

        public Task<IResult> Register(RegisterAppUserRequest request)
        {
            return ResultHandler.HandleErrors(
                async () =>
                {
                    var result = await repository
                        .Add(mapper.Map<AppUser>(request), request.Password)
                        .ConfigureAwait(false);

                    return result.Succeeded
                        ? ResultHandler.Success(request)
                        : ResultHandler.Validations(result.Errors.Select(e => e.Description));
                }
            );
        }

        public Task<IResult> SignIn(SignInRequest request)
        {
            return ResultHandler.HandleErrors(
                async () =>
                {
                    var result = await signInManager
                        .PasswordSignInAsync(
                            request.UserName,
                            request.Password,
                            isPersistent: false,
                            lockoutOnFailure: false
                        )
                        .ConfigureAwait(false);

                    if (!result.Succeeded)
                    {
                        return ResultHandler.Unauthorized("Invalid credentials");
                    }

                    var token = await GenerateToken(request.UserName).ConfigureAwait(false);

                    return ResultHandler.Success(token);
                }
            );
        }

        public Task<IResult> AddToRoles(AddAppUserToRolesRequest request)
        {
            return ResultHandler.HandleErrors(
                async () =>
                {
                    var user = await repository
                        .GetByUserName(request.UserName)
                        .ConfigureAwait(false);

                    var result = await repository
                        .AddToRoles(user, request.Roles)
                        .ConfigureAwait(false);

                    return result.Succeeded
                        ? ResultHandler.Success($"User added to: {request.Roles.Join(",")}")
                        : ResultHandler.Validations(result.Errors.Select(e => e.Description));
                }
            );
        }

        private async Task<string> GenerateToken(string userName)
        {
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: 30.Minutes().FromNow(),
                claims: await GetClaims(userName).ConfigureAwait(false),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                    SecurityAlgorithms.HmacSha256Signature
                )
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<List<Claim>> GetClaims(string userName)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, userName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var roles = await repository.GetRoles(userName).ConfigureAwait(false);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims;
        }
    }
}
