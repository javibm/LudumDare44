using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD44
{
    public class ChamacoController : MonoBehaviour
    {
        [SerializeField]
        private Transform modelTransform;
        [SerializeField]
        private float movementSpeed = 1;
        [SerializeField]
        private AnimationCurve fallAnimationCurve;
        [SerializeField]
        private AnimationCurve walkYAnimationCurve;

        private float hallRadius = 1f;

        void Start()
        {
            Fall();
        }

        void Update()
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, currentDirection, 10f * Time.deltaTime);
        }

        public void Fall()
        {
            StopAllCoroutines();
            StartCoroutine(FallCoroutine());
            IEnumerator FallCoroutine()
            {
                Vector3 iniPos = transform.position;
                Vector3 endPos = new Vector3(iniPos.x, 0f, iniPos.z);
                float totalTime = 1;
                modelTransform.rotation = Random.rotation;
                yield return DoFor(totalTime, (t) =>
                {
                    transform.position = Vector3.Lerp(iniPos, endPos, fallAnimationCurve.Evaluate(t));
                });
                transform.position = endPos;
                modelTransform.rotation = Quaternion.identity;
                Idle();
                GameManager.Instance.NewChamaco();
            }
        }

        public void Idle(bool fromRest = false)
        {
            ChamacoManager.Instance.OnChamacoIdle(this);
            StopAllCoroutines();
            StartCoroutine(IdleCoroutine());
            IEnumerator IdleCoroutine()
            {
                if (fromRest)
                {
                    yield return GoToPositionCoroutine(ChamacoManager.Instance.RestHall.position, hallRadius);
                    GameManager.Instance.SendChamacoToReady();
                }
                while (true)
                {
                    yield return GoToPositionCoroutine(ChamacoManager.Instance.IdleArea.GetRandomPosition());
                    yield return new WaitForSeconds(1f);
                }
            }
        }

        public void GoToWork()
        {
            StopAllCoroutines();
            StartCoroutine(GoToWorkCoroutine());
            IEnumerator GoToWorkCoroutine()
            {
                yield return GoToPositionCoroutine(ChamacoManager.Instance.WorkHall.position, hallRadius);
                yield return GoToPositionCoroutine(ChamacoManager.Instance.WorkPlace.position);
                ChamacoManager.Instance.OnChamacoWorking(this);
                GameManager.Instance.SendChamacoToWork();
            }
        }

        public void GoToRest()
        {
            StopAllCoroutines();
            StartCoroutine(GoToRestCoroutine());
            IEnumerator GoToRestCoroutine()
            {
                yield return GoToPositionCoroutine(ChamacoManager.Instance.WorkHall.position, hallRadius);
                yield return GoToPositionCoroutine(ChamacoManager.Instance.RestHall.position, hallRadius);
                yield return GoToPositionCoroutine(ChamacoManager.Instance.RestPlace.position);
                ChamacoManager.Instance.OnChamacoResting(this);
                GameManager.Instance.SendChamacoToRest();
            }
        }

        private IEnumerator GoToPositionCoroutine(Vector3 newPos, float radius = 0)
        {
            Vector3 iniPos = transform.position;
            newPos = newPos - (newPos - iniPos).normalized * radius;
            float d = (newPos - iniPos).magnitude;
            float totalTime = d / movementSpeed;
            LookAt(newPos);
            float yTimer = 0f;
            float yTotalTime = 0.2f;
            yield return DoFor(totalTime, (t) =>
            {
                var yOffset = 0.2f * walkYAnimationCurve.Evaluate(yTimer / yTotalTime);
                yTimer = (yTimer + Time.deltaTime) % yTotalTime;
                var planePosition = Vector3.Lerp(iniPos, newPos, t);
                transform.position = new Vector3(planePosition.x, planePosition.y + yOffset, planePosition.z);
            });
            transform.position = newPos;
        }

        private void LookAt(Vector3 pos)
        {
            Quaternion iniRot = transform.rotation;
            transform.LookAt(pos);
            currentDirection = transform.rotation;
            transform.rotation = iniRot;
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

        private Quaternion currentDirection;
    }
}
