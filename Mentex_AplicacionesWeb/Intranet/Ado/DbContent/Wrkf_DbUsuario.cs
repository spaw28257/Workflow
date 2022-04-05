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
    /// Clase permite realizar operaciones SQL SERVER con la tabla [Workflow].[tbl_Usuario]
    /// </summary>
    public class Wrkf_DbUsuario
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Wrkf_DbUsuario()
        {
        }

        /// <summary>
        /// El método permite autenticar los datos del usuario que se esta logeando
        /// </summary>
        /// <param name="pUSERID"></param>
        /// <returns></returns>
        public Wrkf_Usuario DatAutenticarUsuario(string pUSERID, string pClaveAcceso)
        {
            Wrkf_Usuario objUsuario = new Wrkf_Usuario();

            //Pasar los parametros al procedimiento almacenado
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pUSERID", pUSERID.Trim()),
                new SqlParameter("@pClaveAcceso", pClaveAcceso.Trim()),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@pTituloError", SqlDbType.VarChar, 60)
            });

            //Paramtros de salida del procedimiento almacenado
            Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;

            //Ejecuta el procedimiento almacenado
            DataTable DtUsuario = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("WorkFlow.PL_Sel_AutenticarUsuario_key", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            //verifica que el procedimiento almacenado al momento de ejecutar no tenga errores
            if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
            {
                objUsuario.Codigox = outparam["@pCodigoError"];
                objUsuario.Mensajex = outparam["@pMensajeError"];
                objUsuario.Tipox = outparam["@pTipoError"];
                objUsuario.Titulox = outparam["@pTituloError"];
            }
            else
            {
                objUsuario.USERID = DtUsuario.Rows[0]["USERID"].ToString().Trim();
                objUsuario.USERNAME = DtUsuario.Rows[0]["USERNAME"].ToString().Trim();
                objUsuario.Rol_Id = DtUsuario.Rows[0]["Rol_Id"].ToString().Trim();
            }

            return objUsuario;
        }
    }
}