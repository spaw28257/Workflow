using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Intranet.Ado.DbContent;
using Intranet.Models;
using Intranet.Utilities;
using EncryptDecrypt;

namespace Intranet.Controllers
{
    /// <summary>
    /// Controlador para las acciones del usuario contralor
    /// </summary>
    public class Wrkf_SolicitudOrdenPagoVPController : Controller
    {
        /// <summary>
        /// Muestra una vista con el listado de grupo de rubros con pagos pendientes por aprobar por contraloria 
        /// </summary>
        /// <returns></returns>
        public ActionResult ListaGrupoRubroVp()
        {
            //Obtener una lista con las opciones de menu
            List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
            List<Wrkf_GrupoRubro> lstgruporubro = new List<Wrkf_GrupoRubro>();
            Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError objmensajeerror;
            Wrkf_DbSolicitudVP wrkf_DbSolicitudVP = new Wrkf_DbSolicitudVP();
            Wrkf_GrupoRubro wrkf_GrupoRubro = new Wrkf_GrupoRubro();

            if (Session["sUsuario_Id"] == null)
            {
                return RedirectToAction("CerrarSesion", "Wrkf_Login");
            }
            else
            {
                try
                {
                    lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());

                    //verificar si la lista tiene las opciones del menu del controlar para mostrar en la vista
                    if (lstopcionesmenuitem.Count > 0)
                    {
                        ViewBag.listaropcionesmenu = lstopcionesmenuitem;
                    }
                    else
                    {
                        objmensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("MNU001", "MENUOPCION");
                        ViewBag.CodigoMnu = objmensajeerror.Codigox;
                        ViewBag.MensajeMnu = objmensajeerror.Mensajex;
                        ViewBag.TipoMnu = objmensajeerror.Tipox;
                        ViewBag.TituloMnu = objmensajeerror.Titulox;
                    }

                    ////Obtener los grupo de rubros con los pagos asociados
                    lstgruporubro = wrkf_DbSolicitudVP.lstGrupoRubroVP();

                    if (lstgruporubro.Count > 0)
                    {
                        ViewBag.listagruporubrospagos = lstgruporubro;
                    }
                    else
                    {
                        objmensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("SOP005", "SOLIORPAGO");

                        wrkf_GrupoRubro.Codigox = objmensajeerror.Codigox;
                        wrkf_GrupoRubro.Mensajex = objmensajeerror.Mensajex;
                        wrkf_GrupoRubro.Tipox = objmensajeerror.Tipox;
                        wrkf_GrupoRubro.Titulox = objmensajeerror.Titulox;

                        lstgruporubro.Add(wrkf_GrupoRubro);

                        ViewBag.listagruporubrospagos = lstgruporubro;
                    }
                }
                catch (Exception ex)
                {
                    objmensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    wrkf_GrupoRubro.Codigox = objmensajeerror.Codigox;
                    wrkf_GrupoRubro.Mensajex = objmensajeerror.Mensajex;
                    wrkf_GrupoRubro.Tipox = objmensajeerror.Tipox;
                    wrkf_GrupoRubro.Titulox = objmensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoContralorController/ListaGrupoRubroContraloria");

                    lstgruporubro.Add(wrkf_GrupoRubro);

                    ViewBag.listagruporubrospagos = lstgruporubro;
                }
            }

            return View();
        }

