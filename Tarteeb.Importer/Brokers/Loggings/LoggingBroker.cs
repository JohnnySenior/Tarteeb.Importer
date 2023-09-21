//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;

namespace Tarteeb.Importer.Brokers.Loggings
{
    internal class LoggingBroker
    {
        public void LogError(string message)
        {
            Console.WriteLine(message);
        }
    }
}
