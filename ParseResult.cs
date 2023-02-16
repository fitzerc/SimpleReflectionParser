using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionParser;

public class ParseResult<T>
{
    public T? TargetObject { get; }
    public Exception? Exception { get; }
    public string? ErrorMsg { get; }
    public bool HasError { get; }

    public ParseResult(T targetObject, string errorMsg = "")
    {
        TargetObject = targetObject;
        HasError = !string.IsNullOrEmpty(errorMsg);
    }

    public ParseResult(Exception e, string errorMsg = "")
    {
        HasError = true;
        Exception = e;
        ErrorMsg = string.IsNullOrEmpty(errorMsg)
            ? e.Message
            : errorMsg;
    }

    public ParseResult(T targetObject, Exception e, string errorMsg = "")
    {
        TargetObject = targetObject;
        HasError = true;
        Exception = e;
        ErrorMsg = string.IsNullOrEmpty(errorMsg)
            ? e.Message
            : errorMsg;
    }
}
