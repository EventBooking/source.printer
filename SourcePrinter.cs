using System;
using System.Collections.Generic;
using System.IO;
using AODL.Document.TextDocuments;

namespace SourcePrinter
{
    public class SourcePrinter
    {
        private readonly string _pathSource;
        private readonly TextDocument _document;
        private readonly List<string> _extentionsToInclude;

        public SourcePrinter( string pathSource )
        {
            _pathSource = pathSource;
            _document = new TextDocument();
            _document.New();
            //_extentionsToInclude = new List<string> {".cs", ".js", ".css", ".less", ".aspx", ".ascx", ".resx", ".xml", ".xslt"};
            _extentionsToInclude = new List<string> { ".cs", ".js", ".ts" };
        }

        public void WriteToOdt( string pathTarget )
        {
            var dirInfo = new DirectoryInfo( _pathSource );
            if (!dirInfo.Exists)
                return;

            ScanDirectory( dirInfo );

            _document.SaveTo( pathTarget );
        }

        private void ScanDirectory( DirectoryInfo dirInfo )
        {
            if (dirInfo.Name == "obj"
                || dirInfo.Name == "bin"
                || dirInfo.Name == "packages"
                || dirInfo.Name == "generated"
                || dirInfo.Name == "_generated"
                || dirInfo.Name == "artifacts"
                || dirInfo.Name == "vendor"
                || dirInfo.Name == "bower_components"
                || dirInfo.Name == "node_modules"
                || dirInfo.Name == "typings"
                || dirInfo.Name == "wwwroot"
                || dirInfo.Name == "T4Gen"
                || dirInfo.Name.StartsWith( "." ))
                return;

            foreach (var file in dirInfo.EnumerateFiles())
            {
                if (!_extentionsToInclude.Contains( file.Extension ))
                    continue;

                Console.WriteLine( file.FullName );
                var fu = new FilePrinter( file.FullName, _pathSource );
                fu.SpewIntoDoc( _document );
            }

            foreach (var dir in dirInfo.EnumerateDirectories())
            {
                ScanDirectory( dir );
            }
        }
    }
}