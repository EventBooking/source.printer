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
        private readonly IFormatterFactory _formatterFactory;

        public SourcePrinter( IConfig config, IFileGlommer glommer, IFormatterFactory formatterFactory )
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

            var tocFilePath = PathHelper.GetTableOfContentsFilePath( _config.TargetFile );

            var sourceDoc = _formatterFactory.Create( _config.TargetFile );
            var tocDoc = _formatterFactory.Create( tocFilePath );

            sourceDoc.AddTitle( _config.ProductName, "Source code", DateTime.Now );
            tocDoc.AddTitle( _config.ProductName, "Table of contents", DateTime.Now );

            ScanDirectory( dirInfo, dirInfo.FullName, sourceDoc, tocDoc );

            sourceDoc.Close();
            tocDoc.Close();
        }


        private void ScanDirectory( DirectoryInfo dirInfo, string rootPath, IDocFormatter sourceDoc,
            IDocFormatter tocDoc )
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

                sourceDoc.AddSourceFile( fileModel );

                tocDoc.AddTableOfContentsLine( fileModel.RelativePath, fileModel.Lines.Count(), fileModel.Checksum );
            }

            foreach (var dir in dirInfo.EnumerateDirectories())
            {
                ScanDirectory( dir, rootPath, sourceDoc, tocDoc );
            }
        }
    }
}