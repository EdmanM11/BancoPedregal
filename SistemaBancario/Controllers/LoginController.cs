using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Web.Security;
using System.Net.Http;
using SistemaBancario.Models;

namespace SistemaBancario.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autenticar(string txtusuario, string txtpassword)
        {
            IEnumerable<spIniciarSesion> clase = null;
            using (var httpcliente = new HttpClient())
            {

                httpcliente.BaseAddress = new Uri("https://localhost:44311/api/");
                //HTTP GET
                var responseTask = httpcliente.GetAsync($"SpIniciarSesion?Usuario={txtusuario}");
                responseTask.Wait();
                var result = responseTask.Result;

                var leerlista = result.Content.ReadAsAsync<IList<spIniciarSesion>>();

                leerlista.Wait();
                clase = leerlista.Result;
                ViewBag.valor = clase;

                string user = "";
                string contra = "";
                string nombre = "";
                string apellido = "";
                string rol = "";
                string estado = "";

                foreach (var lst in ViewBag.valor)
                {
                     user = lst.USUARIO;
                     contra = lst.PASS;
                     nombre = lst.NOMBRES;
                     apellido = lst.APELLIDOS;
                     rol = lst.ROL;
                     estado = lst.ESTADO_USER;
                }

                if (user == "")
                {
                    TempData["inicio"] = "50";
                    return RedirectToAction("../Login/Login");
                }

                if (estado == "INACTIVO")
                {
                    TempData["inicio"] = "1";
                    return RedirectToAction("../Login/Login");
                }


                if (txtusuario == user & Crypto.VerifyHashedPassword(contra, txtpassword))
                {
                    string token = TokenRequest(txtusuario).Replace("\"", "");
                    Session["Rol"] = rol;
                    Session["Nombre"] = nombre +" "+ apellido;
                    Session["Usuario"] = txtusuario;
                    Session["Token"] = token;
                    Session["LogOn"] = true;
                    FormsAuthentication.SetAuthCookie(txtusuario, false);

                    return RedirectToAction("../Home");
                }
                else
                {
                    TempData["inicio"] = "200";
                    return RedirectToAction("../Login/Login");
                }
            }
        }

        [HttpPost]
        public string TokenRequest(string Usuario)
        {
            var HttpClient = new HttpClient();
            //HttpClient.BaseAddress = new Uri("https://localhost:44311/");
            //Ejecutar la url;
            /*string url = "https://localhost:44354/api/login/Authenticated?Usuario=" + Usuario;
            var Json = await HttpClient.GetAsync(url);
            //Obtener el Json
            var Contenido = Json.Content.ReadAsStringAsync().Result;*/
            HttpClient.BaseAddress = new Uri("https://localhost:44311/api/login/");
            //HTTP GET
            var responseTask = HttpClient.GetAsync($"authenticate?Usuario={Usuario}");
            responseTask.Wait();

            var result = responseTask.Result.Content.ReadAsStringAsync();
            return result.Result;
        }


        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();  
            
            if (Session["LogOn"] != null)
            {
                TempData["cerrado"] = "1";
            } 
            return RedirectToAction("../Login/Login");
        }


    }
}