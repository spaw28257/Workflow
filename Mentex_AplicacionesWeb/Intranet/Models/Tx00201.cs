using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    /// <summary>
    /// Detalles del plan de impuesto
    /// </summary>
    public class Tx00201 : MensajeError
    {
        //Atributos
        private string taxdtlid;
        private string txdtldsc;
        private double txdtlpct;

        /// <summary>
        /// Constructor
        /// </summary>
        public Tx00201()
        {
            taxdtlid = "";
            txdtldsc = "";
            txdtlpct = 0.00;
        }

        /// <summary>
        /// Propiedades
        /// </summary>
        public string Taxdtlid { get => taxdtlid; set => taxdtlid = value; }
        public string Txdtldsc { get => txdtldsc; set => txdtldsc = value; }
        public double Txdtlpct { get => txdtlpct; set => txdtlpct = value; }
    }
}