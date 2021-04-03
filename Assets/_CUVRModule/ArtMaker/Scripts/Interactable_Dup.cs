using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobser
{
    public class Interactable_Dup : Interactable
    {
        public GameObject followAR;
        Transform container;
        public int max = 20;
        float initScale;

        public override void HandleEnter()
        {
            base.HandleEnter();
            ArtMakerFace[] f = FindObjectsOfType<ArtMakerFace>();
            if (f.Length < max)
            {
                GameObject g = Instantiate(followAR,this.transform.parent.parent);
                g.transform.localPosition = this.transform.localPosition;
                g.GetComponent<Move>().vec = new Vector3(
                 Random.Range(-.5f, .5f)+this.transform.localPosition.x, 0,
                 Random.Range(-.5f, .5f)+this.transform.localPosition.x);
                g.GetComponentInChildren<ArtMakerFace>().rebuild = true;
                StartCoroutine(Scale(g));
            }

        }

        public IEnumerator Scale(GameObject g)
        {
            float c = 0;
            g.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = false;
            yield return null;
            g.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = true;
            while (c < 1)
            {
                c += Time.deltaTime;
                g.transform.localScale = new Vector3(c, c, c);
                yield return null;

            }
        }
    }
}