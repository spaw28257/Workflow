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
            });

            string sqlQuery = "select vendorid, vendname, txrgnnum, vndclsid from dbo.pm00200 order by vendname";

            DataTable DtListadoProveedor = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

            int total_registros = DtListadoProveedor.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_Proveedores objProveedor = new Wrkf_Proveedores()
                    {
                        Vendorid = Convert.ToString(DtListadoProveedor.Rows[i]["vendorid"]).Trim(),
                        Vendname = Convert.ToString(DtListadoProveedor.Rows[i]["vendname"]).Trim(),
                        Txrgnnum = Convert.ToString(DtListadoProveedor.Rows[i]["txrgnnum"]).Trim(),
                        Vndclsid = Convert.ToString(DtListadoProveedor.Rows[i]["vndclsid"]).Trim()
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
        public List<Wrkf_Proveedores> GetListadoProveedorPorId(string vendorid)
        {
            List<Wrkf_Proveedores> lstProveedor = new List<Wrkf_Proveedores>();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@vendorid", vendorid)
            });

            string sqlQuery = "select vendorid, vendname, txrgnnum, vndclsid from dbo.pm00200 where vendorid = @vendorid order by vendname";

            DataTable DtListadoProveedor = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

            int total_registros = DtListadoProveedor.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_Proveedores objProveedor = new Wrkf_Proveedores()
                    {
                        Vendorid = Convert.ToString(DtListadoProveedor.Rows[i]["vendorid"]).Trim(),
                        Vendname = Convert.ToString(DtListadoProveedor.Rows[i]["vendname"]).Trim(),
                        Txrgnnum = Convert.ToString(DtListadoProveedor.Rows[i]["txrgnnum"]).Trim(),
                        Vndclsid = Convert.ToString(DtListadoProveedor.Rows[i]["vndclsid"]).Trim()
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
        /// El método retorna una lista con los proveedores que coincidan con el nombre especificado
        /// </summary>
        /// <param name="vndclsid"></param>
        /// <param name="vendname"></param>
        /// <param name="curncyid"></param>
        /// <returns></returns>
        public List<Wrkf_Proveedores> GetListadoProveedorPorNombre(string vendname)
        {
            List<Wrkf_Proveedores> lstProveedor = new List<Wrkf_Proveedores>();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@vendname", vendname)
            });

            string SqlQuery = "	select vendorid, vendname, txrgnnum, vndclsid from dbo.pm00200 where ltrim(rtrim(vendname)) like '%' + @vendname + '%'";

            DataTable DtListadoProveedor = Sqlprovider.ExecuteStoredProcedure(SqlQuery, CommandType.Text);

            int total_registros = DtListadoProveedor.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_Proveedores objProveedor = new Wrkf_Proveedores()
                    {
                        Vendorid = Convert.ToString(DtListadoProveedor.Rows[i]["vendorid"]).Trim(),
                        Vendname = Convert.ToString(DtListadoProveedor.Rows[i]["vendname"]).Trim(),
                        Txrgnnum = Convert.ToString(DtListadoProveedor.Rows[i]["txrgnnum"]).Trim(),
                        Vndclsid = Convert.ToString(DtListadoProveedor.Rows[i]["vndclsid"]).Trim()
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