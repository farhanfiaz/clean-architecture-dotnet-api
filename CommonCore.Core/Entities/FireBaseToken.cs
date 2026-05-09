using System;
using System.Collections.Generic;
using System.Text;

namespace CommonCore.Core.Entities;

public class FireBaseToken
{
    public int FirebaseTokenId { get; set; }
    public string UserIdFk { get; set; }
    public string Token { get; set; }
    public string DeviceName { get; set; }
    public string DeviceType { get; set; }
    public DateTime CreatedAt { get; set; }
}
