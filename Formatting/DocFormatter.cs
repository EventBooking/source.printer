using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SourcePrinter.FileReading;

namespace SourcePrinter.Formatting
{
    public class DocFormatter : IDocFormatter
    {
        private readonly Document _doc;
        private readonly Font _titleFont;
        private readonly Font _dateFont;
        private readonly Font _fileHeaderFont;
        private readonly Font _sourceLineFont;
        private readonly Font _tocFont;

        public DocFormatter( string filePath )
        {
            _doc = new Document( PageSize.LETTER );
            PdfWriter.GetInstance( _doc, new FileStream( filePath, FileMode.Create ) );
            _doc.Open();

            _titleFont = new Font( Font.FontFamily.HELVETICA, 18f, Font.BOLD, BaseColor.DARK_GRAY );
            _dateFont = new Font( Font.FontFamily.HELVETICA, 12f, Font.NORMAL, BaseColor.LIGHT_GRAY );
            _fileHeaderFont = new Font( Font.FontFamily.COURIER, 9f, Font.BOLD, BaseColor.LIGHT_GRAY );
            _sourceLineFont = new Font( Font.FontFamily.COURIER, 6f, Font.NORMAL, BaseColor.BLACK );
            _tocFont = new Font( Font.FontFamily.COURIER, 8f, Font.NORMAL, BaseColor.BLACK );
        }

        public void AddTitle( string title, string subtitle, DateTime date )
        {
            var formattedDate = date.ToLongDateString();

            AddParagraph( title, _titleFont );
            AddParagraph( $"{subtitle} - {formattedDate}", _dateFont );
            AddBlankLine();

            _doc.AddTitle( title );
            _doc.AddSubject( subtitle );
            _doc.AddCreationDate();
        }

        public void AddSourceFile( SourceFileModel sourceFile )
        {
            AddParagraph( $"File: {sourceFile.RelativePath}", _fileHeaderFont );
            AddParagraph( $"MD5 Checksum: {sourceFile.Checksum}", _fileHeaderFont );

            var lines = sourceFile.Lines.ToList();
            for (var idx = 0; idx < lines.Count; idx++)
            {
                var line = lines[idx];
                var processed = FormatSourceLine( idx, line );

                AddParagraph( processed, _sourceLineFont );
            }
            AddBlankLine();
        }

        public void AddTableOfContentsLine( string relativePath, int countLines, string checksum )
        {
            AddParagraph( relativePath, _tocFont );
            AddParagraph( $"{countLines} lines / MD5 checksum = {checksum}", _tocFont );
            AddBlankLine();
        }

        private static string FormatSourceLine( int idx, string line )
        {
            var lineNum = ( idx + 1 ).ToString( "00000" );
            var processed = Regex.Replace( line, @"^\s+", match => match.Value.Replace( ' ', '\u00a0' ) );
            processed = lineNum + "  " + processed.Replace( "\t", "\u00a0\u00a0\u00a0\u00a0" );
            return processed;
        }

        private void AddParagraph( string text, Font font )
        {
            var ch = new Chunk( text, font );
            _doc.Add( new Paragraph( ch ) );
        }

        private void AddBlankLine()
        {
            var ch = new Chunk( " " );
            _doc.Add( new Paragraph( ch ) );
        }

        private void NewPage()
        {
            _doc.NewPage();
        }

        public void Close()
        {
            _doc.Close();
        }
    }
}