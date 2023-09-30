//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Tarteeb.Importer.Models.Exceptions
{
    internal class ClientServiceException : Xeption
    {
        public ClientServiceException(Xeption innerException)
            : base(message: "Client service error occured, contact support",
                  innerException)
        { }
    }
}
