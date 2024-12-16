using System.ComponentModel.DataAnnotations;

namespace asp.net_core_web_api_reference_project.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]  // --> this means --> username will be treated as email as well
        public string Username { get; set; } // because we want to use username as an email address as well
        [Required]
        [DataType (DataType.Password)]
        public string Password { get; set; }
        public string[] Roles { get; set; }

    }
}
