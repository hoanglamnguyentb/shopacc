namespace Hinet.API2.Core
{
    public class APIResponseDto<T> where T : class
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public void MessageFalse(string message)
        {
            Message = message;
            Status = false;
        }
    }

    public class APIResponseDto : APIResponseDto<object>
    {
    }
}