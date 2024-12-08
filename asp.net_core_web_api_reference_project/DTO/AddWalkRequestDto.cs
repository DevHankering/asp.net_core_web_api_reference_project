using System.ComponentModel.DataAnnotations;

namespace asp.net_core_web_api_reference_project.DTO
{
    public class AddWalkRequestDto
    {
        [Required]
        [MaxLength(100)]
        //public Guid Id { get; set; } // ager ye yahan hoga to unique id generate nhee hongi, so remove it, kyonki dublicate id post ho jayegi.
        public string Name { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)] // we can also provide range as well  
        public string LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
