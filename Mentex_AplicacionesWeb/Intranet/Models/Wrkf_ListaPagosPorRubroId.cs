using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    //permite mostrar un listado de los pagos asociadoos a un rubro en especifico
    public class Wrkf_ListaPagosPorRubroId : MensajeError
    {
        private int Solicitudordenpagodetalle_Id;
        private int Solicitudordenpago_Id;
        private string documento;
        private string Proveedor;
        private string Descripcion;
        private string Numerodocumento;
        private string Total;
        private string TotalGlobalAPagar;
        private string DescripcionRubro;
        private string FechaPago;
        private string FechaRegistro;
        private string formadepago;
        private int formapago_Id;
        private string Observaciones;
        private string Chequera;
        private bool DocumentoRecibido;
        private int TotalDocumentos;
        private string ObservacionRechaSubContra;
        private string curncyid;
        private string FechaDocumento;
        private bool PagoUrgente;

        private string Codigoplantilla;
        private string Nombreplantilla;
        private string Usuarioregistro;
        private string CodigoSop;
        private string Rif;
        private string Preciounitario;
        private string Anticipo;
        private string Subtotal;
        private string Porcentajeiva;
        private string Montoiva;
        private string Porcentajeretencion;
        private string Totalretenido;

        private string Estatus;
        private string Prioridad;



        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Wrkf_ListaPagosPorRubroId()
        {
            Solicitudordenpagodetalle_Id = -1;
            Solicitudordenpago_Id = -1;
            documento = "";
            Proveedor = "";
            Descripcion = "";
            Numerodocumento = "";
            Total = "0,00";
            TotalGlobalAPagar = "0,00";
            DescripcionRubro = "";
            FechaPago = "";
            FechaRegistro = "";
            formadepago = "";
            formapago_Id = 0;
            Observaciones = "";
            Chequera = "";
            DocumentoRecibido = false;
            TotalDocumentos = 0;
            ObservacionRechaSubContra = "";
            curncyid = "";
            FechaDocumento = "";
            PagoUrgente = false;

            Codigoplantilla = "";
            Nombreplantilla = "";
            Usuarioregistro = "";
            CodigoSop = "";
            Rif = "";
            Preciounitario = "0,00";
            Anticipo = "0,00";
            Subtotal = "0,00";
            Porcentajeiva = "0,00";
            Montoiva = "0,00";
            Porcentajeretencion = "0,00";
            Totalretenido = "0,00";

            Estatus = "";
            Prioridad = "";
        }

        /// <summary>
        /// Propiedades
        /// </summary>
        public int Solicitudordenpagodetalle_Idx { get => Solicitudordenpagodetalle_Id; set => Solicitudordenpagodetalle_Id = value; }
        public int Solicitudordenpago_Idx { get => Solicitudordenpago_Id; set => Solicitudordenpago_Id = value; }
        public string Documentox { get => documento; set => documento = value; }
        public string Proveedorx { get => Proveedor; set => Proveedor = value; }
        public string Descripcionx { get => Descripcion; set => Descripcion = value; }
        public string Numerodocumentox { get => Numerodocumento; set => Numerodocumento = value; }
        public string Totalx { get => Total; set => Total = value; }
        public string TotalGlobalAPagarx { get => TotalGlobalAPagar; set => TotalGlobalAPagar = value; }
        public string DescripcionRubrox { get => DescripcionRubro; set => DescripcionRubro = value; }
        public string FechaPagox { get => FechaPago; set => FechaPago = value; }
        public string FechaRegistrox { get => FechaRegistro; set => FechaRegistro = value; }
        public string formadepagox { get => formadepago; set => formadepago = value; }
        public int formapago_Idx { get => formapago_Id; set => formapago_Id = value; }
        public string Observacionesx { get => Observaciones; set => Observaciones = value; }
        public string Chequerax { get => Chequera; set => Chequera = value; }
        public bool DocumentoRecibidox { get => DocumentoRecibido; set => DocumentoRecibido = value; }
        public int TotalDocumentosx { get => TotalDocumentos; set => TotalDocumentos = value; }
        public string ObservacionRechaSubContrax { get => ObservacionRechaSubContra; set => ObservacionRechaSubContra = value; }
        public string Curncyidx { get => curncyid; set => curncyid = value; }
        public string FechaDocumentox { get => FechaDocumento; set => FechaDocumento = value; }
        public bool PagoUrgentex { get => PagoUrgente; set => PagoUrgente = value; }
        public string Codigoplantillax { get => Codigoplantilla; set => Codigoplantilla = value; }
        public string Nombreplantillax { get => Nombreplantilla; set => Nombreplantilla = value; }
        public string Usuarioregistrox { get => Usuarioregistro; set => Usuarioregistro = value; }
        public string CodigoSopx { get => CodigoSop; set => CodigoSop = value; }
        public string Rifx { get => Rif; set => Rif = value; }
        public string Preciounitariox { get => Preciounitario; set => Preciounitario = value; }
        public string Anticipox { get => Anticipo; set => Anticipo = value; }
        public string Subtotalx { get => Subtotal; set => Subtotal = value; }
        public string Porcentajeivax { get => Porcentajeiva; set => Porcentajeiva = value; }
        public string Montoivax { get => Montoiva; set => Montoiva = value; }
        public string Porcentajeretencionx { get => Porcentajeretencion; set => Porcentajeretencion = value; }
        public string Totalretenidox { get => Totalretenido; set => Totalretenido = value; }

        public string Estatusx { get => Estatus; set => Estatus = value; }
        public string Prioridax { get => Prioridad; set => Prioridad = value; }
    }
}