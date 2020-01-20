using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectScarlet
{
    public abstract class CombatAbility : Ability
    {
        [SerializeField] private float _castTime = 1;

        public float CastTime { get { return _castTime; } protected set { _castTime = value; } }

        public CombatAbility()
        {
            Type = AbilityType.Combat;
        }
    }
}