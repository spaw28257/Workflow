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
    /// La clase permite seleccionar las monedas configuradas en GP enla tabla DYNAMICS..MC40200
    /// </summary>
    public class Wrkf_DbMoneda
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Wrkf_DbMoneda()
        {
        }

        /// <summary>
        /// El metodo devuelve una lista de las monedas activas
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_Moneda> GetMonedasId()
        {
            List<Wrkf_Moneda> lstConfiguracionMoneda = new List<Wrkf_Moneda>();
            //Ejecutar el procedimiento almacenado
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.DYNAMICS);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
            });

            string sqlQuery = "select ltrim(rtrim(curncyid)) as curncyid, ltrim(rtrim(crncydsc)) as crncydsc, ltrim(rtrim(crncysym)) as crncysym from MC40200 order by crncydsc";

            //optener los resultados del procedimiento almacenado
            DataTable DtMoneda = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

            //verifica el procedimiento genero algun resultado
            int total_registros = DtMoneda.Rows.Count;

            if (total_registros > 0)
            {
                //ingresa los datos en la lista lista
                for (int i = 0; i < total_registros; i++)
                {
                    //verify if the currency´s code is allowed to show
                    bool isallowed = VerifyCurrencyCodeExists(DtMoneda.Rows[i]["curncyid"].ToString());

                    if (isallowed)
                    {
                        Wrkf_Moneda objConfiguracionMoneda = new Wrkf_Moneda()
                        {
                            Curncyid = Convert.ToString(DtMoneda.Rows[i]["curncyid"]),
                            Crncydsc = Convert.ToString(DtMoneda.Rows[i]["crncydsc"]),
                            Crncysym = Convert.ToString(DtMoneda.Rows[i]["crncysym"])
                        };

                        lstConfiguracionMoneda.Add(objConfiguracionMoneda);
                    }
                }
            }

            return lstConfiguracionMoneda;
        }

        /// <summary>
        /// This method check if currency's code exists
        /// </summary>
        /// <param name="currencycode"></param>
        /// <returns></returns>
        private Boolean VerifyCurrencyCodeExists(string currencycode)
        {
            Boolean exists;

            //Ejecutar el procedimiento almacenado
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@CurrencyCode", currencycode),
            });

            string SqlQuery = "select curncyid from workflow.Moneda where curncyid = @CurrencyCode";

            DataTable Dtcurrencycodeexits = Sqlprovider.ExecuteStoredProcedure(SqlQuery, CommandType.Text);

            if (Dtcurrencycodeexits.Rows.Count > 0)
            {
                exists = true;
            }
            else
            {
                exists = false;
            }

            return exists;
        }
    }
}