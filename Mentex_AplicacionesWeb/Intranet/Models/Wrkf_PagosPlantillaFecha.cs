using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    public class Wrkf_PagosPlantillaFecha : MensajeError
    {
        //Atributos de la clase
        private string Codigo;
        private string CodigoItem;
        private string CodigoPlantilla;
        private string Monto;
        private string FechaFactura;
        private string FechaVencimiento;
        private string FechaPago;
        private string NroFactura;
        private string TipoPago;
        private string IdProveedor;
        private string VENDNAME;
        private string LOCATNID;
        private string CUSTNAME;
        private string UltimoPago1;
        private string UltimoPago2;
        private string UltimoPago3;
        private string IdChequera;
        private string ACNMVNDR;
        private string Estatus;
        private string ComprobanteIVA;
        private string ComprobanteISLR;
        private string NroControl;
        private string DescripcionFactura;
        private string MontoServicio;
        private string Referencia;
        private string ComplementoFactura;
        private string CartaSolicitud;
        private string Tienda;
        private string MontoAdelanto;

        private string Usuario;
        private bool Seleccionado;
        private string Gruporubro_Id;
        private string Rubro_Id;
        private string curncyid;
        private string Rif;

        //constructor de la clase
        public Wrkf_PagosPlantillaFecha()
        {
        }

        //Propiedades de la clase
        public string Codigo1 { get => Codigo; set => Codigo = value; }
        public string CodigoItem1 { get => CodigoItem; set => CodigoItem = value; }
        public string CodigoPlantilla1 { get => CodigoPlantilla; set => CodigoPlantilla = value; }
        public string Monto1 { get => Monto; set => Monto = value; }
        public string FechaFactura1 { get => FechaFactura; set => FechaFactura = value; }
        public string FechaVencimiento1 { get => FechaVencimiento; set => FechaVencimiento = value; }
        public string FechaPago1 { get => FechaPago; set => FechaPago = value; }
        public string NroFactura1 { get => NroFactura; set => NroFactura = value; }
        public string TipoPago1 { get => TipoPago; set => TipoPago = value; }
        public string IdProveedor1 { get => IdProveedor; set => IdProveedor = value; }
        public string VENDNAME1 { get => VENDNAME; set => VENDNAME = value; }
        public string LOCATNID1 { get => LOCATNID; set => LOCATNID = value; }
        public string CUSTNAME1 { get => CUSTNAME; set => CUSTNAME = value; }
        public string UltimoPago11 { get => UltimoPago1; set => UltimoPago1 = value; }
        public string UltimoPago21 { get => UltimoPago2; set => UltimoPago2 = value; }
        public string UltimoPago31 { get => UltimoPago3; set => UltimoPago3 = value; }
        public string IdChequera1 { get => IdChequera; set => IdChequera = value; }
        public string ACNMVNDR1 { get => ACNMVNDR; set => ACNMVNDR = value; }
        public string Estatus1 { get => Estatus; set => Estatus = value; }
        public string ComprobanteIVA1 { get => ComprobanteIVA; set => ComprobanteIVA = value; }
        public string ComprobanteISLR1 { get => ComprobanteISLR; set => ComprobanteISLR = value; }
        public string NroControl1 { get => NroControl; set => NroControl = value; }
        public string DescripcionFactura1 { get => DescripcionFactura; set => DescripcionFactura = value; }
        public string MontoServicio1 { get => MontoServicio; set => MontoServicio = value; }
        public string Referencia1 { get => Referencia; set => Referencia = value; }
        public string ComplementoFactura1 { get => ComplementoFactura; set => ComplementoFactura = value; }
        public string CartaSolicitud1 { get => CartaSolicitud; set => CartaSolicitud = value; }
        public string Tienda1 { get => Tienda; set => Tienda = value; }
        public string MontoAdelanto1 { get => MontoAdelanto; set => MontoAdelanto = value; }
        public string Usuario1 { get => Usuario; set => Usuario = value; }
        public bool Seleccionado1 { get => Seleccionado; set => Seleccionado = value; }
        public string Gruporubro_Id1 { get => Gruporubro_Id; set => Gruporubro_Id = value; }
        public string Rubro_Id1 { get => Rubro_Id; set => Rubro_Id = value; }
        public string Curncyid1 { get => curncyid; set => curncyid = value; }
        public string Rif1 { get => Rif; set => Rif = value; }
    }
}