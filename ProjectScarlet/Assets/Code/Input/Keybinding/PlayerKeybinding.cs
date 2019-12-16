using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    [CreateAssetMenu(menuName = "Character/Keybindings", fileName = "PlayerKeybinding")]
    public class PlayerKeybinding : ScriptableObject
    {
        [SerializeField] private List<Keybind> _keybindings = new List<Keybind>();
        // Movement
        [SerializeField] private Keybind _forward;
        [SerializeField] private Keybind _right;
        [SerializeField] private Keybind _backward;
        [SerializeField] private Keybind _left;
        [SerializeField] private Keybind _jump;
        [SerializeField] private Keybind _sprint;
        
        // Attacks
        [SerializeField] private Keybind _basicAttack;
        [SerializeField] private Keybind _alternativeAttack;

        // Skills
        [SerializeField] private Keybind _skillOne;
        [SerializeField] private Keybind _skillTwo;
        [SerializeField] private Keybind _skillThree;
        [SerializeField] private Keybind _ultimate;

        public PlayerKeybinding()
        {
            Forward = new Keybind("Forward", KeyCode.W, new MoveCommand());
            Right = new Keybind("Right", KeyCode.D, new MoveCommand());
            Backward = new Keybind("Backward", KeyCode.S, new MoveCommand());
            Left = new Keybind("Left", KeyCode.A, new MoveCommand());
            Jump = new Keybind("Jump", KeyCode.Space, new JumpCommand());
            Sprint = new Keybind("Sprint", KeyCode.LeftShift, new SprintCommand());

            BasicAttack = new Keybind("Basic Attack", KeyCode.Mouse0, new BasicAttackCommand());
            AlternativeAttack = new Keybind("Alternative Attack", KeyCode.Mouse1, new AlternativeAttackCommand());

            SkillOne = new Keybind("Skill One", KeyCode.Alpha1, new SkillOneCommand());
            SkillTwo = new Keybind("Skill Two", KeyCode.Alpha2, new SkillTwoCommand());
            SkillThree = new Keybind("Skill Three", KeyCode.Alpha3, new SkillThreeCommand());
            Ultimate = new Keybind("Ultimate", KeyCode.Alpha4, new UltimateCommand());
            
            UpdateKeybindingList();       
        }

        public List<Keybind> Keybindings { get { return _keybindings; } private set { _keybindings = value; } }

        public Keybind Forward { get { return _forward; } private set { _forward = value; } }
        public Keybind Right { get { return _right; } private set { _right = value; } }
        public Keybind Backward { get { return _backward; } private set { _backward = value; } }
        public Keybind Left { get { return _left; } private set { _left = value; } }
        public Keybind Jump { get { return _jump; } set { _jump = value; } }
        public Keybind Sprint { get { return _sprint; } set { _sprint = value; } }

        public Keybind BasicAttack { get { return _basicAttack; } set { _basicAttack = value; } }
        public Keybind AlternativeAttack { get { return _alternativeAttack; } set { _alternativeAttack = value; } }

        public Keybind SkillOne { get { return _skillOne; } set { _skillOne = value; } }
        public Keybind SkillTwo { get { return _skillTwo; } set { _skillTwo = value; } }
        public Keybind SkillThree { get { return _skillThree; } set { _skillThree = value; } }
        public Keybind Ultimate { get { return _ultimate; } set { _ultimate = value; } }

        private void UpdateKeybindingList()
        {
            Keybindings.Clear();

            Keybindings.Add(Forward);
            Keybindings.Add(Right);
            Keybindings.Add(Backward);
            Keybindings.Add(Left);
            Keybindings.Add(Jump);
            Keybindings.Add(Sprint);

            Keybindings.Add(BasicAttack);
            Keybindings.Add(AlternativeAttack);

            Keybindings.Add(SkillOne);
            Keybindings.Add(SkillTwo);
            Keybindings.Add(SkillThree);
            Keybindings.Add(Ultimate);
        }
    }
}