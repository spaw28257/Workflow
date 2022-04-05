using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    /// <summary>
    /// La clase permite acceder a los datos de la tabla SolicitudOrdenPago
    /// </summary>
    public class Wrkf_SolicitudOrdenPago : MensajeError
    {
        /// <summary>
        /// Atributos de la clase
        /// </summary>
        private int Solicitudordenpago_Id;
        private string Codigoplantilla;
        private string Nombreplantilla;
        private int Codigogrupo;
        private string Codigosubgrupo;
        private bool Recibidocxp;
        private bool Aprobadocontraloria;
        private bool Aprobadovp;
        private bool Aplicadotesoreria;
        private bool Anulada;
        private DateTime Fecharegistro;
        private string Usuarioregistro;
        private DateTime Fechamodificacion;
        private string Usuariomodifico;
        private string curncyid;
        private bool Urgente;
        private string FechaReg;
        private string TotalSolicitudPago;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Wrkf_SolicitudOrdenPago()
        {
            Solicitudordenpago_Id = -1;
            Codigoplantilla = "";
            Nombreplantilla = "";
            Codigogrupo = -1;
            Codigosubgrupo = "";
            Recibidocxp = false;
            Aprobadocontraloria = false;
            Aprobadovp = false;
            Aplicadotesoreria = false;
            Anulada = false;
            Fecharegistro = Convert.ToDateTime("1973-01-01 00:00:00");
            Usuarioregistro = "";
            Fechamodificacion = Convert.ToDateTime("1973-01-01 00:00:00");
            Usuariomodifico = "";
            curncyid = "";
            Urgente = false;
            FechaReg = "";
            TotalSolicitudPago = "0,00";
        }

        /// <summary>
        /// Propiedades de la clase
        /// </summary>
        public int Solicitudordenpago_Idx { get => Solicitudordenpago_Id; set => Solicitudordenpago_Id = value; }
        public string Codigoplantillax { get => Codigoplantilla; set => Codigoplantilla = value; }
        public string Nombreplantillax { get => Nombreplantilla; set => Nombreplantilla = value; }
        public int Codigogrupox { get => Codigogrupo; set => Codigogrupo = value; }
        public string Codigosubgrupox { get => Codigosubgrupo; set => Codigosubgrupo = value; }
        public bool Recibidocxpx { get => Recibidocxp; set => Recibidocxp = value; }
        public bool Aprobadocontraloriax { get => Aprobadocontraloria; set => Aprobadocontraloria = value; }
        public bool Aprobadovpx { get => Aprobadovp; set => Aprobadovp = value; }
        public bool Aplicadotesoreriax { get => Aplicadotesoreria; set => Aplicadotesoreria = value; }
        public bool Anuladax { get => Anulada; set => Anulada = value; }
        public DateTime Fecharegistrox { get => Fecharegistro; set => Fecharegistro = value; }
        public string Usuarioregistrox { get => Usuarioregistro; set => Usuarioregistro = value; }
        public DateTime Fechamodificacionx { get => Fechamodificacion; set => Fechamodificacion = value; }
        public string Usuariomodificox { get => Usuariomodifico; set => Usuariomodifico = value; }
        public string curncyidx { get => curncyid; set => curncyid = value; }
        public bool Urgentex { get => Urgente; set => Urgente = value; }
        public string FechaRegx { get => FechaReg; set => FechaReg = value; }
        public string TotalSolicitudPagox { get => TotalSolicitudPago; set => TotalSolicitudPago = value; }
    }
}