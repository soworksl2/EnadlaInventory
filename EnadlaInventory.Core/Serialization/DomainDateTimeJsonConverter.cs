﻿using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

namespace EnadlaInventory.Core.Serialization
{
    internal sealed class DomainDateTimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString()!, Formats.DomainDateTimeFormat, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Formats.DomainDateTimeFormat));
        }
    }
}
