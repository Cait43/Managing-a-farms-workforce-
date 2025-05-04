namespace ManageFarm.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }

        public int StaffId { get; set; }
        public Staff? Staff { get; set; }  // Navigation property for Staff

        public int? MachineId { get; set; }
        public Machine? Machine { get; set; }

        public int FieldId { get; set; }  // Foreign key for Field
        public Field? Field { get; set; }  // Navigation property for Field

        public required string Task { get; set; }
    }
}
