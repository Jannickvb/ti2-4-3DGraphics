using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasterizer.assets.models
{
    abstract class Object3D
    {

        public List<Vector3> vertices { get; }
        public List<List<int>> polygons { get; }
        public Matrix translation { get; }

        public Object3D()
        {

        }

        public void Render(Graphics g)
        {

        }
    }
}
