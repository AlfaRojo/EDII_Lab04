using System.IO;
using System.Text;

namespace Cifrados.Cifrados
{
    public class Cesar
    {
        public void TodoCesar(FileStream ArchivoImportado, int Key, string opcion)
        {
            var nombreArchivo = Path.GetFileNameWithoutExtension(ArchivoImportado.Name);
            ArchivoImportado.Close();
            using (var Lectura = new StreamReader(("Mis Cifrados/" + nombreArchivo + ".txt"), Encoding.ASCII, true))
            {
                var txtInicio = Lectura.ReadToEnd();
                var txtFinal = string.Empty;
                if (opcion == "Cifrado")
                {
                    txtFinal = CifrarCesar(txtInicio, Key);
                }
                else
                {
                    txtFinal = DesifrarCesar(txtInicio, Key);
                }
                using (var writeStream = new FileStream(("Mis Cifrados/" + opcion + "_" + nombreArchivo + ".txt"), FileMode.OpenOrCreate))
                {
                    using (var writer = new BinaryWriter(writeStream))
                    {
                        writer.Write(txtFinal);
                    }
                }
            }
        }
        public string CifrarCesar(string txtInicial, int Key)
        {
            int t;
            var Letras = txtInicial.Length;
            char[] ch = new char[Letras];
            var Resultante = string.Empty;
            for (int i = 0; i < Letras; i++)
            {
                t = (int)txtInicial[i];
                ch[i] = (char)(t + Key);
                Resultante = Resultante + ch[i];
            }
            return Resultante; 
        }
        public string DesifrarCesar(string txtInicial, int Key)
        {
            int t;
            var Letras = txtInicial.Length;
            char[] ch = new char[Letras];
            var Resultante = string.Empty;
            for (int i = 0; i < Letras; i++)
            {
                t = (int)txtInicial[i];
                ch[i] = (char)(t - Key);
                Resultante = Resultante + ch[i];
            }
            return Resultante;
        }
        #region Funcionales Lambda
        //private string Decrypt(string txtInicio, int key)
        //{
        //    return txtInicio.Select(c => IsLetter(c) ? Desencriptar(c, key) : c).Select(c => c.ToString()).Aggregate((a, b) => a + b);
        //}
        //private char Desencriptar(char c, int k)
        //{
        //    var limite = IsSmallLetter(c) ? 'a' : 'A';
        //    return (char)(c - k < limite ? c - k + 26 : c - k);
        //}
        //private static char EncriptarLetra(char c, int k)
        //{
        //    var limite = IsSmallLetter(c) ? 'z' : 'Z';
        //    return (char)(c + k > limite ? c + k - 26 : c + k);
        //}
        //private static int CryptoAnalize(string text)
        //{
        //    var MostFrecuentLetter = text
        //        .Where(IsLetter)
        //        .GroupBy(c => c, (key, values) => new { Letter = key, Count = values.Count() })
        //        .OrderByDescending(pair => pair.Count)
        //        .First().Letter;

        //    var possibleKey = MostFrecuentLetter - 'e';
        //    return possibleKey < 0 ? possibleKey + 26 : possibleKey;
        //}
        //private static string Encrypt(string txtInicio, int Key)
        //{
        //    return txtInicio.Select(c => IsLetter(c) ? EncriptarLetra(c, Key) : c).Select(c => c.ToString()).Aggregate((a, b) => a + b);
        //}
        //private static bool IsLetter(char c)
        //{
        //    return IsSmallLetter(c) || IsCapitalLetter(c);
        //}
        //private static bool IsSmallLetter(char c)
        //{
        //    return Enumerable.Range('a', 26).Contains(c);
        //}
        //private static bool IsCapitalLetter(char c)
        //{
        //    return Enumerable.Range('A', 26).Contains(c);
        //}
#endregion
    }
}
