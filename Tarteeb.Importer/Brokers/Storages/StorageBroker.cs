//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Threading.Tasks;
using EFxceptions;
using Microsoft.EntityFrameworkCore;
using Tarteeb.Importer.Models.Clients;

namespace Tarteeb.Importer.Brokers.Storages
{
    internal class StorageBroker : EFxceptionsContext
    {
        public DbSet<Client> Clients { get; set; }

        public StorageBroker() =>
            this.Database.EnsureCreated();

        public async Task<Client> InsertClientAsync(Client client)
        {
            await this.Clients.AddAsync(client);
            await this.SaveChangesAsync();

            return client;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Data Source=..\\..\\..\\Tarteeb.DB";
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
