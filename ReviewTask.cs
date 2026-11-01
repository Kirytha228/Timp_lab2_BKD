using System;
using System.Collections.Generic;
using System.Globalization;

public static class ReviewTask
{
    private const string LogExtension = ".log";

    public static int Run(string[] names, int[] sizes)
    {
        int total = 0;
        for (int i = 0; i < names.Length; i++)
        {
            if (names[i].EndsWith(LogExtension, StringComparison.OrdinalIgnoreCase))
                total += sizes[i];
        }
        return total;
    }
}
