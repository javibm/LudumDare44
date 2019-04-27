﻿using System.Collections;
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

        private float currentEnergyChunk;

        private float chamacoEnergyPerSecond;

        public Action OnChunkEnergyProduced;

        public Action<float> OnUpdatedEnergyProduction;

        public void Init(float _chamacoEnergyPerSecond)
        {
            chamacoEnergyPerSecond = _chamacoEnergyPerSecond;
            currentEnergy = 0;
            currentEnergyChunk = 0;

            TimeManager.Instance.OnFactoryTicked += Tick;
        }

        private void Tick()
        {
            currentEnergyChunk += GameManager.Instance.WorkingChamacos * chamacoEnergyPerSecond;
            if (currentEnergyChunk > 1.0f)
            {
                currentEnergyChunk = 0.0f;
                currentEnergy++;
                if (OnChunkEnergyProduced != null)
                {
                    OnChunkEnergyProduced();
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
        }
    }
}
