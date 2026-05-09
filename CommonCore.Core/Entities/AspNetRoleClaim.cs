using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonCore.Core.Entities;

[Table("AspNetRoleClaim")]
public class AspNetRoleClaim : IdentityRoleClaim<Guid>
{
    [Key]
    public override Guid RoleId { get; set; }
}