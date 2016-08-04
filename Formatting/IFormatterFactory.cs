namespace SourcePrinter.Formatting
{
    public interface IFormatterFactory
    {
        IDocFormatter Create( string filePath );
    }
}