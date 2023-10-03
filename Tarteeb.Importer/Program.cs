//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            Client client = new Client
            {
                //Id = Guid.NewGuid(),
                Firstname = "Johnny",
                Lastname = "Senior",
                PhoneNumber = "01076419505",
                Email = "jamshidbektursunboev@gmail.com",
                BirthDate = DateTimeOffset.Parse("21/05/1995"),
                GroupId = Guid.NewGuid(),
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
            catch(ClientDependencyValidationException clientDependencyValidationException)
            {
                loggingBroker.LogError(clientDependencyValidationException.InnerException.Message);
            }
            catch(ClientDependencyException clientDependencyException)
            {
                loggingBroker.LogError(clientDependencyException.InnerException.Message);
            }
            catch(ClientServiceException clientServiceException)
            {
                loggingBroker.LogError(clientServiceException.InnerException.Message);
            }
        }
    }
}