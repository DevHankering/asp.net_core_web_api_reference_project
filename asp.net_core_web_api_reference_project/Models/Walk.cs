namespace asp.net_core_web_api_reference_project.Models
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string LengthInKm { get; set; }
        public string WalkImageUrl { get; set; }

        //Foreign Id
        public Guid DifficultyId {  get; set; }
        public Guid RegionId { get; set; }

        //Navigation property
        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
    }
}
