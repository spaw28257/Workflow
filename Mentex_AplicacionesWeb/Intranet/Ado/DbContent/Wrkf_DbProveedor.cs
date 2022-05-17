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
    public class Wrkf_DbProveedor
    {
        public Wrkf_DbProveedor()
        {
        }

        /// <summary>
        /// El método retorna una lista con los proveedores que correspondan al rubro seleccionado y la moneda 
        /// </summary>
        /// <param name="vndclsid"></param>
        /// <param name="curncyid"></param>
        /// <returns></returns>
        public List<Wrkf_Proveedores> GetListadoProveedor()
        {
            List<Wrkf_Proveedores> lstProveedor = new List<Wrkf_Proveedores>();

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

            string vSqlProcedureName = "WorkFlow.PL_Sel_Proveedor_All";

            DataTable DtListadoProveedor = Sqlprovider.ExecuteStoredProcedureWithOutputParameter(vSqlProcedureName, CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            int total_registros = DtListadoProveedor.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_Proveedores objProveedor = new Wrkf_Proveedores()
                    {
                        Vendorid = Convert.ToString(DtListadoProveedor.Rows[i]["VENDORID"]).Trim(),
                        Vendname = Convert.ToString(DtListadoProveedor.Rows[i]["VENDNAME"]).Trim()
                    };

                    lstProveedor.Add(objProveedor);
                }
            } 
            else
            {
                Wrkf_Proveedores objProveedor = new Wrkf_Proveedores();
                lstProveedor.Add(objProveedor);
            }

            return lstProveedor;
        }

        /// <summary>
        /// El método retorna una lista con los proveedores que correspondan al rubro seleccionado y la moneda 
        /// </summary>
        /// <param name="vndclsid"></param>
        /// <param name="curncyid"></param>
        /// <returns></returns>
        public List<Wrkf_Proveedores> GetListadoProveedorPorId(string pVendorid)
        {
            List<Wrkf_Proveedores> lstProveedor = new List<Wrkf_Proveedores>();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pVENDORID", pVendorid)
            });

            string vSqlProcedureName = "GestionPago.PL_Sel_PM00200_key";

            DataTable DtListadoProveedor = Sqlprovider.ExecuteStoredProcedure(vSqlProcedureName, CommandType.StoredProcedure);

            int total_registros = DtListadoProveedor.Rows.Count;

            if (total_registros > 0)
            {
                Wrkf_Proveedores objProveedorModel = new Wrkf_Proveedores()
                {
                    Vendorid = Convert.ToString(DtListadoProveedor.Rows[0]["VENDORID"]).Trim(),
                    Vendname = Convert.ToString(DtListadoProveedor.Rows[0]["VENDNAME"]).Trim(),
                    Txrgnnum = Convert.ToString(DtListadoProveedor.Rows[0]["TXRGNNUM"]).Trim(),
                    Vndclsid = Convert.ToString(DtListadoProveedor.Rows[0]["VNDCLSID"]).Trim(),
                    Vtaxschid = Convert.ToString(DtListadoProveedor.Rows[0]["TAXSCHID"]).Trim()
                };

                lstProveedor.Add(objProveedorModel);
            }
            else
            {
                Wrkf_Proveedores objProveedorModel = new Wrkf_Proveedores();
                lstProveedor.Add(objProveedorModel);
            }

            return lstProveedor;
        }

        /// <summary>
        /// this method get the tax registration number by provider id
        /// </summary>
        /// <param name="provider_id"></param>
        /// <returns></returns>
        public string GetTaxRegistrationNumber(string vendorid)
        {
            string taxregistrationnumber;

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@vendorid", vendorid)
            });

            string SqlQuery = "select txrgnnum from dbo.pm00200 where vendorid = @vendorid";

            DataTable Dttaxregistrationnumber = Sqlprovider.ExecuteStoredProcedure(SqlQuery, CommandType.Text);

            int total_records = Dttaxregistrationnumber.Rows.Count;

            if (total_records > 0)
            {
                taxregistrationnumber = Dttaxregistrationnumber.Rows[0]["txrgnnum"].ToString().Trim();
            }
            else
            {
                taxregistrationnumber = "";
            }

            return taxregistrationnumber;
        }
    }
}