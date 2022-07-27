using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Colonia
    {

        public static ML.Result GetByIdMunicipio(int IdColonia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var query = context.Colonia.FromSqlRaw($"ColoniaGetByIdMunicipio{IdColonia}").AsEnumerable().FirstOrDefault();
                    if (query != null)
                    {

                        ML.Colonia colonia = new ML.Colonia();

                        colonia.IdColonia = query.IdColonia;
                        colonia.Nombre = query.Nombre;
                        colonia.CodigoPostal = query.CodigoPostal;
                        colonia.Municipio = new ML.Municipio();
                        colonia.Municipio.IdMunicipio = query.IdMunicipio.Value;

                        result.Object = colonia;

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
