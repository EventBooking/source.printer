using Plossum.CommandLine;

namespace SourcePrinter.Config
{
    [CommandLineManager( ApplicationName = "source_printer",
        Copyright = "Copyright 2013-2016 EventBooking.com, LLC" )]
    public class Options
    {
        [CommandLineOption( Description = "Displays this help text" )] public bool Help;
        [CommandLineOption( Description = "Root path of the source project" )] public string Source;
        [CommandLineOption( Description = "Path to target document" )] public string Target;
        [CommandLineOption( Description = "Name of product" )] public string Product;
    }
}