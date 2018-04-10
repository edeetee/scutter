using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SetColors : MonoBehaviour {
	public List<Material> targetMaterials;
	public List<Color> colors;

	List<Material> newMaterials = new List<Material>();

	void OnValidate()
	{
	// cols
	}

	// Use this for initialization
	void Awake () {
		if(targetMaterials == null || targetMaterials.Count == 0)
			return;

		targetMaterials.ForEach(mat => colors.Add(mat.color));
		foreach (var item in GetComponentsInChildren<MeshRenderer>())
		{
			if(targetMaterials.Contains(item.sharedMaterial)){
				Material material = new Material(item.sharedMaterial);
				material.color = colors.random();
				newMaterials.Add(material);
				item.sharedMaterial = material;
			}
		}
	}

	void OnDestroy()
    {
        foreach (var material in newMaterials)
		{
			DestroyImmediate(material);
		}
		newMaterials.Clear();
        //Output the amount of materials to show if the instance was deleted
        print("Materials " + Resources.FindObjectsOfTypeAll(typeof(Material)).Length);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
