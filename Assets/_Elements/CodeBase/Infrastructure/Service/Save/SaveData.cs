using _Elements.CodeBase.Gameplay.Grid.Data;

namespace _Elements.CodeBase.Infrastructure.Service.Save
{
    [System.Serializable]
    public class SaveData
    {
        public int currentLevel;
        public GridStateWrapper gridWrapper;

        public SaveData(int level, GridElementType[,] grid)
        {
            currentLevel = level;
            gridWrapper = new GridStateWrapper(grid);
        }
    }
}