using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab63D
{
    class Matrix
    {
        private double[,] matrix;

        public int Rows { get { return matrix.GetLength(0); } }
        public int Columns { get { return matrix.GetLength(1); } }
        public double[,] Value { get { return matrix; } }

        public Matrix(int rows, int columns)
        {
            matrix = new double[rows, columns];
        }

        public Matrix(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);
            this.matrix = new double[rows, columns];
            Array.Copy(matrix, this.matrix, rows * columns);
        }

        public double this[int i, int j]
        {
            get { return matrix[i, j]; }
            set { matrix[i, j] = value; }
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            int rows1 = m1.Rows;
            int columns1 = m1.Columns;
            int columns2 = m2.Columns;
            Matrix result = new Matrix(rows1, columns2);

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < columns2; j++)
                {
                    for (int k = 0; k < columns1; k++)
                    {
                        result[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }

            return result;
        }
    }
}
