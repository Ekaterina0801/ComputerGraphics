using task8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task8
{
    internal class Hexahedron : Primitive
    {
        public Hexahedron(double size) : base(Construct(size))
        {
            
        }
        private static Tuple<Vector[], int[][]> Construct(double size)
        {
            // кол-во вершин
            var vertices = new Vector[8];
            //кол - во граней
            var indices = new int[6][];
            vertices[0] = new Vector(-size / 2, -size / 2, -size / 2);
            vertices[1] = new Vector(-size / 2, -size / 2, size / 2);
            vertices[2] = new Vector(-size / 2, size / 2, -size / 2);
            vertices[3] = new Vector(size / 2, -size / 2, -size / 2);
            vertices[4] = new Vector(-size / 2, size / 2, size / 2);
            vertices[5] = new Vector(size / 2, -size / 2, size / 2);
            vertices[6] = new Vector(size / 2, size / 2, -size / 2);
            vertices[7] = new Vector(size / 2, size / 2, size / 2);

            indices[0] = new int[4] { 0,1,5,3 };
            indices[1] = new int[4] { 2,6,3,0 };
            indices[2] = new int[4] { 4, 1, 0, 2};
            indices[3] = new int[4] { 7, 5, 3, 6 };
            indices[4] = new int[4] { 2, 4, 7, 6 };
            indices[5] = new int[4] { 4, 1, 5, 7 };
            return new Tuple<Vector[], int[][]>(vertices, indices);
        }
    }
}
