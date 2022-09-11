using System.Collections.Generic;
using System.Security.Claims;
using TheMuscleBar.Models;

namespace TheMuscleBar.AppCode.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        RefreshTokenModel GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
