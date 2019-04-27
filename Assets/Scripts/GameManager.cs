using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LD44
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        private int initialChamacos;

        private int currentChamacos;
        public int CurrentChamacos {
            get {
                return currentChamacos;
            }    
        }

        private void Awake() {
            currentChamacos = initialChamacos;
        }


    }
}
