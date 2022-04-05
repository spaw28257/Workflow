using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    /// <summary>
    /// La clase permite obtener las monedas configuradas desde la tabla Workflow.Moneda
    /// </summary>
    public class Wrkf_Moneda
    {
        private string curncyid; //ID de moneda
        private string crncydsc; //Descripción de la moneda
        private string crncysym; //Símbolo de moneda
        private bool inactiva; //Moneda esta inactiva o no

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Wrkf_Moneda()
        {
            this.Curncyid = "";
            this.Crncydsc = "";
            this.Crncysym = "";
            this.inactiva = false;
        }

        public string Curncyid { get => curncyid; set => curncyid = value; }
        public string Crncydsc { get => crncydsc; set => crncydsc = value; }
        public string Crncysym { get => crncysym; set => crncysym = value; }
        public bool Inactiva { get => inactiva; set => inactiva = value; }
    }
}