using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Intranet.Utilities;
using Intranet.Models;

namespace Intranet.Ado.DbContent
{
    public class Wrkf_DbCalcularMontoSolicitud
    {
        public Wrkf_DbCalcularMontoSolicitud()
        {

        }

        /// <summary>
        /// Calcular el monto total a pagar de la solicitud de laorden de pago
        /// </summary>
        /// <param name="pMontoDocumento"></param>
        /// <param name="pPlanImpuesto"></param>
        /// <returns></returns>
        public List<Wrkf_CalcularMontos> CalcularMontosSolicitud(double pMontoDocumento, double pBaseIvaGe, double pBaseIvaRe, double pBaseIvaAd, string pPlanImpuesto)
        {
            List<Wrkf_CalcularMontos> lstcalcularmontos = new List<Wrkf_CalcularMontos>();
            Wrkf_CalcularMontos objcalcularmontos = new Wrkf_CalcularMontos();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pMontoDocumento", pMontoDocumento),
                new SqlParameter("@pBaseIvaGe", pBaseIvaGe),
                new SqlParameter("@pBaseIvaRe", pBaseIvaRe),
                new SqlParameter("@pBaseIvaAd", pBaseIvaAd),
                new SqlParameter("@pPlanImpuesto", pPlanImpuesto),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@pTituloError", SqlDbType.VarChar, 60)
            });

            Sqlprovider.Oparameters[5].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[6].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[7].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[8].Direction = ParameterDirection.Output;

            //optener los resultados del procedimiento almacenado
            DataTable DtCalcularmonto = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("workflow.PL_Sel_CalcularMontosSolicitud", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            ////Obtiene El Error Generado Desde El Procedimiento Almacenado [workflow].[PL_InsUpd_SolicitudOrdenPago_key].
            if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
            {
                objcalcularmontos.Codigox = outparam["@pCodigoError"];
                objcalcularmontos.Mensajex = outparam["@pMensajeError"];
                objcalcularmontos.Tipox = outparam["@pTipo"];
                objcalcularmontos.Titulox = outparam["@pTitulo"];
            }
            else
            {
                objcalcularmontos.Porcentajeiva = Convert.ToDouble(DtCalcularmonto.Rows[0]["PorcentajeIva"]);
                objcalcularmontos.Montoiva = Convert.ToDouble(DtCalcularmonto.Rows[0]["MontoIva"]);
                objcalcularmontos.Porcentajeretencion = Convert.ToDouble(DtCalcularmonto.Rows[0]["PorcentajeRetencion"]);
                objcalcularmontos.Montoretencion = Convert.ToDouble(DtCalcularmonto.Rows[0]["MontoRetencion"]);
                objcalcularmontos.Totalapagar = Convert.ToDouble(DtCalcularmonto.Rows[0]["TotalPagar"]);
                objcalcularmontos.Codigox = string.Empty;
                objcalcularmontos.Mensajex = string.Empty;
                objcalcularmontos.Tipox = string.Empty;
                objcalcularmontos.Titulox = string.Empty;
            }

            lstcalcularmontos.Add(objcalcularmontos);

            return lstcalcularmontos;
        }
    }
}