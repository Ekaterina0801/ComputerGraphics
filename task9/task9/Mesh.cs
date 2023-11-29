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
                double x = double.Parse(infoPoint[1]);
                double y = double.Parse(infoPoint[2]);
                double z = double.Parse(infoPoint[3]);
                vertices.Add(new Vector(x, y, z));
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

        public virtual void Draw(View3D graphics)
        {
            foreach (var facet in Indices)
                for (int i = 0; i < facet.Length; ++i)
                {
                    var a = new Vertex(Coordinates[facet[i]]);
                    var b = new Vertex(Coordinates[facet[(i + 1) % facet.Length]]);
                    graphics.DrawLine(a, b);
                }

        }
        public virtual void Draw(Graphics3D graphics)
        {
            foreach (var facet in Indices)
                for (int i = 0; i < facet.Length; ++i)
                {
                    var a = new Vertex(Coordinates[facet[i]]);
                    var b = new Vertex(Coordinates[facet[(i + 1) % facet.Length]]);
                    graphics.DrawLine(a, b);
                }

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
        public Vector[] Normals { get; set; }
        public bool VisibleNormals { get; set; } = false;

        public MeshWithNormals(Vector[] vertices, Vector[] normals, int[][] indices) : base(vertices, indices)
        {
            Normals = normals;
        }

        public override void Apply(Matrix transformation)
        {
            var normalTransformation = transformation.Inverse().Transpose();
            for (int i = 0; i < Coordinates.Length; ++i)
            {
                Coordinates[i] *= transformation;
                Normals[i] = (Normals[i] * normalTransformation).Normalize();
            }
        }

        public override void Draw(View3D graphics)
        {
            foreach (var verge in Indices)
            {
                for (int i = 1; i < verge.Length - 1; ++i)
                {
                    var a = new Vertex(Coordinates[verge[0]], Color.White, Normals[verge[0]]);
                    var b = new Vertex(Coordinates[verge[i]], Color.White, Normals[verge[i]]);
                    var c = new Vertex(Coordinates[verge[i + 1]], Color.White, Normals[verge[i + 1]]);
                    graphics.DrawTriangle(a, b, c);
                }
                if (VisibleNormals)
                    for (int i = 0; i < Coordinates.Length; ++i)
                    {
                        var a = new Vertex(Coordinates[i], Color.White, Normals[i]);
                        var b = new Vertex(Coordinates[i] + Normals[i], Color.White, Normals[i]);
                        graphics.DrawLine(a, b);
                    }
            }
        }
    }
}