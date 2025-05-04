using System;


namespace ManageFarm.Models
{
    public class Machine
    {
        public int MachineId { get; set; }
        public required string TypeOfMachine { get; set; }
        public DateTime NextMaintenanceCheck { get; set; }
        public required string Status { get; set; }

        public int FieldId { get; set; }
        public Field? Field { get; set; }
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    }
}

