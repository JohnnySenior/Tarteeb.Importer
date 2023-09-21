//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Threading.Tasks;
using Tarteeb.Importer.Brokers.Storages;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Exceptions;

namespace Tarteeb.Importer.Services
{
    internal class ClientService
    {
        private readonly StorageBroker storageBroker;

        public ClientService() =>
            this.storageBroker = new StorageBroker();

        internal Task<Client> AddClientAsync(Client client)
        {
            if(client is null)
            {
                throw new ClientNullException();
            }
            return this.storageBroker.InsertClientAsync(client);
        }
    }
}
