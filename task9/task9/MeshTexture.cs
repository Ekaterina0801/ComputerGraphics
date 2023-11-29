using System.Drawing;
using task9;

namespace Texturing
{
    public class MeshWithTexture : Mesh
    {
        private Bitmap texture;
        private Vector[] uvCoordinates;

        public MeshWithTexture(Bitmap texture, Vector[] vertices, Vector[] textureCoordinates, int[][] indices) : base(vertices, indices)
        {
            this.texture = texture;
            this.uvCoordinates = textureCoordinates;
        }

        public override void Draw(View3D graphics)
        {
            var curTexture = graphics.ActiveTexture;
            graphics.ActiveTexture = texture;

            foreach (var facet in Indices)
                for (int i = 1; i < facet.Length - 1; ++i)
                    graphics.DrawTriangle(new Vertex(Coordinates[facet[0]], uvCoordinates[facet[0]]), 
                    new Vertex(Coordinates[facet[i]], uvCoordinates[facet[i]]), new Vertex(Coordinates[facet[i + 1]], 
                    uvCoordinates[facet[i + 1]]));
        }
    }
}