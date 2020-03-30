using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cifrados.Modelo
{
	public class ZigZag
	{
		Dictionary<int, Caracter> DicNiveles = new Dictionary<int, Caracter>();
		public void CifrarLectura (string direccion, int niveles)
		{
			var ContadorNiveles = 0;
			while (ContadorNiveles < niveles)
			{
				var ListaInicial = new List<byte>();
				DicNiveles.Add(ContadorNiveles + 1, new Caracter
				{
					ListaCaracter = ListaInicial
				});
				ContadorNiveles++;
			}

			var bufferLength = 50;
			var posicionNivel = 1;
			var elevador = false;

			// aqui necesito un bufer que mande a llamar al archivo guardado en la API
		}
	}
}