        /// <summary>
        /// Muestra una vista con el listado de los rubros con pagos pendientes por aprobar por contraloria
        /// </summary>
        /// <returns></returns>
        public ActionResult ListaRubroVp(string pgrid, int pripg)
        {
            //Obtener una lista con las opciones de menu
            List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
            List<Wrkf_Rubro> lstrubro;
            Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
            Wrkf_DbMensajeError objwrkfdbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError objmensajeerror;
            Wrkf_DbSolicitudVP wrkf_DbSolicitudVP = new Wrkf_DbSolicitudVP();
            Wrkf_Rubro objrubro = new Wrkf_Rubro();

            if (Session["sUsuario_Id"] == null)
            {
                return RedirectToAction("CerrarSesion", "Wrkf_Login");
            }
            else
            {
                try
                {
                    int gruporubro_id = Convert.ToInt32(EncriptadorMD5.Decrypt(pgrid));
                    ViewBag.gruporubroencript = pgrid;
                    lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());

                    //verificar si la lista tiene las opciones del menu del controlar para mostrar en la vista
                    if (lstopcionesmenuitem.Count > 0)
                    {
                        ViewBag.listaropcionesmenu = lstopcionesmenuitem;
                    }
                    else
                    {
                        objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("MNU001", "MENUOPCION");
                        ViewBag.CodigoMnu = objmensajeerror.Codigox;
                        ViewBag.MensajeMnu = objmensajeerror.Mensajex;
                        ViewBag.TipoMnu = objmensajeerror.Tipox;
                        ViewBag.TituloMnu = objmensajeerror.Titulox;
                    }

                    //Obtener los rubros con los pagos asociados
                    lstrubro = wrkf_DbSolicitudVP.LstRubroVP(gruporubro_id, pripg);

                    if (lstrubro.Count > 0)
                    {
                        ViewBag.listarubrospagos = lstrubro;
                    }
                    else
                    {
                        objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("SOP007", "SOLIORPAGO");

                        objrubro.Codigox = objmensajeerror.Codigox;
                        objrubro.Mensajex = objmensajeerror.Mensajex;
                        objrubro.Tipox = objmensajeerror.Tipox;
                        objrubro.Titulox = objmensajeerror.Titulox;

                        lstrubro.Add(objrubro);

                        ViewBag.listarubrospagos = lstrubro;
                    }
                }
                catch (Exception ex)
                {
                    objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    objrubro.Codigox = objmensajeerror.Codigox;
                    objrubro.Mensajex = objmensajeerror.Mensajex;
                    objrubro.Tipox = objmensajeerror.Tipox;
                    objrubro.Titulox = objmensajeerror.Titulox;
                    objwrkfdbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoVPController/ListaRubroVp");
                }
            }

