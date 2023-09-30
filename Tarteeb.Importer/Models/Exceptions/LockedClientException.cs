//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;
using Xeptions;

namespace Tarteeb.Importer.Models.Exceptions
{
    internal class LockedClientException : Xeption
    {
        public LockedClientException(Exception innerException)
            : base(message: "Client is locked, tyr later",
                  innerException)
        { }
    }
}
