using UnityEngine;

namespace ProjectScarlet
{
    public interface ICommand
    {
        void Execute(GameObject unit, Keybind bind);
    }
}