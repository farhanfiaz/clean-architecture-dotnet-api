using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonCore.InterfaceAdapters.Dtos.Auth;

public class ChangePassword
{
    public string UserId { get; set; }
    public string UserEmail { get; set; }
    [Required]
    public string CurrentPassword { get; set; }
    [Required]
    public string NewPassword { get; set; }
}
