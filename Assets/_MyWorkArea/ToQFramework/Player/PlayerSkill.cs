using System.Collections.Generic;
using System.Linq;

namespace QFramework.Car
{
    public class PlayerSkill
    {
        public List<Skill_SO> SkillList = new List<Skill_SO>();
        private GameModel GameModel => GameArch.Interface.GetModel<GameModel>();

        public void Init()
        {
            //GameModel.BeforeGameStart.Register(DoSkillsEffect);
        }

        //private void DoSkillsEffect()
        //{
        //    foreach (var skill in SkillList)
        //        skill.DoSkillEffects();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skill"></param>
        /// <returns>��ӳɹ�</returns>
        public bool Add(Skill_SO skill)
        {
            if (SkillList.Contains(skill)) return false;
            if (skill.PreSkill.Count > 0 && !PreSkillFitOnAdd(skill)) return false;//��ǰ�ò����㣬���޷����

            GameModel.BeforeGameStart.Register(skill.SkillEffectsBeforeStart);
            GameModel.AfterGameStart.Register(skill.SkillEffectsAfterStart);

            SkillList.Add(skill);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skill"></param>
        /// <returns>�Ƴ��ɹ�</returns>
        public bool Remove(Skill_SO skill)
        {
            if (!SkillList.Contains(skill)) return false;
            if (!PreSkillFitOnRemove(skill)) return false;//�������ܱ���Ϊ��������Ч���ܵ�ǰ�ã����޷��Ƴ�

            GameModel.BeforeGameStart.UnRegister(skill.SkillEffectsBeforeStart);
            GameModel.AfterGameStart.UnRegister(skill.SkillEffectsAfterStart);

            SkillList.Remove(skill);
            return true;
        }

        //Ч��ƫ�ͣ���ģ����֮��Ҫ��취�Ż�
        private bool PreSkillFitOnAdd(Skill_SO skill)
        {
            if (skill.PreSkill == null || skill.PreSkill.Count == 0) return true;

            if (skill.IsNeedAllPreSkill)
            {
                foreach (var preSkill in skill.PreSkill)
                {
                    var result = SkillList.Where(skill => skill.Equals(preSkill)).ToList();
                    if (result.Count == 0)
                    {
                        UIKitExtension.OpenPanelAsync<UITipPanel>(uiData:new UITipPanelData {TipText = "��Ҫ���ٽ���һ��ǰ�ü���"});
                        return false;
                    }
                }

                return true;
            }
            else
            {
                foreach (var preSkill in skill.PreSkill)
                {
                    var result = SkillList.Where(skill => skill.Equals(preSkill)).ToList();
                    if (result.Count != 0)
                        return true;
                }

                UIKitExtension.OpenPanelAsync<UITipPanel>(uiData: new UITipPanelData { TipText = "��Ҫ���ٽ���һ��ǰ�ü���" });
                return false;
            }
        }

        //Ч��ƫ�ͣ���ģ����֮��Ҫ��취�Ż�
        private bool PreSkillFitOnRemove(Skill_SO skill)
        {
            List<Skill_SO> result = new List<Skill_SO>();//preskill�к��в���skill��Skill_SO�б�
            for (int i = 0; i < SkillList.Count; i++)
            {
                var tmp = SkillList[i].PreSkill.Where(preSkill => preSkill.Equals(skill)).ToList();
                if (tmp.Count != 0)
                {
                    result.Add(SkillList[i]);
                }
            }

            for (int i = 0; i < result.Count; i++)
            {
                if (!result[i].IsNeedAllPreSkill)//�������Ҫǰ�ü����е�����һ��������
                {
                    var tmp = result[i].PreSkill.Where(preSkill => SkillList.Contains(preSkill)).ToList();
                    if (tmp.Count == 1)//����ǰskill��Ψһʹ���߳�����skill�����ܷ���
                    {
                        UIKitExtension.OpenPanelAsync<UITipPanel>(uiData: new UITipPanelData { TipText = "����Ϊ�������ܵ�ǰ�ñ������ʱ�޷��Ƴ�" });
                        return false;
                    }
                }
                else//�����Ҫ����ǰ�ü��ܶ�������
                {
                    UIKitExtension.OpenPanelAsync<UITipPanel>(uiData: new UITipPanelData { TipText = "����Ϊ�������ܵ�ǰ�ñ������ʱ�޷��Ƴ�" });
                    return false;
                }
            }

            return true;
        }

    }

}
