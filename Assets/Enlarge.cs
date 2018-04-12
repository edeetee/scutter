using UnityEngine;

class Enlarge : MonoBehaviour {
    float startTime;
    Vector3 startScale;
    Vector3 aimScale;

    void Awake(){
        startTime = Time.time;
        startScale = transform.localScale;
        aimScale = Vector3.Scale(startScale, Extensions.All(Random.Range(2f, 4f)));
    }

    void FixedUpdate()
    {
        transform.localScale = Vector3.Lerp(startScale, aimScale, (Time.time-startTime)/10f);
    }
}