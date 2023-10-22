using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Core.CrossCuttingConcerns.Exceptions;

public class BusinessProblemDetails : ProblemDetails
{
    public string Message { get; set; }
    public override string ToString() => JsonConvert.SerializeObject(this);
}