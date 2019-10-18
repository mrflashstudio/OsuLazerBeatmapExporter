namespace OsuLazerBeatmapExporter.Database
{
    public class BeatmapSetInfo
    {
        public int ID { get; set; }
        public int DeletePending { get; set; }
        public string Hash { get; set; }
        public int MetadataID { get; set; }
        public int OnlineBeatmapSetID { get; set; }
        public int Protected { get; set; }
        public int Status { get; set; }
        public string DateAdded { get; set; }
    }
}
