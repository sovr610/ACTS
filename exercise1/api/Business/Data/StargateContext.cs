using Microsoft.EntityFrameworkCore;
using System.Data;

namespace StargateAPI.Business.Data
{
    public class StargateContext : DbContext
    {
        public IDbConnection Connection => Database.GetDbConnection();
        public DbSet<Person> People { get; set; }
        public DbSet<AstronautDetail> AstronautDetails { get; set; }
        public DbSet<AstronautDuty> AstronautDuties { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public StargateContext(DbContextOptions<StargateContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StargateContext).Assembly);

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Person data
            var persons = new List<Person>
    {
        new Person { Id = 1, Name = "John Doe" },
        new Person { Id = 2, Name = "Jane Doe" },
        new Person { Id = 3, Name = "Alice Johnson" },
        new Person { Id = 4, Name = "Bob Smith" },
        new Person { Id = 5, Name = "Carol Williams" },
        new Person { Id = 6, Name = "David Brown" },
        new Person { Id = 7, Name = "Eva Davis" },
        new Person { Id = 8, Name = "Frank Miller" },
        new Person { Id = 9, Name = "Grace Wilson" },
        new Person { Id = 10, Name = "Henry Taylor" },
        new Person { Id = 11, Name = "Ivy Anderson" },
        new Person { Id = 12, Name = "Jack Thompson" },
        new Person { Id = 13, Name = "Kelly Martinez" },
        new Person { Id = 14, Name = "Liam Harris" },
        new Person { Id = 15, Name = "Mia Clark" },
        new Person { Id = 16, Name = "Noah Lewis" },
        new Person { Id = 17, Name = "Olivia Lee" },
        new Person { Id = 18, Name = "Peter White" },
        new Person { Id = 19, Name = "Quinn Moore" },
        new Person { Id = 20, Name = "Rachel Green" },
        new Person { Id = 21, Name = "Samuel King" },
        new Person { Id = 22, Name = "Tina Turner" }
    };

            modelBuilder.Entity<Person>().HasData(persons);

            // Seed AstronautDetail data
            var astronautDetails = new List<AstronautDetail>
    {
        new AstronautDetail { Id = 1, PersonId = 1, CurrentRank = "1LT", CurrentDutyTitle = "Commander", CareerStartDate = DateTime.Now.AddYears(-5) },
        new AstronautDetail { Id = 2, PersonId = 2, CurrentRank = "CAPT", CurrentDutyTitle = "Mission Specialist", CareerStartDate = DateTime.Now.AddYears(-3) },
        new AstronautDetail { Id = 3, PersonId = 3, CurrentRank = "MAJ", CurrentDutyTitle = "Flight Engineer", CareerStartDate = DateTime.Now.AddYears(-7) },
        new AstronautDetail { Id = 4, PersonId = 4, CurrentRank = "COL", CurrentDutyTitle = "Payload Specialist", CareerStartDate = DateTime.Now.AddYears(-10) },
        new AstronautDetail { Id = 5, PersonId = 5, CurrentRank = "2LT", CurrentDutyTitle = "Pilot", CareerStartDate = DateTime.Now.AddYears(-2) },
        new AstronautDetail { Id = 6, PersonId = 6, CurrentRank = "CAPT", CurrentDutyTitle = "Mission Specialist", CareerStartDate = DateTime.Now.AddYears(-4) },
        new AstronautDetail { Id = 7, PersonId = 7, CurrentRank = "MAJ", CurrentDutyTitle = "Flight Surgeon", CareerStartDate = DateTime.Now.AddYears(-6) },
        new AstronautDetail { Id = 8, PersonId = 8, CurrentRank = "LTC", CurrentDutyTitle = "Commander", CareerStartDate = DateTime.Now.AddYears(-9) },
        new AstronautDetail { Id = 9, PersonId = 9, CurrentRank = "1LT", CurrentDutyTitle = "Mission Specialist", CareerStartDate = DateTime.Now.AddYears(-3) },
        new AstronautDetail { Id = 10, PersonId = 10, CurrentRank = "CAPT", CurrentDutyTitle = "Pilot", CareerStartDate = DateTime.Now.AddYears(-5) }
    };

            modelBuilder.Entity<AstronautDetail>().HasData(astronautDetails);

            // Seed AstronautDuty data
            var astronautDuties = new List<AstronautDuty>
    {
        new AstronautDuty { Id = 1, PersonId = 1, DutyStartDate = DateTime.Now.AddMonths(-6), DutyTitle = "Commander", Rank = "1LT", DutyEndDate = DateTime.Now.AddMonths(6) },
        new AstronautDuty { Id = 2, PersonId = 2, DutyStartDate = DateTime.Now.AddMonths(-3), DutyTitle = "Mission Specialist", Rank = "CAPT", DutyEndDate = DateTime.Now.AddMonths(9) },
        new AstronautDuty { Id = 3, PersonId = 3, DutyStartDate = DateTime.Now.AddMonths(-12), DutyTitle = "Flight Engineer", Rank = "MAJ", DutyEndDate = DateTime.Now.AddMonths(3) },
        new AstronautDuty { Id = 4, PersonId = 4, DutyStartDate = DateTime.Now.AddMonths(-9), DutyTitle = "Payload Specialist", Rank = "COL", DutyEndDate = DateTime.Now.AddMonths(12) },
        new AstronautDuty { Id = 5, PersonId = 5, DutyStartDate = DateTime.Now.AddMonths(-1), DutyTitle = "Pilot", Rank = "2LT", DutyEndDate = DateTime.Now.AddMonths(11) },
        new AstronautDuty { Id = 6, PersonId = 6, DutyStartDate = DateTime.Now.AddMonths(-4), DutyTitle = "Mission Specialist", Rank = "CAPT", DutyEndDate = DateTime.Now.AddMonths(8) },
        new AstronautDuty { Id = 7, PersonId = 7, DutyStartDate = DateTime.Now.AddMonths(-7), DutyTitle = "Flight Surgeon", Rank = "MAJ", DutyEndDate = DateTime.Now.AddMonths(5) },
        new AstronautDuty { Id = 8, PersonId = 8, DutyStartDate = DateTime.Now.AddMonths(-10), DutyTitle = "Commander", Rank = "LTC", DutyEndDate = DateTime.Now.AddMonths(2) },
        new AstronautDuty { Id = 9, PersonId = 9, DutyStartDate = DateTime.Now.AddMonths(-2), DutyTitle = "Mission Specialist", Rank = "1LT", DutyEndDate = DateTime.Now.AddMonths(10) },
        new AstronautDuty { Id = 10, PersonId = 10, DutyStartDate = DateTime.Now.AddMonths(-5), DutyTitle = "Pilot", Rank = "CAPT", DutyEndDate = DateTime.Now.AddMonths(7) },
        new AstronautDuty { Id = 11, PersonId = 11, DutyStartDate = DateTime.Now.AddMonths(-8), DutyTitle = "Flight Engineer", Rank = "MAJ", DutyEndDate = DateTime.Now.AddMonths(4) },
        new AstronautDuty { Id = 12, PersonId = 12, DutyStartDate = DateTime.Now.AddMonths(-11), DutyTitle = "Payload Specialist", Rank = "LTC", DutyEndDate = DateTime.Now.AddMonths(1) }
    };

            modelBuilder.Entity<AstronautDuty>().HasData(astronautDuties);
        }
    }
}
