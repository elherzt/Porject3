using Proyecto3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto3.Controllers
{
    public class CorporacionesController : Controller
    {

        pruebaDataContext prueba = new pruebaDataContext();
        //
        // GET: /Corporaciones/

        public ActionResult Index()
        {
            var corporaciones = (from corp in prueba.PRUEBAS_TA_02_CAT_CORPORACION select corp).ToList();
            return View(corporaciones);
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PRUEBAS_TA_02_CAT_CORPORACION corp)
        {
            prueba.PRUEBAS_TA_02_CAT_CORPORACION.InsertOnSubmit(corp);
            prueba.SubmitChanges();
            return View();
        }
       

    }
}
