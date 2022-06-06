using Newtonsoft.Json;
using SistemaBancario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net.Http.Headers;
using System.Web.Helpers;


namespace SistemaBancario.Controllers
{
    [Authorize]
    public class RegistroController : Controller
    {

        public async Task<ActionResult> RegistroUsuarios()
        {
            var permiso = Convert.ToString(Session["Rol"]);

            if (permiso == "CAJERO")
            {
                TempData["sesionin"] = "400";
                return RedirectToAction("../Login/Logout");
            }

            if (Session["LogOn"] != null)
            { 
            // instancia de http cliente
            var HTTPClient = new HttpClient();

            //Configurar el token en la cabecera de la peticion
            var token = Session["Token"];
            HTTPClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

            //ejecutar la url
            var Consulta = await HTTPClient.GetAsync("https://localhost:44311/api/TBL_ROLES/");
            //obtener el json

            var Contenido = Consulta.Content.ReadAsStringAsync().Result;
            //convertir la json a lista y asignarlo al objeto definido globalmente
            var lst = JsonConvert.DeserializeObject<List<TBL_ROLES>>(Contenido).ToList();

            return View(lst);
            }
            else
            {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
            }
        }

        [HttpPost]
        public ActionResult NuevoUsuario(string txtname, string txtape, string txtid, string txtuser, string txtpass, string txtpassconfirm, int cmbtipoUser)
        {
            var permiso = Convert.ToString(Session["Rol"]);

            if (permiso == "CAJERO")
            {
                TempData["sesionin"] = "400";
                return RedirectToAction("../Login/Logout");
            }

            if (Session["LogOn"] != null)
            {
         
                var Usuario = new TBL_USUARIOS();
                Usuario.NOMBRES = txtname;
                Usuario.APELLIDOS = txtape;
                Usuario.IDENTIDAD = txtid;
                Usuario.USUARIO = txtuser;          
                Usuario.PASS = Crypto.HashPassword(txtpass);
                Usuario.COD_ROL = cmbtipoUser;
                Usuario.ESTADO_USER = "ACTIVO";
                
                if (txtpass.Length < 8 & txtpassconfirm.Length < 8)
                    {
                      TempData["mensaje"] = "3";
                      return RedirectToAction("RegistroUsuarios");
                    }
                
                if (txtpass == txtpassconfirm & cmbtipoUser != 0)
                {
                using (var HttpClient = new HttpClient())
                {
                    //Configurar el token en la cabecera de la peticion
                    var token = Session["Token"];
                    HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

                    HttpClient.BaseAddress = new Uri("https://localhost:44311/api/TBL_USUARIOS/");

                    //Hacer post
                    var Post = HttpClient.PostAsJsonAsync<TBL_USUARIOS>("TBL_USUARIOS", Usuario);

                    Post.Wait();
                    if (Post.Result.IsSuccessStatusCode)
                    {
                        TempData["mensaje"] = "1";
                    }
                    else
                    {
                        TempData["mensaje"] = "2";
                    }
                    return RedirectToAction("RegistroUsuarios");
                            }
                    } 
                    else
                    {
                        TempData["mensaje"] = "3";
                        return RedirectToAction("RegistroUsuarios");
                    }
            }
            else
            {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
            }
        }



