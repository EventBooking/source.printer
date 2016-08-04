using System.Collections.Generic;

namespace SourcePrinter.Config
{
    public interface IConfig
    {
        string ProductName { get; }
        string SourcePath { get; }
        string TargetFile { get; }
        IEnumerable<string> Extensions { get; }
        IEnumerable<string> DirectoryBlacklist { get; }
    }
}