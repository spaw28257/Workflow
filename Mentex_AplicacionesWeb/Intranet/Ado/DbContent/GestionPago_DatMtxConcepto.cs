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
    public class GestionPago_DatMtxConcepto
    {
        /// <summary>
        /// Listar todos los conceptos de gestion pago
        /// </summary>
        /// <returns></returns>
        public List<GestionPago_EntMtxConcepto> ListarConceptosTodos()
        {
            List<GestionPago_EntMtxConcepto> lstConceptosTodos = new List<GestionPago_EntMtxConcepto>();

            SQLClient Sqlprovider = new SQLClient((int)BasedeDatos.CORP);
            Sqlprovider.Oparameters.AddRange(new SqlParameter[] {
            });

            DataTable DtConceptos = Sqlprovider.ExecuteStoredProcedure("GestionPago.PL_Sel_MTX_Concepto_all", CommandType.StoredProcedure);

            int total_registros = DtConceptos.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    GestionPago_EntMtxConcepto objConcepto = new GestionPago_EntMtxConcepto()
                    {
                        Codigo = Convert.ToInt32(DtConceptos.Rows[i]["Codigo"]),
                        Descripcion = Convert.ToString(DtConceptos.Rows[i]["Descripcion"]),
                        Segmento2 = Convert.ToString(DtConceptos.Rows[i]["Segmento2"]),
                        Segmento3 = Convert.ToString(DtConceptos.Rows[i]["Segmento3"]),
                        Tipo = Convert.ToString(DtConceptos.Rows[i]["Tipo"]),
                        CodigoISLRNatural = Convert.ToString(DtConceptos.Rows[i]["CodigoISLRNatural"]),
                        CodigoISLRJuridico = Convert.ToString(DtConceptos.Rows[i]["CodigoISLRJuridico"])
                    };

                    lstConceptosTodos.Add(objConcepto);
                }
            }

            return lstConceptosTodos;
        }
    }
}