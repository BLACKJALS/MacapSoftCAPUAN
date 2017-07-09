using MacapSoftCAPUAN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.DALC
{
    public class UsuarioDALC
    {
        public ApplicationDbContext bd;

        public void editarUsuario(ApplicationUser user,bool activo) {
            bd = new ApplicationDbContext();
            var usuarios = bd.Users.ToList();
            foreach (var item in usuarios) {
                if (item.Id == user.Id) {
                    item.Activo = activo;
                    break;
                }
            }
            bd.SaveChanges();
        }

        public void listarUsuario(ApplicationUser user, string Contraseña)
        {
            bd = new ApplicationDbContext();
            var usuarios = bd.Users.ToList();
            foreach (var item in usuarios)
            {
                if (user.Id == item.Id)
                {
                    item.PasswordHash = Contraseña;
                    break;
                }
            }
            bd.SaveChanges();
        }


        public List<ApplicationUser> listarUsuarios()
        {
            bd = new ApplicationDbContext();
            var usuarios = bd.Users.ToList();
            return usuarios;
        }
    }
}