using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.OleDb;
using System.Data;

namespace BL
{
    public class Empresa
    {
        public static ML.Result EmpresaAdd(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpresaAdd '{empresa.Nombre}','{empresa.Telefono}','{empresa.Email}','{empresa.DireccionWeb}','{empresa.Logo}'");
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

        public static ML.Result EmpresaUpdate(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpresaUpdate {empresa.IdEmpresa}, '{empresa.Nombre}', '{empresa.Telefono}', '{empresa.Email}', '{empresa.DireccionWeb}', '{empresa.Logo}'");
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

        public static ML.Result EmpresaDelete(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpresaDelete {empresa.IdEmpresa}");
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

        public static ML.Result EmpresaGetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var query = context.Empresas.FromSqlRaw($"EmpresaGetAll").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Empresa empresa = new ML.Empresa();

                            empresa.IdEmpresa = obj.IdEmpresa;
                            empresa.Nombre = obj.Nombre;
                            empresa.Telefono = obj.Telefono;
                            empresa.Email = obj.Email;
                            empresa.DireccionWeb = obj.DireccionWeb;
                            empresa.Logo = obj.Logo;

                            result.Objects.Add(empresa);
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

        public static ML.Result EmpresaGetById(int IdEmpresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var obj = context.Empresas.FromSqlRaw($"EmpresaGetById {IdEmpresa}").AsEnumerable().FirstOrDefault();

                    if (obj != null)
                    {
                        ML.Empresa empresa = new ML.Empresa();

                        empresa.IdEmpresa = obj.IdEmpresa;
                        empresa.Nombre = obj.Nombre;
                        empresa.Telefono = obj.Telefono;
                        empresa.Email = obj.Email;
                        empresa.DireccionWeb = obj.DireccionWeb;
                        empresa.Logo = obj.Logo;

                        result.Object = empresa;

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

        public static ML.Result ConvertirExcelDataTable(string connectionString)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (OleDbConnection context = new OleDbConnection(connectionString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableEmpresa = new DataTable();

                        da.Fill(tableEmpresa);

                        if (tableEmpresa.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableEmpresa.Rows)
                            {
                                ML.Empresa empresa = new ML.Empresa();
                                empresa.Nombre = row[0].ToString();
                                empresa.Telefono = row[1].ToString();
                                empresa.Email = row[2].ToString();
                                empresa.DireccionWeb = row[3].ToString();

                                result.Objects.Add(empresa);
                            }
                            result.Correct = true;
                        }
                        result.Object = tableEmpresa;
                        if (tableEmpresa.Rows.Count > 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el excel";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result ValidarExcel(List<object> Objects)
        {
            ML.Result result = new ML.Result();
            try
            {
                result.Objects = new List<object>();
                int i = 1;

                foreach (ML.Empresa empresa in Objects)
                {
                    ML.ExcelErrores error = new ML.ExcelErrores();
                    error.IdRegistro = i;

                    if (empresa.Nombre == "")
                    {
                        error.Message += "Ingrese nombre";
                    }
                    if (empresa.Telefono == "")
                    {
                        error.Message += "Ingrese Telefono";
                    }
                    if (empresa.Email == "")
                    {
                        error.Message += "Ingrese Email";
                    }
                    if (empresa.DireccionWeb == "")
                    {
                        error.Message += "Ingrese DireccionWeb";
                    }
              

                    if (error.Message != null)
                    {
                        result.Objects.Add(error);
                    }
                    i++;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return result;

        }
    }

}

