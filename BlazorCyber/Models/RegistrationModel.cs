﻿using System.ComponentModel.DataAnnotations;

namespace BlazorCyber.Models
{
    public class RegistrationModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}
