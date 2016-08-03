using System;
using System.IO;
using System.Security.Cryptography;

namespace SourcePrinter.FileReading
{
    public class FileGlommer : IFileGlommer
    {
        public SourceFileModel GlomTheFile( string fullPath )
        {
            var fi = new FileInfo( fullPath );
            if (!fi.Exists)
                throw new Exception( $"The file {fi.FullName} does not exist!" );

            var content = File.ReadAllLines( fullPath );
            var checksum = CheckMd5( fullPath );

            return new SourceFileModel
            {
                FullPath = fullPath,
                Lines = content,
                Checksum = checksum
            };
        }

        private static string CheckMd5( string fullPath )
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead( fullPath ))
                {
                    var checksum = md5.ComputeHash( stream );
                    var text = BitConverter.ToString( checksum ).Replace( "-", "" ).ToLower();
                    return text;
                }
            }
        }
    }
}