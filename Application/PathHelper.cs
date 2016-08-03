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
    }
}