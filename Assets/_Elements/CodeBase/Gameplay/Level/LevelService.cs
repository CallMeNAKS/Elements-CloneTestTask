using System.Collections.Generic;
using _Elements.CodeBase.Gameplay.Grid.Data;

namespace _Elements.CodeBase.Gameplay.Level
{
    public class LevelService
    {
        private readonly List<GridElementType[,]> _levels;
        private readonly List<int> _levelSequence;
        private readonly int _sequenceLength = 1000;
        private GridElementType[,] _savedLevel;
        public int CurrentLevel { get; private set; }


        public LevelService()
        {
            _levels = new List<GridElementType[,]>
            {
                new GridElementType[6, 4]
                {
                    { GridElementType.Water, GridElementType.Fire, GridElementType.Water, GridElementType.Water },
                    { GridElementType.Water, GridElementType.Fire, GridElementType.Water, GridElementType.Water },
                    { GridElementType.Fire, GridElementType.Water, GridElementType.Fire, GridElementType.Fire },
                    { GridElementType.Water, GridElementType.Water, GridElementType.Empty, GridElementType.Water },
                    { GridElementType.Water, GridElementType.Fire, GridElementType.Empty, GridElementType.Empty },
                    { GridElementType.Empty, GridElementType.Water, GridElementType.Empty, GridElementType.Empty },
                },
                new GridElementType[2, 5]
                {
                    { GridElementType.Water, GridElementType.Empty, GridElementType.Water, GridElementType.Fire, GridElementType.Fire },
                    { GridElementType.Water, GridElementType.Empty, GridElementType.Fire, GridElementType.Empty, GridElementType.Empty },
                },
                new GridElementType[5, 4]
                {
                    { GridElementType.Water, GridElementType.Fire, GridElementType.Water, GridElementType.Water },
                    { GridElementType.Water, GridElementType.Fire, GridElementType.Water, GridElementType.Water },
                    { GridElementType.Fire, GridElementType.Water, GridElementType.Fire, GridElementType.Fire },
                    { GridElementType.Water, GridElementType.Fire, GridElementType.Water, GridElementType.Water },
                    { GridElementType.Water, GridElementType.Water, GridElementType.Empty, GridElementType.Empty },
                }
            };
            
            _levelSequence = new List<int>(_sequenceLength);
            GenerateSequence();
        }

        private void GenerateSequence()
        {
            for (int i = 0; i < _sequenceLength; i++)
                _levelSequence.Add(i % _levels.Count);
        }
        
        public GridElementType[,] GetCurrentLevel()
        {
            if (_savedLevel != null)
            {
                var level =_savedLevel;
                _savedLevel = null;
                return level;
            }
            
            if (CurrentLevel >= _levelSequence.Count)
                throw new System.ArgumentOutOfRangeException(nameof(CurrentLevel), "No more levels in sequence");

            return _levels[_levelSequence[CurrentLevel]];
        }

        public void LevelComplete() => CurrentLevel++;

        public void LoadLevel(int saveCurrentLevel, GridElementType[,] savedLevel)
        {
            CurrentLevel = saveCurrentLevel;
            _savedLevel = savedLevel;
        }
    }
}