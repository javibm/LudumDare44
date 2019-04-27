using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LD44
{
    public class TimeManager : Singleton<TimeManager>
    {
        private float timePerDay;

        public Action OnDayEnded;
        public Action OnFactoryTicked;

        private float currentTime;

        private bool initialized = false;

        private float factoryTickedTime = 1.0f;

        public void Init(float _timePerDay)
        {
            initialized = true;
            timePerDay = _timePerDay;
            currentTime = 0.0f;
        }

        private void Update()
        {
            if (initialized)
            {
                if (currentTime > timePerDay)
                {
                    currentTime = 0.0f;
                    if (OnDayEnded != null)
                    {
                        OnDayEnded();
                    }
                }
                if (currentTime > factoryTickedTime)
                {
                    if (OnFactoryTicked != null)
                    {
                        OnFactoryTicked();
                    }
                }

                currentTime += Time.deltaTime;
            }
        }
    }
}
