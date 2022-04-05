using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    public class Wrkf_Proveedores : MensajeError
    {
        private string vendorid; //Id del proveedor
        private string vendname; //Nombre del proveedor
        private string txrgnnum; //rif del proveedor
        private string vndclsid; //Clase del proveedor

        public Wrkf_Proveedores()
        {
            this.vendorid = "";
            this.vendname = "";
            this.txrgnnum = "";
            this.vndclsid = "";
        }

        public string Vendorid { get => vendorid; set => vendorid = value; }
        public string Vendname { get => vendname; set => vendname = value; }
        public string Txrgnnum { get => txrgnnum; set => txrgnnum = value; }
        public string Vndclsid { get => vndclsid; set => vndclsid = value; }
    }
}