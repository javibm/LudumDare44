using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD44
{
    public class ChamacosTestingUI : MonoBehaviour
    {
        public void SpawnChamaco()
        {
            ChamacoManager.Instance.SpawnChamaco(ChamacoManager.Instance.IdleArea.transform.position + Vector3.up * 5f);
        }

        public void SpawnChamaco10()
        {
            ChamacoManager.Instance.SpawnChamacos(10);
        }

        public void MakeChamacoGoToWork()
        {
            ChamacoManager.Instance.MakeIdleChamacoGoToWork();
        }

        public void MakeChamacoGoToRest()
        {
            ChamacoManager.Instance.MakeWorkingChamacoGoToRest();
        }

        public void MakeChamacoGoToIdle()
        {
            ChamacoManager.Instance.MakeRestingChamacoGoToIdle();
        }
    }
}
