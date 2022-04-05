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
        /// Ejecutarel procedimiento almacenado para calcular losmontos de la solicitud
        /// </summary>
        /// <param name="calculariva"></param>
        /// <param name="calcularretencion"></param>
        /// <param name="cantidad"></param>
        /// <param name="preciounitario"></param>
        /// <param name="anticipo"></param>
        /// <param name="porcentajeretencion"></param>
        /// <returns></returns>
        public List<Wrkf_CalcularMontos> CalcularMontosSolicitud(bool calculariva, bool calcularretencion, double cantidad, double preciounitario, double anticipo, double porcentajeretencion)
        {
            List<Wrkf_CalcularMontos> lstcalcularmontos = new List<Wrkf_CalcularMontos>();
            Wrkf_CalcularMontos objcalcularmontos = new Wrkf_CalcularMontos();

            double Porcentajeivax;
            double Montoivax;
            double Porcentajeretencionx;
            double Montoretencionx;
            double Subtotalx;
            double Totalx;

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pcalculariva", calculariva),
                new SqlParameter("@pcalcularretencion", calcularretencion),
                new SqlParameter("@pcantidad", cantidad),
                new SqlParameter("@ppreciounitario", preciounitario),
                new SqlParameter("@panticipo", anticipo),
                new SqlParameter("@pporcentajeretencion", porcentajeretencion),
                new SqlParameter("@pporcentajeivax", SqlDbType.Money),
                new SqlParameter("@pmontoivax", SqlDbType.Money),
                new SqlParameter("@pporcentajeretencionx", SqlDbType.Money),
                new SqlParameter("@pmontoretencionx", SqlDbType.Money),
                new SqlParameter("@psubtotalx", SqlDbType.Money),
                new SqlParameter("@ptotalx", SqlDbType.Money),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipo", SqlDbType.VarChar, 20),
                new SqlParameter("@pTitulo", SqlDbType.VarChar, 30)
            });

            Sqlprovider.Oparameters[6].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[7].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[8].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[9].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[10].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[11].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[12].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[13].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[14].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[15].Direction = ParameterDirection.Output;

            //optener los resultados del procedimiento almacenado
            Sqlprovider.ExecuteStoredProcedureWithOutputParameter2("workflow.PL_Sel_CalcularMontosSolicitud", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            Porcentajeivax = Convert.ToDouble(outparam["@pporcentajeivax"]);
            Montoivax = Convert.ToDouble(outparam["@pmontoivax"]);
            Porcentajeretencionx = Convert.ToDouble(outparam["@pporcentajeretencionx"]);
            Montoretencionx = Convert.ToDouble(outparam["@pmontoretencionx"]);
            Subtotalx = Convert.ToDouble(outparam["@psubtotalx"]);
            Totalx = Convert.ToDouble(outparam["@ptotalx"]);

            //Obtiene El Error Generado Desde El Procedimiento Almacenado [workflow].[PL_InsUpd_SolicitudOrdenPago_key].
            if (!string.IsNullOrEmpty(outparam["@pCodigoError"]))
            {
                objcalcularmontos.Porcentajeiva = Porcentajeivax.ToString("N", new CultureInfo("is-IS"));
                objcalcularmontos.Montoiva = Montoivax.ToString("N", new CultureInfo("is-IS"));
                objcalcularmontos.Porcentajeretencion = Porcentajeretencionx.ToString("N", new CultureInfo("is-IS"));
                objcalcularmontos.Montoretencion = Montoretencionx.ToString("N", new CultureInfo("is-IS"));
                objcalcularmontos.Subtotal = Subtotalx.ToString("N", new CultureInfo("is-IS"));
                objcalcularmontos.Total = Totalx.ToString("N", new CultureInfo("is-IS"));
                objcalcularmontos.Codigox = outparam["@pCodigoError"];
                objcalcularmontos.Mensajex = outparam["@pMensajeError"];
                objcalcularmontos.Tipox = outparam["@pTipo"];
                objcalcularmontos.Titulox = outparam["@pTitulo"];
            }
            else
            {
                objcalcularmontos.Porcentajeiva = Porcentajeivax.ToString("N", new CultureInfo("is-IS"));
                objcalcularmontos.Montoiva = Montoivax.ToString("N", new CultureInfo("is-IS"));
                objcalcularmontos.Porcentajeretencion = Porcentajeretencionx.ToString("N", new CultureInfo("is-IS"));
                objcalcularmontos.Montoretencion = Montoretencionx.ToString("N", new CultureInfo("is-IS"));
                objcalcularmontos.Subtotal = Subtotalx.ToString("N", new CultureInfo("is-IS"));
                objcalcularmontos.Total = Totalx.ToString("N", new CultureInfo("is-IS"));
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