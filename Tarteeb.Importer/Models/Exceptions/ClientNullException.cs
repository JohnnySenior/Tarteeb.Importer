//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Tarteeb.Importer.Models.Exceptions
{
    internal class ClientNullException : Xeption
    {
        public ClientNullException()
            : base(message: "Client is null")
        { }
    }
}
