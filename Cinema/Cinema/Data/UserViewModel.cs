﻿using System.ComponentModel.DataAnnotations;

namespace Cinema.Data
{
    public class UserViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string RoleId { get; set; }
    }
}
