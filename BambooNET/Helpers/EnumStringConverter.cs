using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BambooNET.Helpers;

/// <summary>
/// EnumStringConverter
/// </summary>
public class EnumStringConverter : JsonConverter
{
  /// <summary>
  /// 
  /// </summary>
  public override bool CanRead => false;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="objectType"></param>
  /// <param name="existingValue"></param>
  /// <param name="serializer"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer) => throw new NotImplementedException();


  /// <summary>
  /// 
  /// </summary>
  /// <param name="writer"></param>
  /// <param name="value"></param>
  /// <param name="serializer"></param>
  /// <exception cref="JsonSerializationException"></exception>
  public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
  {
    if (value is SortDirection sort_direction)
    {
      writer.WriteValue(sort_direction.GetString());
    }
    else if(value is FiltersMatch filters_match)
    {
      writer.WriteValue(filters_match.GetString());
    }
    else if (value is FilterOperator filter_operator)
    {
      writer.WriteValue(filter_operator.GetString());
    }
    else
    {
      throw new JsonSerializationException("Unexpected object value.");
    }

  } //end public override void WriteJson


  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectType"></param>
  /// <returns></returns>
  public override bool CanConvert(Type objectType)
  {
    return objectType.IsEnum;

  } //end public override bool CanConvert


} //end public class EnumStringConverter : JsonConverter
