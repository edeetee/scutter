using System.Collections.Generic;
using UnityEngine;

public static class Extensions {
    public static Vector3 ChangeX(this Vector3 v, float x)
    {
        return new Vector3(x, v.y, v.z);
    }

    public static Vector3 ChangeY(this Vector3 v, float y)
    {
        return new Vector3(v.x, y, v.z);
    }

    public static Vector3 ChangeZ(this Vector3 v, float z)
    {
        return new Vector3(v.x, v.y, z);
    }

    private static System.Random rng = new System.Random();  

    public static IList<T> Shuffle<T>(this IList<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }
        return list;
    }
}