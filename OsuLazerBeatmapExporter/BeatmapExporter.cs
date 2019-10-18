using OsuLazerBeatmapExporter.Database;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace OsuLazerBeatmapExporter
{
    public class BeatmapExporter
    {
        public void Export(string pathToLazer, string outputPath, ExportType exportType)
        {
            var maps = ParseDatabase($@"{pathToLazer}\client.db");

            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            string pathToFiles = $@"{pathToLazer}\files";

            int counter = 0;
            foreach (var map in maps)
            {
                counter++;
                Console.WriteLine($"Exporting {counter} beatmapset out of {maps.Count()}... ({map.BeatmapSetName})");

                string beatmapDirectory = $@"{outputPath}\{map.BeatmapSetName}";

                if (!Directory.Exists(beatmapDirectory) && exportType == ExportType.Folder)
                    Directory.CreateDirectory(beatmapDirectory);

                foreach (var file in map.Files)
                {
                    var filePath = Directory.GetFiles(pathToFiles, file.Hash, SearchOption.AllDirectories).FirstOrDefault();

                    if (filePath != default)
                    {
                        if (string.IsNullOrEmpty(Path.GetFileName(file.FileName)) || string.IsNullOrWhiteSpace(Path.GetFileName(file.FileName)))
                            continue;

                        var fileDirectory = Path.GetDirectoryName(file.FileName);

                        if (exportType == ExportType.Folder)
                        {
                            if (!Directory.Exists($@"{beatmapDirectory}\{fileDirectory}"))
                                Directory.CreateDirectory($@"{beatmapDirectory}\{fileDirectory}");

                            File.Copy(filePath, $@"{beatmapDirectory}\{file.FileName}");
                        }
                        else
                            using (ZipArchive zip = ZipFile.Open($"{beatmapDirectory}.osz", ZipArchiveMode.Update))
                                zip.CreateEntryFromFile(filePath, file.FileName);
                    }
                }
            }
        }

        private IEnumerable<Beatmap> ParseDatabase(string pathToDb)
        {
            var connection = new SQLiteConnection(pathToDb);
            var setInfo = connection.Query<BeatmapSetInfo>("Select * from BeatmapSetInfo").Where(s => s.OnlineBeatmapSetID > 0 && s.Protected == 0);
            var setFileInfo = connection.Query<BeatmapSetFileInfo>("Select * from BeatmapSetFileInfo").Where(s => s.FileInfoID > 0);
            var fileInfo = connection.Query<OsuFileInfo>("Select * from FileInfo");

            var maps = new List<Beatmap>();
            foreach (var set in setInfo)
            {
                var map = new Beatmap 
                { 
                    BeatmapSetID = set.OnlineBeatmapSetID, 
                    BeatmapSetName = set.OnlineBeatmapSetID.ToString()
                };
                
                foreach (var file in setFileInfo.Where(s => s.BeatmapSetInfoID == set.ID))
                {
                    var foundFile = fileInfo.Find(f => f.ID == file.FileInfoID);
                    if (foundFile != default)
                    {
                        map.Files.Add(new OsuFile { FileName = file.FileName, Hash = foundFile.Hash });

                        if (Path.GetExtension(file.FileName) == ".osu" && map.BeatmapSetName == map.BeatmapSetID.ToString())
                            map.BeatmapSetName += $" {file.FileName.Substring(0, file.FileName.LastIndexOf('('))}";
                    }
                }

                map.BeatmapSetName = map.BeatmapSetName.Trim();

                maps.Add(map);
            }

            return maps;
        }
    }

    public enum ExportType
    {
        Folder,
        Osz
    }
}
