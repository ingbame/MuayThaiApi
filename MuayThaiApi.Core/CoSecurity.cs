using Microsoft.IdentityModel.Tokens;
using MuayThaiApi.Data;
using MuayThaiApi.Entity.Common;
using MuayThaiApi.Entity.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MuayThaiApi.Core
{
    public class CoSecurity
    {
        #region Patron de Diseño
        private static CoSecurity _instance;
        private static readonly object _instanceLock = new object();
        public static CoSecurity Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new CoSecurity();
                    }
                }
                return _instance;
            }
        }
        #endregion
        #region Metodos publicos
        public ResponseObject<string> LoginAuthentication(LoginEn userReq, JwtConfEn jwtConf)
        {
            var response = new ResponseObject<string>();
            try
            {
                var credential = DaSecurity.Instance.GetUser(userReq.UserName);
                if (credential != null)
                {
                    var existingSalt = credential.User.PasswordSalt;
                    var existingPassword = credential.User.Password;
                    var isPasswordMatched = CompareHashedPasswords(userReq.Password, existingPassword, existingSalt);
                    if (isPasswordMatched)
                    {
                        //Genera Token JWT
                        var responseToken = GenerateLoginToken(credential, jwtConf);
                        response.Model = responseToken;
                    }
                    else
                    {
                        response.Error = true;
                        response.Message = "Contraseña incorrecta";
                    }
                }
                else
                {
                    response.Error = true;
                    response.Message = "El usuario no existe";
                }
                //response = DaSecurity.Instance.Users();
                return response;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
                if (ex.InnerException != null)
                    response.Message += $"\n{ex.InnerException.Message}";
                return response;
            }
        }

        public ResponseObject<CredentialsDtoEn> CreateNewPerson(CredentialsEn newUser)
        {
            var response = new ResponseObject<CredentialsDtoEn>();
            try
            {
                string newSalt = GenerateSalt();
                byte[] hashedPassword = GetHash(newUser.User.Password, newSalt);
                string hashedBase64StringPassword = Convert.ToBase64String(hashedPassword);

                response.Model = DaSecurity.Instance.CreateNewPerson(newUser, newSalt, hashedBase64StringPassword);
                return response;
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.Message = ex.Message;
                if (ex.InnerException != null)
                    response.Message += $"\n{ex.InnerException.Message}";
                return response;
            }
        }
        #endregion
        #region Metodos privados
        private string GenerateSalt()
        {
            string refreshToken = string.Empty;
            byte[] salt;
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt = new byte[16]);
                refreshToken = Convert.ToBase64String(salt);
            }
            return refreshToken;
        }
        private byte[] GetHash(string PlainPassword, string Salt)
        {
            byte[] byteArray = Encoding.Unicode.GetBytes(string.Concat(Salt, PlainPassword));
            var sha256 = new HMACSHA256();
            byte[] hashedBytes = sha256.ComputeHash(byteArray);
            return hashedBytes;
        }
        private bool CompareHashedPasswords(string InputPassword, string ExistingHashedBase64StringPassword, string Salt)
        {
            string inputHashedPassword = Convert.ToBase64String(GetHash(InputPassword, Salt));
            return inputHashedPassword.Equals(ExistingHashedBase64StringPassword);
        }
        private string GenerateLoginToken(CredentialsEn credential, JwtConfEn jwtConf)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConf.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, credential.User.UserName),
                new Claim(ClaimTypes.Name, "Prueba Nombre"),
                new Claim(ClaimTypes.Role,"SuperAdmin")
            };

            var token = new JwtSecurityToken(
                jwtConf.Issuer,
                jwtConf.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}