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
        private string vRubro_IdEncript;
        private string vRubro_Id;
        private string vDescripcion;
        private int vGrupoRubro_Id;
        private int vTotalRubros;
        private int vPagoUrgente;

        public Wrkf_Rubro()
        {
            vRubro_IdEncript = "";
            vRubro_Id = "";
            vDescripcion = "";
            vGrupoRubro_Id = 0;
            vTotalRubros = 0;
            vPagoUrgente = 0;
        }

        /*Metodos*/
        public string Rubro_IdEncript { get => vRubro_IdEncript; set => vRubro_IdEncript = value; }
        public string Rubro_Id { get => vRubro_Id; set => vRubro_Id = value; }
        public string Descripcion { get => vDescripcion; set => vDescripcion = value; }
        public int GrupoRubro_Id { get => vGrupoRubro_Id; set => vGrupoRubro_Id = value; }
        public int TotalRubros { get => vTotalRubros; set => vTotalRubros = value; }
        public int PagoUrgente { get => vPagoUrgente; set => vPagoUrgente = value; }
    }
}