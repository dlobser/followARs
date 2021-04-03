using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobser
{
    public class RepositionDistance : MonoBehaviour
    {
        List<GameObject> followARs;
        List<GameObject> posers;

        GameObject camPos;

        GameObject moving;
        int moveIndex;
        public GameObject container;

        public void Init()
        {
            followARs = new List<GameObject>();
            posers = new List<GameObject>();
            AddCam();
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

            UpdateCamera();
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

        public void AddCam()
        {
            camPos = new GameObject();
            CircleCollider2D collider = camPos.AddComponent<CircleCollider2D>();
            collider.radius = 1f;
            Rigidbody2D rigid = camPos.AddComponent<Rigidbody2D>();
            rigid.gravityScale = 0;
            Vector3 pos = Camera.main.transform.position;
            camPos.transform.localPosition = new Vector3(pos.x, pos.z, 0);
            camPos.name = "cam";
            camPos.transform.parent = this.transform;
        }

        public void UpdateCamera()
        {
            Vector3 pos = Camera.main.transform.position;
            print(pos);

            Vector3 p2 = container.transform.worldToLocalMatrix.MultiplyPoint(pos);
            print(p2);
            camPos.transform.localPosition = new Vector3(p2.x, p2.z, 0);
        }

        public void Add(GameObject followAR, Vector3 pos)
        {
            GameObject g = new GameObject();
            CircleCollider2D collider = g.AddComponent<CircleCollider2D>();
            collider.radius = .7f;
            StartCoroutine(Scale(collider));
            Rigidbody2D rigid = g.AddComponent<Rigidbody2D>();
            rigid.gravityScale = 0;
            g.transform.localPosition = new Vector3(pos.x, pos.z, 0);
            g.name = "followAR center";
            g.transform.parent = this.transform;

            followARs.Add(followAR);
            posers.Add(g);
        }

        public IEnumerator Scale(CircleCollider2D C)
        {
            float c = 0;
            while (c < 1)
            {
                c += Time.deltaTime;
                C.radius = c*.7f;//.transform.localScale = new Vector3(c, c, c);
                yield return null;

            }
        }
    }

}