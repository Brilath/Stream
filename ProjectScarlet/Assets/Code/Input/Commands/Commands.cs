﻿using UnityEngine;

namespace ProjectScarlet
{
    public class MoveCommand : ICommand
    {
        public void Execute(GameObject unit)
        {
            Debug.Log("Execute Move Command");
            CharacterMotor motor = unit.GetComponent<CharacterMotor>();
            motor.Move();
        }
    }

    public class JumpCommand : ICommand
    {
        public void Execute(GameObject unit)
        {
            Debug.Log("Execute Jump Command");
            CharacterMotor motor = unit.GetComponent<CharacterMotor>();
            motor.Jump();
        }
    }

    public class SprintCommand : ICommand
    {
        public void Execute(GameObject unit)
        {
            Debug.Log("Execute Sprint Command");
            CharacterMotor motor = unit.GetComponent<CharacterMotor>();
            motor.ToggleSprint();
        }
    }

    public class BasicAttackCommand : ICommand
    {
        public void Execute(GameObject unit)
        {
            Debug.Log("Execute Attack Command");
        }
    }

    public class AlternativeAttackCommand : ICommand
    {
        public void Execute(GameObject unit)
        {
            Debug.Log("Execute Alternative Attack Command");
        }
    }

    public class SkillOneCommand : ICommand
    {
        public void Execute(GameObject unit)
        {
            Debug.Log("Execute Skill One Command");
            Ability ability1 = unit.GetComponent<CharacterAbilityProcessor>().GetAbility(0);
            Debug.Log($"Ability Name {ability1.Name}");

            if(ability1 != null)
                ability1.ProcessAbility(unit);
        }        
    }


    public class SkillTwoCommand : ICommand
    {
        public void Execute(GameObject unit)
        {
            Debug.Log("Execute Skill Two Command");
            Ability ability2 = unit.GetComponent<CharacterAbilityProcessor>().GetAbility(1);
            Debug.Log($"Ability Name {ability2.Name}");

            if (ability2 != null)
                ability2.ProcessAbility(unit);
        }
    }

    public class SkillThreeCommand : ICommand
    {
        public void Execute(GameObject unit)
        {
            Debug.Log("Execute Skill Three Command");
            Ability ability3 = unit.GetComponent<CharacterAbilityProcessor>().GetAbility(2);
            Debug.Log($"Ability Name {ability3.Name}");

            if (ability3 != null)
                ability3.ProcessAbility(unit);
        }
    }

    public class UltimateCommand : ICommand
    {
        public void Execute(GameObject unit)
        {
            Debug.Log("Execute Ultimate Command");
        }
    }
}