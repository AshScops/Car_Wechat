using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    [Serializable]
    [CreateAssetMenu(menuName = "Skill/SkillInfo", fileName = "Skill_")]
    public class Skill_SO : ScriptableObject
    {
        public string Name;
        public int Cost;

        [PreviewField]
        public Sprite Img;

        [TextArea]
        public string Description;

        [ShowInInspector, SerializeReference]
        public List<SkillBase> SkillEffects;

        [ShowInInspector]
        public bool IsNeedAllPreSkill;

        [ShowInInspector]
        public List<Skill_SO> PreSkill;


        public void SkillEffectsBeforeStart()
        {
            foreach (var skill in SkillEffects)
                skill.EffectsBeforeStart();
        }

        public void SkillEffectsAfterStart()
        {
            foreach (var skill in SkillEffects)
                skill.EffectsAfterStart();
        }

    }

}
