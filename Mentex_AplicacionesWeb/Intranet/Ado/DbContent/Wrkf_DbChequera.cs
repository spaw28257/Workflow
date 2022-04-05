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
        public List<Wrkf_Chequera> GetListadoChequera()
        {
            List<Wrkf_Chequera> lstChequera = new List<Wrkf_Chequera>();

            //Ejecutar el procedimiento almacenado
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipo", SqlDbType.VarChar, 20),
                new SqlParameter("@pTitulo", SqlDbType.VarChar, 30)
            });

            Sqlprovider.Oparameters[0].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[1].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;

            //Ejecuta el procedimiento almacenado
            DataTable DtChequeraid = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("WorkFlow.PL_Sel_ListadoChequera", 
                                                                                            CommandType.StoredProcedure, 
                                                                                            out Dictionary<string, string> outparam);

            //verifica que el procedimiento almacenado al momento de ejecutar no tenga errores
            if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
            {
                Wrkf_Chequera objChequera = new Wrkf_Chequera()
                {
                    Codigox = outparam["@pCodigoError"],
                    Mensajex = outparam["@pMensajeError"],
                    Tipox = outparam["@pTipo"],
                    Titulox = outparam["@pTitulo"]
                };

                lstChequera.Add(objChequera);
            }
            else
            {
                int total_registros = DtChequeraid.Rows.Count;

                if (total_registros > 0)
                {
                    for (int i = 0; i < total_registros; i++)
                    {
                        Wrkf_Chequera objChequera = new Wrkf_Chequera() 
                        {
                            ChequeraIdx = Convert.ToString(DtChequeraid.Rows[i]["ChequeraId"]),
                            Titularcuentax = Convert.ToString(DtChequeraid.Rows[i]["Titularcuenta"]),
                            Numerocuentax = Convert.ToString(DtChequeraid.Rows[i]["Numerocuenta"]),
                            Codigobancox = Convert.ToString(DtChequeraid.Rows[i]["Codigobanco"]),
                            Codigomonedax = Convert.ToString(DtChequeraid.Rows[i]["Codigomoneda"])
                        };

                        lstChequera.Add(objChequera);
                    }
                }
                else
                {
                    Wrkf_Chequera objChequera = new Wrkf_Chequera()
                    {
                        Codigox = "CHK001",
                        Mensajex = "No existen chequeras activas para mostrar",
                        Tipox = "error",
                        Titulox = "Listar Chequeras Para Asignar en la Revisión de CxP"
                    };

                    lstChequera.Add(objChequera);
                }
            }

            return lstChequera;
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