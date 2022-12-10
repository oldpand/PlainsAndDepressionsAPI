using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlainsAndDepressions.Services.Results;

public class Result
{
    public Guid PackId { get; private set; }

    public Result(Guid packId)
    {
        PackId = packId;
    }

    public Result(Error error)
    {
        AddError(error);
    }

    public IList<Error> Errors { get; } = new List<Error>();

    public bool IsSuccess { get; private set; } = true;

    public void AddError(Error error)
    {
        IsSuccess = false;
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