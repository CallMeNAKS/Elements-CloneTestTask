namespace _Elements.CodeBase.Gameplay.Grid.Data
{
    public class GridFieldData
    {
        private GridElementType[,] _elements;

        public int Rows => _elements.GetLength(0);
        public int Columns => _elements.GetLength(1);

        public void LoadLevel(GridElementType[,] levelData)
        {
            _elements = levelData;
        }

        public GridElementType GetAt(int row, int column)
        {
            return _elements[row, column];
        }
    }
}