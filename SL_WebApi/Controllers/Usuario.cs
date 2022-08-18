using Microsoft.AspNetCore.Mvc;

namespace SL_WebApi.Controllers
{
    [ApiController]
    public class Usuario : Controller
    {
        [HttpPost]
        [Route("/api/Usuario/Add")]
        public IActionResult Add([FromBody]ML.Usuario usuario)
        {
            var result = BL.Usuario.UsuarioAdd(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }          
        }
        [HttpDelete]
        [Route("/api/Usuario/Delete")]
        public IActionResult Delete(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = IdUsuario;
            var result = BL.Usuario.UsuarioDelete(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut]
        [Route("/api/Usuario/Update")]
        public IActionResult Update([FromBody] ML.Usuario usuario)
        {
            var result = BL.Usuario.UsuarioUpdate(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("/api/Usuario/GetAll")]
        public IActionResult GetAll()

        {
            ML.Usuario usuarioBusquedaAbierta = new ML.Usuario();
            var result = BL.Usuario.UsuarioGetAll(usuarioBusquedaAbierta);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("/api/Usuario/GetById")]
        public IActionResult GetById(int IdUsuario )
        {
            var result = BL.Usuario.UsuarioGetById(IdUsuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
