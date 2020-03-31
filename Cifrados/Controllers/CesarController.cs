using Cifrados.Modelo;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace Cifrados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CesarController : ControllerBase
    {
        [HttpPost]
        public ActionResult<IEnumerable<string>> Post([FromForm]Requisitos Tipos)
        {
            if (Tipos.File == null)
            {
                return BadRequest(new string[] { "El valor -File- es inválido" });
            }
            else if (Tipos.Name == null)
            {
                return BadRequest(new string[] { "El valor -File- es inválido" });
            }
            if (Path.GetExtension(Tipos.File.Name) != ".txt")
            {
                return BadRequest(new string[] { "Extensión no válida" });
            }
            if (!(int.TryParse(Tipos.Key, out int x)))
            {
                using (FileStream thisFile = new FileStream("Mis Cifrados/" + Tipos.File.Name, FileMode.OpenOrCreate))
                {
                    //Mandar parametros a clase/métodos
                }
            }
            else if (Tipos.Key == null)
            {
                return BadRequest(new string[] { "El valor -KEY- no puede ser un número" });
            }
            return new string[] { "Satisfactorio" };
        }
    }
}