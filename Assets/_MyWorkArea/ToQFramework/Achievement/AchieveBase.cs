namespace QFramework.Car
{
    public abstract class AchieveBase : IAchieve
    {
        protected EasyEvent<IAchieve> m_unlockEvent;

        public void Detect(EasyEvent<IAchieve> unlockEvent)
        {
            m_unlockEvent = unlockEvent;
            DetectCondition();
        }

        public abstract void DetectCondition();

        public void Unlock()
        {
            m_unlockEvent.Trigger(this);
        }

        public abstract int GetAchieveId();
    }
}

