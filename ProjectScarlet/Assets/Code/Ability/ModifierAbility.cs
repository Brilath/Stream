using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectScarlet
{    
    public abstract class ModifierAbility : Ability
    {
        [SerializeField] protected Dictionary<GameObject, float> _unitOrginalStates = new Dictionary<GameObject, float>();
        [SerializeField] protected float _modifier;
        [SerializeField] protected int _modifiedTime;
        [SerializeField] protected float _range;
    }
}