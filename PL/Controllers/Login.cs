using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Login : Controller
    {
        [HttpGet]
        public ActionResult LoginInicio()
        {
            ML.Usuario usuario = new ML.Usuario();
            return View(usuario);

        }
        [HttpPost]
        public ActionResult LoginInicio(ML.Usuario usuarioLogin)
        {
            ML.Result result = BL.Usuario.UsuarioGetByUserName(usuarioLogin.UserName);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                if (usuarioLogin.Password == usuario.Password)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Mensaje = "Credenciales invalidas";
                    return View("ModalError");
                  
                }
            }
            else
            {
                ViewBag.Mensaje = "El username no esta registrado";
                return View("ModalError");
            }

        }
    }
}
