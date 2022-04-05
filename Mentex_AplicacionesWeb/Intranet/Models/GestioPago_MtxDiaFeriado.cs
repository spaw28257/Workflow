using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    /// <summary>
    /// Permite el acceso a los datos de la tabla GestionPago.MTX_DiaFeriado
    /// </summary>
    public class GestioPago_MtxDiaFeriado
    {
        //Atributo
        string Dia;
        string Descripcion;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="dia"></param>
        /// <param name="descripcion"></param>
        public GestioPago_MtxDiaFeriado()
        {
            Dia = "1973-01-01";
            Descripcion = "";
        }

        public string Diax { get => Dia; set => Dia = value; }
        public string Descripcionx { get => Descripcion; set => Descripcion = value; }
    }
}