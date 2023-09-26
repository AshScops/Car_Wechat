using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

namespace QFramework.Car
{
    public class AchieveSystem : AbstractSystem, IAchieveSystem
    {
        private List<IAchieve> m_achieveList;

        /// <summary>
        /// 给外部UI（PanelRunningUI）显示用的
        /// </summary>
        public EasyEvent<AchieveInfo> ShowAchieveUnlock;

        /// <summary>
        /// 成就解锁时触发
        /// </summary>
        private EasyEvent<IAchieve> m_unlockAchieve;


        /// <summary>
        /// 已解锁的成就Id
        /// </summary>
        private List<int> m_unlockedAchieveIdList;

        /// <summary>
        /// 已领取的成就Id
        /// </summary>
        private List<int> m_gotBonusAchieveIdList;

        protected override void OnInit()
        {
            m_achieveList = new List<IAchieve>();
            ShowAchieveUnlock = new EasyEvent<AchieveInfo>();
            m_unlockAchieve = new EasyEvent<IAchieve>();
            m_unlockedAchieveIdList = new List<int>();
            m_gotBonusAchieveIdList = new List<int>();

            //全new
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies.SelectMany(assembly => assembly.GetTypes());
            var filterTypes = types.Where(type => type.IsSubclassOf(typeof(AchieveBase)) && !type.ContainsGenericParameters && type.IsClass);
            var achieveTypes = filterTypes.ToList();
            for(int i = 0; i < achieveTypes.Count;i++)
            {
                var obj = Activator.CreateInstance(achieveTypes[i]);
                m_achieveList.Add(obj as IAchieve);
            }

            m_unlockAchieve.Register((achieve) =>
            {
                //通知解锁相关成就
                //Debug.Log("achieve:" + achieve.GetType());
                var achieveInfo = this.GetModel<AchieveModel>().AchieveInfoList.Where(
                    (singleInfo) => achieve.GetAchieveId() == singleInfo.AchieveId).First();

                m_unlockedAchieveIdList.Add(achieve.GetAchieveId());

                ShowAchieveUnlock.Trigger(achieveInfo);
            });

            //TODO:从云端读取是否解锁

            for (int i = 0; i < m_achieveList.Count; i++)
            {
                m_achieveList[i].Detect(m_unlockAchieve);
            }
        }

        public bool AchieveUnlocked(int id)
        {
            return m_unlockedAchieveIdList.Contains(id);
        }

        public bool AchieveBonusGot(int id)
        {
            return m_gotBonusAchieveIdList.Contains(id);
        }

        public void AddBonusGot(int id)
        {
            if(!m_gotBonusAchieveIdList.Contains(id))
                m_gotBonusAchieveIdList.Add(id);
        }
    }

}
