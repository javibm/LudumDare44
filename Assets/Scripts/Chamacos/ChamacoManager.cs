using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD44
{
    public class ChamacoManager : Singleton<ChamacoManager>
    {
        [SerializeField]
        private ChamacoController chamacoPrefab;
        [SerializeField]
        private ChamacoSpawner chamacoSpawner;
        [SerializeField]
        private ChamacosIdleArea idleArea;
        [SerializeField]
        private Transform workPlace;
        [SerializeField]
        private Transform restPlace;
        [SerializeField]
        private Transform workHall;
        [SerializeField]
        private Transform restHall;

        public ChamacosIdleArea IdleArea { get { return idleArea; } }
        public Transform WorkPlace { get { return workPlace; } }
        public Transform RestPlace { get { return restPlace; } }
        public Transform WorkHall { get { return workHall; } }
        public Transform RestHall { get { return restHall; } }

        public void SpawnChamacos(int count)
        {
            chamacoSpawner.SpawnChamacos(count);
        }

        public void SpawnChamaco(Vector3 spawnPos)
        {
            var chamaco = Instantiate(chamacoPrefab);
            chamaco.transform.position = spawnPos;
        }

        public IEnumerator DespawnChamacos(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ChamacoManager.Instance.DespawnChamaco();
                yield return new WaitForSeconds(0.3f);
            }
        }

        public void DespawnChamaco()
        {
            // Por ahora hacemos despawn a los ready
            if (idleChamacos.Count > 0)
            {
                int random = Random.Range(0, idleChamacos.Count);
                Destroy(idleChamacos[random].gameObject);
                idleChamacos.RemoveAt(random);

                GameManager.Instance.KillChamaco();
            }

        }

        public void MakeIdleChamacoGoToWork()
        {
            if (idleChamacos.Count > 0)
            {
                idleChamacos[0].GoToWork();
                idleChamacos.RemoveAt(0);
            }
        }

        public void MakeWorkingChamacoGoToRest()
        {
            if (workingChamacos.Count > 0)
            {
                workingChamacos[0].GoToRest();
                workingChamacos.RemoveAt(0);
            }
        }

        public void MakeRestingChamacoGoToIdle()
        {
            if (restingChamacos.Count > 0)
            {
                restingChamacos[0].Idle(true);
                restingChamacos.RemoveAt(0);
            }
        }

        public void OnChamacoIdle(ChamacoController chamaco)
        {
            idleChamacos.Add(chamaco);
        }

        public void OnChamacoWorking(ChamacoController chamaco)
        {
            workingChamacos.Add(chamaco);
        }

        public void OnChamacoResting(ChamacoController chamaco)
        {
            restingChamacos.Add(chamaco);
        }

        private List<ChamacoController> idleChamacos = new List<ChamacoController>();
        private List<ChamacoController> workingChamacos = new List<ChamacoController>();
        private List<ChamacoController> restingChamacos = new List<ChamacoController>();
    }
}
