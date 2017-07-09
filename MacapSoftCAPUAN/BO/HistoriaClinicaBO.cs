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
        public HistoriaClinicaDALC hcDALC;

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

        public void agregarpaciente(Paciente paciente) {
            hcDALC = new HistoriaClinicaDALC();
            hcDALC.agregarPaciente(paciente);
        }

        public void agregarRemision(Remision remision)
        {
            hcDALC = new HistoriaClinicaDALC();
            hcDALC.agregarRemision(remision);
        }

        public void agregarRemitido(Remitido remitido)
        {
            hcDALC = new HistoriaClinicaDALC();
            hcDALC.agregarRemitido(remitido);
        }
    }
}