using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Ado.DbContent;
using Intranet.Models;
using Intranet.Utilities;

namespace Intranet.Controllers
{
    public class Wrkf_SolicitudOrdenPagoCxPController : Controller
    {
        /// <summary>
        /// Retorna una vista con los pagos que seran aprobados por CXP
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //Verificar que la sesión de usuario este activa
            if (Session["sUsuario_Id"] == null)
            {
                return RedirectToAction("CerrarSesion", "Wrkf_Login");
            }
            else
            {
                try
                {
                    //Obtener una lista con las opciones de menu
                    List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
                    Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();

                    //obtiene el menu de opciones
                    lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());
                    ViewBag.listaropcionesmenu = lstopcionesmenuitem;

                    //obtiene la fecha actual y calcula el primer dia de la semana
                    FechaHora objFechaHora = new FechaHora();
                    ConvertExtension objConvertExtension = new ConvertExtension();
                    //obtiene el rango de la fecha de pago 15 días hacia atras y 15 días hacia adelante
                    Wrkf_DiaIFSemana objwrkf_DiaIFSemana = objConvertExtension.ObtenerPrimerDiaSemana(objFechaHora.Fecha_Actual2);
                    ViewBag.FechaPagoDesde = objwrkf_DiaIFSemana.Primerdiasemana;
                    ViewBag.FechaPagoHasta = objwrkf_DiaIFSemana.Ultimodiasemana;
                }
                catch(Exception ex)
                {
                    MensajeError mensajeerror;
                    Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
                    Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();

                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    objRespuestaOperacion.Codigox = mensajeerror.Codigox;
                    objRespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                    objRespuestaOperacion.Tipox = mensajeerror.Tipox;
                    objRespuestaOperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoCxPController/SolicitudOrdenPagoCxP");

                }
            }

