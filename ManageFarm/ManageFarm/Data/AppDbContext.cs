using Microsoft.EntityFrameworkCore;
using ManageFarm.Models;




    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // dbSets are automatically initialized by EF Core, no need to manually set them
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        // configure the model with relationships, etc.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // mapping table names (optional)
            modelBuilder.Entity<Staff>().ToTable("Staff");
            modelBuilder.Entity<Field>().ToTable("Field");
            modelBuilder.Entity<Machine>().ToTable("Machines");
            modelBuilder.Entity<Assignment>().ToTable("Assignment");

            // configuring relationships between entities (many-to-one relationships)
            modelBuilder.Entity<Machine>()
                .HasOne(m => m.Field)
                .WithMany(f => f.Machines)
                .HasForeignKey(m => m.FieldId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Machine)
                .WithMany(m => m.Assignments)
                .HasForeignKey(a => a.MachineId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Staff)
                .WithMany(s => s.Assignments)
                .HasForeignKey(a => a.StaffId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Field)
                .WithMany(f => f.Assignments)
                .HasForeignKey(a => a.FieldId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

