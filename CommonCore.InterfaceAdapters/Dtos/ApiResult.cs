using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CommonCore.InterfaceAdapters.Dtos;

public record ApiResult(HttpStatusCode Status, string? Message)
{
    public static ApiResult Success() => new(HttpStatusCode.OK, null);
    public static ApiResult Fail(string Message) => new(HttpStatusCode.NotFound, Message);
    public static ApiResult CustomException(string Message) => new(HttpStatusCode.InternalServerError, Message);
}
public record ApiResult<TData>(HttpStatusCode Status, TData Data, string? Message)
{
    public static ApiResult<TData> Success(TData data) => new(HttpStatusCode.OK, data, null);
    public static ApiResult<TData> Fail(string Message) => new(HttpStatusCode.NotFound, default!, Message);
    public static ApiResult<TData> CustomException(string Message) => new(HttpStatusCode.InternalServerError, default!, Message);
}
