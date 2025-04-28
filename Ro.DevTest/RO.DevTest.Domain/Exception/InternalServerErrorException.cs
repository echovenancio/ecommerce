using System.Net;

namespace RO.DevTest.Domain.Exception;

/// <summary>
/// Returns a <see cref="HttpStatusCode.InternalServerError"/> to
/// the request
/// </summary>
public class InternalServerErrorException : ApiException {
    public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;
    public InternalServerErrorException(string error) : base(error) { }
}
