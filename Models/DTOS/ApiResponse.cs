using System.Net;

namespace Models.DTOS;

public class ApiResponse<T>
{
    public HttpStatusCode StatusCode { get; set; } //200,201,400,401,500
    public bool IsSuccesfuly { get; set; }
    public string? Message { get; set; }
    public T? Result { get; set; }
}