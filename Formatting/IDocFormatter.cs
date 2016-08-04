using System;
using SourcePrinter.FileReading;

namespace SourcePrinter.Formatting
{
    public interface IDocFormatter
    {
        void AddTitle( string title, string subtitle, DateTime date );
        void AddSourceFile( SourceFileModel sourceFile );
        void Close();
        void AddTableOfContentsLine( string relativePath, int countLines, string checksum );
    }
}