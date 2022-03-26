using System;
using System.Net.Http.Json;

namespace BlazorEC.Client.Extensions;

public static class HttpResponseMessageExtension
{
    public static async ValueTask HandleError(this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return;

        var message = await response.Content.ReadFromJsonAsync<string>();

        var ex = new Exception();
        ex.Data.Add((int)response.StatusCode, message);
        throw ex;
    }
}

