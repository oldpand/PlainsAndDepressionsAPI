using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlainsAndDepressions.Services.Results;

public class Result
{
    public IList<Error> Errors { get; } = new List<Error>();

    public bool IsOk() => Errors.Any();

    public void AddError(Error error)
    {
        Errors.Add(error);
    }
}

public class Error
{
    Error(Enum code, string message)
    {
        Code = code;
        Message = message;
    }

    public Enum Code { get; }
    public string Message { get; }
}