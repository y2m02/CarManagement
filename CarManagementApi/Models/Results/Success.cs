namespace CarManagementApi.Models.Results
{
    public class Success : IResult
    {
        public Success() { }

        public Success(object response)
        {
            Response = response;
        }

        public object Response { get; }
    }
}
