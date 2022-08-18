using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Aseguradora : Controller
    {
        [HttpGet]
        public ActionResult AseguradoraGetAll()
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            ML.Result result = BL.Aseguradora.AseguradoraGetAll();
            if (result.Correct)
            {
                aseguradora.Aseguradoras = result.Objects;
            }
            else
            {
                result.Correct = false;
                ViewBag.Mensaje = "Fallo la consulta";
            }
            return View(aseguradora);
        }
        [HttpGet]

        public ActionResult FormAseguradora(int? IdAseguradora)
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            aseguradora.Usuario = new ML.Usuario();
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultUsuario = BL.Usuario.UsuarioGetAll(usuario);
            if (resultUsuario.Correct)
            {
                if (IdAseguradora == null)
                {
                    aseguradora.Usuario = new ML.Usuario();
                    aseguradora.Usuario.Usuarios = resultUsuario.Objects;

                    return View (aseguradora);
                }
                else
                {
                    ML.Result result = BL.Aseguradora.AseguradoraGetById(IdAseguradora.Value);
                    if (result.Correct)
                    {
                        
                        aseguradora = (ML.Aseguradora)result.Object;
                        aseguradora.Usuario.Usuarios = resultUsuario.Objects;
                        return View(aseguradora);
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un error";
                        return View("ModalAseguradora");
                    }
                }
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error de consulta";
            }
            return View(aseguradora);
        }

        [HttpPost]
        public ActionResult FormAseguradora(ML.Aseguradora aseguradora)
        {
            IFormFile imagen = Request.Form.Files["fuImage"];
            if (imagen != null)
            {
                byte[] ImagenByte = ConvertToBytes(imagen);
                aseguradora.Imagen = Convert.ToBase64String(ImagenByte);

            }

            if (aseguradora.IdAseguradora == 0)
            {
                ML.Result result = BL.Aseguradora.AseguradoraAdd(aseguradora);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Registro exitoso";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error" + result.ErrorMessage;
                }
            }
            else
            {
                ML.Result result = BL.Aseguradora.AseguradoraUpdate(aseguradora);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Modiciacion exitosa";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error";
                }

            }
            return View("ModalAseguradora");
        }



        [HttpGet]
        public ActionResult DeleteAseguradora(int IdAseguradora)
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            aseguradora.IdAseguradora = IdAseguradora;
            ML.Result result = BL.Aseguradora.AseguradoraDelete(aseguradora);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Se elimino correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error";
            }
            return View("ModalAseguradora");
        }

        public static byte[] ConvertToBytes(IFormFile imagen)
        {
            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}