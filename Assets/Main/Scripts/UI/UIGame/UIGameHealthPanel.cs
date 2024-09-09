using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Main.Scripts.UI.UIGame
{
    public class UIGameHealthPanel : MonoBehaviour
    {
        [SerializeField] private UIGameHealthPanelItem uiGameEffectsPanelItem;
        [SerializeField] private RectTransform itemsParent;

        private List<UIGameHealthPanelItem> ItemsPool = new();
    
        public UIGame Owner { get; set; }

        public void Show()
        {
            for (var i = 0; i < Owner.LevelData.PlayerData.Healths; i++)
            {
                var prefab = Instantiate(uiGameEffectsPanelItem, itemsParent);
            
                prefab.Owner = this;
                prefab.Fill();
            
                ItemsPool.Add(prefab);
            }
        }

        public void RemoveHealths()
        {
            var item = ItemsPool.LastOrDefault(o => o.isActive);
            
            if(item == null) return;
            
            item.Remove();
        }
    }
}
