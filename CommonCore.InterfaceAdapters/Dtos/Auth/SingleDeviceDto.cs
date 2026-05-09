using System;
using System.Collections.Generic;
using System.Text;

namespace CommonCore.InterfaceAdapters.Dtos.Auth;

public class SingleDeviceDto
{
    public string PublicKey { get; set; }
    public string DeviceId { get; set; }
    public string DeviceName { get; set; }
    public string DevicePlatForm { get; set; }
    public string UserName { get; set; }
}
