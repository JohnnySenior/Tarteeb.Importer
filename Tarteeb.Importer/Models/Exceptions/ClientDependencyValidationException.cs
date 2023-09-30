//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Xeptions;

namespace Tarteeb.Importer.Models.Exceptions
{
    internal class ClientDependencyValidationException : Xeption
    {
        public ClientDependencyValidationException(Xeption innerException)
            : base(message: "Client dependency validation error occured, fix the error and try again",
                  innerException)
        { }
    }
}
