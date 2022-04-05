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
    public class Wrkf_DbTipoDocumento
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Wrkf_DbTipoDocumento()
        {

        }

        /// <summary>
        /// Crea una lista con los tipos de documentos registrados
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_TipoDocumento> GetTipoDocumentos()
        {
            List<Wrkf_TipoDocumento> lsttipodocumento = new List<Wrkf_TipoDocumento>();

            //Ejecutar el procedimiento almacenado
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
            });

            string sqlQuery = "select tipodocumento_Id, codigo, documento from Workflow.TipoDocumento where visualizar = 1 order by tipodocumento_Id";

            //optener los resultados de la consulta
            DataTable DtTipoDocumento = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

            //verifica el procedimiento genero algun resultado
            int total_registros = DtTipoDocumento.Rows.Count;

            if (total_registros > 0)
            {
                //ingresa los datos en la lista lista
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_TipoDocumento objtipodocumento = new Wrkf_TipoDocumento()
                    {
                        Tipodocumento_Id = Convert.ToInt32(DtTipoDocumento.Rows[i]["tipodocumento_Id"]),
                        Codigo = Convert.ToString(DtTipoDocumento.Rows[i]["codigo"]).Trim(),
                        Documento = Convert.ToString(DtTipoDocumento.Rows[i]["documento"]).Trim()
                    };

                    lsttipodocumento.Add(objtipodocumento);
                }
            }
            else
            {
                Wrkf_TipoDocumento objtipodocumento = new Wrkf_TipoDocumento();
                lsttipodocumento.Add(objtipodocumento);
            }

            return lsttipodocumento;
        }
    }
}