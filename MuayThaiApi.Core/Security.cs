using MuayThaiApi.Data;
using MuayThaiApi.Data.Models;

namespace MuayThaiApi.Core
{
    public class Security
    {
        #region Patron de Diseño
        private static Security _instance;
        private static readonly object _instanceLock = new object();
        public static Security Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new Security();
                    }
                }
                return _instance;
            }
        }
        #endregion
        #region Metodos publicos
        public List<MetodosPago> Prueba()
        {
            
            return BOSecurity.Instance.Users();
        }
        #endregion
        #region Metodos privados
        #endregion
    }
}