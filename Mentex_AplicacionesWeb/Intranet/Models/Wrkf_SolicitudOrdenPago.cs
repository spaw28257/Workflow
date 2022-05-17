using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    /// <summary>
    /// La clase permite acceder a los datos de la tabla SolicitudOrdenPagoDetalle
    /// </summary>
    public class Wrkf_SolicitudOrdenPago : MensajeError
    {
        /// <summary>
        /// Atributos de la clase
        /// </summary>
        private string vCodigo;
        private int vGrupoRubro_Id;
        private string vGrupoRubroDescripcion;
        private string vRubro_Id;
        private string vRubroDescripcion;
        private int vTipodocumento;
        private string vDescripcionDocumento;
        private int vTipoPago;
        private string vDescripcionTipoPago;
        private double vMontoIva;
        private double vMontoRetenido;
        private string vPlanImpuesto;
        private string vCodigoMoneda;
        private string vObservaciones;
        private string vUsuarioAnulacion;
        private string vFechaAnulacion;
        private bool vAnulada;
        private bool vSoportesRecibido;
        private bool vPagoUrgente;
        private string vUsuarioAprobacionCxP;
        private string vFechaAprobacionCxP;
        private string vObservacionCxP;
        private string vUsuarioAprobacionSubContr;
        private string vFechaAprobacionSubContr;
        private string vObservacionSubContra;
        private string vUsuarioAprobacionContralor;
        private string vFechaAprobacionContralor;
        private string vObservacionContralor;
        private string vUsuarioAprobacionVPFinanza;
        private string vFechaAprobacionVPFinanza;
        private string vObservacionVPFinanza;
        private string vUsuarioRevisionCxP;
        private string vFechaRevisionCxP;
        private string vObservacionRevisionCxP;

        //GAP
        private string vGAPCodigoItem;
        private string vGAPCodigoPlantilla;
        private string vGAPDescripcionPlantilla;
        private double vGAPMonto;
        private string vGAPDescripcionFactura;
        private string vGAPFechaFactura;
        private string vGAPFechaPago;
        private int vGAPEstatus;
        private string vGAPFechaAprobacion;
        private string vGAPUsuarioAprueba;
        private string vGAPFechaRechazo;
        private string vGAPUsuarioRechaza;
        private string vGAPNroFactura;
        private string vGAPNroControl;
        private string vGAPTipoPago;
        private int vGAPCodigoConcepto;
        private string vGAPIdProveedor;
        private string vGAPProveedor;
        private string vGAPRIF;
        private string vGAPCuentaBancaria;
        private string vGAPEmail;
        private int vGAPPorcentajeRetencion;
        private double vGAPMontoExento;
        private double vGAPBaseImpIVAGeneral;
        private double vGAPBaseImpIVAReducido;
        private double vGAPBaseImpIVAAdicional;
        private string vGAPTienda;
        private string vGAPIdChequera;

        public Wrkf_SolicitudOrdenPago()
        {
            //WorkFlow
            vCodigo = "";
            vGrupoRubro_Id = 0;
            vGrupoRubroDescripcion = "";
            vRubro_Id = "";
            vRubroDescripcion = "";
            vTipodocumento = 0;
            vDescripcionDocumento = "";
            vTipoPago = 0;
            vDescripcionTipoPago = "";
            vMontoIva = 0.00;
            vMontoRetenido = 0.00;
            vPlanImpuesto = "";
            vCodigoMoneda = "";
            vObservaciones = "";
            vUsuarioAnulacion = "";
            vFechaAnulacion = "19000101";
            vAnulada = false;
            vSoportesRecibido = false;
            vPagoUrgente = false;
            vUsuarioAprobacionCxP = "";
            vFechaAprobacionCxP = "";
            vObservacionCxP = "";
            vUsuarioAprobacionSubContr = "";
            vFechaAprobacionSubContr = "1900010";
            vObservacionSubContra = "";
            vUsuarioAprobacionContralor = "";
            vFechaAprobacionContralor = "19000101";
            vObservacionContralor = "";
            vUsuarioAprobacionVPFinanza = "";
            vFechaAprobacionVPFinanza = "19000101";
            vObservacionVPFinanza = "";
            vUsuarioRevisionCxP = "";
            vFechaRevisionCxP = "19000101";
            vObservacionRevisionCxP = "";

            //GAP
            vGAPCodigoItem = "";
            vGAPCodigoPlantilla = "";
            vGAPDescripcionPlantilla = "";
            vGAPMonto = 0.00;
            vGAPDescripcionFactura = "";
            vGAPFechaFactura = "19000101";
            vGAPFechaPago = "19000101";
            vGAPEstatus = 2;
            vGAPFechaAprobacion = "19000101";
            vGAPUsuarioAprueba = "";
            vGAPFechaRechazo = "19000101";
            vGAPUsuarioRechaza = "";
            vGAPNroFactura = "";
            vGAPNroControl = "";
            vGAPTipoPago = "T";
            vGAPCodigoConcepto = 0;
            vGAPIdProveedor = "";
            vGAPProveedor = "";
            vGAPRIF = "";
            vGAPCuentaBancaria = "";
            vGAPEmail = "";
            vGAPPorcentajeRetencion = 0;
            vGAPMontoExento = 0.00;
            vGAPBaseImpIVAGeneral = 0.00;
            vGAPBaseImpIVAReducido = 0.00;
            vGAPBaseImpIVAAdicional = 0.00;
            vGAPTienda = "";
            vGAPIdChequera = "";
        }

        //Workflow
        public string Codigo { get => vCodigo; set => vCodigo = value; }
        public int GrupoRubro_Id { get => vGrupoRubro_Id; set => vGrupoRubro_Id = value; }
        public string GrupoRubroDescripcion { get => vGrupoRubroDescripcion; set => vGrupoRubroDescripcion = value; }
        public string Rubro_Id { get => vRubro_Id; set => vRubro_Id = value; }
        public string RubroDescripcion { get => vRubroDescripcion; set => vRubroDescripcion = value; }
        public int Tipodocumento { get => vTipodocumento; set => vTipodocumento = value; }
        public string DescripcionDocumento { get => vDescripcionDocumento; set => vDescripcionDocumento = value; }
        public int TipoPago { get => vTipoPago; set => vTipoPago = value; }
        public string DescripcionTipoPago { get => vDescripcionTipoPago; set => vDescripcionTipoPago = value; }
        public double MontoIva { get => vMontoIva; set => vMontoIva = value; }
        public double MontoRetenido { get => vMontoRetenido; set => vMontoRetenido = value; }
        public string PlanImpuesto { get => vPlanImpuesto; set => vPlanImpuesto = value; }
        public string CodigoMoneda { get => vCodigoMoneda; set => vCodigoMoneda = value; }
        public string Observaciones { get => vObservaciones; set => vObservaciones = value; }
        public string UsuarioAnulacion { get => vUsuarioAnulacion; set => vUsuarioAnulacion = value; }
        public string FechaAnulacion { get => vFechaAnulacion; set => vFechaAnulacion = value; }
        public bool Anulada { get => vAnulada; set => vAnulada = value; }
        public bool SoportesRecibido { get => vSoportesRecibido; set => vSoportesRecibido = value; }
        public bool PagoUrgente { get => vPagoUrgente; set => vPagoUrgente = value; }
        public string UsuarioAprobacionCxP { get => vUsuarioAprobacionCxP; set => vUsuarioAprobacionCxP = value; }
        public string FechaAprobacionCxP { get => vFechaAprobacionCxP; set => vFechaAprobacionCxP = value; }
        public string ObservacionCxP { get => vObservacionCxP; set => vObservacionCxP = value; }
        public string UsuarioAprobacionSubContr { get => vUsuarioAprobacionSubContr; set => vUsuarioAprobacionSubContr = value; }
        public string FechaAprobacionSubContr { get => vFechaAprobacionSubContr; set => vFechaAprobacionSubContr = value; }
        public string ObservacionSubContra { get => vObservacionSubContra; set => vObservacionSubContra = value; }
        public string UsuarioAprobacionContralor { get => vUsuarioAprobacionContralor; set => vUsuarioAprobacionContralor = value; }
        public string FechaAprobacionContralor { get => vFechaAprobacionContralor; set => vFechaAprobacionContralor = value; }
        public string ObservacionContralor { get => vObservacionContralor; set => vObservacionContralor = value; }
        public string UsuarioAprobacionVPFinanza { get => vUsuarioAprobacionVPFinanza; set => vUsuarioAprobacionVPFinanza = value; }
        public string FechaAprobacionVPFinanza { get => vFechaAprobacionVPFinanza; set => vFechaAprobacionVPFinanza = value; }
        public string ObservacionVPFinanza { get => vObservacionVPFinanza; set => vObservacionVPFinanza = value; }
        public string UsuarioRevisionCxP { get => vUsuarioRevisionCxP; set => vUsuarioRevisionCxP = value; }
        public string FechaRevisionCxP { get => vFechaRevisionCxP; set => vFechaRevisionCxP = value; }
        public string ObservacionRevisionCxP { get => vObservacionRevisionCxP; set => vObservacionRevisionCxP = value; }

        //GAP
        public string GAPCodigoItem { get => vGAPCodigoItem; set => vGAPCodigoItem = value; }
        public string GAPCodigoPlantilla { get => vGAPCodigoPlantilla; set => vGAPCodigoPlantilla = value; }
        public string GAPDescripcionPlantilla { get => vGAPDescripcionPlantilla; set => vGAPDescripcionPlantilla = value; }
        public double GAPMonto { get => vGAPMonto; set => vGAPMonto = value; }
        public string GAPDescripcionFactura { get => vGAPDescripcionFactura; set => vGAPDescripcionFactura = value; }
        public string GAPFechaFactura { get => vGAPFechaFactura; set => vGAPFechaFactura = value; }
        public string GAPFechaPago { get => vGAPFechaPago; set => vGAPFechaPago = value; }
        public int GAPEstatus { get => vGAPEstatus; set => vGAPEstatus = value; }
        public string GAPFechaAprobacion { get => vGAPFechaAprobacion; set => vGAPFechaAprobacion = value; }
        public string GAPUsuarioAprueba { get => vGAPUsuarioAprueba; set => vGAPUsuarioAprueba = value; }
        public string GAPFechaRechazo { get => vGAPFechaRechazo; set => vGAPFechaRechazo = value; }
        public string GAPUsuarioRechaza { get => vGAPUsuarioRechaza; set => vGAPUsuarioRechaza = value; }
        public string GAPNroFactura { get => vGAPNroFactura; set => vGAPNroFactura = value; }
        public string GAPNroControl { get => vGAPNroControl; set => vGAPNroControl = value; }
        public string GAPTipoPago { get => vGAPTipoPago; set => vGAPTipoPago = value; }
        public int GAPCodigoConcepto { get => vGAPCodigoConcepto; set => vGAPCodigoConcepto = value; }
        public string GAPIdProveedor { get => vGAPIdProveedor; set => vGAPIdProveedor = value; }
        public string GAPProveedor { get => vGAPProveedor; set => vGAPProveedor = value; }
        public string GAPRIF { get => vGAPRIF; set => vGAPRIF = value; }
        public string GAPCuentaBancaria { get => vGAPCuentaBancaria; set => vGAPCuentaBancaria = value; }
        public string GAPEmail { get => vGAPEmail; set => vGAPEmail = value; }
        public int GAPPorcentajeRetencion { get => vGAPPorcentajeRetencion; set => vGAPPorcentajeRetencion = value; }
        public double GAPMontoExento { get => vGAPMontoExento; set => vGAPMontoExento = value; }
        public double GAPBaseImpIVAGeneral { get => vGAPBaseImpIVAGeneral; set => vGAPBaseImpIVAGeneral = value; }
        public double GAPBaseImpIVAReducido { get => vGAPBaseImpIVAReducido; set => vGAPBaseImpIVAReducido = value; }
        public double GAPBaseImpIVAAdicional { get => vGAPBaseImpIVAAdicional; set => vGAPBaseImpIVAAdicional = value; }
        public string GAPTienda { get => vGAPTienda; set => vGAPTienda = value; }
        public string GAPIdChequera { get => vGAPIdChequera; set => vGAPIdChequera = value; }
    }
}