//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tarteeb.Importer.Brokers.DateTimes;
using Tarteeb.Importer.Brokers.Storages;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Exceptions;

namespace Tarteeb.Importer.Services
{
    internal partial class ClientService
    {
        private readonly StorageBroker storageBroker;
        private readonly DateTimeBroker dateTimeBroker;

        public ClientService(StorageBroker storageBroker, DateTimeBroker dateTimeBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        /// <exception cref="ClientValidationException"></exception>

        internal Task<Client> AddClientAsync(Client client) =>
        TryCatch(() =>
        {
            ValidateClientOnAdd(client);

            return this.storageBroker.InsertClientAsync(client);
        });
    }
}
