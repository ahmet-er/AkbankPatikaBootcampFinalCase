using AFC.Base.Response;
using AFC.Business.Cqrs;
using AFC.Schema;
using MediatR;

namespace AFC.Business.Command;

public class TokenCommandHandler :
    IRequestHandler<CreateTokenCommand, ApiResponse<TokenResponse>>
{
    public Task<ApiResponse<TokenResponse>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
