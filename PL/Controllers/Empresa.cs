using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Empresa : Controller
    {

        [HttpGet]
        public ActionResult EmpresaGetAll()
        {
            ML.Empresa empresa = new ML.Empresa();
            ML.Result result = BL.Empresa.EmpresaGetAll();
            if (result.Correct)
            {
                empresa.Empresas = result.Objects;
            }
            else
            {
                result.Correct = false;
                ViewBag.Mensaje = "Fallo la consulta";

            }
            return View(empresa);
        }
        [HttpGet]
        public ActionResult EmpresaForm(int? IdEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();
            if (IdEmpresa == null)
            {
                return View(empresa);
            }
            else
            {
                ML.Result result = BL.Empresa.EmpresaGetById(IdEmpresa.Value);
                if (result.Correct)
                {
                    empresa = (ML.Empresa)result.Object;
                    return View (empresa);
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error";
                    return View("EmpresaModal");
                }
            }
            return View(empresa);
        }
        [HttpPost]
        public ActionResult EmpresaForm(ML.Empresa empresa)
        {
            if (empresa.IdEmpresa == 0)
            {
                ML.Result result = BL.Empresa.EmpresaAdd(empresa);
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
                ML.Result result = BL.Empresa.EmpresaUpdate(empresa);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Modificacion exitosa";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error";
                }
            }
            return View("EmpresaModal");
        }

        [HttpGet]

        public ActionResult EmpresaDelete(int IdEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();
            empresa.IdEmpresa = IdEmpresa;
            ML.Result result = BL.Empresa.EmpresaDelete(empresa);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Se elimino correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error";
            }
            return View("EmpresaModal");
        }
    }
}
