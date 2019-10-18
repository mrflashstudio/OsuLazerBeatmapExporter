using System;

namespace OsuLazerBeatmapExporter
{
    class Program
    {
        private static BeatmapExporter beatmapExporter;

        static void Main(string[] args)
        {
            beatmapExporter = new BeatmapExporter();

            ExportType exportType = ExportType.Folder;
            string pathToLazer = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\osu";
            string outputPath = @"Beatmaps";

            for (int i = 0; i < args.Length; i++)
            {
                if (i + 1 >= args.Length)
                    break;

                string arg = args[i];
                string nextArg = args[i + 1];

                if (arg.StartsWith('-'))
                {
                    switch (arg)
                    {
                        case "-e":
                        case "-t":
                        case "-type":
                        case "-exportType":
                            exportType = nextArg.ToLower() == "osz" ? ExportType.Osz : ExportType.Folder;
                            break;
                        case "-l":
                        case "-lazer":
                        case "-lazerPath":
                            pathToLazer = string.IsNullOrEmpty(nextArg) ? pathToLazer : nextArg;
                            break;
                        case "-o":
                        case "-output":
                        case "-outputPath":
                            outputPath = string.IsNullOrEmpty(nextArg) ? outputPath : nextArg;
                            break;
                    }
                }
            }

            Console.WriteLine($"Export type - {exportType}");
            Console.WriteLine($"Path to osu!lazer - {pathToLazer}");
            Console.WriteLine($"Output path - {outputPath}");

            Console.WriteLine("\nExporting your beatmaps...");

            beatmapExporter.Export(pathToLazer, outputPath, exportType);
        }
    }
}
