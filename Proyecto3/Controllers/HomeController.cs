using Proyecto3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;


namespace Proyecto3.Controllers
{
    public class HomeController : Controller
    {
       pruebaDataContext prueba = new pruebaDataContext();

        public ActionResult Index()
        {
            ViewBag.Message = "Todos los usuarios";
            var users = (from us in prueba.PRUEBAS_TA_01_CAT_USUARIOS select us).ToList();
            //@ViewBag.Empresas = (from corp in prueba.PRUEBAS_TA_02_CAT_CORPORACION select corp).ToList();
            return View(users);
        }

        public ActionResult Create(){
        return View();
        }

        [HttpPost]
        public ActionResult Create(PRUEBAS_TA_01_CAT_USUARIOS user) {
            user.T_DIRECCION = getMd5Hash(user.T_DIRECCION);
            prueba.PRUEBAS_TA_01_CAT_USUARIOS.InsertOnSubmit(user);
            prueba.SubmitChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id) {
            var user = (from us in prueba.PRUEBAS_TA_01_CAT_USUARIOS where us.E_ID == id select us).SingleOrDefault();
            return View(user);
        }

        public ActionResult edit(int id) {
            var user = (from us in prueba.PRUEBAS_TA_01_CAT_USUARIOS where us.E_ID == id select us).SingleOrDefault();
            return View(user);
        }

        [HttpPost]

        public ActionResult edit(PRUEBAS_TA_01_CAT_USUARIOS user) {
            var edit_user = (from us in prueba.PRUEBAS_TA_01_CAT_USUARIOS where us.E_ID == user.E_ID select us).SingleOrDefault();
            edit_user.E_EDAD = user.E_EDAD;
            edit_user.REL_TA02_E_ID_CORPORACION = user.REL_TA02_E_ID_CORPORACION;
            edit_user.T_APELLIDOS = user.T_APELLIDOS;
            edit_user.T_COLONIA = user.T_COLONIA;
            edit_user.T_DIRECCION = user.T_DIRECCION;
            edit_user.T_EMAIL = user.T_EMAIL;
            edit_user.T_NOMBRE = user.T_NOMBRE;
            edit_user.T_USUARIO = user.T_USUARIO;
            prueba.SubmitChanges();
            return View(edit_user);
        }

        public ActionResult Delete(int id) {
            var user = (from us in prueba.PRUEBAS_TA_01_CAT_USUARIOS where us.E_ID == id select us).SingleOrDefault();
            return View(user);
        }

        [HttpPost]
        public ActionResult Delete(PRUEBAS_TA_01_CAT_USUARIOS user)
        {
            var UserDelete = (from us in prueba.PRUEBAS_TA_01_CAT_USUARIOS where us.E_ID == user.E_ID select us).SingleOrDefault();
            prueba.PRUEBAS_TA_01_CAT_USUARIOS.DeleteOnSubmit(UserDelete);
            prueba.SubmitChanges();
            var users = (from us in prueba.PRUEBAS_TA_01_CAT_USUARIOS select us).ToList();
            return RedirectToAction("Index");

        }

        public ActionResult About()
        {
            ViewBag.Message = "Página de descripción de la aplicación.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Página de contacto.";

            return View();
        }

