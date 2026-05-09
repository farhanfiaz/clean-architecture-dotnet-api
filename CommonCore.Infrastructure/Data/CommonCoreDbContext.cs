using CommonCore.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CommonCore.Infrastructure.Data;

public class CommonCoreDbContext : IdentityDbContext<AspNetUser, AspNetRole, Guid, AspNetUserClaim, AspNetUserRole, AspNetUserLogin, AspNetRoleClaim, AspNetUserToken>
{
    public CommonCoreDbContext(DbContextOptions<CommonCoreDbContext> options)
            : base(options)
    {
    }
    public DbSet<AspNetRole> AspNetRoles { get; set; }
    public DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
    public DbSet<AspNetUser> AspNetUsers { get; set; }
    public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
    public DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
    public DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
    public DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
    public DbSet<AspNetUsersPasswordReset> AspNetUsersPasswordReset { get; set; }
    public DbSet<FireBaseToken> FireBaseTokens { get; set; }
    public DbSet<AuthToken> AuthTokens { get; set; }
}


