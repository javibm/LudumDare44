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

        private int currentTick;
        public int CurrentTick
        {
            get
            {
                return currentTick;
            }
        }
        private float currentTickTime;

        private bool initialized = false;

        private float worldTickedTime;

        private Dictionary<int, List<Action>> timers;

        public void Init(int _timePerDay, float tickedTime)
        {
            timers = new Dictionary<int, List<Action>>();

            initialized = true;
            timePerDay = _timePerDay;
            worldTickedTime = tickedTime / 60;
            currentTick = 0;
            currentTickTime = 0.0f;
        }

        public void SetTimer(int time, Action OnTimeEnded)
        {
            int timeToAction = time + currentTick;

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
                CheckIfWorldTicked();
                CheckTimers();

                currentTickTime += Time.deltaTime;
            }
        }

        private void CheckIfWorldTicked()
        {
            if (currentTickTime > worldTickedTime)
            {
                currentTickTime = 0.0f;
                if (currentTick + 1 > timePerDay)
                {
                    currentTick = 0;
                    if (OnFactoryTicked != null)
                    {
                        OnFactoryTicked();
                    }
                    if (OnDayEnded != null)
                    {
                        Debug.Log("Day Ended");
                        OnDayEnded();
                    }
                }
                else
                {
                    currentTick++;
                    if (OnFactoryTicked != null)
                    {
                        OnFactoryTicked();
                    }
                }
            }
        }

        private void CheckTimers()
        {
            if (timers.ContainsKey(currentTick))
            {
                foreach (Action action in timers[currentTick].ToArray())
                {
                    if (action != null)
                    {
                        action();
                    }
                }
                timers.Remove(currentTick);
            }
        }
    }
}
