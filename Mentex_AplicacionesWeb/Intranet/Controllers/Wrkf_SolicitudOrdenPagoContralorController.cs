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
    public class Wrkf_SolicitudOrdenPagoContralorController : Controller
    {
        /// <summary>
        /// Muestra una vista con el listado de grupo de rubros con pagos pendientes por aprobar por contraloria 
        /// </summary>
        /// <returns></returns>
        public ActionResult ListaGrupoRubroContraloria()
        {
            //Obtener una lista con las opciones de menu
            List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
            List<Wrkf_GrupoRubro> lstgruporubro = new List<Wrkf_GrupoRubro>();
            List<Wrkf_GrupoRubro> lstgruporubronotacreditoscontralor;
            List<Wrkf_GrupoRubro> lstgruporubrourgente;
            Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError objmensajeerror;
            Wrkf_DbSolicitudContralor objsolicitudcontralor = new Wrkf_DbSolicitudContralor();
            Wrkf_GrupoRubro objdepartamento = new Wrkf_GrupoRubro();

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
                    //lstgruporubro = objsolicitudcontralor.lstGrupoRubroContralor(false);

                    if (lstgruporubro.Count > 0)
                    {
                        ViewBag.listagruporubrospagos = lstgruporubro;
                    }
                    else
                    {
                        objmensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("SOP005", "SOLIORPAGO");

                        objdepartamento.Codigox = objmensajeerror.Codigox;
                        objdepartamento.Mensajex = objmensajeerror.Mensajex;
                        objdepartamento.Tipox = objmensajeerror.Tipox;
                        objdepartamento.Titulox = objmensajeerror.Titulox;

                        lstgruporubro.Add(objdepartamento);

                        ViewBag.listagruporubrospagos = lstgruporubro;
                    }

                    ////obtener los grupo de rubros con las notas de creditos pendientes
                    //lstgruporubronotacreditoscontralor = objsolicitudcontralor.lstGrupoRubroContralor(true);

                    //if (lstgruporubronotacreditoscontralor.Count > 0)
                    //{
                    //    ViewBag.lstgruporubronotacreditoscontralor = lstgruporubronotacreditoscontralor;
                    //}
                    //else
                    //{
                    //    objmensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("SOP006", "SOLIORPAGO");

                    //    objdepartamento.Codigox = objmensajeerror.Codigox;
                    //    objdepartamento.Mensajex = objmensajeerror.Mensajex;
                    //    objdepartamento.Tipox = "card card-info";
                    //    objdepartamento.Titulox = objmensajeerror.Titulox;

                    //    lstgruporubronotacreditoscontralor.Add(objdepartamento);

                    //    ViewBag.lstgruporubronotacreditoscontralor = lstgruporubronotacreditoscontralor;
                    //}

                    ////obtener los grupos de rubros con pagos urgentes
                    //lstgruporubrourgente = objsolicitudcontralor.lstGrupoRubroUrgentesContralor();

                    //if (lstgruporubrourgente.Count > 0)
                    //{
                    //    ViewBag.lstgruporubrourgentes = lstgruporubrourgente;
                    //}
                    //else
                    //{
                    //    objmensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("SOP006", "SOLIORPAGO");

                    //    objdepartamento.Codigox = objmensajeerror.Codigox;
                    //    objdepartamento.Mensajex = objmensajeerror.Mensajex;
                    //    objdepartamento.Tipox = "card card-danger";
                    //    objdepartamento.Titulox = objmensajeerror.Titulox;

                    //    lstgruporubrourgente.Add(objdepartamento);

                    //    ViewBag.lstgruporubrourgentes = lstgruporubrourgente;
                    //}
                }
                catch (Exception ex)
                {
                    objmensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    objdepartamento.Codigox = objmensajeerror.Codigox;
                    objdepartamento.Mensajex = objmensajeerror.Mensajex;
                    objdepartamento.Tipox = objmensajeerror.Tipox;
                    objdepartamento.Titulox = objmensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoContralorController/ListaGrupoRubroContraloria");

                    lstgruporubro.Add(objdepartamento);

                    ViewBag.listagruporubrospagos = lstgruporubro;

                }
            }

            return View();
        }

        /// <summary>
        /// Muestra una vista con el listado de los rubros con pagos pendientes por aprobar por contraloria
        /// </summary>
        /// <returns></returns>
        //public ActionResult ListaRubroContraloria(string grid)
        //{
        //    //Obtener una lista con las opciones de menu
        //    List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
        //    List<Wrkf_Rubro> lstrubrocontralor;
        //    List<Wrkf_Rubro> lstrubronotacreditoscontralor = new List<Wrkf_Rubro>();
        //    List<Wrkf_Rubro> lstrubrocontralorurgente;
        //    Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
        //    Wrkf_DbMensajeError objwrkfdbmensajeerror = new Wrkf_DbMensajeError();
        //    MensajeError objmensajeerror;
        //    Wrkf_DbSolicitudContralor objsolicitudcontralor = new Wrkf_DbSolicitudContralor();
        //    Wrkf_Rubro objrubro = new Wrkf_Rubro();

        //    if (Session["sUsuario_Id"] == null)
        //    {
        //        return RedirectToAction("CerrarSesion", "Wrkf_Login");
        //    }
        //    else
        //    {
        //        try
        //        {
        //            int gruporubro_id = Convert.ToInt32(EncriptadorMD5.Decrypt(grid));
        //            lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());
        //            ViewBag.grid = grid; //grupo del rubro encriptado

        //            //verificar si la lista tiene las opciones del menu del controlar para mostrar en la vista
        //            if (lstopcionesmenuitem.Count > 0)
        //            {
        //                ViewBag.listaropcionesmenu = lstopcionesmenuitem;
        //            }
        //            else
        //            {
        //                objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("MNU001", "MENUOPCION");
        //                ViewBag.CodigoMnu = objmensajeerror.Codigox;
        //                ViewBag.MensajeMnu = objmensajeerror.Mensajex;
        //                ViewBag.TipoMnu = objmensajeerror.Tipox;
        //                ViewBag.TituloMnu = objmensajeerror.Titulox;
        //            }

        //            //Obtener los grupo de rubros con los pagos asociados
        //            lstrubrocontralor = objsolicitudcontralor.LstRubroContralor(gruporubro_id, false);

        //            if (lstrubrocontralor.Count > 0)
        //            {
        //                ViewBag.listarubrospagos = lstrubrocontralor;
        //            }
        //            else
        //            {
        //                objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("SOP007", "SOLIORPAGO");

        //                objrubro.Codigox = objmensajeerror.Codigox;
        //                objrubro.Mensajex = objmensajeerror.Mensajex;
        //                objrubro.Tipox = objmensajeerror.Tipox;
        //                objrubro.Titulox = objmensajeerror.Titulox;

        //                lstrubrocontralor.Add(objrubro);

        //                ViewBag.listarubrospagos = lstrubrocontralor;
        //            }

        //            //obtener los grupo de rubros con las notas de creditos pendientes
        //            lstrubronotacreditoscontralor = objsolicitudcontralor.LstRubroContralor(gruporubro_id, true);

        //            if (lstrubronotacreditoscontralor.Count > 0)
        //            {
        //                ViewBag.lstrubronotacreditoscontralor = lstrubronotacreditoscontralor;
        //            }
        //            else
        //            {
        //                objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("SOP006", "SOLIORPAGO");

        //                objrubro.Codigox = objmensajeerror.Codigox;
        //                objrubro.Mensajex = objmensajeerror.Mensajex;
        //                objrubro.Tipox = "card card-info";
        //                objrubro.Titulox = objmensajeerror.Titulox;

        //                lstrubronotacreditoscontralor.Add(objrubro);

        //                ViewBag.lstrubronotacreditoscontralor = lstrubronotacreditoscontralor;
        //            }

        //            //obtener los rubros marcados como pagos urgentes
        //            lstrubrocontralorurgente = objsolicitudcontralor.LstRubroContralorUrgentes(gruporubro_id);

        //            if (lstrubrocontralorurgente.Count > 0)
        //            {
        //                ViewBag.lstrubrocontralorurgente = lstrubrocontralorurgente;
        //            }
        //            else
        //            {
        //                objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("SOP006", "SOLIORPAGO");

        //                objrubro.Codigox = objmensajeerror.Codigox;
        //                objrubro.Mensajex = objmensajeerror.Mensajex;
        //                objrubro.Tipox = "card card-info";
        //                objrubro.Titulox = objmensajeerror.Titulox;

        //                lstrubrocontralorurgente.Add(objrubro);

        //                ViewBag.lstrubrocontralorurgente = lstrubrocontralorurgente;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("99999", "Exception");
        //            objrubro.Codigox = objmensajeerror.Codigox;
        //            objrubro.Mensajex = objmensajeerror.Mensajex;
        //            objrubro.Tipox = objmensajeerror.Tipox;
        //            objrubro.Titulox = objmensajeerror.Titulox;
        //            objwrkfdbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoContralorController/ListaRubroContraloria");

        //            lstrubronotacreditoscontralor.Add(objrubro);

        //            ViewBag.lstrubronotacreditoscontralor = lstrubronotacreditoscontralor;
        //        }
        //    }

        //    return View();
        //}

        /// <summary>
        /// Muestra un listado delos pagospendientes de aprobación por contaloria
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="rid"></param>
        /// <param name="fch1"></param>
        /// <param name="fch2"></param>
        /// <param name="tpdo"></param>
        /// <returns></returns>
        public ActionResult ListaPagosRubrosIdContralor(string grid, string rid, string fch1, string fch2, string tpdo)
        {
            //Obtener una lista con las opciones de menu
            List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
            Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
            Wrkf_DiaIFSemana wrkfdiaifsemana;
            ConvertExtension objconvertextension = new ConvertExtension();
            List<Wrkf_ListaPagosPorRubroId> lstpagosporrubro = new List<Wrkf_ListaPagosPorRubroId>();
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
                    //desencriptar los datos para listar los pagos pendientes por aprobar del contralor
                    int gruporubro_id = Convert.ToInt32(EncriptadorMD5.Decrypt(grid));
                    string rubro_id = Convert.ToString(EncriptadorMD5.Decrypt(rid));
                    string tipodocumento = Convert.ToString(EncriptadorMD5.Decrypt(tpdo));
                    string fechadesde;
                    string fechahasta;

                    lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());
                    ViewBag.listaropcionesmenu = lstopcionesmenuitem;

                    string format = "yyyy-MM-dd";
                    var now = DateTime.Now.ToString(format);

                    //verificar los rangos de fecha
                    if ((fch1 == null || fch1 == "") && (fch2 == null || fch2 == ""))
                    {
                        wrkfdiaifsemana = objconvertextension.ObtenerPrimerDiaSemana(Convert.ToDateTime(now));
                        fch1 = Convert.ToString(wrkfdiaifsemana.Primerdiasemana);
                        fch2 = Convert.ToString(wrkfdiaifsemana.Ultimodiasemana);
                    }

                    fechadesde = Convert.ToString(fch1);
                    fechahasta = Convert.ToString(fch2);

                    ViewBag.gruporubro_id = grid;
                    ViewBag.rubro_id = rid;
                    ViewBag.fechadesde = fechadesde;
                    ViewBag.fechahasta = fechahasta;
                    ViewBag.tipodocumento = tpdo;

                    //Obtener los pagos asociados al rubro seleccionado
                    Wrkf_DbSolicitudContralor objsolicitudcontralor = new Wrkf_DbSolicitudContralor();

                    if (tipodocumento == "NC")
                    {
                        //verificar el tipo de documento
                        lstpagosporrubro = objsolicitudcontralor.ListaPagosPorRubroIdContraloria(gruporubro_id, rubro_id, Convert.ToDateTime(fechadesde), Convert.ToDateTime(fechahasta), true);
                    }
                    else
                    {
                        //verificar el tipo de documento
                        lstpagosporrubro = objsolicitudcontralor.ListaPagosPorRubroIdContraloria(gruporubro_id, rubro_id, Convert.ToDateTime(fechadesde), Convert.ToDateTime(fechahasta), false);
                    }

                    ViewBag.listarpagoscontraloria = lstpagosporrubro;
                }
                catch (Exception ex)
                {
                    objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    wrkf_listapagosporrubroid.Codigox = objmensajeerror.Codigox;
                    wrkf_listapagosporrubroid.Mensajex = objmensajeerror.Mensajex;
                    wrkf_listapagosporrubroid.Tipox = objmensajeerror.Tipox;
                    wrkf_listapagosporrubroid.Titulox = objmensajeerror.Titulox;
                    objwrkfdbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoContralorController/ListaPagosRubrosIdContralor");

                    lstpagosporrubro.Add(wrkf_listapagosporrubroid);

                    ViewBag.listarpagoscontraloria = lstpagosporrubro;
                }
            }

            return View();
        }

        /// <summary>
        /// Lista el detalle de los pagos urgentes pendientes por aprobación de contraloria.
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="rid"></param>
        /// <param name="fch1"></param>
        /// <param name="fch2"></param>
        /// <returns></returns>
        public ActionResult ListaPagosRubrosIdContralor_Urgentes(string grid, string rid, string fch1, string fch2)
        {
            //Obtener una lista con las opciones de menu
            List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
            Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
            Wrkf_DiaIFSemana wrkfdiaifsemana;
            ConvertExtension objconvertextension = new ConvertExtension();
            List<Wrkf_ListaPagosPorRubroId> lstpagosporrubro = new List<Wrkf_ListaPagosPorRubroId>();
            Wrkf_DbMensajeError objwrkfdbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError objmensajeerror;
            Wrkf_ListaPagosPorRubroId wrkf_listapagosporrubroid = new Wrkf_ListaPagosPorRubroId();
            Wrkf_DbSolicitudContralor objsolicitudcontralor = new Wrkf_DbSolicitudContralor();

            if (Session["sUsuario_Id"] == null)
            {
                return RedirectToAction("CerrarSesion", "Wrkf_Login");
            }
            else
            {
                try
                {
                    //desencriptar los datos para listar los pagos pendientes por aprobar del contralor
                    int gruporubro_id = Convert.ToInt32(EncriptadorMD5.Decrypt(grid));
                    string rubro_id = Convert.ToString(EncriptadorMD5.Decrypt(rid));
                    string fechadesde;
                    string fechahasta;

                    lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());
                    ViewBag.listaropcionesmenu = lstopcionesmenuitem;

                    string format = "yyyy-MM-dd";
                    var now = DateTime.Now.ToString(format);

                    //verificar los rangos de fecha
                    if ((fch1 == null || fch1 == "") && (fch2 == null || fch2 == ""))
                    {
                        wrkfdiaifsemana = objconvertextension.ObtenerPrimerDiaSemana(Convert.ToDateTime(now));
                        fch1 = Convert.ToString(wrkfdiaifsemana.Primerdiasemana);
                        fch2 = Convert.ToString(wrkfdiaifsemana.Ultimodiasemana);
                    }

                    fechadesde = Convert.ToString(fch1);
                    fechahasta = Convert.ToString(fch2);

                    ViewBag.gruporubro_id = grid;
                    ViewBag.rubro_id = rid;
                    ViewBag.fechadesde = fechadesde;
                    ViewBag.fechahasta = fechahasta;

                    //verificar el tipo de documento
                    lstpagosporrubro = objsolicitudcontralor.ListaPagosPorRubroIdContraloria_Urgentes(gruporubro_id, rubro_id, Convert.ToDateTime(fechadesde), Convert.ToDateTime(fechahasta));

                    ViewBag.listarpagoscontraloria = lstpagosporrubro;
                }
                catch (Exception ex)
                {
                    objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    wrkf_listapagosporrubroid.Codigox = objmensajeerror.Codigox;
                    wrkf_listapagosporrubroid.Mensajex = objmensajeerror.Mensajex;
                    wrkf_listapagosporrubroid.Tipox = objmensajeerror.Tipox;
                    wrkf_listapagosporrubroid.Titulox = objmensajeerror.Titulox;
                    objwrkfdbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoContralorController/ListaPagosRubrosIdContralor");

                    lstpagosporrubro.Add(wrkf_listapagosporrubroid);

                    ViewBag.listarpagoscontraloria = lstpagosporrubro;
                }
            }

            return View();
        }

        /// <summary>
        /// Muestra una vista con el listado de la solicitud de pago post verifica si el tipo de documento es una nota de credito
        /// </summary>
        /// <returns></returns>
        public ActionResult ListaPagosPorRubroPost()
        {
            //Obtener una lista con las opciones de menu
            List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
            Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
            Wrkf_DiaIFSemana wrkfdiaifsemana;
            ConvertExtension objconvertextension = new ConvertExtension();
            List<Wrkf_ListaPagosPorRubroId> lstpagosporrubro;
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
                    string gruporubro;
                    string rubro_id;
                    string fechadesde;
                    string fechahasta;
                    string tipodocumento;
                    bool esnotacredito;

                    lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());
                    ViewBag.listaropcionesmenu = lstopcionesmenuitem;

                    string format = "yyyy-MM-dd";
                    var now = DateTime.Now.ToString(format);

                    gruporubro = EncriptadorMD5.Decrypt(Request.Form["gruporubro_id"].Trim());
                    rubro_id = EncriptadorMD5.Decrypt(Request.Form["rubro_id"].Trim());
                    fechadesde = objconvertextension.FormatoFecha2(Convert.ToDateTime(Request.Form["txtfechadesde"])).Trim();
                    fechahasta = objconvertextension.FormatoFecha2(Convert.ToDateTime(Request.Form["txtfechahasta"])).Trim();
                    tipodocumento = EncriptadorMD5.Decrypt(Request.Form["tipodocumento"].Trim());

                    //verificar los rangos de fecha
                    if ((fechadesde == null || fechadesde == "") && (fechahasta == null || fechahasta == ""))
                    {
                        wrkfdiaifsemana = objconvertextension.ObtenerPrimerDiaSemana(Convert.ToDateTime(now));
                        fechadesde = wrkfdiaifsemana.Primerdiasemana;
                        fechahasta = wrkfdiaifsemana.Ultimodiasemana;
                    }

                    ViewBag.gruporubro_id = EncriptadorMD5.Encrypt(gruporubro);
                    ViewBag.rubro_id = EncriptadorMD5.Encrypt(rubro_id);
                    ViewBag.fechadesde = fechadesde;
                    ViewBag.fechahasta = fechahasta;
                    ViewBag.tipodocu = EncriptadorMD5.Encrypt(tipodocumento);

                    //Obtener los pagos asociados al rubro seleccionado
                    Wrkf_DbSolicitudContralor dbSolicitudContralor = new Wrkf_DbSolicitudContralor();

                    if (tipodocumento != "NC")
                    {
                        esnotacredito = false;
                    }
                    else
                    {
                        esnotacredito = true;
                    }

                    lstpagosporrubro = dbSolicitudContralor.ListaPagosPorRubroIdContraloria(Convert.ToInt32(gruporubro), rubro_id, Convert.ToDateTime(fechadesde), Convert.ToDateTime(fechahasta), esnotacredito);

                    ViewBag.listarpagoscontraloria = lstpagosporrubro;
                }
                catch (Exception ex)
                {
                    objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    wrkf_listapagosporrubroid.Codigox = objmensajeerror.Codigox;
                    wrkf_listapagosporrubroid.Mensajex = objmensajeerror.Mensajex;
                    wrkf_listapagosporrubroid.Tipox = objmensajeerror.Tipox;
                    wrkf_listapagosporrubroid.Titulox = objmensajeerror.Titulox;
                    objwrkfdbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoContralorController/ListaPagosPorRubroPost");
                }
            }

            return View("ListaPagosRubrosIdContralor");
        }

        /// <summary>
        /// Lista el detalle de los pagos urgentes pendientes por aprobación de contraloria.
        /// </summary>
        /// <returns></returns>
        public ActionResult ListaPagosPorRubroPost_Urgentes()
        {
            //Obtener una lista con las opciones de menu
            List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
            Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
            Wrkf_DiaIFSemana wrkfdiaifsemana;
            ConvertExtension objconvertextension = new ConvertExtension();
            List<Wrkf_ListaPagosPorRubroId> lstpagosporrubro;
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
                    string gruporubro;
                    string rubro_id;
                    string fechadesde;
                    string fechahasta;

                    lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());
                    ViewBag.listaropcionesmenu = lstopcionesmenuitem;

                    string format = "yyyy-MM-dd";
                    var now = DateTime.Now.ToString(format);

                    gruporubro = EncriptadorMD5.Decrypt(Request.Form["gruporubro_id"].Trim());
                    rubro_id = EncriptadorMD5.Decrypt(Request.Form["rubro_id"].Trim());
                    fechadesde = objconvertextension.FormatoFecha2(Convert.ToDateTime(Request.Form["txtfechadesde"])).Trim();
                    fechahasta = objconvertextension.FormatoFecha2(Convert.ToDateTime(Request.Form["txtfechahasta"])).Trim();

                    //verificar los rangos de fecha
                    if ((fechadesde == null || fechadesde == "") && (fechahasta == null || fechahasta == ""))
                    {
                        wrkfdiaifsemana = objconvertextension.ObtenerPrimerDiaSemana(Convert.ToDateTime(now));
                        fechadesde = wrkfdiaifsemana.Primerdiasemana;
                        fechahasta = wrkfdiaifsemana.Ultimodiasemana;
                    }

                    ViewBag.gruporubro_id = EncriptadorMD5.Encrypt(gruporubro);
                    ViewBag.rubro_id = EncriptadorMD5.Encrypt(rubro_id);
                    ViewBag.fechadesde = fechadesde;
                    ViewBag.fechahasta = fechahasta;

                    //Obtener los pagos asociados al rubro seleccionado
                    Wrkf_DbSolicitudContralor dbSolicitudContralor = new Wrkf_DbSolicitudContralor();

                    lstpagosporrubro = dbSolicitudContralor.ListaPagosPorRubroIdContraloria_Urgentes(Convert.ToInt32(gruporubro), rubro_id, Convert.ToDateTime(fechadesde), Convert.ToDateTime(fechahasta));

                    ViewBag.listarpagoscontraloria = lstpagosporrubro;
                }
                catch (Exception ex)
                {
                    objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    wrkf_listapagosporrubroid.Codigox = objmensajeerror.Codigox;
                    wrkf_listapagosporrubroid.Mensajex = objmensajeerror.Mensajex;
                    wrkf_listapagosporrubroid.Tipox = objmensajeerror.Tipox;
                    wrkf_listapagosporrubroid.Titulox = objmensajeerror.Titulox;
                    objwrkfdbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoContralorController/ListaPagosPorRubroPost");
                }
            }

            return View("ListaPagosRubrosIdContralor_Urgentes");
        }

        /// <summary>
        /// Registra el motivo del rechazo del pago por parte del contralor
        /// </summary>
        /// <param name="Solicitudordenpagodetalle_Id"></param>
        /// <param name="Solicitudordenpago_Id"></param>
        /// <param name="motivorechazo"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public JsonResult RegistrarMotivoRechazo(int Solicitudordenpagodetalle_Id, int Solicitudordenpago_Id, string motivorechazo)
        {
            Wrkf_RespuestaOperacion wrkf_RespuestaOperacion = new Wrkf_RespuestaOperacion();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                wrkf_RespuestaOperacion.Codigox = mensajeerror.Codigox;
                wrkf_RespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                wrkf_RespuestaOperacion.Tipox = mensajeerror.Tipox;
                wrkf_RespuestaOperacion.Titulox = mensajeerror.Titulox;
            }
            else
            {
                try
                {
                    Wrkf_DbSolicitudContralor wrkf_DbSolicitudContralor = new Wrkf_DbSolicitudContralor();
                    wrkf_RespuestaOperacion = wrkf_DbSolicitudContralor.RegistrarMotivoRechazo(Solicitudordenpagodetalle_Id, Solicitudordenpago_Id, motivorechazo, Session["sUsuario_Id"].ToString());
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    wrkf_RespuestaOperacion.Codigox = mensajeerror.Codigox;
                    wrkf_RespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                    wrkf_RespuestaOperacion.Tipox = mensajeerror.Tipox;
                    wrkf_RespuestaOperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoContralorController/RegistrarMotivoRechazo");
                }
            }

            return Json(wrkf_RespuestaOperacion, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Selecciona el motivo del rechazo del pago
        /// </summary>
        /// <param name="Solicitudordenpagodetalle_Id"></param>
        /// <param name="Solicitudordenpago_Id"></param>
        /// <param name="motivorechazo"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public JsonResult SeleccionarMotivoRechazo(int Solicitudordenpagodetalle_Id, int Solicitudordenpago_Id)
        {
            Wrkf_RespuestaOperacion wrkf_RespuestaOperacion = new Wrkf_RespuestaOperacion();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                wrkf_RespuestaOperacion.Codigox = mensajeerror.Codigox;
                wrkf_RespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                wrkf_RespuestaOperacion.Tipox = mensajeerror.Tipox;
                wrkf_RespuestaOperacion.Titulox = mensajeerror.Titulox;
            }
            else
            {
                try
                {
                    Wrkf_DbSolicitudContralor wrkf_DbSolicitudContralor = new Wrkf_DbSolicitudContralor();
                    wrkf_RespuestaOperacion = wrkf_DbSolicitudContralor.SeleccionarMotivoRechazo(Solicitudordenpagodetalle_Id, Solicitudordenpago_Id);
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    wrkf_RespuestaOperacion.Codigox = mensajeerror.Codigox;
                    wrkf_RespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                    wrkf_RespuestaOperacion.Tipox = mensajeerror.Tipox;
                    wrkf_RespuestaOperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoContralorController/SeleccionarMotivoRechazo");
                }
            }

            return Json(wrkf_RespuestaOperacion, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Aprobar y rechazar las solictudes de pago contraloria
        /// </summary>
        /// <returns></returns>
        public JsonResult AprobarRechazarSolicitudContraloria(string listapagosaprobados, string listapagosrechazados)
        {
            Wrkf_DbSolicitudContralor wrkf_DbSolicitudContralor = new Wrkf_DbSolicitudContralor();
            Wrkf_RespuestaOperacion wrkf_RespuestaOperacion = new Wrkf_RespuestaOperacion();
            MensajeError mensajeerror;
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            if (Session["sUsuario_Id"] == null)
            {
                mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99998", "SessionLogout");
                wrkf_RespuestaOperacion.Codigox = mensajeerror.Codigox;
                wrkf_RespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                wrkf_RespuestaOperacion.Tipox = mensajeerror.Tipox;
                wrkf_RespuestaOperacion.Titulox = mensajeerror.Titulox;
            }
            else
            {
                try
                {
                    //aprobar o rechazar las solicitudes de pago
                    wrkf_RespuestaOperacion = wrkf_DbSolicitudContralor.AprobarRechazarSolicitudContraloria(listapagosaprobados, listapagosrechazados, Session["sUsuario_Id"].ToString());
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    wrkf_RespuestaOperacion.Codigox = mensajeerror.Codigox;
                    wrkf_RespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                    wrkf_RespuestaOperacion.Tipox = mensajeerror.Tipox;
                    wrkf_RespuestaOperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoContralorController/AprobarRechazarSolicitudContraloria");
                }
            }

            return Json(wrkf_RespuestaOperacion, JsonRequestBehavior.AllowGet);
        }
    }
}