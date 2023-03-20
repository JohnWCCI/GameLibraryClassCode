﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ServiceBroker
{
    public static class ApiHelper
    {
        public static async Task<T> ReadContentAsync<T>(this HttpResponseMessage response)
        {
            if(response.IsSuccessStatusCode == false)
                throw new ApplicationException($"Something went wrong calling the API: " + response.ReasonPhrase);
            var dataAsString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(

                dataAsString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            return result;
        }
    }
}
