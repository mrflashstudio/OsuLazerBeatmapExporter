namespace OsuLazerBeatmapExporter.Database
{
    public class OsuFileInfo
    {
        public int ID { get; set; }
        public string Hash { get; set; }
        public int ReferenceCount { get; set; }
    }
}
