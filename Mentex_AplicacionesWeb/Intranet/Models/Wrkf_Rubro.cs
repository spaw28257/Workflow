using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    public class Wrkf_Rubro : MensajeError
    {
        /// <summary>
        /// Atributos de la clase
        /// </summary>
        private string RubroEncript_Id;
        private string Rubro_Id;
        private string Descripcion;
        private int Cantidad_Pagos;
        private string GruporubroEncript_Id;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Wrkf_Rubro()
        {
            RubroEncript_Id = "";
            Rubro_Id = "";
            Descripcion = "";
            Cantidad_Pagos = 0;
            GruporubroEncript_Id = "";
        }

        /// <summary>
        /// Propiedades de la clase
        /// </summary>
        
        public string RubroEncript_Idx { get => RubroEncript_Id; set => RubroEncript_Id = value; }
        public string Rubro_Idx { get => Rubro_Id; set => Rubro_Id = value; }
        public string Descripcionx { get => Descripcion; set => Descripcion = value; }

        /// <summary>
        /// Especifica la cantidad de pagos asociados al rubro se utiliza para la interfaz de la revisión de CxP
        /// </summary>
        public int Cantidad_Pagosx { get => Cantidad_Pagos; set => Cantidad_Pagos = value; }

        public string GruporubroEncript_Idx { get => GruporubroEncript_Id; set => GruporubroEncript_Id = value; }
    }
}