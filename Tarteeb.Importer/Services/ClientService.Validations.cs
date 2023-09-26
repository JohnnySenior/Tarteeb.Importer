//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Text.RegularExpressions;
using System;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Exceptions;

namespace Tarteeb.Importer.Services
{
    internal partial class ClientService
    {
        private void ValidateClientOnAdd(Client client)
        {
            ClientNotNull(client);

            Validate(
                (Rule: IsInvalid(client.Id), Parameter: nameof(Client.Id)),
                (Rule: IsInvalid(client.Firstname), Parameter: nameof(Client.Firstname)),
                (Rule: IsInvalid(client.Lastname), Parameter: nameof(Client.Lastname)),
                (Rule: IsInvalid(client.Email), Parameter: nameof(Client.Email)),
                (Rule: IsInvalid(client.BirthDate), Parameter: nameof(Client.BirthDate)),
                (Rule: IsAgeLess12(client.BirthDate), Parameter: nameof(Client.BirthDate)),
                (Rule: IsInvalid(client.GroupId), Parameter: nameof(Client.GroupId)));

            Validate(
                (Rule: IsInvalidEmail(client.Email), Parameter: nameof(Client.Email)));
        }
        private static void ClientNotNull(Client client)
        {
            if (client is null)
            {
                throw new ClientNullException();
            }
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

        private dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private dynamic IsAgeLess12(DateTimeOffset date) => new
        {
            Condition = IsAgeLessThan12(date),
            Message = "Age is less than 12"
        };

        private bool IsAgeLessThan12(DateTimeOffset date)
        {
            DateTimeOffset now = this.dateTimeBroker.GetCurrentDateTimeOffset();
            int age = (now - date).Days / 365;

            return age < 12;
        }

        private dynamic IsInvalidEmail(string email) => new
        {
            Condition = !Regex.IsMatch(email,
                "[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"),

            Message = "Email is invalid"
        };

        private void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidClientException = new InvalidClientException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
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
