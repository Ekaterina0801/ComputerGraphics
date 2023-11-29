using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task8
{
    internal class Octahedron : Primitive
    {

    public Octahedron(double size) : base(Construct(size))
        {

    }
        private static Tuple<Vector[], int[][]> Construct(double size)
        {
            var vertices = new Vector[6];
            var indices = new int[8][];
            vertices[0] = new Vector(-size / 2, 0, 0);
            vertices[1] = new Vector(0, -size / 2, 0);
            vertices[2] = new Vector(0, 0, -size / 2);
            vertices[3] = new Vector(size / 2, 0, 0);
            vertices[4] = new Vector(0, size / 2, 0);
            vertices[5] = new Vector(0, 0, size / 2);


            indices[0] = new int[3] { 0, 2, 4};
            indices[1] = new int[3] { 2, 4, 3 };
            indices[2] = new int[3] { 4, 5, 3 };
            indices[3] = new int[3] { 0, 5, 4 };
            indices[4] = new int[3] { 0, 5, 1 };
            indices[5] = new int[3] { 5, 3, 1 };
            indices[6] = new int[3] { 0, 2, 1 };
            indices[7] = new int[3] { 2, 1, 3 };

            return new Tuple<Vector[], int[][]>(vertices, indices);
        }
    }
}
