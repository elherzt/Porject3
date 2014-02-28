using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Proyecto3.Models.Security
{
    public class MyRoleProvider : RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            //Implementacion para obtener los roles del usuario

            using (pruebaDataContext db = new pruebaDataContext())
            {
                //PRUEBAS_TA_01_CAT_USUARIOS user = db.PRUEBAS_TA_01_CAT_USUARIOS.FirstOrDefault(u => u.T_USUARIO.Equals(username, StringComparison.CurrentCultureIgnoreCase) || u.T_EMAIL.Equals(username, StringComparison.CurrentCultureIgnoreCase));
                var user = (from us in db.PRUEBAS_TA_01_CAT_USUARIOS where us.T_USUARIO == username select us).SingleOrDefault();
                var roles = from rol in db.Roles where rol.RoleID == user.RoleID select rol.RoleName;
                           
                if (roles != null)
                    return roles.ToArray();
                else
                    return new string[] { }; ;
            }

            /// termina implementacion
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            using (pruebaDataContext db = new pruebaDataContext())
            {
                //PRUEBAS_TA_01_CAT_USUARIOS user = db.PRUEBAS_TA_01_CAT_USUARIOS.FirstOrDefault(u => u.T_USUARIO.Equals(username, StringComparison.CurrentCultureIgnoreCase) || u.T_EMAIL.Equals(username, StringComparison.CurrentCultureIgnoreCase));
                var user = (from us in db.PRUEBAS_TA_01_CAT_USUARIOS where us.T_USUARIO == username select us).SingleOrDefault();
                var roles = from rol in db.Roles where rol.RoleID == user.RoleID select rol.RoleName;

                if (user != null)
                    return roles.Any(r => r.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));
                else
                    return false;
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}