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

        float rotationY = 0, rotationX = 0;
        float rotationDelta = 0.05f;

        List<Vector3> dinges = new List<Vector3>();
        bool showVerticals = true;

        private void Form1_Load(object sender, EventArgs e)
        {
            //Kubus();
            //Kegel();
            Cilinder();
            //Bol();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.LightGray);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen pen = new Pen(Color.Black, 2);



            Matrix perspective = Matrix.perspective((float)Math.PI / 2, (float)Width / Height, .1f, 50);

            Matrix translation = Matrix.translate(new Vector3(0, 0, -5.0f));
            Matrix rotationMatrixX = Matrix.rotation(rotationY, new Vector3(0, 1, 0));
            Matrix rotationMatrixY = Matrix.rotation(rotationX, new Vector3(1, 0, 0));
            Matrix rotationMatrix = rotationMatrixX * rotationMatrixY;

            float schermPositieX = 0, schermPositieY = 0;

            if (showVerticals)
            {
                foreach (Vector3 v in vertices)
                {
                    Vector3 v1 = perspective * translation * rotationMatrix * v;
                    //                Console.WriteLine($"Width: {Width},Height: {Height}");
                    schermPositieX = Width / 2 + v1.x / v1.z * Width / 2;
                    schermPositieY = Height / 2 + v1.y / v1.z * Height / 2;

                    g.DrawRectangle(pen, (int)schermPositieX - 5, (int)schermPositieY - 5, 10, 10);
                }

                foreach (Vector3 v in dinges)
                {
                    Vector3 v1 = perspective * translation * rotationMatrix * v;
                    //                Console.WriteLine($"Width: {Width},Height: {Height}");
                    schermPositieX = Width / 2 + v1.x / v1.z * Width / 2;
                    schermPositieY = Height / 2 + v1.y / v1.z * Height / 2;

                    g.DrawRectangle(pen, (int)schermPositieX - 5, (int)schermPositieY - 5, 10, 10);
                }

            }

            foreach (var pl in polygons)
            {
                Point lastPoint = new Point(0, 0);
                for (int i = 0; i <= pl.Count; i++)
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

            /*
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
            vertices.Add(new Vector3(0f, -2f, 0f));
            polygons.Add(new List<int> { 1, 16, 5, 13, 17, 9 });
            
    */
            //midden
            vertices.Add(new Vector3(1.0f, 0f, 1.0f));
            vertices.Add(new Vector3(1.5f, 0f, 0f));
            vertices.Add(new Vector3(1.0f, 0f, -1.0f));
            vertices.Add(new Vector3(0f, 0f, -1.5f));
            vertices.Add(new Vector3(-1.0f, 0f, -1.0f));
            vertices.Add(new Vector3(-1.5f, 0f, 0f));
            vertices.Add(new Vector3(-1.0f, 0f, 1.0f));
            vertices.Add(new Vector3(0f, 0f, 1.5f));


            Vector3 tr = new Vector3(0, 1f, 0);
            Vector3 tr2 = new Vector3(0, -1f, 0);

            Matrix translatie = Matrix.translate(tr);
            Matrix translatie2 = Matrix.translate(tr2);
            Matrix rotatie = Matrix.rotation((float)Math.PI / 4, new Vector3(0, 0, 0));
            foreach (Vector3 vec in vertices)
            {
                //vertices 8 tm 15
                Vector3 v1 = translatie * rotatie * vec;
                dinges.Add(v1);
            }
            foreach (Vector3 vec in vertices)
            {
                //vertices 16 tm 23
                Vector3 v2 = translatie2 * rotatie * vec;
                dinges.Add(v2);
            }


            foreach (Vector3 vec in dinges)
            {
                vertices.Add(vec);
            }
            //24 en 25
            vertices.Add(new Vector3(0f, 1.5f, 0f));
            vertices.Add(new Vector3(0f, -1.5f, 0f));


            //boven - midden - onder
            polygons.Add(new List<int> { 8, 9, 10, 11, 12, 13, 14, 15 });
            polygons.Add(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 });
            polygons.Add(new List<int> { 16, 17, 18, 19, 20, 21, 22, 23 });

            polygons.Add(new List<int> { 24, 8, 0, 16, 25, 20, 4, 12 });
            polygons.Add(new List<int> { 24, 9, 1, 17, 25, 21, 5, 13 });
            polygons.Add(new List<int> { 24, 10, 2, 18, 25, 22, 6, 14 });
            polygons.Add(new List<int> { 24, 11, 3, 19, 25, 23, 7, 15 });


        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch(e.KeyChar)
            {
                case '1':
                    clearLists();
                    Kubus();
                    break;
                case '2':
                    clearLists();
                    Kegel();
                    break;
                case '3':
                    clearLists();
                    Cilinder();
                    break;
                case '4':
                    clearLists();
                    Bol();
                    break;
                case 'r':
                    showVerticals = !showVerticals;
                    break;
            }
        }

        private void clearLists()
        {
            vertices.Clear();
            polygons.Clear();
            dinges.Clear();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    break;
                case Keys.Down:
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    //Console.WriteLine("up down");
                    rotationX -= rotationDelta;
                    break;
                case Keys.S:
                case Keys.Down:
                    rotationX += rotationDelta;
                    break;
                case Keys.A:
                case Keys.Left:
                    rotationY += rotationDelta;
                    break;
                case Keys.D:
                case Keys.Right:
                    rotationY -= rotationDelta;
                    break;
            }
            //rotationY %= 1.6f;
        }
    }
}





