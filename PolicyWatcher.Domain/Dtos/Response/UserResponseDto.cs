using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyWatcher.Domain.Dtos.Response
{
    public class UserResponseDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public bool IsFlagged { get; set; }
        public decimal Tier { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}
