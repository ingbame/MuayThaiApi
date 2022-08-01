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
    public class ApplicationBo
    {
        #region Patron de Diseño
        private static ApplicationBo _instance;
        private static readonly object _instanceLock = new object();
        public static ApplicationBo Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new ApplicationBo();
                    }
                }
                return _instance;
            }
        }
        #endregion
        #region Metodos publicos
        public ResponseObject<List<MenuItemEn>> GetMenu(string Role)
        {
            var response = new ResponseObject<List<MenuItemEn>>();
            try
            {
                response.Model = DaApplication.Instance.GetMenu(Role);
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
