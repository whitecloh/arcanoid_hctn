using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.UI.UIGame
{
   public class UIGameHealthPanelItem : MonoBehaviour
   {
      [SerializeField] private Image heartIcon;
      [SerializeField] private Sprite activeIcon;
      [SerializeField] private Sprite nonActiveIcon;
   
      public bool isActive = false;
      public UIGameHealthPanel Owner { get; set; }
   
      public void Fill()
      {
         heartIcon.sprite = activeIcon;
         isActive = true;
      }

      public void Remove()
      {
         isActive = false;
         heartIcon.sprite = nonActiveIcon;
      }
   }
}
