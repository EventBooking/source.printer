using System.IO;

namespace SourcePrinter.Application
{
    public class PathHelper
    {
        public static string GetRelativePath( string fullPath, string rootPath )
        {
            if (!fullPath.StartsWith( rootPath ))
                return fullPath;

            var relPath = fullPath.Substring( rootPath.Length );
            var prefix = relPath.StartsWith( @"\" ) ? "~" : @"~\";
            return prefix + relPath;
        }

        public static string GetTableOfContentsFilePath( string targetFile )
        {
            var fi = new FileInfo( targetFile );
            if (string.IsNullOrWhiteSpace( fi.DirectoryName ))
                throw new DirectoryNotFoundException();

            var basename = Path.GetFileNameWithoutExtension( targetFile );
            var tocFilePath = Path.Combine( fi.DirectoryName, $"{basename}-toc.pdf" );
            return tocFilePath;
        }
    }
}