        public ActionResult CrearUsuarioAjax()
        {
            ViewBag.Message = "Página crear usuarios";

            return View();
        }
        [HttpPost]
        public JsonResult CrearUsuarioAjax(PRUEBAS_TA_01_CAT_USUARIOS usuarios)
        {
            try{
                usuarios.T_PASSWORD = getMd5Hash(usuarios.T_PASSWORD);
                prueba.PRUEBAS_TA_01_CAT_USUARIOS.InsertOnSubmit(usuarios);
                prueba.SubmitChanges();
               
            }
            catch {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            return Json(usuarios, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AllUser() {
            var users = (from us in prueba.PRUEBAS_TA_01_CAT_USUARIOS select us).ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarUsuariosAjax()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ListarUsuariosAjax2()
        {
            try
            {
                var users = (from us in prueba.PRUEBAS_TA_01_CAT_USUARIOS select us).ToList();
                return Json(users, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
           
        }
        public ActionResult DeleteAll() {
            return View();
        }
        [HttpPost]
        public String DeleteAll2() {
            var users = (from us in prueba.PRUEBAS_TA_01_CAT_USUARIOS select us).ToList();
            for (var i=1; i<=users.Count; i++){
                var a = prueba.PRUEBAS_TA_01_CAT_USUARIOS.First();
                delete2(a);
            }
            return "Deleted All";
        }
        [HttpPost]
        public void delete2(PRUEBAS_TA_01_CAT_USUARIOS user) { 
             prueba.PRUEBAS_TA_01_CAT_USUARIOS.DeleteOnSubmit(user);
             prueba.SubmitChanges();
        }

       
        public JsonResult searchuser(int id) {
            try {
                var user = (from us in prueba.PRUEBAS_TA_01_CAT_USUARIOS where us.E_ID == id select us).SingleOrDefault();
                return Json(user, JsonRequestBehavior.AllowGet);
            }

            catch {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditarUsuarioAjax(int id) {
            
            ViewBag.Idato = id;
             return View();
        }

        [HttpPost]
        public int EditarUsuarioAjax(PRUEBAS_TA_01_CAT_USUARIOS user) {
              try{
                    var edit_user = (from us in prueba.PRUEBAS_TA_01_CAT_USUARIOS where us.E_ID == user.E_ID select us).SingleOrDefault();
                    edit_user.E_EDAD = user.E_EDAD;
                    edit_user.REL_TA02_E_ID_CORPORACION = user.REL_TA02_E_ID_CORPORACION;
                    edit_user.T_APELLIDOS = user.T_APELLIDOS;
                    edit_user.T_COLONIA = user.T_COLONIA;
                    edit_user.T_DIRECCION = user.T_DIRECCION;
                    edit_user.T_EMAIL = user.T_EMAIL;
                    edit_user.T_NOMBRE = user.T_NOMBRE;
                    edit_user.T_USUARIO = user.T_USUARIO;
                    prueba.SubmitChanges();
                    return 1;
              }
            catch {
                return 0;
            }
            
        
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(String username, String password, String returnUrl)
        {
            var user = (from us in prueba.PRUEBAS_TA_01_CAT_USUARIOS where us.T_USUARIO == username select us).SingleOrDefault();
            if (user != null) {
                 var pass = getMd5Hash(password);
                if (pass == user.T_PASSWORD){
                    FormsAuthentication.SetAuthCookie(username, false);
                    Session["id"] = user.E_ID;
                    //Session["user_name"] = user.T_USUARIO;
                    Session["user"] = user.T_NOMBRE;
                }
            }
                        
            return RedirectToLocal(returnUrl);

        }

        public ActionResult Logout() {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        

    static string getMd5Hash(string input)
      { // Create a new instance of the MD5CryptoServiceProvider object.
         MD5 md5Hasher = MD5.Create(); // Convert the input string to a byte array and compute the hash.
         byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
        // Create a new Stringbuilder to collect the bytes // and create a string.
         StringBuilder sBuilder = new StringBuilder(); // Loop through each byte of the hashed data // and format each one as a hexadecimal string.
         for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
       // Return the hexadecimal string.
        return sBuilder.ToString();
      }

    public ActionResult Calendar() {
        ViewBag.PRUEBAS_TA_01_E_ID_USUARIOS = new SelectList(prueba.PRUEBAS_TA_01_CAT_USUARIOS, "E_ID", "T_NOMBRE");

        return PartialView();
    }

    public JsonResult GetEvents(){
      try
      {
          var tareas = (from tar in prueba.PRUEBAS_TA_04_CAT_TAREAS select tar).ToList();
          var JsonTareas = from e in tareas
                           select new
                           {
                               id = e.E_ID,
                               title = e.T_NOMBRE,
                               start = e.F_START.Value.ToString("yyyy-MM-dd'T'HH:mm:ssZ"),
                               end = e.F_END.Value.ToString("yyyy-MM-dd'T'HH:mm:ssZ"),
                               color = color(e.E_PRIORIDAD),
                               descripcion = e.T_DESCRIPCION
                           };
          var rows = JsonTareas.ToArray();
          return Json(rows, JsonRequestBehavior.AllowGet);
      }
      catch
      {
          return Json(null, JsonRequestBehavior.AllowGet);
      }
      
    }

    public String color(int i)
    {
        if (i == 1)
        {
            return "#d15b47";
        }
        else
        {
            if (i == 2)
            {
                return "#3a87ad";
            }
            else
            {
                return "#82af6f";
            }
        }

    }

    private ActionResult RedirectToLocal(string returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }


    }
}
