using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción: Espacios de nombre para las clases que representan un uso global en el sistema.
/// Creado Por: Pablo Aponte
/// Fecha: 24/08/2021
/// </summary>
namespace Intranet.Utilities
{
    /// <summary>
    /// Descripción: Se definen los atributos para obtener el string de conexion de las bases de datos
    /// Fecha: 24/08/2021
    /// </summary>
    public class ConfigurationSetting
    {
        /// <summary>
        /// Descripción: Se especifica la cadena de conexión a la base de datos SQL SERVER
        /// </summary>
        public string SQLConnection { get; set; }

        public string SQLConnectionDY { get; set; }

        public string SQLConnectionCORP { get; set; }

        /// <summary>
        /// Descripción: Se especifica el valor del tiempo de espera para la conexión de la base de datos
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// Descripción: Se especifica el esquema de la base de datos al que pertenece el objeto
        /// </summary>
        public string SQLschema { get; set; }
    }
}