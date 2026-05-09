using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonCore.Core.Entities;

[Table("AspNetUser")]
public class AspNetUser : IdentityUser<Guid>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string FullName { get; set; }
    public string Gender { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsExternalLogin { get; set; }
    public bool IsExternalInfoSubmitted { get; set; }
    public string LoginProvider { get; set; }
    public string ZipCode { get; set; }
    public bool IsEligibleToLogin { get; set; } = true;
    public string Firebase_uid { get; set; }
    public string State { get; set; }
    public bool IsTermAndCondition { get; set; }
    public DateTime? LastLoginDateTime { get; set; }
    public bool IsTwoFactorEnabled { get; set; } = false;
    public DateTime? RememberMe { get; set; }
    public string TwoFactorCode { get; set; }
    public string OtpCode { get; set; }
    public DateTime? OtpExpiry { get; set; }

    public virtual ICollection<AspNetUsersPasswordReset> AspNetUsersPasswordResets { get; set; }
}
