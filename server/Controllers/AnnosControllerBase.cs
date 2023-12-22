using Microsoft.AspNetCore.Mvc;

namespace annos_server.Controllers;

public class AnnosControllerBase : ControllerBase
{
    protected static bool MissingKeys(IFormCollection data, string[] keys)
    {
        IEnumerable<string> invalidKeys = keys.Where(key => String.IsNullOrEmpty(data[key]));
        return invalidKeys.Any();
    }
}