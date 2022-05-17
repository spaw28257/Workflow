using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    public class Wrkf_CalcularMontos : MensajeError
    {
        private double vporcentajeiva;
        private double vmontoiva;
        private double vporcentajeretencion;
        private double vmontoretencion;
        private double vtotalapagar;

        public Wrkf_CalcularMontos()
        {
            vporcentajeiva = 0.00;
            vmontoiva = 0.00;
            vporcentajeretencion = 0.00;
            vmontoretencion = 0.00;
            vtotalapagar = 0.00;
        }

        public double Porcentajeiva { get => vporcentajeiva; set => vporcentajeiva = value; }
        public double Montoiva { get => vmontoiva; set => vmontoiva = value; }
        public double Porcentajeretencion { get => vporcentajeretencion; set => vporcentajeretencion = value; }
        public double Montoretencion { get => vmontoretencion; set => vmontoretencion = value; }
        public double Totalapagar { get => vtotalapagar; set => vtotalapagar = value; }
    }
}