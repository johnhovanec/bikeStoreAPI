using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikestoreAPI.Models
{
    public class Session
    {
        public int Id { get; set; }
        public User.UserType UserSessionType { get; set; }
        public string SessionId { get; set; }
        public DateTime? SessionStart { get; set; }
        public DateTime? SessionExpires { get; set; }

        // 
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
