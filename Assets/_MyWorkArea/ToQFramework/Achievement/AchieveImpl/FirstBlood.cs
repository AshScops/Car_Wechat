using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QFramework.Car
{
    public class FirstBlood : AchieveBase
    {
        private int m_cnt = 0;
        private EnemyModel m_enemyModel;

        public override void DetectCondition()
        {
            m_enemyModel = GameArch.Interface.GetModel<EnemyModel>();
            m_enemyModel.OnEnemyDead.Register(CalcCnt);
        }

        private void CalcCnt(Vector3 pos)
        {
            m_cnt++;
            if (m_cnt == 1)
            {
                Unlock();

                //½âËøÍêºóÒÆ³ý×¢²á
                m_enemyModel.OnEnemyDead.UnRegister(CalcCnt);
            }
        }

        public override int GetAchieveId()
        {
            return 1;
        }
    }

}
