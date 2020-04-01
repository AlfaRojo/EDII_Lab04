using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifrados.Cifrados
{
    public class Cesar
    {
        public void TodoCesar(FileStream ArchivoImportado, int Key)
        {
            var nombreArchivo = Path.GetFileNameWithoutExtension(ArchivoImportado.Name);
            ArchivoImportado.Close();
            using (var Lectura = new StreamReader(("Mis Cifrados/" + nombreArchivo + ".txt"), Encoding.ASCII, true))
            {
                var txtInicio = Lectura.ReadToEnd();
                Encriptado(txtInicio, Key);
            }
        }
        public static string Encriptado(string txtInicio, int Key)
        {
            return txtInicio.Select(c => IsLetter(c) ? EncryptyLetter(c, Key) : c).Select(c => c.ToString()).Aggregate((a, b) => a + b);
        }
        public static char EncryptyLetter(char c, int k)
        {
            int Limit = IsSmallLetter(c) ? 'z' : 'Z';
            return (char)(c + k > Limit ? c + k - 26 : c + k);
        }
        public static bool IsLetter(char c)
        {
            return IsSmallLetter(c) || IsCapitalLetter(c);
        }
        public static bool IsSmallLetter(char c)
        {
            return Enumerable.Range('a', 26).Contains(c);
        }
        public static bool IsCapitalLetter(char c)
        {
            return Enumerable.Range('A', 26).Contains(c);
        }
    }
}
