using System;
using System.Collections.Generic;
using System.Globalization;
using static ReviewTask;

class Program
{
    static int failed = 0;
    static void Main()
    {
        Check("T01", () => Run(new[] { "a.log", "b.log", "x.txt" }, new[] { 10, 20, 1 }), "30");
        Check("T02", () => Run(new[] { "A.LOG", "x.txt" }, new[] { 7, 1 }), "7");
        Check("T03", () => Run(new[] { "x.log.bak", "x.txt" }, new[] { 9, 1 }), "0");
        Check("T04", () => Run(new[] { "a.log" }, new[] { 5 }), "5");
        Check("T05", () => Run(Array.Empty<string>(), Array.Empty<int>()), "0");
        Console.WriteLine("Failed: " + failed);
        Environment.ExitCode = failed == 0 ? 0 : 1;
    }

    static string Display(object value)
    {
        if (value is decimal d)
            return d.ToString("G29", CultureInfo.InvariantCulture);
        return Convert.ToString(value, CultureInfo.InvariantCulture);
    }

    static void Check(string id, Func<object> action, string expected)
    {
        try
        {
            string actual = Display(action());
            bool ok = actual == expected;
            if (!ok) failed++;
            Console.WriteLine(id + (ok ? " PASS" : " FAIL")
                + " expected=[" + expected + "] actual=[" + actual + "]");
        }
        catch (Exception error)
        {
            failed++;
            Console.WriteLine(id + " FAIL exception=" + error.GetType().Name
                + " expected=[" + expected + "]");
        }
    }
}
