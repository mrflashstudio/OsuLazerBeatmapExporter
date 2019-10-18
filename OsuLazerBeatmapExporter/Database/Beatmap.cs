using System.Collections.Generic;

namespace OsuLazerBeatmapExporter.Database
{
    public class Beatmap
    {
        public int BeatmapSetID { get; set; }
        public string BeatmapSetName { get; set; }
        public List<OsuFile> Files = new List<OsuFile>();
    }
}
