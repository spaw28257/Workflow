using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    public class Wrkf_RespuestaOperacion : MensajeError
    {
        private int NumeroRegistroOrdenPago;
        private int NumeroRegistroDetallePago;
        private int RegistrosProcesados;
        private bool RespuestaSioNo;
        private string Observaciones;

        public Wrkf_RespuestaOperacion()
        {
            NumeroRegistroOrdenPago = -1;
            NumeroRegistroDetallePago = -1;
            RegistrosProcesados = 0;
            RespuestaSioNo = false;
            Observaciones = "";
        }

        public int NumeroRegistroOrdenPagox { get => NumeroRegistroOrdenPago; set => NumeroRegistroOrdenPago = value; }
        public int NumeroRegistroDetallePagox { get => NumeroRegistroDetallePago; set => NumeroRegistroDetallePago = value; }
        public int RegistrosProcesadosx { get => RegistrosProcesados; set => RegistrosProcesados = value; }
        public bool RespuestaSioNox { get => RespuestaSioNo; set => RespuestaSioNo = value; }
        public string Observacionesx { get => Observaciones; set => Observaciones = value; }
    }
}