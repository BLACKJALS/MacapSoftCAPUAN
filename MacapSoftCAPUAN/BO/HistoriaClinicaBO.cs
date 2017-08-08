using MacapSoftCAPUAN.DALC;
using MacapSoftCAPUAN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MacapSoftCAPUAN.BO
{
    public class HistoriaClinicaBO
    {
        private HistoriaClinicaDALC hcDALC;

        public List<Barrios> listarBarrios() {
            hcDALC = new HistoriaClinicaDALC();
            var listaBarrios = hcDALC.listarBarrios();
            return listaBarrios; 
        }

        public List<Localidades> listarLocalidades()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaLicalidades = hcDALC.listarLocalidades();
            return listaLicalidades;
        }

        public List<Ciudades> listarCiudades()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaCiudades = hcDALC.listarCiudades();
            return listaCiudades;
        }

        public List<Paises> listarPaises()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaPaises = hcDALC.listarPaises();
            return listaPaises;
        }

        public List<Eps> listarEps()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaEps = hcDALC.listarEps();
            return listaEps;
        }


        public string agregarpaciente(Paciente paciente)
        {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.agregarPaciente(paciente);
        }

        public string agregarConsultante(Consultante consultante) {
            hcDALC = new HistoriaClinicaDALC();
            return hcDALC.agregarConsultante(consultante);
        }
        
        //public string agregarConsultantePaciente(consultantePaciente consultantePa)
        //{
        //    hcDALC = new HistoriaClinicaDALC();
        //    return hcDALC.agregarConsultantePaciente(consultantePa);
        //}

        public void agregarRemision(Remision remision)
        {
            hcDALC = new HistoriaClinicaDALC();
            hcDALC.agregarRemision(remision);
        }

        public string agregarRemitido(Remitido remitido)
        {
            try
            {
                hcDALC = new HistoriaClinicaDALC();
                hcDALC.agregarRemitido(remitido);
                return "Creado existoso";
            }
            catch (Exception e)
            {

                System.ArgumentException argxEx = new System.ArgumentException("No se pudo agregar la remision.", e.Message);
                throw argxEx;
            }
        }

        public List<Paciente> listarPaciente() {
            hcDALC = new HistoriaClinicaDALC();
            var pacientes = hcDALC.listarPacientes().ToList();
            return pacientes;
        }

        public List<TiposDocumentos> listaTiposDocumento()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaDocumento = hcDALC.listaTipoDocumento();
            return listaDocumento;
        }

        public List<Consecutivo> listarConsecutivo() {
            hcDALC = new HistoriaClinicaDALC();
            var listaConsecutivo = hcDALC.listarConsecutivo();
            return listaConsecutivo;
        }


        public List<Estrato> listarEstrato() {
            hcDALC = new HistoriaClinicaDALC();
            var listaEstrato = hcDALC.listaEstratos();
            return listaEstrato;
        }


        public void agregarConsecutivo(Consecutivo consecutivo) {
            hcDALC = new HistoriaClinicaDALC();
            hcDALC.agregarConsecutivo(consecutivo);
        }

        public void agregarListaRemision(List<Remision> remision)
        {
            hcDALC = new HistoriaClinicaDALC();
            hcDALC.agregarRemisionL(remision);
        }

        public List<Remision> listarRemisiones() {
            hcDALC = new HistoriaClinicaDALC();
            var listaPr = hcDALC.listaPacientesRemitidos();
            return listaPr;
        }

        public List<MotivosRemisiones> listarMotivosRemisiones()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaMotivosR = hcDALC.listaMotivosRemisiones();
            return listaMotivosR;
        }

        public string modificarPaciente(Paciente paciente)
        {
            try
            {
                hcDALC = new HistoriaClinicaDALC();
                hcDALC.modificarPaciente(paciente);
                return "Se ha actualizado correctamente el paciente";
            }
            catch (Exception e)
            {

                System.ArgumentException argxEx = new System.ArgumentException("No se pudo actualizar el paciente creado.", e.Message);
                throw argxEx;
            }
            
        }

        public string ingresoClinica(IngresoClinica ingresoClinica) {
            try
            {
                hcDALC = new HistoriaClinicaDALC();
                hcDALC.agregarIngresoClinica(ingresoClinica);
                return "Se ha creado el ingreso clinica correctamente del paciente";
            }
            catch (Exception e)
            {

                System.ArgumentException argxEx = new System.ArgumentException("No se pudo guardar la información del ingreso del paciente.", e.Message);
                throw argxEx;
            }

        }
        

        public List<IngresoClinica> listarIngresoClinica()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaIngrCl = hcDALC.listarIngresoClinica();
            return listaIngrCl;
        }

        public List<Remitido> listarRemitido() {
            hcDALC = new HistoriaClinicaDALC();
            var listaRem = hcDALC.listarRemitido();
            return listaRem;
        }

        public string modificarIngresoCl(IngresoClinica ingrCl) {

            try
            {
                hcDALC = new HistoriaClinicaDALC();
                return hcDALC.modificarIngresoCl(ingrCl);
            }
            catch (Exception e)
            {
                System.ArgumentException argxEx = new System.ArgumentException(e.Message);
                throw argxEx;
            }
            
        }

        public List<Sexo> listarSexo()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaSexo = hcDALC.listarSexoPac();
            return listaSexo;
        }


        public List<NivelEscolaridad> listarNivelEscolaridad()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaNivelEscolaridad = hcDALC.listarlistaNivelEscolaridad();
            return listaNivelEscolaridad;
        }

        public List<Ocupacion> listarOcupacion()
        {
            hcDALC = new HistoriaClinicaDALC();
            var listaOcupacion = hcDALC.listarOcupacion();
            return listaOcupacion;
        }
    }
}