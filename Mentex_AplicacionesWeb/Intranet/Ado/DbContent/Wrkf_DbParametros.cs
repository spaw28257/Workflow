using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Intranet.Utilities;
using Intranet.Models;
using System.Globalization;
using Intranet.Ado.DbContent;

namespace Intranet.Ado.DbContent
{
    /// <summary>
    /// Establece el acceso alos datos de la tabla de parametros
    /// </summary>
    public class Wrkf_DbParametros
    {
        public Wrkf_DbParametros()
        {
        }

        /// <summary>
        /// Selecciona los datos del parametro segun el codigo especificado
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public Wrkf_Parametros SeleccionarParametroCodigo(string codigo, string usuario)
        {
            Wrkf_Parametros wrkf_parametros = new Wrkf_Parametros();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            try
            {
                SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@Codigo", codigo)
                });

                string sqlQuery = "select Codigo, Descripcion, ValorNumerico, ValorAlfaNumerico, Row_Id from [Workflow].[Parametros] where Codigo = @Codigo";

                DataTable DtRegistros = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

                int total_registros = DtRegistros.Rows.Count;

                if (total_registros > 0)
                {
                    wrkf_parametros.Codigo1 = DtRegistros.Rows[0]["Codigo"].ToString();
                    wrkf_parametros.Descripcion1 = DtRegistros.Rows[0]["Descripcion"].ToString();
                    wrkf_parametros.ValorNumerico1 = Convert.ToDouble(DtRegistros.Rows[0]["ValorNumerico"]);
                    wrkf_parametros.ValorAlfaNumerico1 = Convert.ToString(DtRegistros.Rows[0]["ValorAlfaNumerico"]);
                }
                else
                {
                    wrkf_parametros.Codigox = "SPARC001";
                    wrkf_parametros.Mensajex = "El código del parámetro especificado, no esta registrado";
                    wrkf_parametros.Tipox = "error";
                    wrkf_parametros.Titulox = "Seleccionar Parametro por Código";
                }
            }
            catch (Exception ex)
            {
                wrkf_parametros.Codigox = "SPARC002";
                wrkf_parametros.Mensajex = "Ocurrio un error al momento de obtener el valor del parárametro con el código especificado";
                wrkf_parametros.Tipox = "error";
                wrkf_parametros.Titulox = "Seleccionar Parametro por Código";

                Wrkf_RespuestaOperacion wrkf_respuestaoperacion = wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), usuario.Trim().ToUpper(), "Wrkf_DbParametros/SeleccionarParametroCodigo");
            }

            return wrkf_parametros;
        }
    }
}