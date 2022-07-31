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
                var response = new CredentialsEn
                {
                    User = ctx.Users.Where(w => w.UserName.ToLower().Equals(user.ToLower())).Select(s => s.CopyProperties(new UserEn())).FirstOrDefault()
                };
                return response;
            }
        }
        public CredentialsDtoEn CreateNewPerson(CredentialsEn newUser, string salt, string password)
        {
            using (var ctx = new IngbameDbContext())
            {
                var personaToAdd = newUser.Persona.CopyProperties(new Persona());
                var userToAdd = newUser.User.CopyProperties(new User());
                userToAdd.Password = password;
                userToAdd.PasswordSalt = salt;

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
                var addPersona = ctx.Personas.Add(personaToAdd);
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
