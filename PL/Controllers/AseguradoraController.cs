using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AseguradoraController : Controller
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
            ML.Result resultUsuario = BL.Usuario.UsuarioGetAll();
            if(resultUsuario.Correct)
            {
                if (IdAseguradora == null)
                {
                    ML.Result result = BL.Aseguradora.AseguradoraAdd(aseguradora);
                    aseguradora.Usuario.Usuarios = resultUsuario.Objects;
                    return View(aseguradora);
                }
                else
                {
                    ML.Result result = BL.Aseguradora.AseguradoraGetById(IdAseguradora.Value);
                    if (result.Correct)
                    {
                        aseguradora.Usuario = new ML.Usuario();
                        aseguradora = (ML.Aseguradora)result.Object;
                        aseguradora.Usuario.Usuarios = resultUsuario.Objects;
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un error";
                    }
                }
            }
            return View(aseguradora);
        }

        [HttpPost]
        public ActionResult FormAseguradora(ML.Aseguradora aseguradora)
        {
            if (aseguradora.IdAseguradora == 0)
            {
                ML.Result result = BL.Aseguradora.AseguradoraAdd(aseguradora);
                ViewBag.Mensaje = "Registro exitoso";
            }
            else
            {
                ML.Result result = BL.Aseguradora.AseguradoraUpdate(aseguradora);
                ViewBag.Mensaje = "Modificacion exitosa";
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
    }
}