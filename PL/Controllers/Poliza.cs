using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Poliza : Controller
    {
        [HttpGet]
        public ActionResult PolizaGetAll()
        {
            ML.Poliza poliza = new ML.Poliza();
            ML.Result result = BL.Poliza.PolizaGetAll();
            if (result.Correct)
            {
                poliza.Polizas = result.Objects;
            }
            else
            {
                result.Correct = false;
                ViewBag.Message = "Fallo la consulta";
            }
            return View(poliza);
        }
        [HttpGet]
        public ActionResult PolizaForm(int? IdPoliza)
        {
            ML.Poliza poliza = new ML.Poliza();
            ML.Usuario usuario = new ML.Usuario();
            poliza.SubPoliza = new ML.SubPoliza();  
            ML.Result resultUsuario = BL.Usuario.UsuarioGetAll(usuario);
            ML.Result resultSubPoliza = BL.SubPoliza.SubPolizaGetAll();

            if (resultUsuario.Correct && resultSubPoliza.Correct)
            {
                if (IdPoliza == null)
                {
                    poliza.Usuario = new ML.Usuario();
                    poliza.Usuario.Usuarios = resultUsuario.Objects;
                    poliza.SubPoliza = new ML.SubPoliza();
                    poliza.SubPoliza.SubPolizas = resultSubPoliza.Objects;

                    return View(poliza);
                }
                else
                {
                    ML.Result result = BL.Poliza.PolizaGetById(IdPoliza.Value);
                    if (result.Correct)
                    {
                        poliza = (ML.Poliza)result.Object;
                        poliza.Usuario.Usuarios = resultUsuario.Objects;
                        poliza.SubPoliza.SubPolizas = resultSubPoliza.Objects;
                        return View(poliza);
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error";
                        return View("PolizaModal");
                    }
                }
            }
            else
            {
                ViewBag.Message = "Ocurrio un error de consulta";
            }
            return View(poliza);

        }
        [HttpPost]
        public ActionResult PolizaForm(ML.Poliza poliza)
        {
            if (poliza.IdPoliza == 0)
            {
                ML.Result result = BL.Poliza.PolizaAdd(poliza);
                if (result.Correct)
                {
                    ViewBag.Message = "Registro exitoso";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error" + result.ErrorMessage;
                }
            }
            else
            {
                ML.Result result = BL.Poliza.PolizaUpdate(poliza);
                if (result.Correct)
                {
                    ViewBag.Message = "Modificacion exitosa";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error";
                }
            }
            return View("PolizaModal");
        }

        [HttpGet]

        public ActionResult PolizaDelete(int IdPoliza)
        {
            ML.Poliza poliza = new ML.Poliza();
            poliza.IdPoliza = IdPoliza;
            ML.Result result = BL.Poliza.PolizaDelete(poliza);
            if (result.Correct)
            {
                ViewBag.Message = "Se elimino correctamente";
            }
            else
            {
                ViewBag.Message = "Ocurrio un error";
            }
            return View("PolizaModal");
        }
    }
}
