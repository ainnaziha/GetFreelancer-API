using System.ComponentModel.DataAnnotations;

namespace GetFreelancer_API.Models
{
    public class Freelancer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Skillset { get; set; }
        public string hobby { get; set; }
    }
}
