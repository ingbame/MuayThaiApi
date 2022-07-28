using MuayThaiApi.Data;
using MuayThaiApi.Entity.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuayThaiApi.Core
{
    public class CoApplication
    {
        #region Patron de Diseño
        private static CoApplication _instance;
        private static readonly object _instanceLock = new object();
        public static CoApplication Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new CoApplication();
                    }
                }
                return _instance;
            }
        }
        #endregion
        #region Metodos publicos
        public List<MenuItemModel> GetMenu(int id)
        {

            return DaApplication.Instance.GetMenu(id);
        }
        #endregion
        #region Metodos privados
        #endregion
    }
}
