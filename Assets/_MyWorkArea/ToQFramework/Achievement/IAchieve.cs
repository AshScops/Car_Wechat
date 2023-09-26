using QFramework;

namespace QFramework.Car
{
    public interface IAchieve
    {
        public void Detect(EasyEvent<IAchieve> unlockEvent);

        public void Unlock();

        /// <summary>
        ///
        /// </summary>
        /// <returns>返回与成就信息对应的ID</returns>
        public int GetAchieveId();

    }

}
