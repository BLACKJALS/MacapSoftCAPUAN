using MacapSoftCAPUAN.DALC;
using MacapSoftCAPUAN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.BO
{
    public class AccountBO
    {
        public void editarUsuario(ApplicationUser user, bool estado) {
            UsuarioDALC userDALC = new UsuarioDALC();
            userDALC.editarUsuario(user, estado);
        }


        public void listarUsuarios(ApplicationUser user, string Contraseña)
        {
            UsuarioDALC userDALC = new UsuarioDALC();
            userDALC.listarUsuario(user, Contraseña);
        }

    }
}