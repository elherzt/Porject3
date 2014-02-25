using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto3.Models;

namespace Proyecto3.Filters 
{
    public class BitacoraActionFilter : ActionFilterAttribute, IActionFilter
    {

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
        pruebaDataContext prueba = new pruebaDataContext();
        HttpSessionStateBase session = filterContext.HttpContext.Session;
        var id = Convert.ToInt32(session["id"]);
        PRUEBAS_TA_05_BITACORA log = new PRUEBAS_TA_05_BITACORA()
        {
            T_CONTROLLER = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
            T_METHOD = filterContext.ActionDescriptor.ActionName,
            E_USER_ID = id,
            F_DATE = filterContext.HttpContext.Timestamp,
            T_MESSAGE = "A dummy message"
        };

        prueba.PRUEBAS_TA_05_BITACORA.InsertOnSubmit(log);
        prueba.SubmitChanges();
        this.OnActionExecuting(filterContext);
    }

    }
}