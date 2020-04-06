using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace Cifrados.Modelo
{
	public class ZigZag
	{
		static Dictionary<int, Caracter> DicNiveles = new Dictionary<int, Caracter>();
		static List<byte> BytesExistentes = new List<byte>();

		public void TodoZigZag(FileStream ArchivoImportado, string opcion, int niveles)
		{
			var nombreArchivo = Path.GetFileNameWithoutExtension(ArchivoImportado.Name);
			var extrencion = Path.GetExtension(ArchivoImportado.Name);
			if (ArchivoImportado != null)
			{
				var archivoByte = new byte[ArchivoImportado.Length];
				var i = 0;
				using (var lectura = new BinaryReader(ArchivoImportado))
				{
					while (lectura.BaseStream.Position != lectura.BaseStream.Length)
					{
						archivoByte[i] = lectura.ReadByte();
						i++;
					}
				}
				var txtResultado = new byte[1];
				if (opcion == "Cifrar")
				{

					var txtDesifrado = CifrarLectura(archivoByte, niveles);
					txtResultado = new byte[txtDesifrado.Length];
					txtResultado = txtDesifrado;
				}
				else
				{
					var txtCifrado = DescifrarLectura(archivoByte, niveles);
					txtResultado = new byte[txtCifrado.Length];
					txtResultado = txtCifrado;
				}
				using (var writeStream = new FileStream(("Mis Cifrados/CIFRADO_" + opcion+"_" + nombreArchivo + ".txt"), FileMode.OpenOrCreate))
				{
					using (var writer = new BinaryWriter(writeStream))
					{
						writer.Write(txtResultado);
					}
				}
			}
		}
		public static byte[] CifrarLectura (byte[] texto, int niveles)
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
			var buffer = new byte[bufferLength];

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
			return null;
		}

		public static byte[] DescifrarLectura(byte[] texto, int niveles)
		{
			int contadorNiveles = 0;
			while (contadorNiveles < niveles)
			{
				var ListaInicial = new List<byte>();
				DicNiveles.Add(contadorNiveles + 1, new Caracter
				{
					ListaCaracter = ListaInicial
				});
				contadorNiveles++;
			}
			int cantMinima = (4 * (niveles - 1)) + 1;
			int uveNueva = (niveles * 2) - 2;
			int p = (niveles - 2) * 2;
			int longitudRelativa = 0;

			if (texto.Length <= cantMinima)
			{
				longitudRelativa = cantMinima;
			}
			else
			{
				int elementosExtra = texto.Length - cantMinima;
				int repetirUve = elementosExtra / uveNueva;
				if (elementosExtra % uveNueva != 0)
				{
					repetirUve++;
				}
				longitudRelativa = cantMinima + (repetirUve * uveNueva);
			}
			int picoSuperior = (longitudRelativa + 1 + p) / (2 + p);
			int valorIntermedio = (2 * (picoSuperior - 1));
			int picoInferior = picoSuperior - 1;
			int nivelIntermedio = niveles - 2;

			int auxiliarIntermedios = nivelIntermedio;
			int conteoIntermedios = valorIntermedio;
			int bufferLength = 50;
			var buffer = new byte[bufferLength];
			// aqui necesito un bufer que mande a llamar al archivo guardado en la API
			// a continuacion se coloca lo que ira dentro del foreach del buffer
			byte item = 32; // se coloco esta variable provisionalmente, al momento de poner el buffer se eliminará
			if (picoSuperior > 0)
			{
				DicNiveles.ElementAt(0).Value.ListaCaracter.Add(item);
				picoSuperior--;
			}
			else if (picoSuperior == 0 && auxiliarIntermedios > 0)
			{
				DicNiveles.ElementAt((nivelIntermedio - auxiliarIntermedios) + 1).Value.ListaCaracter.Add(item);
				conteoIntermedios--;
				if (conteoIntermedios == 0 && nivelIntermedio > 0)
				{
					auxiliarIntermedios--;
					conteoIntermedios = valorIntermedio;
				}
			}
			else if (picoSuperior == 0 && auxiliarIntermedios == 0)
			{
				DicNiveles.ElementAt(niveles - 1).Value.ListaCaracter.Add(item);
			}
			return null;
		}
	} 
}
