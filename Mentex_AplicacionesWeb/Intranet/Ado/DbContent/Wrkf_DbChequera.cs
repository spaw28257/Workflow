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
    /// Permite accedera los datos de la tabla CM00101 => Maestro de chequera
    /// </summary>
    public class Wrkf_DbChequera
    {
        /// <summary>
        /// El método permite obtener un listado de las chequeras
        /// </summary>
        /// <returns></returns>
        public Wrkf_Chequera GetChequera_Key(string pchequera_id)
        {
            Wrkf_Chequera objChequera = new Wrkf_Chequera();

            //Ejecutar el procedimiento almacenado
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pCHEKBKID", pchequera_id)
            });

            //Ejecuta el procedimiento almacenado
            DataTable DtChequeraid = Sqlprovider.ExecuteStoredProcedure("BankTransfer.PL_Sel_CM00100_key", CommandType.StoredProcedure);

            int total_registros = DtChequeraid.Rows.Count;

            if (total_registros > 0)
            {
                objChequera.ChequeraIdx = DtChequeraid.Rows[0]["CHEKBKID"].ToString().Trim();
                objChequera.Titularcuentax = DtChequeraid.Rows[0]["DSCRIPTN"].ToString().Trim();
                objChequera.Numerocuentax = DtChequeraid.Rows[0]["CMUSRDF1"].ToString().Trim();
                objChequera.Codigobancox = DtChequeraid.Rows[0]["BANKID"].ToString().Trim();
                objChequera.Codigomonedax = DtChequeraid.Rows[0]["CURNCYID"].ToString().Trim();
                objChequera.Inactivax = DtChequeraid.Rows[0]["INACTIVE"].ToString().Trim();
            }

            return objChequera;
        }

        /// <summary>
        /// Get the Currency Code of the Checkbook (Obtener el Codigo de la Moneda de la chequera)
        /// </summary>
        /// <param name="chekbkid"></param>
        /// <returns></returns>
        public string GetCurrencyCodeCheckbook(string chekbkid)
        {
            string currencycode;

            //Ejecutar el procedimiento almacenado
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@CHEKBKID", chekbkid)
            });

            string SqlQuery = "select CURNCYID from CM00100 where CHEKBKID = @CHEKBKID order by CHEKBKID";

            DataTable DtChequera = Sqlprovider.ExecuteStoredProcedure(SqlQuery, CommandType.Text);

            int total_registros = DtChequera.Rows.Count;

            if (total_registros > 0)
            {
                currencycode = Convert.ToString(DtChequera.Rows[0]["CURNCYID"]);
            }
            else
            {
                currencycode = "";
            }

            return currencycode;
        }
    }
}