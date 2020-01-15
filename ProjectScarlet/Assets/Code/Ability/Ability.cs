using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    
    public abstract class Ability : ScriptableObject
    {
        [SerializeField] protected string _name;
        [SerializeField] protected int _cooldown;
        [SerializeField] protected AbilityType _type;
        [SerializeField] protected float _countdown;
        [SerializeField] protected Sprite _icon;
        [SerializeField] protected AnimatorOverrideController overrideController;
        [SerializeField] protected Animation _animation;
        [SerializeField] protected AnimationClip _animationClip;       

        public string Name { get { return _name; } protected set { _name = value; } }
        public int Cooldown { get { return _cooldown; } protected set { _cooldown  = value; } }
        public float CountDown { get { return _countdown; } set { _countdown = value; } }
        public AbilityType Type { get { return _type; } protected set { _type = value; } }
        public Sprite Icon { get { return _icon; } protected set { _icon = value; } }

        public abstract void ProcessAbility(GameObject unit);        
    }
}