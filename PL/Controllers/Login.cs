using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Login : Controller
    {
        public ActionResult LoginInicio()
        {
            return View();
        }
    }
}
