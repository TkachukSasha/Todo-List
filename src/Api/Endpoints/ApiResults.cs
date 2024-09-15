﻿using Domain.Abstractions;

namespace Api.Endpoints;

public static class ApiResults
{
    public static IResult Problem(Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        return Microsoft.AspNetCore.Http.Results.Problem(
            title: GetTitle(result.Errors[0]),
            detail: GetDetail(result.Errors[0]),
            type: GetType(result.Errors[0].ErrorType),
            statusCode: GetStatusCode(result.Errors[0].ErrorType));

        static string GetTitle(Error error) =>
            error.ErrorType switch
            {
                ErrorType.Validation => error.Code,
                ErrorType.NotFound => error.Code,
                ErrorType.Conflict => error.Code,
                _ => "Server failure"
            };

        static string GetDetail(Error error) =>
            error.ErrorType switch
            {
                ErrorType.Validation => error.Description,
                ErrorType.NotFound => error.Description,
                ErrorType.Conflict => error.Description,
                _ => "An unexpected error occurred"
            };

        static string GetType(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

        static int GetStatusCode(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };
    }
}