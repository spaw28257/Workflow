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
    public class Wrkf_DbFormaPago
    {
        /// <summary>
        /// Obtiene una lista de las formas de pago registradas
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_FormaPago> GetFormasPago()
        {
            List<Wrkf_FormaPago> lstformapago = new List<Wrkf_FormaPago>();

            //Ejecutar el procedimiento almacenado
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
            });

            string sqlQuery = "select formapago_Id, formadepago, codigo from Workflow.FormaPago order by formapago_Id";

            //optener los resultados del procedimiento almacenado
            DataTable DtFormaPago = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

            //verifica el procedimiento genero algun resultado
            int total_registros = DtFormaPago.Rows.Count;

            if (total_registros > 0)
            {
                //ingresa los datos en la lista lista
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_FormaPago objformapago = new Wrkf_FormaPago()
                    {
                        Formapago_Id = Convert.ToInt32(DtFormaPago.Rows[i]["formapago_Id"]),
                        Formadepago = Convert.ToString(DtFormaPago.Rows[i]["formadepago"]),
                        Codigo = Convert.ToString(DtFormaPago.Rows[i]["codigo"])
                    };

                    lstformapago.Add(objformapago);
                }
            }
            else
            {
                Wrkf_FormaPago objformapago = new Wrkf_FormaPago();
                lstformapago.Add(objformapago);
            }

            return lstformapago;
        }

        /// <summary>
        /// Obtiene los datos de la forma de pago por id
        /// </summary>
        /// <returns></returns>
        public Wrkf_FormaPago GetFormasPagoId(int formapago_id, string usuario)
        {
            Wrkf_FormaPago wrkf_formapago = new Wrkf_FormaPago();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError mensajeerror = new MensajeError();

            try
            {
                //Ejecutar el procedimiento almacenado
                SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@formapago_Id", formapago_id)
                });

                string sqlQuery = "select formapago_Id, formadepago, codigo from Workflow.FormaPago where formapago_Id = @formapago_Id order by formapago_Id";

                //optener los resultados del procedimiento almacenado
                DataTable DtFormaPago = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

                //verifica el procedimiento genero algun resultado
                int total_registros = DtFormaPago.Rows.Count;

                if (total_registros > 0)
                {
                    wrkf_formapago.Formapago_Id = Convert.ToInt32(DtFormaPago.Rows[0]["formapago_Id"]);
                    wrkf_formapago.Formadepago = Convert.ToString(DtFormaPago.Rows[0]["formadepago"]);
                    wrkf_formapago.Codigo = Convert.ToString(DtFormaPago.Rows[0]["codigo"]);
                }
                else
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("00002", "Wrkf_DbFormaPago/GetFormasPagoId");
                    wrkf_formapago.Codigox = mensajeerror.Codigox;
                    wrkf_formapago.Mensajex = mensajeerror.Mensajex;
                    wrkf_formapago.Titulox = mensajeerror.Titulox;
                    wrkf_formapago.Tipox = mensajeerror.Tipox;
                }
            }
            catch (Exception ex)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                wrkf_formapago.Codigox = mensajeerror.Codigox;
                wrkf_formapago.Mensajex = mensajeerror.Mensajex;
                wrkf_formapago.Titulox = mensajeerror.Titulox;
                wrkf_formapago.Tipox = mensajeerror.Tipox;

                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), usuario, "Wrkf_DbFormaPago/GetFormasPagoId");
            }

            return wrkf_formapago;
        }
    }
}