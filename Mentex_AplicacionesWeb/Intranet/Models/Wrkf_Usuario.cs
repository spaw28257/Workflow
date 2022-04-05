using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    /// <summary>
    /// Clase que permite acceder y manipular los datos de la tabla [Workflow].[tbl_Usuario]
    /// </summary>
    public class Wrkf_Usuario : MensajeError
    {
        /// <summary>
        /// Atributos de la clase
        /// </summary>
        private int vDEX_ROW_ID;
        private string vUSERID;
        private string vUSERNAME;
        private int vUserStatus;
        private string vClaveAcceso;
        private string vRol_Id;

        /// <summary>
        /// Atributos Extendidos
        /// </summary>
        private string Rol;
        private string Estatus;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Wrkf_Usuario()
        {
            vDEX_ROW_ID = 0;
            vUSERID = "";
            vUSERNAME = "";
            vUserStatus = 0;
            vRol_Id = "";
            vClaveAcceso = "";
        }

        public int DEX_ROW_ID { get => vDEX_ROW_ID; set => vDEX_ROW_ID = value; }
        public string USERID { get => vUSERID; set => vUSERID = value; }
        public string USERNAME { get => vUSERNAME; set => vUSERNAME = value; }
        public int UserStatus { get => vUserStatus; set => vUserStatus = value; }
        public string ClaveAcceso { get => vClaveAcceso; set => vClaveAcceso = value; }
        public string Rol_Id { get => vRol_Id; set => vRol_Id = value; }
    }
}