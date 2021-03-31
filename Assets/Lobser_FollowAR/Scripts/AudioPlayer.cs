using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobser
{
    public class AudioPlayer : MonoBehaviour
    {
        public int whoami;
        public AudioClip[] clips;
        public Vector2 minMaxWait;
        AudioSource source;
        bool first = false;

        void Start()
        {
            whoami = Random.Range(0, clips.Length);
            StartCoroutine(Play());
            source = GetComponent<AudioSource>();
        }

        IEnumerator Play()
        {
            yield return new WaitForSeconds(!first ? Random.Range(0, minMaxWait.x) : Random.Range(minMaxWait.x, minMaxWait.y));
            whoami = Random.Range(0, clips.Length);
            first = true;
            source.clip = clips[whoami];
            source.Play();
            source.pitch = Random.Range(.8f, 1.1f);
            StartCoroutine(Play());
            print("start");
        }
    }
}