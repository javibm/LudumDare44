using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LD44
{
    public class AlienManager : Singleton<AlienManager>
    {
        private int currentEnergyNeeded;
        public int CurrentEnergyNeeded
        {
            get
            {
                return currentEnergyNeeded;
            }
        }

        public Action<int> OnEnergyRequest;

        public void GetNextRequest()
        {
            currentEnergyNeeded = UnityEngine.Random.Range(1, 3);
            if (OnEnergyRequest != null)
            {
                OnEnergyRequest(currentEnergyNeeded);
            }
        }
    }
}
