using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SilliconValley.Integration.dto
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        [JsonPropertyName("first_name")]
        public string Nombre { get; set; }

        [JsonPropertyName("last_name")]
        public string Apellido { get; set; }
        public string? Avatar { get; set; }


    }
}