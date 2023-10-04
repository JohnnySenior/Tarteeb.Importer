//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bogus;
using EFxceptions.Models.Exceptions;
using Tarteeb.Importer.Brokers.DateTimes;
using Tarteeb.Importer.Brokers.Loggings;
using Tarteeb.Importer.Brokers.Storages;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Exceptions;
using Tarteeb.Importer.Services;

namespace Tarteeb.Importer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var faceClient = new Faker();

            for (int i = 0; i < 2000; i++)
            {
                Client client = new Client
                {
                    Id = faceClient.Random.Guid(),
                    Firstname = faceClient.Name.FirstName(),
                    Lastname = faceClient.Name.LastName(),
                    PhoneNumber = faceClient.Phone.PhoneNumber(),
                    Email = faceClient.Internet.Email(),
                    BirthDate = faceClient.Date.Between(new DateTime(1995, 1, 1), new DateTime(2011, 12, 31)),
                    GroupId = faceClient.Random.Guid(),
                };

                var loggingBroker = new LoggingBroker();
                var clientService = new ClientService(new StorageBroker(), new DateTimeBroker());

                try
                {
                    var persistedClient = await clientService.AddClientAsync(client);
                }
                catch (ClientValidationException clientValidationException)
                {
                    loggingBroker.LogError(clientValidationException.InnerException.Message);

                    foreach (DictionaryEntry entry in clientValidationException.InnerException.Data)
                    {
                        string errorSummary = string.Join(",", (List<string>)entry.Value);

                        loggingBroker.LogError(entry.Key + " => " + errorSummary);
                    }
                }
                catch (ClientDependencyValidationException clientDependencyValidationException)
                {
                    loggingBroker.LogError(clientDependencyValidationException.InnerException.Message);
                }
                catch (ClientDependencyException clientDependencyException)
                {
                    loggingBroker.LogError(clientDependencyException.InnerException.Message);
                }
                catch (ClientServiceException clientServiceException)
                {
                    loggingBroker.LogError(clientServiceException.InnerException.Message);
                }
            }
        }
    }
}