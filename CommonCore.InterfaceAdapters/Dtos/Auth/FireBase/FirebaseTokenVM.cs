using System;
using System.Collections.Generic;
using System.Text;

namespace CommonCore.InterfaceAdapters.Dtos.Auth.FireBase;

public class FirebaseTokenVM
{
    public string UserId { get; set; }
    public string Token { get; set; }
    public string PreviousToken { get; set; }
    public string DeviceName { get; set; }
    public string DeviceType { get; set; }
    public DateTime? CreatedDate { get; set; }
}
