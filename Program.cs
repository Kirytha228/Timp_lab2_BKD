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

        // Свои тесты
        Check("U01", () => Run(new[] { "sys.log", "app.LOG", "notes.txt", "web.Log" }, new[] { 4, 6, 100, 10 }), "20");
        Check("U02", () => Run(new[] { "x.logs", "log", "y.log" }, new[] { 3, 2, 1 }), "1");
        Check("U03", () => Run(new[] { "a.txt", "b.log" }, new[] { 5, 8 }), "8");
        Check("U04", () =>
        {
            string[] names = { "a.log", "b.txt" };
            int[] sizes = { 3, 4 };
            int first = Run(names, sizes);
            int second = Run(names, sizes);
            return first + "|" + string.Join(",", names) + "|" + string.Join(",", sizes) + "|" + second;
        }, "3|a.log,b.txt|3,4|3");
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
