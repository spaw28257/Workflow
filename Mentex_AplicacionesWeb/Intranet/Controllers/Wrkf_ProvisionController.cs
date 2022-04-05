using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Ado.DbContent;
using Intranet.Models;
using Intranet.Utilities;
using System.Data;

namespace Intranet.Controllers
{
    public class Wrkf_ProvisionController : Controller
    {
        // GET: Wrkf_Provicion
        public ActionResult CargarProvision()
        {
            //Obtener una lista con las opciones de menu
            List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem = new List<Wrkf_OpcionesMenuItem>();
            Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
            lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());
            ViewBag.listaropcionesmenu = lstopcionesmenuitem;

            string format = "yyyy-MM-dd";
            var now = DateTime.Now.ToString(format);

            return View();
        }

        /// <summary>
        /// Genera el listado de provisión para un rango de fecha y codigo de moneda especificado
        /// </summary>
        /// <param name="codigo_moneda"></param>
        /// <param name="fecha_desde"></param>
        /// <param name="fecha_hasta"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public JsonResult GenerarProvision(string codigo_moneda, string fecha_desde, string fecha_hasta, string listadoChequera)
        {
            Wrkf_DbProvision wrkfdbprovision = new Wrkf_DbProvision();
            ConvertExtension convertextension = new ConvertExtension();
            Wrkf_RespuestaOperacion objRespuestaOperacion;
            Wrkf_Provision objProvision = new Wrkf_Provision();
            List<Wrkf_Provision> lstProvision = new List<Wrkf_Provision>();

            if (!string.IsNullOrEmpty(fecha_desde) && !string.IsNullOrEmpty(fecha_hasta))
            {
                int fecha_desde2 = convertextension.FormatoFechayyyyMMdd(Convert.ToDateTime(fecha_desde));
                int fecha_hasta2 = convertextension.FormatoFechayyyyMMdd(Convert.ToDateTime(fecha_hasta));
                objRespuestaOperacion = wrkfdbprovision.GenerarProvision(codigo_moneda, fecha_desde2.ToString(), fecha_hasta2.ToString(), 
                                                                            Session["sUsuario_Id"].ToString(), listadoChequera);

                if (!string.IsNullOrEmpty(objRespuestaOperacion.Codigox))
                {
                    objProvision.Codigox = objRespuestaOperacion.Codigox;
                    objProvision.Mensajex = objRespuestaOperacion.Mensajex;
                    objProvision.Tipox = objRespuestaOperacion.Tipox;
                    objProvision.Titulox = objRespuestaOperacion.Titulox;

                    lstProvision.Add(objProvision);

                    return Json(new { provision = lstProvision });
                }
                else
                {
                    return ListarProvision(codigo_moneda, fecha_desde, fecha_hasta, listadoChequera);
                }
            }
            else
            {
                objProvision.Codigox = "";
                objProvision.Mensajex = "";
                objProvision.Tipox = "";
                objProvision.Titulox = "";

                lstProvision.Add(objProvision);

                return Json(new { provision = lstProvision });
            }
        }

