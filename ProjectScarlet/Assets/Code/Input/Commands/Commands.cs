using UnityEngine;

namespace ProjectScarlet
{
    public class MoveCommand : ICommand
    {
        public void Execute(CharacterMotor motor)
        {
            Debug.Log("Execute Move Command");
            motor.Move();
        }
    }

    public class JumpCommand : ICommand
    {
        public void Execute(CharacterMotor motor)
        {
            Debug.Log("Execute Jump Command");
            motor.Jump();
        }
    }

    public class SprintCommand : ICommand
    {
        public void Execute(CharacterMotor motor)
        {
            Debug.Log("Execute Sprint Command");
            motor.ToggleSprint();
        }
    }

    public class BasicAttackCommand : ICommand
    {
        public void Execute(CharacterMotor motor)
        {
            Debug.Log("Execute Attack Command");
        }
    }

    public class AlternativeAttackCommand : ICommand
    {
        public void Execute(CharacterMotor motor)
        {
            Debug.Log("Execute Alternative Attack Command");
        }
    }

    public class SkillOneCommand : ICommand
    {
        public void Execute(CharacterMotor motor)
        {
            Debug.Log("Execute Skill One Command");
        }
    }


    public class SkillTwoCommand : ICommand
    {
        public void Execute(CharacterMotor motor)
        {
            Debug.Log("Execute Skill Two Command");
        }
    }

    public class SkillThreeCommand : ICommand
    {
        public void Execute(CharacterMotor motor)
        {
            Debug.Log("Execute Skill Three Command");
        }
    }

    public class UltimateCommand : ICommand
    {
        public void Execute(CharacterMotor motor)
        {
            Debug.Log("Execute Ultimate Command");
        }
    }
}