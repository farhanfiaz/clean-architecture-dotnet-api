using System;
using System.Collections.Generic;
using System.Text;

namespace CommonCore.InterfaceAdapters.Dtos.Auth;

public class AuthResponceVM
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Token { get; set; }
}
