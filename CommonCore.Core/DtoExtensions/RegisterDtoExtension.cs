using CommonCore.Core.Entities;
using CommonCore.InterfaceAdapters.Dtos.Auth;

namespace CommonCore.Core.DtoExtensions;

public static class RegisterDtoExtension
{
    public static AspNetUser DtoToEntity(this RegisterDto model)
    {
        return new AspNetUser()
        {
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = string.Concat(model.FirstName, model.LastName),
            PhoneNumber = model.PhoneNumber,
            SecurityStamp = new Guid().ToString(),
            Firebase_uid = "",
            FullName = string.Concat(model.FirstName, " ", model.LastName),
            Gender = model.Gender,
            LoginProvider = "",
            ZipCode = "",
            State = "",
            OtpCode = string.Empty,
            TwoFactorCode = string.Empty,
            DateOfBirth = Convert.ToDateTime(model.DateOfBirth).Date,
            PasswordHash = string.Empty

        };
    }
}
