using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    /// <summary>
    /// La clase permite acceder al detalle de la solicitud de orden de pago
    /// </summary>
    public class Wrkf_SolicitudOrdenPagoDetalle : MensajeError
    {
        private int Solicitudordenpagodetalle_Id;
        private int Solicitudordenpago_Id;
        private string Rif;
        private string Proveedor;
        private string Descripcion;
        private string Numerodocumento;
        private string Cantidad;
        private string Preciounitario;
        private string Subtotal;
        private string Anticipo;
        private string Total;
        private bool Aprobado;
        private int TipoDocumento;
        private bool Calculaiva;
        private bool Realizaretencion;
        private string Porcentajeiva;
        private string Montoiva;
        private string Porcentajeretencion;
        private string Totalretenido;
        private int Gruporubro_Id;
        private string Rubro_Id;
        private DateTime Fechapago;
        private string FechaPag;
        private int formapago_Id;
        private string observaciones;
        private DateTime FechaDocumento;
        private string FechaDocu;
        private string FechaAprobacionSubContr;
        private string UsuarioAprobacionSubContr;
        private string ObservacionRechaSubContra;
        private string FechaAprobacionContralor;
        private string UsuarioAprobacionContralor;
        private string ObservacionesContralor;
        private string FechaAprobacionVPFinanza;
        private string UsuarioAprobacionVPFinanza;
        private string ObservacionesVPFinanza;
        private bool PagoUrgente;
        private string IdProveedor;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Wrkf_SolicitudOrdenPagoDetalle()
        {
            Solicitudordenpagodetalle_Id = -1;
            Solicitudordenpago_Id = -1;
            Rif = string.Empty;
            Proveedor = string.Empty;
            Descripcion = string.Empty;
            Numerodocumento = string.Empty;
            Cantidad = "1,00";
            Preciounitario = "0,00";
            Subtotal = "0.00";
            Anticipo = "0.00";
            Total = "0.00";
            Aprobado = false;
            TipoDocumento = 0;
            Calculaiva = true;
            Realizaretencion = true;
            Porcentajeiva = "0.00";
            Montoiva = "0.00";
            Porcentajeretencion = "0.00";
            Totalretenido = "0.00";
            Gruporubro_Id = 0;
            Rubro_Id = "";
            Fechapago = Convert.ToDateTime("1973-01-01 00:00:00");
            FechaPag = "";
            formapago_Id = 0;
            observaciones = "";
            FechaDocumento = Convert.ToDateTime("1973-01-01 00:00:00");
            FechaDocu = "";
            FechaAprobacionSubContr = "";
            UsuarioAprobacionSubContr = "";
            ObservacionRechaSubContra = "";
            FechaAprobacionContralor = "";
            UsuarioAprobacionContralor = "";
            ObservacionesContralor = "";
            FechaAprobacionVPFinanza = "";
            UsuarioAprobacionVPFinanza = "";
            ObservacionesVPFinanza = "";
            PagoUrgente = false;
            IdProveedor = "";
        }

        public int Solicitudordenpagodetalle_Idx { get => Solicitudordenpagodetalle_Id; set => Solicitudordenpagodetalle_Id = value; }
        public int Solicitudordenpago_Idx { get => Solicitudordenpago_Id; set => Solicitudordenpago_Id = value; }
        public string Rifx { get => Rif; set => Rif = value; }
        public string Proveedorx { get => Proveedor; set => Proveedor = value; }
        public string Descripcionx { get => Descripcion; set => Descripcion = value; }
        public string Numerodocumentox { get => Numerodocumento; set => Numerodocumento = value; }
        public string Cantidadx { get => Cantidad; set => Cantidad = value; }
        public string Preciounitariox { get => Preciounitario; set => Preciounitario = value; }
        public string Subtotalx { get => Subtotal; set => Subtotal = value; }
        public string Anticipox { get => Anticipo; set => Anticipo = value; }
        public string Totalx { get => Total; set => Total = value; }
        public bool Aprobadox { get => Aprobado; set => Aprobado = value; }
        public int TipoDocumentox { get => TipoDocumento; set => TipoDocumento = value; }
        public bool Calculaivax { get => Calculaiva; set => Calculaiva = value; }
        public bool Realizaretencionx { get => Realizaretencion; set => Realizaretencion = value; }
        public string Porcentajeivax { get => Porcentajeiva; set => Porcentajeiva = value; }
        public string Montoivax { get => Montoiva; set => Montoiva = value; }
        public string Porcentajeretencionx { get => Porcentajeretencion; set => Porcentajeretencion = value; }
        public string Totalretenidox { get => Totalretenido; set => Totalretenido = value; }
        public int Gruporubro_Idx { get => Gruporubro_Id; set => Gruporubro_Id = value; }
        public string Rubro_Idx { get => Rubro_Id; set => Rubro_Id = value; }
        public DateTime Fechapagox { get => Fechapago; set => Fechapago = value; }
        public string FechaPagx { get => FechaPag; set => FechaPag = value; }
        public int Formapago_Idx { get => formapago_Id; set => formapago_Id = value; }
        public string Observacionesx { get => observaciones; set => observaciones = value; }
        public DateTime FechaDocumentox { get => FechaDocumento; set => FechaDocumento = value; }
        public string FechaDocux { get => FechaDocu; set => FechaDocu = value; }
        public string FechaAprobacionSubContrx { get => FechaAprobacionSubContr; set => FechaAprobacionSubContr = value; }
        public string UsuarioAprobacionSubContrx { get => UsuarioAprobacionSubContr; set => UsuarioAprobacionSubContr = value; }
        public string ObservacionRechaSubContrax { get => ObservacionRechaSubContra; set => ObservacionRechaSubContra = value; }
        public string FechaAprobacionContralorx { get => FechaAprobacionContralor; set => FechaAprobacionContralor = value; }
        public string UsuarioAprobacionContralorx { get => UsuarioAprobacionContralor; set => UsuarioAprobacionContralor = value; }
        public string ObservacionesContralorx { get => ObservacionesContralor; set => ObservacionesContralor = value; }
        public string FechaAprobacionVPFinanzax { get => ObservacionesContralor; set => ObservacionesContralor = value; }
        public string UsuarioAprobacionVPFinanzax { get => ObservacionesContralor; set => ObservacionesContralor = value; }
        public string ObservacionesVPFinanzax { get => ObservacionesContralor; set => ObservacionesContralor = value; }
        public bool PagoUrgentex { get => PagoUrgente; set => PagoUrgente = value; }
        public string IdProveedorx { get => IdProveedor; set => IdProveedor = value; }
    }
}