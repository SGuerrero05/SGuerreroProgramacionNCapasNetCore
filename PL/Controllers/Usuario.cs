using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Usuario : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultApi = new ML.Result();
         //  ML.Result result = BL.Usuario.UsuarioGetAll(usuario);
           using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5216/");

                var responseTask = client.GetAsync("api/Usuario/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    resultApi.Objects = new List<object>();
                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Usuario resultUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                        resultApi.Objects.Add(resultUsuario);
                    }
                }

            }
            usuario.Usuarios = resultApi.Objects;
            return View(usuario);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {

            ML.Result result = BL.Usuario.UsuarioGetAll(usuario);
            if (result.Correct)
            {
                usuario.Usuarios=result.Objects;
            }
            else
            {
                result.Correct = false;
                ViewBag.Mensaje = "Fallo la consulta";
            }
            return View(usuario);
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result resultRol = BL.Rol.RolGetAll();
            ML.Result resultPais = BL.Pais.PaisGetAll();


            if (resultRol.Correct && resultPais.Correct)
            {
                if (IdUsuario == null)

                {
                    usuario.Rol = new ML.Rol();
                    usuario.Rol.Roles = resultRol.Objects;
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                    usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

                    return View(usuario);
                }

                else

                {
                  //  ML.Result result = BL.Usuario.UsuarioGetById(IdUsuario.Value);
                  ML.Result resultApi = new ML.Result();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:5216/");

                        var responseTask = client.GetAsync("api/Usuario/GetById?IdUsuario=" + IdUsuario);
                        responseTask.Wait();

                        var result = responseTask.Result;

                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            resultApi.Objects = new List<object>();

                            var resultItem = readTask.Result.Object;

                            ML.Usuario resultUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            resultApi.Object = resultUsuario;


                         

                            usuario = (ML.Usuario)resultApi.Object;
                            usuario.Rol.Roles = resultRol.Objects;
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                            ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.IdColonia);
                            ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                            ML.Result resultEstado = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);

                            usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                            usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                           
                                return View(usuario);

                            
                        }
                        else
                        {
                            ViewBag.Mensaje = "Ocurrio un error";
                            return View("Modal");
                        }
                    }
                    }

                }
            
            else
            {
                ViewBag.Mensaje = "Ocurrio un error de consulta";
            }
            return View(usuario);
        }


        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (usuario.IdUsuario == 0)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:5216/");

                        var postTask = client.PostAsJsonAsync<ML.Usuario>("api/Usuario/Add", usuario);
                        postTask.Wait();

                        var result = postTask.Result;

                        if (result.IsSuccessStatusCode)
                        {
                            ViewBag.Mensaje = "Registro exitoso";
                            return PartialView("Modal");
                        }
                        else
                        {
                            ViewBag.Mensaje = "Ocurrio un error";
                            return PartialView("Modal");
                        }
                    }
                }
                else
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:5216/");

                        var postTask = client.PutAsJsonAsync<ML.Usuario>("api/Usuario/Update", usuario);
                        postTask.Wait();

                        var result = postTask.Result;

                        if (result.IsSuccessStatusCode)
                        {
                            ViewBag.Mensaje = "Registro exitoso";
                            return PartialView("Modal");
                        }
                        else
                        {
                            ViewBag.Mensaje = "Ocurrio un error";
                            return PartialView("Modal");
                        }

                    }
                    return View("Modal");
                }
            }
            else
            {
                ML.Result resultRol = BL.Rol.RolGetAll();
                ML.Result resultPais = BL.Pais.PaisGetAll();
                ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.IdColonia);
                ML.Result resultMunicipio = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                ML.Result resultEstado = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Rol.Roles = resultRol.Objects;
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;


                return View(usuario);
            }
        }
        



        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = IdUsuario;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5216/");

                var postTask = client.DeleteAsync("/api/Usuario/Delete?IdUsuario="+ IdUsuario);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Eliminacion exitosa";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error";
                    return PartialView("Modal");
                }
            }


        }
        public JsonResult EstadoGetByIdPais(int IdPais)
        {
            ML.Result result = BL.Estado.GetByIdPais(IdPais);
            return Json(result.Objects);//, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = BL.Municipio.GetByIdEstado(IdEstado);
            return Json(result.Objects);//, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = BL.Colonia.GetByIdMunicipio(IdMunicipio);
            return Json(result.Objects);//, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateStatus(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.UsuarioGetById(IdUsuario);
            if (result.Correct)
            {

                usuario = (ML.Usuario)result.Object;

                usuario.Status = (usuario.Status) ? false : true;

                ML.Result resultUpdate = BL.Usuario.UsuarioUpdate(usuario);
                ViewBag.Mensaje = "Se actualizo su status";
                if (resultUpdate.Correct)
                {
                    ViewBag.Mensaje = "Se actualizo su status";
                }
                else
                {
                    ViewBag.Mensaje = "No se actualizo su status";
                }
            }
            return View("modal");
        }



        public byte[] ConvertToBytes(IFormFile imagen)
        {
            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

    }

}
