using ManageFarm.Models;
using System.ComponentModel.DataAnnotations;

public class Staff
{
    public int StaffId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string ContactInfo { get; set; }

    [Required]
    public string Role { get; set; }

    [Range(0.0, (double)decimal.MaxValue, ErrorMessage = "Pay must be a posiive number")]
    public decimal Pay { get; set; }

    // Initialize the collection to avoid null reference errors
    public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
}
