using System;
using System.IO;

namespace Cifrados.Cifrados
{
    public class Espiral
    {
        public void TodoEspiral(FileStream ArchivoImportado, string opcion, int ancho, string reloj)//Modificar para Cifrado/Desifrado
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
                    var txtDesifrado = CifrarEspiral(ancho, reloj, archivoByte);
                    txtResultado = new byte[txtDesifrado.Length];
                    txtResultado = txtDesifrado;
                }
                else
                {
                    var txtCifrado = DesifrarEspiral(ancho, reloj, archivoByte);
                    txtResultado = new byte[txtCifrado.Length];
                    txtResultado = txtCifrado;
                }
                using (var writeStream = new FileStream(("Mis Cifrados/CIFRADO_" + nombreArchivo + ".txt"), FileMode.OpenOrCreate))
                {
                    using (var writer = new BinaryWriter(writeStream))
                    {
                        writer.Write(txtResultado);
                    }
                }
            }
        }
        public static byte[] CifrarEspiral(int Ancho, string Abajo, byte[] txtEncript)
        {
            var DivisionAncho = Math.Ceiling(Convert.ToDecimal(txtEncript.Length) / Convert.ToDecimal(Ancho));
            var Altura = Convert.ToInt32(DivisionAncho);
            var DCircularMatriz = new byte[Ancho, Altura];

            var PosicionTexto = 0;
            for (int i = 0; i < Altura; i++)
            {
                for (int j = 0; j < Ancho; j++)
                {
                    if (PosicionTexto < txtEncript.Length)
                    {
                        DCircularMatriz[j, i] = txtEncript[PosicionTexto];
                        PosicionTexto++;
                    }
                    else
                    {
                        DCircularMatriz[j, i] = 0;
                    }
                }
            }
            var REGRESA = new byte[Ancho * Altura];
            var CantidadIteraciones = Ancho < Altura ? Ancho / 2 : Altura / 2;
            var AnchoAux = Ancho;
            var AltoAux = Altura;
            var contador = 0;
            if (Abajo == "Abajo")
            {
                for (int i = 0; i < CantidadIteraciones; i++)
                {
                    for (int j = i; j < AltoAux + i; j++)
                    {
                        REGRESA[contador] = DCircularMatriz[i, j];
                        contador++;
                    }
                    for (int j = i + 1; j < AnchoAux + i; j++)
                    {
                        REGRESA[contador] = DCircularMatriz[j, AltoAux - 1 + i];
                        contador++;
                    }
                    for (int j = AltoAux - 2 + i; j >= i; j--)
                    {
                        REGRESA[contador] = DCircularMatriz[AnchoAux - 1 + i, j];
                        contador++;
                    }
                    for (int j = AnchoAux - 2 + i; j > i; j--)
                    {
                        REGRESA[contador] = DCircularMatriz[j, i];
                        contador++;
                    }
                    AnchoAux = AnchoAux - 2;
                    AltoAux = AltoAux - 2;
                }
                if (AnchoAux == 1)
                {
                    for (int i = CantidadIteraciones; i < AltoAux + CantidadIteraciones; i++)
                    {
                        REGRESA[contador] = DCircularMatriz[CantidadIteraciones, i];
                        contador++;
                    }
                    AnchoAux = 0;
                    AltoAux = 0;
                }
                else if (AltoAux == 1)
                {
                    for (int i = CantidadIteraciones; i < AnchoAux + CantidadIteraciones; i++)
                    {
                        REGRESA[contador] = DCircularMatriz[i, CantidadIteraciones];
                        contador++;
                    }
                    AnchoAux = 0;
                    AltoAux = 0;
                }
            }
            else
            {
                for (int i = 0; i < CantidadIteraciones; i++)
                {
                    for (int j = i; j < AnchoAux + i; j++)
                    {
                        REGRESA[contador] = DCircularMatriz[j, i];
                        contador++;
                    }
                    for (int j = i + 1; j < AltoAux + i; j++)
                    {
                        REGRESA[contador] = DCircularMatriz[AnchoAux - 1 + i, j];
                        contador++;
                    }
                    for (int j = AnchoAux - 2 + i; j >= i; j--)
                    {
                        REGRESA[contador] = DCircularMatriz[j, AltoAux - 1 + i];
                        contador++;
                    }
                    for (int j = AltoAux - 2 + i; j > i; j--)
                    {
                        REGRESA[contador] = DCircularMatriz[i, j];
                        contador++;
                    }
                    AnchoAux = AnchoAux - 2;
                    AltoAux = AltoAux - 2;
                }
                if (AnchoAux == 1)
                {
                    for (int i = CantidadIteraciones; i < AltoAux + CantidadIteraciones; i++)
                    {
                        REGRESA[contador] = DCircularMatriz[CantidadIteraciones, i];
                        contador++;
                    }
                    AnchoAux = 0;
                    AltoAux = 0;
                }
                else if (AltoAux == 1)
                {
                    for (int i = CantidadIteraciones; i < AnchoAux + CantidadIteraciones; i++)
                    {
                        REGRESA[contador] = DCircularMatriz[i, CantidadIteraciones];
                        contador++;
                    }
                    AnchoAux = 0;
                    AltoAux = 0;
                }
            }
            return REGRESA;
        }
        public static byte[] DesifrarEspiral(int Ancho, string Abajo, byte[] txtEncript)
        {
            var DivisionAncho = Math.Ceiling(Convert.ToDecimal(txtEncript.Length) / Convert.ToDecimal(Ancho));
            var Altura = Convert.ToInt32(DivisionAncho);
            var DCircularMatriz = new byte[Ancho, Altura];
            var PosicionTexto = 0;
            var AnchoAux = Ancho;
            var AltoAux = Altura;
            var CantidadIteraciones = Ancho < Altura ? Ancho / 2 : Altura / 2;
            var contador = 0;
            if (txtEncript.Length < (Ancho * Altura))
            {
                for (int i = txtEncript.Length; i <= Ancho * Altura; i++)
                {
                    txtEncript[i] = 0;
                }
            }
            if (Abajo == "Abajo")
            {
                for (int i = 0; i < CantidadIteraciones; i++)
                {
                    for (int j = i; j < AltoAux + i; j++)
                    {
                        DCircularMatriz[i, j] = txtEncript[PosicionTexto];
                        PosicionTexto++;
                    }
                    for (int j = i + 1; j < AnchoAux + i; j++)
                    {
                        DCircularMatriz[j, AltoAux - 1 + i] = txtEncript[PosicionTexto];
                        PosicionTexto++;
                    }
                    for (int j = AltoAux - 2 + i; j >= i; j--)
                    {
                        DCircularMatriz[AnchoAux - 1 + i, j] = txtEncript[PosicionTexto];
                        PosicionTexto++;
                    }
                    for (int j = AnchoAux - 2 + i; j > i; j--)
                    {
                        DCircularMatriz[j, i] = txtEncript[PosicionTexto];
                        PosicionTexto++;
                    }
                    AnchoAux = AnchoAux - 2;
                    AltoAux = AltoAux - 2;
                }
                if (AnchoAux == 1)
                {
                    for (int i = CantidadIteraciones; i < AltoAux + CantidadIteraciones; i++)
                    {
                        DCircularMatriz[CantidadIteraciones, i] = txtEncript[PosicionTexto];
                        PosicionTexto++;
                    }
                    AnchoAux = 0;
                    AltoAux = 0;
                }
                else if (AltoAux == 1)
                {
                    for (int i = CantidadIteraciones; i < AnchoAux + CantidadIteraciones; i++)
                    {
                        DCircularMatriz[i, CantidadIteraciones] = txtEncript[PosicionTexto];
                        PosicionTexto++;
                    }
                    AnchoAux = 0;
                    AltoAux = 0;
                }
            }
            else
            {
                for (int i = 0; i < CantidadIteraciones; i++)
                {
                    for (int j = i; j < AnchoAux + i; j++)
                    {
                        DCircularMatriz[j, i] = txtEncript[PosicionTexto];
                        PosicionTexto++;
                    }
                    for (int j = i + 1; j < AltoAux + i; j++)
                    {
                        DCircularMatriz[AnchoAux - 1 + i, j] = txtEncript[PosicionTexto];
                        PosicionTexto++;
                    }
                    for (int j = AnchoAux - 2 + i; j >= i; j--)
                    {
                        DCircularMatriz[j, AltoAux - 1 + i] = txtEncript[PosicionTexto];
                        PosicionTexto++;
                    }
                    for (int j = AltoAux - 2 + i; j > i; j--)
                    {
                        DCircularMatriz[i, j] = txtEncript[PosicionTexto];
                        PosicionTexto++;
                    }
                    AnchoAux = AnchoAux - 2;
                    AltoAux = AltoAux - 2;
                }
                if (AnchoAux == 1)
                {
                    for (int i = CantidadIteraciones; i < AltoAux + CantidadIteraciones; i++)
                    {
                        DCircularMatriz[CantidadIteraciones, i] = txtEncript[PosicionTexto];
                        PosicionTexto++;
                    }
                    AnchoAux = 0;
                    AltoAux = 0;
                }
                else if (AltoAux == 1)
                {
                    for (int i = CantidadIteraciones; i < AnchoAux + CantidadIteraciones; i++)
                    {
                        DCircularMatriz[i, CantidadIteraciones] = txtEncript[PosicionTexto];
                        PosicionTexto++;
                    }
                    AnchoAux = 0;
                    AltoAux = 0;
                }
            }

            var REGRESA = new byte[Ancho * Altura];
            for (int i = 0; i < Altura; i++)
            {
                for (int j = 0; j < Ancho; j++)
                {
                    REGRESA[contador] = DCircularMatriz[j, i];
                    contador++;
                }
            }
            return REGRESA;
        }
    }
}