        /// <summary>
        /// Listar las provisiones cargadas de forma manual y por solicitud de orden de pago 
        /// </summary>
        /// <param name="fecha_desde"></param>
        /// <param name="fecha_hasta"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ListarProvision(string codigo_moneda, string fecha_desde, string fecha_hasta, string listadoChequera)
        {
            List<Wrkf_Provision> lstProvision = new List<Wrkf_Provision>();
            Wrkf_DbProvision wrkfdbprovision = new Wrkf_DbProvision();
            ConvertExtension convertextension = new ConvertExtension();
            Wrkf_DbParametros wrkf_dbparametros = new Wrkf_DbParametros();
            Wrkf_Parametros wrkf_parametros;
            FechaHora fechahora = new FechaHora();
            Wrkf_DiaIFSemana wrkfdiaifsemana;
            DataTable DtProvision;

            if (!string.IsNullOrEmpty(fecha_desde) && !string.IsNullOrEmpty(fecha_hasta))
            {
                int fecha_desde2 = convertextension.FormatoFechayyyyMMdd(Convert.ToDateTime(fecha_desde));
                int fecha_hasta2 = convertextension.FormatoFechayyyyMMdd(Convert.ToDateTime(fecha_hasta));
                DtProvision = wrkfdbprovision.ListadoProvisiones(codigo_moneda, fecha_desde2.ToString(), fecha_hasta2.ToString(), Session["sUsuario_Id"].ToString(), listadoChequera);
            }
            else
            {
                //obtiene el formato de fecha yyyy-MM-dd
                wrkf_parametros = wrkf_dbparametros.SeleccionarParametroCodigo("00001", Session["sUsuario_Id"].ToString());
                var now = fechahora.Fecha_Actual2.ToString(wrkf_parametros.ValorAlfaNumerico1);

                wrkfdiaifsemana = convertextension.ObtenerPrimerDiaSemana(Convert.ToDateTime(now));
                int fecha_desde2 = convertextension.FormatoFechayyyyMMdd(Convert.ToDateTime(wrkfdiaifsemana.Primerdiasemana));
                int fecha_hasta2 = convertextension.FormatoFechayyyyMMdd(Convert.ToDateTime(wrkfdiaifsemana.Ultimodiasemana));

                //obtiene el codigo de la moneda por defecto
                wrkf_parametros = wrkf_dbparametros.SeleccionarParametroCodigo("00002", Session["sUsuario_Id"].ToString());
                codigo_moneda = wrkf_parametros.ValorAlfaNumerico1;
                DtProvision = wrkfdbprovision.ListadoProvisiones(codigo_moneda, fecha_desde2.ToString(), fecha_hasta2.ToString(), Session["sUsuario_Id"].ToString(), listadoChequera);
            }

            //obtiene el total de registro de la consulta
            int total_registros = DtProvision.Rows.Count;

            if (total_registros > 0)
            {
                for (int i = 0; i < total_registros; i++)
                {
                    Wrkf_Provision objProvision = new Wrkf_Provision()
                    {
                        IdProvisionx = Convert.ToInt32(DtProvision.Rows[i]["IdProvision"]),
                        IdGrupoRubrox = Convert.ToInt32(DtProvision.Rows[i]["IdGrupoRubro"]),
                        Departamentox = DtProvision.Rows[i]["Departamento"].ToString(),
                        IdRubrox = DtProvision.Rows[i]["IdRubro"].ToString(),
                        Rubrox = DtProvision.Rows[i]["Descripcion"].ToString(),
                        CodigoPlantillax = DtProvision.Rows[i]["CodigoPlantilla"].ToString(),
                        Plantillax = DtProvision.Rows[i]["Nombre"].ToString(),
                        IdProveedorx = DtProvision.Rows[i]["IdProveedor"].ToString(),
                        Proveedorx = DtProvision.Rows[i]["Proveedor"].ToString(),
                        Tiendax = DtProvision.Rows[i]["Tienda"].ToString(),
                        CodigoMonedax = DtProvision.Rows[i]["CodigoMoneda"].ToString(),
                        IdChequerax = DtProvision.Rows[i]["IdChequera"].ToString(),
                        IdFormaPagox = Convert.ToInt32(DtProvision.Rows[i]["IdFormaPago"]),
                        Formapagox = DtProvision.Rows[i]["formadepago"].ToString(),
                        Montox = Convert.ToDouble(DtProvision.Rows[i]["Monto"]),
                        FechaCreacionx = DtProvision.Rows[i]["FechaRegistro"].ToString(),
                        FechaPagox = DtProvision.Rows[i]["Fecha_Pago"].ToString(),
                        Observacionesx = DtProvision.Rows[i]["Observaciones"].ToString(),
                        Anuladax = Convert.ToBoolean(DtProvision.Rows[i]["Anulada"])
                    };

                    lstProvision.Add(objProvision);
                }
            }
            else
            {
                Wrkf_Provision objProvision = new Wrkf_Provision();
                lstProvision.Add(objProvision);
            }
            
            return Json(new { provision = lstProvision });
        }

        /// <summary>
        /// Registrar o actualizar la provisión
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult RegistrarActualizarProvision(string[] item)
        {
            Wrkf_RespuestaOperacion wrkf_respuestaoperacion = new Wrkf_RespuestaOperacion();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            Wrkf_DbProvision wrkf_dbprovision = new Wrkf_DbProvision();
            MensajeError mensajeerror;
            ConvertExtension convertextension = new ConvertExtension();

            try
            {
                Wrkf_Provision objProvision = new Wrkf_Provision()
                {
                   IdProvisionx = Convert.ToInt32(item[0]),
                   IdProveedorx = item[1],
                   CodigoPlantillax = item[2],
                   IdGrupoRubrox = Convert.ToInt32(item[3]),
                   IdRubrox = item[4],
                   Tiendax = item[5],
                   CodigoMonedax = item[6],
                   IdChequerax = item[7],
                   IdFormaPagox = Convert.ToInt32(item[8]),
                   Montox = Convert.ToDouble(item[9]),
                   FechaPagox = convertextension.FormatoFechayyyyMMdd(Convert.ToDateTime(item[10])).ToString(),
                   Observacionesx = item[11],
                   Anuladax = false,
                   Usuariox = Session["sUsuario_Id"].ToString()
                };

                wrkf_respuestaoperacion = wrkf_dbprovision.RegistrarActualizarProvision(objProvision);
            }
            catch (Exception ex)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");

                wrkf_respuestaoperacion.Codigox = mensajeerror.Codigox;
                wrkf_respuestaoperacion.Mensajex = mensajeerror.Mensajex;
                wrkf_respuestaoperacion.Tipox = mensajeerror.Tipox;
                wrkf_respuestaoperacion.Titulox = mensajeerror.Titulox;

                wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Session["sUsuario_Id"].ToString(), "Wrkf_ProvicionController/RegistrarActualizarProvision");
            }

            return Json(wrkf_respuestaoperacion, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Anular la provisión
        /// </summary>
        /// <param name="provision_id"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult AnularProvision(int provision_id)
        //{
        //    try 
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        /// <summary>
        /// Imprimir el reporte de la provisión
        /// </summary>
        /// <param name="m"></param>
        /// <param name="d"></param>
        /// <param name="h"></param>
        public ActionResult ImprimirProvision(string m, string d, string h, string l)
        {
            ViewBag.codigomoneda = m;
            ViewBag.fechadesde = d;
            ViewBag.fechahasta = h;
            ViewBag.listadochequera = l;

            return View();
        }


    }
}