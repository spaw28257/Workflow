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
    public class Wrkf_DbDepartamento
    {
        public Wrkf_DbDepartamento()
        {
        }

        /// <summary>
        /// El método retorna una lista de los departamentos o grupos registrados
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_Departamento> GetDepartamentos()
        {
            List<Wrkf_Departamento> lstDepartamento = new List<Wrkf_Departamento>();
            //Ejecutar el procedimiento almacenado
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
            });

            string sqlQuery = "select Departamento_Id, Departamento from Workflow.Departamento order by Departamento";

            //optener los resultados del procedimiento almacenado
            DataTable DtDepartamento = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

            //verifica el procedimiento genero algun resultado
            int total_registros = DtDepartamento.Rows.Count;

            if (total_registros > 0)
            {
                //ingresa los datos en la lista lista
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_Departamento objDepartamento = new Wrkf_Departamento()
                    {
                        Departamento_Idx = Convert.ToInt32(DtDepartamento.Rows[i]["Departamento_Id"]),
                        Departamentox = Convert.ToString(DtDepartamento.Rows[i]["Departamento"])
                    };

                    lstDepartamento.Add(objDepartamento);
                }
            }
            else
            {
                Wrkf_Departamento objDepartamento = new Wrkf_Departamento();
            }

            return lstDepartamento;
        }

        /// <summary>
        /// Obtiene los datos del departamento o grupo de rubro por Departamento_Id
        /// </summary>
        /// <param name="Departamento_Id"></param>
        /// <returns></returns>
        public List<Wrkf_Departamento> GetDepartamentosPorId(int Departamento_Id)
        {
            List<Wrkf_Departamento> lstDepartamento = new List<Wrkf_Departamento>();
            //Ejecutar el procedimiento almacenado
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@Departamento_Id", Departamento_Id)
            });

            string sqlQuery = "select Departamento_Id, Departamento from Workflow.Departamento where Departamento_Id = @Departamento_Id";

            //optener los resultados del procedimiento almacenado
            DataTable DtDepartamento = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

            //verifica el procedimiento genero algun resultado
            int total_registros = DtDepartamento.Rows.Count;

            if (total_registros > 0)
            {
                Wrkf_Departamento objDepartamento = new Wrkf_Departamento()
                {
                    Departamento_Idx = Convert.ToInt32(DtDepartamento.Rows[0]["Departamento_Id"]),
                    Departamentox = Convert.ToString(DtDepartamento.Rows[0]["Departamento"])
                };

                lstDepartamento.Add(objDepartamento);
            }
            else
            {
                Wrkf_Departamento objDepartamento = new Wrkf_Departamento();
                lstDepartamento.Add(objDepartamento);
            }

            return lstDepartamento;
        }

        /// <summary>
        /// Obtener departamento por nombre
        /// </summary>
        /// <param name="departamentonombre"></param>
        /// <returns></returns>
        public List<Wrkf_Departamento> GetDepartamentosPorNombre(string departamentonombre)
        {
            List<Wrkf_Departamento> lstDepartamento = new List<Wrkf_Departamento>();
            //Ejecutar el procedimiento almacenado
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pdepartamento", departamentonombre)
            });

            //optener los resultados del procedimiento almacenado
            DataTable DtDepartamento = Sqlprovider.ExecuteStoredProcedure("workflow.PL_Sel_GrupoRubroPorNombre", CommandType.StoredProcedure);

            //verifica el procedimiento genero algun resultado
            int total_registros = DtDepartamento.Rows.Count;

            if (total_registros > 0)
            {
                //ingresa los datos en la lista lista
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_Departamento objDepartamento = new Wrkf_Departamento()
                    {
                        Departamento_Idx = Convert.ToInt32(DtDepartamento.Rows[i]["Departamento_Id"]),
                        Departamentox = Convert.ToString(DtDepartamento.Rows[i]["Departamento"])
                    };

                    lstDepartamento.Add(objDepartamento);
                }
            }
            else
            {
                Wrkf_Departamento objDepartamento = new Wrkf_Departamento();
                lstDepartamento.Add(objDepartamento);
            }

            return lstDepartamento;
        }
    }
}