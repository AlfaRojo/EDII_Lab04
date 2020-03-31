using System;
using System.Collections.Generic;
using System.IO;

namespace Cifrados.Cifrados
{
    public class Espiral
    {
        public void TodoEspiral(FileStream ArchivoImportado, string opcion, int ancho, string reloj)//Modificar para Cifrado/Desifrado
        {
            var cifradoOp = true;
            if (opcion == "Descifrar")
            {
                cifradoOp = false;
            }
            var direccion = true;
            if (reloj != "Abajo")
            {
                direccion = false;
            }
            var extensionNuevoArchivo = string.Empty;
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
                if (cifradoOp && extrencion == ".txt")
                {
                    extensionNuevoArchivo = ".cif";
                }
                if (!cifradoOp && extrencion == ".cif")
                {
                    extensionNuevoArchivo = ".txt";
                }
                if (extensionNuevoArchivo != null)
                {
                    var txtResultado = new byte[1];
                    if (cifradoOp)
                    {
                        var txtCifrado = CifradoEspiral(ancho, direccion, archivoByte);
                        txtResultado = new byte[txtCifrado.Length];
                        txtResultado = txtCifrado;
                    }
                    else
                    {
                        var txtDesifrado = DescifradoEspiral(ancho, direccion, archivoByte);
                        txtResultado = new byte[txtDesifrado.Length];
                        txtResultado = txtDesifrado;
                    }
                    using (var writeStream = new FileStream(("Mis Cifrados/" + nombreArchivo + extensionNuevoArchivo), FileMode.OpenOrCreate))
                    {
                        using (var writer = new BinaryWriter(writeStream))
                        {
                            writer.Write(txtResultado);
                        }
                    }
                }
            }
        }
        public static byte[] CifradoEspiral(int Ancho, bool Abajo, byte[] TextoEncripcion)
        {
            var DivisionAncho = Math.Ceiling(Convert.ToDecimal(TextoEncripcion.Length) / Convert.ToDecimal(Ancho));
            var Altura = Convert.ToInt32(DivisionAncho);
            var DCircularMatriz = new byte[Ancho, Altura];

            var PosicionTexto = 0;
            for (int i = 0; i < Altura; i++)
            {
                for (int j = 0; j < Ancho; j++)
                {
                    if (PosicionTexto < TextoEncripcion.Length)
                    {
                        DCircularMatriz[j, i] = TextoEncripcion[PosicionTexto];
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
            if (Abajo)
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
        public static byte[] DescifradoEspiral(int Ancho, bool Abajo, byte[] TextoEncripcion)
        {
            var DivisionAncho = Math.Ceiling(Convert.ToDecimal(TextoEncripcion.Length) / Convert.ToDecimal(Ancho));
            var Altura = Convert.ToInt32(DivisionAncho);
            var DCircularMatriz = new byte[Ancho, Altura];
            var PosicionTexto = 0;
            var AnchoAux = Ancho;
            var AltoAux = Altura;
            var CantidadIteraciones = Ancho < Altura ? Ancho / 2 : Altura / 2;
            var contador = 0;
            if (TextoEncripcion.Length < (Ancho * Altura))
            {
                for (int i = TextoEncripcion.Length; i <= Ancho * Altura; i++)
                {
                    TextoEncripcion[i] = 0;
                }
            }
            if (Abajo)
            {
                for (int i = 0; i < CantidadIteraciones; i++)
                {
                    for (int j = i; j < AltoAux + i; j++)
                    {
                        DCircularMatriz[i, j] = TextoEncripcion[PosicionTexto];
                        PosicionTexto++;
                    }
                    for (int j = i + 1; j < AnchoAux + i; j++)
                    {
                        DCircularMatriz[j, AltoAux - 1 + i] = TextoEncripcion[PosicionTexto];
                        PosicionTexto++;
                    }
                    for (int j = AltoAux - 2 + i; j >= i; j--)
                    {
                        DCircularMatriz[AnchoAux - 1 + i, j] = TextoEncripcion[PosicionTexto];
                        PosicionTexto++;
                    }
                    for (int j = AnchoAux - 2 + i; j > i; j--)
                    {
                        DCircularMatriz[j, i] = TextoEncripcion[PosicionTexto];
                        PosicionTexto++;
                    }
                    AnchoAux = AnchoAux - 2;
                    AltoAux = AltoAux - 2;
                }
                if (AnchoAux == 1)
                {
                    for (int i = CantidadIteraciones; i < AltoAux + CantidadIteraciones; i++)
                    {
                        DCircularMatriz[CantidadIteraciones, i] = TextoEncripcion[PosicionTexto];
                        PosicionTexto++;
                    }
                    AnchoAux = 0;
                    AltoAux = 0;
                }
                else if (AltoAux == 1)
                {
                    for (int i = CantidadIteraciones; i < AnchoAux + CantidadIteraciones; i++)
                    {
                        DCircularMatriz[i, CantidadIteraciones] = TextoEncripcion[PosicionTexto];
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
                        DCircularMatriz[j, i] = TextoEncripcion[PosicionTexto];
                        PosicionTexto++;
                    }
                    for (int j = i + 1; j < AltoAux + i; j++)
                    {
                        DCircularMatriz[AnchoAux - 1 + i, j] = TextoEncripcion[PosicionTexto];
                        PosicionTexto++;
                    }
                    for (int j = AnchoAux - 2 + i; j >= i; j--)
                    {
                        DCircularMatriz[j, AltoAux - 1 + i] = TextoEncripcion[PosicionTexto];
                        PosicionTexto++;
                    }
                    for (int j = AltoAux - 2 + i; j > i; j--)
                    {
                        DCircularMatriz[i, j] = TextoEncripcion[PosicionTexto];
                        PosicionTexto++;
                    }
                    AnchoAux = AnchoAux - 2;
                    AltoAux = AltoAux - 2;
                }
                if (AnchoAux == 1)
                {
                    for (int i = CantidadIteraciones; i < AltoAux + CantidadIteraciones; i++)
                    {
                        DCircularMatriz[CantidadIteraciones, i] = TextoEncripcion[PosicionTexto];
                        PosicionTexto++;
                    }
                    AnchoAux = 0;
                    AltoAux = 0;
                }
                else if (AltoAux == 1)
                {
                    for (int i = CantidadIteraciones; i < AnchoAux + CantidadIteraciones; i++)
                    {
                        DCircularMatriz[i, CantidadIteraciones] = TextoEncripcion[PosicionTexto];
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