        public ActionResult RegistroClientes()
        {
            var permiso = Convert.ToString(Session["Rol"]);

            if (permiso == "CAJERO")
            {
                TempData["sesionin"] = "400";
                return RedirectToAction("../Login/Logout");
            }

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

        public async Task<ActionResult> ListaClientes()
        {
            var permiso = Convert.ToString(Session["Rol"]);

            if (permiso == "CAJERO")
            {
                TempData["sesionin"] = "400";
                return RedirectToAction("../Login/Logout");
            }

            if (Session["LogOn"] != null)
            {
                // instancia de http cliente
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


        [HttpPost]
        public ActionResult NuevoCliente(string txtnombres, string txtapellidos, string txtidentidad, string txtdireccion, DateTime txtfecha)
        {
            var permiso = Convert.ToString(Session["Rol"]);

            if (permiso == "CAJERO")
            {
                TempData["sesionin"] = "400";
                return RedirectToAction("../Login/Logout");
            }

            if (Session["LogOn"] != null)
            {
          
            try
            {
                var Cliente = new TBL_PERSONAS();
                Cliente.NOMBRE = txtnombres;
                Cliente.APELLIDO = txtapellidos;
                Cliente.DIRECCION = txtdireccion;
                Cliente.FECHA_NAC = txtfecha;
                Cliente.NUM_IDENTIDAD = txtidentidad;

                using (var HttpClient = new HttpClient())
                {
                    //Configurar el token en la cabecera de la peticion
                    var token = Session["Token"];
                    HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

                    HttpClient.BaseAddress = new Uri("https://localhost:44311/api/TBL_PERSONAS/");

                    //Hacer post
                    var Post = HttpClient.PostAsJsonAsync<TBL_PERSONAS>("TBL_PERSONAS", Cliente);

                    Post.Wait();
                    if (Post.Result.IsSuccessStatusCode)
                    {
                        TempData["mensaje"] = "1";
                    }
                    else
                    {
                        TempData["mensaje"] = "2";
                    }
                    return RedirectToAction("RegistroClientes");
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
        public ActionResult UpdateClienteDes(int btnDesactivar, int codPersona)
        {
            var permiso = Convert.ToString(Session["Rol"]);

            if (permiso == "CAJERO")
            {
                TempData["sesionin"] = "400";
                return RedirectToAction("../Login/Logout");
            }

            if (Session["LogOn"] != null)
        {
          
            var codigourl = btnDesactivar.ToString();
            var codigoCliente = btnDesactivar;

            var cliente = new TBL_CLIENTES();
            cliente.COD_CLIENTE = btnDesactivar;
            cliente.COD_PERSONA = codPersona;
            cliente.ESTADO = "INACTIVO";

            
            using (var httpcliente = new HttpClient())
            {
                //Configurar el token en la cabecera de la peticion
                var token = Session["Token"];
                httpcliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

                httpcliente.BaseAddress = new Uri("https://localhost:44311/api/TBL_CLIENTES/" + codigoCliente);
                //PUT
                var putTask = httpcliente.PutAsJsonAsync<TBL_CLIENTES>(codigourl, cliente);
                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    TempData["mensaje"] = "1";
                }
                else
                {
                    TempData["mensaje"] = "2";
                }
                return RedirectToAction("ListaClientes");
            }
        }
        else
        {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
        }
        }


        [HttpPost]
        public ActionResult UpdateClienteAct(int btnActivar, int codPersonaact)
        {
            var permiso = Convert.ToString(Session["Rol"]);

            if (permiso == "CAJERO")
            {
                TempData["sesionin"] = "400";
                return RedirectToAction("../Login/Logout");
            }

            if (Session["LogOn"] != null)
            {

            var codigourl = btnActivar.ToString();
            var codigoCliente = btnActivar;

            var cliente = new TBL_CLIENTES();
            cliente.COD_CLIENTE = btnActivar;
            cliente.COD_PERSONA = codPersonaact;
            cliente.ESTADO = "ACTIVO";


            using (var httpcliente = new HttpClient())
            {
                //Configurar el token en la cabecera de la peticion
                var token = Session["Token"];
                httpcliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

                httpcliente.BaseAddress = new Uri("https://localhost:44311/api/TBL_CLIENTES/" + codigoCliente);
                //PUT
                var putTask = httpcliente.PutAsJsonAsync<TBL_CLIENTES>(codigourl, cliente);
                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    TempData["mensaje2"] = "1";
                }
                else
                {
                    TempData["mensaje2"] = "2";
                }
                return RedirectToAction("ListaClientes");
            }

            }
            else
            {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
            }
        }



        public async Task<ActionResult> GestionCuentas()
        {
            var permiso = Convert.ToString(Session["Rol"]);

            if (permiso == "CAJERO")
            {
                TempData["sesionin"] = "400";
                return RedirectToAction("../Login/Logout");
            }

            if (Session["LogOn"] != null)
            {

            // instancia de http cliente
            var HTTPClient = new HttpClient();

            //Configurar el token en la cabecera de la peticion
            var token = Session["Token"];
            HTTPClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

            //ejecutar la url
            var Consulta = await HTTPClient.GetAsync("https://localhost:44311/api/TBL_TIPO_CUENTA/");
            //obtener el json

            var Contenido = Consulta.Content.ReadAsStringAsync().Result;
            //convertir la json a lista y asignarlo al objeto definido globalmente
            var lst = JsonConvert.DeserializeObject<List<TBL_TIPO_CUENTA>>(Contenido).ToList();

            return View(lst);
            }
            else
            {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
            }
        }


        [HttpPost]
        public ActionResult NuevoTipCuenta(string txttipcuenta)
        {
            var permiso = Convert.ToString(Session["Rol"]);

            if (permiso == "CAJERO")
            {
                TempData["sesionin"] = "400";
                return RedirectToAction("../Login/Logout");
            }

            if (Session["LogOn"] != null)
            {

            try
            {
                var Cuenta = new TBL_TIPO_CUENTA();
                Cuenta.TIPO_CUENTA = txttipcuenta;
                Cuenta.ESTADO_CUENTA = "ACTIVO";

                using (var HttpClient = new HttpClient())
                {
                    //Configurar el token en la cabecera de la peticion
                    var token = Session["Token"];
                    HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

                    HttpClient.BaseAddress = new Uri("https://localhost:44311/api/TBL_TIPO_CUENTA/");

                    //Hacer post
                    var Post = HttpClient.PostAsJsonAsync<TBL_TIPO_CUENTA>("TBL_TIPO_CUENTA", Cuenta);

                    Post.Wait();
                    if (Post.Result.IsSuccessStatusCode)
                    {
                        TempData["mensaje3"] = "1";
                    }
                    else
                    {
                        TempData["mensaje3"] = "2";
                    }
                    return RedirectToAction("GestionCuentas");
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
        public ActionResult UpdateEstadoCuentaDes(int btnDesactivarCta, string txttipocta)
        {
            var permiso = Convert.ToString(Session["Rol"]);

            if (permiso == "CAJERO")
            {
                TempData["sesionin"] = "400";
                return RedirectToAction("../Login/Logout");
            }

            if (Session["LogOn"] != null)
            {
         
            var codigourl = btnDesactivarCta.ToString();
            var codigoCliente = btnDesactivarCta;

            var cuenta = new TBL_TIPO_CUENTA();
            cuenta.COD_TIP_CUENTA = btnDesactivarCta;
            cuenta.TIPO_CUENTA = txttipocta;
            cuenta.ESTADO_CUENTA = "INACTIVA";


            using (var httpcliente = new HttpClient())
            {
                //Configurar el token en la cabecera de la peticion
                var token = Session["Token"];
                httpcliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

                httpcliente.BaseAddress = new Uri("https://localhost:44311/api/TBL_TIPO_CUENTA/" + codigoCliente);
                //PUT
                var putTask = httpcliente.PutAsJsonAsync<TBL_TIPO_CUENTA>(codigourl, cuenta);
                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    TempData["mensajeCta1"] = "1";
                }
                else
                {
                    TempData["mensajeCta1"] = "2";
                }
                return RedirectToAction("GestionCuentas");
            }
          }
          else
          {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
            }
        }

        [HttpPost]
        public ActionResult UpdateEstadoCuentaAct(int btnActivarCta, string txttipocta)
        {
            var permiso = Convert.ToString(Session["Rol"]);

            if (permiso == "CAJERO")
            {
                TempData["sesionin"] = "400";
                return RedirectToAction("../Login/Logout");
            }

            if (Session["LogOn"] != null)
            {
            
            var codigourl = btnActivarCta.ToString();
            var codigoCliente = btnActivarCta;

            var cuenta = new TBL_TIPO_CUENTA();
            cuenta.COD_TIP_CUENTA = btnActivarCta;
            cuenta.TIPO_CUENTA = txttipocta;
            cuenta.ESTADO_CUENTA = "ACTIVO";


            using (var httpcliente = new HttpClient())
            {
                //Configurar el token en la cabecera de la peticion
                var token = Session["Token"];
                httpcliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

                httpcliente.BaseAddress = new Uri("https://localhost:44311/api/TBL_TIPO_CUENTA/" + codigoCliente);
                //PUT
                var putTask = httpcliente.PutAsJsonAsync<TBL_TIPO_CUENTA>(codigourl, cuenta);
                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    TempData["mensajeCta2"] = "1";
                }
                else
                {
                    TempData["mensajeCta2"] = "2";
                }
                return RedirectToAction("GestionCuentas");
            }
            }
            else
            {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
            }
        }

        //vista de asignar las cuentas a los clientes
        //llenar modales
        public async Task<ActionResult> AsignarCuenta()
        {
            var permiso = Convert.ToString(Session["Rol"]);

            if (permiso == "CAJERO")
            {
                TempData["sesionin"] = "400";
                return RedirectToAction("../Login/Logout");
            }

            if (Session["LogOn"] != null)
            {
          
            // instancia de http cliente
            var HTTPClient = new HttpClient();
            //ejecutar la url
            //Configurar el token en la cabecera de la peticion
            var token = Session["Token"];
            HTTPClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());


            var Consulta = await HTTPClient.GetAsync("https://localhost:44311/api/SpClientes/");
            var Consulta2 = await HTTPClient.GetAsync("https://localhost:44311/api/SpTipoCuenta/");

            var Contenido = Consulta.Content.ReadAsStringAsync().Result;
            var Contenido2 = Consulta2.Content.ReadAsStringAsync().Result;

            var lst = JsonConvert.DeserializeObject<List<spListarClientes>>(Contenido).ToList();
            var lst2 = JsonConvert.DeserializeObject<List<spListarCuentasAc>>(Contenido2).ToList();

            ViewBag.valor = lst;
            ViewBag.valor2 = lst2;

            return View();
            }
            else
            {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
            }
        }

        [HttpPost]
        public ActionResult CuentarelCliente(int txtcod_cliente, int txtcodigo_cuenta)
        {
            var permiso = Convert.ToString(Session["Rol"]);

            if (permiso == "CAJERO")
            {
                TempData["sesionin"] = "400";
                return RedirectToAction("../Login/Logout");
            }

            if (Session["LogOn"] != null)
            {
           
            try
            {
                var asignacion = new TBL_CUENTAS_BANCARIAS();

                asignacion.COD_CLIENTE = txtcod_cliente;
                asignacion.COD_TIP_CUENTA = txtcodigo_cuenta;
                asignacion.ESTADO = "ACTIVO";
                asignacion.SALDO = 0;
                asignacion.NUMERO_CTA = 0;
               

                using (var HttpClient = new HttpClient())
                {
                    //Configurar el token en la cabecera de la peticion
                    var token = Session["Token"];
                    HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

                    HttpClient.BaseAddress = new Uri("https://localhost:44311/api/TBL_CUENTAS_BANCARIAS/");

                    //Hacer post
                    var Post = HttpClient.PostAsJsonAsync<TBL_CUENTAS_BANCARIAS>("TBL_CUENTAS_BANCARIAS", asignacion);

                    Post.Wait();
                    if (Post.Result.IsSuccessStatusCode)
                    {
                        TempData["sweet"] = "1";
                    }
                    else
                    {
                        TempData["sweet"] = "2";
                    }
                    return RedirectToAction("AsignarCuenta");
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
        public ActionResult UpdateEstadoCuentaI(int txtcodcuenta, int txtcodCliente, int txtcodtipo, decimal txtsaldo, int txtnumerocta)
        {
            var permiso = Convert.ToString(Session["Rol"]);

            if (permiso == "CAJERO")
            {
                TempData["sesionin"] = "400";
                return RedirectToAction("../Login/Logout");
            }

            if (Session["LogOn"] != null)
            {
           

            var codigourl = txtcodcuenta.ToString();
            var codigocuenta = txtcodcuenta;

            var cuenta = new TBL_CUENTAS_BANCARIAS();
            cuenta.NUMERO_CTA = txtnumerocta;
            cuenta.COD_CUENTA = txtcodcuenta;
            cuenta.COD_CLIENTE = txtcodCliente;
            cuenta.COD_TIP_CUENTA = txtcodtipo;
            cuenta.SALDO = txtsaldo;
            cuenta.ESTADO = "INACTIVO";


            using (var httpcliente = new HttpClient())
            {
                //Configurar el token en la cabecera de la peticion
                var token = Session["Token"];
                httpcliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

                httpcliente.BaseAddress = new Uri("https://localhost:44311/api/TBL_CUENTAS_BANCARIAS/" + codigocuenta);
                //PUT
                var putTask = httpcliente.PutAsJsonAsync<TBL_CUENTAS_BANCARIAS>(codigourl, cuenta);
                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    TempData["cambio1"] = "1";
                }
                else
                {
                    TempData["cambio1"] = "2";
                }
                return RedirectToAction("AdministrarCuentas");
            }
            }
            else
            {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
            }
        }

        [HttpPost]
        public ActionResult UpdateEstadoCuentaA(int txtcodcuenta, int txtcodCliente, int txtcodtipo, decimal txtsaldo, int txtnumerocta)
        {
            var permiso = Convert.ToString(Session["Rol"]);

            if (permiso == "CAJERO")
            {
                TempData["sesionin"] = "400";
                return RedirectToAction("../Login/Logout");
            }

            if (Session["LogOn"] != null)
            {
          
            var codigourl = txtcodcuenta.ToString();
            var codigocuenta = txtcodcuenta;

            var cuenta = new TBL_CUENTAS_BANCARIAS();
            cuenta.NUMERO_CTA = txtnumerocta;
            cuenta.COD_CUENTA = txtcodcuenta;
            cuenta.COD_CLIENTE = txtcodCliente;
            cuenta.COD_TIP_CUENTA = txtcodtipo;
            cuenta.SALDO = txtsaldo;
            cuenta.ESTADO = "ACTIVO";


            using (var httpcliente = new HttpClient())
            {
                //Configurar el token en la cabecera de la peticion
                var token = Session["Token"];
                httpcliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

                httpcliente.BaseAddress = new Uri("https://localhost:44311/api/TBL_CUENTAS_BANCARIAS/" + codigocuenta);
                //PUT
                var putTask = httpcliente.PutAsJsonAsync<TBL_CUENTAS_BANCARIAS>(codigourl, cuenta);
                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    TempData["cambio"] = "1";
                }
                else
                {
                    TempData["cambio"] = "2";
                }
                return RedirectToAction("AdministrarCuentas");
            }
            }
            else
            {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
            }
        }

        public async Task<ActionResult> AdministrarCuentas()
        {
            var permiso = Convert.ToString(Session["Rol"]);

            if (permiso == "CAJERO")
            {
                TempData["sesionin"] = "400";
                return RedirectToAction("../Login/Logout");
            }

            if (Session["LogOn"] != null)
            {
          

            // instancia de http cliente
            var HTTPClient = new HttpClient();

            //Configurar el token en la cabecera de la peticion
            var token = Session["Token"];
            HTTPClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());

            //ejecutar la url
            var Consulta = await HTTPClient.GetAsync("https://localhost:44311/api/SpCuentasAdmon/");
            //obtener el json

            var Contenido = Consulta.Content.ReadAsStringAsync().Result;
            //convertir la json a lista y asignarlo al objeto definido globalmente
            var lst = JsonConvert.DeserializeObject<List<spAdministrarCuentas>>(Contenido).ToList();

            return View(lst);
            }
            else
            {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
            }
        }


        public async Task<ActionResult> ListaUsuarios()
        {
            var permiso = Convert.ToString(Session["Rol"]);

            if (permiso == "CAJERO")
            {
                TempData["sesionin"] = "400";
                return RedirectToAction("../Login/Logout");
            }

            if (Session["LogOn"] != null)
            {
                // instancia de http cliente
                var HTTPClient = new HttpClient();

                //Configurar el token en la cabecera de la peticion
                var token = Session["Token"];
                HTTPClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());


                //ejecutar la url
                var Consulta = await HTTPClient.GetAsync("https://localhost:44311/api/SpListarUsuarios/");
                //obtener el json

                var Contenido = Consulta.Content.ReadAsStringAsync().Result;
                //convertir la json a lista y asignarlo al objeto definido globalmente
                var lst = JsonConvert.DeserializeObject<List<spListarUsuarios>>(Contenido).ToList();

                return View(lst);
            }
            else
            {
                TempData["sesionin"] = "1";
                return RedirectToAction("../Login/Logout");
            }
        }

        public ActionResult EstadoUser(string cmbtipoUser, int txtcoduser)
        {
            var permiso = Convert.ToString(Session["Rol"]);

            if (permiso == "CAJERO")
            {
                TempData["sesionin"] = "400";
                return RedirectToAction("../Login/Logout");
            }

            var codigo = Convert.ToInt32(txtcoduser);

            var HttpClient = new HttpClient();         
            HttpClient.BaseAddress = new Uri("https://localhost:44311/api/");
            //HTTP GET
            var responseTask = HttpClient.GetAsync($"SpEstadoUser?cod_Usuario={codigo}&estado={cmbtipoUser}");
            responseTask.Wait();
            var result = responseTask.Result.Content.ReadAsStringAsync();

            if (responseTask.Result.IsSuccessStatusCode)
            {
                TempData["content"] = "1";
            }
            else
            {
                TempData["content"] = "2";
            }

            return RedirectToAction("ListaUsuarios");
        }


        public ActionResult CambioPass(string txtnewpass, string txtconfirmpass, int txtcoduser2)
        {
            var permiso = Convert.ToString(Session["Rol"]);

            if (permiso == "CAJERO")
            {
                TempData["sesionin"] = "400";
                return RedirectToAction("../Login/Logout");
            }

            if (txtnewpass != txtconfirmpass)
            {
                TempData["Error"] = "1";
                return RedirectToAction("ListaUsuarios");
            }

            if(txtnewpass == "" & txtconfirmpass == "")
            {
                TempData["msg"] = "2";
                return RedirectToAction("ListaUsuarios");
            }

            string contra = Crypto.HashPassword(txtnewpass);

            var HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri("https://localhost:44311/api/");
            //HTTP GET
            var responseTask = HttpClient.GetAsync($"SpCambiarPass?codUser={txtcoduser2}&pass={contra}");
            responseTask.Wait();
            var result = responseTask.Result.Content.ReadAsStringAsync();

            if (responseTask.Result.IsSuccessStatusCode)
            {
                TempData["msg"] = "1";
            }    
            return RedirectToAction("ListaUsuarios");
        }

    }


}
