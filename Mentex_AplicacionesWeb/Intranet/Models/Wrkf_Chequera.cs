using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    /// <summary>
    /// Permite el acceso a los campos de la tabla CM00101 => Maestro de chequera
    /// </summary>
    public class Wrkf_Chequera : MensajeError
    {
        /// <summary>
        /// Atributos
        /// </summary>
        private string ChequeraId;
        private string Titularcuenta;
        private string Numerocuenta;
        private string Codigobanco;
        private string Codigomoneda;
        private string Inactiva;

        /// <summary>
        /// constructor
        /// </summary>
        public Wrkf_Chequera()
        {
            ChequeraId = "";
            Titularcuenta = "";
            Numerocuenta = "";
            Codigobanco = "";
            Codigomoneda = "";
            Inactiva = "";
        }

        /// <summary>
        /// Propiedades
        /// </summary>
        public string ChequeraIdx { get => ChequeraId; set => ChequeraId = value; }
        public string Titularcuentax { get => Titularcuenta; set => Titularcuenta = value; }
        public string Numerocuentax { get => Numerocuenta; set => Numerocuenta = value; }
        public string Codigobancox { get => Codigobanco; set => Codigobanco = value; }
        public string Codigomonedax { get => Codigomoneda; set => Codigomoneda = value; }
        public string Inactivax { get => Inactiva; set => Inactiva = value; }
    }
}