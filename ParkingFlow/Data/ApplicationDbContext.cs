using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParkingFlow.Models;

namespace ParkingFlow.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ParkingSlots> ParkingSlots { get; set; }
        public DbSet<Bookings> Bookings { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        // Seed parking slots data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the primary key for Bookings
            modelBuilder.Entity<Bookings>()
                .HasOne(b => b.ParkingSlot)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.ParkingSlotId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CartItem>()
                .Property(c => c.TotalPrice)
                .HasPrecision(18, 2);
            /* Seed sample data for Bookings
            modelBuilder.Entity<Bookings>().HasData(
                new Bookings
                {
                    Id = 1,
                    UserId = "test-user",
                    ParkingSlotId = 1,
                    BookingDate = new DateTime(2025, 4, 7),
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(10, 0, 0)
                }
                );*/

            // Seed data for ParkingSlots
            modelBuilder.Entity<ParkingSlots>().HasData(
                // Kensington Ave
                new ParkingSlots { Id = 1, SlotCode = "K1", Location = "Kensington Avenue", IsVacant = true },
                new ParkingSlots { Id = 2, SlotCode = "K2", Location = "Kensington Avenue", IsVacant = true },
                new ParkingSlots { Id = 3, SlotCode = "K3", Location = "Kensington Avenue", IsVacant = true },
                new ParkingSlots { Id = 4, SlotCode = "K4", Location = "Kensington Avenue", IsVacant = true },
                new ParkingSlots { Id = 5, SlotCode = "K5", Location = "Kensington Avenue", IsVacant = true },
                new ParkingSlots { Id = 6, SlotCode = "K6", Location = "Kensington Avenue", IsVacant = true },
                new ParkingSlots { Id = 7, SlotCode = "K7", Location = "Kensington Avenue", IsVacant = true },
                new ParkingSlots { Id = 8, SlotCode = "K8", Location = "Kensington Avenue", IsVacant = true },
                new ParkingSlots { Id = 9, SlotCode = "K9", Location = "Kensington Avenue", IsVacant = true },
                new ParkingSlots { Id = 10, SlotCode = "K10", Location = "Kensington Avenue", IsVacant = true },
                new ParkingSlots { Id = 11, SlotCode = "K11", Location = "Kensington Avenue", IsVacant = true },
                new ParkingSlots { Id = 12, SlotCode = "K12", Location = "Kensington Avenue", IsVacant = true },

                // Huia Street
                new ParkingSlots { Id = 13, SlotCode = "H1", Location = "Huia Street", IsVacant = true },
                new ParkingSlots { Id = 14, SlotCode = "H2", Location = "Huia Street", IsVacant = true },
                new ParkingSlots { Id = 15, SlotCode = "H3", Location = "Huia Street", IsVacant = true },
                new ParkingSlots { Id = 16, SlotCode = "H4", Location = "Huia Street", IsVacant = true },
                new ParkingSlots { Id = 17, SlotCode = "H5", Location = "Huia Street", IsVacant = true },
                new ParkingSlots { Id = 18, SlotCode = "H6", Location = "Huia Street", IsVacant = true },
                new ParkingSlots { Id = 19, SlotCode = "H7", Location = "Huia Street", IsVacant = true },
                new ParkingSlots { Id = 20, SlotCode = "H8", Location = "Huia Street", IsVacant = true },
                new ParkingSlots { Id = 21, SlotCode = "H9", Location = "Huia Street", IsVacant = true },
                new ParkingSlots { Id = 22, SlotCode = "H10", Location = "Huia Street", IsVacant = true },
                new ParkingSlots { Id = 23, SlotCode = "H11", Location = "Huia Street", IsVacant = true },
                new ParkingSlots { Id = 24, SlotCode = "H12", Location = "Huia Street", IsVacant = true },

                // Udy Street
                new ParkingSlots { Id = 25, SlotCode = "U1", Location = "Udy Street", IsVacant = true },
                new ParkingSlots { Id = 26, SlotCode = "U2", Location = "Udy Street", IsVacant = true },
                new ParkingSlots { Id = 27, SlotCode = "U3", Location = "Udy Street", IsVacant = true },
                new ParkingSlots { Id = 28, SlotCode = "U4", Location = "Udy Street", IsVacant = true },
                new ParkingSlots { Id = 29, SlotCode = "U5", Location = "Udy Street", IsVacant = true },
                new ParkingSlots { Id = 30, SlotCode = "U6", Location = "Udy Street", IsVacant = true },
                new ParkingSlots { Id = 31, SlotCode = "U7", Location = "Udy Street", IsVacant = true },
                new ParkingSlots { Id = 32, SlotCode = "U8", Location = "Udy Street", IsVacant = true },
                new ParkingSlots { Id = 33, SlotCode = "U9", Location = "Udy Street", IsVacant = true },
                new ParkingSlots { Id = 34, SlotCode = "U10", Location = "Udy Street", IsVacant = true },
                new ParkingSlots { Id = 35, SlotCode = "U11", Location = "Udy Street", IsVacant = true },
                new ParkingSlots { Id = 36, SlotCode = "U12", Location = "Udy Street", IsVacant = true },

                // Atiawa Street
                new ParkingSlots { Id = 37, SlotCode = "A1", Location = "Atiawa Street", IsVacant = true },
                new ParkingSlots { Id = 38, SlotCode = "A2", Location = "Atiawa Street", IsVacant = true },
                new ParkingSlots { Id = 39, SlotCode = "A3", Location = "Atiawa Street", IsVacant = true },
                new ParkingSlots { Id = 40, SlotCode = "A4", Location = "Atiawa Street", IsVacant = true },
                new ParkingSlots { Id = 41, SlotCode = "A5", Location = "Atiawa Street", IsVacant = true },
                new ParkingSlots { Id = 42, SlotCode = "A6", Location = "Atiawa Street", IsVacant = true },
                new ParkingSlots { Id = 43, SlotCode = "A7", Location = "Atiawa Street", IsVacant = true },
                new ParkingSlots { Id = 44, SlotCode = "A8", Location = "Atiawa Street", IsVacant = true },
                new ParkingSlots { Id = 45, SlotCode = "A9", Location = "Atiawa Street", IsVacant = true },
                new ParkingSlots { Id = 46, SlotCode = "A10", Location = "Atiawa Street", IsVacant = true },
                new ParkingSlots { Id = 47, SlotCode = "A11", Location = "Atiawa Street", IsVacant = true },
                new ParkingSlots { Id = 48, SlotCode = "A12", Location = "Atiawa Street", IsVacant = true }

                );
        }
    }
}
