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
    public class Wrk_SolicitudOrdenPagoSoporteController : Controller
    {
        /// <summary>
        /// Lee el directorio que contiene los soportes asociados al pago 
        /// </summary>
        /// <param name="pCodigo"></param>
        /// <returns></returns>
        public ActionResult DescargarSoporte(string pCodigo)
        {
            /*buscamos los datos del pago*/
            Wrkf_DbSolicitudOrdenPagoCxP Objwrkf_DbSolicitudOrdenPagoCxP = new Wrkf_DbSolicitudOrdenPagoCxP();
            Wrkf_SolicitudOrdenPago Objwrkf_SolicitudOrdenPago;
            Objwrkf_SolicitudOrdenPago = Objwrkf_DbSolicitudOrdenPagoCxP.GetDetalleOrdenPagoPorRevisarCxP(pCodigo);

            Wrkf_DbParametros wrkf_dbparametros = new Wrkf_DbParametros();
            Wrkf_Parametros wrkf_parametros;
            ConvertExtension ObjConvertExtension = new ConvertExtension();

            //obtener la ruta donde se guardan los soportes digitales del pago
            wrkf_parametros = wrkf_dbparametros.SeleccionarParametroCodigo("RUTSOP", Session["sUsuario_Id"].ToString().Trim().ToUpper());
            string ruta = wrkf_parametros.ValorAlfaNumerico1.Trim();
            string vPlantilla = Objwrkf_SolicitudOrdenPago.GAPCodigoPlantilla;
            string vCodigoItemPago = Objwrkf_SolicitudOrdenPago.GAPCodigoItem;
            string vFechaPagoItem = ObjConvertExtension.FormatoFechayyyyMMdd(Convert.ToDateTime(Objwrkf_SolicitudOrdenPago.GAPFechaPago)).ToString();
            string vRuta2 = vPlantilla + "\\" + vFechaPagoItem + "\\" + vCodigoItemPago;

            var pathCarpeta = System.IO.Path.Combine(ruta, vRuta2);

            if (System.IO.Directory.Exists(pathCarpeta))
            {
                var listaArchivos = System.IO.Directory.GetFiles(pathCarpeta);

                ViewBag.listaArchivos = listaArchivos;
                ViewBag.Ruta = pathCarpeta;
            }
            else
            {
                ViewBag.listaArchivos = new string[0];
                ViewBag.Ruta = pathCarpeta;
            }

            return View();
        }

        /// <summary>
        /// Descarga el archivo de soporte
        /// </summary>
        /// <param name="ruta"></param>
        /// <param name="archivo"></param>
        /// <returns></returns>
        public ActionResult RevisarSoporte(string ruta, string archivo)
        {
            Wrkf_DbMensajeError wrkf_dbmensajeerror = new Wrkf_DbMensajeError();

            string rutadescarga = ruta + archivo;

            if (Session["sUsuario_Id"] == null)
            {
                return RedirectToAction("CerrarSesion", "Wrkf_Login");
            }
            else
            {
                try
                {
                    string contentType = MimeMapping.GetMimeMapping(rutadescarga);
                    var cd = new System.Net.Mime.ContentDisposition
                    {
                        FileName = archivo,
                        Inline = true,
                    };
                    Response.AppendHeader("Content-Disposition", cd.ToString());

                    return File(rutadescarga, archivo, contentType);
                }
                catch (Exception ex)
                { 
                    wrkf_dbmensajeerror.RegistrarLogErrores(ex.HResult, ex.Message.ToString(), Convert.ToString(Session["sUsuario_Id"]), "Wrk_SolicitudOrdenPagoSoporteController/RevisarSoporte");
                }
            }

            return File(rutadescarga, archivo);
        }
    }
}