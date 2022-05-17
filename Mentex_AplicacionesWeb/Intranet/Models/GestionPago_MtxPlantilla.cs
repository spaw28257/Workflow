using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    /// <summary>
    /// Obtener los datos de la plantilla
    /// </summary>
    public class GestionPago_MtxPlantilla : MensajeError
    {
        //Atributos
        private string vCodigo;
        private string vNombre;
        private string vCodigoSubgrupo;
        private string vFrecuencia;
        private int vSensibilidad;
        private bool vEstatus;
        private string vIdClaseProveedor;
        private string vIdChequera;


        public GestionPago_MtxPlantilla()
        {
            vCodigo = "";
            vNombre = "";
            vCodigoSubgrupo = "";
            vFrecuencia = "";
            vSensibilidad = 0;
            vEstatus = false;
            vIdClaseProveedor = "";
            vIdChequera = "";
        }

        public string Codigo { get => vCodigo; set => vCodigo = value; }
        public string Nombre { get => vNombre; set => vNombre = value; }
        public string CodigoSubgrupo { get => vCodigoSubgrupo; set => vCodigoSubgrupo = value; }
        public string Frecuencia { get => vFrecuencia; set => vFrecuencia = value; }
        public int Sensibilidad { get => vSensibilidad; set => vSensibilidad = value; }
        public bool Estatus { get => vEstatus; set => vEstatus = value; }
        public string IdClaseProveedor { get => vIdClaseProveedor; set => vIdClaseProveedor = value; }
        public string IdChequera { get => vIdChequera; set => vIdChequera = value; }
    }
}