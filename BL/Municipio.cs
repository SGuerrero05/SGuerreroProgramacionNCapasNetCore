using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Municipio
    {

        public static ML.Result GetByIdEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var query = context.Municipios.FromSqlRaw($"MunicipioGetByIdEstado{IdEstado}").AsEnumerable().FirstOrDefault();
                    if (query != null)
                    {

                        ML.Municipio municipio = new ML.Municipio();

                        municipio.IdMunicipio = query.IdMunicipio;
                        municipio.Nombre = query.Nombre;
                        municipio.Estado = new ML.Estado();
                        municipio.Estado.IdEstado = query.IdEstado.Value;

                        result.Object = municipio;

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