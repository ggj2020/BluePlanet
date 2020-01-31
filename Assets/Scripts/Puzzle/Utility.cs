using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Utility
{
    public static T Pick<T>(this ICollection<T> collection) where T : class {
        if ( collection.Count == 0 )
        {
            return null;
        }
        else
        {
            return collection.ElementAt(Random.Range(0, collection.Count));
        }
    }
}