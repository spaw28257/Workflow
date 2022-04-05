using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    public class Wrkf_Departamento : MensajeError
    {
        /// <summary>
        /// Atributos de la clase
        /// </summary>

        private string DepartamentoEncript_Id;
        private int Departamento_Id;
        private string Departamento;
        private int TotalRubros;

        public Wrkf_Departamento()
        {
            DepartamentoEncript_Id = "";
            Departamento_Id = 0;
            Departamento = "";
            TotalRubros = 0;
        }

        public string DepartamentoEncript_Idx { get => DepartamentoEncript_Id; set => DepartamentoEncript_Id = value; }
        public int Departamento_Idx { get => Departamento_Id; set => Departamento_Id = value; }
        public string Departamentox { get => Departamento; set => Departamento = value; }
        public int TotalRubrosx { get => TotalRubros; set => TotalRubros = value; }
    }
}