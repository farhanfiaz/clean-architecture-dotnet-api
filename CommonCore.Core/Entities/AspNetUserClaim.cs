using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonCore.Core.Entities;

[Table("AspNetUserClaim")]
public class AspNetUserClaim : IdentityUserClaim<Guid>
{
    [Key]
    public override Guid UserId { get; set; }
}