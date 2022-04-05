using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    /// <summary>
    /// Clase permite manipular los datos de tabla [Workflow].[tbl_MensajeError]
    /// </summary>
    public class MensajeError
    {
        /// <summary>
        /// Atributos de la clase
        /// </summary>
        private int Mensaje_Id;
        private string Codigo;
        private string Mensaje;
        private string Modulo;
        private string Tipo;
        private string Titulo;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public MensajeError()
        {
            this.Mensaje_Id = -1;
            this.Codigo = "";
            this.Mensaje = "";
            this.Modulo = "";
            this.Tipo = "";
            this.Titulo = "";
        }

        /// <summary>
        /// Propiedades de la clase
        /// </summary>
        public int Mensaje_Idx { get => Mensaje_Id; set => Mensaje_Id = value; }
        public string Codigox { get => Codigo; set => Codigo = value; }
        public string Mensajex { get => Mensaje; set => Mensaje = value; }
        public string Modulox { get => Modulo; set => Modulo = value; }
        public string Tipox { get => Tipo; set => Tipo = value; }
        public string Titulox { get => Titulo; set => Titulo = value; }
    }
}