using MuayThaiApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuayThaiApi.Data
{
    public class BOSecurity
    {
        #region Patron de Diseño
        private static BOSecurity _instance;
        private static readonly object _instanceLock = new object();
        public static BOSecurity Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new BOSecurity();
                    }
                }
                return _instance;
            }
        }
        #endregion
        public List<MetodosPago> Users()
        {
            using (var ctx = new IngbameDbContext())
            {
                return ctx.MetodosPagos.ToList();
            }
        }
    }
}
