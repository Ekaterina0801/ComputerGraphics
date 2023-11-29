using System;
using System.Collections.Generic;
using System.Diagnostics;
using task9;
using task9.Properties;
using Texturing;

namespace task9
{
    public static class Figures
    {

        public static Mesh Octahedron(double sideLength)
        {
            var vertices = new Vector[6];
            var normals = new Vector[6];
            var indices = new int[8][];

            var halfSideLength = sideLength / 2;
            var topVertex = new Vector(0, halfSideLength, 0);
            var bottomVertex = new Vector(0, -halfSideLength, 0);

            // Вершины октаэдра
            vertices[0] = new Vector(halfSideLength, 0, 0);
            vertices[1] = new Vector(-halfSideLength, 0, 0);
            vertices[2] = new Vector(0, 0, halfSideLength);
            vertices[3] = new Vector(0, 0, -halfSideLength);
            vertices[4] = topVertex;
            vertices[5] = bottomVertex;

            // Нормали
            normals[0] = (vertices[0] - topVertex).Normalize();
            normals[1] = (vertices[1] - topVertex).Normalize();
            normals[2] = (vertices[2] - topVertex).Normalize();
            normals[3] = (vertices[3] - topVertex).Normalize();
            normals[4] = (vertices[4] - bottomVertex).Normalize();
            normals[5] = (vertices[5] - bottomVertex).Normalize();

            // Индексы граней октаэдра
            indices[0] = new int[] { 0, 2, 4 }; // Грань 1
            indices[1] = new int[] { 2, 1, 4 }; // Грань 2
            indices[2] = new int[] { 1, 3, 4 }; // Грань 3
            indices[3] = new int[] { 3, 0, 4 }; // Грань 4
            indices[4] = new int[] { 2, 0, 5 }; // Грань 5
            indices[5] = new int[] { 1, 2, 5 }; // Грань 6
            indices[6] = new int[] { 3, 1, 5 }; // Грань 7
            indices[7] = new int[] { 0, 3, 5 }; // Грань 8

            return new MeshWithNormals(vertices, normals, indices);
        }

