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


            //ע�ᱳ������
            m_itemModel.UpdateItemInInventoryEvent.Register((item, cnt) =>
            {
                if(cnt == 0)
                {
                    //����ɾ�����򷵻�
                    if (!m_itemInInventory.ContainsKey(item)) return;

                    //����ɾ����ӦGrid
                    Destroy(m_itemInInventory[item]);
                    m_itemInInventory.Remove(item);
                }
                else
                {
                    //���޶�ӦGrid����������
                    if (!m_itemInInventory.ContainsKey(item))
                    {
                        GenerateItemInInventory(item);
                    }
                    //��ʾ��Ʒ��Ŀ
                    m_itemInInventory[item].transform.Find("InventoryItemCnt").GetComponent<Text>().text = cnt.ToString();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            //ע��װ����Ʒ
            m_itemModel.UpdateItemEquipped.Register((item) =>
            {
                //װװ��
                if (m_playerModel.PlayerWeapon.Add(item.ItemId))
                {
                    GenerateItemEquipped(item);
                    m_itemModel.RemoveItemInInventory(item);
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            //ע��ж����Ʒ
            m_itemModel.UpdateItemUnEquipped.Register((item) =>
            {
                if (!m_itemEquipped.ContainsKey(item)) return;

                //жװ��
                m_playerModel.PlayerWeapon.Remove(item.ItemId);

                Destroy(m_itemEquipped[item][0]);
                m_itemEquipped[item].RemoveAt(0);
                if (m_itemEquipped[item].Count == 0)
                    m_itemEquipped.Remove(item);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            //��Ⱦ��Ʒ
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
                    //����������
                    purchaseBtnTrans.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        if (m_itemModel.Coin < item.ItemPrice)
                        {
                            //Ǯ������������ʾ
                            UIKitExtension.OpenPanelAsync<UITipPanel>(uiData: new UITipPanelData { TipText = "��Ҳ���" });
                            return;
                        }

                        m_itemModel.Coin.Value -= item.ItemPrice;
                        m_itemModel.AddItemInInventory(item);
                    });
                });
            }

            //��Ⱦ��������Ʒ
            var itemInInventory = m_itemModel.InInventoryItem;
            foreach (var item in itemInInventory)
            {
                GenerateItemInInventory(item.Key);
            }

            //��Ⱦ��װ����Ʒ
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

                //�����װ��
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
                    StoreBtn.transform.Find("StoreTitle").GetComponent<Text>().text = "<b>></b>�̵�";
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
                    StoreBtn.transform.Find("StoreTitle").GetComponent<Text>().text = "<b><</b>�̵�";
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
