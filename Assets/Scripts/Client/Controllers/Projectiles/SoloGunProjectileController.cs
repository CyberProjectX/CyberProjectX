using UnityEngine;

namespace Scripts.Client.Controllers.Projectiles
{
    public class SoloGunProjectileController : BaseProjectileController
    {
        public override void MakeImpact(RaycastHit hitPoint)
        {
            base.MakeImpact(hitPoint);
            WeaponController.MakeImpact(hitPoint.point + hitPoint.normal * FxOffset);            
        }
    }
}
