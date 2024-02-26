namespace PolicyWatcher.Domain.Dtos.Request
{
    public class AddDateRequestDto
    {
        public int UserId { get; set; }
        public int DaysToBackDate { get; set; }
    }
}
