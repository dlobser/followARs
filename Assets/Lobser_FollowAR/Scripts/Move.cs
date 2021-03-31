using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobser
{
    public class Move : MonoBehaviour
    {
        public Vector3 vec;
        Vector3 prevVec;
        RepositionDistance repo;

        void Update()
        {
            if (repo == null)
            {
                repo = FindObjectOfType<RepositionDistance>();
            }
            if (!vec.Equals(prevVec))
            {
                this.gameObject.transform.localPosition = vec;
                repo.Move(this.gameObject);
            }
            prevVec = vec;
        }
    }
}