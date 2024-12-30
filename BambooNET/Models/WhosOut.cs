/// <summary>
/// BambooNET
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
/// @name WhosOut.cs
/// @version 2024-12-20
/// @author Craig Roberts
/// </summary>
namespace BambooNET.Models;

/// <summary>
/// WhosOut
/// </summary>
public class WhosOut
{
  /// <summary>
  /// 
  /// </summary>
  [JsonProperty("id")]
  public int Id { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [JsonProperty("type")]
  public string Type { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [JsonProperty("employeeNumber")]
  public string EmployeeNumber { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [JsonProperty("name")]
  public string Name { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [JsonProperty("start")]
  public DateTime? Start { get; set; }

  /// <summary>
  /// 
  /// </summary>
  [JsonProperty("end")]
  public DateTime? End { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public bool IsTimeOff => Type.Equals("timeoff", StringComparison.OrdinalIgnoreCase);

  /// <summary>
  /// 
  /// </summary>
  public bool IsHoliday => Type.Equals("holiday", StringComparison.OrdinalIgnoreCase);

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString()
  {
    return $"ID: {Id}, Type: {Type}, EmployeeNumber: {EmployeeNumber}, Name: {Name}" + 
      (Start.HasValue ? $", Start: {Start.Value.ToString("yyyy-MM-dd")}" : "") +
      (End.HasValue ? $", End: {End.Value.ToString("yyyy-MM-dd")}" : "");
  }

} //end public class WhosOut
