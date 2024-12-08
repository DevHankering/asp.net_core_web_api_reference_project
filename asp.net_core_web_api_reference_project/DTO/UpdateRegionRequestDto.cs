using System.ComponentModel.DataAnnotations;

namespace asp.net_core_web_api_reference_project.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be minimum of three characters")] // this sets the min length, it should take, as a optional, you can also error message as well, but it is optional
        [MaxLength(10, ErrorMessage = "Code has to be minimum of ten characters")] // this sets the maximum length, it should take, set it to 3 as well, if you want only three length. // error message is optional

        public string Code { get; set; }
        [Required] // it means, the value of Name can not be null
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters")] // if we get a null value here, autmatically "name can not be a null value will be printed in ModelState object

        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
