using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Data
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "ScriptableObjects/LevelsData", order = 1)]
    public class LevelsData : ScriptableObject
    {
        [SerializeField] private List<LevelData> levels;
    
        public List<LevelData> Levels => levels;

        public int GetIndex(int currentLevel)
        {
            var firstLevelIndex = levels.Count - 1;
            var result = 0;
            while (currentLevel > firstLevelIndex)
            {
                firstLevelIndex += levels.Count;
                result += levels.Count;
            }

            return result;
        }
    }
}
