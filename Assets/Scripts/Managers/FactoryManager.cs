using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LD44
{
    public class FactoryManager : Singleton<FactoryManager>
    {
        private int currentEnergy;

        public int CurrentEnergy
        {
            get
            {
                return currentEnergy;
            }
        }

        private float currentEnergyChunk = 0.0f;

        private float chamacoEnergyPerSecond;

        public Action<int> OnChunkEnergyProduced;

        public Action<float> OnUpdatedEnergyProduction;

        public void Init(float _chamacoEnergyPerSecond)
        {
            chamacoEnergyPerSecond = _chamacoEnergyPerSecond;
            currentEnergy = 0;
            currentEnergyChunk = 0.0f;

            TimeManager.Instance.OnFactoryTicked += Tick;
        }

        private void Tick()
        {
            currentEnergyChunk += GameManager.Instance.WorkingChamacos * chamacoEnergyPerSecond;
            if (currentEnergyChunk >= 1.0f)
            {
                currentEnergy += (int)Math.Floor(currentEnergyChunk);
                currentEnergyChunk -= Math.Max(0, (int)Math.Floor(currentEnergyChunk));

                if (OnChunkEnergyProduced != null)
                {
                    OnChunkEnergyProduced(currentEnergy);
                }
            }

            if (OnUpdatedEnergyProduction != null)
            {
                OnUpdatedEnergyProduction(currentEnergyChunk);
            }
        }

        public void ResetEnergy()
        {
            currentEnergy = 0;
            OnChunkEnergyProduced(0);
        }
    }
}
