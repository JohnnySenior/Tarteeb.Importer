//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Tarteeb.Importer.Models.Exceptions
{
    internal class ClientDependencyException : Xeption
    {
        public ClientDependencyException(Xeption innerException)
            : base(message: "Client dependency exception error occured, contact support",
                  innerException)
        { }
    }
}
