using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class Matrix
    {
        private double[,] data;

        private int m;
        public int M { get => this.m; }

        private int n;
        public int N { get => this.n; }

        public Matrix(int m, int n)
        {
            this.m = m;
            this.n = n;
            this.data = new double[m, n];
        }
        public double this[int x, int y]
        {
            get
            {
                return this.data[x, y];
            }
            set
            {
                this.data[x, y] = value;
            }
        }

        public static Matrix operator *(Matrix matrix, double value)
        {
            var res = new Matrix(matrix.M, matrix.N);
            for (var i = 0; i < res.M; i++)
            {
                for (var j = 0; j < res.N; j++)
                {
                    res[i, j] = matrix[i, j] * value;
                }
            }
            return res;
        }

        public static Matrix operator *(Matrix matr, Matrix matr2)
        {
            var m1 = matr.M;
            var n1 = matr.N;
            var m2 = matr2.M;
            var n2 = matr2.N;

            var res = new Matrix(m1, n2);
            for (int i = 0; i < res.M; i++)
            {
                for (int j = 0; j < res.N; j++)
                {
                    for (int k = 0; k < n1; k++)
                    {
                        res[i, j] += matr[i,k] * matr2[k,j];
                    }
                }
            }
            return res;
        }
    }
}
