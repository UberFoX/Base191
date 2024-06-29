using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class UBase191
    {
        #region Static Variables
        private static readonly Char[] EncodeTable = {
            ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ':', ';', '<', '=', '>', '?',
            '@', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
            'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '[', '\\', ']', '^', '_',
            '`', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o',
            'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '{', '|', '}', '~', ' ',
            '¡', '¢', '£', '¤', '¥', '¦', '§', '¨', '©', 'ª', '«', '¬', '­', '®', '¯', '°',
            '±', '²', '³', '´', 'µ', '¶', '·', '¸', '¹', 'º', '»', '¼', '½', '¾', '¿', 'À',
            'Á', 'Â', 'Ã', 'Ä', 'Å', 'Æ', 'Ç', 'È', 'É', 'Ê', 'Ë', 'Ì', 'Í', 'Î', 'Ï', 'Ð',
            'Ñ', 'Ò', 'Ó', 'Ô', 'Õ', 'Ö', '×', 'Ø', 'Ù', 'Ú', 'Û', 'Ü', 'Ý', 'Þ', 'ß', 'à',
            'á', 'â', 'ã', 'ä', 'å', 'æ', 'ç', 'è', 'é', 'ê', 'ë', 'ì', 'í', 'î', 'ï', 'ð',
            'ñ', 'ò', 'ó', 'ô', 'õ', 'ö', '÷', 'ø', 'ù', 'ú', 'û', 'ü', 'ý', 'þ', 'ÿ'
        };
        private static Dictionary<Byte, Int32> _decodeTable;
        #endregion
        #region Static Constructor
        static UBase191()
        {
            InitDecodeTable();
        }
        #endregion
        #region InitDecodeTable
        private static void InitDecodeTable()
        {
            _decodeTable = new Dictionary<Byte, Int32>();
            for (var i = 0; i < Byte.MaxValue; i++)
                _decodeTable[(Byte)i] = -1;
            for (var i = 0; i < EncodeTable.Length; i++)
                _decodeTable[(Byte)EncodeTable[i]] = i;
        }
        #endregion
        #region Encode
        public static String Encode(Byte[] input)
        {
            var output = new StringBuilder();
            var buffer = 0;
            var bitsInBuffer = 0;
            foreach (var b in input)
            {
                buffer |= b << bitsInBuffer;
                bitsInBuffer += 8;
                while (bitsInBuffer >= 15)
                {
                    var value = buffer & 0x7FFF; // take 15 bits
                    output.Append(EncodeTable[value % 191]);
                    output.Append(EncodeTable[value / 191]);
                    buffer >>= 15;
                    bitsInBuffer -= 15;
                }
            }
            // handle remaining bits
            if (bitsInBuffer > 0)
            {
                var value = buffer & 0x7FFF;
                output.Append(EncodeTable[value % 191]);
                output.Append(EncodeTable[value / 191]);
            }
            return output.ToString();
        }
        #endregion
        #region Decode
        public static Byte[] Decode(String input)
        {
            var output = new List<Byte>();
            var buffer = 0;
            var bitsInBuffer = 0;
            for (var i = 0; i < input.Length; i += 2)
            {
                if (i + 1 >= input.Length)
                    break;
                var value = _decodeTable[(Byte)input[i]] + 191 * _decodeTable[(Byte)input[i + 1]];
                buffer |= value << bitsInBuffer;
                bitsInBuffer += 15;
                while (bitsInBuffer >= 8)
                {
                    output.Add((Byte)(buffer & 0xFF));
                    buffer >>= 8;
                    bitsInBuffer -= 8;
                }
            }
            // handle remaining bits
            if (bitsInBuffer > 0 && buffer != 0)
                output.Add((Byte)buffer);
            return output.ToArray();
        }
        #endregion
        #region EncodeString
        public static String EncodeString(String input, Encoding encoding = null)
        {
            if (encoding != null)
            {
                var bytes = encoding.GetBytes(input);
                return Encode(bytes);
            }
            else
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                return Encode(bytes);
            }
        }
        #endregion
        #region DecodeString
        public static String DecodeString(String input, Encoding encoding = null)
        {
            if (encoding != null)
            {
                var bytes = Decode(input);
                return encoding.GetString(bytes);
            }
            else
            {
                var bytes = Decode(input);
                return Encoding.UTF8.GetString(bytes);
            }
        }
        #endregion
    }
}
