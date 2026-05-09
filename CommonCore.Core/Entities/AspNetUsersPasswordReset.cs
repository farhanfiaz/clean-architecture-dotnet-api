using System;
using System.Collections.Generic;
using System.Text;

namespace CommonCore.Core.Entities;

public class AspNetUsersPasswordReset
{
    public int Id { get; set; }
    public string Code { get; set; }
    public DateTime? ExpirationTime { get; set; }
    public string UserName { get; set; }
    public string AspNetUserId { get; set; }
    public string CreatedBy { get; set; }
    public string CreatedByName { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedByName { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }

    public virtual AspNetUser AspNetUser { get; set; }
    public string Key { get; set; }
}
