using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Car
{
    public class Exp : ViewController, ColliderInteractableOnEnter
    {
        public int exp = 1;
        public float moveSpeed = 15f;

        /// <summary>bool:协程是否开始运行</summary>
        private bool bIsStartCoroutine = false;

        private float m_outDistance = 1.5f;

        IEnumerator MoveToPlayer()
        {
            var playerModel = GameArch.Interface.GetModel<PlayerModel>();
            var gameModel = GameArch.Interface.GetModel<GameModel>();
            this.bIsStartCoroutine = true;
            float distance;

            Vector3 outDirection = this.transform.position - playerModel.PlayerTrans.position;
            outDirection = outDirection.normalized;
            float lerpMult = 0.3f;
            do
            {
                yield return null;
                if (gameModel.GameState != GameStates.isRunning) continue;

                var v = outDirection * moveSpeed * Time.deltaTime;
                //这样插值是线性的，速度为原先的lerpMult倍，效果一般
                this.transform.position = Vector3.Lerp(this.transform.position, this.transform.position + v, lerpMult);
                m_outDistance -= v.magnitude * lerpMult;
            }
            while (m_outDistance > 0f);

            do
            {
                distance = Vector3.Distance(this.transform.position, playerModel.PlayerTrans.position);

                yield return null;
                if (gameModel.GameState != GameStates.isRunning) continue;

                Vector3 direction = playerModel.PlayerTrans.position - this.transform.position;
                direction = direction.normalized;

                this.transform.position += direction * moveSpeed * Time.deltaTime;
            }
            while (distance > 0.3f);


            GameArch.Interface.SendCommand(new GetExpCommand());
            Destroy(this.gameObject);
        }


        public void ColliderInteractOnEnter()
        {
            if (bIsStartCoroutine) return;

            StartCoroutine(MoveToPlayer());
        }
    }
}