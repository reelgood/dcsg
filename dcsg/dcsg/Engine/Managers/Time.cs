using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace dcsg.Engine.Alarm
{
    /// <summary>
    /// Timer Delegate Callback. Void and Void basically...
    /// </summary>
    public delegate void TimeAlarmCallback();
    struct Timer
    {
        public TimeAlarmCallback callback;
        public DateTime hitTime;
    }
}
namespace dcsg.Engine
{
    public class Time
    {
        #region Private declarations

        static DateTime start_dt;
        static DateTime nowcount;
        static TimeSpan diff;
        static Time mainTime;
        static List<dcsg.Engine.Alarm.Timer> timers = new List<dcsg.Engine.Alarm.Timer>();
        public Time()
        {
            if (mainTime != null) { throw new NullReferenceException("Time object created twice"); }
            start_dt = DateTime.Now;
            nowcount = DateTime.Now;
            diff = new TimeSpan();
            mainTime = this;
            DCSG.OnUpdate += new DCSG.XNAHookEvent(this.DCSG_OnUpdate);
        }
        void DCSG_OnUpdate()
        {
            diff = DateTime.Now - nowcount;
            nowcount = DateTime.Now;
            for (int i = 0; i < timers.Count; i++)
            {
                if (timers[i].hitTime <= DateTime.Now)
                {
                    timers[i].callback.Invoke();
                    timers.RemoveAt(i);
                    i--;
                }
            }
        }

        #endregion

        #region Public declarations

        /// <summary>
        /// Time in seconds since last frame
        /// </summary>
        public static float DeltaTime
        {
            get
            {
                return (float)(diff.Ticks / 10000000.0);
            }
        }
        /// <summary>
        /// Time in seconds since game start
        /// </summary>
        public static float ElapsedTime
        {
            get
            {
                return (float)((DateTime.Now - start_dt).Ticks / 10000000.0);
            }
        }
        /// <summary>
        /// Initializes and starts a timer that will callback after specified time
        /// </summary>
        /// <param name="callback">Delegate to callback when timer expires</param>
        /// <param name="time">Time specified in seconds</param>
        public static void StartTimer(dcsg.Engine.Alarm.TimeAlarmCallback callback, float time)
        {
            timers.Add(new dcsg.Engine.Alarm.Timer() { callback = callback, hitTime = DateTime.Now + new TimeSpan((long)(time * 10000000)) });
        }

        #endregion
    }
}
