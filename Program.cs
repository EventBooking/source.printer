﻿using System;
using System.IO;
using Plossum.CommandLine;

namespace SourcePrinter
{
	internal class Program
	{
		private static int Main()
		{
			var options = new Options();
			var parser = new CommandLineParser(options);
			parser.Parse();

			if (options.Help || string.IsNullOrWhiteSpace(options.Source))
			{
				Console.WriteLine(parser.UsageInfo.GetOptionsAsString(78));
				return 0;
			}
			if (parser.HasErrors)
			{
				Console.WriteLine(parser.UsageInfo.GetErrorsAsString(78));
				return -1;
			}
			if (!Directory.Exists(options.Source))
			{
				Console.WriteLine("Specified source path does not exist!");
				Console.WriteLine(parser.UsageInfo.GetOptionsAsString(78));
				return -1;
			}

			var sp = new SourcePrinter(options.Source);
			sp.WriteToOdt(options.Target);
			return 0;
		}
	}
}