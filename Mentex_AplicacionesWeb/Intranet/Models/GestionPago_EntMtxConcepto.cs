using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    /// <summary>
    /// Permite acceder a la tabla de conceptos del GAP
    /// </summary>
    public class GestionPago_EntMtxConcepto
    {
        private int vCodigo;
        private string vDescripcion;
        private string vSegmento2;
        private string vSegmento3;
        private string vTipo;
        private string vCodigoISLRNatural;
        private string vCodigoISLRJuridico;

        public GestionPago_EntMtxConcepto()
        {
            vCodigo = 0;
            vDescripcion = "";
            vSegmento2 = "";
            vSegmento3 = "";
            vTipo = "";
            vCodigoISLRNatural = "";
            vCodigoISLRJuridico = "";
        }

        public int Codigo { get => vCodigo; set => vCodigo = value; }
        public string Descripcion { get => vDescripcion; set => vDescripcion = value; }
        public string Segmento2 { get => vSegmento2; set => vSegmento2 = value; }
        public string Segmento3 { get => vSegmento3; set => vSegmento3 = value; }
        public string Tipo { get => vTipo; set => vTipo = value; }
        public string CodigoISLRNatural { get => vCodigoISLRNatural; set => vCodigoISLRNatural = value; }
        public string CodigoISLRJuridico { get => vCodigoISLRJuridico; set => vCodigoISLRJuridico = value; }
    }
}