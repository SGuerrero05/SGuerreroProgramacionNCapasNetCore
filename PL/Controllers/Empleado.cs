using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Empleado : Controller
    {
        [HttpGet]
        public ActionResult EmpleadoGetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Result result = BL.Empleado.EmpleadoGetAll(empleado);
            ML.Result resultEmpresa = BL.Empresa.EmpresaGetAll();
            if (result.Correct)
            {
                empleado.Empresa = new ML.Empresa();
                empleado.Empresa.Empresas = resultEmpresa.Objects;
                empleado.Empleados = result.Objects;
            }
            else
            {
                result.Correct = false;
                ViewBag.Mensaje = "Fallo la consulta";
            }
            return View(empleado);
        }
        [HttpPost]
        public ActionResult EmpleadoGetAll(ML.Empleado empleado)
        {
            ML.Result result = BL.Empleado.EmpleadoGetAll(empleado);
            if (result.Correct)
            {
                empleado.Empleados = result.Objects;
            }
            else
            {
                result.Correct = false;
                ViewBag.Mensaje = "Fallo la consulta";
            }
            return View(empleado);
        }

        [HttpGet]

        public ActionResult EmpleadoForm(string NumeroEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            ML.Result resultEmpresa = BL.Empresa.EmpresaGetAll();
            if (resultEmpresa.Correct)
            {
                if (NumeroEmpleado == null) 
                {
                    empleado.Empresa = new ML.Empresa();
                    empleado.Empresa.Empresas = resultEmpresa.Objects;
                    return View(empleado);
                }
                else
                {
                    ML.Result result = BL.Empleado.EmpleadoGetById(NumeroEmpleado);
                    if (result.Correct)
                    {
                        empleado = (ML.Empleado)result.Object;
                        empleado.Empresa.Empresas = resultEmpresa.Objects;
                        return View(empleado);
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un error";
                        return View("Modal");
                    }
                }
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error de consulta";
            }
            return View (empleado);
        }

        [HttpPost]

        public ActionResult EmpleadoForm(ML.Empleado empleado)
        {
            if (empleado.NumeroEmpleado == null)
            {
                ML.Result result = BL.Empleado.EmpleadoAdd(empleado);
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
                ML.Result result = BL.Empleado.EmpleadoUpdate(empleado);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Modificacion exitosa";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error";
                }
            }
            return View("EmpleadoModal");
        }

        [HttpGet]
        public ActionResult EmpleadoDelete(string NumeroEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.NumeroEmpleado = NumeroEmpleado;
            ML.Result result = BL.Empleado.EmpleadoDelete(empleado);
            if (result.Correct)
            {
                ViewBag.Mensaje = "El registro se elimino correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error";
            }
            return View("EmpleadoModal");
        }
    }
}
