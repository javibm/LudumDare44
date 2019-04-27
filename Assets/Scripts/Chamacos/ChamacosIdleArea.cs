using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD44
{
    public class ChamacosIdleArea : MonoBehaviour
    {
        [SerializeField]
        private Transform limit1;
        [SerializeField]
        private Transform limit2;

        public Vector3 GetRandomPosition()
        {
            return new Vector3(Random.Range(limit1.position.x, limit2.position.x), 0f, Random.Range(limit1.position.z, limit2.position.z));
        }
    }
}
