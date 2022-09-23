using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Poliza
    {
        public static ML.Result PolizaAdd(ML.Poliza poliza)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"PolizaAdd '{poliza.Nombre}', '{poliza.NumeroPoliza}', {poliza.SubPoliza.IdSubPoliza}, {poliza.Usuario.IdUsuario}");
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

        public static ML.Result PolizaUpdate(ML.Poliza poliza)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"PolizaUpdate {poliza.IdPoliza}, '{poliza.Nombre}', '{poliza.NumeroPoliza}', {poliza.SubPoliza.IdSubPoliza}, {poliza.Usuario.IdUsuario}");
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

        public static ML.Result PolizaDelete(ML.Poliza poliza)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"PolizaDelete {poliza.IdPoliza}");
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

        public static ML.Result PolizaGetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var query = context.Polizas.FromSqlRaw($"PolizaGetAll").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Poliza poliza = new ML.Poliza();

                            poliza.IdPoliza = obj.IdPoliza;
                            poliza.Nombre = obj.Nombre;
                            poliza.NumeroPoliza = obj.NumeroPoliza;
                            poliza.FechaCreacion = obj.FechaCreacion.ToString();
                            poliza.FechaModificacion = obj.FechaModificacion.ToString();
                            poliza.SubPoliza = new ML.SubPoliza();
                            poliza.SubPoliza.IdSubPoliza = obj.IdSubPoliza.Value;
                            poliza.Usuario = new ML.Usuario();
                            poliza.Usuario.IdUsuario = obj.IdUsuario.Value;

                            result.Objects.Add(poliza);
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
        public static ML.Result PolizaGetById(int IdPoliza)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var obj = context.Polizas.FromSqlRaw($"PolizaGetById {IdPoliza}").AsEnumerable().FirstOrDefault();
                  
                    if (obj != null)
                    {
                       
                            ML.Poliza poliza = new ML.Poliza();

                            poliza.IdPoliza = obj.IdPoliza;
                            poliza.Nombre = obj.Nombre;
                            poliza.NumeroPoliza = obj.NumeroPoliza;
                            poliza.FechaCreacion = obj.FechaCreacion.ToString();
                            poliza.FechaModificacion = obj.FechaModificacion.ToString();
                            poliza.SubPoliza = new ML.SubPoliza();
                            poliza.SubPoliza.IdSubPoliza = obj.IdSubPoliza.Value;
                            poliza.Usuario = new ML.Usuario();
                            poliza.Usuario.IdUsuario = obj.IdUsuario.Value;

                            result.Object = poliza;
                        
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


