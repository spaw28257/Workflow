using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Intranet.Utilities;
using Intranet.Models;

namespace Intranet.Ado.DbContent
{
    /// <summary>
    /// Permite acceder al contenido de la tabla GestionPago.DiaFeriado
    /// </summary>
    public class GestionPago_DbDiaFeriado
    {
        /// <summary>
        /// El método permite verificar si una fecha especificada es un día feriado devolviendo true
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public bool GetDiaFeriado(DateTime dia)
        {
            //Ejecutar el procedimiento almacenado
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@Dia", dia)
            });

            string sqlQuery = "select Dia, Descripcion from GestionPago.MTX_DiaFeriado where Dia = @Dia";

            //optener los resultados de la consulta sql server
            DataTable DtDiaFeriado = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

            //verifica si la consulta genera algun resultado
            int total_registros = DtDiaFeriado.Rows.Count;

            if (total_registros > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}