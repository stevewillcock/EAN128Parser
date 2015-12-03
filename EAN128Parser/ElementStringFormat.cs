using System;

namespace Drkstr.EAN128
{
    public enum CharType
    {
        Alphanumeric,
        Numeric
    }

    public struct ElementStringFormat : ICloneable
    {
        public string AI { get; set; }

        public CharType DataType { get; set; }

        public int DataLength { get; set; }

        public bool FNC1 { get; set; }

        public string Description { get; set; }

        public int NumberOfDecimal { get; set; }

        public override string ToString()
        {
            return AI + "-" + Description;
        }

        public static ElementStringFormat GetFormat(string applicationIdentifier, CharType dataType, int dataLength, bool fnc1, string description)
        {
            var format = new ElementStringFormat
            {
                AI = applicationIdentifier,
                DataType = dataType,
                DataLength = dataLength,
                FNC1 = fnc1,
                Description = description,
                NumberOfDecimal = -1
            };


            return format;
        }

        public static ElementStringFormat GetFormat(string applicationIdentifier, CharType dataType, int dataLength, bool fnc1, string description, int numberOfDecimals)
        {
            var format = GetFormat(applicationIdentifier, dataType, dataLength, fnc1, description);
            format.NumberOfDecimal = numberOfDecimals;

            return format;
        }

        public object Clone()
        {
            return GetFormat(AI, DataType, DataLength, FNC1, Description);
        }
    }
}