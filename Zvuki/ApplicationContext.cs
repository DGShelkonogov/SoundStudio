using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zvuki.Models;

namespace Zvuki
{
    public class ApplicationContext : DbContext
    {
        public DbSet<AdType> AdTypes { get; set; }
        public DbSet<AdvertisingOrder> AdvertisingOrders { get; set; }
        public DbSet<AudioRecording> AudioRecordings { get; set; }
        public DbSet<AudioRecordingClient> AudioRecordingClients { get; set; }
        public DbSet<AudioRecordingGroup> AudioRecordingGroups { get; set; }
        public DbSet<BankAccount> BankAccount { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Cleaning> Cleanings { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Contract> Contracts{ get; set; }
        public DbSet<Copyright> Copyrights { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Human> Humans{ get; set; }
        public DbSet<PaymentAccount> PaymentAccounts { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<RecordingRoom> RecordingRooms { get; set; }
        public DbSet<Rent> Rents { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<VoiceActingRole> VoiceActingRoles { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Zvuki;Username=postgres;Password=123");
        }
    }
}
