﻿using System.ComponentModel.DataAnnotations;

namespace OurProject.Models
{
    public class RegisterUserModel
    {
        [Required,MaxLength(256)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required, DataType(DataType.EmailAddress),MaxLength(256)]
        public string Email { get; set; }

        [Required,DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name ="UserRoles")]
        public string UserRoles { get; set; }

    }
}
