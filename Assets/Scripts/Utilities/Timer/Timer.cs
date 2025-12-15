
namespace Test.Core.Utilities
{
    public abstract class Timer
    {
        private float currentTime;
        protected TimerSettings timerSettings;

        public float CurrentTime { get => currentTime; set => currentTime = value; }

        public Timer(float startingTime, float endTime, float tickMultiplier)
        {
            timerSettings = new TimerSettings();
            timerSettings.StartTime = startingTime;
            timerSettings.EndTime = endTime;
            timerSettings.TickMultiplier = tickMultiplier;
            timerSettings.IsTicking = true;
        }
        public void SetCurrentTime(float timeToSet)
        {
            currentTime = timeToSet;
        }

        public abstract void Tick(float deltaTime);

        public virtual void AddTime(float value) => currentTime += value;

        public virtual void SetTickActive(bool value) => timerSettings.IsTicking = value;

        public virtual void Restart() => currentTime = timerSettings.StartTime;

        public TimerSettings GetTimerSettings()
        {
            return timerSettings;
        }

        public abstract bool IsFinished();
    }


    public class CountDownTimer : Timer
    {
        public CountDownTimer(float startingTime, float endTime, float tickMultiplier) : base(startingTime, endTime, tickMultiplier) { }

        public override bool IsFinished()
        {
            if (CurrentTime <= timerSettings.EndTime) return true;
            else return false;
        }

        public override void Tick(float deltaTime)
        {
            if (!timerSettings.IsTicking) return;

            if (!IsFinished())
                CurrentTime -= deltaTime * timerSettings.TickMultiplier;
        }
    }

    [System.Serializable]
    public class TimerSettings
    {
        private float startTime;
        private float endTime;
        private float tickMultiplier = 1;
        private bool isTicking;

        public float TickMultiplier { get => tickMultiplier; set => tickMultiplier = value; }
        public float EndTime { get => endTime; set => endTime = value; }
        public float StartTime { get => startTime; set => startTime = value; }
        public bool IsTicking { get => isTicking; set => isTicking = value; }
    }
}