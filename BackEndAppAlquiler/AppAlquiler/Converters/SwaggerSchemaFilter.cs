using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using Microsoft.OpenApi.Any;

namespace AppAlquiler_DataAccessLayer.Converters
{
    public class SwaggerSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            // Si el tipo es DateOnly, lo tratamos como una cadena con el formato 'date'
            if (context.Type == typeof(DateOnly))
            {
                schema.Type = "string";
                schema.Format = "date"; // 'date' es el formato estándar para una fecha en Swagger
            }

            // Si el tipo es TimeOnly, lo tratamos como una cadena con el formato 'time'
            if (context.Type == typeof(TimeOnly))
            {
                schema.Type = "string";
                schema.Format = "time";  // 'time' es el formato estándar para tiempo en Swagger
                schema.Example = new OpenApiString("18:00:00");  // Ejemplo de valor para mostrar en Swagger
            }
        }
    }
}
