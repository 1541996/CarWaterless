using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Infra.Models
{
    public partial class CarWaterLessContext : DbContext
    {
        //static CarWaterLessContext()
        //{
        //    Database.SetInitializer<CarWaterLessContext>(null);
        //}

        //public CarWaterLessContext()
        //    : base("Name=CarWaterLessContext")
        //{
        //}

        private const string CarWaterLessDbContext = "CarWaterLessContext";
        public CarWaterLessContext()
          : base(CarWaterLessDbContext)
        {

        }

        public DbSet<tbAdmin> tbAdmins { get; set; }
        public DbSet<tbBranch> tbBranches { get; set; }
        public DbSet<tbCarCategory> tbCarCategories { get; set; }
        public DbSet<tbChatMessage> tbChatMessages { get; set; }
        public DbSet<tbCustomer> tbCustomers { get; set; }
        public DbSet<tbCustomerVehicle> tbCustomerVehicles { get; set; }
        public DbSet<tbDiscount> tbDiscounts { get; set; }
        public DbSet<tbDiscountedCar> tbDiscountedCars { get; set; }
        public DbSet<tbFinance> tbFinances { get; set; }
        public DbSet<tbNotification> tbNotifications { get; set; }
        public DbSet<tbOperation> tbOperations { get; set; }
        public DbSet<tbRank> tbRanks { get; set; }
        public DbSet<tbAdditionalService> tbAdditionalServices { get; set; }
        public DbSet<tbTopCustomer> tbTopCustomers { get; set; }
        public DbSet<tbTownship> tbTownships { get; set; }
        public DbSet<tbPhoto> tbPhotos { get; set; }
        public DbSet<tbFeedBack> tbFeedBacks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Ignore field map
            modelBuilder.Entity<tbBranch>().Ignore(t => t.PhotoUrl);
        }
    }
}
