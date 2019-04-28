using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD44
{
    public class ChamacoSpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform modelTransform;
        [SerializeField]
        private Transform iniTransform;
        [SerializeField]
        private Transform endTransform;
        [SerializeField]
        private AnimationCurve iniAnimationCurve;
        [SerializeField]
        private AnimationCurve endAnimationCurve;
        [SerializeField]
        private AnimationCurve iniYAnimationCurve;
        [SerializeField]
        private AnimationCurve endYAnimationCurve;

        public void Awake()
        {
            modelTransform.position = iniTransform.position;
        }

        public void SpawnChamacos(int count)
        {
            StopAllCoroutines();
            StartCoroutine(SpawnChamacosCoroutine());
            IEnumerator SpawnChamacosCoroutine()
            {
                modelTransform.position = iniTransform.position;
                // COME
                yield return DoFor(1f, (t) =>
                {
                    var yPos = Mathf.Lerp(iniTransform.position.y, transform.position.y, iniYAnimationCurve.Evaluate(t));
                    var planePosition = Vector3.Lerp(iniTransform.position, transform.position, iniAnimationCurve.Evaluate(t));
                    modelTransform.position = new Vector3(planePosition.x, yPos, planePosition.z);
                });
                modelTransform.position = transform.position;
                yield return new WaitForSeconds(0.5f);
                // SPAWN
                for (int i = 0; i < count; i++)
                {
                    var offset = new Vector3(Random.Range(-1f, 1f), -0.5f, Random.Range(-1f, 1f));
                    ChamacoManager.Instance.SpawnChamaco(transform.position + offset);
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(1f);
                // GO
                yield return DoFor(1f, (t) =>
                {
                    var yPos = Mathf.Lerp(transform.position.y, endTransform.position.y, endYAnimationCurve.Evaluate(t));
                    var planePosition = Vector3.Lerp(transform.position, endTransform.position, endAnimationCurve.Evaluate(t));
                    modelTransform.position = new Vector3(planePosition.x, yPos, planePosition.z);
                });
                modelTransform.position = endTransform.position;
            }
        }

        private IEnumerator DoFor(float totalTime, System.Action<float> action)
        {
            float timer = 0;
            while (timer <= totalTime)
            {
                float t = timer / totalTime;
                action(t);
                timer += Time.deltaTime;
                yield return null;
            }
        }
    }
}
