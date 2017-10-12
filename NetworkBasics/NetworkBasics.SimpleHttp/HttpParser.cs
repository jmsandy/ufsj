using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NetworkBasics.SimpleHttp
{
    /// <summary>
    /// Html Parser Helper.
    /// </summary>
    /// <Author>Jose Mauro da Silva Sandy</Author>
    /// <Date>10/12/2017</Date>
    internal sealed class HttpParser
    {
        public MemoryStream Body { get; private set; }

        /// <summary>
        /// Extracts all attributes and body from received html.
        /// </summary>
        /// <typeparam name="TEnum">return enum type.</typeparam>
        /// <param name="data">received html.</param>
        /// <param name="indRequest">indicates a request or a response.</param>
        /// <returns>parameters.</returns>
        public Dictionary<TEnum, string> ExtractParameters<TEnum>(Stream data, bool indRequest) where TEnum : struct, IConvertible 
        {
            var parameters = new Dictionary<TEnum, string>();

            TEnum option = default(TEnum);
            var regex = new Regex("[^a-zA-Z0-9]");

            Body = new MemoryStream();
            data.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(data))
            {
                var line = reader.ReadLine();
                if (line != null)
                {
                    var splitter = line.Split(' ');

                    if (indRequest)
                    {
                        parameters.Add((TEnum)Enum.Parse(typeof(TEnum), "Method"), splitter[0]);
                        parameters.Add((TEnum)Enum.Parse(typeof(TEnum), "Address"), splitter[1]);
                        parameters.Add((TEnum)Enum.Parse(typeof(TEnum), "HttpVersion"), splitter[2]);
                    }
                    else
                    {
                        parameters.Add((TEnum)Enum.Parse(typeof(TEnum), "HttpVersion"), splitter[0]);
                        parameters.Add((TEnum)Enum.Parse(typeof(TEnum), "StatusCode"), splitter[1]);
                        parameters.Add((TEnum)Enum.Parse(typeof(TEnum), "StatusMessage"), splitter[2]);
                    }

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (string.Empty.Equals(line.Trim()))
                        {
                            if (!indRequest)
                            {
                                option = (TEnum)Enum.Parse(typeof(TEnum), "ContentLength");
                                data.Seek(data.Length - Int32.Parse(parameters[option]), SeekOrigin.Begin);
                                data.CopyTo(Body);
                            }
                            break;
                        }

                        try
                        {
                            splitter = line.Split(':');
                            option = (TEnum)Enum.Parse(typeof(TEnum), regex.Replace(splitter[0], ""));
                            parameters.Add((TEnum)Enum.Parse(typeof(TEnum), regex.Replace(splitter[0], "")),
                            line.Substring(line.IndexOf(":") + 1));
                        }
                        catch { }
                    }
                }
            }

            return parameters;
        }
    }
}
