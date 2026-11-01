using System;
using System.Collections.Generic;
using System.Globalization;

public static class ReviewTask
{
    public static int Run(string[] names, int[] sizes)
    {
        int total = 0;
        for (int i = 0; i < names.Length - 1; i++)
        {
            if (names[i].Contains(".log"))
                total = sizes[i];
        }
        return total;
    }
}
