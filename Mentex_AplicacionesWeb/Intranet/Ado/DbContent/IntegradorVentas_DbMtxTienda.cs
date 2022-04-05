using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Intranet.Utilities;
using Intranet.Models;

namespace Intranet.Ado.DbContent
{
    public class IntegradorVentas_DbMtxTienda
    {
        //Constructor
        public IntegradorVentas_DbMtxTienda()
        {
        }

        /// <summary>
        /// Metodo para listar las tiendas
        /// </summary>
        /// <returns></returns>
        public List<IntegradorVentas_MtxTienda> Listartiendas(string usuario)
        {
            List<IntegradorVentas_MtxTienda> lstIntegradorVentas_MtxTienda = new List<IntegradorVentas_MtxTienda>();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError mensajeerror;

            try
            {
                SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {});

                StringBuilder query = new StringBuilder("SELECT Tienda, TipoInventario, ZonaLibre, RemesaTarjeta, ConEgreso, DireccionIP, ");
                query.Append("PrecierrePDF, IntegraVenDol FROM IntegradorVentas.MTX_Tienda");

                DataTable DtRegistros = Sqlprovider.ExecuteStoredProcedure(query.ToString(), CommandType.Text);

                int total_registro = DtRegistros.Rows.Count;

                if (total_registro > 0)
                {
                    for (int i = 0; i < total_registro; i++)
                    {
                        IntegradorVentas_MtxTienda objintegradorventas_mtxtienda = new IntegradorVentas_MtxTienda()
                        {
                            Tienda = DtRegistros.Rows[i]["Tienda"].ToString(),
                            Tipoinventario = DtRegistros.Rows[i]["TipoInventario"].ToString(),
                            Zonalibre = Convert.ToBoolean(DtRegistros.Rows[i]["ZonaLibre"]),
                            Remesatarjeta = Convert.ToBoolean(DtRegistros.Rows[i]["RemesaTarjeta"]),
                            Conegreso = Convert.ToBoolean(DtRegistros.Rows[i]["ConEgreso"]),
                            Direccionip = DtRegistros.Rows[i]["DireccionIP"].ToString(),
                            Precierrepdf = Convert.ToBoolean(DtRegistros.Rows[i]["PrecierrePDF"]),
                            Integravendol = Convert.ToBoolean(DtRegistros.Rows[i]["IntegraVenDol"])
                        };

                        lstIntegradorVentas_MtxTienda.Add(objintegradorventas_mtxtienda);
                    }
                }
                else
                {
                    //obtiene el mensaje de error
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("00001", "IntegradorVentas_DbMtxTienda");
                    IntegradorVentas_MtxTienda objintegradorventas_mtxtienda = new IntegradorVentas_MtxTienda()
                    {
                        Codigox = mensajeerror.Codigox,
                        Mensajex = mensajeerror.Mensajex,
                        Tipox = mensajeerror.Tipox,
                        Titulox = mensajeerror.Titulox
                    };

                    lstIntegradorVentas_MtxTienda.Add(objintegradorventas_mtxtienda);
                }
            }
            catch  (Exception ex)
            {
                //obtiene el mensaje de error
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                IntegradorVentas_MtxTienda objintegradorventas_mtxtienda = new IntegradorVentas_MtxTienda()
                {
                    Codigox = mensajeerror.Codigox,
                    Mensajex = mensajeerror.Mensajex,
                    Tipox = mensajeerror.Tipox,
                    Titulox = mensajeerror.Titulox
                };

                lstIntegradorVentas_MtxTienda.Add(objintegradorventas_mtxtienda);

                //registra en el log de errores
                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), usuario, "IntegradorVentas_DbMtxTienda/Listartiendas");
            }

            return lstIntegradorVentas_MtxTienda;
        }

        /// <summary>
        /// Metodo para seleccionar la tienda por código
        /// </summary>
        /// <returns></returns>
        public IntegradorVentas_MtxTienda Seleccionartienda(string tienda, string usuario)
        {
            IntegradorVentas_MtxTienda integradorventas_mtxtienda = new IntegradorVentas_MtxTienda();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError mensajeerror;

            try
            {
                SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@Tienda", tienda)
                });

                StringBuilder query = new StringBuilder("SELECT Tienda, TipoInventario, ZonaLibre, RemesaTarjeta, ConEgreso, DireccionIP, ");
                query.Append("PrecierrePDF, IntegraVenDol FROM IntegradorVentas.MTX_Tienda ");
                query.Append("WHERE Tienda = @Tienda");

                DataTable DtRegistros = Sqlprovider.ExecuteStoredProcedure(query.ToString(), CommandType.Text);

                int total_registro = DtRegistros.Rows.Count;

                if (total_registro > 0)
                {
                    integradorventas_mtxtienda.Tienda = DtRegistros.Rows[0]["Tienda"].ToString();
                    integradorventas_mtxtienda.Tipoinventario = DtRegistros.Rows[0]["TipoInventario"].ToString();
                    integradorventas_mtxtienda.Zonalibre = Convert.ToBoolean(DtRegistros.Rows[0]["ZonaLibre"]);
                    integradorventas_mtxtienda.Remesatarjeta = Convert.ToBoolean(DtRegistros.Rows[0]["RemesaTarjeta"]);
                    integradorventas_mtxtienda.Conegreso = Convert.ToBoolean(DtRegistros.Rows[0]["ConEgreso"]);
                    integradorventas_mtxtienda.Direccionip = DtRegistros.Rows[0]["DireccionIP"].ToString();
                    integradorventas_mtxtienda.Precierrepdf = Convert.ToBoolean(DtRegistros.Rows[0]["PrecierrePDF"]);
                    integradorventas_mtxtienda.Integravendol = Convert.ToBoolean(DtRegistros.Rows[0]["IntegraVenDol"]);
                }
                else
                {
                    //obtiene el mensaje de error
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("00001", "IntegradorVentas_DbMtxTienda");
                    integradorventas_mtxtienda.Codigox = mensajeerror.Codigox;
                    integradorventas_mtxtienda.Mensajex = mensajeerror.Mensajex;
                    integradorventas_mtxtienda.Tipox = mensajeerror.Tipox;
                    integradorventas_mtxtienda.Titulox = mensajeerror.Titulox;
                }
            }
            catch (Exception ex)
            {
                //obtiene el mensaje de error
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                integradorventas_mtxtienda.Codigox = mensajeerror.Codigox;
                integradorventas_mtxtienda.Mensajex = mensajeerror.Mensajex;
                integradorventas_mtxtienda.Tipox = mensajeerror.Tipox;
                integradorventas_mtxtienda.Titulox = mensajeerror.Titulox;


                //registra en el log de errores
                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), usuario, "IntegradorVentas_DbMtxTienda/Seleccionartienda");
            }

            return integradorventas_mtxtienda;
        }
    }
}