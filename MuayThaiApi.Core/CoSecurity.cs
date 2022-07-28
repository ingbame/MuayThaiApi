using MuayThaiApi.Data;
using MuayThaiApi.Entity.Security;

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
        public List<_UserDto> Prueba()
        {
            
            return DaSecurity.Instance.Users();
        }
        public _Afiliado AltaAfiliado(_Afiliado persona)
        {

            return DaSecurity.Instance.AltaAfiliado(persona);
        }
        public _AfiliadoDto GetAfiliado(int? id)
        {

            return DaSecurity.Instance.GetAfiliado(id);
        }
        #endregion
        #region Metodos privados
        #endregion
    }
}