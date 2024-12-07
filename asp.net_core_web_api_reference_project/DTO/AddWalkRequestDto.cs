namespace asp.net_core_web_api_reference_project.DTO
{
    public class AddWalkRequestDto
    {
        //public Guid Id { get; set; } // ager ye yahan hoga to unique id generate nhee hongi, so remove it, kyonki dublicate id post ho jayegi.
        public string Name { get; set; }
        public string Description { get; set; }

        public string LengthInKm { get; set; }
        public string WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
