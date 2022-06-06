using Newtonsoft.Json;
using SistemaBancario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net.Http.Headers;

namespace SistemaBancario.Controllers
{
    [Authorize]
    public class TransaccionesController : Controller
    {
 
        public ActionResult Transacciones()
        {

            if (Session["LogOn"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("../Login/Logout");
            }

        }


        public async Task<ActionResult> Depositos()
        {

            if (Session["LogOn"] != null)
            {
            // instancia de http cliente
            var HTTPClient = new HttpClient();

            //Configurar el token en la cabecera de la peticion
            var token = Session["Token"];
            HTTPClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

            //ejecutar la url
            var Consulta = await HTTPClient.GetAsync("https://localhost:44311/api/SpListaDeposito/");
            //obtener el json

            var Contenido = Consulta.Content.ReadAsStringAsync().Result;
            //convertir la json a lista y asignarlo al objeto definido globalmente
            var lst = JsonConvert.DeserializeObject<List<spListaCuentasBank>>(Contenido).ToList();

            return View(lst);
            }
            else
            {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
            }
        }

        public async Task<ActionResult> Retiros()
        {
            if (Session["LogOn"] != null)
            {
          
            // instancia de http cliente
            var HTTPClient = new HttpClient();

            //Configurar el token en la cabecera de la peticion
            var token = Session["Token"];
            HTTPClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

            //ejecutar la url
            var Consulta = await HTTPClient.GetAsync("https://localhost:44311/api/SpListaDeposito/");
            //obtener el json

            var Contenido = Consulta.Content.ReadAsStringAsync().Result;
            //convertir la json a lista y asignarlo al objeto definido globalmente
            var lst = JsonConvert.DeserializeObject<List<spListaCuentasBank>>(Contenido).ToList();

            return View(lst);
                
            }
            else
            {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
            }
        }

        [HttpPost]
        public ActionResult nuevoDeposito(decimal txtmontoadep, int txtcodcuentadep)
        {
            if (Session["LogOn"] != null)
            {
            try
            {
                var deposito = new TBL_HISTORIAL_TRANSACCIONES();


                deposito.COD_CUENTA = txtcodcuentadep;
                deposito.FECHA = DateTime.Now.Date;
                deposito.MONTO = Convert.ToDecimal(txtmontoadep);
                deposito.TIPO_TRANSACCION = "DEPOSITO";
                var User = Convert.ToString(Session["Usuario"]);
                deposito.USUARIO_TRA = User;

                using (var HttpClient = new HttpClient())
                {
                    //Configurar el token en la cabecera de la peticion
                    var token = Session["Token"];
                    HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

                    HttpClient.BaseAddress = new Uri("https://localhost:44311/api/TBL_HISTORIAL_TRANSACCIONES/");

                    //Hacer post
                    var Post = HttpClient.PostAsJsonAsync<TBL_HISTORIAL_TRANSACCIONES>("TBL_HISTORIAL_TRANSACCIONES", deposito);

                    Post.Wait();
                    if (Post.Result.IsSuccessStatusCode)
                    {
                        TempData["deposito"] = "1";
                    }
                    else
                    {
                        TempData["deposito"] = "2";
                    }
                    return RedirectToAction("Depositos");
                }
            }
            catch (Exception e)
            { return Content(e.Message); }
            }
            else
            {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
            }
        }

        [HttpPost]
        public ActionResult nuevoRetiro(decimal txtmontoret, int txtcodcuentaret)
        {
            if (Session["LogOn"] != null)
            {
            try
            {
                var deposito = new TBL_HISTORIAL_TRANSACCIONES();


                deposito.COD_CUENTA = txtcodcuentaret;
                deposito.FECHA = DateTime.Now.Date;
                deposito.MONTO = Convert.ToDecimal(txtmontoret);
                deposito.TIPO_TRANSACCION = "RETIRO";
                var User = Convert.ToString(Session["Usuario"]);
                deposito.USUARIO_TRA = User;

                    using (var HttpClient = new HttpClient())
                {
                    //Configurar el token en la cabecera de la peticion
                    var token = Session["Token"];
                    HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

                    HttpClient.BaseAddress = new Uri("https://localhost:44311/api/TBL_HISTORIAL_TRANSACCIONES/");

                    //Hacer post
                    var Post = HttpClient.PostAsJsonAsync<TBL_HISTORIAL_TRANSACCIONES>("TBL_HISTORIAL_TRANSACCIONES", deposito);

                    Post.Wait();
                    if (Post.Result.IsSuccessStatusCode)
                    {
                        TempData["retiro"] = "1";
                    }
                    else
                    {
                        TempData["retiro"] = "2";
                    }
                    return RedirectToAction("Retiros");
                }
            }
            catch (Exception e)
            { return Content(e.Message); }
            }
            else
            {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
            }
        }

        public async Task<ActionResult> HistorialTransacciones()
        {
            if (Session["LogOn"] != null)
            {
            // instancia de http cliente
            var HTTPClient = new HttpClient();

            //Configurar el token en la cabecera de la peticion
            var token = Session["Token"];
            HTTPClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

            //ejecutar la url
            var Consulta = await HTTPClient.GetAsync("https://localhost:44311/api/SpListaHistorialAll/");
            //obtener el json

            var Contenido = Consulta.Content.ReadAsStringAsync().Result;
            //convertir la json a lista y asignarlo al objeto definido globalmente
            var lst = JsonConvert.DeserializeObject<List<spListaHistorialAll>>(Contenido).ToList();
          
            return View(lst);  
            }
            else
            {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
            }
        }

        [Route("Transacciones/Encontrar/{txtcodigo}")]
        public ActionResult Encontrar()
        {
            if (Session["LogOn"] != null)
            {
          
            //Obtener la url del sitio
            string url = Request.Url.ToString();
            //Instanciar la clase para codificar
            var Code = new CodigoURLController();
            //Decodificar el parametro desde que aparece el simbolo ?
            string Param = Code.Decode32(url.Split('?').Last());
            //Obtener el codigo de cliente desde el =
            int txtcodigo = Convert.ToInt32(Param.Split('=').Last());
            ViewBag.Cuenta = txtcodigo;


            var numero = Convert.ToString(txtcodigo);
            IEnumerable<spListarhistorial> clase = null;
            using (var httpcliente = new HttpClient())
            {
                //Configurar el token en la cabecera de la peticion
                var token = Session["Token"];
                httpcliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

                httpcliente.BaseAddress = new Uri("https://localhost:44311/api/SpHistorialTransaccion/");
                var respuestatarea = httpcliente.GetAsync(numero);
                respuestatarea.Wait();

                var result = respuestatarea.Result;
                if (result.IsSuccessStatusCode)
                {
                    var leerlista = result.Content.ReadAsAsync<IList<spListarhistorial>>();
                    leerlista.Wait();
                    clase = leerlista.Result;
                    ViewBag.valor = clase;
                }
            }


            IEnumerable<spListaCuentaCod> clase2 = null;
            using (var httpcliente2 = new HttpClient())
            {
                //Configurar el token en la cabecera de la peticion
                var token = Session["Token"];
                httpcliente2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

                httpcliente2.BaseAddress = new Uri("https://localhost:44311/api/SpListaCuentaCod/");            
                var respuestatarea2 = httpcliente2.GetAsync(numero);
                respuestatarea2.Wait();

                var result2 = respuestatarea2.Result;
                if (result2.IsSuccessStatusCode)
                {
                    var leerlista2 = result2.Content.ReadAsAsync<IList<spListaCuentaCod>>();
                    leerlista2.Wait();
                    clase2 = leerlista2.Result;
                    ViewBag.valor2 = clase2;
                }
            }
            return View("HistorialSeleccionado");
            }
            else
            {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
            }
        } 
    }
}
