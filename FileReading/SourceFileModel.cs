using System.Collections.Generic;

namespace SourcePrinter.FileReading
{
    public class SourceFileModel
    {
        public string FullPath { get; set; }
        public string RelativePath { get; set; }
        public IEnumerable<string> Lines { get; set; } = new List<string>();
        public string Checksum { get; set; }
    }
}