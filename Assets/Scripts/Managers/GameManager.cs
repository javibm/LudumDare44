using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LD44
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        private int initialChamacos;

        [SerializeField]
        private float timePerDay;

        [SerializeField]
        private float chamacoEnergyPerSecond;

        [SerializeField]
        private float chamacoSecondsGeneratingEnergy;

        [SerializeField]
        private int chamacosKilledPerFail;

        private int currentChamacos;
        public int CurrentChamacos
        {
            get
            {
                return currentChamacos;
            }
        }

        private int readyChamacos;
        public int ReadyChamacos
        {
            get
            {
                return readyChamacos;
            }
        }

        private int restingChamacos;
        public int RestingChamacos
        {
            get
            {
                return restingChamacos;
            }
        }

        private int workingChamacos;
        public int WorkingChamacos
        {
            get
            {
                return workingChamacos;
            }
        }

        public Action OnGameOver;

        public void InitGame()
        {
            // Los chamacos empiezan directamente en ready
            currentChamacos = readyChamacos = initialChamacos;
            restingChamacos = workingChamacos = 0;

            // FactoryManager
            FactoryManager.Instance.Init(chamacoEnergyPerSecond);

            // Arranca el TimeManager
            TimeManager.Instance.Init(timePerDay);

            // Notificar a marcos que instancia los chamacos en Ready
        }

        private void SendChamacoToRest()
        {
            // Solo se pueden mover chamacos a rest desde work
            workingChamacos--;
            restingChamacos++;

            // Notificar a Marcos para que los mueva
        }

        private void SendChamacoToWork()
        {
            // Solo se pueden mover desde ready
            readyChamacos--;
            workingChamacos++;

            // Notificar a Marcos para que los mueva
        }

        private void SendChamacoToReady()
        {
            // Se pueden llamar a ready poque ha pasado el tiempo de rest o porque has gastado comida
            restingChamacos--;
            readyChamacos++;

            // Notificar a Marcos para que los mueva
        }

        private void NewChamacos(int quantity)
        {
            readyChamacos += quantity;
            currentChamacos += quantity;
            // Notificar a Marcos para que los spawnee
        }

        private void KillChamacos(int quantity)
        {
            readyChamacos -= quantity;
            currentChamacos -= quantity;
            CheckChumacosGameOver();
        }

        private void CheckChumacosGameOver()
        {
            if (readyChamacos <= 0)
            {
                OnGameOver();
            }
        }

        private void OnDayEnded()
        {
            if (FactoryManager.Instance.CurrentEnergy < AlienManager.Instance.CurrentEnergyNeeded)
            {
                KillChamacos(chamacosKilledPerFail);
            }
            else
            {
                int energyForShip = FactoryManager.Instance.CurrentEnergy - AlienManager.Instance.CurrentEnergyNeeded;
                ShipManager.Instance.AddEnergy(energyForShip);
            }

            FactoryManager.Instance.ResetEnergy();
        }
    }
}
