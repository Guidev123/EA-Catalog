using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CatalogService.Application.Responses
{
    public class Response<TData>
    {
        private readonly int _code;

        [JsonConstructor]
        public Response()
            => _code = 200;

        public Response(
            TData? data,
            int code = 200,
            string? message = null,
            string[]? errors = null)
        {
            Data = data;
            Message = message;
            _code = code;
            Errors = errors;
        }

        public TData? Data { get; set; }
        public string? Message { get; set; }
        public string[]? Errors { get; set; }

        [JsonIgnore]
        public bool IsSuccess
            => _code is >= 200 and <= 299;
    }
}
