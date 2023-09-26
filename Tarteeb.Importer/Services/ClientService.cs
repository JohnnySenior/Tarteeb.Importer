//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
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

        /// <exception cref="ClientNullException"></exception>
        /// <exception cref="InvalidCastException"></exception>

        internal Task<Client> AddClientAsync(Client client)
        {
            if(client is null)
            {
                throw new ClientNullException();
            }

            Validate(
                (Rule: IsInvalid(client.Id), Parameter: nameof(Client.Id)),
                (Rule: IsInvalid(client.Firstname), Parameter: nameof(Client.Firstname)),
                (Rule: IsInvalid(client.Lastname), Parameter: nameof(Client.Lastname)),
                (Rule: IsInvalid(client.Email), Parameter: nameof(Client.Email)),
                (Rule: IsInvalid(client.GroupId), Parameter: nameof(Client.GroupId)));

            Validate(
                (Rule: IsInvalidEmail(client.Email), Parameter: nameof(Client.Email)));

            return this.storageBroker.InsertClientAsync(client);
        }

        private dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private dynamic IsInvalid(Guid id) => new
        {
            Condition = id == default,
            Message = "Id is required"
        };

        private dynamic IsInvalidEmail(string email) => new
        {
            Condition = !Regex.IsMatch(email, "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$\r\n"),
            Message = "Email is invalid"
        };

        private void Validate(params(dynamic Rule, string Parameter)[] validations)
        {
            var invalidClientException = new InvalidClientException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if(rule.Condition)
                {
                    invalidClientException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidClientException.ThrowIfContainsErrors();
        }
    }
}
