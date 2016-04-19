using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasterizer.assets.models
{
    class Car : Object3D
    {
        public Car(float x, float y, float z) : base(x, y, z)
        {

        }

        public override void Init()
        {
            base.Init();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Render(Graphics g){
            base.Render(g);
        }
    }
}
