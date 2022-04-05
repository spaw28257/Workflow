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
    /// Clase para acceder a la tabla TX00201 detalle del impuesto
    /// </summary>
    public class DbTx00201
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public DbTx00201()
        {
        }

        /// <summary>
        /// Lista los detalles de impuestos para seleccionar el porcentaje de iva o retención
        /// </summary>
        /// <returns></returns>
        public List<Tx00201> ListarDetalleImpuesto()
        {
            List<Tx00201> lstdetalleimpuesto = new List<Tx00201>();

            //el procedimiento no recibe parametros 
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
            });

            string sqlQuery = "select TAXDTLID, TXDTLDSC, TXDTLPCT from TX00201 order by TAXDTLID";

            //optener los resultados de la consulta SQL
            DataTable DtDetalleImpuesto = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

            //verifica el procedimiento genero algun resultado
            int total_registros = DtDetalleImpuesto.Rows.Count;

            if (total_registros > 0)
            {
                //ingresa los datos en la lista lista
                for (int i = 0; i < total_registros; i++)
                {
                    Tx00201 objtx00201 = new Tx00201()
                    {
                        Taxdtlid = DtDetalleImpuesto.Rows[i]["TAXDTLID"].ToString(),
                        Txdtldsc = DtDetalleImpuesto.Rows[i]["TXDTLDSC"].ToString(),
                        Txdtlpct = Convert.ToDouble(DtDetalleImpuesto.Rows[i]["TXDTLPCT"])
                    };

                    lstdetalleimpuesto.Add(objtx00201);
                }
            }
            else
            {
                Tx00201 objtx00201 = new Tx00201();
                lstdetalleimpuesto.Add(objtx00201);
            }

            return lstdetalleimpuesto;
        }
    }
}