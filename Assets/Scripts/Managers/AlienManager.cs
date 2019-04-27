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
        private int sacrificeCost;

        public Action<int> OnEnergyRequest;
        public Action<int> OnUpdateSacrificeCost;
        public void Init(int _alienEnergyRequest, int _sacrificeCost)
        {
            baseAlienEnergyRequest = _alienEnergyRequest;
            SetSacrificeCost(_sacrificeCost);
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

        public void SetSacrificeCost(int value)
        {
            sacrificeCost = value;
            if (OnUpdateSacrificeCost != null)
            {
                OnUpdateSacrificeCost(sacrificeCost);
            }
        }
    }
}
