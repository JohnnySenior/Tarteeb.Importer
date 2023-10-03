//===========================
// Copyright (c) Tarteeb LLC
// Powering True Leadership
//===========================

using System;

namespace Tarteeb.Importer.Brokers.DateTimes
{
    internal class DateTimeBroker
    {
        public DateTimeOffset GetCurrentDateTimeOffset() =>
            DateTimeOffset.UtcNow;
    }
}
