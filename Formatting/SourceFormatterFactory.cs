namespace SourcePrinter.Formatting
{
    public class SourceFormatterFactory : ISourceFormatterFactory
    {
        public ISourceFormatter Create( string filePath )
        {
            return new SourceFormatter( filePath );
        }
    }
}