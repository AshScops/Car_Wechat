using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class BuffHandler
    {
        private Dictionary<Type, BuffBase> m_buffs = new Dictionary<Type, BuffBase>();

        private GameObject targetGO;

        private EnemyBuffUIHandler uIHandler;
        public BuffHandler(GameObject targetGO)
        {
            this.targetGO = targetGO;
            uIHandler = new EnemyBuffUIHandler(targetGO, this);
        }

        
        public EasyEvent<BuffBase> OnBuffHandlerAdd = new EasyEvent<BuffBase>();



        public void Add(Type buffType, int cnt = 1)
        {
            while(cnt > 0)
            {
                //´¦ÀíBUFFµþ¼Ó
                if (m_buffs.ContainsKey(buffType))
                {
                    m_buffs[buffType].OnOverlay();
                }
                else
                {
                    var buff = targetGO.AddComponent(buffType) as BuffBase;
                    m_buffs[buffType] = buff;
                    buff.OnBuffDestroy.Register(() =>
                    {
                        Remove(buffType);
                    });
                    OnBuffHandlerAdd.Trigger(buff);
                }

                cnt--;
            }
        }

        public void Remove(Type buffType)
        {
            if (m_buffs.ContainsKey(buffType))
            {
                m_buffs.Remove(buffType);
            }
                
        }


    }
}