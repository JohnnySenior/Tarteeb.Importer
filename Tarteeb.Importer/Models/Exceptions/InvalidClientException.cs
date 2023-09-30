//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using Xeptions;

namespace Tarteeb.Importer.Models.Exceptions
{
    internal class InvalidClientException : Xeption
    {
        public InvalidClientException()
            : base(message: "Invalid Client, Fix the error and try again")
        { }
    }
}
