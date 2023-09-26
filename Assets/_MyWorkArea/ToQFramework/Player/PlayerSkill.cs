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
        /// <returns>添加成功</returns>
        public bool Add(Skill_SO skill)
        {
            if (SkillList.Contains(skill)) return false;
            if (skill.PreSkill.Count > 0 && !PreSkillFitOnAdd(skill)) return false;//若前置不满足，则无法添加

            GameModel.BeforeGameStart.Register(skill.SkillEffectsBeforeStart);
            GameModel.AfterGameStart.Register(skill.SkillEffectsAfterStart);

            SkillList.Add(skill);
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skill"></param>
        /// <returns>移除成功</returns>
        public bool Remove(Skill_SO skill)
        {
            if (!SkillList.Contains(skill)) return false;
            if (!PreSkillFitOnRemove(skill)) return false;//若本技能被作为其他已生效技能的前置，则无法移除

            GameModel.BeforeGameStart.UnRegister(skill.SkillEffectsBeforeStart);
            GameModel.AfterGameStart.UnRegister(skill.SkillEffectsAfterStart);

            SkillList.Remove(skill);
            return true;
        }

        //效率偏低，规模做大之后要想办法优化
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
                        UIKitExtension.OpenPanelAsync<UITipPanel>(uiData:new UITipPanelData {TipText = "需要至少解锁一个前置技能"});
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

                UIKitExtension.OpenPanelAsync<UITipPanel>(uiData: new UITipPanelData { TipText = "需要至少解锁一个前置技能" });
                return false;
            }
        }

        //效率偏低，规模做大之后要想办法优化
        private bool PreSkillFitOnRemove(Skill_SO skill)
        {
            List<Skill_SO> result = new List<Skill_SO>();//preskill中含有参数skill的Skill_SO列表
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
                if (!result[i].IsNeedAllPreSkill)//如果仅需要前置技能中的任意一个被激活
                {
                    var tmp = result[i].PreSkill.Where(preSkill => SkillList.Contains(preSkill)).ToList();
                    if (tmp.Count == 1)//若当前skill是唯一使后者成立的skill，则不能放行
                    {
                        UIKitExtension.OpenPanelAsync<UITipPanel>(uiData: new UITipPanelData { TipText = "已作为其他技能的前置被激活，暂时无法移除" });
                        return false;
                    }
                }
                else//如果需要所有前置技能都被激活
                {
                    UIKitExtension.OpenPanelAsync<UITipPanel>(uiData: new UITipPanelData { TipText = "已作为其他技能的前置被激活，暂时无法移除" });
                    return false;
                }
            }

            return true;
        }

    }

}
