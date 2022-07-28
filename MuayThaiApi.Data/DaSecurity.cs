using Microsoft.EntityFrameworkCore;
using MuayThaiApi.Data.Models;
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
        public List<_UserDto> Users()
        {
            using (var ctx = new IngbameDbContext())
            {
                return ctx.Users.Select(s => s.CopyProperties(new _UserDto())).ToList();
            }
        }
        public _Afiliado AltaAfiliado(_Afiliado persona)
        {
            try
            {                
                using (var ctx = new IngbameDbContext())
                {
                    Persona afiliadoNuevo = persona.CopyProperties(new Persona());
                    User userNuevo = persona.Credenciales.CopyProperties(new User());

                    var trans = ctx.Database.BeginTransaction();
                    var userResult = ctx.Users.Where(w => w.UserName == userNuevo.UserName).FirstOrDefault();
                    if(userResult != null)
                        throw new Exception("Nombre de usuario registrado ya existe.");
                    var addUser = ctx.Users.Add(userNuevo);
                    if (addUser.State != EntityState.Added)
                        throw new Exception("No se agregó el usuario correctamente");
                    ctx.SaveChanges();
                    //persona.Credenciales = addUser.Entity.CopyProperties(new _User());
                    afiliadoNuevo.UserId = userNuevo.UserId;
                    var addAfiliado = ctx.Personas.Add(afiliadoNuevo);
                    if (addAfiliado.State != EntityState.Added)
                        throw new Exception("No se agregó el afiliado correctamente");
                    ctx.SaveChanges();
                    trans.Commit();
                    persona = afiliadoNuevo.CopyProperties(new _Afiliado());
                    persona.Credenciales = userNuevo.CopyProperties(new _User());
                    return persona;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public _AfiliadoDto GetAfiliado(int? id)
        {
            try
            {
                using (var ctx = new IngbameDbContext())
                {
                    var afiliadoResult = ctx.Personas.Find(id);
                    if (afiliadoResult == null)
                        throw new Exception("No se encontró afiliado.");
                    var userResult = ctx.Users.Find(afiliadoResult.UserId);
                    if (userResult == null)
                        throw new Exception("No se encontró usuario relacionado al afiliado");
                    var afiliadoReturn = afiliadoResult.CopyProperties(new _AfiliadoDto());
                    afiliadoReturn.Credenciales = userResult.CopyProperties(new _UserDto());

                    return afiliadoReturn;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #region Metodos privados
        #endregion
    }
}
