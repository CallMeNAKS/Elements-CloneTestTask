using System.Collections.Generic;
using _Elements.CodeBase.Gameplay.Grid.Data;

namespace _Elements.CodeBase.Infrastructure.Service.Save
{
    [System.Serializable]
    public class GridStateWrapper
    {
        public int width;
        public int height;
        public List<int> flattenedData;

        public GridStateWrapper(GridElementType[,] data)
        {
            height = data.GetLength(0);
            width = data.GetLength(1);
            flattenedData = new List<int>(height * width);

            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                flattenedData.Add((int)data[y, x]);
        }

        public GridElementType[,] To2DArray()
        {
            var array = new GridElementType[height, width];
            for (int i = 0; i < flattenedData.Count; i++)
            {
                int y = i / width;
                int x = i % width;
                array[y, x] = (GridElementType)flattenedData[i];
            }

            return array;
        }
    }
}