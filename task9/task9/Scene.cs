using System.Drawing;
using System.Windows.Forms;
using System;
using System.Numerics;
using task9;

namespace task9
{
    public class Scene : Control
    {
        public Camera Camera { get; set; }
        public Mesh Drawable { get; set; }

        public View3D View3D { get; private set; }

        public Scene() : base()
        {
            var flags = ControlStyles.AllPaintingInWmPaint
                      | ControlStyles.DoubleBuffer
                      | ControlStyles.UserPaint;
            SetStyle(flags, true);
            ResizeRedraw = true;
            //View3D = new View3D(this);
            View3D = new View3D(this);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            View3D.Resize();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (null == Camera) return;
            var zero = new Vector(0, 0, 0);
            var x = new Vector(0.8, 0, 0);
            var y = new Vector(0, 0.8, 0);
            var z = new Vector(0, 0, 0.8);
            View3D.StartDrawing();
            View3D.DrawLine(
                new Vertex(zero, Color.Red),
                new Vertex(x, Color.Red));
            View3D.DrawPoint(new Vertex(x, Color.Red));
            View3D.DrawLine(
                new Vertex(zero, Color.Green),
                new Vertex(y, Color.Green));
            View3D.DrawPoint(new Vertex(y, Color.Green));
            View3D.DrawLine(
                new Vertex(zero, Color.Blue),
                new Vertex(z, Color.Blue));
            View3D.DrawPoint(new Vertex(z, Color.Blue));
            Drawable.Draw(View3D);
            e.Graphics.DrawImage(View3D.FinishDrawing(), 0, 0);
        }
    }
}