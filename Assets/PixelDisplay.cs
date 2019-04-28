using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD44
{
    public class PixelDisplay : MonoBehaviour
    {
        [SerializeField]
        private Texture texture1;

        [SerializeField]
        private Texture texture2;

        private Material material;

        private float currentTime;

        [SerializeField]
        private float updateTime;

        private bool isTexture1;

        void Awake()
        {
            material = GetComponent<MeshRenderer>().material;
            material.SetTexture("_MainTexture", texture1);
            isTexture1 = true;
        }

        void Update()
        {
            if (currentTime > updateTime)
            {
                currentTime = 0.0f;
                material.SetTexture("_MainTexture", isTexture1 ? texture1 : texture2);
                isTexture1 = !isTexture1;
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
    }
}
