using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Dependiente : Controller
    {
        [HttpGet]
        public ActionResult EmpleadoDGetAll()
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
        public ActionResult EmpleadoDGetAll(ML.Empleado empleado)
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
        public ActionResult DependienteGet(string numeroEmpleado)
        {
            if (numeroEmpleado == null)
            {
                numeroEmpleado = HttpContext.Session.GetString("NumeroEmpleado");
            }
            ML.Dependiente dependiente = new ML.Dependiente();
            ML.Result resultDependientes = BL.Dependiente.DependienteGetByEmpleado(numeroEmpleado);

            if (resultDependientes.Correct)
            {
                HttpContext.Session.SetString("NumeroEmpleado", numeroEmpleado);
                dependiente.Dependientes = new List<object>();
                dependiente.Dependientes = resultDependientes.Objects;
                return View(dependiente);
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error al cargar los dependientes" + resultDependientes.ErrorMessage;
                return PartialView("DependienteModal");

            }
        }

        [HttpGet]

        public ActionResult DependienteForm(int? IdDependiente)
        {
            ML.Dependiente dependiente = new ML.Dependiente();
            dependiente.DependienteTipo = new ML.DependienteTipo();
            ML.Result resultDependienteTipo = BL.DependienteTipo.DependienteTipoGetAll();
           
            if (resultDependienteTipo.Correct)
            {
                if (IdDependiente == null)
                {
                    dependiente.Empleado = new ML.Empleado();
                    dependiente.Empleado.NumeroEmpleado = HttpContext.Session.GetString("NumeroEmpleado");
                    dependiente.DependienteTipo = new ML.DependienteTipo();
                    dependiente.DependienteTipo.DependienteTipos = resultDependienteTipo.Objects;
                    return View(dependiente);
                }
                else
                {
                    ML.Result resultDependiente = BL.Dependiente.DependienteGetById(IdDependiente.Value);
                    if (resultDependiente.Correct)
                    {
                        dependiente = ((ML.Dependiente)resultDependiente.Object);
                        dependiente.DependienteTipo.DependienteTipos = resultDependienteTipo.Objects;
                        return View(dependiente);

                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un error al obtener al dependiente" + resultDependiente;
                        return PartialView("DependienteModal");
                    }
                }
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error de consulta";
            }
            return View(dependiente);
        }

        [HttpPost]
        public ActionResult DependienteForm(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();
            if (dependiente.IdDependiente == 0)
            {
                result = BL.Dependiente.DependienteAdd(dependiente);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Registro exitoso";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrior un error" + result.ErrorMessage;

                }
            }
            else
            {
                result = BL.Dependiente.DependienteUpdate(dependiente);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Modificacion exitosa";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error";
                }
            }
            return View("DependienteModal");

        }
        [HttpGet]
        public ActionResult DependienteDelete(int IdDependiente)
        {
            ML.Dependiente dependiente = new ML.Dependiente();
            dependiente.IdDependiente = IdDependiente;
            ML.Result result = BL.Dependiente.DependienteDelete(dependiente);
            if (result.Correct)
            {
                ViewBag.Mensaje = "Se elimino correctamente";
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error";
            }
            return View("DependienteModal");
        }
    }
}


