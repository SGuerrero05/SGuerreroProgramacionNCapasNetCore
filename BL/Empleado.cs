using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Empleado
    {
        public static ML.Result EmpleadoAdd(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleado.NumeroEmpleado}','{empleado.RFC}','{empleado.Nombre}','{empleado.ApellidoPaterno}','{empleado.ApellidoMaterno}','{empleado.Email}','{empleado.Telefono}','{empleado.FechaNacimiento}','{empleado.NSS}',{empleado.FechaIngreso},'{empleado.FOTO}',{empleado.Empresa.IdEmpresa}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }

        public static ML.Result EmpleadoUpdate(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoUpdate '{empleado.NumeroEmpleado}', '{empleado.RFC}', '{empleado.Nombre}', '{empleado.ApellidoPaterno}', '{empleado.ApellidoMaterno}', '{empleado.Email}', '{empleado.Telefono}', '{empleado.FechaNacimiento}', '{empleado.NSS}', '{empleado.FOTO}', {empleado.Empresa.IdEmpresa}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }

        public static ML.Result EmpleadoDelete(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoDelete '{empleado.NumeroEmpleado}'");
                        if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }

        public static ML.Result EmpleadoGetAll(ML.Empleado empleadoBusquedaAbierta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {

                    var query = context.Empleados.FromSqlRaw($"EmpleadoGetAll '{empleadoBusquedaAbierta.Nombre}', '{empleadoBusquedaAbierta.ApellidoPaterno}','{empleadoBusquedaAbierta.ApellidoMaterno}'").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.NumeroEmpleado = obj.NumeroEmpleado;
                            empleado.RFC = obj.Rfc;
                            empleado.Nombre = obj.Nombre;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;
                            empleado.Email = obj.Email;
                            empleado.Telefono = obj.Telefono;
                            empleado.FechaNacimiento = obj.FechaNacimiento.Value;
                            empleado.NSS = obj.Nss;
                            empleado.FechaIngreso = obj.FechaIngreso.Value;
                            empleado.FOTO = obj.Foto;
                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = obj.IdEmpresa.Value;

                            result.Objects.Add(empleado);

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }

        public static ML.Result EmpleadoGetById(string NumeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var obj = context.Empleados.FromSqlRaw($"EmpleadoGetById '{NumeroEmpleado}'").AsEnumerable().FirstOrDefault();
                    if (obj != null)
                    {
                        ML.Empleado empleado = new ML.Empleado();
                        empleado.NumeroEmpleado = obj.NumeroEmpleado;
                        empleado.RFC = obj.Rfc;
                        empleado.Nombre = obj.Nombre;
                        empleado.ApellidoPaterno = obj.ApellidoPaterno;
                        empleado.ApellidoMaterno = obj.ApellidoMaterno;
                        empleado.Email = obj.Email;
                        empleado.Telefono = obj.Telefono;
                        empleado.FechaNacimiento = obj.FechaNacimiento.Value;
                        empleado.NSS = obj.Nss;
                        empleado.FechaIngreso = obj.FechaIngreso.Value;
                        empleado.FOTO = obj.Foto;
                        empleado.Empresa = new ML.Empresa();
                        empleado.Empresa.IdEmpresa = obj.IdEmpresa.Value;

                        result.Object = empleado;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
