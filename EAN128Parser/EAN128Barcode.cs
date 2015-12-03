using System.Collections.Generic;
using System.Linq;

namespace Drkstr.EAN128
{
    public class EAN128Barcode
    {
        public EAN128Barcode()
        {
            Elements = new List<ElementString>();
        }

        /// <summary>
        ///     Recupera tutti gli elementi correnti
        /// </summary>
        public List<ElementString> Elements { get; }

        /// <summary>
        ///     Aggiunge un barcode al corrente EAN128
        /// </summary>
        /// <param name="rawData">Il dat grezzo del barcode</param>
        /// <exception>
        ///     <cref>Drkstr.Ean128.EAN128Exception</cref>
        /// </exception>
        /// se si verifica un errore di parsing
        public void AddBarcode(byte[] rawData)
        {
            var t = new EAN128Tokenizer(rawData);
            AddToken(t);
        }

        /// <summary>
        ///     Aggiunge un barcode al corrente EAN128
        /// </summary>
        /// <param name="asciiData">Il barcode decodificato in ASCII</param>
        public void AddBarcode(char[] asciiData)
        {
            var t = new EAN128Tokenizer(asciiData);
            AddToken(t);
        }

        /// <summary>
        ///     Aggiunge un token al barcode se questo non è stato ancora aggiunto
        /// </summary>
        /// <param name="t">EAN128Tokenizer da inserire</param>
        public void AddToken(EAN128Tokenizer t)
        {
            while (t.HasMoreToken())
            {
                var nextToken = t.NextToken();
                var elements = (from e in Elements where e.Data.Equals(nextToken.Data) select e);

                if (!elements.Any())
                {
                    Elements.Add(nextToken);
                }
            }
        }

        /// <summary>
        ///     Stampa il barcode contenuto nel formato (AI1)Data1(AI2)Data2(...
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Elements.Aggregate(string.Empty, (current, es) => current + es.ToString());
        }
    }
}