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

        private int baseAlienEnergyRequest;

        public Action<int> OnEnergyRequest;
        public void Init(int _alienEnergyRequest)
        {
            baseAlienEnergyRequest = _alienEnergyRequest;
            GetNextRequest();
        }
        public void GetNextRequest()
        {
            currentEnergyNeeded = baseAlienEnergyRequest;
            if (OnEnergyRequest != null)
            {
                OnEnergyRequest(currentEnergyNeeded);
            }
        }
    }
}
