using System;
using System.IO;
using Plossum.CommandLine;
using SourcePrinter.Config;
using SourcePrinter.FileReading;
using SourcePrinter.Formatting;

namespace SourcePrinter
{
    public class Program
    {
        public static int Main()
        {
            var options = new Options();
            var parser = new CommandLineParser( options );
            parser.Parse();

            if (options.Help || string.IsNullOrWhiteSpace( options.Source ))
            {
                Console.WriteLine( parser.UsageInfo.GetOptionsAsString( 78 ) );
                return 0;
            }
            if (parser.HasErrors)
            {
                Console.WriteLine( parser.UsageInfo.GetErrorsAsString( 78 ) );
                return -1;
            }
            if (!Directory.Exists( options.Source ))
            {
                Console.WriteLine( "Specified source path does not exist!" );
                Console.WriteLine( parser.UsageInfo.GetOptionsAsString( 78 ) );
                return -1;
            }

            var config = new Config.Config
            {
                SourcePath = options.Source,
                TargetFile = options.Target,
                ProductName = options.Product,
            };

            var sp = new Application.SourcePrinter( config, new FileGlommer(), new SourceFormatterFactory() );
            sp.WriteToPdf();

            return 0;
        }
    }
}