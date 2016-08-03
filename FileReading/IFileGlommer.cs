namespace SourcePrinter.FileReading
{
    public interface IFileGlommer
    {
        SourceFileModel GlomTheFile( string fullPath );
    }
}