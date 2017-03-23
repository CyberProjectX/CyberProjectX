using System;
using UnityEngine;

namespace Scripts.Client.Controllers.Projectiles
{
    public class VulcanProjectileController : BaseProjectileController
    {
        public override void MakeImpact(RaycastHit hitPoint)
        {
            base.MakeImpact(hitPoint);
            WeaponController.MakeImpact(hitPoint.point + hitPoint.normal * FxOffset);
        }
    }
}
