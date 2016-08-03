using System.Text.RegularExpressions;
using AODL.Document.Content.Text;
using AODL.Document.TextDocuments;
using System.IO;
using System.Linq;

namespace SourcePrinter
{
    public class FilePrinter
    {
        private readonly string _filePath;
        private readonly string _prefix;

        public FilePrinter( string filePath, string prefix )
        {
            _filePath = filePath;
            _prefix = prefix;
            if (!_prefix.EndsWith( "\\" ))
                _prefix += "\\";
        }

        public void SpewIntoDoc( TextDocument document )
        {
            var lines = File.ReadAllLines( _filePath );
            var relativePath = _filePath.Substring( _prefix.Length );

            var header = new Header( document, Headings.Heading );
            header.TextContent.Add( new SimpleText( document, relativePath ) );
            document.Content.Add( header );

            for (var idx = 0; idx < lines.Count(); idx++)
            {
                var line = lines[idx];
                var lineNum = ( idx + 1 ).ToString( "00000" );
                var processed = Regex.Replace( line, @"^\s+", match => match.Value.Replace( ' ', '\u00a0' ) );
                processed = lineNum + " " + processed.Replace( "\t", "\u00a0\u00a0\u00a0\u00a0" );

                var paragraph = ParagraphBuilder.CreateStandardTextParagraph( document );
                var text = new SimpleText( document, processed );
                paragraph.TextContent.Add( text );
                document.Content.Add( paragraph );
            }
        }
    }
}