using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD44
{
    public class PlayAnimationOnCollider : MonoBehaviour
    {
        [SerializeField]
        private Animation anim;

        private bool someoneInside;

        private static List<Collider> colliders = new List<Collider>();

        void OnTriggerEnter(Collider other)
        {
            colliders.Add(other);
            anim.Play("doorOpen");
        }

        void OnTriggerStay(Collider other)
        {
            if (colliders.Count > 1)
            {
                someoneInside = true;
            }
            else
            {
                someoneInside = false;
                colliders.Clear();
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (!someoneInside && colliders.Count == 0)
            {
                anim.Play("doorClose");
            }

            colliders.Remove(other);
        }
    }
}
