using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    public class Wrkf_CalcularMontos : MensajeError
    {
        private string porcentajeiva;
        private string montoiva;
        private string porcentajeretencion;
        private string montoretencion;
        private string subtotal;
        private string total;

        public Wrkf_CalcularMontos()
        {
            this.Porcentajeiva = "0,00";
            this.Montoiva = "0,00";
            this.Porcentajeretencion = "0,00";
            this.Montoretencion = "0,00";
            this.Subtotal = "0,00";
            this.Total = "0,00";
        }

        public string Porcentajeiva { get => porcentajeiva; set => porcentajeiva = value; }
        public string Montoiva { get => montoiva; set => montoiva = value; }
        public string Porcentajeretencion { get => porcentajeretencion; set => porcentajeretencion = value; }
        public string Montoretencion { get => montoretencion; set => montoretencion = value; }
        public string Subtotal { get => subtotal; set => subtotal = value; }
        public string Total { get => total; set => total = value; }
    }
}