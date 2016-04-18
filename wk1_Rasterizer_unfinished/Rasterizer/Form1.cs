﻿using System;
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
            //Kubus();
            //Kegel();
            //Cilinder();
            Bol();
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



            Matrix perspective = Matrix.perspective((float)Math.PI/2, (float)Width/Height,.1f,50);
            Matrix translation = Matrix.translate(new Vector3(0, 0, -5f));
            Matrix rotationMatrix = Matrix.rotation(rotation, new Vector3(1f, 0, 0));
            float schermPositieX = 0, schermPositieY = 0;

            foreach (Vector3 v in vertices)
            {
                Vector3 v1 = perspective * translation* rotationMatrix * v;
//                Console.WriteLine($"Width: {Width},Height: {Height}");
                schermPositieX = Width / 2 + v1.x / v1.z * Width / 2;
                schermPositieY = Height / 2 + v1.y / v1.z * Height / 2;

                g.DrawRectangle(pen, (int)schermPositieX-5, (int)schermPositieY-5, 10, 10);
            }

            foreach(var pl in polygons)
            {
                Point lastPoint = new Point(0, 0);
                for(int i = 0; i <= pl.Count; i++)
                {
                    Vector3 p = perspective * translation * rotationMatrix * vertices[pl[i % pl.Count]];
                    Point screenP = new Point((int)(Width / 2 + p.x / p.z * Width / 2),
                                              (int)(Height / 2 + p.y / p.z * Height / 2));
                    if (i == 0)
                    {
                        lastPoint = screenP;
                        continue;
                    }

                    g.DrawLine(pen, screenP, lastPoint);
                    lastPoint = screenP;


                }
            }
        }




        private void Kubus()
        {
            vertices.Add(new Vector3(-1.0f, -1.0f, -1.0f));
            vertices.Add(new Vector3(1.0f, -1.0f, -1.0f));
            vertices.Add(new Vector3(1.0f, 1.0f, -1.0f));
            vertices.Add(new Vector3(-1.0f, 1.0f, -1.0f));

            vertices.Add(new Vector3(-1.0f, -1.0f, 1.0f));
            vertices.Add(new Vector3(1.0f, -1.0f, 1.0f));
            vertices.Add(new Vector3(1.0f, 1.0f, 1.0f));
            vertices.Add(new Vector3(-1.0f, 1.0f, 1.0f));

            polygons.Add(new List<int> { 0, 1, 2, 3 });
            polygons.Add(new List<int> { 4, 5, 6, 7 });
            polygons.Add(new List<int> { 0, 1, 5, 4 });
            polygons.Add(new List<int> { 2, 3, 7, 6 });
        }

        private void Kegel()
        {
            //onderkant
            vertices.Add(new Vector3(1.0f, -1.0f, 1.0f));
            vertices.Add(new Vector3(1.5f, -1.0f, 0f));
            vertices.Add(new Vector3(1.0f, -1.0f, -1.0f));
            vertices.Add(new Vector3(0f, -1.0f, -1.5f));
            vertices.Add(new Vector3(-1.0f, -1.0f, -1.0f));
            vertices.Add(new Vector3(-1.5f, -1.0f, 0f));
            vertices.Add(new Vector3(-1.0f, -1.0f, 1.0f));
            vertices.Add(new Vector3(0f, -1.0f, 1.5f));

            //bovenste vertix
            vertices.Add(new Vector3(0f, 2f, 0f));

            //lijntjes vormen driehoeken
            polygons.Add(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 });
            polygons.Add(new List<int> { 0, 4, 8 });
            polygons.Add(new List<int> { 1, 5, 8 });
            polygons.Add(new List<int> { 2, 6, 8 });
            polygons.Add(new List<int> { 3, 7, 8 });
        }

        private void Cilinder()
        {
            //bovenste rondje
            vertices.Add(new Vector3(1.0f, 2f, 1.0f));
            vertices.Add(new Vector3(1.5f, 2f, 0f));
            vertices.Add(new Vector3(1.0f, 2f, -1.0f));
            vertices.Add(new Vector3(0f, 2f, -1.5f));
            vertices.Add(new Vector3(-1.0f, 2f, -1.0f));
            vertices.Add(new Vector3(-1.5f, 2f, 0f));
            vertices.Add(new Vector3(-1.0f, 2f, 1.0f));
            vertices.Add(new Vector3(0f, 2f, 1.5f));
            
            polygons.Add(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 });

            //onderste rondje
            vertices.Add(new Vector3(1.0f, -1.0f, 1.0f));
            vertices.Add(new Vector3(1.5f, -1.0f, 0f));
            vertices.Add(new Vector3(1.0f, -1.0f, -1.0f));
            vertices.Add(new Vector3(0f, -1.0f, -1.5f));
            vertices.Add(new Vector3(-1.0f, -1.0f, -1.0f));
            vertices.Add(new Vector3(-1.5f, -1.0f, 0f));
            vertices.Add(new Vector3(-1.0f, -1.0f, 1.0f));
            vertices.Add(new Vector3(0f, -1.0f, 1.5f));
            polygons.Add(new List<int> { 8, 9, 10, 11, 12, 13, 14, 15 });

            //lijnen tussen onder en boven     //dit is voor de streepjes in het midden, lijstjes vormen vierkanten
            polygons.Add(new List<int> { 0, 8, 12, 4 });
            polygons.Add(new List<int> { 1, 9, 13, 5 });
            polygons.Add(new List<int> { 2, 10, 14, 6 });
            polygons.Add(new List<int> { 3, 11, 15, 7 });
        }

        private void Bol()
        {
            //bovenste rondje
            vertices.Add(new Vector3(1.0f, 2f, 1.0f));
            vertices.Add(new Vector3(1.5f, 2f, 0f));
            vertices.Add(new Vector3(1.0f, 2f, -1.0f));
            vertices.Add(new Vector3(0f, 2f, -1.5f));
            vertices.Add(new Vector3(-1.0f, 2f, -1.0f));
            vertices.Add(new Vector3(-1.5f, 2f, 0f));
            vertices.Add(new Vector3(-1.0f, 2f, 1.0f));
            vertices.Add(new Vector3(0f, 2f, 1.5f));

            polygons.Add(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 });

            //onderste rondje
            vertices.Add(new Vector3(1.0f, -1.0f, 1.0f));
            vertices.Add(new Vector3(1.5f, -1.0f, 0f));
            vertices.Add(new Vector3(1.0f, -1.0f, -1.0f));
            vertices.Add(new Vector3(0f, -1.0f, -1.5f));
            vertices.Add(new Vector3(-1.0f, -1.0f, -1.0f));
            vertices.Add(new Vector3(-1.5f, -1.0f, 0f));
            vertices.Add(new Vector3(-1.0f, -1.0f, 1.0f));
            vertices.Add(new Vector3(0f, -1.0f, 1.5f));
            polygons.Add(new List<int> { 8, 9, 10, 11, 12, 13, 14, 15 });

            //topje
            vertices.Add(new Vector3(0f, 3f, 0f));

            //onderkant
            vertices.Add(new Vector3(0f, -3f, 0f));
            polygons.Add(new List<int> { 0, 16, 4, 12, 17, 8 });


        }

    }


}
