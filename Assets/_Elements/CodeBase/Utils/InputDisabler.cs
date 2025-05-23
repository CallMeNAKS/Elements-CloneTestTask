using UnityEngine;
using UnityEngine.UI;

namespace _Elements.CodeBase.Utils
{
    public class InputDisabler : MonoBehaviour
    {
        [SerializeField] private GraphicRaycaster _raycaster;

        public void DisableInput()
        {
            _raycaster.enabled = false;
        }

        public void EnableInput()
        {
            _raycaster.enabled = true;
        }
    }
}