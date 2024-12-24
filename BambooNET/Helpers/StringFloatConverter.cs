﻿/// <summary>
/// BambooNET.Test
/// Copyright(c) 2024 CR Development
/// 
/// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
/// documentation files (the “Software”), to deal in the Software without restriction, including without limitation 
/// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
/// and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
/// 
/// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
/// 
/// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
/// THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
/// OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
/// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
/// 
/// @name StringFloatConverter.cs
/// @version 2024-12-20
/// @author Craig Roberts
/// </summary>
namespace BambooNET.Helpers;

/// <summary>
/// StringFloatConverter
/// </summary>
public partial class StringFloatConverter : JsonConverter
{
  [GeneratedRegex("[^0-9.,]")]
  private static partial Regex StringFloatRegex();


  /// <summary>
  /// 
  /// </summary>
  public override bool CanWrite => false;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="writer"></param>
  /// <param name="value"></param>
  /// <param name="serializer"></param>
  /// <exception cref="NotImplementedException"></exception>
  public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
  {
    throw new NotImplementedException();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="reader"></param>
  /// <param name="objectType"></param>
  /// <param name="existingValue"></param>
  /// <param name="serializer"></param>
  /// <returns></returns>
  public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
  {
    var regex = StringFloatRegex();
    var value = reader.Value?.ToString();

    if (value == null || string.IsNullOrEmpty(value) || string.IsNullOrEmpty(regex.Replace(value, "").Trim()))
    {
      return null;
    }
    var success = float.TryParse(regex.Replace(value, "").Trim(), out var result);
    return success ? result : null;

  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectType"></param>
  /// <returns></returns>
  public override bool CanConvert(Type objectType)
  {
    if (objectType == typeof(string) || objectType == typeof(float))
    {
      return true;
    }
    return false;
  }

} //end public partial class StringFloatConverter : JsonConverter