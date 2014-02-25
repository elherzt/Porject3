using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto3.Models;

namespace Proyecto3.Controllers
{
    public class BitacoraController : Controller
    {
        pruebaDataContext prueba = new pruebaDataContext();
       

        public ActionResult Index()
        {
            var logs = (from log in prueba.PRUEBAS_TA_05_BITACORA select log).ToList();
            return View(logs);
        }

    }
}
