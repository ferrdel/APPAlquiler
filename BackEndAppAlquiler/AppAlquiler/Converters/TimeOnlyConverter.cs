using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppAlquiler_DataAccessLayer.Converters
{
    public class TimeOnlyConverter : JsonConverter<TimeOnly>
    {
        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Obtener la cadena de texto
            var time = reader.GetString();
            // Convertir la cadena al formato TimeOnly (HH:mm)
            return TimeOnly.Parse(time);
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        {
            // Escribir la hora en formato "HH:mm"
            writer.WriteStringValue(value.ToString("HH:mm:ss"));
        }
    }
}
