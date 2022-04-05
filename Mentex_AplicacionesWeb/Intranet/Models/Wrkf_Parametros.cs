using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    public class Wrkf_Parametros : MensajeError
    {
        private string Codigo;
        private string Descripcion;
        private double ValorNumerico;
        private string ValorAlfaNumerico;
        private int Row_Id;

        public Wrkf_Parametros()
        {
            Codigo = "";
            Descripcion = "";
            ValorNumerico = 0;
            ValorAlfaNumerico = "";
            Row_Id = 0;
        }

        public string Codigo1 { get => Codigo; set => Codigo = value; }
        public string Descripcion1 { get => Descripcion; set => Descripcion = value; }
        public double ValorNumerico1 { get => ValorNumerico; set => ValorNumerico = value; }
        public string ValorAlfaNumerico1 { get => ValorAlfaNumerico; set => ValorAlfaNumerico = value; }
        public int Row_Id1 { get => Row_Id; set => Row_Id = value; }
    }
}