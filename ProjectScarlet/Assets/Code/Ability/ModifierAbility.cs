using System.Threading.Tasks;
using UnityEngine;

namespace ProjectScarlet
{    
    public abstract class ModifierAbility : Ability
    {
        [SerializeField] protected float _orginalModifier;
        [SerializeField] protected float _modifier;
        [SerializeField] protected int _modifiedTime;
    }
}