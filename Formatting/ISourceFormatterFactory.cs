namespace SourcePrinter.Formatting
{
    public interface ISourceFormatterFactory
    {
        ISourceFormatter Create( string filePath );
    }
}