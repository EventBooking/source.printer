using System.Collections.Generic;

namespace SourcePrinter.Config
{
    public class Config : IConfig
    {
        public string ProductName { get; set; }
        public string SourcePath { get; set; }
        public string TargetFile { get; set; }
        public IEnumerable<string> Extensions { get; set; } = new List<string> { ".cs" };

        public IEnumerable<string> DirectoryBlacklist { get; set; } = new List<string>
        {
            ".cs",
            "obj",
            "bin",
            "packages",
            "generated",
            "_generated",
            "artifacts",
            "vendor",
            "bower_components",
            "node_modules",
            "typings",
            "wwwroot",
            "T4Gen",
        };
    }
}