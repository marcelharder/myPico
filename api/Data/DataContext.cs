using DatingApp.API.Models;
using myPicoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data {
    public class DataContext : DbContext {
        public DataContext (DbContextOptions<DataContext> options) : base (options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<dateNumber> DateNumbers { get; set; }
        public DbSet<dateOccupancy> DateOccupancy { get; set; }
        public DbSet<picoUnit> PicoUnits { get; set; }
        public DbSet<Model_Currency> Currency { get; set; }


        protected override void OnModelCreating (ModelBuilder builder) {
            builder.Entity<Message> ()
                .HasOne (u => u.Sender)
                .WithMany (u => u.MessagesSent)
                .OnDelete (DeleteBehavior.Restrict);

            builder.Entity<Message> ()
                .HasOne (u => u.Recipient)
                .WithMany (u => u.MessagesReceived)
                .OnDelete (DeleteBehavior.Restrict);
        }

    }
}