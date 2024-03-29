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
        public List<MenuItemEn> GetMenu(string Role)
        {
            using (var ctx = new IngbameDbContext())
            {
                var getRole = ctx.Roles.Where(w => w.RoleDescription.Equals(Role)).FirstOrDefault();
                var itemsMenu = ctx.AssignRoleMenus.Where(w => w.RoleId == getRole.RoleId).Select(s => s.MenuItemId).ToList();
                if (!itemsMenu.Any())
                    throw new Exception("No existe el rol asignado.");
                var result = ctx.MenuItems.Where(w => itemsMenu.Contains(w.MenuItemId)).Select(s => s.CopyProperties(new MenuItemEn())).ToList();
                if (!result.Any())
                    throw new Exception("No existe el menu para el rol que tiene este usuario.");
                return result;
            }
        }
        #endregion
        #region Metodos privados
        #endregion
    }
}
