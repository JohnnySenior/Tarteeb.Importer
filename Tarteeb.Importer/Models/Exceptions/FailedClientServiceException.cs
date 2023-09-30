//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Xeptions;

namespace Tarteeb.Importer.Models.Exceptions
{
    internal class FailedClientServiceException : Xeption
    {
        public FailedClientServiceException(Exception innerException)
            : base(message: "Failed client service error occured, contact support",
                  innerException)
        { }
    }
}
