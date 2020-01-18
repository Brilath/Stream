using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectScarlet
{
    public abstract class CombatAbility : Ability
    {
        [SerializeField] protected float _range = 10;
        [SerializeField] protected float _damage = 75;

        public float Range { get { return _range; } protected set { _range = value; } }
        public float Damage { get { return _damage; } protected set { _damage = value; } }

        public CombatAbility()
        {
            Type = AbilityType.Combat;
        }
    }
}