            return View();
        }

        /// <summary>
        /// Muestra la vista con las solicitudes de pago por aprobar por el VP
        /// </summary>
        /// <param name="pgruporubro_id"></param>
        /// <param name="prubro_id"></param>
        /// <param name="pprioridadpago"></param>
        /// <returns></returns>
        public ActionResult ListaPagosRubrosIdVP(string pgruporubro_id, string prubro_id, int pprioridadpago)
        {
            //Obtener una lista con las opciones de menu
            List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
            Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
            Wrkf_DiaIFSemana wrkfdiaifsemana;
            ConvertExtension objconvertextension = new ConvertExtension();
            Wrkf_DbMensajeError objwrkfdbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError objmensajeerror;
            Wrkf_ListaPagosPorRubroId wrkf_listapagosporrubroid = new Wrkf_ListaPagosPorRubroId();

            if (Session["sUsuario_Id"] == null)
            {
                return RedirectToAction("CerrarSesion", "Wrkf_Login");
            }
            else
            {
                try
                {
                    lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());
                    ViewBag.listaropcionesmenu = lstopcionesmenuitem;

                    string format = "yyyy-MM-dd";
                    var now = DateTime.Now.ToString(format);

                    wrkfdiaifsemana = objconvertextension.ObtenerPrimerDiaSemana(Convert.ToDateTime(now));
                    string vFechadesde = wrkfdiaifsemana.Primerdiasemana;
                    string vFechahasta = wrkfdiaifsemana.Ultimodiasemana;

                    ViewBag.gruporubro_id = pgruporubro_id.Trim();
                    ViewBag.rubro_id = prubro_id.Trim();
                    ViewBag.fechadesde = vFechadesde.Trim();
                    ViewBag.fechahasta = vFechahasta.Trim();
                    ViewBag.prioridadpago = pprioridadpago;
                }
                catch (Exception ex)
                {
                    objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    wrkf_listapagosporrubroid.Codigox = objmensajeerror.Codigox;
                    wrkf_listapagosporrubroid.Mensajex = objmensajeerror.Mensajex;
                    wrkf_listapagosporrubroid.Tipox = objmensajeerror.Tipox;
                    wrkf_listapagosporrubroid.Titulox = objmensajeerror.Titulox;
                    objwrkfdbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoContralorController/ListaPagosRubrosIdContralor");
                }
            }

            return View();
        }

        /// <summary>
        /// Listar la solicitudes de pago por aprobar VP
        /// </summary>
        /// <returns></returns>
        public JsonResult ObtenerPagosPorAprobarVP(string pgruporubro_id, string prubro_id, string pfechapagodesde, string pfechapagohasta, int pprioridadpago)
        {
            Wrkf_DbSolicitudVP wrkf_DbSolicitudVP = new Wrkf_DbSolicitudVP();
            Wrkf_SolicitudOrdenPago wrkf_SolicitudOrdenPago = new Wrkf_SolicitudOrdenPago();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            List<Wrkf_SolicitudOrdenPago> listwrkf_SolicitudOrdenPagos = new List<Wrkf_SolicitudOrdenPago>();
            ConvertExtension convertExtension = new ConvertExtension();

            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                wrkf_SolicitudOrdenPago.Codigox = mensajeerror.Codigox;
                wrkf_SolicitudOrdenPago.Mensajex = mensajeerror.Mensajex;
                wrkf_SolicitudOrdenPago.Tipox = mensajeerror.Tipox;
                wrkf_SolicitudOrdenPago.Titulox = mensajeerror.Titulox;
                listwrkf_SolicitudOrdenPagos.Add(wrkf_SolicitudOrdenPago);
            }
            else
            {
                try
                {
                    Int32 vGrupoRubro_Id = Convert.ToInt32(EncriptadorMD5.Decrypt(pgruporubro_id));
                    string vRubro_Id = EncriptadorMD5.Decrypt(prubro_id).ToString();
                    string vFechaPagoDesde = convertExtension.FormatoFechayyyyMMdd(Convert.ToDateTime(pfechapagodesde)).ToString();
                    string vFechaPagoHasta = convertExtension.FormatoFechayyyyMMdd(Convert.ToDateTime(pfechapagohasta)).ToString();

                    //aprobar o rechazar las solicitudes de pago
                    listwrkf_SolicitudOrdenPagos = wrkf_DbSolicitudVP.ListaPagosPorRubroIdVpDb(vGrupoRubro_Id, vRubro_Id, vFechaPagoDesde, vFechaPagoHasta, pprioridadpago);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    wrkf_SolicitudOrdenPago.Codigox = mensajeerror.Codigox;
                    wrkf_SolicitudOrdenPago.Mensajex = mensajeerror.Mensajex;
                    wrkf_SolicitudOrdenPago.Tipox = mensajeerror.Tipox;
                    wrkf_SolicitudOrdenPago.Titulox = mensajeerror.Titulox;

                    listwrkf_SolicitudOrdenPagos.Add(wrkf_SolicitudOrdenPago);

                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoContralor/ObtenerPagosPorAprobarContraloria");
                }
            }

            return Json(listwrkf_SolicitudOrdenPagos, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Aprobar y rechazar las solictudes de pago
        /// </summary>
        /// <returns></returns>
        public JsonResult AprobarRechazarSolicitudVP(string pcodigo, string pobservaciones, string paccion)
        {
            Wrkf_DbSolicitudVP wrkf_DbSolicitudVP = new Wrkf_DbSolicitudVP();
            Wrkf_RespuestaOperacion objrespuestaoperacion = new Wrkf_RespuestaOperacion();
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
                    //aprobar o rechazar las solicitudes de pago
                    objrespuestaoperacion = wrkf_DbSolicitudVP.AprobarRechazarSolicitudVp(pcodigo, Session["sUsuario_Id"].ToString(), pobservaciones, paccion);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    objrespuestaoperacion.Codigox = mensajeerror.Codigox;
                    objrespuestaoperacion.Mensajex = mensajeerror.Mensajex;
                    objrespuestaoperacion.Tipox = mensajeerror.Tipox;
                    objrespuestaoperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoVP/AprobarRechazarSolicitudVP");
                }
            }

            return Json(objrespuestaoperacion, JsonRequestBehavior.AllowGet);
        }
    }
}