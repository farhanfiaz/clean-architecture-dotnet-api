using MediatR;
using CommonCore.InterfaceAdapters.Dtos;

namespace CommonCore.Application.Commands.AuthToken;

public class SendVerificationCodeCommandHandler() : IRequestHandler<SendVerificationCodeCommand, ApiResult>
{
    public Task<ApiResult> Handle(SendVerificationCodeCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
