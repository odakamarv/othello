namespace BoardStruct
{
    public struct BoardMatrix
    {
        private BoardValues[,] matrix;

        public BoardMatrix(BoardValues InitValue)
        {
            matrix = new BoardValues[8, 8];

            for (var col = 0; col < 8; col++)
            {
                for (var row = 0; row < 8; row++)
                {
                    matrix[col, row] = InitValue;
                }
            }
        }

        public void Set(int col, int row, BoardValues value)
        {
            matrix[col, row] = value;
        }

        public BoardValues Get(int col, int row)
        {
            return matrix[col, row];
        }

        public BoardMatrix Clone()
        {
            var clone = new BoardMatrix(BoardValues.Empty);

            for (var col = 0; col < 8; col++)
            {
                for (var row = 0; row < 8; row++)
                {
                    clone.Set(col, row, matrix[col, row]);
                }
            }

            return clone;
        }
    }
}