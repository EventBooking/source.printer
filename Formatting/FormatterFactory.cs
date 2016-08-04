namespace SourcePrinter.Formatting
{
    public class FormatterFactory : IFormatterFactory
    {
        public IDocFormatter Create( string filePath )
        {
            return new DocFormatter( filePath );
        }
    }
}