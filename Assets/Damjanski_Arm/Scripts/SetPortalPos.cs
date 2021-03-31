using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SetPortalPos : MonoBehaviour
{
    public Material mat;

    void Update()
    {
        Vector4 v = mat.GetVector("_Data");
        Vector4 nv = new Vector4(this.transform.position.y*-1, v.y, v.z, v.w);
        mat.SetVector("_Data", nv);
    }
}
