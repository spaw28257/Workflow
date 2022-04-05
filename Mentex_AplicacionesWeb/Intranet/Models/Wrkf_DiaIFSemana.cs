using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    /// <summary>
    /// Permite obtener el primer y ultimo día de la semana
    /// </summary>
    public class Wrkf_DiaIFSemana
    {
        /// <summary>
        /// Atributos
        /// </summary>
        private string primerdiasemana;
        private string ultimodiasemana;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Wrkf_DiaIFSemana()
        {
        }

        /// <summary>
        /// Propiedades
        /// </summary>
        public string Primerdiasemana { get => primerdiasemana; set => primerdiasemana = value; }
        public string Ultimodiasemana { get => ultimodiasemana; set => ultimodiasemana = value; }
    }
}