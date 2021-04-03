using UnityEngine;
using System.Collections;

public class lookAtTarget : MonoBehaviour {

	public GameObject target;
	public float scalar;
	public float maxScale = 1;
	public Vector3 offset = Vector3.zero;
	public Vector3 scaleOffset = Vector3.one;
    public float lerpSpeed = .01f;
    Quaternion rot;

	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
        if (target == null)
        {
            target = Camera.main.transform.gameObject;
        }
        //print(target);
        Vector3 s = scaleOffset;
        Quaternion savedRot = this.transform.rotation;
		this.transform.LookAt (target.transform.position);
        Quaternion newRot = this.transform.rotation;
        this.transform.rotation = Quaternion.Slerp(savedRot, newRot, lerpSpeed*Time.deltaTime);
        Vector3 eulers = new Vector3(0, this.transform.localEulerAngles.y, 0);
        this.transform.localEulerAngles = eulers;
//		float d = Mathf.Min(maxScale, Vector3.Distance (this.transform.position, target.transform.position));
//		this.transform.localScale = new Vector3 (Mathf.Min(d,s.x), Mathf.Min(d,s.y), Mathf.Min(d,s.z));
		this.transform.Rotate (offset);
	
	}
}
