//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System.Threading.Tasks;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Exceptions;

namespace Tarteeb.Importer.Services
{
    internal partial class ClientService
    {
        private delegate Task<Client> ReturningClientFunction();

        private Task<Client> TryCatch(ReturningClientFunction returningClientFunction)
        {
            try
            {
                return returningClientFunction();
            }
            catch (ClientNullException clientNullException)
            {
                var clientValidationException =
                    new ClientValidationException(clientNullException);

                throw clientValidationException;
            }
            catch (InvalidClientException invalidClientException)
            {
                var clientValidationException =
                    new ClientValidationException(invalidClientException);

                throw clientValidationException;
            }
        }
    }
}
