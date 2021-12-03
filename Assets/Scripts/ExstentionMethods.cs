using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class ExstentionMethods
{
    public static bool AreAll<T>(this T[] source, Func<T, bool> condition)
    {
        return source.Where(condition).Count() == source.Count();
    }
}
