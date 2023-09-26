using UnityEngine;
using System;
using System.Collections;
using QFramework.Custom;

namespace QFramework.Car
{
    public partial class Player : ViewController, IController
	{
        private const float TOUCH_Y_LOW_EDGE = 0.1f;
        private const float TOUCH_Y_HIGH_EDGE = 0.9f;

        private GameModel m_gameModel;
        private PlayerModel m_playerModel;
        private bool m_playing = false;

        public IArchitecture GetArchitecture()
        {
            return GameArch.Interface;
        }

        public void Init()
        {
            m_gameModel = this.GetModel<GameModel>();
            m_playerModel = this.GetModel<PlayerModel>();
            m_playerModel.PlayerTrans = transform;

            ///监听CurrentExp
            m_playerModel.CurrentExp.RegisterWithInitValue(currentExp =>
            {
                if (currentExp >= m_playerModel.LevelUpExpUpperLimit)
                {
                    m_playerModel.LevelUpExpUpperLimit.Value = 10 * m_playerModel.CurrentLevel;
                    m_playerModel.CurrentLevel.Value++;
                    m_playerModel.CurrentExp.Value = 0;
                    this.SendCommand(new ShowLevelUpUICommand());
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            ///监听CurrentLevel
            m_playerModel.CurrentLevel.Register(playerCurrentLevel =>
            {
                //print("更新LevelUI");

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            m_gameModel.GameState.RegisterWithInitValue(state =>
            {
                if (state == GameStates.isRunning)
                    m_playing = true;
                else
                    m_playing = false;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }


        private void Update()
        {
            if (!NetManager.NetInitDone) return;
            if (!m_playing) return;
            if (Camera.main == null) return;

            m_playerModel.OnUpdate();
            this.SendCommand(new CollectDropCommand());

            Vector3 direction = new Vector3(m_playerModel.MoveDirection.x, 0, m_playerModel.MoveDirection.y) ;
            Move(this.transform, direction, m_playerModel.MoveSpeed, m_playerModel.RotSpeed);
        }

        public void Move(Transform objectTrans, Vector3 direction, float moveSpeed, float rotSpeed)
        {
            direction = direction.normalized;
            //位移
            objectTrans.position += direction * moveSpeed * Time.deltaTime;

            //旋转
            if(direction.magnitude != 0f)
            {
                Quaternion targetRot = Quaternion.LookRotation(direction);
                objectTrans.rotation = Quaternion.Slerp(objectTrans.rotation, targetRot, rotSpeed * Time.deltaTime);
                //限制只沿Y轴旋转
                objectTrans.rotation = Quaternion.Euler(0, objectTrans.eulerAngles.y, 0);
            }
        }

        public IEnumerator HitFlash(float hurtCd)
        {
            var cd = hurtCd;
            var flashDuration = 0.1f;
            do
            {
                if (m_gameModel.GameState != GameStates.isRunning)
                {
                    yield return null;
                    continue;
                }

                Body.material.SetFloat("_ColorRange", 1f);
                yield return new WaitForSeconds(flashDuration);
                Body.material.SetFloat("_ColorRange", 0f);
                yield return new WaitForSeconds(flashDuration);

                cd -= Time.deltaTime + 2 * flashDuration;
            }
            while (cd > 0);
        }
    }
}
