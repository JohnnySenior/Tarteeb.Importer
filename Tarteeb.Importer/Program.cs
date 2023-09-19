//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using EFxceptions.Models.Exceptions;
using Tarteeb.Importer.Brokers.Storages;
using Tarteeb.Importer.Models.Clients;

namespace Tarteeb.Importer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var client = new Client
            {
                Id = Guid.NewGuid(),
                Firstname = "Johnny",
                Lastname = "Senior",
                PhoneNumber = "01076419505",
                Email = "jamshidbektursunboev@gmail.com",
                BirthDate = DateTimeOffset.Now,
            };

            using (var storageBroker = new StorageBroker())
            {
                storageBroker.Clients.Add(client);
                storageBroker.SaveChanges();
            }
        }
    }
}