using Cifrados.Modelo;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cifrados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspiralController : ControllerBase
    {
        [HttpPost]
        public ActionResult<IEnumerable<string>> Post([FromForm]Requisitos Tipos)
        {
            if (!(int.TryParse(Tipos.Key, out int x)))
            {

            }
            else
            {
                return new string[] { "El valor de -KEY- no puede ser un número" };
            }
            return new string[] { "Satisfactorio" };
        }
    }
}