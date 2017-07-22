using MacapSoftCAPUAN.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.DALC
{
    public class HistoriaClinicaDALC
    {
        public ApplicationDbContext bd;

        public List<Barrios> listarBarrios()
        {
            bd = new ApplicationDbContext();
            List<Barrios> listaBarrios = new List<Barrios>();
            listaBarrios = bd.barriosContext.ToList();
            return listaBarrios;
        }

        public List<Paciente> listarPacientes() {
            bd = new ApplicationDbContext();
            var listaPacientes = bd.pacienteContext.ToList();
            return listaPacientes;
        }

        public List<Estrato> listaEstratos()
        {
            bd = new ApplicationDbContext();
            var listaEstrato = bd.estratoContext.ToList();
            return listaEstrato;
        }

        public List<TiposDocumentos> listaTipoDocumento() {
            bd = new ApplicationDbContext();
            var listaDocumento = bd.tipoDocumentoContext.ToList();
            return listaDocumento;
        }

        public List<Localidades> listarLocalidades()
        {
            bd = new ApplicationDbContext();
            List<Localidades> listaLocalidades = new List<Localidades>();
            listaLocalidades = bd.localidadesContext.ToList();
            return listaLocalidades;
        }

        public List<Ciudades> listarCiudades()
        {
            bd = new ApplicationDbContext();
            List<Ciudades> listaCiudades = new List<Ciudades>();
            listaCiudades = bd.ciudadesContext.ToList();
            return listaCiudades;
        }


        public List<Paises> listarPaises()
        {
            bd = new ApplicationDbContext();
            List<Paises> listaPaises = new List<Paises>();
            listaPaises = bd.paisesContext.ToList();
            return listaPaises;

        }

        public List<Eps> listarEps()
        {
            bd = new ApplicationDbContext();
            List<Eps> listaEps = new List<Eps>();
            listaEps = bd.epsConext.ToList();
            return listaEps;
        }

        public List<Consecutivo> listarConsecutivo() {
            bd = new ApplicationDbContext();
            List<Consecutivo> listaConsecutivo = new List<Consecutivo>();
            listaConsecutivo = bd.consecutivoContext.ToList();
            return listaConsecutivo;
        }

        public void agregarRemision(Remision remision)
        {
                bd = new ApplicationDbContext();
                bd.remisionContext.Add(remision);
                bd.SaveChanges();
        }

        public void agregarRemitido(Remitido remitido)
        {
            bd = new ApplicationDbContext();
            bd.remitidoContext.Add(remitido);
            bd.SaveChanges();
        }

        public void agregarPaciente(Paciente paciente)
        {

            try
            {
                bd = new ApplicationDbContext();
                bd.pacienteContext.Add(paciente);
                bd.Entry(paciente.barrio).State = EntityState.Unchanged;//Esto permite que en la tabla barrio no se agregue otro registro que viene del formulario
                bd.Entry(paciente.eps).State = EntityState.Unchanged;
                bd.Entry(paciente.localidad).State = EntityState.Unchanged;
                bd.Entry(paciente.tipoDocumento).State = EntityState.Unchanged;
                bd.Entry(paciente.paises).State = EntityState.Unchanged;
                bd.Entry(paciente.estrato).State = EntityState.Unchanged;
                bd.SaveChanges();
            }
            catch (Exception e)
            {
                
                throw;

            }
        }


        public void agregarConsultante(Consultante consultante)
        {

            try
            {
                bd = new ApplicationDbContext();
                bd.consultanteContext.Add(consultante);
                bd.Entry(consultante.tipoDocumento).State = EntityState.Unchanged;//Esto permite que en la tabla barrio no se agregue otro registro que viene del formulario
                bd.SaveChanges();
            }
            catch (Exception e)
            {

                throw;

            }

        }



        public void agregarConsultantePaciente(consultantePaciente consultantePa)
        {

            try
            {
                bd = new ApplicationDbContext();
                bd.consultantePacienteContext.Add(consultantePa);
                bd.Entry(consultantePa.idConsultante.tipoDocumento).State = EntityState.Detached;
                bd.Entry(consultantePa.idPaciente.barrio).State = EntityState.Detached;
                bd.Entry(consultantePa.idPaciente.localidad).State = EntityState.Detached;
                bd.Entry(consultantePa.idPaciente.tipoDocumento).State = EntityState.Detached;
                bd.Entry(consultantePa.idPaciente.eps).State = EntityState.Detached;
                bd.Entry(consultantePa.idPaciente.paises).State = EntityState.Detached;
                bd.Entry(consultantePa.idPaciente.estrato).State = EntityState.Detached;
                bd.Entry(consultantePa.idConsultante).State = EntityState.Unchanged;
                bd.Entry(consultantePa.idPaciente).State = EntityState.Unchanged;
                bd.SaveChanges();
            }
            catch (Exception e)
            {

                throw;

            }

        }


        public void agregarConsecutivo(Consecutivo consecutivo)
        {
            bd = new ApplicationDbContext();
            bd.consecutivoContext.Add(consecutivo);
            bd.SaveChanges();
        }


        public void agregarRemisionL(List<Remision> remision)
        {
            bd = new ApplicationDbContext();
            foreach (var item in remision) {
                bd.remisionContext.Add(item);
                //bd.Entry(item.paciente.barrio).State = EntityState.Detached;
                //bd.Entry(item.paciente.localidad).State = EntityState.Detached;
                //bd.Entry(item.paciente.tipoDocumento).State = EntityState.Detached;
                //bd.Entry(item.paciente.eps).State = EntityState.Detached;
                //bd.Entry(item.paciente.paises).State = EntityState.Detached;
                //bd.Entry(item.paciente.estrato).State = EntityState.Detached;
                bd.Entry(item.paciente).State = EntityState.Unchanged;
                //bd.Entry(item.serviSoli).State = EntityState.Unchanged;
                //bd.Entry(item.diagnostico).State = EntityState.Unchanged;
                //bd.Entry(item.motivoRemision).State = EntityState.Unchanged;
            }
            bd.SaveChanges();
        }
    }
}