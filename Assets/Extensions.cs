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

    public static Vector3 All(float z)
    {
        return new Vector3(z, z, z);
    }

    public static Vector3 Scale(this Vector3 v, float z)
    {
        return Vector3.Scale(v, All(z));
    }

    public static System.Random rng = new System.Random();  

    public static T random<T>(this IList<T> list){
        return list[rng.Next(list.Count)];
    }

	public static Vector3 GetPointOnUnitSphereCap(Quaternion targetDirection, float angle)
	{
		var angleInRad = Random.Range(0.0f,angle) * Mathf.Deg2Rad;
		var PointOnCircle = (Random.insideUnitCircle.normalized)*Mathf.Sin(angleInRad);
		var V = new Vector3(PointOnCircle.x,PointOnCircle.y,Mathf.Cos(angleInRad));
		return targetDirection*V;
	}

    public static bool HasComponent<T>(this GameObject obj) where T : Component
    {
        return obj.GetComponent<T>() != null;
    }

    // public static IList<T> Shuffle<T>(this IList<T> list)  
    // {  
    //     int n = list.Count;  
    //     while (n > 1) {  
    //         n--;  
    //         int k = rng.Next(n + 1);  
    //         T value = list[k];  
    //         list[k] = list[n];  
    //         list[n] = value;  
    //     }
    //     return list;
    // }
}