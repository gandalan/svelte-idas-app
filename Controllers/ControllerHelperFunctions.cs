using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IDASFeedbackTool.Controllers
{
    public class ControllerHelperFunctions
    {
        public static bool CheckPermissionCode(ClaimsPrincipal user, string roleCode)
        {
            return user.HasClaim(x => x.Type == ClaimTypes.Role && x.Value.ToLower() == roleCode.ToLower());
        }

        public static bool CheckPermissionCode(JwtSecurityToken token, string roleCode)
        {
            return token.Claims.Any(x => x.Type == "role" && x.Value.ToLower() == roleCode.ToLower());
        }

        private static Guid GetGuidFromClaim(ClaimsPrincipal user, string type)
        {
            if(user == null)
                return Guid.Empty;
            if (!user.HasClaim(x => x.Type == type && x.Value != null))
                return Guid.Empty;
            return Guid.Parse(user.Claims.Where(x => x.Type == type).FirstOrDefault().Value);
        }

        private static Guid GetGuidFromClaim(JwtSecurityToken token, string type)
        {
            if (!token.Claims.Any(x => x.Type == type && x.Value != null))
                return Guid.Empty;
            return Guid.Parse(token.Claims.Where(x => x.Type == type).FirstOrDefault().Value);
        }

        private static string GetStringFromClaim(ClaimsPrincipal user, string type)
        {
            if (user == null)
                return null;
            return user.Claims.Where(x => x.Type == type).FirstOrDefault()?.Value;
        }

        private static string GetStringFromClaim(JwtSecurityToken token, string type)
        {
            return token.Claims.Where(x => x.Type == type).FirstOrDefault()?.Value;
        }

        public static Guid GetAppTokenFromClaim(ClaimsPrincipal user)
        {
            return GetGuidFromClaim(user, "appToken");
        }

        public static Guid GetAppTokenFromClaim(JwtSecurityToken token)
        {
            return GetGuidFromClaim(token, "appToken");
        }

        public static Guid GetMandantGuidFromClaim(ClaimsPrincipal user)
        {
            return GetGuidFromClaim(user, "mandantGuid");
        }

        public static Guid GetMandantGuidFromClaim(JwtSecurityToken token)
        {
            return GetGuidFromClaim(token, "mandantGuid");
        }

        public static string GetUserNameFromClaim(ClaimsPrincipal user)
        {
            return GetStringFromClaim(user, "id");
        }
        public static string GetUserNameFromClaim(JwtSecurityToken token)
        {
            return GetStringFromClaim(token, "id");
        }
        public static Guid GetIDASAuthTokenFromClain(ClaimsPrincipal user)
        {
            return GetGuidFromClaim(user, "idasAuthToken");
        }
    }
}
