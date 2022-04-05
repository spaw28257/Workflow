using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    public class Wrkf_SolicitudOrdenPagoSoporte : MensajeError
    {
        /// <summary>
        /// Atributos
        /// </summary>
        private int Soporte_id;
        private int Solicitudordenpago_Id;
        private int Solicitudordenpagodetalle_Id;
        private string RutaDirectorio;
        private string NombreArchivo;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Wrkf_SolicitudOrdenPagoSoporte()
        {
            Soporte_id = -1;
            Solicitudordenpago_Id = -1;
            Solicitudordenpagodetalle_Id = -1;
            RutaDirectorio = "";
            NombreArchivo = "";
        }

        /// <summary>
        /// Propiedades
        /// </summary>
        public int Soporte_idx { get => Soporte_id; set => Soporte_id = value; }
        public int Solicitudordenpago_Idx { get => Solicitudordenpago_Id; set => Solicitudordenpago_Id = value; }
        public int Solicitudordenpagodetalle_Idx { get => Solicitudordenpagodetalle_Id; set => Solicitudordenpagodetalle_Id = value; }
        public string RutaDirectoriox { get => RutaDirectorio; set => RutaDirectorio = value; }
        public string NombreArchivox { get => NombreArchivo; set => NombreArchivo = value; }
    }
}