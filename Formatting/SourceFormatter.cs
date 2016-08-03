using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SourcePrinter.FileReading;

namespace SourcePrinter.Formatting
{
    public class SourceFormatter : ISourceFormatter
    {
        private readonly Document _doc;

        public SourceFormatter( string filePath )
        {
            _doc = new Document( PageSize.LEGAL );
            PdfWriter.GetInstance( _doc, new FileStream( filePath, FileMode.Create ) );
            _doc.Open();
        }

        public void AddSourceFile( SourceFileModel sourceFile )
        {
            _doc.Add( new Paragraph( $"File: {sourceFile.RelativePath}" ) );
            _doc.Add( new Paragraph( $"MD5 Checksum: {sourceFile.Checksum}" ) );

            var lines = sourceFile.Lines.ToList();
            for (var idx = 0; idx < lines.Count; idx++)
            {
                var line = lines[idx];

                var lineNum = ( idx + 1 ).ToString( "00000" );
                var processed = Regex.Replace( line, @"^\s+", match => match.Value.Replace( ' ', '\u00a0' ) );
                processed = lineNum + " " + processed.Replace( "\t", "\u00a0\u00a0\u00a0\u00a0" );

                _doc.Add( new Paragraph( processed ) );
            }
        }

        public void Close()
        {
            _doc.Close();
        }
    }
}