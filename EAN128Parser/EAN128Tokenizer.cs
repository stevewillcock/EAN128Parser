using System;
using System.Collections.Generic;
using System.Text;

namespace Drkstr.EAN128
{
    public class EAN128Tokenizer
    {
        public static Dictionary<string, ElementStringFormat> Formats = new Dictionary<string, ElementStringFormat>
        {
            {"00", ElementStringFormat.GetFormat("00", CharType.Numeric, 18, false, "SSCC")},
            {"01", ElementStringFormat.GetFormat("01", CharType.Numeric, 14, false, "GTIN")},
            { "02", ElementStringFormat.GetFormat("02", CharType.Numeric, 14, false, "CONTENT")},
            { "10", ElementStringFormat.GetFormat("10", CharType.Alphanumeric, 20, true, "BATCH/LOT")},
            { "11", ElementStringFormat.GetFormat("11", CharType.Alphanumeric, 6, false, "PROD DATE")},
            { "12", ElementStringFormat.GetFormat("12", CharType.Numeric, 6, false, "DUE DATE")},
            { "13", ElementStringFormat.GetFormat("13", CharType.Numeric, 6, false, "PACK DATE")},
            { "15", ElementStringFormat.GetFormat("15", CharType.Numeric, 6, false, "BEST BEFORE or SELL BY")},
            { "17", ElementStringFormat.GetFormat("17", CharType.Numeric, 6, false, "USE BY OR EXPIRY")},
            { "20", ElementStringFormat.GetFormat("20", CharType.Numeric, 2, false, "VARIANT")},
            { "21", ElementStringFormat.GetFormat("21", CharType.Alphanumeric, 20, true, "SERIAL")},
            { "22", ElementStringFormat.GetFormat("22", CharType.Alphanumeric, 29, true, "QTY/DATE/BATCH")},
            { "240", ElementStringFormat.GetFormat("240", CharType.Alphanumeric, 30, true, "ADDITIONAL ID")},
            { "241", ElementStringFormat.GetFormat("241", CharType.Alphanumeric, 30, true, "CUST. PART NO.")},
            { "242", ElementStringFormat.GetFormat("242", CharType.Numeric, 6, true, "MTO VARIANT")},
            { "250", ElementStringFormat.GetFormat("250", CharType.Alphanumeric, 30, true, "SECONDARY SERIAL")},
            { "251", ElementStringFormat.GetFormat("251", CharType.Alphanumeric, 30, true, "REF. TO SOURCE")},
            { "253", ElementStringFormat.GetFormat("253", CharType.Alphanumeric, 30, true, "DOC. ID")},
            { "254", ElementStringFormat.GetFormat("254", CharType.Alphanumeric, 20, true, "GLN EXTENSION")},
            { "30", ElementStringFormat.GetFormat("30", CharType.Numeric, 8, true, "VAR. COUNT")},
            { "310", ElementStringFormat.GetFormat("310", CharType.Numeric, 6, false, "NET WEIGHT (kg)")},
            { "311", ElementStringFormat.GetFormat("311", CharType.Numeric, 6, false, "LENGTH (m)")},
            { "312", ElementStringFormat.GetFormat("312", CharType.Numeric, 6, false, "WIDTH (m)")},
            { "313", ElementStringFormat.GetFormat("313", CharType.Numeric, 6, false, "HEIGHT (m)")},
            { "314", ElementStringFormat.GetFormat("314", CharType.Numeric, 6, false, "AREA (m2)")},
            { "315", ElementStringFormat.GetFormat("315", CharType.Numeric, 6, false, "NET VOLUME (l)")},
            { "316", ElementStringFormat.GetFormat("316", CharType.Numeric, 6, false, "NET VOLUME (m3)")},
            { "320", ElementStringFormat.GetFormat("320", CharType.Numeric, 6, false, "NET WEIGHT (lb)")},
            { "321", ElementStringFormat.GetFormat("321", CharType.Numeric, 6, false, "LENGTH (i)")},
            { "322", ElementStringFormat.GetFormat("322", CharType.Numeric, 6, false, "LENGTH (f)")},
            { "323", ElementStringFormat.GetFormat("323", CharType.Numeric, 6, false, "LENGTH (y)")},
            { "324", ElementStringFormat.GetFormat("324", CharType.Numeric, 6, false, "WIDTH (i)")},
            { "325", ElementStringFormat.GetFormat("325", CharType.Numeric, 6, false, "WIDTH (f)")},
            { "326", ElementStringFormat.GetFormat("326", CharType.Numeric, 6, false, "WIDTH (y)")},
            { "327", ElementStringFormat.GetFormat("327", CharType.Numeric, 6, false, "HEIGHT (i)")},
            { "328", ElementStringFormat.GetFormat("328", CharType.Numeric, 6, false, "HEIGHT (f)")},
            { "329", ElementStringFormat.GetFormat("329", CharType.Numeric, 6, false, "HEIGHT (y)")},
            { "330", ElementStringFormat.GetFormat("330", CharType.Numeric, 6, false, "GROSS WEIGHT (kg)")},
            { "331", ElementStringFormat.GetFormat("331", CharType.Numeric, 6, false, "LENGTH (m), log")},
            { "332", ElementStringFormat.GetFormat("332", CharType.Numeric, 6, false, "WIDTH (m), log")},
            { "333", ElementStringFormat.GetFormat("333", CharType.Numeric, 6, false, "HEIGHT (m), log")},
            { "334", ElementStringFormat.GetFormat("334", CharType.Numeric, 6, false, "AREA (m2), log")},
            { "335", ElementStringFormat.GetFormat("335", CharType.Numeric, 6, false, "VOLUME (l), log")},
            { "336", ElementStringFormat.GetFormat("336", CharType.Numeric, 6, false, "VOLUME (m3), log")},
            { "337", ElementStringFormat.GetFormat("337", CharType.Numeric, 6, false, "KG PER m2")},
            { "340", ElementStringFormat.GetFormat("340", CharType.Numeric, 6, false, "GROSS WEIGHT (lb)")},
            { "341", ElementStringFormat.GetFormat("341", CharType.Numeric, 6, false, "LENGTH (i), log")},
            { "342", ElementStringFormat.GetFormat("342", CharType.Numeric, 6, false, "LENGTH (f), log")},
            { "343", ElementStringFormat.GetFormat("343", CharType.Numeric, 6, false, "LENGTH (y), log")},
            { "344", ElementStringFormat.GetFormat("344", CharType.Numeric, 6, false, "WIDTH (i), log")},
            { "345", ElementStringFormat.GetFormat("345", CharType.Numeric, 6, false, "WIDTH (f), log")},
            { "346", ElementStringFormat.GetFormat("346", CharType.Numeric, 6, false, "WIDTH (y), log")},
            { "347", ElementStringFormat.GetFormat("347", CharType.Numeric, 6, false, "HEIGHT (i), log")},
            { "348", ElementStringFormat.GetFormat("348", CharType.Numeric, 6, false, "HEIGHT (f), log")},
            { "349", ElementStringFormat.GetFormat("349", CharType.Numeric, 6, false, "HEIGHT (t), log")},
            { "350", ElementStringFormat.GetFormat("350", CharType.Numeric, 6, false, "AREA (i2)")},
            { "351", ElementStringFormat.GetFormat("351", CharType.Numeric, 6, false, "AREA (f2)")},
            { "352", ElementStringFormat.GetFormat("352", CharType.Numeric, 6, false, "AREA (y2)")},
            { "353", ElementStringFormat.GetFormat("353", CharType.Numeric, 6, false, "AREA (i2), log")},
            { "354", ElementStringFormat.GetFormat("354", CharType.Numeric, 6, false, "AREA (f2), log")},
            { "355", ElementStringFormat.GetFormat("355", CharType.Numeric, 6, false, "AREA (y2), log")},
            { "356", ElementStringFormat.GetFormat("356", CharType.Numeric, 6, false, "NET WEIGHT (t)")},
            { "357", ElementStringFormat.GetFormat("357", CharType.Numeric, 6, false, "NET VOLUME (oz)")},
            { "360", ElementStringFormat.GetFormat("360", CharType.Numeric, 6, false, "NET VOLUME (q)")},
            { "361", ElementStringFormat.GetFormat("361", CharType.Numeric, 6, false, "NET VOLUME (g)")},
            { "362", ElementStringFormat.GetFormat("362", CharType.Numeric, 6, false, "VOLUME (q), log")},
            { "363", ElementStringFormat.GetFormat("363", CharType.Numeric, 6, false, "VOLUME (g), log")},
            { "364", ElementStringFormat.GetFormat("364", CharType.Numeric, 6, false, "VOLUME (i3)")},
            { "365", ElementStringFormat.GetFormat("365", CharType.Numeric, 6, false, "VOLUME (f3)")},
            { "366", ElementStringFormat.GetFormat("366", CharType.Numeric, 6, false, "VOLUME (y3)")},
            { "367", ElementStringFormat.GetFormat("367", CharType.Numeric, 6, false, "VOLUME (i3), log")},
            { "368", ElementStringFormat.GetFormat("368", CharType.Numeric, 6, false, "VOLUME (f3), log")},
            { "369", ElementStringFormat.GetFormat("369", CharType.Numeric, 6, false, "VOLUME (y3), log")},
            { "37", ElementStringFormat.GetFormat("37", CharType.Numeric, 8, true, "COUNT")},
            { "390", ElementStringFormat.GetFormat("390", CharType.Numeric, 15, true, "AMOUNT")},
            { "391", ElementStringFormat.GetFormat("391", CharType.Numeric, 18, true, "AMOUNT")},
            { "392", ElementStringFormat.GetFormat("392", CharType.Numeric, 15, true, "PRICE")},
            { "393", ElementStringFormat.GetFormat("393", CharType.Numeric, 18, true, "PRICE")},
            { "400", ElementStringFormat.GetFormat("400", CharType.Alphanumeric, 30, true, "ORDER NUMBER")},
            { "401", ElementStringFormat.GetFormat("401", CharType.Alphanumeric, 30, true, "CONSIGNMENT")},
            { "402", ElementStringFormat.GetFormat("402", CharType.Numeric, 17, true, "SHIPMENT NO.")},
            { "403", ElementStringFormat.GetFormat("403", CharType.Alphanumeric, 30, true, "ROUTE")},
            { "410", ElementStringFormat.GetFormat("410", CharType.Numeric, 13, false, "SHIP TO LOC")},
            { "411", ElementStringFormat.GetFormat("411", CharType.Numeric, 13, false, "BILL TO")},
            { "412", ElementStringFormat.GetFormat("412", CharType.Numeric, 13, false, "PURCHASE FROM")},
            { "413", ElementStringFormat.GetFormat("413", CharType.Numeric, 13, false, "SHIP FOR LOC")},
            { "414", ElementStringFormat.GetFormat("414", CharType.Numeric, 13, false, "LOC No")},
            { "415", ElementStringFormat.GetFormat("415", CharType.Numeric, 13, false, "PAY TO")},
            { "420", ElementStringFormat.GetFormat("420", CharType.Alphanumeric, 20, true, "SHIP TO POST")},
            { "421", ElementStringFormat.GetFormat("421", CharType.Alphanumeric, 12, true, "SHIP TO POST")},
            { "422", ElementStringFormat.GetFormat("422", CharType.Numeric, 3, true, "ORIGIN")},
            { "423", ElementStringFormat.GetFormat("423", CharType.Numeric, 15, true, "COUNTRY-INITIAL PROCESS.")},
            { "424", ElementStringFormat.GetFormat("424", CharType.Numeric, 3, true, "COUNTRY-PROCESS.")},
            { "425", ElementStringFormat.GetFormat("425", CharType.Numeric, 3, true, "COUNTRY-DISASSEMBLY")},
            { "426", ElementStringFormat.GetFormat("426", CharType.Numeric, 3, true, "COUNTRY-FULL PROCESS")},
            { "7001", ElementStringFormat.GetFormat("7001", CharType.Numeric, 13, true, "NSN")},
            { "7002", ElementStringFormat.GetFormat("7002", CharType.Alphanumeric, 30, true, "MEAT CUT")},
            { "7003", ElementStringFormat.GetFormat("7003", CharType.Numeric, 10, true, "EXPIRY TIME")},
            { "7004", ElementStringFormat.GetFormat("7004", CharType.Numeric, 4, true, "ACTIVE POTENCY")},
            { "703s", ElementStringFormat.GetFormat("7004", CharType.Alphanumeric, 30, true, "PROCESS # s")},
            { "8001", ElementStringFormat.GetFormat("8001", CharType.Numeric, 14, true, "DIMENSIONS")},
            { "8002", ElementStringFormat.GetFormat("8002", CharType.Alphanumeric, 20, true, "CMT No")},
            { "8003", ElementStringFormat.GetFormat("8003", CharType.Alphanumeric, 30, true, "GRAI")},
            { "8004", ElementStringFormat.GetFormat("8004", CharType.Alphanumeric, 30, true, "GIAI")},
            { "8005", ElementStringFormat.GetFormat("8005", CharType.Numeric, 6, true, "PRICE PER UNIT")},
            { "8006", ElementStringFormat.GetFormat("8006", CharType.Numeric, 18, true, "GCTIN")},
            { "8007", ElementStringFormat.GetFormat("8007", CharType.Alphanumeric, 30, true, "IBAN")},
            { "8008", ElementStringFormat.GetFormat("8008", CharType.Numeric, 12, true, "PROD TIME")},
            { "8018", ElementStringFormat.GetFormat("8018", CharType.Numeric, 18, true, "GSRN")},
            { "8020", ElementStringFormat.GetFormat("8020", CharType.Alphanumeric, 25, true, "REF No")},
            { "8100", ElementStringFormat.GetFormat("8100", CharType.Numeric, 6, true, "")},
            { "8101", ElementStringFormat.GetFormat("8101", CharType.Numeric, 10, true, "-")},
            { "8102", ElementStringFormat.GetFormat("8102", CharType.Numeric, 2, true, "-")},
            { "8110", ElementStringFormat.GetFormat("8110", CharType.Alphanumeric, 70, false, "")},
            { "8200", ElementStringFormat.GetFormat("8200", CharType.Alphanumeric, 70, true, "PRODUCT URL")},
            { "90", ElementStringFormat.GetFormat("90", CharType.Alphanumeric, 30, true, "INTERNAL")},
            { "91", ElementStringFormat.GetFormat("91", CharType.Alphanumeric, 30, true, "INTERNAL")},
            { "92", ElementStringFormat.GetFormat("92", CharType.Alphanumeric, 30, true, "INTERNAL")},
            { "93", ElementStringFormat.GetFormat("93", CharType.Alphanumeric, 30, true, "INTERNAL")},
            { "94", ElementStringFormat.GetFormat("94", CharType.Alphanumeric, 30, true, "INTERNAL")},
            { "95", ElementStringFormat.GetFormat("95", CharType.Alphanumeric, 30, true, "INTERNAL")},
            { "96", ElementStringFormat.GetFormat("96", CharType.Alphanumeric, 30, true, "INTERNAL")},
            { "97", ElementStringFormat.GetFormat("97", CharType.Alphanumeric, 30, true, "INTERNAL")},
            { "98", ElementStringFormat.GetFormat("98", CharType.Alphanumeric, 30, true, "INTERNAL")},
            { "99", ElementStringFormat.GetFormat("99", CharType.Alphanumeric, 30, true, "INTERNAL")}
        };

        private readonly GS1Algorithm _gs1Algorithm;

        public EAN128Tokenizer(byte[] rowData)
        {
            var barcode = Encoding.ASCII.GetChars(rowData);
            _gs1Algorithm = new GS1Algorithm(barcode);
        }

        public EAN128Tokenizer(char[] asciiData)
        {
            _gs1Algorithm = new GS1Algorithm(asciiData);
        }

        public ElementString NextToken()
        {
            _gs1Algorithm.ResetAI();
            var format = _gs1Algorithm.NextFormat();
            return _gs1Algorithm.NextElementWithFormat(format);
        }

        public bool HasMoreToken()
        {
            return _gs1Algorithm.Index < _gs1Algorithm.Barcode.Length;
        }
    }

    public class EAN128Exception : Exception
    {
        public EAN128Exception()
        {
        }

        public EAN128Exception(string msg) : base(msg)
        {
        }
    }
}