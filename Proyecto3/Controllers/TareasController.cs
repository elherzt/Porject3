using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto3.Models;
using Proyecto3.Filters;
using System.Globalization;

namespace Proyecto3.Controllers
{
     [BitacoraActionFilter]
    public class TareasController : Controller
    {

        pruebaDataContext prueba = new pruebaDataContext();
        private IFormatProvider provider;
        //
        // GET: /Tareas/

        public ActionResult Index()
        {
            var tareas = (from tar in prueba.PRUEBAS_TA_04_CAT_TAREAS select tar).ToList();
            return View(tareas);
        }

        public ActionResult create() {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PRUEBAS_TA_04_CAT_TAREAS tarea) {
            prueba.PRUEBAS_TA_04_CAT_TAREAS.InsertOnSubmit(tarea);
            prueba.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id) {
            var tarea = (from tar in prueba.PRUEBAS_TA_04_CAT_TAREAS where tar.E_ID == id select tar).SingleOrDefault();
            return View(tarea);
        }

        [HttpPost]
        public ActionResult edit(PRUEBAS_TA_04_CAT_TAREAS tarea)
        {
            var edit_tarea = (from tar in prueba.PRUEBAS_TA_04_CAT_TAREAS where tar.E_ID == tarea.E_ID select tar).SingleOrDefault();
            edit_tarea.E_PRIORIDAD = tarea.E_PRIORIDAD;
            edit_tarea.F_START = tarea.F_START;
            edit_tarea.F_END = tarea.F_END;
            edit_tarea.T_DESCRIPCION = tarea.T_DESCRIPCION;
            edit_tarea.T_NOMBRE = tarea.T_NOMBRE;
            prueba.SubmitChanges();
            return RedirectToAction("Index");
        }


        public ActionResult DeleteAll()
        {
            return View();
        }
        [HttpPost]
        public String DeleteAll2()
        {
            var users = (from us in prueba.PRUEBAS_TA_04_CAT_TAREAS select us).ToList();
            for (var i = 1; i <= users.Count; i++)
            {
                var a = prueba.PRUEBAS_TA_04_CAT_TAREAS.First();
                delete2(a);
            }
            return "Deleted All";
        }

        [HttpPost]
        public void delete2(PRUEBAS_TA_04_CAT_TAREAS user)
        {
            prueba.PRUEBAS_TA_04_CAT_TAREAS.DeleteOnSubmit(user);
            prueba.SubmitChanges();
        }

        [HttpPost]
        public int EditarTareaAjax(int id, String start, String end)
        {
            try
            {
                var edit_tarea = (from t in prueba.PRUEBAS_TA_04_CAT_TAREAS where t.E_ID == id select t).SingleOrDefault();
                DateTime date_start = DateTime.Parse(start, null, DateTimeStyles.RoundtripKind).ToLocalTime();
                DateTime date_end;
                if (end == null)
                {
                    date_end = date_start;
                }
                else
                {
                    date_end = DateTime.Parse(end, null, DateTimeStyles.RoundtripKind).ToLocalTime();
                }

                edit_tarea.F_START = date_start;
                edit_tarea.F_END = date_end;
                prueba.SubmitChanges();
                return 1;
            }
            catch
            {
                return 0;
            }


        }

        public ActionResult Chart() {
            return View();
        }

