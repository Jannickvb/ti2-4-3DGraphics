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

        public float x { get; }
        public float y { get; }
        public float z { get; }
        public float rotation { get; set; }

        private Matrix translation = Matrix.identity();

        public Object3D(float x, float y, float z)
        {
            vertices = new List<Vector3>();
            polygons = new List<List<int>>();
        }

        public void translate(float x, float y, float z)
        {
            translation = Matrix.translate(new Vector3(x, y, z));
        }

        public virtual void Init()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Render(Graphics g)
        {

        }
    }
}
