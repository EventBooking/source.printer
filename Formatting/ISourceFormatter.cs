using System;
using SourcePrinter.FileReading;

namespace SourcePrinter.Formatting
{
    public interface ISourceFormatter
    {
        void AddTitle( string title, DateTime date );
        void AddSourceFile( SourceFileModel sourceFile );
        void Close();
    }
}