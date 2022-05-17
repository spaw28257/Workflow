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
    public class Wrkf_DatGrupoRubro
    {
        public Wrkf_DatGrupoRubro()
        {
        }

        /// <summary>
        /// El método retorna una lista de los departamentos o grupos registrados
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_GrupoRubro> GetGrupoRubros()
        {
            List<Wrkf_GrupoRubro> lstGrupoRubro = new List<Wrkf_GrupoRubro>();
            //Ejecutar el procedimiento almacenado
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@pTituloError", SqlDbType.VarChar, 60)
            });

            Sqlprovider.Oparameters[0].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[1].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;

            string vSqlProcedureName = "WorkFlow.PL_Sel_Gruporubro_All";

            //optener los resultados del procedimiento almacenado
            DataTable dtGrupoRubro = Sqlprovider.ExecuteStoredProcedureWithOutputParameter(vSqlProcedureName, CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            //verifica el procedimiento genero algun resultado
            int total_registros = dtGrupoRubro.Rows.Count;

            if (total_registros > 0)
            {
                //ingresa los datos en la lista lista
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_GrupoRubro objGrupoRubroModel = new Wrkf_GrupoRubro()
                    {
                        GrupoRubroIdEncript = "",
                        GrupoRubro_Id = Convert.ToInt32(dtGrupoRubro.Rows[i]["GrupoRubro_Id"]),
                        Descripcion = Convert.ToString(dtGrupoRubro.Rows[i]["Descripcion"]),
                        TotalGrupoRubros = total_registros
                    };

                    lstGrupoRubro.Add(objGrupoRubroModel);
                }
            }
            else
            {
                Wrkf_GrupoRubro objGrupoRubroModel = new Wrkf_GrupoRubro();
                lstGrupoRubro.Add(objGrupoRubroModel);
            }

            return lstGrupoRubro;
        }

        /// <summary>
        /// Obtiene los datos del departamento o grupo de rubro por Departamento_Id
        /// </summary>
        /// <param name="Departamento_Id"></param>
        /// <returns></returns>
        public Wrkf_GrupoRubro GetGrupoRubroPorId(int pGrupoRubro_Id)
        {
            Wrkf_GrupoRubro objGrupoRubro = new Wrkf_GrupoRubro();

            //Ejecutar el procedimiento almacenado
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pGrupoRubro_Id", pGrupoRubro_Id),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@pTituloError", SqlDbType.VarChar, 60)
            });

            Sqlprovider.Oparameters[1].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;

            string vSqlProcedureName = "WorkFlow.PL_Sel_Gruporubro_key";

            //optener los resultados del procedimiento almacenado
            DataTable dtGrupoRubro = Sqlprovider.ExecuteStoredProcedureWithOutputParameter(vSqlProcedureName, CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            //verifica el procedimiento genero algun resultado
            int total_registros = dtGrupoRubro.Rows.Count;

            if (total_registros > 0)
            {
                objGrupoRubro.GrupoRubroIdEncript = "";
                objGrupoRubro.GrupoRubro_Id = Convert.ToInt32(dtGrupoRubro.Rows[0]["GrupoRubro_Id"]);
                objGrupoRubro.Descripcion = Convert.ToString(dtGrupoRubro.Rows[0]["Descripcion"]);
                objGrupoRubro.TotalGrupoRubros = total_registros;
            }

            return objGrupoRubro;
        }
    }
}