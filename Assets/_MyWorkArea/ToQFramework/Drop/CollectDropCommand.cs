using UnityEngine;

namespace QFramework.Car
{
    public class CollectDropCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var playerModel = this.GetModel<PlayerModel>();
            var colliders = Physics.OverlapSphere(playerModel.PlayerTrans.position, playerModel.CollectRadius);
            foreach (var target in colliders)
            {
                ColliderInteractableOnEnter interactable;
                target.gameObject.TryGetComponent(out interactable);
                if (interactable != null)
                    interactable.ColliderInteractOnEnter();
            }
        }
    }
}