using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class DependienteTipo
    {
        public static ML.Result DependienteTipoGetAll()
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var query = context.DependienteTipos.FromSqlRaw($"DependienteTipoGetAll").ToList();
                    result.Objects = new List<object>();
                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.DependienteTipo dependienteTipo = new ML.DependienteTipo();
                            dependienteTipo.IdDependienteTipo = obj.IdDependienteTipo;
                            dependienteTipo.Nombre = obj.Nombre;

                            result.Objects.Add(dependienteTipo);
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
    }
}
