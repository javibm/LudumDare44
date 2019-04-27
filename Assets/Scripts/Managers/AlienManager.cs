using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
