using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace QFramework.Car
{
	public partial class JoyStick : ViewController, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [Header("ҡ������")]
        public JoystickType type;

        private bool inDrag;//�Ƿ�����ק��
        private float maxDragDist;//�����ק����

        private BindableProperty<Vector2> v;//����+λ��

        #region external interfaces

        /// <summary>
        /// �õ�����+λ��
        /// </summary>
        public Vector2 GetDirAndDist()
        {
            return v;
        }

        /// <summary>
        /// �õ�����
        /// </summary>
        public Vector2 GetDir()
        {
            return v.Value.normalized;
        }

        #endregion

        private void Start()
        {
            Background.GetComponent<Image>().raycastTarget = false;
            Thumb.GetComponent<Image>().raycastTarget = false;
            maxDragDist = Background.rect.width / 2;
            if (type == JoystickType.PosCanChange || type == JoystickType.FollowMove)
            {
                UpdateShow(true);
            }

            v = new BindableProperty<Vector2>();
            v.RegisterWithInitValue((dir) =>
            {
                GameArch.Interface.GetModel<PlayerModel>().MoveDirection = dir;
            }).UnRegisterWhenGameObjectDestroyed(this);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            inDrag = true;

            if (type == JoystickType.PosCanChange || type == JoystickType.FollowMove)
            {
                Background.localPosition = eventData.position;
                //Background.localPosition = Screen2UI(eventData.position, Background.parent.GetComponent<RectTransform>());
                UpdateShow(true);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            //Vector2 targetLocalPos = Screen2UI(eventData.position, Thumb);
            Vector2 targetPos = eventData.position;
            Vector2 center = Background.localPosition;
            Vector2 targetDir = (targetPos - center).normalized;
            float distance = Vector2.Distance(targetPos, center);
            if (distance > maxDragDist)
            {
                if (type == JoystickType.FollowMove)
                {
                    float offset = distance - maxDragDist;
                    Background.anchoredPosition += targetDir * offset;
                }
                Thumb.localPosition = targetDir * maxDragDist;
            }
            else
            {
                Thumb.localPosition = targetDir * distance;
            }

            v.Value = targetDir * distance;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            v.Value = Vector2.zero;
            inDrag = false;

            if (type == JoystickType.PosCanChange || type == JoystickType.FollowMove)
            {
                UpdateShow(true);
            }
        }

        private void Update()
        {
            if (!inDrag)
            {
                Thumb.anchoredPosition = Vector2.Lerp(Thumb.anchoredPosition, Vector2.zero, 0.1f);
            }
        }

        /// <summary>
        /// ������ʾ
        /// </summary>
        void UpdateShow(bool isShow)
        {
            Background.gameObject.SetActive(isShow);
            Thumb.gameObject.SetActive(isShow);
        }

        /// <summary>
        /// ��Ļ����תUI����
        /// </summary>
        Vector2 Screen2UI(Vector2 v, RectTransform rect, Camera camera = null)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, v, camera, out pos);
            return pos;
        }
    }

    /// <summary>
    /// ҡ������
    /// </summary>
    public enum JoystickType
    {
        Normal,//�̶�λ��
        PosCanChange,//�ɱ�λ��
        FollowMove,//�����ƶ�
    }

}
