using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cifrados.Modelo;

namespace Cifrados.Modelo
{
	public class ZigZag
	{
		Dictionary<int, Caracter> DicNiveles = new Dictionary<int, Caracter>();
		List<byte> BytesExistentes = new List<byte>();
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
			// a continuacion se coloca lo que ira dentro del foreach del buffer
			byte item = 32; // se coloco esta variable provisionalmente, al momento de poner el buffer se eliminará
			if (!BytesExistentes.Contains(item))
			{
				BytesExistentes.Add(item);
			}
			DicNiveles.ElementAt(posicionNivel - 1).Value.ListaCaracter.Add(item);
			if (!elevador)
			{
				if (posicionNivel == niveles)
				{
					posicionNivel--;
					elevador = true;
				}
				else
				{
					posicionNivel++;
				}
			}
			else
			{
				if (posicionNivel == 1)
				{
					posicionNivel++;
					elevador = false;
				}
				else
				{
					posicionNivel--;
				}
			}
			// aqui termina el segmento que va dentro del foreach

			// esta seccion es cuando faltan caracteres y se rellenan con un caracter aleatorio
			byte caracterRelleno = 36;
			
			if (!BytesExistentes.Contains(36))
			{
				caracterRelleno = 254;
				while(BytesExistentes.Contains(caracterRelleno))
				{
					caracterRelleno--;
				}

			}
			while (posicionNivel - 1 != 1)
			{
				DicNiveles.ElementAt(posicionNivel - 1).Value.ListaCaracter.Add(caracterRelleno);
				if (!elevador)
				{
					if (posicionNivel == niveles)
					{
						posicionNivel--;
						elevador = true;
					}
					else
					{
						posicionNivel++;
					}
				}
				else
				{
					if (posicionNivel == 1)
					{
						posicionNivel++;
						elevador = false;
					}
					else
					{
						posicionNivel--;
					}
				}

			}
		}
	}
}
