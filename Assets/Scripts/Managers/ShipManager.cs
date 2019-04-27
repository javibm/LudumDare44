using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public void AddEnergy(int quantity)
        {
            currentEnergy += quantity;
        }

    }
}
