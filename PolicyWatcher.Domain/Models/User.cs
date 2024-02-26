using PolicyWatcher.Domain.Dtos.Response;
using System.ComponentModel.DataAnnotations;

namespace PolicyWatcher.Domain.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string FullName { get; set; }
        public decimal Balance { get; set; }
        public bool IsFlagged { get; set; }
        public List<Transaction> Transactions { get; set; }
        public decimal Tier { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserResponseDto ToDto()
        {
            return new UserResponseDto
            {
                UserId = this.UserId,
                FullName = this.FullName,
                IsFlagged = this.IsFlagged,
                Tier = this.Tier,
                CreatedAt = this.CreatedAt
            };
        }
    }
}
