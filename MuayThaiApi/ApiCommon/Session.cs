using MuayThaiApi.Entity.Business;
using MuayThaiApi.Entity.Security;
using System.Security.Claims;

namespace MuayThaiApi.ApiCommon
{
    public class Session
    {
        #region Patron de Diseño
        private static Session _instance;
        private static readonly object _instanceLock = new object();
        public static Session Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new Session();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public CredentialsDtoEn GetCurrentUser(HttpContext httpContext)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var credentialClaims = identity.Claims;

                return new CredentialsDtoEn
                {
                    Persona = new PersonaEn
                    {
                        NombreCompleto = credentialClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value
                    },
                    User = new LoginDtoEn
                    {
                        UserName = credentialClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value
                    }
                };
            }
            return default(CredentialsDtoEn);
        }
    }
}
