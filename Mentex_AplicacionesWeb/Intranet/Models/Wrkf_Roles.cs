using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    /// <summary>
    /// Clase permite la manupulación de los datos para los roles registrados
    /// </summary>
    public class Wrkf_Roles
    {
        //Atributos de la clase
        private int Rol_Id;
        private string Rol;
        private bool Activo;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Wrkf_Roles()
        {
            this.Rol_Id = -1;
            this.Rol = "";
            this.Activo = false;
        }

        /// <summary>
        /// Propiedades de la clase
        /// </summary>
        public int Rol_Idx { get => Rol_Id; set => Rol_Id = value; }
        public string Rolx { get => Rol; set => Rol = value; }
        public bool Activox { get => Activo; set => Activo = value; }
    }
}