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
        private int timePerDay;

        [SerializeField]
        private float chamacoEnergyPerSecond;

        [SerializeField]
        private int chamacoSecondsWorking;

        [SerializeField]
        private int chamacosKilledPerFail;

        [SerializeField]
        private int chamacoSecondsResting;

        [Header("UI")]
        [SerializeField]
        private GameplayUIManager gameplayUIManager;

        private int days;
        public int Days
        {
            get
            {
                return days;
            }
            private set
            {
                days = value;
                gameplayUIManager.UpdateDaysLabel(days);
            }
        }

        private int ticks;

        private int currentChamacos;

        private int readyChamacos;

        private int restingChamacos;

        private int workingChamacos;
        public int WorkingChamacos
        {
            get
            {
                return workingChamacos;
            }
        }

        public Action OnGameOver;
        public Action<int> OnCurrentChamacosModified;
        public Action<int> OnWorkingChamacosModified;
        public Action<int> OnRestingChamacosModified;
        public Action<int> OnReadyChamacosModified;

        public void Awake()
        {
            InitGame();
        }
        public void InitGame()
        {
            gameplayUIManager.Init();

            // Reset
            Days = 0;
            SetCurrentChamacos(initialChamacos);
            SetReadyChamacos(initialChamacos);
            SetRestingChamacos(0);
            SetWorkingChamacos(0);

            // Arranca el TimeManager
            TimeManager.Instance.Init(timePerDay);
            TimeManager.Instance.OnDayEnded += OnDayEnded;

            // FactoryManager
            FactoryManager.Instance.Init(chamacoEnergyPerSecond);
            AlienManager.Instance.GetNextRequest();
            // Notificar a marcos que instancia los chamacos en Ready

        }

        private void OnDestroy()
        {
            if (TimeManager.Instance != null)
            {
                TimeManager.Instance.OnDayEnded -= OnDayEnded;
            }
        }

        private void SendChamacoToRest()
        {
            // Solo se pueden mover chamacos a rest desde work
            SetWorkingChamacos(--workingChamacos);
            SetRestingChamacos(++restingChamacos);

            // Notificar a Marcos para que los mueva

            // Timer para que dejen de descansar
            TimeManager.Instance.SetTimer(chamacoSecondsResting, SendChamacoToReady);
        }

        public void SendChamacoToWork()
        {
            if (readyChamacos > 0)
            {
                // Solo se pueden mover desde ready
                SetWorkingChamacos(++workingChamacos);
                SetReadyChamacos(--readyChamacos);

                // Notificar a Marcos para que los mueva

                // Timer para que dejen de trabajar
                TimeManager.Instance.SetTimer(chamacoSecondsWorking, SendChamacoToRest);
            }
        }

        private void SendChamacoToReady()
        {
            // Se pueden llamar a ready poque ha pasado el tiempo de rest o porque has gastado comida
            SetRestingChamacos(--restingChamacos);
            SetReadyChamacos(++readyChamacos);

            // Notificar a Marcos para que los mueva

        }

        private void NewChamacos(int quantity)
        {
            SetReadyChamacos(readyChamacos + quantity);
            SetCurrentChamacos(currentChamacos + quantity);
            // Notificar a Marcos para que los spawnee

        }

        private void KillChamacos(int quantity)
        {
            SetReadyChamacos(readyChamacos - quantity);
            SetCurrentChamacos(currentChamacos - quantity);
            // Notificar a Marcos para que los despawnee

            CheckChumacosGameOver();
        }

        private void SetCurrentChamacos(int value)
        {
            currentChamacos = value;
            if (OnCurrentChamacosModified != null)
            {
                OnCurrentChamacosModified(value);
            }
        }

        private void SetWorkingChamacos(int value)
        {
            workingChamacos = value;
            if (OnWorkingChamacosModified != null)
            {
                OnWorkingChamacosModified(value);
            }
        }

        private void SetRestingChamacos(int value)
        {
            restingChamacos = value;
            if (OnRestingChamacosModified != null)
            {
                OnRestingChamacosModified(value);
            }
        }

        private void SetReadyChamacos(int value)
        {
            readyChamacos = value;
            if (OnReadyChamacosModified != null)
            {
                OnReadyChamacosModified(value);
            }
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
            Days++;

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
