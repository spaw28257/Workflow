using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Intranet.Models;

/// <summary>
/// Descripción: Espacios de nombre para las clases que representan un uso global en el sistema.
/// Creado Por: Pablo Aponte
/// Fecha: 24/08/2021
/// </summary>
namespace Intranet.Utilities
{
    /// <summary>
    /// Descripción: Se definen los metodos que se utilizar para realizar la transformación de un dato
    /// Fecha: 24/08/2021
    /// </summary>
    public class ConvertExtension
    {
        public ConvertExtension()
        {
        }

        /// <summary>
        /// Descripción: Recibe un parametro tipo fecha y lo retorna en formato yyyyMMdd
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns>yyyymmdd</returns>
        public int FormatoFechayyyyMMdd(DateTime fecha)
        {
            int xday = fecha.Day;
            int xmonth = fecha.Month;
            int xyear = fecha.Year;

            int xdate_yyyyMMdd = (xyear * 10000) + (xmonth * 100) + xday;
            return xdate_yyyyMMdd;
        }

        /// <summary>
        /// Recibe un parametro tipo fecha y de vuelve un string en formato yyyy-mm-dd
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns>yyyy-mm-dd</returns>
        public string FormatoFecha2(DateTime fecha)
        {
            int xday = fecha.Day;
            int xmonth = fecha.Month;
            int xyear = fecha.Year;
            string fecha_transformada;

            fecha_transformada = Convert.ToString(xyear) + "-";

            if (xmonth <= 9)
            {
                fecha_transformada += "0" + Convert.ToString(xmonth) + "-";
            }
            else
            {
                fecha_transformada += Convert.ToString(xmonth) + "-";
            }

            if (xday <= 9)
            {
                fecha_transformada += "0" + Convert.ToString(xday);
            }
            else
            {
                fecha_transformada += Convert.ToString(xday);
            }

            return fecha_transformada;
        }

        /// <summary>
        /// Coloca la fecha en formato dd/mm/yyyy
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public string FormatoFecha3(DateTime fecha)
        {
            int xday = fecha.Day;
            int xmonth = fecha.Month;
            int xyear = fecha.Year;
            string fecha_transformada = string.Empty;

            if (xday <= 9)
            {
                fecha_transformada += "0" + Convert.ToString(xday) + "/";
            }
            else
            {
                fecha_transformada += Convert.ToString(xday) + "/";
            }

            if (xmonth <= 9)
            {
                fecha_transformada += "0" + Convert.ToString(xmonth) + "/";
            }
            else
            {
                fecha_transformada += Convert.ToString(xmonth) + "/";
            }

            fecha_transformada += Convert.ToString(xyear);

            return fecha_transformada;
        }

        public double FormatoNumeroDecimal(double valor)
        {
            return valor / 100;
        }

        /// <summary>
        /// recibe un valor númerico como string y lo transforma en valor decimal, quitando los separadores de miles y decimales divide el valor entre 100.
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public double FormatoDecimal2(string value)
        {
            string newvalue;
            newvalue = value.Replace(",", "");
            newvalue = newvalue.Replace(".", "");
            return Convert.ToDouble(newvalue) / 100;
        }

        /// <summary>
        /// Verificar si una fecha ingresada es Sabado o Domingo
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public bool VerificarSabadoDomingo(DateTime fecha)
        {
            bool retornar = false;

            //indica que el dia es domingo
            if (fecha.DayOfWeek == DayOfWeek.Saturday)
            {
                retornar = true;
            }

            //indica que el dia es sabado
            if (Convert.ToDateTime(fecha).DayOfWeek == DayOfWeek.Sunday)
            {
                retornar = true;
            }

            return retornar;
        }

        /// <summary>
        /// Obtiene la fecha posterior a 15 días de la fecha actual
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public Wrkf_DiaIFSemana ObtenerPrimerDiaSemana(DateTime fecha)
        {
            Wrkf_DiaIFSemana objwrkfdiaifsemana = new Wrkf_DiaIFSemana();
            DateTime fechainicioobtenida;
            DateTime fechaobtenida;

            fechainicioobtenida = fecha.AddDays(-15);
            objwrkfdiaifsemana.Primerdiasemana = FormatoFecha2(fechainicioobtenida);
            fechaobtenida = fecha.AddDays(15);
            objwrkfdiaifsemana.Ultimodiasemana = FormatoFecha2(fechaobtenida);

            return objwrkfdiaifsemana;
        }

        /// <summary>
        /// The function returns the value with the number of characters for the database field
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fieldsizedatabase"></param>
        /// <returns></returns>
        public string ValidateNumberCharacters(string value, int fieldsizedatabase)
        {
            //if the number of characters is greater than the number of characters that the field supports in the database, it makes the substring
            if (value.Trim().Length > 0)
            {
                if (value.Trim().Length > fieldsizedatabase)
                {
                    value = value.Substring(0, fieldsizedatabase);
                }
                else
                {
                    value = value.Substring(0, value.Trim().Length);
                }
            }
            else
            {
                value = "";
            }

            return value;
        }

        /// <summary>
        /// Compara dos fechas para saber si la fecha desde es mayor a la fecha hasta
        /// </summary>
        /// <param name="pFechaDesde"></param>
        /// <param name="pFechaHasta"></param>
        /// <returns></returns>
        public int CompararFechas(DateTime pFechaDesde, DateTime pFechaHasta)
        {
            int result = DateTime.Compare(pFechaDesde, pFechaHasta);
            return result;
        }
    }
}