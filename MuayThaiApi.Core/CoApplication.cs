using MuayThaiApi.Data;
using MuayThaiApi.Entity.Application;
using MuayThaiApi.Entity.Common;
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
        public ResponseObject<List<MenuItemEn>> GetMenu(int id)
        {
            var response = new ResponseObject<List<MenuItemEn>>();
            try
            {
                response.Model = DaApplication.Instance.GetMenu(id);
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
        #endregion
    }
}
