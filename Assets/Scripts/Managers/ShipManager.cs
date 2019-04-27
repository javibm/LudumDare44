using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LD44
{
    public class ShipManager : Singleton<ShipManager>
    {
        private int currentEnergy;
        public int CurrentEnergy
        {
            get
            {
                return currentEnergy;
            }
        }

        public Action<int> OnEnergyAdded;

        public void AddEnergy(int quantity)
        {
            currentEnergy += quantity;
            if (OnEnergyAdded != null)
            {
                OnEnergyAdded(currentEnergy);
            }
        }

    }
}
