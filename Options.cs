// Copyright 2013 EventBooking.com, LLC. All rights reserved.

using Plossum.CommandLine;

namespace SourcePrinter
{
	[CommandLineManager( ApplicationName = "source_printer",
		Copyright = "Copyright 2013 EventBooking.com, LLC" )]
	public class Options
	{
		[CommandLineOption( Description = "Displays this help text" )] public bool Help;
		[CommandLineOption( Description = "Root path of the source project" )] public string Source;
		[CommandLineOption( Description = "Path to target document" )] public string Target;
	}
}