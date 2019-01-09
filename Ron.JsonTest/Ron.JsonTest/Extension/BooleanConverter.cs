using Newtonsoft.Json;
using System;

/// <summary>
/// 
/// </summary>
public class BooleanConverter : JsonConverter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="objectType"></param>
    /// <returns></returns>
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(bool) || objectType == typeof(Nullable<bool>);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="objectType"></param>
    /// <param name="existingValue"></param>
    /// <param name="serializer"></param>
    /// <returns></returns>
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.Value == null)
            return null;

        return Convert.ToBoolean(reader.Value);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="serializer"></param>
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null)
            writer.WriteNull();
        else
        {
            UInt32 val = Convert.ToUInt32(Convert.ToBoolean(value));
            writer.WriteValue(val);
        }
    }
}
