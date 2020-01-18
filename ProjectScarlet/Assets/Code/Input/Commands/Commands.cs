using UnityEngine;

namespace ProjectScarlet
{
    public class MoveCommand : ICommand
    {
        public void Execute(GameObject unit, Keybind bind)
        {
            Debug.Log("Execute Move Command");
            CharacterMotor motor = unit.GetComponent<CharacterMotor>();
            motor.Move();
        }
    }

    public class JumpCommand : ICommand
    {
        public void Execute(GameObject unit, Keybind bind)
        {
            Debug.Log("Execute Jump Command");
            CharacterMotor motor = unit.GetComponent<CharacterMotor>();
            motor.Jump();
        }
    }

    public class SprintCommand : ICommand
    {
        public void Execute(GameObject unit, Keybind bind)
        {
            Debug.Log("Execute Sprint Command");
            CharacterMotor motor = unit.GetComponent<CharacterMotor>();
            motor.ToggleSprint();
        }
    }

    public class BasicAttackCommand : ICommand
    {
        public void Execute(GameObject unit, Keybind bind)
        {
            Debug.Log("Execute Attack Command");
        }
    }

    public class AlternativeAttackCommand : ICommand
    {
        public void Execute(GameObject unit, Keybind bind)
        {
            Debug.Log("Execute Alternative Attack Command");
        }
    }

    public class SkillOneCommand : ICommand
    {
        public void Execute(GameObject unit, Keybind bind)
        {
            Ability ability = unit.GetComponent<CharacterAbilityProcessor>().GetAbility(0);            

            if(ability != null)
            {
                ability.KeyBind = bind.Key;
                ability.ProcessAbility(unit);
            }
        }        
    }

    public class SkillTwoCommand : ICommand
    {
        public void Execute(GameObject unit, Keybind bind)
        {
            Ability ability = unit.GetComponent<CharacterAbilityProcessor>().GetAbility(1);

            if (ability != null)
            {
                ability.KeyBind = bind.Key;
                ability.ProcessAbility(unit);
            }
        }
    }

    public class SkillThreeCommand : ICommand
    {
        public void Execute(GameObject unit, Keybind bind)
        {
            Ability ability = unit.GetComponent<CharacterAbilityProcessor>().GetAbility(2);

            if (ability != null)
            {
                ability.KeyBind = bind.Key;
                ability.ProcessAbility(unit);
            }
        }
    }

    public class UltimateCommand : ICommand
    {
        public void Execute(GameObject unit, Keybind bind)
        {
            Ability ultimate = unit.GetComponent<CharacterAbilityProcessor>().GetAbility(3);

            if (ultimate != null)
            {
                ultimate.KeyBind = bind.Key;
                ultimate.ProcessAbility(unit);
            }
        }
    }
}