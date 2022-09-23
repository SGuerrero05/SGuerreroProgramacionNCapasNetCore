using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BL
{
    public class Usuario
    {
        public static ML.Result UsuarioAdd(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}','{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}', {usuario.Rol.IdRol}, '{usuario.UserName}', '{usuario.Password}', '{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.FechaNacimiento}', '{usuario.CURP}','{usuario.Imagen}',{usuario.Direccion.IdDireccion},'{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}','{usuario.Direccion.NumeroExterior}',{usuario.Direccion.Colonia.IdColonia},{usuario.Status}");
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
        public static ML.Result UsuarioUpdate(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario}, '{usuario.Nombre}', '{ usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.CURP}', {usuario.Rol.IdRol}, '{usuario.UserName}', '{usuario.Password}', '{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.FechaNacimiento}', '{usuario.CURP}', '{usuario.Imagen}', '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', '{usuario.Direccion.Colonia.IdColonia}', {usuario.Status}");
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
        public static ML.Result UsuarioDelete(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioDelete {usuario.IdUsuario}");
                    if (query > 1)
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
        public static ML.Result UsuarioGetAll(ML.Usuario usuarioBusquedaAbierta)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    usuarioBusquedaAbierta.Nombre = (usuarioBusquedaAbierta.Nombre == null) ? "" : usuarioBusquedaAbierta.Nombre;
                    usuarioBusquedaAbierta.ApellidoPaterno = (usuarioBusquedaAbierta.ApellidoPaterno == null) ? "" : usuarioBusquedaAbierta.ApellidoPaterno;
                    usuarioBusquedaAbierta.ApellidoMaterno = (usuarioBusquedaAbierta.ApellidoMaterno == null) ? "" : usuarioBusquedaAbierta.ApellidoMaterno;

                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuarioBusquedaAbierta.Nombre}','{usuarioBusquedaAbierta.ApellidoPaterno}','{usuarioBusquedaAbierta.ApellidoMaterno}'").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Email = obj.Email;
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.Nombre = obj.NombreRol;
                            usuario.UserName = obj.UserName;
                            usuario.Password = obj.Password;
                            usuario.Sexo = obj.Sexo;
                            usuario.Telefono = obj.Telefono;
                            usuario.Celular = obj.Celular;
                            usuario.FechaNacimiento = obj.FechaNacimiento.Value;
                            usuario.CURP = obj.Curp;
                            usuario.Imagen = obj.Imagen;

                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.IdDireccion = obj.IdDireccion;
                            usuario.Direccion.Calle = obj.Calle;
                            usuario.Direccion.NumeroExterior = obj.NumeroExterior;
                            usuario.Direccion.NumeroInterior = obj.NumeroInterior;

                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.IdColonia = obj.IdColonia;
                            usuario.Direccion.Colonia.Nombre = obj.NombreColonia;
                            usuario.Direccion.Colonia.CodigoPostal = obj.CodigoPostal;

                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = obj.IdMunicipio;
                            usuario.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;

                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = obj.IdEstado;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;

                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = obj.IdPais;
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.NombrePais;

                            usuario.Status = obj.Status;


                            result.Objects.Add(usuario);
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

        public static ML.Result UsuarioGetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var obj = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();
                    if (obj != null)
                    {

                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = obj.IdUsuario;
                        usuario.Nombre = obj.Nombre;
                        usuario.ApellidoPaterno = obj.ApellidoPaterno;
                        usuario.ApellidoMaterno = obj.ApellidoMaterno;
                        usuario.Email = obj.Email;
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = obj.IdRol.Value;
                        usuario.UserName = obj.UserName;
                        usuario.Password = obj.Password;
                        usuario.Sexo = obj.Sexo;
                        usuario.Telefono = obj.Telefono;
                        usuario.Celular = obj.Celular;
                        usuario.FechaNacimiento = obj.FechaNacimiento.Value;
                        usuario.CURP = obj.Curp;
                        usuario.Imagen = obj.Imagen;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = obj.IdDireccion;
                        usuario.Direccion.Calle = obj.Calle;
                        usuario.Direccion.NumeroExterior = obj.NumeroExterior;
                        usuario.Direccion.NumeroInterior = obj.NumeroInterior;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = obj.IdColonia;
                        usuario.Direccion.Colonia.Nombre = obj.NombreColonia;
                        usuario.Direccion.Colonia.CodigoPostal = obj.CodigoPostal;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = obj.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;

                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = obj.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;

                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = obj.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.NombrePais;

                        usuario.Status = obj.Status;

                        result.Object = usuario;

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
        public static ML.Result UsuarioGetByUserName(string UserName)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var obj = context.Usuarios.FromSqlRaw($"UsuarioGetByUserName '{UserName}'").AsEnumerable().FirstOrDefault();
                    if (obj != null)
                    {

                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = obj.IdUsuario;
                   
                        usuario.UserName = obj.UserName;
                        usuario.Password = obj.Password;
 

                        result.Object = usuario;

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
