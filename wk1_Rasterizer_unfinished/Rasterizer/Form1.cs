using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rasterizer
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			this.DoubleBuffered = true;
		}
		List<Vector3> vertices = new List<Vector3>();
		List<List<int>> polygons = new List<List<int>>();
		float rotation = 0;


		private void Form1_Load(object sender, EventArgs e)
		{
            //initialise vertices & polygons here
            vertices.Add(new Vector3(-1f, -1f, -1f));
            vertices.Add(new Vector3( 1f, -1f, -1f));
            vertices.Add(new Vector3( 1f,  1f, -1f));
            vertices.Add(new Vector3(-1f,  1f, -1f));
            //vertices.Add(new Vector3() { x = -1f, y = -1f, z = -1f });
            //vertices.Add(new Vector3() { x =  1f, y = -1f, z = -1f });
            //vertices.Add(new Vector3() { x =  1f, y =  1f, z = -1f });
            //vertices.Add(new Vector3() { x = -1f, y =  1f, z = -1f });

            vertices.Add(new Vector3(-1f, -1f, 1f));
            vertices.Add(new Vector3( 1f, -1f, 1f));
            vertices.Add(new Vector3( 1f,  1f, 1f));
            vertices.Add(new Vector3(-1f,  1f, 1f));
            //vertices.Add(new Vector3() { x = -1f, y = -1f, z = 1f });
            //vertices.Add(new Vector3() { x =  1f, y = -1f, z = 1f });
            //vertices.Add(new Vector3() { x =  1f, y =  1f, z = 1f });
            //vertices.Add(new Vector3() { x = -1f, y =  1f, z = 1f });

            polygons.Add(new List<int> { 1,2,3,4});
        }

		private void timer1_Tick(object sender, EventArgs e)
		{
            rotation += 0.01f;
			Invalidate();
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			var g = e.Graphics;
			g.Clear(Color.LightGray);
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			Pen pen = new Pen(Color.Black, 2);

            

            Matrix perspective = Matrix.perspective((float)Math.PI/2, 1.0f ,.1f,10f);
            Matrix translation = Matrix.translate(new Vector3(0, 0, 0));
            Matrix rotationMatrix = Matrix.rotation(rotation, new Vector3(0, 1, 0));
            float schermPositieX = 0, schermPositieY = 0;

            foreach (Vector3 v in vertices)
            {
                Vector3 v1 = perspective * translation* rotationMatrix * v;
//                Console.WriteLine($"Width: {Width},Height: {Height}");
                schermPositieX = Width / 2 + v1.x / v1.z * Width / 2;
                schermPositieY = Height / 2 + v1.y / v1.z * Height / 2;

                g.DrawRectangle(pen, (int)schermPositieX-5, (int)schermPositieY-5, 10, 10);
            }

			//g.DrawRectangle(pen, 50, 50, 10, 10);
		}





	}
}
