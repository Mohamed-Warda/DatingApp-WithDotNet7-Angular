﻿using System.ComponentModel.DataAnnotations;

namespace DatingApp.Dtos
{
    public class LoginDto
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
