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
    public class Wrkf_SolicitudOrdenPagoVPController : Controller
    {
        /// <summary>
        /// Muestra una vista con el listado de grupo de rubros con pagos pendientes por aprobar por contraloria 
        /// </summary>
        /// <returns></returns>
        public ActionResult ListaGrupoRubroVP()
        {
            //Obtener una lista con las opciones de menu
            List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
            List<Wrkf_Departamento> lstgruporubro = new List<Wrkf_Departamento>();
            List<Wrkf_Departamento> lstgruporubronotacreditos;
            List<Wrkf_Departamento> lstgruporubro_Urgentes;
            Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
            Wrkf_DbMensajeError objwrkfdbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError objmensajeerror;
            Wrkf_DbSolicitudVP objsolicitudVP = new Wrkf_DbSolicitudVP();
            Wrkf_Departamento objdepartamento = new Wrkf_Departamento();

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
                        objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("MNU001", "MENUOPCION");
                        ViewBag.CodigoMnu = objmensajeerror.Codigox;
                        ViewBag.MensajeMnu = objmensajeerror.Mensajex;
                        ViewBag.TipoMnu = objmensajeerror.Tipox;
                        ViewBag.TituloMnu = objmensajeerror.Titulox;
                    }

                    //Obtener los grupo de rubros con los pagos asociados
                    lstgruporubro = objsolicitudVP.LstGrupoRubroVP(false);

                    if (lstgruporubro.Count > 0)
                    {
                        ViewBag.listagruporubrospagos = lstgruporubro;
                    }
                    else
                    {
                        objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("SOP005", "SOLIORPAGO");

                        objdepartamento.Codigox = objmensajeerror.Codigox;
                        objdepartamento.Mensajex = objmensajeerror.Mensajex;
                        objdepartamento.Tipox = objmensajeerror.Tipox;
                        objdepartamento.Titulox = objmensajeerror.Titulox;

                        lstgruporubro.Add(objdepartamento);

                        ViewBag.listagruporubrospagos = lstgruporubro;
                    }

                    //obtener los grupo de rubros con las notas de creditos pendientes
                    lstgruporubronotacreditos = objsolicitudVP.LstGrupoRubroVP(true);

                    if (lstgruporubronotacreditos.Count > 0)
                    {
                        ViewBag.lstgruporubronotacreditos = lstgruporubronotacreditos;
                    }
                    else
                    {
                        objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("SOP006", "SOLIORPAGO");

                        objdepartamento.Codigox = objmensajeerror.Codigox;
                        objdepartamento.Mensajex = objmensajeerror.Mensajex;
                        objdepartamento.Tipox = "card card-info";
                        objdepartamento.Titulox = objmensajeerror.Titulox;

                        lstgruporubronotacreditos.Add(objdepartamento);

                        ViewBag.lstgruporubronotacreditoscontralor = lstgruporubronotacreditos;
                    }

                    //obtener los grupo de rubros con pagos urgentes
                    lstgruporubro_Urgentes = objsolicitudVP.LstGrupoRubroVP_Urgentes();

                    if (lstgruporubro_Urgentes.Count > 0)
                    {
                        ViewBag.lstgruporubrourgentes = lstgruporubro_Urgentes;
                    }
                    else
                    {
                        objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("SOP006", "SOLIORPAGO");

                        objdepartamento.Codigox = objmensajeerror.Codigox;
                        objdepartamento.Mensajex = objmensajeerror.Mensajex;
                        objdepartamento.Tipox = "card card-info";
                        objdepartamento.Titulox = objmensajeerror.Titulox;

                        lstgruporubro_Urgentes.Add(objdepartamento);

                        ViewBag.lstgruporubrourgentes = lstgruporubro_Urgentes;
                    }
                }
                catch (Exception ex)
                {
                    objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    objdepartamento.Codigox = objmensajeerror.Codigox;
                    objdepartamento.Mensajex = objmensajeerror.Mensajex;
                    objdepartamento.Tipox = objmensajeerror.Tipox;
                    objdepartamento.Titulox = objmensajeerror.Titulox;
                    objwrkfdbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoVPController/ListaGrupoRubroVP");

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
        public ActionResult ListaRubroVP(string grid)
        {
            //Obtener una lista con las opciones de menu
            List<Wrkf_OpcionesMenuItem> lstopcionesmenuitem;
            List<Wrkf_Rubro> lstrubro;
            List<Wrkf_Rubro> lstrubronotacreditos = new List<Wrkf_Rubro>();
            List<Wrkf_Rubro> lstrubrourgentes = new List<Wrkf_Rubro>();
            Wrkf_DbOpcionesMenu objdbopcionesmenu = new Wrkf_DbOpcionesMenu();
            Wrkf_DbMensajeError objwrkfdbmensajeerror = new Wrkf_DbMensajeError();
            MensajeError objmensajeerror;
            Wrkf_DbSolicitudVP objsolicitudVP = new Wrkf_DbSolicitudVP();
            Wrkf_Rubro objrubro = new Wrkf_Rubro();

            if (Session["sUsuario_Id"] == null)
            {
                return RedirectToAction("CerrarSesion", "Wrkf_Login");
            }
            else
            {
                try
                {
                    int gruporubro_id = Convert.ToInt32(EncriptadorMD5.Decrypt(grid));
                    lstopcionesmenuitem = objdbopcionesmenu.Fn_ListarOpcionesMenuPorRol(Session["sUsuario_Id"].ToString());
                    ViewBag.grid = grid; //grupo del rubro encriptado

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

                    //Obtener los grupo de rubros con los pagos asociados
                    lstrubro = objsolicitudVP.LstRubroVP(gruporubro_id, false);

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

                    //obtener los grupo de rubros con las notas de creditos pendientes
                    lstrubronotacreditos = objsolicitudVP.LstRubroVP(gruporubro_id, true);

                    if (lstrubronotacreditos.Count > 0)
                    {
                        ViewBag.lstrubronotacreditos = lstrubronotacreditos;
                    }
                    else
                    {
                        objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("SOP006", "SOLIORPAGO");

                        objrubro.Codigox = objmensajeerror.Codigox;
                        objrubro.Mensajex = objmensajeerror.Mensajex;
                        objrubro.Tipox = "card card-info";
                        objrubro.Titulox = objmensajeerror.Titulox;

                        lstrubronotacreditos.Add(objrubro);

                        ViewBag.lstrubronotacreditos = lstrubronotacreditos;
                    }

                    //obtener los rubros con pagos urgentes pendientes por aprobación del VP
                    lstrubrourgentes = objsolicitudVP.LstRubroVP_Urgentes(gruporubro_id);

                    if (lstrubrourgentes.Count > 0)
                    {
                        ViewBag.lstrubrourgentes = lstrubrourgentes;
                    }
                    else
                    {
                        objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("SOP006", "SOLIORPAGO");

                        objrubro.Codigox = objmensajeerror.Codigox;
                        objrubro.Mensajex = objmensajeerror.Mensajex;
                        objrubro.Tipox = "card card-info";
                        objrubro.Titulox = objmensajeerror.Titulox;

                        lstrubrourgentes.Add(objrubro);

                        ViewBag.lstrubrourgentes = lstrubrourgentes;
                    }
                }
                catch (Exception ex)
                {
                    objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    objrubro.Codigox = objmensajeerror.Codigox;
                    objrubro.Mensajex = objmensajeerror.Mensajex;
                    objrubro.Tipox = objmensajeerror.Tipox;
                    objrubro.Titulox = objmensajeerror.Titulox;
                    objwrkfdbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoVPController/ListaRubroVP");

                    lstrubronotacreditos.Add(objrubro);

                    ViewBag.lstrubronotacreditos = lstrubronotacreditos;
                }
            }

            return View();
        }

        /// <summary>
        /// Muestra un listado delos pagospendientes de aprobación por contaloria
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="rid"></param>
        /// <param name="fch1"></param>
        /// <param name="fch2"></param>
        /// <param name="tpdo"></param>
        /// <returns></returns>
        public ActionResult ListaPagosRubrosIdVP(string grid, string rid, string fch1, string fch2, string tpdo)
        {
            List<Wrkf_ListaPagosPorRubroId> lstpagosporrubro = new List<Wrkf_ListaPagosPorRubroId>();
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

                    fechadesde = fch1;
                    fechahasta = fch2;

                    ViewBag.gruporubro_id = grid;
                    ViewBag.rubro_id = rid;
                    ViewBag.fechadesde = fechadesde;
                    ViewBag.fechahasta = fechahasta;
                    ViewBag.tipodoc = tpdo;

                    //Obtener los pagos asociados al rubro seleccionado
                    //Obtener los pagos asociados al rubro seleccionado
                    Wrkf_DbSolicitudVP wrkf_DbSolicitudVP = new Wrkf_DbSolicitudVP();

                    if (tipodocumento == "NC")
                    {
                        //verificar el tipo de documento
                        lstpagosporrubro = wrkf_DbSolicitudVP.ListaPagosPorRubroIdVP(gruporubro_id, rubro_id, Convert.ToDateTime(fechadesde), Convert.ToDateTime(fechahasta), true);
                    }
                    else
                    {
                        //verificar el tipo de documento
                        lstpagosporrubro = wrkf_DbSolicitudVP.ListaPagosPorRubroIdVP(gruporubro_id, rubro_id, Convert.ToDateTime(fechadesde), Convert.ToDateTime(fechahasta), false);
                    }

                    ViewBag.listarpagos = lstpagosporrubro;
                }
                catch (Exception ex)
                {
                    objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    wrkf_listapagosporrubroid.Codigox = objmensajeerror.Codigox;
                    wrkf_listapagosporrubroid.Mensajex = objmensajeerror.Mensajex;
                    wrkf_listapagosporrubroid.Tipox = objmensajeerror.Tipox;
                    wrkf_listapagosporrubroid.Titulox = objmensajeerror.Titulox;
                    objwrkfdbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoVPController/ListaPagosRubrosIdVP");

                    lstpagosporrubro.Add(wrkf_listapagosporrubroid);

                    ViewBag.listarpagos = lstpagosporrubro;
                }
            }

            return View();
        }

        /// <summary>
        /// Lista los pagos urgentes pendientes por la aprobación de VP
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="rid"></param>
        /// <param name="fch1"></param>
        /// <param name="fch2"></param>
        /// <returns></returns>
        public ActionResult ListaPagosRubrosIdVP_Urgentes(string grid, string rid, string fch1, string fch2)
        {
            List<Wrkf_ListaPagosPorRubroId> lstpagosporrubro = new List<Wrkf_ListaPagosPorRubroId>();
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

                    fechadesde = fch1;
                    fechahasta = fch2;

                    ViewBag.gruporubro_id = grid;
                    ViewBag.rubro_id = rid;
                    ViewBag.fechadesde = fechadesde;
                    ViewBag.fechahasta = fechahasta;

                    //Obtener los pagos asociados al rubro seleccionado
                    Wrkf_DbSolicitudVP wrkf_DbSolicitudVP = new Wrkf_DbSolicitudVP();

                    lstpagosporrubro = wrkf_DbSolicitudVP.ListaPagosPorRubroIdVP_Urgentes(gruporubro_id, rubro_id, Convert.ToDateTime(fechadesde), Convert.ToDateTime(fechahasta));

                    ViewBag.listarpagos = lstpagosporrubro;
                }
                catch (Exception ex)
                {
                    objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    wrkf_listapagosporrubroid.Codigox = objmensajeerror.Codigox;
                    wrkf_listapagosporrubroid.Mensajex = objmensajeerror.Mensajex;
                    wrkf_listapagosporrubroid.Tipox = objmensajeerror.Tipox;
                    wrkf_listapagosporrubroid.Titulox = objmensajeerror.Titulox;
                    objwrkfdbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoVPController/ListaPagosRubrosIdVP_Urgentes");

                    lstpagosporrubro.Add(wrkf_listapagosporrubroid);

                    ViewBag.listarpagos = lstpagosporrubro;
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
                    Wrkf_DbSolicitudVP wrkf_DbSolicitudVP = new Wrkf_DbSolicitudVP();

                    if (tipodocumento != "NC")
                    {
                        esnotacredito = false;
                    }
                    else
                    {
                        esnotacredito = true;
                    }

                    lstpagosporrubro = wrkf_DbSolicitudVP.ListaPagosPorRubroIdVP(Convert.ToInt32(gruporubro), rubro_id, Convert.ToDateTime(fechadesde), Convert.ToDateTime(fechahasta), esnotacredito);

                    ViewBag.listarpagos = lstpagosporrubro;
                }
                catch (Exception ex)
                {
                    objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    wrkf_listapagosporrubroid.Codigox = objmensajeerror.Codigox;
                    wrkf_listapagosporrubroid.Mensajex = objmensajeerror.Mensajex;
                    wrkf_listapagosporrubroid.Tipox = objmensajeerror.Tipox;
                    wrkf_listapagosporrubroid.Titulox = objmensajeerror.Titulox;
                    objwrkfdbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoVPController/ListaPagosPorRubroPost");

                    lstpagosporrubro.Add(wrkf_listapagosporrubroid);

                    ViewBag.listarpagos = lstpagosporrubro;
                }
            }

            return View("ListaPagosRubrosIdVP");
        }

        /// <summary>
        /// Listarlos pagos urgentes pendientes por aprobacion de VP
        /// </summary>
        /// <returns></returns>
        public ActionResult ListaPagosPorRubroPost_Urgente()
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
                    Wrkf_DbSolicitudVP wrkf_DbSolicitudVP = new Wrkf_DbSolicitudVP();

                    lstpagosporrubro = wrkf_DbSolicitudVP.ListaPagosPorRubroIdVP_Urgentes(Convert.ToInt32(gruporubro), rubro_id, Convert.ToDateTime(fechadesde), Convert.ToDateTime(fechahasta));

                    ViewBag.listarpagos = lstpagosporrubro;
                }
                catch (Exception ex)
                {
                    objmensajeerror = objwrkfdbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    wrkf_listapagosporrubroid.Codigox = objmensajeerror.Codigox;
                    wrkf_listapagosporrubroid.Mensajex = objmensajeerror.Mensajex;
                    wrkf_listapagosporrubroid.Tipox = objmensajeerror.Tipox;
                    wrkf_listapagosporrubroid.Titulox = objmensajeerror.Titulox;
                    objwrkfdbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoVPController/ListaPagosPorRubroPost_Urgente");

                    lstpagosporrubro.Add(wrkf_listapagosporrubroid);

                    ViewBag.listarpagos = lstpagosporrubro;
                }
            }

            return View("ListaPagosRubrosIdVP_Urgentes");
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
                    Wrkf_DbSolicitudVP wrkf_DbSolicitudVP = new Wrkf_DbSolicitudVP();
                    wrkf_RespuestaOperacion = wrkf_DbSolicitudVP.RegistrarMotivoRechazo(Solicitudordenpagodetalle_Id, Solicitudordenpago_Id, motivorechazo, Session["sUsuario_Id"].ToString());
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
        public JsonResult AprobarRechazarSolicitudVP(string listapagosaprobados, string listapagosrechazados)
        {
            Wrkf_DbSolicitudVP wrkf_DbSolicitudVP = new Wrkf_DbSolicitudVP();
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
                    wrkf_RespuestaOperacion = wrkf_DbSolicitudVP.AprobarRechazarSolicitudVP(listapagosaprobados, listapagosrechazados, Session["sUsuario_Id"].ToString());
                }
                catch (Exception ex)
                {
                    mensajeerror = wrkf_dbmensajeerror.GetObtenerMensajeError("99999", "Exception");
                    wrkf_RespuestaOperacion.Codigox = mensajeerror.Codigox;
                    wrkf_RespuestaOperacion.Mensajex = mensajeerror.Mensajex;
                    wrkf_RespuestaOperacion.Tipox = mensajeerror.Tipox;
                    wrkf_RespuestaOperacion.Titulox = mensajeerror.Titulox;
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrkf_SolicitudOrdenPagoContralorController/AprobarRechazarSolicitudVP");
                }
            }

            return Json(wrkf_RespuestaOperacion, JsonRequestBehavior.AllowGet);
        }
    }
}