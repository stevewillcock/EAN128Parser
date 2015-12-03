using System;

namespace Drkstr.EAN128
{
    public class ElementString
    {
        private ElementStringFormat _format;

        public ElementString(ElementStringFormat esf)
        {
            Format = esf;
            Data = "";
        }

        public ElementStringFormat Format
        {
            get { return _format; }
            set
            {
                _format = value;
                AI = value.AI;

                if (_format.NumberOfDecimal >= 0)
                {
                    AI += Format.NumberOfDecimal;
                }
            }
        }

        public string AI { get; set; }

        public string Description => _format.Description;

        public string Data { get; set; }

        public DateTime ParseDate()
        {
            if (GS1Algorithm.StringEquals(_format.AI, new[] {"11", "12", "13", "15", "17"}))
            {
                var y = Convert.ToInt32("20" + Data.Substring(0, 2));
                var m = Convert.ToInt32(Data.Substring(2, 2));
                var d = Convert.ToInt32(Data.Substring(4, 2));

                if (d == 0) d = 1; // If first day not specified

                return new DateTime(y, m, d);
            }
            throw new EAN128Exception("AI(" + _format.AI + ") is not a date");
        }

        public int ParseInt()
        {
            return Convert.ToInt32(Data);
        }

        public double ParseDouble()
        {
            return Convert.ToDouble(Data);
        }

        public override string ToString()
        {
            var ai = AI;

            if (Format.NumberOfDecimal >= 0)
            {
                ai += Format.NumberOfDecimal;
            }

            return $"({ai}){Data}";
        }
    }
}