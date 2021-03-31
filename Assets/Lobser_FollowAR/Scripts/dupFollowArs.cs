using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobser
{
    public class dupFollowArs : ArtMakerTemplate
    {
        public int amount;
        public GameObject followAR;

        public override void MakeArt()
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject g = Instantiate(followAR);

                g.transform.parent = root.transform;
                Vector2 v = Random.insideUnitCircle;
                g.transform.localPosition = new Vector3(v.x, 0, v.y);
            }
        }
    }
}
