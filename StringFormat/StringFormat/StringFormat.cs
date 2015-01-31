using System;
using System.Text;

namespace StringFormat
{
    /// <summary>
    /// Apply couple of format methods that similiar to the String.Format() method.
    /// But will not throw Exception when the placeholders's count in format string does not match the args,
    /// instead it will format the string dynamicly.  
    /// </summary>
    public static class StringFormat
    {
        public static String Format(String format, Object arg0)
        {
            return Format(format, new[] { arg0 });
        }
        public static String Format(String format, Object arg0, Object arg1)
        {
            return Format(format, new[] { arg0, arg1 });
        }
        public static String Format(String format, Object arg0, Object arg1, Object arg2)
        {
            return Format(format, new[] { arg0, arg1, arg2 });
        }
        public static String Format(String format, params Object[] args)
        {
            if (format == null || args == null)
            {
                throw new ArgumentNullException((format == null) ? "format" : "args");
            }

            var pos = 0;
            var len = format.Length;
            var sb = new StringBuilder();

            while (true)
            {
                char ch;
                while (pos < len)
                {
                    ch = format[pos];

                    pos++;
                    if (ch == '}')
                    {
                        if (pos < len && format[pos] == '}') // Treat as escape character for }}
                            pos++;
                        else
                            throw new FormatException("Format_InvalidString");
                    }

                    if (ch == '{')
                    {
                        if (pos < len && format[pos] == '{') // Treat as escape character for {{
                            pos++;
                        else
                        {
                            pos--;
                            break;
                        }
                    }

                    sb.Append(ch);
                }

                if (pos == len) break;
                pos++;
                if (pos == len || (ch = format[pos]) < '0' || ch > '9') throw new FormatException("Format_InvalidString");
                int index = 0;
                do
                {
                    index = index * 10 + ch - '0';
                    pos++;
                    if (pos == len) throw new FormatException("Format_InvalidString");
                    ch = format[pos];
                } while (ch >= '0' && ch <= '9' && index < 1000000);
                while (pos < len && (ch = format[pos]) == ' ') pos++;
                bool leftJustify = false;
                int width = 0;
                if (ch == ',')
                {
                    pos++;
                    while (pos < len && format[pos] == ' ') pos++;

                    if (pos == len) throw new FormatException("Format_InvalidString");
                    ch = format[pos];
                    if (ch == '-')
                    {
                        leftJustify = true;
                        pos++;
                        if (pos == len) throw new FormatException("Format_InvalidString");
                        ch = format[pos];
                    }
                    if (ch < '0' || ch > '9') throw new FormatException("Format_InvalidString");
                    do
                    {
                        width = width * 10 + ch - '0';
                        pos++;
                        if (pos == len) throw new FormatException("Format_InvalidString");
                        ch = format[pos];
                    } while (ch >= '0' && ch <= '9' && width < 1000000);
                }

                while (pos < len && (ch = format[pos]) == ' ') pos++;
                Object arg = "null";
                if (index < args.Length)
                {
                    arg = args[index];
                }
                StringBuilder fmt = null;
                if (ch == ':')
                {
                    pos++;
                    while (true)
                    {
                        if (pos == len) throw new FormatException("Format_InvalidString");
                        ch = format[pos];
                        pos++;
                        if (ch == '{')
                        {
                            if (pos < len && format[pos] == '{')  // Treat as escape character for {{
                                pos++;
                            else
                                throw new FormatException("Format_InvalidString");
                        }
                        else if (ch == '}')
                        {
                            if (pos < len && format[pos] == '}')  // Treat as escape character for }}
                                pos++;
                            else
                            {
                                pos--;
                                break;
                            }
                        }

                        if (fmt == null)
                        {
                            fmt = new StringBuilder();
                        }
                        fmt.Append(ch);
                    }
                }

                if (ch != '}') throw new FormatException("Format_InvalidString");
                pos++;
                String s = null;
                IFormattable formattableArg = arg as IFormattable;

                if (formattableArg != null)
                {
                    s = formattableArg.ToString();
                }
                else if (arg != null)
                {
                    s = arg.ToString();
                }

                if (s == null) s = String.Empty;
                int pad = width - s.Length;
                if (!leftJustify && pad > 0) sb.Append(' ', pad);
                sb.Append(s);
                if (leftJustify && pad > 0) sb.Append(' ', pad);
            }
            return sb.ToString();
        }