        public JsonResult GetUsersChart()
        {
            try
            {
                var users = (from us in prueba.View_Tarea_User select us).ToList();

                var JsonTareas = from e in users
                                 select new
                                 {
                                     name = e.T_NOMBRE,
                                     y = e.Cantidad
                                 };
                var rows = JsonTareas.ToArray();
                return Json(rows, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult FilterChart(String inicio, String fin) {
            try
            {
                DateTime date_start = DateTime.Parse(inicio, null, DateTimeStyles.RoundtripKind).ToLocalTime();
                DateTime date_end = DateTime.Parse(fin, null, DateTimeStyles.RoundtripKind).ToLocalTime();
                var users = (from us in prueba.PRUEBAS_TA_04_CAT_TAREAS where us.F_START >= date_start && us.F_END <= date_end select us);

                var query = (from usuarios in prueba.PRUEBAS_TA_01_CAT_USUARIOS
                             join tareas in users on new { PRUEBAS_TA_01_E_ID_USUARIOS = usuarios.E_ID } equals new { PRUEBAS_TA_01_E_ID_USUARIOS = tareas.PRUEBAS_TA_01_E_ID_USUARIOS }
                             group new { usuarios, tareas } by new
                             {
                                 tareas.PRUEBAS_TA_01_E_ID_USUARIOS,
                                 usuarios.T_NOMBRE,
                                 usuarios.E_ID,
                                 Column1 = tareas.PRUEBAS_TA_01_E_ID_USUARIOS
                             } into g
                             select new
                             {
                                 g.Key.T_NOMBRE,
                                 E_ID = (int?)g.Key.E_ID,
                                 PRUEBAS_TA_01_E_ID_USUARIOS = (int?)g.Key.Column1,
                                 Cantidad = g.Count(p => p.tareas.PRUEBAS_TA_01_E_ID_USUARIOS != null)
                             }).ToList();

                var JsonTareas = from e in query
                                 select new
                                 {
                                     name = e.T_NOMBRE,
                                     y = e.Cantidad
                                 };
                var rows = JsonTareas.ToArray();
                return Json(rows, JsonRequestBehavior.AllowGet);
            }
            catch {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
         }
        
        [HttpPost]
        public JsonResult CrearTareaAjax(PRUEBAS_TA_04_CAT_TAREAS tarea)
        {
            var start = tarea.F_START.ToString();
            var end = tarea.F_END.ToString();
            DateTime date_start = DateTime.Parse(start, null, DateTimeStyles.RoundtripKind).ToLocalTime();
            DateTime date_end;
            if (end == null)
            {
                date_end = date_start;
            }
            else
            {
                date_end = DateTime.Parse(end, null, DateTimeStyles.RoundtripKind).ToLocalTime();
            }
            try
            {
                tarea.F_START = date_start;
                tarea.F_END = date_end;
                
                prueba.PRUEBAS_TA_04_CAT_TAREAS.InsertOnSubmit(tarea);
                prueba.SubmitChanges();
                var id = GetId(tarea);
                return id;
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
           
        }


        public JsonResult GetId(PRUEBAS_TA_04_CAT_TAREAS newtarea) {
            try
            {
                var tarea = (from t in prueba.PRUEBAS_TA_04_CAT_TAREAS
                             where t.T_NOMBRE == newtarea.T_NOMBRE && t.T_DESCRIPCION == newtarea.T_DESCRIPCION &&
                                 t.F_START == newtarea.F_START && t.F_END == newtarea.F_END && t.E_PRIORIDAD == newtarea.E_PRIORIDAD && 
                                 t.PRUEBAS_TA_01_E_ID_USUARIOS == newtarea.PRUEBAS_TA_01_E_ID_USUARIOS
                             select t).SingleOrDefault();

                var jsontareas = new {
                               id = tarea.E_ID,
                               title = tarea.T_NOMBRE,
                               start = tarea.F_START.Value.ToString("yyyy-MM-dd'T'HH:mm:ssZ"),
                               end = tarea.F_END.Value.ToString("yyyy-MM-dd'T'HH:mm:ssZ"),
                               color = color(tarea.E_PRIORIDAD),
                               descripcion = tarea.T_DESCRIPCION
                           };

                return Json(jsontareas, JsonRequestBehavior.AllowGet);
            }
            catch {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        public String color(int i)
        {
            if (i == 1)
            {
                return "red";
            }
            else
            {
                if (i == 2)
                {
                    return "blue";
                }
                else
                {
                    return "green";
                }
            }

        }



    }
}
