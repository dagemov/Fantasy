using Models.DTOS;
using System.Net;
using System.Text.Json;

namespace Fantasy.Frontend.Repositories;

public class HttpResponseWrapper<T>
{
    public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
    {
        Response = response;
        Error = error;
        HttpResponseMessage = httpResponseMessage;
    }

    public T? Response { get; }
    public bool Error { get; }
    public HttpResponseMessage HttpResponseMessage { get; }

    public async Task<string?> GetErrorMessageAsync()
    {
        if (!Error)
        {
            return null;
        }

        var statusCode = HttpResponseMessage.StatusCode;
        var responseContent = await HttpResponseMessage.Content.ReadAsStringAsync();

        if (statusCode == HttpStatusCode.NotFound)
        {
            return "Record don't Found";
        }
        if (statusCode == HttpStatusCode.BadRequest)
        {
            return await HttpResponseMessage.Content.ReadAsStringAsync();
        }
        if (statusCode == HttpStatusCode.Unauthorized)
        {
            return "You have to be Autorized to do this Action";
        }
        if (statusCode == HttpStatusCode.Forbidden)
        {
            return "You do not have permissions to perform this operation.";
        }

        return "An unexpected error has occurred.";
    }
}