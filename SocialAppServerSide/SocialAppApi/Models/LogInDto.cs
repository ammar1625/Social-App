using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppApi.Models
{
    public class LogInDto
    {
        public string Email { get; set; } = null!;
        public string PassWord { get; set; } = null!;
    }
}
