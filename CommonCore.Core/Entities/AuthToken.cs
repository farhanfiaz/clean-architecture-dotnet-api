using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonCore.Core.Entities;

public class AuthToken
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string PublicKey { get; set; }
    public string DeviceId { get; set; }
    public string DeviceName { get; set; }
    public string DevicePlatForm { get; set; }
    public string UserName { get; set; }
    public DateTime CreatedAt { get; set; }
}
