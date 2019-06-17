namespace Dto.Common
{
    public abstract class ResponseBase
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
