using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonCore.InterfaceAdapters.Dtos.Auth;

public class ProfileUserDto
{
    public string UserId { get; set; }
    [Required(ErrorMessage = "First Name is required")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "Last Name is required")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "DOB is required")]
    public DateTime? DateOfBirth { get; set; }
    [Required(ErrorMessage = "Phone Number is required")]
    //[RegularExpression("^[0-9]+$", ErrorMessage = "Please enter a valid phone number.")]
    public string PhoneNumber { get; set; }
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    //  public string defaultshippingAddress1 { get; set; }
    //  public string defaultshippingAddress2 { get; set; }
    //  public string defaultcity { get; set; }
    //  public string defaultzipcode { get; set; }
    //  public string defaultState { get; set; }
    public bool IsTwoFactorEnabled { get; set; } = false;
}
