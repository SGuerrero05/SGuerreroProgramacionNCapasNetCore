using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Estado
    {

        public static ML.Result GetByIdPais(int IdPais)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.SGuerreroProgramacionNcapasContext context = new DL.SGuerreroProgramacionNcapasContext())
                {
                    var query = context.Estados.FromSqlRaw($"EstadoGetByIdPais{IdPais}").AsEnumerable().FirstOrDefault();
                    if (query != null)
                    {

                        ML.Estado estado = new ML.Estado();

                        estado.IdEstado = query.IdEstado;
                        estado.Nombre = query.Nombre;
                        estado.Pais = new ML.Pais();
                        estado.Pais.IdPais = query.IdPais.Value;

                        result.Object = estado;

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

