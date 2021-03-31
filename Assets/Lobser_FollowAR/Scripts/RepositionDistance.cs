using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobser
{
    public class RepositionDistance : MonoBehaviour
    {
        List<GameObject> followARs;
        List<GameObject> posers;

        GameObject moving;
        int moveIndex;

        public void Init()
        {
            followARs = new List<GameObject>();
            posers = new List<GameObject>();
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < followARs.Count; i++)
            {
                Vector3 pos = new Vector3(posers[i].transform.localPosition.x, 0, posers[i].transform.localPosition.y);
                if (followARs[i] != moving)
                {
                    followARs[i].transform.localPosition = pos;
                }
                else
                {
                    Vector3 v = moving.transform.localPosition;
                    posers[i].transform.localPosition = new Vector3(v.x, v.z, 0);
                }
            }
        }

        public void Move(GameObject g)
        {
            moving = g;
        }

        int FindPoser()
        {
            int which = -1;
            for (int i = 0; i < followARs.Count; i++)
            {
                if (followARs[i] == moving)
                    which = i;
            }
            return which;
        }

        public void Add(GameObject followAR, Vector3 pos)
        {
            GameObject g = new GameObject();
            CircleCollider2D collider = g.AddComponent<CircleCollider2D>();
            collider.radius = .7f;
            Rigidbody2D rigid = g.AddComponent<Rigidbody2D>();
            rigid.gravityScale = 0;
            g.transform.localPosition = new Vector3(pos.x, pos.z, 0);
            g.name = "followAR center";
            g.transform.parent = this.transform;

            followARs.Add(followAR);
            posers.Add(g);
        }
    }

}