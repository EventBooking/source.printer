using SourcePrinter.FileReading;

namespace SourcePrinter.Formatting
{
    public interface ISourceFormatter
    {
        void AddSourceFile( SourceFileModel sourceFile );
        void Close();
    }
}