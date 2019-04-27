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
        private int incrementAlienEnergyRequest;
        private int sacrificeCost;

        public Action<int> OnEnergyRequest;
        public Action<int> OnUpdateSacrificeCost;
        public void Init(int _alienEnergyRequest, int _incrementAlienEnergyRequest, int _sacrificeCost)
        {
            baseAlienEnergyRequest = _alienEnergyRequest;
            incrementAlienEnergyRequest = _incrementAlienEnergyRequest;
            SetSacrificeCost(_sacrificeCost);
            GetNextRequest(true);
        }
        public void GetNextRequest(bool initial = false)
        {
            currentEnergyNeeded = initial ? baseAlienEnergyRequest : currentEnergyNeeded + incrementAlienEnergyRequest;
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
