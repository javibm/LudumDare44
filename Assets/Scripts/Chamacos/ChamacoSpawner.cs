using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD44
{
    public class ChamacoSpawner : MonoBehaviour
    {
        public void SpawnChamacos(int count)
        {
            StopAllCoroutines();
            StartCoroutine(SpawnChamacosCoroutine());
            IEnumerator SpawnChamacosCoroutine()
            {
                for (int i = 0; i < count; i++)
                {
                    var offset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
                    ChamacoManager.Instance.SpawnChamaco(transform.position + offset);
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
    }
}
