using Microsoft.EntityFrameworkCore;
using MuayThaiApi.Data.Models;
using MuayThaiApi.Entity.Business;
using MuayThaiApi.Entity.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuayThaiApi.Data
{
    public class DaSecurity
    {
        #region Patron de Diseño
        private static DaSecurity _instance;
        private static readonly object _instanceLock = new object();
        public static DaSecurity Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new DaSecurity();
                    }
                }
                return _instance;
            }
        }
        #endregion
        #region Metodos publicos
        public CredentialsEn GetUser(string user)
        {
            using (var ctx = new IngbameDbContext())
            {
                var response = default(CredentialsEn);
                var userResult = ctx.Users.Where(w => w.UserName.ToLower().Equals(user.ToLower())).FirstOrDefault();
                if (userResult != null)
                {
                    response = new CredentialsEn();
                    response.User = userResult.CopyProperties(new UserEn());
                    var getRole = ctx.Roles.Find(userResult.RoleId);
                    response.User.Role = getRole.CopyProperties(new RoleEn());
                    var getPerson = ctx.People.Where(w => w.UserId == userResult.UserId).FirstOrDefault();
                    response.Persona = getPerson.CopyProperties(new PersonaEn());
                }
                return response;
            }
        }
        public CredentialsDtoEn CreateNewPerson(CredentialsEn newUser, string salt, string password)
        {
            using (var ctx = new IngbameDbContext())
            {
                var personaToAdd = newUser.Persona.CopyProperties(new Person());
                var getDefaultRole = ctx.Roles.Where(w => w.RoleDescription.Equals("Usuario")).FirstOrDefault();
                var userToAdd = new User
                {
                    UserName = newUser.User.UserName,
                    Password = password,
                    PasswordSalt = salt,
                    RoleId = getDefaultRole.RoleId
                };

                var trans = ctx.Database.BeginTransaction();
                var userResult = ctx.Users.Where(w => w.UserName == userToAdd.UserName).FirstOrDefault();
                if (userResult != null)
                    throw new Exception("Nombre de usuario registrado ya existe.");
                var addUser = ctx.Users.Add(userToAdd);
                if (addUser.State != EntityState.Added)
                    throw new Exception("No se agregó el usuario correctamente");
                ctx.SaveChanges();

                //Validar que la persona exista, pendiente
                personaToAdd.UserId = userToAdd.UserId;
                var addPersona = ctx.People.Add(personaToAdd);
                if (addPersona.State != EntityState.Added)
                    throw new Exception("No se agregó el afiliado correctamente");
                ctx.SaveChanges();

                trans.Commit();

                var response = new CredentialsDtoEn
                {
                    Persona = personaToAdd.CopyProperties(new PersonaEn()),
                    User = userToAdd.CopyProperties(new LoginDtoEn())
                };

                return response;
            }
        }
        #endregion
        #region Metodos privados
        #endregion
    }
}
