using System;
using UnityEngine;

namespace ProjectScarlet
{
    [Serializable]
    public class Keybind
    {
        [SerializeField] private string _name;
        [SerializeField] private KeyCode _key;
        [SerializeField] private ICommand _command;

        public Keybind()
        {

        }

        public Keybind(string name, KeyCode key, ICommand command)
        {
            Name = name;
            Key = key;
            Command = command;
        }

        public string Name { get { return _name; } set { _name = value; } }
        public KeyCode Key { get { return _key; } set {_key = value; } }
        public ICommand Command { get { return _command; } set { _command = value; } }
    }
}