        public static Boolean TryFormat(String format, out String result, Object arg0)
        {
            return TryFormat(format, out result, new[] { arg0 });
        }
        public static Boolean TryFormat(String format, out String result, Object arg0, Object arg1)
        {
            return TryFormat(format, out result, new[] { arg0, arg1 });
        }
        public static Boolean TryFormat(String format, out String result, Object arg0, Object arg1, Object arg2)
        {
            return TryFormat(format, out result, new[] { arg0, arg1, arg2 });
        }
        public static Boolean TryFormat(String format, out String result, params Object[] args)
        {
            result = format;
            bool flag = true;
            if (format == null || args == null)
            {
                return false;
            }

            var pos = 0;
            var len = format.Length;
            var sb = new StringBuilder();

            while (true)
            {
                char ch;
                while (pos < len)
                {
                    ch = format[pos];

                    pos++;
                    if (ch == '}')
                    {
                        if (pos < len && format[pos] == '}') // Treat as escape character for }}
                            pos++;
                        else
                            return false;
                    }

                    if (ch == '{')
                    {
                        if (pos < len && format[pos] == '{') // Treat as escape character for {{
                            pos++;
                        else
                        {
                            pos--;
                            break;
                        }
                    }

                    sb.Append(ch);
                }

                if (pos == len) break;
                pos++;
                if (pos == len || (ch = format[pos]) < '0' || ch > '9') return false;
                int index = 0;
                do
                {
                    index = index * 10 + ch - '0';
                    pos++;
                    if (pos == len) flag = false;
                    ch = format[pos];
                } while (ch >= '0' && ch <= '9' && index < 1000000);
                if (index >= args.Length) flag = false;
                while (pos < len && (ch = format[pos]) == ' ') pos++;
                bool leftJustify = false;
                int width = 0;
                if (ch == ',')
                {
                    pos++;
                    while (pos < len && format[pos] == ' ') pos++;

                    if (pos == len) flag = false;
                    ch = format[pos];
                    if (ch == '-')
                    {
                        leftJustify = true;
                        pos++;
                        if (pos == len) flag = false;
                        ch = format[pos];
                    }
                    if (ch < '0' || ch > '9') flag = false;
                    do
                    {
                        width = width * 10 + ch - '0';
                        pos++;
                        if (pos == len) flag = false;
                        ch = format[pos];
                    } while (ch >= '0' && ch <= '9' && width < 1000000);
                }

                while (pos < len && (ch = format[pos]) == ' ') pos++;
                Object arg = "null";
                if (index < args.Length)
                {
                    arg = args[index];
                }

                StringBuilder fmt = null;
                if (ch == ':')
                {
                    pos++;
                    while (true)
                    {
                        if (pos == len) flag = false;
                        ch = format[pos];
                        pos++;
                        if (ch == '{')
                        {
                            if (pos < len && format[pos] == '{')  // Treat as escape character for {{
                                pos++;
                            else
                                flag = false;
                        }
                        else if (ch == '}')
                        {
                            if (pos < len && format[pos] == '}')  // Treat as escape character for }}
                                pos++;
                            else
                            {
                                pos--;
                                break;
                            }
                        }

                        if (fmt == null)
                        {
                            fmt = new StringBuilder();
                        }
                        fmt.Append(ch);
                    }
                }

                if (ch != '}') return false;
                pos++;
                String s = null;
                IFormattable formattableArg = arg as IFormattable;

                if (formattableArg != null)
                {
                    s = formattableArg.ToString();
                }
                else if (arg != null)
                {
                    s = arg.ToString();
                }

                if (s == null) s = String.Empty;
                int pad = width - s.Length;
                if (!leftJustify && pad > 0) sb.Append(' ', pad);
                sb.Append(s);
                if (leftJustify && pad > 0) sb.Append(' ', pad);
            }
            result = sb.ToString();
            return flag;
        }
    }
}
