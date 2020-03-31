using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cifrados.Cifrados;
using Cifrados.Modelo;

namespace Cifrados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DecipherController : ControllerBase
    {
        [Route("ZigZag")]
        public ActionResult<IEnumerable<string>> PostZigZag([FromForm]Requisitos Tipos)//Comprobar mas errores posibles
        {
            if (Tipos.File == null)
            {
                return BadRequest(new string[] { "El valor -File- es inválido" });
            }
            else if (Path.GetExtension(Tipos.File.Name) != ".txt")
            {
                return BadRequest(new string[] { "Extensión no válida" });
            }
            else if (Tipos.Key == null)
            {
                return BadRequest(new string[] { "El valor -Key- es inválido" });
            }
            else
            {
                using (FileStream thisFile = new FileStream("Mis Cifrados/" + Tipos.File.Name, FileMode.OpenOrCreate))
                {
                    //Mandar archivo -thisFile- y parametros de -Tipos- necesarios
                }
            }
            return new string[] { "Cifrado " + Tipos.Name + " satisfactorio" };
        }
        [Route("Cesar")]
        public ActionResult<IEnumerable<string>> PostCesar([FromForm]Requisitos Tipos)//Comprobar mas errores posibles
        {
            if (Tipos.File == null)
            {
                return BadRequest(new string[] { "El valor -File- es inválido" });
            }
            else if (Path.GetExtension(Tipos.File.Name) != ".txt")
            {
                return BadRequest(new string[] { "Extensión no válida" });
            }
            else if (Tipos.Key == null)
            {
                return BadRequest(new string[] { "El valor -Key- es inválido" });
            }
            else
            {
                using (FileStream thisFile = new FileStream("Mis Cifrados/" + Tipos.File.Name, FileMode.OpenOrCreate))
                {
                    //Mandar archivo -thisFile- y parametros de -Tipos- necesarios
                }
            }
            return new string[] { "Cifrado " + Tipos.Name + " satisfactorio" };
        }
        [Route("Espiral")]
        public ActionResult<IEnumerable<string>> PostEspiral([FromForm]Requisitos Tipos)
        {
            if (Tipos.File == null)
            {
                return BadRequest(new string[] { "El valor -File- es inválido" });
            }
            else if (Path.GetExtension(Tipos.File.Name) != ".txt")
            {
                return BadRequest(new string[] { "Extensión no válida" });
            }
            else if (Tipos.Ancho > 0)
            {
                return BadRequest(new string[] { "El valor -Ancho- es inválido" });
            }
            else if (!(Tipos.Reloj == "Abajo"))
            {
                return BadRequest(new string[] { "El valor -Reloj- es inválido" });
            }
            else
            {
                using (FileStream thisFile = new FileStream("Mis Cifrados/" + Tipos.File.Name, FileMode.OpenOrCreate))
                {
                    Espiral Espiral = new Espiral();
                    //Archivo-Desifrar-Ancho-Reloj
                    Espiral.TodoEspiral(thisFile, "Desifrar", Tipos.Ancho, Tipos.Reloj);
                }
            }
            return new string[] { "Cifrado " + Tipos.Name + " satisfactorio" };
        }
    }
}