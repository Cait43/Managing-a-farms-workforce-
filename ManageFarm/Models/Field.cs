using ManageFarm.Models;

public class Field
{
    public int FieldId { get; set; }  
    public string Crop { get; set; }

    // Navigation properties
    public ICollection<Machine> Machines { get; set; } = new List<Machine>();
    public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
}
