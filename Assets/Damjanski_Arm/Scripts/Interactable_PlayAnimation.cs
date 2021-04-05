using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobser
{
    public class Interactable_PlayAnimation : Interactable
    {
        //explodes an object when clicked

        public Animator animator;
        public string trigger;
        bool triggered = false;

        public override void HandleHover()
        {
            if (clicked > .5f && !triggered)
            {
                HandleTrigger();
                triggered = true;
                StartCoroutine(Reactivate());
            }
        }

        public override void HandleTrigger()
        {
            base.HandleTrigger();
            animator.SetTrigger(trigger);
        }

        IEnumerator Reactivate()
        {
            yield return new WaitForSeconds(1);
            triggered = false;
        }
    }

}