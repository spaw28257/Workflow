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
    public class Wrkf_DbSolicitudOrdenPagoSoporte
    {
        /// <summary>
        /// El método obtiene un listado de los soportes asociados a la solicitud de la orden de pago
        /// </summary>
        /// <returns></returns>
        public List<Wrkf_SolicitudOrdenPagoSoporte> GetListarSoportes(int Solicitudordenpago_Id, int Solicitudordenpagodetalle_Id)
        {
            List<Wrkf_SolicitudOrdenPagoSoporte> lstSoportePagos = new List<Wrkf_SolicitudOrdenPagoSoporte>();
            Wrkf_SolicitudOrdenPagoSoporte objwrkfsolicitudordenpagosoporte = new Wrkf_SolicitudOrdenPagoSoporte();
            string sqlQuery, codigoplantilla;

            try
            {
                SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);

                //verificar si la solicitud de pago es por plantilla o manual
                Sqlprovider.Oparameters = new List<SqlParameter>();
                Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@Solicitudordenpago_Id", Solicitudordenpago_Id),
                });

                sqlQuery = "select Codigoplantilla from Workflow.SolicitudOrdenPago where Solicitudordenpago_Id = @Solicitudordenpago_Id";
                DataTable DtCodPlantilla = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

                if (DtCodPlantilla.Rows.Count > 0)
                {
                    codigoplantilla = DtCodPlantilla.Rows[0]["Codigoplantilla"].ToString();
                }
                else
                {
                    codigoplantilla = "";
                }

                //si los soportes son de una solicitud de pago manual
                if (string.IsNullOrEmpty(codigoplantilla))
                {
                    Sqlprovider.Oparameters = new List<SqlParameter>();
                    Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                        new SqlParameter("@Solicitudordenpago_Id", Solicitudordenpago_Id),
                        new SqlParameter("@Solicitudordenpagodetalle_Id", Solicitudordenpagodetalle_Id)
                    });
                }
                else // si los soportes son de una plantilla
                {
                    Solicitudordenpagodetalle_Id = 0;

                    Sqlprovider.Oparameters = new List<SqlParameter>();
                    Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                        new SqlParameter("@Solicitudordenpago_Id", Solicitudordenpago_Id),
                        new SqlParameter("@Solicitudordenpagodetalle_Id", Solicitudordenpagodetalle_Id)
                    });
                }

                sqlQuery = "select Soporte_id, Solicitudordenpago_Id, Solicitudordenpagodetalle_Id, RutaDirectorio, NombreArchivo ";
                sqlQuery += "from Workflow.SolicitudOrdenPagoSoporte ";
                sqlQuery += "where Solicitudordenpago_Id = @Solicitudordenpago_Id and ";
                sqlQuery += "Solicitudordenpagodetalle_Id = @Solicitudordenpagodetalle_Id";

                //optener los resultados del procedimiento almacenado
                DataTable DtSoportePagos = Sqlprovider.ExecuteStoredProcedure(sqlQuery, CommandType.Text);

                //verifica si la consulta genero algun resultado
                int total_registros = DtSoportePagos.Rows.Count;

                if (total_registros > 0)
                {
                    for (int i = 0; i < total_registros; i++)
                    {
                        Wrkf_SolicitudOrdenPagoSoporte objsoportepagos = new Wrkf_SolicitudOrdenPagoSoporte()
                        {
                            Soporte_idx = Convert.ToInt32(DtSoportePagos.Rows[i]["Soporte_id"]),
                            Solicitudordenpago_Idx = Convert.ToInt32(DtSoportePagos.Rows[i]["Soporte_id"]),
                            Solicitudordenpagodetalle_Idx = Convert.ToInt32(DtSoportePagos.Rows[i]["Solicitudordenpagodetalle_Id"]),
                            RutaDirectoriox = Convert.ToString(DtSoportePagos.Rows[i]["RutaDirectorio"]),
                            NombreArchivox = Convert.ToString(DtSoportePagos.Rows[i]["NombreArchivo"])
                        };
                        lstSoportePagos.Add(objsoportepagos);
                    }
                }
                else
                {
                    Wrkf_SolicitudOrdenPagoSoporte objsoportepagos = new Wrkf_SolicitudOrdenPagoSoporte();
                    lstSoportePagos.Add(objsoportepagos);
                }
            }
            catch (Exception ex)
            {
                objwrkfsolicitudordenpagosoporte.Codigox = ex.HResult.ToString();
                objwrkfsolicitudordenpagosoporte.Mensajex = ex.Message.ToString();
                objwrkfsolicitudordenpagosoporte.Tipox = "error";
                objwrkfsolicitudordenpagosoporte.Titulox = "Solicitud Orden de Pago";

                lstSoportePagos.Add(objwrkfsolicitudordenpagosoporte);
            }

            return lstSoportePagos;
        }
    }
}