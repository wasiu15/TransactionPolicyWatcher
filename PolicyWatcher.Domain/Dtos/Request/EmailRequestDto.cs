namespace PolicyWatcher.Domain.Dtos.Request
{
    public class EmailRequestDto
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
    }
}
