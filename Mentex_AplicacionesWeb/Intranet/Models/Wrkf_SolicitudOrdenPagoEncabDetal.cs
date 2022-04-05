using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    public class Wrkf_SolicitudOrdenPagoEncabDetal : MensajeError
    {
        /// <summary>
        /// Atributos
        /// </summary>

        private string Solicitudordenpago_Id;
        private string Codigoplantilla;
        private string Nombreplantilla;
        private string Recibidocxp;
        private string Aprobadocontraloria;
        private string Aprobadovp;
        private string Aplicadotesoreria;
        private string Anulada;
        private string Fecharegistro;
        private string Usuarioregistro;
        private string Fechamodificacion;
        private string Usuariomodifico;
        private string curncyid;
        private string Urgente;
        private string Enviadoacxp;
        private string TotalSolicitudPago;
        private string UsuarioAnulacion;
        private string FechaAnulacion;
        private string Solicitudordenpagodetalle_Id;
        private string Rif;
        private string Proveedor;
        private string Descripcion;
        private string Numerodocumento;
        private string Cantidad;
        private string Preciounitario;
        private string Subtotal;
        private string Anticipo;
        private string Total;
        private string Aprobado;
        private string Tipodocumento;
        private string DescripTipoDocumento;
        private string Calculaiva;
        private string Realizaretencion;
        private string Porcentajeiva;
        private string MontoIva;
        private string Porcentajeretencion;
        private string Totalretenido;
        private string Gruporubro_Id;
        private string DescripGruporubro;
        private string Rubro_Id;
        private string DescripRubro;
        private string Fechapago;
        private string formapago_Id;
        private string DescripFormaPago;
        private string observaciones;
        private string anuladaDetalle;
        private string UsuarioAnulacionDetalle;
        private string FechaAnulacionDetalle;
        private string Chekbkid;
        private string UsuariomodificoDetalle;
        private string FechamodificoDetalle;
        private string DocumentoRecibido;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Wrkf_SolicitudOrdenPagoEncabDetal()
        {            
            Solicitudordenpago_Id = string.Empty;
            Codigoplantilla = string.Empty;
            Nombreplantilla = string.Empty;
            Recibidocxp = string.Empty;
            Aprobadocontraloria = string.Empty;
            Aprobadovp = string.Empty;
            Aplicadotesoreria = string.Empty;
            Anulada = string.Empty;
            Fecharegistro = string.Empty;
            Usuarioregistro = string.Empty;
            Fechamodificacion = string.Empty;
            Usuariomodifico = string.Empty;
            curncyid = string.Empty;
            Urgente = string.Empty;
            Enviadoacxp = string.Empty;
            TotalSolicitudPago = string.Empty;
            UsuarioAnulacion = string.Empty;
            FechaAnulacion = string.Empty;
            Solicitudordenpagodetalle_Id = string.Empty;
            Rif = string.Empty;
            Proveedor = string.Empty;
            Descripcion = string.Empty;
            Numerodocumento = string.Empty;
            Cantidad = string.Empty;
            Preciounitario = string.Empty;
            Subtotal = string.Empty;
            Anticipo = string.Empty;
            Total = string.Empty;
            Aprobado = string.Empty;
            Tipodocumento = string.Empty;
            DescripTipoDocumento = string.Empty;
            Calculaiva = string.Empty;
            Realizaretencion = string.Empty;
            Porcentajeiva = string.Empty;
            MontoIva = string.Empty;
            Porcentajeretencion = string.Empty;
            Totalretenido = string.Empty;
            Gruporubro_Id = string.Empty;
            DescripGruporubro = string.Empty;
            Rubro_Id = string.Empty;
            DescripRubro = string.Empty;
            Fechapago = string.Empty;
            formapago_Id = string.Empty;
            DescripFormaPago = string.Empty;
            observaciones = string.Empty;
            anuladaDetalle = string.Empty;
            UsuarioAnulacionDetalle = string.Empty;
            FechaAnulacionDetalle = string.Empty;
            Chekbkid = string.Empty;
            UsuariomodificoDetalle = string.Empty;
            FechamodificoDetalle = string.Empty;
            DocumentoRecibido = string.Empty;
            
        }

        /// <summary>
        /// Propiedades
        /// </summary>
        public string Solicitudordenpago_Idx { get => Solicitudordenpago_Id; set => Solicitudordenpago_Id = value; }
        public string Codigoplantillax { get => Codigoplantilla; set => Codigoplantilla = value; }
        public string Nombreplantillax { get => Nombreplantilla; set => Nombreplantilla = value; }
        public string Recibidocxpx { get => Recibidocxp; set => Recibidocxp = value; }
        public string Aprobadocontraloriax { get => Aprobadocontraloria; set => Aprobadocontraloria = value; }
        public string Aprobadovpx { get => Aprobadovp; set => Aprobadovp = value; }
        public string Aplicadotesoreriax { get => Aplicadotesoreria; set => Aplicadotesoreria = value; }
        public string Anuladax { get => Anulada; set => Anulada = value; }
        public string Fecharegistrox { get => Fecharegistro; set => Fecharegistro = value; }
        public string Usuarioregistrox { get => Usuarioregistro; set => Usuarioregistro = value; }
        public string Fechamodificacionx { get => Fechamodificacion; set => Fechamodificacion = value; }
        public string Usuariomodificox { get => Usuariomodifico; set => Usuariomodifico = value; }
        public string Curncyid { get => curncyid; set => curncyid = value; }
        public string Urgentex { get => Urgente; set => Urgente = value; }
        public string Enviadoacxpx { get => Enviadoacxp; set => Enviadoacxp = value; }
        public string TotalSolicitudPagox { get => TotalSolicitudPago; set => TotalSolicitudPago = value; }
        public string UsuarioAnulacionx { get => UsuarioAnulacion; set => UsuarioAnulacion = value; }
        public string FechaAnulacionx { get => FechaAnulacion; set => FechaAnulacion = value; }
        public string Solicitudordenpagodetalle_Idx { get => Solicitudordenpagodetalle_Id; set => Solicitudordenpagodetalle_Id = value; }
        public string Rifx { get => Rif; set => Rif = value; }
        public string Proveedorx { get => Proveedor; set => Proveedor = value; }
        public string Descripcionx { get => Descripcion; set => Descripcion = value; }
        public string Numerodocumentox { get => Numerodocumento; set => Numerodocumento = value; }
        public string Cantidadx { get => Cantidad; set => Cantidad = value; }
        public string Preciounitariox { get => Preciounitario; set => Preciounitario = value; }
        public string Subtotalx { get => Subtotal; set => Subtotal = value; }
        public string Anticipox { get => Anticipo; set => Anticipo = value; }
        public string Totalx { get => Total; set => Total = value; }
        public string Aprobadox { get => Aprobado; set => Aprobado = value; }
        public string Tipodocumentox { get => Tipodocumento; set => Tipodocumento = value; }
        public string DescripTipoDocumentox { get => DescripTipoDocumento; set => DescripTipoDocumento = value; }
        public string Calculaivax { get => Calculaiva; set => Calculaiva = value; }
        public string Realizaretencionx { get => Realizaretencion; set => Realizaretencion = value; }
        public string Porcentajeivax { get => Porcentajeiva; set => Porcentajeiva = value; }
        public string MontoIvax { get => MontoIva; set => MontoIva = value; }
        public string Porcentajeretencionx { get => Porcentajeretencion; set => Porcentajeretencion = value; }
        public string Totalretenidox { get => Totalretenido; set => Totalretenido = value; }
        public string Gruporubro_Idx { get => Gruporubro_Id; set => Gruporubro_Id = value; }
        public string DescripGrupoRubrox { get => DescripGruporubro; set => DescripGruporubro = value; }
        public string Rubro_Idx { get => Rubro_Id; set => Rubro_Id = value; }
        public string DescripRubrox { get => DescripRubro; set => DescripRubro = value; }
        public string Fechapagox { get => Fechapago; set => Fechapago = value; }
        public string Formapago_Idx { get => formapago_Id; set => formapago_Id = value; }
        public string DescripFormaPagox { get => DescripFormaPago; set => DescripFormaPago = value; }
        public string Observacionesx { get => observaciones; set => observaciones = value; }
        public string anuladaDetallex { get => anuladaDetalle; set => anuladaDetalle = value; }
        public string UsuarioAnulacionDetallex { get => UsuarioAnulacionDetalle; set => UsuarioAnulacionDetalle = value; }
        public string FechaAnulacionDetallex { get => FechaAnulacionDetalle; set => FechaAnulacionDetalle = value; }
        public string Chekbkidx { get => Chekbkid; set => Chekbkid = value; }
        public string UsuariomodificoDetallex { get => UsuariomodificoDetalle; set => UsuariomodificoDetalle = value; }
        public string FechamodificoDetallex { get => FechamodificoDetalle; set => FechamodificoDetalle = value; }
        public string DocumentoRecibidox { get => DocumentoRecibido; set => DocumentoRecibido = value; }
    }
}