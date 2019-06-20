namespace ConsoleApp
{
    public class JsonResponseGeneric<T>
        where T : class
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Body { get; set; }
    }
}