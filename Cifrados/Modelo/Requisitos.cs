using Microsoft.AspNetCore.Http;
using static Cifrados.Modelo.Valores;

namespace Cifrados.Modelo
{
    public class Requisitos : IRequestModel<string>
    {
        public IFormFile File { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
    }
}
