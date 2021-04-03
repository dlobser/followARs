using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobser
{
    public class ArtMakerFace : ArtMakerTemplate
    {
        public Transform noses;
        public Transform eyes;
        public Transform mouths;
        public Transform hair;
        public Transform eyeBrows;

        public GameObject head;
        GameObject _head;

        public float mouthHeightMin;
        public float mouthHeightMax;

        public float mouthSizeMin;
        public float mouthSizeMax;

        public float noseSizeMin;
        public float noseSizeMax;

        public float eyeSizeMin;
        public float eyeSizeMax;

        public float eyeSeparationMin;
        public float eyeSeparationMax;

        public float eyeRotationMin;
        public float eyeRotationMax;


        public float eyebrowSizeMin;
        public float eyebrowSizeMax;

        public float eyebrowRotationMin;
        public float eyebrowRotationMax;

        GameObject LeftEye;
        GameObject RightEye;
        GameObject LeftEyebrow;
        GameObject RightEyebrow;
        GameObject Nose;
        GameObject Mouth;

        public Color[] skinColors;
        public Color[] shirtColors;
        public Color[] pantsColors;
        public Color[] hairColors;
        public Color[] shoesColors;

        public Color[] irisColors;
        public Color colorA;
        public Color colorB;
        public Material skinMat;

        public RenderTexture renderTexture;
        Texture2D texture;
        public Material personMat;
        public GameObject followAR;
        GameObject myFollowAR;
        Material myMaterial;
        public Camera captureCam;
        public Material pantsMaterial;
        public Material shirtMaterial;
        public Material hairMaterial;
        public Material shoesMaterial;

        public GameObject captureGroup;
        public bool destroyFace = true;

        public Transform headJoint;

        public TextMesh logo;

        public override void MakeArt()
        {
            captureGroup.SetActive(true);
            if (_head != null)
                Destroy(_head);

            captureCam.Render();

            _head = Instantiate(head,captureGroup.transform);
            _head.transform.parent = captureGroup.transform;
            _head.transform.Translate(0, 0, .5f);

            LeftEye = new GameObject();
            LeftEye.transform.parent = _head.transform;
            LeftEye.name = "LeftEye";

            RightEye = new GameObject();
            RightEye.transform.parent = _head.transform;
            RightEye.name = "RightEye";

            LeftEyebrow = new GameObject();
            LeftEyebrow.transform.parent = _head.transform;
            LeftEyebrow.name = "LeftEyebrow";

            RightEyebrow = new GameObject();
            RightEyebrow.transform.parent = _head.transform;
            RightEyebrow.name = "RightEyebrow";

            Nose = new GameObject();
            Nose.transform.parent = _head.transform;
            Nose.name = "Nose";

            Mouth = new GameObject();
            Mouth.transform.parent = _head.transform;
            Mouth.name = "Mouth";


            for (int i = 0; i < 400; i++)
            {
                Transform h = Instantiate(hair.GetChild(Random.Range(0,hair.childCount)),_head.transform);
                Vector3 v = new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(0, 1.5f), -.4f);
                float d = Vector2.Distance(Vector2.zero, new Vector2(v.x, v.y));
                h.transform.localPosition = v;
                float e = d * .3f;
                h.transform.localScale = new Vector3(e,e,e);
                h.transform.Rotate(0, 0, Random.value * 360);
                if (d < .65f)
                    h.transform.localScale = Vector3.zero;


            }

            int randomEye = Random.Range(0, eyes.childCount);
            int randomEyebrow = Random.Range(0, eyeBrows.childCount);
            float scale = Random.Range(eyeSizeMin, eyeSizeMax);
            float sep = Random.Range(eyeSeparationMin, eyeSeparationMax);
            float rot = Random.Range(eyeRotationMin, eyeRotationMax);

            Transform t = Instantiate(eyes.GetChild(randomEye), LeftEye.transform);
            LeftEye.transform.localScale = new Vector3(-scale, scale, scale);
            LeftEye.transform.localPosition = new Vector3(-sep, .25f, -.5f);
            LeftEye.transform.localEulerAngles = new Vector3(0, 0, rot);

            t = Instantiate(eyes.GetChild(randomEye), RightEye.transform);
            RightEye.transform.localScale = new Vector3(scale, scale, scale);
            RightEye.transform.localPosition = new Vector3(sep, .25f, -.5f);
            RightEye.transform.localEulerAngles = new Vector3(0, 0, -rot);

            t = Instantiate(eyeBrows.GetChild(randomEyebrow), LeftEyebrow.transform);
            LeftEyebrow.transform.localScale = new Vector3(-scale, scale, scale);
            LeftEyebrow.transform.localPosition = new Vector3(-sep, .35f, -.4f);
            LeftEyebrow.transform.localEulerAngles = new Vector3(0, 0, rot);

            t = Instantiate(eyeBrows.GetChild(randomEyebrow), RightEyebrow.transform);
            RightEyebrow.transform.localScale = new Vector3(scale, scale, scale);
            RightEyebrow.transform.localPosition = new Vector3(sep, .35f, -.4f);
            RightEyebrow.transform.localEulerAngles = new Vector3(0, 0, -rot);

            

            t = Instantiate(noses.GetChild(Random.Range(0, noses.childCount)), Nose.transform);
            scale = Random.Range(noseSizeMin, noseSizeMax);
            Nose.transform.localScale = new Vector3(scale, scale, scale);
            Nose.transform.localPosition = new Vector3(0, 0, -.6f);
            Nose.transform.localEulerAngles = Vector3.zero;

            t = Instantiate(mouths.GetChild(Random.Range(0, mouths.childCount)), Mouth.transform);
            scale = Random.Range(mouthSizeMin, mouthSizeMax);
            float pos = Random.Range(mouthHeightMin, mouthHeightMax);
            Mouth.transform.localScale = new Vector3(scale, scale, scale);
            Mouth.transform.localPosition = new Vector3(0, pos, -.5f);
            Mouth.transform.localEulerAngles = Vector3.zero;

            SpriteRenderer[] sprites = _head.GetComponentsInChildren<SpriteRenderer>();
            float r = Random.value;

            Color skin = skinColors[Random.Range(0, skinColors.Length)];
            Color eye = irisColors[Random.Range(0, irisColors.Length)];

            int hairInt = Random.Range(0, hairColors.Length);

            foreach (SpriteRenderer sp in sprites)
            {
                if (sp.GetComponent<Tint>() != null)
                {
                    if (sp.GetComponent<Tint>().skin)
                        sp.color = skin;
                    if (sp.GetComponent<Tint>().iris)
                        sp.color = eye;
                    if (sp.GetComponent<Tint>().hair)
                        sp.color = hairColors[hairInt];
                    if (sp.GetComponent<Tint>().eyebrow)
                        sp.color = new Color(hairColors[hairInt].r * .25f, hairColors[hairInt].g * .25f, hairColors[hairInt].b * .25f, 1);
                }

            }


            hairMaterial.color =  hairColors[hairInt];
            shirtMaterial.color = shirtColors[Random.Range(0, shirtColors.Length)];
            pantsMaterial.color = pantsColors[Random.Range(0, pantsColors.Length)];
            shoesMaterial.color = shoesColors[Random.Range(0, shoesColors.Length)];


            string[] Alphabet = new string[52] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

            string rs = Alphabet[Random.Range(0, Alphabet.Length)];

            logo.text = rs;
            logo.color = shirtColors[Random.Range(0, shirtColors.Length)];


            skinMat.color = skin*.82f;
            captureCam.Render();
            RenderTexture.active = renderTexture;
            texture = new Texture2D(renderTexture.width, renderTexture.height);
            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture.Apply();
            followAR.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.SetTexture("_MainTex", texture);

            headJoint.transform.localScale = Vector3.Lerp(Vector3.one*1.2f, Vector3.one * 1.8f, Random.value);

            if (destroyFace)
                Destroy(_head);

            rebuild = false;

            captureGroup.SetActive(false);

            GameObject repo;
            if (FindObjectOfType<RepositionDistance>() == null)
            {
                repo = new GameObject();
                repo.name = "FollowARs_Repositioner";
                repo.AddComponent<RepositionDistance>();
                repo.GetComponent<RepositionDistance>().Init();
                AudioReverbZone reverb = repo.AddComponent<AudioReverbZone>();
                reverb.reverbPreset = AudioReverbPreset.Drugged;
            }
            else
            {
                repo = FindObjectOfType<RepositionDistance>().gameObject;
            }

            Destroy(root.gameObject);
            repo.GetComponent<RepositionDistance>().container = this.transform.parent.parent.gameObject;
            repo.GetComponent<RepositionDistance>().Add(this.transform.parent.gameObject,this.transform.parent.localPosition);



        }
    }
}