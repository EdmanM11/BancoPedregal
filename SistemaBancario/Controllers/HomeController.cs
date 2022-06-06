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
    public class HomeController : Controller
    {

        public async Task<ActionResult> BusquedaCuentas()
        {
            if (Session["LogOn"] != null)
            {

            var HTTPClient = new HttpClient();

              //Configurar el token en la cabecera de la peticion
              var token = Session["Token"];
              HTTPClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
              //ejecutar la url
              var Consulta = await HTTPClient.GetAsync("https://localhost:44311/api/SpClientes/");
              //obtener el json
              var Contenido = Consulta.Content.ReadAsStringAsync().Result;
              //convertir la json a lista y asignarlo al objeto definido globalmente
              var lst = JsonConvert.DeserializeObject<List<spListarClientes>>(Contenido).ToList();

            return View(lst);
            }
            else
            {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
            }
        }


        [Route("Home/Encontrar/{txtcliente}")]
        public ActionResult EncontrarCliente()
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
            int txtcliente = Convert.ToInt32(Param.Split('=').Last());
            ViewBag.Cuenta = txtcliente;


            var numero = Convert.ToString(txtcliente);
            IEnumerable<spListarClientesCta> clase = null;
            using (var httpcliente = new HttpClient())
            {

                //Configurar el token en la cabecera de la peticion
                var token = Session["Token"];
                httpcliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

                httpcliente.BaseAddress = new Uri("https://localhost:44311/api/SpBuscarCuentas/");
                var respuestatarea = httpcliente.GetAsync(numero);
                respuestatarea.Wait();

                var result = respuestatarea.Result;
                if (result.IsSuccessStatusCode)
                {
                    var leerlista = result.Content.ReadAsAsync<IList<spListarClientesCta>>();
                    leerlista.Wait();
                    clase = leerlista.Result;
                    ViewBag.valor = clase;
                }
            }

            IEnumerable<spBuscarClienteCod> clase2 = null;
            using (var httpcliente2 = new HttpClient())
            {
                //Configurar el token en la cabecera de la peticion
                var token = Session["Token"];
                httpcliente2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

                httpcliente2.BaseAddress = new Uri("https://localhost:44311/api/SpBuscarClienteCod/");
                var respuestatarea2 = httpcliente2.GetAsync(numero);
                respuestatarea2.Wait();

                var result2 = respuestatarea2.Result;
                if (result2.IsSuccessStatusCode)
                {
                    var leerlista2 = result2.Content.ReadAsAsync<IList<spBuscarClienteCod>>();
                    leerlista2.Wait();
                    clase2 = leerlista2.Result;
                    ViewBag.valor2 = clase2;
                }
            }
            return View("BusquedaSeleccionada");
            }
            else
            {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
            }
        }

   
        public ActionResult Index()
        {
            if (Session["LogOn"] != null)
            {
            return View();
            }
            else
            {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
            }
        }

    
        public ActionResult Transferencias()
        {
            if (Session["LogOn"] != null)
            {
            return View();
            }
            else
            {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
            }
        }

       
     
    }
}