//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using Tarteeb.Importer.Models.Clients;
using Tarteeb.Importer.Models.Exceptions;
using Xeptions;

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
                throw CreateValidationException(clientNullException);
            }
            catch (InvalidClientException invalidClientException)
            {
                throw CreateValidationException(invalidClientException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistsClientException =
                    new AlreadyExistsClientException(duplicateKeyException);

                throw CreateDependencyValidationException(alreadyExistsClientException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedClientException =
                    new LockedClientException(dbUpdateConcurrencyException);

                throw CreateDependencyException(lockedClientException);
            }
            catch (DbUpdateException dbUpdateException)
            { 
                var failedStorageClientException =
                    new FailedStorageClientException(dbUpdateException);

                throw CreateDependencyException(failedStorageClientException);
            }
            catch (Exception exception)
            {
                var failedClientServiceException =
                    new FailedClientServiceException(exception);

                throw CreateClientServiceException(failedClientServiceException);
            }

        }

        private ClientServiceException CreateClientServiceException(Xeption exception)
        {
            var clientServiceException = new ClientServiceException(exception);

            return clientServiceException;
        }

        private ClientDependencyException CreateDependencyException(Xeption exception)
        {
            var clientDependencyException = new ClientDependencyException(exception);

            return clientDependencyException;
        }

        private ClientDependencyValidationException CreateDependencyValidationException(Xeption exception)
        {
            var clientDependencyValidationException = new ClientDependencyValidationException(exception);

            return clientDependencyValidationException;
        }

        private ClientValidationException CreateValidationException(Xeption exception)
        {
            var clientValidationException = new ClientValidationException(exception);

            return clientValidationException;
        }
    }
}
