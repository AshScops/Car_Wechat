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
        /// <returns>������ɾ���Ϣ��Ӧ��ID</returns>
        public int GetAchieveId();

    }

}
