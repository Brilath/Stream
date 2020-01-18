﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectScarlet
{
    [CreateAssetMenu(menuName = "Ability/Ranged Ability", fileName = "Ranged Combat Ability")]
    public class RangedCombatAbility : CombatAbility
    {
        [SerializeField] private List<Bullet> _bullets = new List<Bullet>();
        [SerializeField] private List<Vector3> _angle = new List<Vector3>();
        [SerializeField] private Vector3 _offset;
        [SerializeField] private int _castTime = 1;

        public override void ProcessAbility(GameObject unit)
        {

            Animator anim = unit.GetComponentInChildren<Animator>();

            if (anim != null)
                anim.Play(_clip.name);

            Cast(unit);

        }

        private async void Cast(GameObject unit)
        {
            unit.GetComponent<CharacterMotor>().Stop();
            unit.GetComponent<CharacterMotor>().CanMove = false;

            await Task.Delay(_castTime * 1000);

            for (int i = 0; i < _bullets.Count; i++)
            {
                Transform attackPoint = unit.GetComponent<Fighter>().AttackPoint;
                Quaternion newRotation = unit.transform.rotation * Quaternion.Euler(_angle[i]);                

                var b = Instantiate(_bullets[i], unit.transform.position + _offset,
                    newRotation);
                b.Launch(attackPoint);
            }

            unit.GetComponent<CharacterMotor>().CanMove = true;
        }
    }
}