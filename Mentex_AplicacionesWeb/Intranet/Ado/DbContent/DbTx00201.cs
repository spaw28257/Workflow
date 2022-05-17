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
    /// Clase para acceder a la tabla TX00201 detalle del impuesto
    /// </summary>
    public class DbTx00201
    {
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public DbTx00201()
        {
        }

        /// <summary>
        /// Lista los detalles de impuestos del proveedor
        /// </summary>
        /// <returns></returns>
        public List<Tx00201> ListaPlanImpuesto_filter_TaxSchedule(string pTaxScheduleID)
        {
            List<Tx00201> lstdetalleimpuesto = new List<Tx00201>();

            //Ejecutar el procedimiento almacenado
            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
                new SqlParameter("@pTaxScheduleID", pTaxScheduleID),
                new SqlParameter("@pCodigoError", SqlDbType.VarChar, 10),
                new SqlParameter("@pMensajeError", SqlDbType.VarChar, 200),
                new SqlParameter("@pTipoError", SqlDbType.VarChar, 20),
                new SqlParameter("@pTituloError", SqlDbType.VarChar, 60)
            });

            Sqlprovider.Oparameters[1].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[2].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[3].Direction = ParameterDirection.Output;
            Sqlprovider.Oparameters[4].Direction = ParameterDirection.Output;

            //optener los resultados del procedimiento almacenado
            DataTable DtPlanImpuesto = Sqlprovider.ExecuteStoredProcedureWithOutputParameter("WorkFlow.PL_Sel_ListaPlanImpuesto_filter_TaxSchedule", CommandType.StoredProcedure, out Dictionary<string, string> outparam);

            //verifica el procedimiento genero algun resultado
            int total_registros = DtPlanImpuesto.Rows.Count;

            if (total_registros > 0)
            {
                //ingresa los datos en la lista lista
                for (int i = 0; i < total_registros; i++)
                {
                    Tx00201 objtx00201 = new Tx00201()
                    {
                        Taxdtlid = DtPlanImpuesto.Rows[i]["TAXDTLID"].ToString(),
                        Txdtldsc = DtPlanImpuesto.Rows[i]["TXDTLDSC"].ToString(),
                        Txdtlpct = Convert.ToDouble(DtPlanImpuesto.Rows[i]["TXDTLPCT"])
                    };

                    lstdetalleimpuesto.Add(objtx00201);
                }
            }
            else
            {
                Tx00201 objtx00201 = new Tx00201();
                lstdetalleimpuesto.Add(objtx00201);
            }

            return lstdetalleimpuesto;
        }
    }
}