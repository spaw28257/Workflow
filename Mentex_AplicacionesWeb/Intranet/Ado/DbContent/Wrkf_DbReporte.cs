using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Intranet.Utilities;
using Intranet.Models;
using System.Globalization;

namespace Intranet.Ado.DbContent
{
    /// <summary>
    /// Clase para generar los reportes de la aplicación
    /// </summary>
    public class Wrkf_DbReporte
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Wrkf_DbReporte()
        {
        }

        /// <summary>
        /// Genera un reporte que muestra los pagos pendientes por aprobación por área
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_ListaPagosPorRubroId> GenerarReportePagosPendientes()
        {
            List<Wrkf_ListaPagosPorRubroId> lstpagospendientesporarea = new List<Wrkf_ListaPagosPorRubroId>();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@CodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@MensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@TipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@TituloError", SqlDbType.VarChar, 60)
            });

            Sqlprovider.Oparameters[0].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[1].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;

            DataTable Dtreporte = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("Workflow.sp_sel_ReportePagosPorArea", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            int total_registros = Dtreporte.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    double preciounitario = Convert.ToDouble(Dtreporte.Rows[i]["Preciounitario"]);
                    double montoiva = Convert.ToDouble(Dtreporte.Rows[i]["Montoiva"]);
                    double totalretenido = Convert.ToDouble(Dtreporte.Rows[i]["Totalretenido"]);
                    double subtotal = Convert.ToDouble(Dtreporte.Rows[i]["Subtotal"]);
                    double total = Convert.ToDouble(Dtreporte.Rows[i]["Total"]);

                    Wrkf_ListaPagosPorRubroId wrkf_listapagosporrubroid = new Wrkf_ListaPagosPorRubroId()
                    {
                        Solicitudordenpago_Idx = Convert.ToInt32(Dtreporte.Rows[i]["Solicitudordenpago_Id"]),
                        Codigoplantillax = Dtreporte.Rows[i]["Codigoplantilla"].ToString(),
                        Nombreplantillax = Dtreporte.Rows[i]["Nombreplantilla"].ToString(),
                        Rifx = Dtreporte.Rows[i]["Rif"].ToString(),
                        Proveedorx = Dtreporte.Rows[i]["Proveedor"].ToString(),
                        Descripcionx = Dtreporte.Rows[i]["Descripcion"].ToString(),
                        Numerodocumentox = Dtreporte.Rows[i]["Numerodocumento"].ToString(),
                        Preciounitariox = preciounitario.ToString("N", new CultureInfo("is-IS")),
                        Montoivax = montoiva.ToString("N", new CultureInfo("is-IS")),
                        Totalretenidox = totalretenido.ToString("N", new CultureInfo("is-IS")),
                        Subtotalx = subtotal.ToString("N", new CultureInfo("is-IS")),
                        Totalx = total.ToString("N", new CultureInfo("is-IS")),
                        Curncyidx = Dtreporte.Rows[i]["curncyid"].ToString(),
                        FechaDocumentox = Dtreporte.Rows[i]["FechaDocumento"].ToString(),
                        FechaPagox = Dtreporte.Rows[i]["Fechapago"].ToString(),
                        Estatusx = Dtreporte.Rows[i]["Estatus"].ToString(),
                        Prioridax = Dtreporte.Rows[i]["Prioridad"].ToString()
                    };

                    lstpagospendientesporarea.Add(wrkf_listapagosporrubroid);
                }
            }
            else
            {
                Wrkf_ListaPagosPorRubroId wrkf_listapagosporrubroid = new Wrkf_ListaPagosPorRubroId();
                lstpagospendientesporarea.Add(wrkf_listapagosporrubroid);
            }

            return lstpagospendientesporarea;
        }

        /// <summary>
        /// Crea una lista con las solicitudes de pagos anuladas
        /// </summary>
        public List<Wrkf_ListaPagosPorRubroId> GenerarReporteSolicitudesAnuladas()
        {
            List<Wrkf_ListaPagosPorRubroId> lstpagospendientesporarea = new List<Wrkf_ListaPagosPorRubroId>();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@CodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@MensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@TipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@TituloError", SqlDbType.VarChar, 60)
            });

            Sqlprovider.Oparameters[0].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[1].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;

            DataTable Dtreporte = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("Workflow.sp_sel_ReportePagosPorArea", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            int total_registros = Dtreporte.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    double preciounitario = Convert.ToDouble(Dtreporte.Rows[i]["Preciounitario"]);
                    double montoiva = Convert.ToDouble(Dtreporte.Rows[i]["Montoiva"]);
                    double totalretenido = Convert.ToDouble(Dtreporte.Rows[i]["Totalretenido"]);
                    double subtotal = Convert.ToDouble(Dtreporte.Rows[i]["Subtotal"]);
                    double total = Convert.ToDouble(Dtreporte.Rows[i]["Total"]);

                    Wrkf_ListaPagosPorRubroId wrkf_listapagosporrubroid = new Wrkf_ListaPagosPorRubroId()
                    {
                        Solicitudordenpago_Idx = Convert.ToInt32(Dtreporte.Rows[i]["Solicitudordenpago_Id"]),
                        Codigoplantillax = Dtreporte.Rows[i]["Codigoplantilla"].ToString(),
                        Nombreplantillax = Dtreporte.Rows[i]["Nombreplantilla"].ToString(),
                        Rifx = Dtreporte.Rows[i]["Rif"].ToString(),
                        Proveedorx = Dtreporte.Rows[i]["Proveedor"].ToString(),
                        Descripcionx = Dtreporte.Rows[i]["Descripcion"].ToString(),
                        Numerodocumentox = Dtreporte.Rows[i]["Numerodocumento"].ToString(),
                        Preciounitariox = preciounitario.ToString("N", new CultureInfo("is-IS")),
                        Montoivax = montoiva.ToString("N", new CultureInfo("is-IS")),
                        Totalretenidox = totalretenido.ToString("N", new CultureInfo("is-IS")),
                        Subtotalx = subtotal.ToString("N", new CultureInfo("is-IS")),
                        Totalx = total.ToString("N", new CultureInfo("is-IS")),
                        Curncyidx = Dtreporte.Rows[i]["curncyid"].ToString(),
                        FechaDocumentox = Dtreporte.Rows[i]["FechaDocumento"].ToString(),
                        FechaPagox = Dtreporte.Rows[i]["Fechapago"].ToString(),
                        Estatusx = Dtreporte.Rows[i]["Estatus"].ToString(),
                        Prioridax = Dtreporte.Rows[i]["Prioridad"].ToString()
                    };

                    lstpagospendientesporarea.Add(wrkf_listapagosporrubroid);
                }
            }
            else
            {
                Wrkf_ListaPagosPorRubroId wrkf_listapagosporrubroid = new Wrkf_ListaPagosPorRubroId();
                lstpagospendientesporarea.Add(wrkf_listapagosporrubroid);
            }

            return lstpagospendientesporarea;
        }
    }
}