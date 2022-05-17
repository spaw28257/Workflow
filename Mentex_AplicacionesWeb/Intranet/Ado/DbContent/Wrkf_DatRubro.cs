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
    /// La clase permite acceder a realizar las operaciones en la base de datos [GestionPago].[MTX_Grupo]
    /// </summary>
    public class Wrkf_DatRubro
    {
        /// <summary>
        /// Cosntructor de la clase
        /// </summary>
        public Wrkf_DatRubro()
        {
        }

        /// <summary>
        /// El método obtiene el listado de los grupos de rubros para asignar al pago
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_Rubro> GetRubro_All(int pGrupoRubro_Id)
        {
            List<Wrkf_Rubro> lstRubro = new List<Wrkf_Rubro>();
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pGrupoRubro_Id", pGrupoRubro_Id),
                new SqlParameter("@pCodigoError", SqlDbType.NVarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.NVarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.NVarChar, 10),
                new SqlParameter("@pTituloError", SqlDbType.NVarChar, 60)
            });

            Sqlprovider.Oparameters[1].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;

            string vSqlProcedureName = "WorkFlow.PL_Sel_Rubro_All";

            //optener los resultados del procedimiento almacenado
            DataTable dtRubro = Sqlprovider.ExecuteStoredProcedureWithOutputParameter(vSqlProcedureName, CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            //verifica el procedimiento genero algun resultado
            int total_registros = dtRubro.Rows.Count;

            if (total_registros > 0)
            {
                //ingresa los datos en la lista
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_Rubro ObjRubro = new Wrkf_Rubro()
                    {
                        Rubro_Id = Convert.ToString(dtRubro.Rows[i]["Rubro_Id"]).Trim(),
                        Descripcion = Convert.ToString(dtRubro.Rows[i]["Descripcion"]).Trim(),
                        GrupoRubro_Id = Convert.ToInt32(dtRubro.Rows[i]["GrupoRubro_Id"])
                    };

                    lstRubro.Add(ObjRubro);
                }
            }

            return lstRubro;
        }

        /// <summary>
        /// Selecciona los datos del rubro por id
        /// </summary>
        /// <param name="pGrupoRubro_Id"></param>
        /// <param name="pRubro_Id"></param>
        /// <returns></returns>
        public Wrkf_Rubro GetRubro_Key(int pGrupoRubro_Id, string pRubro_Id)
        {
            Wrkf_Rubro objRubro = new Wrkf_Rubro();
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pRubro_Id", pRubro_Id),
                 new SqlParameter("@pGrupoRubro_Id", pGrupoRubro_Id),
                new SqlParameter("@pCodigoError", SqlDbType.NVarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.NVarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.NVarChar, 10),
                new SqlParameter("@pTituloError", SqlDbType.NVarChar, 60)
            });

            Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;

            string vSqlProcedureName = "WorkFlow.PL_Sel_Rubro_Key";

            //optener los resultados del procedimiento almacenado
            DataTable dtRubro = Sqlprovider.ExecuteStoredProcedureWithOutputParameter(vSqlProcedureName, CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            //verifica el procedimiento genero algun resultado
            int total_registros = dtRubro.Rows.Count;

            if (total_registros > 0)
            {
                objRubro.Rubro_Id = Convert.ToString(dtRubro.Rows[0]["Rubro_Id"]).Trim();
                objRubro.Descripcion = Convert.ToString(dtRubro.Rows[0]["Descripcion"]).Trim();
                objRubro.GrupoRubro_Id = Convert.ToInt32(dtRubro.Rows[0]["GrupoRubro_Id"]);
            }

            return objRubro;
        }

        /// <summary>
        /// El metodo obtiene el id y la descripción del rubro
        /// </summary>
        /// <param name="Rubro_Id"></param>
        /// <returns></returns>
        //public List<Wrkf_Rubro> GetRubroPorId(string Rubro_Id, int Departamento_Id)
        //{
        //    List<Wrkf_Rubro> lstGrupo = new List<Wrkf_Rubro>();
        //    SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
        //    Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
        //        new SqlParameter("@Rubro_Id", Rubro_Id),
        //        new SqlParameter("@Departamento_Id", Departamento_Id)
        //    });
        //    string sqlQuery = "select Rubro_Id, Descripcion from Workflow.Rubros where Rubro_Id = @Rubro_Id and Departamento_Id = @Departamento_Id order by Descripcion";

        //    //optener los resultados del procedimiento almacenado
        //    DataTable DtGrupo = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

        //    //verifica el procedimiento genero algun resultado
        //    int total_registros = DtGrupo.Rows.Count;

        //    if (total_registros > 0)
        //    {
        //        Wrkf_Rubro ObjGrupo = new Wrkf_Rubro()
        //        {
        //            Rubro_Idx = Convert.ToString(DtGrupo.Rows[0]["Rubro_Id"]).Trim(),
        //            Descripcionx = Convert.ToString(DtGrupo.Rows[0]["Descripcion"]).Trim()
        //        };

        //        lstGrupo.Add(ObjGrupo);
        //    } else
        //    {
        //        Wrkf_Rubro ObjGrupo = new Wrkf_Rubro();
        //        lstGrupo.Add(ObjGrupo);
        //    }

        //    return lstGrupo;
        //}

        /// <summary>
        /// Método para listar los rubros por descripcion
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <param name="Departamento_Id"></param>
        /// <returns></returns>
        //public List<Wrkf_Rubro> GetRubroPorNombre(string Descripcion, int Departamento_Id)
        //{
        //    List<Wrkf_Rubro> lstGrupo = new List<Wrkf_Rubro>();
        //    SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
        //    Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
        //        new SqlParameter("@pDescripcion", Descripcion),
        //        new SqlParameter("@pDepartamento_Id", Departamento_Id)
        //    });

        //    //optener los resultados del procedimiento almacenado
        //    DataTable DtRubro = Sqlprovider.ExecuteStoredProcedure("workflow.PL_Sel_RubroPorNombreDepartamento", CommandType.StoredProcedure);

        //    //verifica el procedimiento genero algun resultado
        //    int total_registros = DtRubro.Rows.Count;

        //    if (total_registros > 0)
        //    {
        //        //ingresa los datos en la lista
        //        for (int i = 0; i < total_registros; i++)
        //        {
        //            Wrkf_Rubro objrubro = new Wrkf_Rubro()
        //            {
        //                Rubro_Idx = Convert.ToString(DtRubro.Rows[i]["Rubro_Id"]).Trim(),
        //                Descripcionx = Convert.ToString(DtRubro.Rows[i]["Descripcion"]).Trim()
        //            };
        //            lstGrupo.Add(objrubro);
        //        }
        //    }
        //    else
        //    {
        //        Wrkf_Rubro objrubro = new Wrkf_Rubro();
        //        lstGrupo.Add(objrubro);
        //    }

        //    return lstGrupo;
        //}
    }
}