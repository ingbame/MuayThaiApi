﻿using MuayThaiApi.Data.Models;
using MuayThaiApi.Entity.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuayThaiApi.Data
{
    public class DaApplication
    {
        #region Patron de Diseño
        private static DaApplication _instance;
        private static readonly object _instanceLock = new object();
        public static DaApplication Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_instanceLock)
                    {
                        if (_instance == null)
                            _instance = new DaApplication();
                    }
                }
                return _instance;
            }
        }
        #endregion
        #region Metodos publicos
        public List<MenuItemModel> GetMenu(int id)
        {
            using (var ctx = new IngbameDbContext())
            {
                var itemsMenu = ctx.AssignRoleMenus.Where(w => w.RolId == id).Select(s => s.MenuItemId).ToList();
                var result = ctx.MenuItems.Where(w => itemsMenu.Contains(w.MenuItemId)).Select(s => s.CopyProperties(new MenuItemModel())).ToList();
                return result;
            }
        }
        #endregion
        #region Metodos privados
        #endregion
    }
}