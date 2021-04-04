namespace PublicApi.DTO.v1.Error
{
    public class Error
    {
        public Error(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}