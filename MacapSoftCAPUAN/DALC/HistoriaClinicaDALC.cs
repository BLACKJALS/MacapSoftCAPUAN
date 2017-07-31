using MacapSoftCAPUAN.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.ServiceModel;
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

        public void agregarIngresoClinica(IngresoClinica ingresoClinica)
        {
            try
            {
                bd = new ApplicationDbContext();
                bd.ingresoClinicaContext.Add(ingresoClinica);
                //bd.Entry(ingresoClinica.EntidadRemitente).State = EntityState.Detached;
                bd.SaveChanges();
            }
            catch (Exception e)
            {

                System.ArgumentException argxEx = new System.ArgumentException("Error al guardar los datos de ingreso.", e.Message);
                throw argxEx;
            }
            
        }

        public List<IngresoClinica> listarIngresoClinica()
        {
            bd = new ApplicationDbContext();
            List<IngresoClinica> listaIngresoClinica = new List<IngresoClinica>();
            listaIngresoClinica = bd.ingresoClinicaContext.ToList();
            return listaIngresoClinica;
        }

        public string agregarPaciente(Paciente paciente)
        {

            try
            {
                bd = new ApplicationDbContext();
                bd.pacienteContext.Add(paciente);
                //bd.Entry(paciente.barrio).State = EntityState.Unchanged;//Esto permite que en la tabla barrio no se agregue otro registro que viene del formulario
                //bd.Entry(paciente.eps).State = EntityState.Unchanged;
                //bd.Entry(paciente.localidad).State = EntityState.Unchanged;
                //bd.Entry(paciente.tipoDocumento).State = EntityState.Unchanged;
                //bd.Entry(paciente.paises).State = EntityState.Unchanged;
                //bd.Entry(paciente.id_estrato).State = EntityState.Unchanged;
                bd.SaveChanges();
                return "paciente creado existosamente";
            }
            catch (Exception e)
            {
                System.ArgumentException argxEx = new System.ArgumentException("No se pudo crear el paciente es posible que el numero de historica clínica ya exista.",e);
                throw argxEx;

            }
        }


        public string agregarConsultante(Consultante consultante)
        {

            try
            {
                bd = new ApplicationDbContext();
                bd.consultanteContext.Add(consultante);
                //bd.Entry(consultante.tipoDocumento).State = EntityState.Unchanged;//Esto permite que en la tabla barrio no se agregue otro registro que viene del formulario
                bd.SaveChanges();
                return "Consultante creado existosamente";
            }
            catch (Exception e)
            {

                System.ArgumentException argxEx = new System.ArgumentException("No se pudo crear el paciente ya existe un consultante con ese documento registrado.", e);
                throw argxEx;

            }

        }



        public string agregarConsultantePaciente(consultantePaciente consultantePa)
        {

            try
            {
                bd = new ApplicationDbContext();
                bd.consultantePacienteContext.Add(consultantePa);
                //bd.Entry(consultantePa.idConsultante.tipoDocumento).State = EntityState.Detached;
                //bd.Entry(consultantePa.idConsultante).State = EntityState.Unchanged;
                bd.Entry(consultantePa.IdConsultante).State = EntityState.Unchanged;
                bd.Entry(consultantePa.IdPaciente).State = EntityState.Unchanged;
                bd.SaveChanges();
                return "ConsultantePaciente creado existosamente";
            }
            catch (Exception e)
            {

                System.ArgumentException argxEx = new System.ArgumentException("No se pudo crear el paciente ya existe la información del consultante paciente con ese documentos de identidad registrado.", e);
                throw argxEx;

            }

        }


        public void agregarConsecutivo(Consecutivo consecutivo)
        {
            bd = new ApplicationDbContext();
            bd.consecutivoContext.Add(consecutivo);
            bd.SaveChanges();
        }


        public string agregarRemisionL(List<Remision> remision)
        {
            try
            {
                bd = new ApplicationDbContext();
                foreach (var item in remision)
                {
                    bd.remisionContext.Add(item);
                }
                bd.SaveChanges();
                return "Se ha creado correctamente la remisión.";
            }
            catch (Exception ex)
            {
                System.ArgumentException argxEx = new System.ArgumentException("No se pudo crear la remisión.", ex.Message);
                throw argxEx;
            }
            
        }

        public List<Remision> listaPacientesRemitidos() {
            bd = new ApplicationDbContext();
            var listaUsuarios = bd.pacienteContext;
            var listaRemisiones = bd.remisionContext.ToList();
            return listaRemisiones;
        }


        public List<MotivosRemisiones> listaMotivosRemisiones()
        {
            bd = new ApplicationDbContext();
            var listaMotivosRemisiones = bd.motivosRemisionesContext.ToList();
            return listaMotivosRemisiones;
        }

        public string modificarPaciente(Paciente paciente)
        {

            try
            {
                var pacienteMod = new Paciente { numeroHistoriaClinica = paciente.numeroHistoriaClinica};
                using (var context = new ApplicationDbContext())
                {
                    context.pacienteContext.Attach(pacienteMod);
                    pacienteMod.nombre = paciente.nombre;
                    pacienteMod.apellido = paciente.apellido;
                    pacienteMod.numeroDocumento = paciente.numeroDocumento;
                    pacienteMod.id_tipoDocumento = paciente.id_tipoDocumento;
                    pacienteMod.fechaNacimiento = paciente.fechaNacimiento;
                    pacienteMod.id_estrato = paciente.id_estrato;
                    pacienteMod.id_localidad = paciente.id_localidad;
                    pacienteMod.id_barrio = paciente.id_barrio;
                    pacienteMod.id_Eps = paciente.id_Eps;
                    pacienteMod.direccion = paciente.direccion;
                    pacienteMod.edad = paciente.edad;
                    pacienteMod.email = paciente.email;
                    pacienteMod.idUser = paciente.idUser;
                    pacienteMod.id_ciudad = paciente.id_ciudad;
                    pacienteMod.telefono = paciente.telefono;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                System.ArgumentException argxEx = new System.ArgumentException("No se pudo actualizar el paciente creado.", e.Message);
                throw argxEx;
            }
            return "Exito";
        }

        public List<Remitido> listarRemitido()
        {
            bd = new ApplicationDbContext();
            List<Remitido> listaRemision = new List<Remitido>();
            listaRemision = bd.remitidoContext.ToList();
            return listaRemision;
        }

        public string modificarIngresoCl(IngresoClinica ingresoCl)
        {

            try
            {
                var ingresoClMod = new IngresoClinica { idIngresoClinica = ingresoCl.idIngresoClinica };
                using (var context = new ApplicationDbContext())
                {
                    context.ingresoClinicaContext.Attach(ingresoClMod);
                    ingresoClMod.fechaIngreso = ingresoCl.fechaIngreso;
                    ingresoCl.id_paciente = ingresoCl.id_paciente;
                    ingresoClMod.observaciones = ingresoCl.observaciones;
                    ingresoClMod.motivoConsulta = ingresoCl.motivoConsulta;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

                System.ArgumentException argxEx = new System.ArgumentException("No se pudo actualizar la informacion de ingreso nueva.", e.Message);
                throw argxEx;
            }
            return "Exito";
        }
    }
}