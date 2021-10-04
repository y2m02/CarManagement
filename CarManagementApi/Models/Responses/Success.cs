namespace CarManagementApi.Models.Responses
{
    public class Success : IResponse
    {
        public Success() { }

        public Success(object response)
        {
            Response = response;
        }

        public object Response { get; }
    }
}
