using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonCore.InterfaceAdapters.Dtos.Auth;

public class RegisterDto
{
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Required]
    public string ConfirmPassword { get; set; } = null!;
    [Required]
    public string Gender { get; set; } = null!;
    [Required]
    public string PhoneNumber { get; set; } = null!;
    [Required]
    public string DateOfBirth { get; set; } = null!;
    [Required]
    public string UserType { get; set; } = "Customer";
    public string DeviceId { get; set; }
    public string DeviceName { get; set; }
    public string DevicePlatform { get; set; }
}
