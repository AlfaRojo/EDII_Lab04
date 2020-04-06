using System.Collections.Generic;
using System.IO;
using Cifrados.Modelo;
using Microsoft.AspNetCore.Mvc;
using Cifrados.Cifrados;

namespace Cifrados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CipherController : ControllerBase
    {
        [Route("ZigZag")]
        public ActionResult<IEnumerable<string>> PostZigZag([FromForm]Requisitos Tipos)//Comprobar mas errores posibles
        {
            if (Tipos.File == null)
            {
                return BadRequest(new string[] { "El valor -File- es inválido" });
            }
            else if (Path.GetExtension(Tipos.File.FileName) != ".txt")
            {
                return BadRequest(new string[] { "Extensión no válida" });
            }
            else if (Tipos.Niveles > 0)
            {
                return BadRequest(new string[] { "El valor -Niveles- es inválido" });
            }
            else
            {
                using (FileStream thisFile = new FileStream("Mis Cifrados/" + Tipos.File.FileName, FileMode.OpenOrCreate))
                {
                    ZigZag zigZag = new ZigZag();
                    zigZag.TodoZigZag(thisFile,"Cifrar", Tipos.Niveles);
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
            else if (Path.GetExtension(Tipos.File.FileName) != ".txt")
            {
                return BadRequest(new string[] { "Extensión no válida" });
            }
            else if (Tipos.Key == null || !(int.TryParse(Tipos.Key, out int Key)))
            {
                return BadRequest(new string[] { "El valor -Key- es inválido" });
            }
            else
            {
                using (FileStream thisFile = new FileStream("Mis Cifrados/" + Tipos.File.FileName, FileMode.OpenOrCreate))
                {
                    Cesar Cesar = new Cesar();
                    //Archivo-Llave-Desifrado
                    Cesar.TodoCesar(thisFile, Key, "Cifrado");
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
                using (FileStream thisFile = new FileStream("Mis Cifrados/" + Tipos.File.FileName, FileMode.OpenOrCreate))
                {
                    Espiral Espiral = new Espiral();
                    //Archivo-Cifrar-Ancho-Reloj
                    Espiral.TodoEspiral(thisFile, "Cifrar", Tipos.Ancho, Tipos.Reloj);
                }
            }
            return new string[] { "Cifrado " + Tipos.Name + " satisfactorio" };
        }
    }
}