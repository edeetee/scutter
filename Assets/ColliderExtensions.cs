using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class ColliderExtensions{
    /// <summary>
    /// Method to get top of collider, using derived extension methods
    /// </summary>
    /// <param name="collider"></param>
    /// <returns></returns>
    public static Vector3 getTop(this Collider collider){
        // hack to get most derived extension
        Vector3? tryDerived = (collider as BoxCollider)?.getTop() ?? (collider as SphereCollider)?.getTop();
        if(tryDerived != null)
            return (Vector3)tryDerived;
        
        //fallback
        Vector3 top = collider.transform.InverseTransformPoint(collider.bounds.center);
        return top.ChangeY(top.y + collider.bounds.size.y / collider.transform.localScale.y / 2);
    }

    public static Vector3 getTop(this BoxCollider collider){
        return collider.center.ChangeY(collider.center.y+collider.size.y/2);
    }

    public static Vector3 getTop(this SphereCollider collider){
        return collider.center.ChangeY(collider.center.y+collider.radius);
    }

    // public static float getTop(this CapsuleCollider collider){
	// 	return collider.height/2 + collider.center.y;
    // }
}