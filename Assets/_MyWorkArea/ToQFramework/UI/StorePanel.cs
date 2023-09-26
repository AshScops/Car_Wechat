using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

namespace QFramework.Car
{
	public class StorePanelData : UIPanelData
	{
    }

	public partial class StorePanel : UIPanel
	{
        private PlayerModel m_playerModel;
        private ItemModel m_itemModel;
        private bool m_storeOpened = false;

        private Dictionary<ItemData, GameObject> m_itemInInventory;
        private Dictionary<ItemData, List<GameObject>> m_itemEquipped;

        protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as StorePanelData ?? new StorePanelData();

            m_playerModel = GameArch.Interface.GetModel<PlayerModel>();
            m_itemModel = GameArch.Interface.GetModel<ItemModel>();
            m_itemInInventory = new Dictionary<ItemData, GameObject>();
            m_itemEquipped = new Dictionary<ItemData, List<GameObject>>();


            m_itemModel.Coin.RegisterWithInitValue((coin) =>
            {
                CoinNum.text = coin.ToString();
            }).UnRegisterWhenGameObjectDestroyed(this);


            //注册背包更新
            m_itemModel.UpdateItemInInventoryEvent.Register((item, cnt) =>
            {
                if(cnt == 0)
                {
                    //若已删除，则返回
                    if (!m_itemInInventory.ContainsKey(item)) return;

                    //否则删除对应Grid
                    Destroy(m_itemInInventory[item]);
                    m_itemInInventory.Remove(item);
                }
                else
                {
                    //若无对应Grid，则生成它
                    if (!m_itemInInventory.ContainsKey(item))
                    {
                        GenerateItemInInventory(item);
                    }
                    //显示物品数目
                    m_itemInInventory[item].transform.Find("InventoryItemCnt").GetComponent<Text>().text = cnt.ToString();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            //注册装备物品
            m_itemModel.UpdateItemEquipped.Register((item) =>
            {
                //装装备
                if (m_playerModel.PlayerWeapon.Add(item.ItemId))
                {
                    GenerateItemEquipped(item);
                    m_itemModel.RemoveItemInInventory(item);
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            //注册卸下物品
            m_itemModel.UpdateItemUnEquipped.Register((item) =>
            {
                if (!m_itemEquipped.ContainsKey(item)) return;

                //卸装备
                m_playerModel.PlayerWeapon.Remove(item.ItemId);

                Destroy(m_itemEquipped[item][0]);
                m_itemEquipped[item].RemoveAt(0);
                if (m_itemEquipped[item].Count == 0)
                    m_itemEquipped.Remove(item);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            //渲染商品
            foreach (var item in m_itemModel.ItemDataList)
            {
                ResUtil.GenerateGOAsync("ItemGrid", StoreRoot, (grid) =>
                {
                    ResUtil.LoadTextureAsync(item.ItemImg, (tex) =>
                    {
                        Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
                        grid.transform.Find("ItemImg").GetComponent<Image>().sprite = sprite;
                    });
                    
                    grid.transform.Find("ItemName").GetComponent<Text>().text = item.ItemName;
                    var purchaseBtnTrans = grid.transform.Find("PurchaseBtn");
                    purchaseBtnTrans.Find("Price").GetComponent<Text>().text = item.ItemPrice.ToString();
                    //购买后进背包
                    purchaseBtnTrans.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        if (m_itemModel.Coin < item.ItemPrice)
                        {
                            //钱不够，弹窗提示
                            UIKitExtension.OpenPanelAsync<UITipPanel>(uiData: new UITipPanelData { TipText = "金币不足" });
                            return;
                        }

                        m_itemModel.Coin.Value -= item.ItemPrice;
                        m_itemModel.AddItemInInventory(item);
                    });
                });
            }

            //渲染背包中物品
            var itemInInventory = m_itemModel.InInventoryItem;
            foreach (var item in itemInInventory)
            {
                GenerateItemInInventory(item.Key);
            }

            //渲染已装备物品
            var itemEquipped = m_itemModel.EquippedItem;
            foreach (var item in itemEquipped)
            {
                GenerateItemEquipped(item);
            }
        }

        private void GenerateItemInInventory(ItemData item)
        {
            ResUtil.GenerateGOAsync("InventoryItemBtn", InventoryItemParent, (grid) =>
            {
                ResUtil.LoadTextureAsync(item.ItemImg, (tex) =>
                {
                    Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
                    grid.GetComponent<Image>().sprite = sprite;
                });

                //点击后装备
                grid.GetComponent<Button>().onClick.AddListener(() =>
                {
                    m_itemModel.EquipItem(item);
                });
                m_itemInInventory.Add(item, grid);
            });
        }

        private void GenerateItemEquipped(ItemData item)
        {
            ResUtil.GenerateGOAsync("EquippedItemBtn", EquippedItemParent, (grid) =>
            {
                ResUtil.LoadTextureAsync(item.ItemImg, (tex) =>
                {
                    Sprite sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
                    grid.GetComponent<Image>().sprite = sprite;
                });
                
                grid.GetComponent<Button>().onClick.AddListener(() =>
                {
                    m_itemModel.UnEquipItem(item);
                    m_itemModel.AddItemInInventory(item);
                });

                if (!m_itemEquipped.ContainsKey(item))
                    m_itemEquipped[item] = new List<GameObject>();
                m_itemEquipped[item].Add(grid);
            });
        }

        protected override void OnOpen(IUIData uiData = null)
		{
            this.StoreBtn.onClick.AddListener(() =>
            {
                m_storeOpened = !m_storeOpened;
                //this.ConfirmBtn.interactable = !storeOpened;

                if (m_storeOpened)
                {
                    StoreBtn.transform.Find("StoreTitle").GetComponent<Text>().text = "<b>></b>商店";
                    //StoreBtn.GetComponent<RectTransform>().DOLocalMoveX(0, 0.3f);

                    ActionKit.Custom(c =>
                    {
                        c.OnStart(() =>
                        {
                            GameArch.Interface.GetSystem<GameSystem>().GamePause();
                            PanelRoot.DOAnchorPos(new Vector2(-1920f / 2f, 0), 0.3f);
                        });

                    }).Start(this);
                }
                else
                {
                    StoreBtn.transform.Find("StoreTitle").GetComponent<Text>().text = "<b><</b>商店";
                    //StoreBtn.GetComponent<RectTransform>().DOLocalMoveX(1920f/ 2f, 0.3f);
                    //StoreBtn.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-1920f / 2f, 0), 0.3f).From(true);

                    ActionKit.Custom(c =>
                    {
                        c.OnStart(() => { PanelRoot.DOAnchorPos(new Vector2(0, 0), 0.3f).OnComplete(c.Finish); });
                        c.OnFinish(() => { GameArch.Interface.GetSystem<GameSystem>().GameResume(); });

                    }).Start(this);
                }

            });
        }
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
