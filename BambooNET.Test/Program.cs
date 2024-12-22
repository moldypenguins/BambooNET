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
/// @name Program.cs
/// @version 2024-12-20
/// @author Craig Roberts
/// </summary>
namespace BambooNET.Test;

public class Program
{
  public static async Task Main(string[] args)
  {
    try
    {
      Console.WriteLine("Running...");

      var bambooClient = new BambooClient("company_subdomain", "api_key");

      var results = await bambooClient.TimeTracking.GetTimesheetEntriesAsync(new DateOnly(2024, 11, 30), new DateOnly(2024, 12, 13));

      Console.WriteLine($"Results: {results.Count}");

      Console.WriteLine("Press any key to exit...");
      Console.ReadKey();

    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.ToString());
    }

  } //end public static async void Main(string[] args)

} //end public class Program