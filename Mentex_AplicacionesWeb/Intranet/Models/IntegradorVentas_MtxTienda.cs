using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    public class IntegradorVentas_MtxTienda : MensajeError
    {
        private string tienda;
        private string tipoinventario;
        private bool zonalibre;
        private bool remesatarjeta;
        private bool conegreso;
        private string direccionip;
        private bool precierrepdf;
        private bool integravendol;

        public IntegradorVentas_MtxTienda()
        {
            tienda = "";
            tipoinventario = "";
            zonalibre = false;
            remesatarjeta = false;
            conegreso = false;
            direccionip = "";
            precierrepdf = false;
            integravendol = false;
        }

        public string Tienda { get => tienda; set => tienda = value; }
        public string Tipoinventario { get => tipoinventario; set => tipoinventario = value; }
        public bool Zonalibre { get => zonalibre; set => zonalibre = value; }
        public bool Remesatarjeta { get => remesatarjeta; set => remesatarjeta = value; }
        public bool Conegreso { get => conegreso; set => conegreso = value; }
        public string Direccionip { get => direccionip; set => direccionip = value; }
        public bool Precierrepdf { get => precierrepdf; set => precierrepdf = value; }
        public bool Integravendol { get => integravendol; set => integravendol = value; }
    }
}