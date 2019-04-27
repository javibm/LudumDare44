using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LD44
{
    public class TimeManager : Singleton<TimeManager>
    {
        private int timePerDay;

        public Action OnDayEnded;
        public Action OnFactoryTicked;

        private float currentTime;
        private float currentTickTime;

        private bool initialized = false;

        private int worldTickedTime = 1;

        private Dictionary<int, List<Action>> timers;

        public void Init(int _timePerDay)
        {
            timers = new Dictionary<int, List<Action>>();

            initialized = true;
            timePerDay = _timePerDay;
            currentTime = 0.0f;
        }

        public void SetTimer(int time, Action OnTimeEnded)
        {
            int timeToAction = time + (int)currentTime;

            if (timeToAction > timePerDay)
            {
                timeToAction -= timePerDay;
            }

            if (!timers.ContainsKey(timeToAction))
            {
                timers[timeToAction] = new List<Action>();
            }
            timers[timeToAction].Add(OnTimeEnded);
        }

        private void Update()
        {
            if (initialized)
            {
                CheckIfDayEnded();
                CheckIfWorldTicked();
                CheckTimers();

                currentTime += Time.deltaTime;
                currentTickTime += Time.deltaTime;
            }
        }

        private void CheckIfDayEnded()
        {
            if (currentTime > timePerDay)
            {
                currentTime = 0.0f;
                if (OnDayEnded != null)
                {
                    Debug.Log("Day Ended");
                    OnDayEnded();
                }
            }
        }

        private void CheckIfWorldTicked()
        {
            if (currentTickTime > worldTickedTime)
            {
                currentTickTime = 0.0f;
                if (OnFactoryTicked != null)
                {
                    Debug.Log("World Ticked");
                    OnFactoryTicked();
                }
            }
        }

        private void CheckTimers()
        {
            if (timers.ContainsKey((int)currentTime))
            {
                foreach (Action action in timers[(int)currentTime])
                {
                    if (action != null)
                    {
                        action();
                    }
                }
            }
        }
    }
}
