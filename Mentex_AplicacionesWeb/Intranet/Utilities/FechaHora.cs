using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Intranet.Models;

namespace Intranet.Utilities
{
    public class FechaHora : MensajeError
    {
        private string fecha_actual;
        private DateTime fecha_actual2;

        public FechaHora()
        {
            this.fecha_actual = "";
            this.fecha_actual2 = DateTime.Now;
        }

        public string Fecha_actual { get => fecha_actual; set => fecha_actual = value; }
        public DateTime Fecha_Actual2 { get => fecha_actual2; set => fecha_actual2 = value; }
    }
}