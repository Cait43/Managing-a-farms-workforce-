using ManageFarm.Models;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Staff.Any()) return; // Already seeded

        var fields = new Field[]
        {
            new Field { Crop = "Strawberries" },
            new Field { Crop = "Strawberries" }
        };
        context.Fields.AddRange(fields);
        context.SaveChanges();

        var machines = new Machine[]
        {
            new Machine { TypeOfMachine = "Auto Harvester", Status = "Operational", NextMaintenanceCheck = DateTime.Now.AddDays(30), Field = fields[0] },
            new Machine { TypeOfMachine = "Auto Harvester", Status = "Under Maintenance", NextMaintenanceCheck = DateTime.Now.AddDays(10), Field = fields[1] }
        };
        context.Machines.AddRange(machines);
        context.SaveChanges();

        var staff = new Staff[]
        {
            new Staff { Name = "Alice", ContactInfo = "alice@example.com", Pay = 3000, Role = "Harvester" },
            new Staff { Name = "Bob", ContactInfo = "bob@example.com", Pay = 3500, Role = "Technician" }
        };
        context.Staff.AddRange(staff);
        context.SaveChanges();

        var assignments = new Assignment[]
        {
            new Assignment { Staff = staff[0], Field = fields[0], Machine = machines[0], Task = "Harvest strawberries" },
            new Assignment { Staff = staff[1], Field = fields[1], Machine = machines[1], Task = "Maintain harvester" }
        };
        context.Assignments.AddRange(assignments);
        context.SaveChanges();
    }
}
