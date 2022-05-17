using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    public class Wrkf_GrupoRubro : MensajeError
    {
        /// <summary>
        /// Atributos de la clase
        /// </summary>

        private string vGrupoRubroIdEncript;
        private int vGrupoRubro_Id;
        private string vDescripcion;
        private int vTotalGrupoRubros;

        public Wrkf_GrupoRubro()
        {
           vGrupoRubroIdEncript = "";
           vGrupoRubro_Id = 0;
           vDescripcion = "";
            vTotalGrupoRubros = 0;
        }

        public string GrupoRubroIdEncript { get => vGrupoRubroIdEncript; set => vGrupoRubroIdEncript = value; }
        public int GrupoRubro_Id { get => vGrupoRubro_Id; set => vGrupoRubro_Id = value; }
        public string Descripcion { get => vDescripcion; set => vDescripcion = value; }
        public int TotalGrupoRubros { get => vTotalGrupoRubros; set => vTotalGrupoRubros = value; }
    }
}