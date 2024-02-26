using PolicyWatcher.Domain.Enums;

namespace PolicyWatcher.Domain.Dtos.Request
{
    public class UserRequestDto
    {
        public string FullName { get; set; }
        public TierLevel Tier { get; set; }
    }
}
