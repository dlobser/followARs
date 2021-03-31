using UnityEngine;
using System.Collections;

public class Gizmo : MonoBehaviour {

	public float size = 1 ;

	void OnDrawGizmos() {
        Gizmos.color = new Color(1, 1, 1, .3f);
		Gizmos.DrawSphere(transform.position, transform.localScale.x*.8f * size);
		Gizmos.DrawWireSphere(transform.position, transform.localScale.x * size);
	}

}
