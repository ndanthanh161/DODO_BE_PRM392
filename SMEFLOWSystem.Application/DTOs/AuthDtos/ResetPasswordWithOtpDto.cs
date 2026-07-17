using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMEFLOWSystem.Application.DTOs.AuthDtos
{
    public class ResetPasswordWithOtpDto
    {
        public string Email { get; set; } = string.Empty;
        public String Otp { get; set; } = String.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
