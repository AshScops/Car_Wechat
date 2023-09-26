using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace QFramework.Car
{
	public partial class SkillGrid : ViewController
	{
        [AssetList]
        public Skill_SO SkillSO;

        private PlayerModel PlayerModel => GameArch.Interface.GetModel<PlayerModel>();
        private ItemModel ItemModel => GameArch.Interface.GetModel<ItemModel>();

        private Image m_image;

        private void Awake()
        {
            transform.parent.GetComponent<UISkillTreePanel>().SkillInfos.Add(SkillSO, SkillBtn);

            DiamondCost.text = SkillSO.Cost.ToString();

            m_image = SkillBtn.gameObject.GetComponent<Image>();
            m_image.sprite = SkillSO.Img;
            if (PlayerModel.PlayerSkill.SkillList.Contains(SkillSO))
            {
                m_image.color = Color.white;
            }
        }

        void Start()
        {
            SkillBtn.onClick.AddListener(() =>
            {
                if (ItemModel.Diamond >= SkillSO.Cost && PlayerModel.PlayerSkill.Add(SkillSO))//≥¢ ‘ÃÌº”
                {
                    ItemModel.Diamond.Value -= SkillSO.Cost;
                    m_image.color = Color.white;
                }
                else if (PlayerModel.PlayerSkill.Remove(SkillSO))//≥¢ ‘“∆≥˝
                {
                    ItemModel.Diamond.Value += SkillSO.Cost;
                    m_image.color = Color.grey;
                }

            });

        }
    }
}
