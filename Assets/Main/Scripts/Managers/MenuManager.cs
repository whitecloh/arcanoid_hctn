using Main.Scripts.Data;
using Main.Scripts.UI.UIMenu;
using UnityEngine;

namespace Main.Scripts.Managers
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private LevelsData data;
    
        [SerializeField] private UIMenu uiMenu;

        private void Awake()
        {
            uiMenu.Initialize(data);
        }
    }
}
