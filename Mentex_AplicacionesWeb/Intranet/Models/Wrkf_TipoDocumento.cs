using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    public class Wrkf_TipoDocumento
    {
        /// <summary>
        /// Atributos
        /// </summary>
        private int tipodocumento_Id;
        private string codigo;
        private string documento;

        /// <summary>
        /// Constructor
        /// </summary>
        public Wrkf_TipoDocumento()
        {
            tipodocumento_Id = 0;
            codigo = string.Empty;
            documento = string.Empty;
        }

        /// <summary>
        /// Propiedades
        /// </summary>
        public int Tipodocumento_Id { get => tipodocumento_Id; set => tipodocumento_Id = value; }
        public string Codigo { get => codigo; set => codigo = value; }
        public string Documento { get => documento; set => documento = value; }
    }
}