            return View();
        }

        /// <summary>
        /// Realizar la aprobacion de los pagos por el jefe de cuentas por pagar
        /// </summary>
        /// <returns></returns>
        public ActionResult ListadoAprobacionRechazoPagoCxP()
        {
            //Verificar que la sesión de usuario este activa
            if (Session["sUsuario_Id"] == null)
            {
                return RedirectToAction("CerrarSesion", "Wrkf_Login");
            }
            else
            {
                try
                {
                    //Obtener una lista con las opciones de menu
                    List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
                    Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();

                    //obtiene el menu de opciones
                    lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());
                    ViewBag.listaropcionesmenu = lstopcionesmenuitem;

                    //obtiene la fecha actual y calcula el primer dia de la semana
                    FechaHora objFechaHora = new FechaHora();
                    ConvertExtension objConvertExtension = new ConvertExtension();
                    //obtiene el rango de la fecha de pago 15 días hacia atras y 15 días hacia adelante
                    Wrkf_DiaIFSemana objwrkf_DiaIFSemana = objConvertExtension.ObtenerPrimerDiaSemana(objFechaHora.Fecha_Actual2);
                    ViewBag.FechaPagoDesde = objwrkf_DiaIFSemana.Primerdiasemana;
                    ViewBag.FechaPagoHasta = objwrkf_DiaIFSemana.Ultimodiasemana;
                }
                catch (Exception ex)
                {
                    MensajeError mensajeerror;
                    Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
                    Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();

                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    objRespuestaOperacion.Codigox = mensajeerror.Codigox;
                    objRespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                    objRespuestaOperacion.Tipox = mensajeerror.Tipox;
                    objRespuestaOperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoCxPController/AprobacionRechazoPagoCxP");

                }
            }

            return View();
        }

        // GET: Wrkf_SolicitudOrdenPagoCxP
        public ActionResult SolicitudOrdenPagoCxP()
        {
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();

            //Verificar que la sesión de usuario este activa
            if (Session["sUsuario_Id"] == null)
            {
                return RedirectToAction("CerrarSesion", "Wrkf_Login");
            }
            else
            {
                try
                {
                    //Obtener una lista con las opciones de menu
                    List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem = new List<Wrkf_OpcionesMenuItem>();
                    Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
                    lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());
                    ViewBag.listaropcionesmenu = lstopcionesmenuitem;
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    objRespuestaOperacion.Codigox = mensajeerror.Codigox;
                    objRespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                    objRespuestaOperacion.Tipox = mensajeerror.Tipox;
                    objRespuestaOperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoCxPController/SolicitudOrdenPagoCxP");
                }
            }

            return View();
        }

        ///// <summary>
        ///// Muestar un listado de los pagos por el rubro especificado parametros por get
        ///// </summary>
        ///// <param name="rubro_id"></param>
        ///// <returns></returns>
        //public ActionResult ListaPagosPorRubroId(string rubro_id, string fechadesde, string fechahasta, string tipodocumento)
        //{
        //    MensajeError mensajeerror;
        //    Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
        //    Wrkf_RespuestaOperacion objRespuestaOperacion = new Wrkf_RespuestaOperacion();
        //    //Obtener una lista con las opciones de menu
        //    List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
        //    Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
        //    Wrkf_DiaIFSemana wrkfdiaifsemana;
        //    ConvertExtension objconvertextension = new ConvertExtension();
        //    List<Wrkf_ListaPagosPorRubroId> lstpagosporrubro;
        //    //Obtener las formas de pagos
        //    Wrkf_DbFormaPago objdbformapago = new Wrkf_DbFormaPago();
        //    //Obtener el listado de chequeras
        //    List<Wrkf_Chequera> lstchequeras;
        //    Wrkf_DbChequera objdbchequera = new Wrkf_DbChequera();
        //    Wrkf_DbSolicitudOrdenPagoCxP objdbsolicitudordenpagocxp = new Wrkf_DbSolicitudOrdenPagoCxP();

        //    //Verificar que la sesión de usuario este activa
        //    if (Session["sUsuario_Id"] == null)
        //    {
        //        return RedirectToAction("CerrarSesion", "Wrkf_Login");
        //    }
        //    else
        //    {
        //        try
        //        {
        //            lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());
        //            ViewBag.listaropcionesmenu = lstopcionesmenuitem;

        //            string format = "yyyy-MM-dd";
        //            var now = DateTime.Now.ToString(format);

        //            //verificar los rangos de fecha
        //            if ((fechadesde == null || fechadesde == "") && (fechahasta == null || fechahasta == ""))
        //            {
        //                wrkfdiaifsemana = objconvertextension.ObtenerPrimerDiaSemana(Convert.ToDateTime(now));
        //                fechadesde = wrkfdiaifsemana.Primerdiasemana;
        //                fechahasta = wrkfdiaifsemana.Ultimodiasemana;
        //            }

        //            ViewBag.rubro_id = rubro_id;
        //            ViewBag.fechadesde = fechadesde;
        //            ViewBag.fechahasta = fechahasta;
        //            ViewBag.tipodocu = tipodocumento;

        //            if (tipodocumento != "NC")
        //            {
        //                lstpagosporrubro = objdbsolicitudordenpagocxp.ListaPagosPorRubroId(rubro_id, Convert.ToDateTime(fechadesde), Convert.ToDateTime(fechahasta));
        //            }
        //            else
        //            {
        //                lstpagosporrubro = objdbsolicitudordenpagocxp.ListaNotasCreditoPorRubroId(rubro_id, Convert.ToDateTime(fechadesde), Convert.ToDateTime(fechahasta));
        //            }

        //            ViewBag.listarubrospagos = lstpagosporrubro;


        //            List<Wrkf_FormaPago> lstformapago = objdbformapago.GetFormasPago();
        //            ViewBag.listarformapago = lstformapago;


        //            //lstchequeras = objdbchequera.GetListadoChequera();
        //            //ViewBag.listadochequera = lstchequeras;
        //        }
        //        catch (Exception ex)
        //        {
        //            mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
        //            objRespuestaOperacion.Codigox = mensajeerror.Codigox;
        //            objRespuestaOperacion.Mensajex = mensajeerror.Mensajex;
        //            objRespuestaOperacion.Tipox = mensajeerror.Tipox;
        //            objRespuestaOperacion.Titulox = mensajeerror.Titulox;
        //            wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoCxPController/ListaPagosPorRubroId");
        //        }
        //    }

        //    return View();
        //}

        ///// <summary>
        ///// Muestar un listado de los pagos por el rubro especificado parametros por post
        ///// </summary>
        ///// <param name="rubro_id"></param>
        ///// <returns></returns>
        //public ActionResult ListaPagosPorRubroIdPost()
        //{
        //    //Verificar que la sesión de usuario este activa
        //    if (Session["sUsuario_Id"] == null)
        //    {
        //        return RedirectToAction("CerrarSesion", "Wrkf_Login");
        //    } 
        //    else
        //    {
        //        try
        //        {
        //            //Obtener una lista con las opciones de menu
        //            List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem = new List<Wrkf_OpcionesMenuItem>();
        //            Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
        //            Wrkf_DiaIFSemana wrkfdiaifsemana = new Wrkf_DiaIFSemana();
        //            ConvertExtension objconvertextension = new ConvertExtension();
        //            List<Wrkf_ListaPagosPorRubroId> lstpagosporrubro;

        //            string fechadesde;
        //            string fechahasta;
        //            string rubro_id;
        //            string tipodocumento;

        //            lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());
        //            ViewBag.listaropcionesmenu = lstopcionesmenuitem;

        //            string format = "yyyy-MM-dd";
        //            var now = DateTime.Now.ToString(format);

        //            //obtener los pagos asociados al rubro por rango de fecha
        //            rubro_id = Request.Form["rubro_id"];
        //            fechadesde = objconvertextension.FormatoFecha2(Convert.ToDateTime(Request.Form["txtfechadesde"]));
        //            fechahasta = objconvertextension.FormatoFecha2(Convert.ToDateTime(Request.Form["txtfechahasta"]));
        //            tipodocumento = Request.Form["tipodocumento"];

        //            //verificar los rangos de fecha
        //            if ((fechadesde == null || fechadesde == "") && (fechahasta == null || fechahasta == ""))
        //            {
        //                wrkfdiaifsemana = objconvertextension.ObtenerPrimerDiaSemana(Convert.ToDateTime(now));
        //                fechadesde = wrkfdiaifsemana.Primerdiasemana;
        //                fechahasta = wrkfdiaifsemana.Ultimodiasemana;
        //            }

        //            ViewBag.rubro_id = rubro_id;
        //            ViewBag.fechadesde = fechadesde;
        //            ViewBag.fechahasta = fechahasta;
        //            Wrkf_DbSolicitudOrdenPagoCxP objdbsolicitudordenpagocxp = new Wrkf_DbSolicitudOrdenPagoCxP();

        //            if (tipodocumento != "NC")
        //            {
        //                lstpagosporrubro = objdbsolicitudordenpagocxp.ListaPagosPorRubroId(rubro_id, Convert.ToDateTime(fechadesde), Convert.ToDateTime(fechahasta));
        //            }
        //            else
        //            {
        //                lstpagosporrubro = objdbsolicitudordenpagocxp.ListaNotasCreditoPorRubroId(rubro_id, Convert.ToDateTime(fechadesde), Convert.ToDateTime(fechahasta));
        //            }

        //            ViewBag.listarubrospagos = lstpagosporrubro;

        //            //Obtener las formas de pagos
        //            Wrkf_DbFormaPago objdbformapago = new Wrkf_DbFormaPago();
        //            List<Wrkf_FormaPago> lstformapago = objdbformapago.GetFormasPago();
        //            ViewBag.listarformapago = lstformapago;

        //            //Obtener el listado de chequeras
        //            List<Wrkf_Chequera> lstchequeras = new List<Wrkf_Chequera>();
        //            Wrkf_DbChequera objdbchequera = new Wrkf_DbChequera();
        //            //lstchequeras = objdbchequera.GetListadoChequera();
        //            //ViewBag.listadochequera = lstchequeras;
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewBag.CodigoError = "EXC000";
        //            ViewBag.MensajeError = ex.Message.ToString();
        //            ViewBag.TipoError = "error";
        //            ViewBag.TituloError = "Listado Solicitudes Pagos Por Rubro Id";
        //        }
        //    }

        //    return View("ListaPagosPorRubroId");
        //}

        /// <summary>
        /// Obtener el listado de pagos por revisar por el analista de CXP
        /// </summary>
        /// <param name="pFechaPagoDesde"></param>
        /// <param name="pFechaPagoHasta"></param>
        /// <returns></returns>
        public JsonResult GetPagosRevisionCxP(DateTime pFechaPagoDesde, DateTime pFechaPagoHasta)
        {
            List<Wrkf_SolicitudOrdenPago> lstSolicitudOrdenPagoCxP = new List<Wrkf_SolicitudOrdenPago>();
            Wrkf_DbSolicitudOrdenPagoCxP Objwrkf_DbSolicitudOrdenPagoCxP = new Wrkf_DbSolicitudOrdenPagoCxP();
            Wrkf_SolicitudOrdenPago Objwrkf_SolicitudOrdenPagoCxP = new Wrkf_SolicitudOrdenPago();
            ConvertExtension ObjconvertExtension = new ConvertExtension();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                Objwrkf_SolicitudOrdenPagoCxP.Codigox = mensajeerror.Codigox;
                Objwrkf_SolicitudOrdenPagoCxP.Mensajex = mensajeerror.Mensajex;
                Objwrkf_SolicitudOrdenPagoCxP.Tipox = mensajeerror.Tipox;
                Objwrkf_SolicitudOrdenPagoCxP.Titulox = mensajeerror.Titulox;
                lstSolicitudOrdenPagoCxP.Add(Objwrkf_SolicitudOrdenPagoCxP);
            }
            else
            {
                try
                {
                    string vFechaPagoDesde = ObjconvertExtension.FormatoFechayyyyMMdd(pFechaPagoDesde).ToString();
                    string vFechaPagoHasta = ObjconvertExtension.FormatoFechayyyyMMdd(pFechaPagoHasta).ToString();
                    lstSolicitudOrdenPagoCxP = Objwrkf_DbSolicitudOrdenPagoCxP.GetPagosRevisionCxP(vFechaPagoDesde, vFechaPagoHasta);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    Objwrkf_SolicitudOrdenPagoCxP.Codigox = mensajeerror.Codigox;
                    Objwrkf_SolicitudOrdenPagoCxP.Mensajex = mensajeerror.Mensajex;
                    Objwrkf_SolicitudOrdenPagoCxP.Tipox = mensajeerror.Tipox;
                    Objwrkf_SolicitudOrdenPagoCxP.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoCxPController/GetPagosRevisionCxP");
                    lstSolicitudOrdenPagoCxP.Add(Objwrkf_SolicitudOrdenPagoCxP);
                }
            }

            return Json(new { ListadoPago = lstSolicitudOrdenPagoCxP });
        }


        /// <summary>
        /// Obtener el listado de pagos por aprobar por el jefe de CXP
        /// </summary>
        /// <param name="pFechaPagoDesde"></param>
        /// <param name="pFechaPagoHasta"></param>
        /// <returns></returns>
        public JsonResult GetPagosAprobacionCxP(DateTime pFechaPagoDesde, DateTime pFechaPagoHasta)
        {
            List<Wrkf_SolicitudOrdenPago> lstSolicitudOrdenPagoCxP = new List<Wrkf_SolicitudOrdenPago>();
            Wrkf_DbSolicitudOrdenPagoCxP Objwrkf_DbSolicitudOrdenPagoCxP = new Wrkf_DbSolicitudOrdenPagoCxP();
            Wrkf_SolicitudOrdenPago Objwrkf_SolicitudOrdenPagoCxP = new Wrkf_SolicitudOrdenPago();
            ConvertExtension ObjconvertExtension = new ConvertExtension();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                Objwrkf_SolicitudOrdenPagoCxP.Codigox = mensajeerror.Codigox;
                Objwrkf_SolicitudOrdenPagoCxP.Mensajex = mensajeerror.Mensajex;
                Objwrkf_SolicitudOrdenPagoCxP.Tipox = mensajeerror.Tipox;
                Objwrkf_SolicitudOrdenPagoCxP.Titulox = mensajeerror.Titulox;
                lstSolicitudOrdenPagoCxP.Add(Objwrkf_SolicitudOrdenPagoCxP);
            }
            else
            {
                try
                {
                    string vFechaPagoDesde = ObjconvertExtension.FormatoFechayyyyMMdd(pFechaPagoDesde).ToString();
                    string vFechaPagoHasta = ObjconvertExtension.FormatoFechayyyyMMdd(pFechaPagoHasta).ToString();
                    lstSolicitudOrdenPagoCxP = Objwrkf_DbSolicitudOrdenPagoCxP.ListadoAprobacionRechazoPagoCxP(vFechaPagoDesde, vFechaPagoHasta);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    Objwrkf_SolicitudOrdenPagoCxP.Codigox = mensajeerror.Codigox;
                    Objwrkf_SolicitudOrdenPagoCxP.Mensajex = mensajeerror.Mensajex;
                    Objwrkf_SolicitudOrdenPagoCxP.Tipox = mensajeerror.Tipox;
                    Objwrkf_SolicitudOrdenPagoCxP.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoCxPController/GetPagosRevisionCxP");
                    lstSolicitudOrdenPagoCxP.Add(Objwrkf_SolicitudOrdenPagoCxP);
                }
            }

            return Json(new { ListadoPago = lstSolicitudOrdenPagoCxP });
        }

        /// <summary>
        /// Obtiene el detalle de las solicitudes de ordenes de pago por código
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetDetalleOrdenPagoPorRevisarCxP(string pCodigo)
        {
            Wrkf_DbSolicitudOrdenPagoCxP objDbSolicitudOrdenPagoCxP = new Wrkf_DbSolicitudOrdenPagoCxP();
            Wrkf_SolicitudOrdenPago objsolicitudordenpago = new Wrkf_SolicitudOrdenPago();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                objsolicitudordenpago.Codigox = mensajeerror.Codigox;
                objsolicitudordenpago.Mensajex = mensajeerror.Mensajex;
                objsolicitudordenpago.Tipox = mensajeerror.Tipox;
                objsolicitudordenpago.Titulox = mensajeerror.Titulox;
            }
            else
            {
                try
                {
                    objsolicitudordenpago = objDbSolicitudOrdenPagoCxP.GetDetalleOrdenPagoPorRevisarCxP(pCodigo);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    objsolicitudordenpago.Codigox = mensajeerror.Codigox;
                    objsolicitudordenpago.Mensajex = mensajeerror.Mensajex;
                    objsolicitudordenpago.Tipox = mensajeerror.Tipox;
                    objsolicitudordenpago.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoCxPController/GetDetalleOrdenPagoPorRevisarCxP");
                }
            }

            return Json(new { DetallePago = objsolicitudordenpago });
        }

        /// <summary>
        /// Actualiza los datos del pago en la tabla del workflow
        /// </summary>
        /// <param name="pcodigo"></param>
        /// <param name="pgruporubro_id"></param>
        /// <param name="prubro_id"></param>
        /// <param name="psoportesfacturas"></param>
        /// <returns></returns>
        public JsonResult ActualizarGrupoRubro(string pcodigo, int pgruporubro_id, string prubro_id, bool psoportesfacturas, string pobservacion)
        {
            Wrkf_DbSolicitudOrdenPagoCxP objDbSolicitudOrdenPagoCxP = new Wrkf_DbSolicitudOrdenPagoCxP();
            Wrkf_RespuestaOperacion Objwrkf_RespuestaOperacion = new Wrkf_RespuestaOperacion();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                Objwrkf_RespuestaOperacion.Codigox = mensajeerror.Codigox;
                Objwrkf_RespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                Objwrkf_RespuestaOperacion.Tipox = mensajeerror.Tipox;
                Objwrkf_RespuestaOperacion.Titulox = mensajeerror.Titulox;
            }
            else
            {
                try
                {
                    Objwrkf_RespuestaOperacion = objDbSolicitudOrdenPagoCxP.ActualizarGrupoRubro(pcodigo, pgruporubro_id, prubro_id, psoportesfacturas, Convert.ToString(Session["sUsuario_Id"]), pobservacion);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    Objwrkf_RespuestaOperacion.Codigox = mensajeerror.Codigox;
                    Objwrkf_RespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                    Objwrkf_RespuestaOperacion.Tipox = mensajeerror.Tipox;
                    Objwrkf_RespuestaOperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoCxPController/ActualizarGrupoRubro");
                }
            }

            return Json(new { RespuestaOperacion = Objwrkf_RespuestaOperacion });
        }

        /// <summary>
        /// Aprobar o rechazar los pagos por CxP
        /// </summary>
        /// <param name="pcodigo"></param>
        /// <param name="pgruporubro_id"></param>
        /// <param name="prubro_id"></param>
        /// <param name="psoportesfacturas"></param>
        /// <returns></returns>
        public JsonResult AprobarRechazarPagosCxP(string plistapagosaprobados, string plistapagosrechazados, string plistadopagossoportes, string pobservacion)
        {
            Wrkf_DbSolicitudOrdenPagoCxP objDbSolicitudOrdenPagoCxP = new Wrkf_DbSolicitudOrdenPagoCxP();
            Wrkf_RespuestaOperacion Objwrkf_RespuestaOperacion = new Wrkf_RespuestaOperacion();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                Objwrkf_RespuestaOperacion.Codigox = mensajeerror.Codigox;
                Objwrkf_RespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                Objwrkf_RespuestaOperacion.Tipox = mensajeerror.Tipox;
                Objwrkf_RespuestaOperacion.Titulox = mensajeerror.Titulox;
            }
            else
            {
                try
                {
                    Objwrkf_RespuestaOperacion = objDbSolicitudOrdenPagoCxP.AprobarRechazarPagosCxP(plistapagosaprobados, plistapagosrechazados, plistadopagossoportes, Convert.ToString(Session["sUsuario_Id"]), pobservacion);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    Objwrkf_RespuestaOperacion.Codigox = mensajeerror.Codigox;
                    Objwrkf_RespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                    Objwrkf_RespuestaOperacion.Tipox = mensajeerror.Tipox;
                    Objwrkf_RespuestaOperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoCxPController/ActualizarGrupoRubro");
                }
            }

            return Json(new { RespuestaOperacion = Objwrkf_RespuestaOperacion });
        }

        /// <summary>
        /// Controller Actualiza forma de pago enla revisión de la solicitud del pago por CxP
        /// </summary>
        /// <param name="forma_pago_id"></param>
        /// <param name="Solicitudordenpagodetalle_Id"></param>
        /// <param name="Solicitudordenpago_Id"></param>
        /// <returns></returns>
        //public JsonResult ActualizarFormaPagoRevisionCxP(int forma_pago_id, int Solicitudordenpagodetalle_Id, int Solicitudordenpago_Id)
        //{
        //    Wrkf_RespuestaOperacion objrespuestaoperacion = new Wrkf_RespuestaOperacion();
        //    Wrkf_DbSolicitudOrdenPagoCxP objSolicitudOrdenPagoCxP = new Wrkf_DbSolicitudOrdenPagoCxP();
        //    MensajeError mensajeerror;
        //    Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

        //    if (Session["sUsuario_Id"] == null)
        //    {
        //        mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
        //        objrespuestaoperacion.Codigox = mensajeerror.Codigox;
        //        objrespuestaoperacion.Mensajex = mensajeerror.Mensajex;
        //        objrespuestaoperacion.Tipox = mensajeerror.Tipox;
        //        objrespuestaoperacion.Titulox = mensajeerror.Titulox;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            objrespuestaoperacion = objSolicitudOrdenPagoCxP.ActualizarFormaPagoRevisionCxP(forma_pago_id, Solicitudordenpagodetalle_Id, Solicitudordenpago_Id);
        //        }
        //        catch (Exception ex)
        //        {
        //            mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
        //            objrespuestaoperacion.Codigox = mensajeerror.Codigox;
        //            objrespuestaoperacion.Mensajex = mensajeerror.Mensajex;
        //            objrespuestaoperacion.Tipox = mensajeerror.Tipox;
        //            objrespuestaoperacion.Titulox = mensajeerror.Titulox;
        //            wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoCxPController/ActualizarFormaPagoRevisionCxP");
        //        }
        //    }

        //    return Json(objrespuestaoperacion, JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// Actualizar el número de chequera a la solicitud
        /// </summary>
        /// <param name="Solicitudordenpagodetalle_Id"></param>
        /// <param name="Solicitudordenpago_Id"></param>
        /// <param name="Chekbkid"></param>
        /// <returns></returns>
        public JsonResult ActualizarNumeroChequeraRevisionCxP(int Solicitudordenpagodetalle_Id, int Solicitudordenpago_Id, string Chekbkid)
        {
            Wrkf_RespuestaOperacion objrespuestaoperacion = new Wrkf_RespuestaOperacion();
            Wrkf_DbSolicitudOrdenPagoCxP objSolicitudOrdenPagoCxP = new Wrkf_DbSolicitudOrdenPagoCxP();
            Wrkf_DbChequera wrkf_dbchequera = new Wrkf_DbChequera();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                objrespuestaoperacion.Codigox = mensajeerror.Codigox;
                objrespuestaoperacion.Mensajex = mensajeerror.Mensajex;
                objrespuestaoperacion.Tipox = mensajeerror.Tipox;
                objrespuestaoperacion.Titulox = mensajeerror.Titulox;
            } 
            else
            {
                try
                {
                    //obtiene el codigo de la moneda en GP de la chequera
                    string Currencycodegp = wrkf_dbchequera.GetCurrencyCodeCheckbook(Chekbkid);
                    objrespuestaoperacion = objSolicitudOrdenPagoCxP.ActualizarNumeroChequeraRevisionCxP(Solicitudordenpagodetalle_Id, Solicitudordenpago_Id, Chekbkid, Currencycodegp, Convert.ToString(Session["sUsuario_Id"]));
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    objrespuestaoperacion.Codigox = mensajeerror.Codigox;
                    objrespuestaoperacion.Mensajex = mensajeerror.Mensajex;
                    objrespuestaoperacion.Tipox = mensajeerror.Tipox;
                    objrespuestaoperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoCxPController/ActualizarNumeroChequeraRevisionCxP");
                }
            }

            return Json(objrespuestaoperacion, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Obtiene las observaciones de la solicitud de pago seleccionada
        /// </summary>
        /// <param name="Solicitudordenpagodetalle_Id"></param>
        /// <param name="Solicitudordenpago_Id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ObtieneObservacionesSolicitud(int Solicitudordenpagodetalle_Id, int Solicitudordenpago_Id)
        {
            Wrkf_RespuestaOperacion objrespuestaoperacion = new Wrkf_RespuestaOperacion();
            Wrkf_DbSolicitudOrdenPagoCxP objSolicitudOrdenPagoCxP = new Wrkf_DbSolicitudOrdenPagoCxP();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                objrespuestaoperacion.Codigox = mensajeerror.Codigox;
                objrespuestaoperacion.Mensajex = mensajeerror.Mensajex;
                objrespuestaoperacion.Tipox = mensajeerror.Tipox;
                objrespuestaoperacion.Titulox = mensajeerror.Titulox;
            }
            else
            {
                try
                {
                    objrespuestaoperacion = objSolicitudOrdenPagoCxP.ObtenerObservacionesSolicitudPago(Solicitudordenpagodetalle_Id, Solicitudordenpago_Id);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    objrespuestaoperacion.Codigox = mensajeerror.Codigox;
                    objrespuestaoperacion.Mensajex = mensajeerror.Mensajex;
                    objrespuestaoperacion.Tipox = mensajeerror.Tipox;
                    objrespuestaoperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoCxPController/ObtieneObservacionesSolicitud");
                }
            }

            return Json(objrespuestaoperacion, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Muestra un detalle completo del pago para la revisión de CxP
        /// </summary>
        /// <param name="Solicitudordenpagodetalle_Id"></param>
        /// <param name="Solicitudordenpago_Id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DetalleCompletoSolicitudPagoCxP(int Solicitudordenpagodetalle_Id, int Solicitudordenpago_Id)
        {
            List<Wrkf_SolicitudOrdenPagoEncabDetal> lstencabezadodetalleid = new List<Wrkf_SolicitudOrdenPagoEncabDetal>();
            Wrkf_DbSolicitudOrdenPagoCxP objdbsolicitudordenpagocxp = new Wrkf_DbSolicitudOrdenPagoCxP();
            Wrkf_SolicitudOrdenPagoEncabDetal wrkf_solicitudordenpagoencabdetal = new Wrkf_SolicitudOrdenPagoEncabDetal();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                wrkf_solicitudordenpagoencabdetal.Codigox = mensajeerror.Codigox;
                wrkf_solicitudordenpagoencabdetal.Mensajex = mensajeerror.Mensajex;
                wrkf_solicitudordenpagoencabdetal.Tipox = mensajeerror.Tipox;
                wrkf_solicitudordenpagoencabdetal.Titulox = mensajeerror.Titulox;

                lstencabezadodetalleid.Add(wrkf_solicitudordenpagoencabdetal);
            }
            else
            {
                try
                {
                    lstencabezadodetalleid = objdbsolicitudordenpagocxp.ObtenerSolicitudOrdenPagoEncabDetallePorId(Solicitudordenpagodetalle_Id, Solicitudordenpago_Id);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    wrkf_solicitudordenpagoencabdetal.Codigox = mensajeerror.Codigox;
                    wrkf_solicitudordenpagoencabdetal.Mensajex = mensajeerror.Mensajex;
                    wrkf_solicitudordenpagoencabdetal.Tipox = mensajeerror.Tipox;
                    wrkf_solicitudordenpagoencabdetal.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoCxPController/DetalleCompletoSolicitudPagoCxP");

                    lstencabezadodetalleid.Add(wrkf_solicitudordenpagoencabdetal);
                }
            }

            return Json(lstencabezadodetalleid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Confirma la recepción de los soportes del pago
        /// </summary>
        /// <param name="Solicitudordenpagodetalle_Id"></param>
        /// <param name="DocumentoRecibido"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ConfirmarRecepcionSoporteCxP(int Solicitudordenpagodetalle_Id, bool DocumentoRecibido)
        {
            Wrkf_RespuestaOperacion objrespuestaoperacion = new Wrkf_RespuestaOperacion();
            Wrkf_DbSolicitudOrdenPagoCxP objSolicitudOrdenPagoCxP = new Wrkf_DbSolicitudOrdenPagoCxP();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                objrespuestaoperacion.Codigox = mensajeerror.Codigox;
                objrespuestaoperacion.Mensajex = mensajeerror.Mensajex;
                objrespuestaoperacion.Tipox = mensajeerror.Tipox;
                objrespuestaoperacion.Titulox = mensajeerror.Titulox;
            }
            else
            {
                try
                {
                    objrespuestaoperacion = objSolicitudOrdenPagoCxP.ConfirmarRecepcionSoporteCxP(Solicitudordenpagodetalle_Id, DocumentoRecibido);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    objrespuestaoperacion.Codigox = mensajeerror.Codigox;
                    objrespuestaoperacion.Mensajex = mensajeerror.Mensajex;
                    objrespuestaoperacion.Tipox = mensajeerror.Tipox;
                    objrespuestaoperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoCxPController/ConfirmarRecepcionSoporteCxP");
                }
            }

            return Json(objrespuestaoperacion, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Aprueba el pago que esta asociado a la solicitud
        /// </summary>
        /// <param name="Solicitudordenpagodetalle_Id"></param>
        /// <param name="Aprobado"></param>
        /// <param name="UsuarioAprobacionCxP"></param>
        /// <returns></returns>
        public JsonResult AprobacionPorCxP(string listapagosaprobados, string listapagosrechazados)
        {
            Wrkf_RespuestaOperacion objrespuestaoperacion = new Wrkf_RespuestaOperacion();
            Wrkf_DbSolicitudOrdenPagoCxP objSolicitudOrdenPagoCxP = new Wrkf_DbSolicitudOrdenPagoCxP();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                objrespuestaoperacion.Codigox = mensajeerror.Codigox;
                objrespuestaoperacion.Mensajex = mensajeerror.Mensajex;
                objrespuestaoperacion.Tipox = mensajeerror.Tipox;
                objrespuestaoperacion.Titulox = mensajeerror.Titulox;
            }
            else
            {
                try
                {
                    objrespuestaoperacion = objSolicitudOrdenPagoCxP.AprobacionPorCxP(listapagosaprobados, listapagosrechazados, Session["sUsuario_Id"].ToString());
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    objrespuestaoperacion.Codigox = mensajeerror.Codigox;
                    objrespuestaoperacion.Mensajex = mensajeerror.Mensajex;
                    objrespuestaoperacion.Tipox = mensajeerror.Tipox;
                    objrespuestaoperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoCxPController/AprobacionPorCxP");
                }
            }

            return Json(objrespuestaoperacion, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Obtiene el motivo del rechazo de la solicitud por subcontraloria
        /// </summary>
        /// <param name="Solicitudordenpagodetalle_Id"></param>
        /// <returns></returns>
        public JsonResult ObtenerMotivoRechazoSubContraloria(int Solicitudordenpagodetalle_Id)
        {
            List<Wrkf_RespuestaOperacion> lstrespuestaoperacion = new List<Wrkf_RespuestaOperacion>();
            Wrkf_DbSolicitudOrdenPagoCxP objSolicitudOrdenPagoCxP = new Wrkf_DbSolicitudOrdenPagoCxP();
            Wrkf_RespuestaOperacion objwrkfrespuestaoperacion = new Wrkf_RespuestaOperacion();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                objwrkfrespuestaoperacion.Codigox = mensajeerror.Codigox;
                objwrkfrespuestaoperacion.Mensajex = mensajeerror.Mensajex;
                objwrkfrespuestaoperacion.Tipox = mensajeerror.Tipox;
                objwrkfrespuestaoperacion.Titulox = mensajeerror.Titulox;

                lstrespuestaoperacion.Add(objwrkfrespuestaoperacion);
            }
            else
            {
                try
                {
                    lstrespuestaoperacion = objSolicitudOrdenPagoCxP.ObtenerMotivoRechazoSubContraloria(Solicitudordenpagodetalle_Id);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    objwrkfrespuestaoperacion.Codigox = mensajeerror.Codigox;
                    objwrkfrespuestaoperacion.Mensajex = mensajeerror.Mensajex;
                    objwrkfrespuestaoperacion.Tipox = mensajeerror.Tipox;
                    objwrkfrespuestaoperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoCxPController/ObtenerMotivoRechazoSubContraloria");

                    lstrespuestaoperacion.Add(objwrkfrespuestaoperacion);
                }
            }

            return Json(lstrespuestaoperacion, JsonRequestBehavior.AllowGet);
        }
    }
}