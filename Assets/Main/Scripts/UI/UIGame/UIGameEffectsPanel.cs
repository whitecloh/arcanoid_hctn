using System.Collections.Generic;
using System.Linq;
using Main.Scripts.Data;
using UnityEngine;

namespace Main.Scripts.UI.UIGame
{
    public class UIGameEffectsPanel : MonoBehaviour
    { 
        [SerializeField] private UIGameEffectsPanelItem uiGameEffectesPanelItem;
        [SerializeField] private RectTransform itemsParent;

        private List<UIGameEffectsPanelItem> ItemsPool = new();
    
        public UIGame Owner { get; set; }

        public void Show()
        {
            foreach (var item in Owner.LevelData.Effects)
            {
                var prefab = Instantiate(uiGameEffectesPanelItem, itemsParent);
            
                prefab.Owner = this;
                prefab.Fill(item.Sprite, item.Duration, item.Type);
                prefab.Hide();
            
                ItemsPool.Add(prefab);
            }
        }

        public void Hide()
        {
        
        }

        public void ShowEffect(EffectType type)
        {
            var item = ItemsPool.FirstOrDefault(o => o.EffectType == type);
       
            if(item == null) return;
       
            item.gameObject.SetActive(true);
            item.ActivateTimer();
        }
    }
}
