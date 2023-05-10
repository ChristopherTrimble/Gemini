//Author: Nathan Evans
using UnityEngine;

namespace Interfaces
{
    public interface IInteractable
    {
        string GetDescription();
        void Interact();

        void SetTarget(bool isTarget);
    }

}
