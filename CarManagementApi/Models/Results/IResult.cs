namespace CarManagementApi.Models.Results
{
    public interface IResult
    {
        sealed bool Succeeded() => this is Success;
        sealed bool HasValidation() => this is Validation;
        sealed bool Failed() => this is Failure;
        sealed bool Unauthorized() => this is Unauthorized;
        sealed bool NotFound() => this is NotFound;
    }
}
