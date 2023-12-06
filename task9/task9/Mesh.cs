﻿using System.Drawing;
using System.Numerics;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace task9
{
    public class Mesh
    {
        public Vector[] Coordinates { get; set; }
        public int[][] Indices { get; set; }

        public bool Solid { get; set; } = false;

        public virtual Vector Center
        { 
            get
            {
                Vector center = new Vector();
                foreach (var v in Coordinates)
                {
                    center.X += v.X;
                    center.Y += v.Y;
                    center.Z += v.Z;
                }
                center.X /= Coordinates.Length;
                center.Y /= Coordinates.Length;
                center.Z /= Coordinates.Length;
                return center;
            }
        }
        public virtual void Draw(View3D graphics)
        {
            foreach (var ind in Indices)
                for (int i = 0; i < ind.Length; ++i)
                    graphics.DrawLine(new Vertex(Coordinates[ind[i]]),
                        new Vertex(Coordinates[ind[(i + 1) % ind.Length]]));

        }


        public Mesh(Vector[] vertices, int[][] indices)
        {
            Coordinates = vertices;
            Indices = indices;
        }

        public Mesh(string path)
        {
            var vertices = new List<Vector>();
            var indices = new List<List<int>>();
            var info = File.ReadAllLines(path);
            int index = 0;
            while (info[index].Equals("") || !info[index][0].Equals('v'))
                index++;
            while (info[index].Equals("") || info[index][0].Equals('v'))
            {
                var infoPoint = info[index].Split(' ');
                vertices.Add(new Vector(double.Parse(infoPoint[1]), double.Parse(infoPoint[2]),
                    double.Parse(infoPoint[3])));
                index++;
            }
            while (info[index].Equals("") || !info[index][0].Equals('f'))
                index++;
            int indexPointSeq = 0;
            while (info[index].Equals("") || info[index][0].Equals('f'))
            {
                var infoPointSeq = info[index].Split(' ');
                var listPoints = new List<int>();
                for (int i = 1; i < infoPointSeq.Length; ++i)
                {
                    int elem;
                    if (int.TryParse(infoPointSeq[i], out elem))
                        listPoints.Add(elem - 1);
                }
                indices.Add(listPoints);
                index++;
                indexPointSeq++;
            }
            Coordinates = vertices.ToArray();
            Indices = indices.Select(x => x.ToArray()).ToArray();
        }

        public virtual void Apply(Matrix transformation)
        {
            for (int i = 0; i < Coordinates.Length; ++i)
                Coordinates[i] *= transformation;
        }

        protected static Color NextColor(Random r)
        {
            return Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));
        }


        public void Save(string path)
        {
            string info = "# File Created: " + DateTime.Now.ToString() + "\r\n";
            foreach (var v in Coordinates)
                info += "v " + v.X + " " + v.Y + " " + v.Z + "\r\n";
            info += "# " + Coordinates.Length + " vertices\r\n";
            foreach (var facet in Indices)
            {
                info += "f ";
                for (int i = 0; i < facet.Length; ++i)
                    info += (facet[i] + 1) + " ";
                info += "\r\n";
            }
            info += "# " + Indices.Length + " polygons\r\n";
            File.WriteAllText(path, info);
        }
    }
    public class MeshWithNormals : Mesh
    {
        public Vector[] VectorNormal { get; set; }
        public bool IsNormalVisible { get; set; } = false;

        public MeshWithNormals(Vector[] vertices, Vector[] normals, int[][] indices) : base(vertices, indices)
        {
            VectorNormal = normals;
        }

        public override void Apply(Matrix transformation)
        {
            var normalTransformation = transformation.Inverse().Transpose();
            for (int i = 0; i < Coordinates.Length; ++i)
            {
                Coordinates[i] *= transformation;
                VectorNormal[i] = (VectorNormal[i] * normalTransformation).Normalize();
            }
        }

        public override void Draw(View3D graphics)
        {
            foreach (var v in Indices)
            {
                for (int i = 1; i < v.Length - 1; ++i)
                {     
                    graphics.DrawTriangle(new Vertex(Coordinates[v[0]], 
                        Color.White, VectorNormal[v[0]]), new Vertex(Coordinates[v[i]], Color.White, VectorNormal[v[i]]),
                        new Vertex(Coordinates[v[i + 1]], Color.White, VectorNormal[v[i + 1]]));
                }
            }
        }
    }
}