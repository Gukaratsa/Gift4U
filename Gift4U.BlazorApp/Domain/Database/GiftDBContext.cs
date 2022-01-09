using Gift4U.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gift4U.Domain.Models
{
    public class GiftDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<GivenGift> GivenGifts { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestState> RequestStates { get; set; }

        public GiftDBContext(DbContextOptions options) 
            : base(options){ }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite($"Data Source=GiftDB.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gift>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<User>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<GivenGift>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<GivenGift>()
                .HasOne<User>(s => s.Giver)
                .WithMany(g => g.GivenGifts)
                .HasForeignKey(s => s.GiverId);

            modelBuilder.Entity<GivenGift>()
                .HasOne<User>(s => s.Receiver)
                .WithMany(g => g.ReceivedGifts)
                .HasForeignKey(s => s.ReceiverId); 

            modelBuilder.Entity<GivenGift>()
                .HasOne<Gift>(s => s.Gift)
                .WithMany(g => g.GivenGifts)
                .HasForeignKey(s => s.GiftId);

            modelBuilder.Entity<Request>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<RequestState>()
                .HasKey(s => s.Id);
            
            modelBuilder.Entity<RequestState>()
                .HasData(
                    new RequestState(){ Id = Guid.NewGuid(), Name = "Pending" },
                    new RequestState(){ Id = Guid.NewGuid(), Name = "RequestApproved" },
                    new RequestState(){ Id = Guid.NewGuid(), Name = "RequestDenied" },
                    new RequestState(){ Id = Guid.NewGuid(), Name = "RequestStarted" },
                    new RequestState(){ Id = Guid.NewGuid(), Name = "Completed" });

             modelBuilder.Entity<Request>()
                .HasOne<RequestState>(s => s.RequestState)
                .WithMany(g => g.Requests)
                .HasForeignKey(s => s.RequestStateId);

            modelBuilder.Entity<Request>()
                .HasOne<GivenGift>(s => s.GivenInRequest)
                .WithMany(g => g.Requests)
                .HasForeignKey(s => s.GivenInRequestId);
        }
    }
}
