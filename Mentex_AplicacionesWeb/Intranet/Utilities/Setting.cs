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
    /// 
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// Descripción: Se define el atributo de tipo ConfigurationSetting para almacenar la configuración de acceso a la base de datos.
        /// Fecha: 24/08/2021
        /// </summary>
        public static ConfigurationSetting ConfigurationSetting { get; set; }
    }
}