using Cifrados.Modelo;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Cifrados.Cifrados;
using System.IO;

namespace Cifrados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspiralController : ControllerBase
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
            else if (!int.TryParse(Tipos.Key, out int x) || Tipos.Key == null)
            {
                return BadRequest(new string[] { "El valor -Key- es inválido" });
            }
            if (Path.GetExtension(Tipos.File.Name) != ".txt")
            {
                return BadRequest(new string[] { "Extensión no válida" });
            }
            else
            {
                using (FileStream thisFile = new FileStream("Mis Cifrados/" + Tipos.File.Name, FileMode.OpenOrCreate))
                {
                    Espiral Espiral = new Espiral();
                    Espiral.TodoEspiral(thisFile, Tipos.Key, 1, Tipos.Name);
                }
            }
            return new string[] { "Satisfactorio" };
        }
    }
}