using AFC.Base.Schema;

namespace AFC.Schema;

public class TokenRequest : BaseRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class TokenResponse : BaseResponse
{
    public DateTime ExpireDate { get; set; }
    public string Token { get; set; }
    public string Email { get; set; }
}