using System;
using System.Collections.Generic;
using System.Linq;

namespace Drkstr.EAN128
{
    public enum GS1State
    {
        E,
        L2,
        L3,
        L4,
        EXT
    }

    public class GS1Algorithm
    {
        private static readonly char[] FNC1Values = {(char) 29, (char) 32};

        public GS1Algorithm(char[] barcode)
        {
            AI = "";
            State = GS1State.E;
            Index = 0;

            Barcode = barcode;
        }

        public string AI { get; private set; }

        public GS1State State { get; private set; }

        public char[] Barcode { get; }

        public int Index { get; set; }

        public void ResetAI()
        {
            AI = "";
            State = GS1State.E;
        }

        public ElementStringFormat NextFormat()
        {
            try
            {
                while (State != GS1State.EXT)
                {
                    SetNextChar(Barcode[Index++]);
                }

                try
                {
                    var format = (ElementStringFormat) EAN128Tokenizer.Formats[AI].Clone();

                    if (!StringEquals(AI.Substring(0, 2), new[] {"31", "32", "33", "34", "35", "36", "39"})) return format;
                    var nod = Convert.ToInt32(Barcode[Index++].ToString());
                    format.NumberOfDecimal = nod;

                    return format;
                }
                catch (KeyNotFoundException)
                {
                    throw new GS1AlgorithmException("AI not found: " + AI);
                }
            }
            catch (Exception)
            {
                throw new GS1AlgorithmException("Error occured while parsing format");
            }
        }

        private void SetNextChar(char next)
        {
            switch (State)
            {
                case GS1State.E:
                    State = GS1State.L2;
                    AI = AI + next;
                    break;
                case GS1State.L2:
                    if (AI.Equals("0"))
                    {
                        if (next == '0' || next == '1' || next == '2')
                        {
                            State = GS1State.EXT;
                            AI = AI + next;
                        }
                        else
                        {
                            throw new GS1AlgorithmException(Index);
                        }
                    }
                    else if (AI.Equals("1"))
                    {
                        if (CharEquals(next, new[] {'0', '1', '2', '3', '5', '7'}))
                        {
                            State = GS1State.EXT;
                            AI = AI + next;
                        }
                        else
                        {
                            throw new GS1AlgorithmException(Index);
                        }
                    }
                    else if (AI.Equals("2"))
                    {
                        if (CharEquals(next, new[] {'0', '1', '2'}))
                        {
                            State = GS1State.EXT;
                            AI = AI + next;
                        }
                        else if (CharEquals(next, new[] {'4', '5'}))
                        {
                            State = GS1State.L3;
                            AI = AI + next;
                        }
                        else
                        {
                            throw new GS1AlgorithmException(Index);
                        }
                    }
                    else if (AI.Equals("3"))
                    {
                        if (CharEquals(next, new[] {'0', '7'}))
                        {
                            State = GS1State.EXT;
                            AI = AI + next;
                        }
                        else if (CharEquals(next, new[] {'0', '1', '2', '3', '4', '5', '6', '9'}))
                        {
                            State = GS1State.L3;
                            AI = AI + next;
                        }
                        else
                        {
                            throw new GS1AlgorithmException(Index);
                        }
                    }
                    else if (StringEquals(AI, new[] {"4", "7", "8"}))
                    {
                        if (CharEquals(next, new[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'}))
                        {
                            State = GS1State.L2;
                            AI = AI + next;
                        }
                        else
                        {
                            throw new GS1AlgorithmException(Index);
                        }
                    }
                    else if (AI.Equals("9"))
                    {
                        if (CharEquals(next, new[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'}))
                        {
                            State = GS1State.EXT;
                            AI = AI + next;
                        }
                        else
                        {
                            throw new GS1AlgorithmException(Index);
                        }
                    }
                    else if (StringEquals(AI, new[] {"40", "41", "42"}))
                    {
                        if (CharEquals(next, new[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'}))
                        {
                            State = GS1State.EXT;
                            AI = AI + next;
                        }
                        else
                        {
                            throw new GS1AlgorithmException(Index);
                        }
                    }
                    else if (StringEquals(AI, new[] {"70", "80", "81", "82"}))
                    {
                        if (CharEquals(next, new[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'}))
                        {
                            State = GS1State.L4;
                            AI = AI + next;
                        }
                        else
                        {
                            throw new GS1AlgorithmException(Index);
                        }
                    }
                    break;
                case GS1State.L3:
                    if (AI.Equals("24"))
                    {
                        if (CharEquals(next, new[] {'0', '1', '2'}))
                        {
                            State = GS1State.EXT;
                            AI = AI + next;
                        }
                        else
                        {
                            throw new GS1AlgorithmException(Index);
                        }
                    }
                    else if (AI.Equals("25"))
                    {
                        if (CharEquals(next, new[] {'0', '1', '3', '4'}))
                        {
                            State = GS1State.EXT;
                            AI = AI + next;
                        }
                        else
                        {
                            throw new GS1AlgorithmException(Index);
                        }
                    }
                    else if (AI.Equals("25"))
                    {
                        if (CharEquals(next, new[] {'0', '1', '3', '5'}))
                        {
                            State = GS1State.EXT;
                            AI = AI + next;
                        }
                        else
                        {
                            throw new GS1AlgorithmException(Index);
                        }
                    }
                    else if (StringEquals(AI, new[] {"31", "32", "33", "34", "35", "36", "39"}))
                    {
                        if (CharEquals(next, new[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'}))
                        {
                            State = GS1State.EXT;
                            AI = AI + next;
                        }
                        else
                        {
                            throw new GS1AlgorithmException(Index);
                        }
                    }
                    break;
                case GS1State.L4:
                    if (CharEquals(next, new[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 's'}))
                    {
                        State = GS1State.EXT;
                        AI = AI + next;
                    }
                    else
                    {
                        throw new GS1AlgorithmException(Index);
                    }
                    break;
                case GS1State.EXT:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public ElementString NextElementWithFormat(ElementStringFormat format)
        {
            var element = new ElementString(format);

            try
            {
                if (format.FNC1) 
                {
                    var startIndex = Index;

                    while (Index < Barcode.Length && (Index - startIndex) <= format.DataLength)
                    {
                        if (CharIsFNC1(Barcode[Index]))
                        {
                            Index++;
                            break;
                        }
                        element.Data = element.Data + Barcode[Index];
                        Index++;
                    }
                }
                else
                {
                    for (var i = 0; i < element.Format.DataLength; i++)
                    {
                        element.Data = element.Data + Barcode[Index];
                        Index++;
                    }
                }

                return element;
            }
            catch (IndexOutOfRangeException)
            {
                throw new GS1AlgorithmException($"Unexpected end of format (AI={AI})");
            }
        }

        public static bool CharEquals(char c, char[] values)
        {
            return values.Any(v => c == v);
        }

        public static bool StringEquals(string s, string[] values)
        {
            return values.Any(s.Equals);
        }

        public static bool CharIsFNC1(char c)
        {
            return CharEquals(c, FNC1Values);
        }
    }

    public class GS1AlgorithmException : EAN128Exception
    {
        public GS1AlgorithmException()
        {
        }

        public GS1AlgorithmException(int index) : base($"Parse error at index {index}")
        {
        }

        public GS1AlgorithmException(string msg) : base(msg)
        {
        }
    }
}