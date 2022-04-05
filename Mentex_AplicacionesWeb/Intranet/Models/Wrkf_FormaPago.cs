using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    public class Wrkf_FormaPago : MensajeError
    {
        private int formapago_Id;
        private string formadepago;
        private string codigo;

        public Wrkf_FormaPago()
        {
            formapago_Id = 0;
            formadepago = "";
            codigo = "";
        }

        public int Formapago_Id { get => formapago_Id; set => formapago_Id = value; }
        public string Formadepago { get => formadepago; set => formadepago = value; }
        public string Codigo { get => codigo; set => codigo = value; }
    }
}