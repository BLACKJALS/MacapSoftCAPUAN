using MacapSoftCAPUAN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.DALC
{
    public class DiagnosticosDALC
    {
        public ApplicationDbContext  bd;

        public List<Diagnostico> ListarDiagnosticos() {
            List<Diagnostico> listaDiagnostico = new List<Diagnostico>();
            bd = new ApplicationDbContext();
            listaDiagnostico = bd.diagnosticoContext.ToList();
            return listaDiagnostico;
        }

        public void CrearDiagnostico(string codigo, string nombre, string destacado)
        {
            bd = new ApplicationDbContext();
            Diagnostico diagnostico = new Diagnostico();
            diagnostico.Codigo = codigo;
            diagnostico.Nombre = nombre;
            diagnostico.Destacado = destacado;
            bd.diagnosticoContext.Add(diagnostico);
            bd.SaveChanges();
        }


        public void ModificarDiagnostico(string codigo, string nuevoNombre, string nuevoDestacado)
        {
            bd = new ApplicationDbContext();
            List<Diagnostico> listaDiagnostico = new List<Diagnostico>();
            listaDiagnostico = bd.diagnosticoContext.ToList();

            foreach (var item in listaDiagnostico)
            {
                if (item.Codigo == codigo)
                {
                    //item.Codigo = nuevoDiagnostico.Codigo;
                    item.Nombre = nuevoNombre;
                    item.Destacado = nuevoDestacado;
                    break;
                }
            }
            bd.SaveChanges();
        }

        public Diagnostico mostrarDiagnostico(string id) {
            bd = new ApplicationDbContext();
            Diagnostico diag = new Diagnostico();
            foreach (var item in bd.diagnosticoContext) {
                if (item.Codigo == id) {
                    diag = item;
                }
            }
            return diag;
        }
    }
}