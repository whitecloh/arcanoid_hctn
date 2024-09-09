using System.Collections.Generic;
using System.Linq;
using Main.Scripts.Data;
using UnityEngine;

namespace Main.Scripts.Game
{
    public class BlocksGenerator : MonoBehaviour
    {
        private LevelData _levelData;
        private List<Block> _allBlocks = new ();
        private List<Block> _availableBlocks = new ();
        private int _maxBlocksCount = 0;
        private int _minBlocksCount = 4;

        public int PointsInBlocks { get; private set; }

        public void Initialize(LevelData data, bool isNeedRandomGeneration)
        {
            _levelData = data;
            
            if (isNeedRandomGeneration)
            {
                InitializeBlocks();
            }
            else
            {
                InitializePredefinedBlocks();
            }
        }

        private void InitializeBlocks()
        {
            _allBlocks = GetComponentsInChildren<Block>().ToList();
            _availableBlocks = new List<Block>(_allBlocks);
            _maxBlocksCount = _availableBlocks.Count / _levelData.Effects.Count + 1;

            foreach (var effect in _levelData.Effects)
            {
                var count = Random.Range(_minBlocksCount, _maxBlocksCount);
                InitializeRandomBlocks(_availableBlocks, effect, count);

            }
            
            foreach (var block in _availableBlocks)
            {
                block.Initialize(_levelData.Effects[0], false , 1 , _levelData.Effects[0].Sprite);
            }
        }

        private void InitializeRandomBlocks(IList<Block> availableBlocks, EffectData type, int count)
        {
            for (var i = 0; i < count; i++)
            {
                if (availableBlocks.Count == 0)
                {
                    Debug.LogWarning("Not enough blocks to initialize!");
                    return;
                }

                var randomIndex = Random.Range(0, availableBlocks.Count);
                var block = availableBlocks[randomIndex];
                var health = _levelData.SetBlockHp();
            
                PointsInBlocks += health;
            
                block.Initialize(type, true , health , type.Sprite);
                availableBlocks.RemoveAt(randomIndex);
            }
        }

        private void InitializePredefinedBlocks()
        {
            _allBlocks = GetComponentsInChildren<Block>().ToList();
        
            foreach (var block in _allBlocks)
            {
                block.Initialize(_levelData.Effects[0], false , 1 , _levelData.Effects[0].Sprite);
            }
        
            foreach (var blockData in _levelData.BlocksData)
            {
                if (blockData.Index >= 0 && blockData.Index < _allBlocks.Count)
                {
                    var block = _allBlocks[blockData.Index];
                    var effect = _levelData.Effects.FirstOrDefault(o => o.Type == blockData.Type) ?? _levelData.Effects[0];

                    PointsInBlocks += blockData.Health;
                
                    block.Initialize(effect, blockData.IsActive, blockData.Health, effect.Sprite);
                }
                else
                {
                    Debug.LogWarning($"Block index {blockData.Index} is out of range!");
                }
            }
        }
    }
}
