using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intranet.Models
{
    public class Wrkf_OpcionesMenuItem : MensajeError
    {
        /// <summary>
        /// Atributos de la clase para el acceso a los datos.
        /// </summary>
        private int Opcionmenu_Id;
        private int Padre_Id;
        private int Nivel;
        private string Menu;
        private string Accion;
        private string Controlador;
        private string Titulo;
        private bool Activo;
        private string Imagen;

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        public Wrkf_OpcionesMenuItem()
        {
            Opcionmenu_Id = -1;
            Padre_Id = -1;
            Nivel = -1;
            Menu = "";
            Accion = "";
            Controlador = "";
            Titulo = "";
            Activo = false;
            Imagen = "";
        }

        /// <summary>
        /// Propiedades de la clase.
        /// </summary>
        public int Opcionmenu_Idx { get => Opcionmenu_Id; set => Opcionmenu_Id = value; }
        public int Padre_Idx { get => Padre_Id; set => Padre_Id = value; }
        public int Nivelx { get => Nivel; set => Nivel = value; }
        public string Menux { get => Menu; set => Menu = value; }
        public string Accionx { get => Accion; set => Accion = value; }
        public string Controladorx { get => Controlador; set => Controlador = value; }
        public string Titulox { get => Titulo; set => Titulo = value; }
        public bool Activox { get => Activo; set => Activo = value; }
        public string Imagenx { get => Imagen; set => Imagen = value; }
    }
}