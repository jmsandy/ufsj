using System;
using System.IO;
using System.Text;

namespace NetworkBasics.Sockets
{
    /// <summary>
    /// Html helper.
    /// </summary>
    /// <Author>Jose Mauro da Silva Sandy</Author>
    /// <Date>10/12/2017</Date>
    internal sealed class HtmlHelper
    {
        /// <summary>
        /// Mounts the html from resources list from a path.
        /// </summary>
        /// <param name="basePath">base path.</param>
        /// <param name="path">resources path.</param>
        /// <returns>html.</returns>
        public static string GetHtmlResourcesList(string basePath, string path)
        {
            var html = new StringBuilder();

            html.Append("<html>");
            html.Append("<head>");
            html.Append("<title>Listing Files and Directories</title>");
            html.Append("</head>");
            html.Append("<body>");
            html.AppendFormat("<h1>My Custom Web Server - {0}</h1>", path);
            html.Append("<hr>");
            html.Append("<pre>");
            html.Append("<table>");
            if (!"/".Equals(path))
            {
                var parentDirectory = new DirectoryInfo(Path.Combine(basePath, path.Substring(1))).Parent.FullName.Replace(basePath, "").Replace('\\', '/');
                html.AppendFormat("<a href=\"{0}\">[To Parent Directory]</a><br><br>", "".Equals(parentDirectory) ? "/" : parentDirectory);
            }

            FileInfo fileInfo = null;
            foreach (var directoryInfo in new DirectoryInfo(Path.Combine(basePath, path.Substring(1))).GetFileSystemInfos())
            {   
                fileInfo = new FileInfo(directoryInfo.FullName);
                html.Append("<tr>");
                html.AppendFormat("<td>{0} </td>", fileInfo.LastAccessTime.ToString("dd/MM/yyyy HH:mm:ss"));
                if (directoryInfo.Attributes.HasFlag(FileAttributes.Directory))
                {
                    html.Append("<td align=\"right\">&lt;dir&gt; </td>");
                }
                else
                {
                    html.AppendFormat("<td align=\"right\">{0} ", fileInfo.Length);
                }
                html.AppendFormat("<td><a href=\"{0}\"> {1}</a></td>", Path.Combine(path, fileInfo.Name), fileInfo.Name);
                html.Append("</tr>");
            }

            html.Append("</table>");
            html.Append("</pre>");
            html.Append("<hr>");
            html.Append("</body>");
            html.Append("</html>");
            return html.ToString();
        }

        /// <summary>
        /// Mounts the html to resource not found.
        /// </summary>
        /// <param name="resource">resource searched.</param>
        /// <returns>html.</returns>
        public static string GetHtmlResourceNotFound(string resource)
        {
            var html = new StringBuilder();

            html.Append("<html>");
            html.Append("<head>");
            html.Append("<title>Resource Not Found</title>");
            html.Append("</head>");
            html.Append("<body>");
            html.AppendFormat("<h1>Resource {0} not found.</h1>", resource);
            html.Append("</body>");
            html.Append("</html>");

            return html.ToString();
        }
    }
}
