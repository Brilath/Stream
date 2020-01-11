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

        public string Name { get { return _name; } protected set { _name = value; } }
        public int Cooldown { get { return _cooldown; } protected set { _cooldown  = value; } }
        public AbilityType Type { get { return _type; } protected set { _type = value; } }

        public abstract void ProcessAbility(GameObject unit);
    }
}