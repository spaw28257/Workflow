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
    public class Wrkf_DbRubro
    {
        /// <summary>
        /// Cosntructor de la clase
        /// </summary>
        public Wrkf_DbRubro()
        {
        }

        /// <summary>
        /// El método obtiene el listado de los grupos de rubros para asignar al pago
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_Rubro> GetRubro(int Departamento_Id)
        {
            List<Wrkf_Rubro> lstGrupo = new List<Wrkf_Rubro>();
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@Departamento_Id", Departamento_Id)
            });
            string sqlQuery = "select Rubro_Id, Descripcion from Workflow.Rubros where Departamento_Id = @Departamento_Id order by Descripcion";

            //optener los resultados del procedimiento almacenado
            DataTable DtGrupo = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

            //verifica el procedimiento genero algun resultado
            int total_registros = DtGrupo.Rows.Count;

            if (total_registros > 0)
            {
                //ingresa los datos en la lista
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_Rubro ObjGrupo = new Wrkf_Rubro()
                    {
                        Rubro_Idx = Convert.ToString(DtGrupo.Rows[i]["Rubro_Id"]).Trim(),
                        Descripcionx = Convert.ToString(DtGrupo.Rows[i]["Descripcion"]).Trim()
                    };
                    lstGrupo.Add(ObjGrupo);
                }
            }

            return lstGrupo;
        }

        /// <summary>
        /// El metodo obtiene el id y la descripción del rubro
        /// </summary>
        /// <param name="Rubro_Id"></param>
        /// <returns></returns>
        public List<Wrkf_Rubro> GetRubroPorId(string Rubro_Id, int Departamento_Id)
        {
            List<Wrkf_Rubro> lstGrupo = new List<Wrkf_Rubro>();
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@Rubro_Id", Rubro_Id),
                new SqlParameter("@Departamento_Id", Departamento_Id)
            });
            string sqlQuery = "select Rubro_Id, Descripcion from Workflow.Rubros where Rubro_Id = @Rubro_Id and Departamento_Id = @Departamento_Id order by Descripcion";

            //optener los resultados del procedimiento almacenado
            DataTable DtGrupo = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

            //verifica el procedimiento genero algun resultado
            int total_registros = DtGrupo.Rows.Count;

            if (total_registros > 0)
            {
                Wrkf_Rubro ObjGrupo = new Wrkf_Rubro()
                {
                    Rubro_Idx = Convert.ToString(DtGrupo.Rows[0]["Rubro_Id"]).Trim(),
                    Descripcionx = Convert.ToString(DtGrupo.Rows[0]["Descripcion"]).Trim()
                };

                lstGrupo.Add(ObjGrupo);
            } else
            {
                Wrkf_Rubro ObjGrupo = new Wrkf_Rubro();
                lstGrupo.Add(ObjGrupo);
            }

            return lstGrupo;
        }

        /// <summary>
        /// Método para listar los rubros por descripcion
        /// </summary>
        /// <param name="Descripcion"></param>
        /// <param name="Departamento_Id"></param>
        /// <returns></returns>
        public List<Wrkf_Rubro> GetRubroPorNombre(string Descripcion, int Departamento_Id)
        {
            List<Wrkf_Rubro> lstGrupo = new List<Wrkf_Rubro>();
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pDescripcion", Descripcion),
                new SqlParameter("@pDepartamento_Id", Departamento_Id)
            });

            //optener los resultados del procedimiento almacenado
            DataTable DtRubro = Sqlprovider.ExecuteStoredProcedure("workflow.PL_Sel_RubroPorNombreDepartamento", CommandType.StoredProcedure);

            //verifica el procedimiento genero algun resultado
            int total_registros = DtRubro.Rows.Count;

            if (total_registros > 0)
            {
                //ingresa los datos en la lista
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_Rubro objrubro = new Wrkf_Rubro()
                    {
                        Rubro_Idx = Convert.ToString(DtRubro.Rows[i]["Rubro_Id"]).Trim(),
                        Descripcionx = Convert.ToString(DtRubro.Rows[i]["Descripcion"]).Trim()
                    };
                    lstGrupo.Add(objrubro);
                }
            }
            else
            {
                Wrkf_Rubro objrubro = new Wrkf_Rubro();
                lstGrupo.Add(objrubro);
            }

            return lstGrupo;
        }
    }
}