using Newtonsoft.Json.Serialization;

/// <summary>
/// 
/// </summary>
public class LowercaseContractResolver : DefaultContractResolver
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    protected override string ResolvePropertyName(string propertyName)
    {
        return propertyName.ToLower();
    }
}


