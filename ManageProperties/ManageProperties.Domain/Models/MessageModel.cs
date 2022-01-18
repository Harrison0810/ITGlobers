namespace ManageProperties.Domain.Models
{
    public class MessageModel<T> where T : class
    {
        public T Data { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
