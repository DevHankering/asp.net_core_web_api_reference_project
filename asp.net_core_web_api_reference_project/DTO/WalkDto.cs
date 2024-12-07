namespace asp.net_core_web_api_reference_project.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string LengthInKm { get; set; }
        public string WalkImageUrl { get; set; }
        //public Guid DifficultyId { get; set; } // since we already have information about ID inside navigation property we don't need this
        //public Guid RegionId { get; set; }     // since we already have information about ID inside navigation property we don't need this
        public RegionDto Region { get; set; }
        public DifficultyDto Difficulty { get; set; }
    }
}
