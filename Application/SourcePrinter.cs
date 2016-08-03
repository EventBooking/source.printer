using System;
using System.IO;
using System.Linq;
using SourcePrinter.Config;
using SourcePrinter.FileReading;
using SourcePrinter.Formatting;

namespace SourcePrinter.Application
{
    public class SourcePrinter
    {
        private readonly IConfig _config;
        private readonly IFileGlommer _glommer;
        private readonly ISourceFormatterFactory _formatterFactory;

        public SourcePrinter( IConfig config, IFileGlommer glommer, ISourceFormatterFactory formatterFactory )
        {
            _config = config;
            _glommer = glommer;
            _formatterFactory = formatterFactory;
        }

        public void WriteToPdf()
        {
            var dirInfo = new DirectoryInfo( _config.SourcePath );
            if (!dirInfo.Exists)
                return;

            var doc = _formatterFactory.Create( _config.TargetFile );
            ScanDirectory( dirInfo, dirInfo.FullName, doc );
            doc.Close();
        }

        private void ScanDirectory( DirectoryInfo dirInfo, string rootPath, ISourceFormatter source )
        {
            if (dirInfo.Name.StartsWith( "." ))
                return;
            if (_config.DirectoryBlacklist.Contains( dirInfo.Name ))
                return;

            foreach (var file in dirInfo.EnumerateFiles())
            {
                if (!_config.Extensions.Contains( file.Extension ))
                    continue;

                Console.WriteLine( file.FullName );

                var fileModel = _glommer.GlomTheFile( file.FullName );
                var relativePath = PathHelper.GetRelativePath( file.FullName, rootPath );
                fileModel.RelativePath = relativePath;

                source.AddSourceFile( fileModel );
            }

            foreach (var dir in dirInfo.EnumerateDirectories())
            {
                ScanDirectory( dir, rootPath, source );
            }
        }
    }
}