        public static Mesh Cube(double sideLength)
        {
            var halfSideLength = sideLength / 2;
            var vertices = new Vector[8];
            var normals = new Vector[8];
            var indices = new int[6][];

            // Вершины куба
            vertices[0] = new Vector(-halfSideLength, -halfSideLength, -halfSideLength);
            vertices[1] = new Vector(halfSideLength, -halfSideLength, -halfSideLength);
            vertices[2] = new Vector(halfSideLength, halfSideLength, -halfSideLength);
            vertices[3] = new Vector(-halfSideLength, halfSideLength, -halfSideLength);
            vertices[4] = new Vector(-halfSideLength, -halfSideLength, halfSideLength);
            vertices[5] = new Vector(halfSideLength, -halfSideLength, halfSideLength);
            vertices[6] = new Vector(halfSideLength, halfSideLength, halfSideLength);
            vertices[7] = new Vector(-halfSideLength, halfSideLength, halfSideLength);

            // Нормали
            normals[0] = new Vector(-1, -1, -1).Normalize();
            normals[1] = new Vector(1, -1, -1).Normalize();
            normals[2] = new Vector(1, 1, -1).Normalize();
            normals[3] = new Vector(-1, 1, -1).Normalize();
            normals[4] = new Vector(-1, -1, 1).Normalize();
            normals[5] = new Vector(1, -1, 1).Normalize();
            normals[6] = new Vector(1, 1, 1).Normalize();
            normals[7] = new Vector(-1, 1, 1).Normalize();

            // Индексы граней куба
            indices[0] = new int[] { 0, 1, 2, 3 }; // Грань 1
            indices[1] = new int[] { 1, 5, 6, 2 }; // Грань 2
            indices[2] = new int[] { 5, 4, 7, 6 }; // Грань 3
            indices[3] = new int[] { 4, 0, 3, 7 }; // Грань 4
            indices[4] = new int[] { 3, 2, 6, 7 }; // Грань 5
            indices[5] = new int[] { 4, 5, 1, 0 }; // Грань 6

            return new MeshWithNormals(vertices, normals, indices);
        }
        public static Mesh Sphere(double diameter, int slices, int stacks)
        {
            var radius = diameter / 2;
            var vertices = new Vector[slices * stacks];
            var normals = new Vector[slices * stacks];
            var indices = new int[slices * (stacks - 1)][];
            for (int stack = 0; stack < stacks; ++stack)
                for (int slice = 0; slice < slices; ++slice)
                {
                    var theta = Math.PI * stack / (stacks - 1.0);
                    var phi = 2 * Math.PI * (slice / (slices - 1.0));
                    vertices[stack * slices + slice] = new Vector(
                        radius * Math.Sin(theta) * Math.Cos(phi),
                        radius * Math.Sin(theta) * Math.Sin(phi),
                        radius * Math.Cos(theta));
                    normals[stack * slices + slice] = vertices[stack * slices + slice].Normalize();
                }
            for (int stack = 0; stack < stacks - 1; ++stack)
                for (int slice = 0; slice < slices; ++slice)
                    indices[stack * slices + slice] = new int[4] {
                        stack * slices + ((slice + 1) % slices),
                        (stack + 1) * slices + ((slice + 1) % slices),
                        (stack + 1) * slices + slice,
                        stack * slices + slice,};
            return new MeshWithNormals(vertices, normals, indices);
        }
        public static Mesh Tetrahedron(double sideLength)
        {
            var height = Math.Sqrt(6) / 3 * sideLength;
            var vertices = new Vector[]
            {
        new Vector(0, 0, 0),
        new Vector(sideLength, 0, 0),
        new Vector(sideLength / 2, height, 0),
        new Vector(sideLength / 2, height / 3, Math.Sqrt(2.0 / 3.0) * height)
            };
            var normals = new Vector[]
            {
        new Vector(0, 0, -1),
        new Vector(0, 0, -1),
        new Vector(0, 0, -1),
        new Vector(0, 0, -1)
            };
            var indices = new int[][]
            {
        new int[]{ 0, 2, 1 },
        new int[]{ 0, 1, 3 },
        new int[]{ 1, 2, 3 },
        new int[]{ 2, 0, 3 }
            };
            return new MeshWithNormals(vertices, normals, indices);
        }
        public static Mesh CubeTexture(double size)
        {
            double s = size / 2;
            return new MeshWithTexture(Resources.Texture3,
                new Vector[24]
                {
                    new Vector(-s, -s, s),
                    new Vector(s, -s, s),
                    new Vector(s, s, s),
                    new Vector(-s, s, s),

                    new Vector(s, -s, s),
                    new Vector(s, -s, -s),
                    new Vector(s, s, -s),
                    new Vector(s, s, s),

                    new Vector(s, -s, -s),
                    new Vector(-s, -s, -s),
                    new Vector(-s, s, -s),
                    new Vector(s, s, -s),

                    new Vector(-s, -s, -s),
                    new Vector(-s, -s, s),
                    new Vector(-s, s, s),
                    new Vector(-s, s, -s),

                    new Vector(-s, -s, -s),
                    new Vector(s, -s, -s),
                    new Vector(s, -s, s),
                    new Vector(-s, -s, s),

                    new Vector(-s, s, s),
                    new Vector(s, s, s),
                    new Vector(s, s, -s),
                    new Vector(-s, s, -s),
                },
                new Vector[24]
                {
                    new Vector(0, 0),
                    new Vector(1, 0),
                    new Vector(1, 1),
                    new Vector(0, 1),

                    new Vector(0, 0),
                    new Vector(1, 0),
                    new Vector(1, 1),
                    new Vector(0, 1),

                    new Vector(0, 0),
                    new Vector(1, 0),
                    new Vector(1, 1),
                    new Vector(0, 1),

                    new Vector(0, 0),
                    new Vector(1, 0),
                    new Vector(1, 1),
                    new Vector(0, 1),

                    new Vector(0, 0),
                    new Vector(1, 0),
                    new Vector(1, 1),
                    new Vector(0, 1),

                    new Vector(0, 0),
                    new Vector(1, 0),
                    new Vector(1, 1),
                    new Vector(0, 1),
                },
                new int[6][]
                {
                   new int[4] { 0, 3, 2, 1 },
                   new int[4] { 4, 7, 6, 5 },
                   new int[4] { 8, 11, 10, 9 },
                   new int[4] { 12, 15, 14, 13 },
                   new int[4] { 16, 19, 18, 17 },
                   new int[4] { 20, 23, 22, 21 },
                });
        }
    }

}