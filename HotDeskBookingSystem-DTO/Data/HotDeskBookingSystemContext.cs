namespace SoftwareMind_Intern_ChallengeDTO.Data
{
    using Microsoft.EntityFrameworkCore;
    using SoftwareMind_Intern_ChallengeDTO.DataObjects;

    /// <summary>
    /// Hot desk booking system context.
    /// </summary>
    public class HotDeskBookingSystemContext : DbContext, IHotDeskBookingSystemContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HotDeskBookingSystemContext"/> class.
        /// </summary>
        /// <param name="options">
        /// Database context options.
        /// </param>
        public HotDeskBookingSystemContext(DbContextOptions<HotDeskBookingSystemContext> options) : base(options)
        {
            if (!this.Database.CanConnect())
            {
                this.Database.EnsureCreated();
            }
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Employee>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Employee>()
                .Property(e => e.Role)
                .HasColumnType("nvarchar(255)")
                .HasDefaultValue("user");
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Reservations)
                .WithOne(r => r.Employee);

            modelBuilder.Entity<Reservation>()
                .HasKey(r => r.Id);
            modelBuilder.Entity<Reservation>()
                .Property(r => r.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Reservation>()
                .Property(r => r.DateStart)
                .IsRequired();
            modelBuilder.Entity<Reservation>()
                .Property(r => r.DateEnd)
                .IsRequired();
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Desk)
                .WithMany(d => d.Reservations);

            modelBuilder.Entity<Desk>()
                .HasKey(d => d.Id);
            modelBuilder.Entity<Desk>()
                .Property(d => d.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Desk>()
                .Property(d => d.IsAvailable)
                .HasDefaultValue("true");
            modelBuilder.Entity<Desk>()
                .HasOne(d => d.Location)
                .WithMany(l => l.Desks)
                .HasForeignKey(d => d.LocationId)
                .HasPrincipalKey(l => l.Id);
            modelBuilder.Entity<Desk>()
                .HasMany(d => d.Reservations)
                .WithOne(r => r.Desk);

            modelBuilder.Entity<Location>()
                .HasKey(l => l.Id);
            modelBuilder.Entity<Location>()
                .Property(l => l.Name)
                .IsRequired()
                .HasColumnType("nvarchar(255)");
            modelBuilder.Entity<Location>()
                .Property(l => l.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Location>()
                .HasMany(l => l.Desks)
                .WithOne(d => d.Location);

            Employee admin = new Employee
            {
                Id = 1,
                Role = "admin",
                Email = "admin@admin.com",
                Password = "Admin*123",
            };
            Employee normalUser = new Employee
            {
                Id = 2,
                Role = "user",
                Email = "test@test.com",
                Password = "Test*123",
            };

            Location unusedDesks = new Location { Id = 1, Name = "Unused desks" };

            modelBuilder.Entity<Employee>().HasData(admin, normalUser);
            modelBuilder.Entity<Location>().HasData(unusedDesks);
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Gets or sets dbSet of Desks.
        /// </summary>
        public DbSet<Desk> Desks { get; set; }

        /// <summary>
        /// Gets or sets dbSet of Employees.
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Gets or sets dbSet of Locations.
        /// </summary>
        public DbSet<Location> Locations { get; set; }

        /// <summary>
        /// Gets or sets dbSet of Reservations.
        /// </summary>
        public DbSet<Reservation> Reservations { get; set; }
    }
}
