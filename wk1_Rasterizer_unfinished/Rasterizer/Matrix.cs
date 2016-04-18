using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasterizer
{
	struct Matrix
	{
		float[,] data;

		private Matrix(int size = 4)
		{
			data = new float[size, size];
		}


		public static Matrix identity()
		{
			Matrix m = new Matrix(4);
			m.data[0,0] = 1;
			m.data[1,1] = 1;
			m.data[2,2] = 1;
			m.data[3,3] = 1;
			return m;
		}

		public static Matrix perspective(float fov, float aspect, float zNear, float zFar)
		{
            Matrix perspective = identity();

            perspective.data[0, 0] = (float)(Math.Tan(fov / 2) / aspect);
            perspective.data[1, 1] = (float)(Math.Tan(fov / 2));
            perspective.data[2, 2] = zFar / (zFar - zNear);
            perspective.data[2, 3] = (zNear*zFar) * (zFar-zNear);
            perspective.data[3, 2] = -1;

            return perspective;
		}

		public static Matrix rotation(float angle, Vector3 axis)
		{
            Matrix rotation = identity();
            float c, s;
            c = (float)Math.Cos(angle);
            s = (float)Math.Sin(angle);

            rotation.data[0, 0] = (float)(Math.Pow(axis.x,2)*(1-c)+c);
            rotation.data[0, 1] = (axis.x*axis.y*(1-c)-axis.z*s);
            rotation.data[0, 2] = (axis.x*axis.z*(1-c)+axis.y*s);

            rotation.data[1, 0] = (axis.x*axis.y*(1-c)+axis.z*s);
            rotation.data[1, 1] = (float)(Math.Pow(axis.y,2)*(1-c)+c);
            rotation.data[1, 2] = (axis.y*axis.z*(1-c)-axis.x*s);

            rotation.data[2, 0] = (axis.x*axis.z*(1-c)-axis.y*s);
            rotation.data[2, 1] = (axis.y*axis.z*(1-c)+axis.x*s);
            rotation.data[2, 2] = (float)(Math.Pow(axis.z,2)*(1-c)+c);

            return rotation;
		}
		public static Matrix translate(Vector3 offset)
		{
            Matrix translation = identity();

            translation.data[0, 3] = offset.x;
            translation.data[1, 3] = offset.y;
            translation.data[2, 3] = offset.z;

            return translation;
		}



        public static Vector3 operator *(Matrix mat, Vector3 vec)
        {
            float x, y, z;
            x = (mat.data[0, 0] * vec.x) + (mat.data[0, 1] * vec.y) + (mat.data[0, 2] * vec.z) + mat.data[0, 3];
            y = (mat.data[1, 0] * vec.x) + (mat.data[1, 1] * vec.y) + (mat.data[1, 2] * vec.z) + mat.data[1, 3];
            z = (mat.data[2, 0] * vec.x) + (mat.data[2, 1] * vec.y) + (mat.data[2, 2] * vec.z) + mat.data[2, 3];

            return new Vector3( x, y, z );
        }
		public static Matrix operator * (Matrix mat1, Matrix mat2)
		{
            Matrix result = Matrix.identity();
            result.data = new float[mat1.data.GetLength(0), mat2.data.GetLength(1)];
            for (int i = 0; i < result.data.GetLength(0); i++)
            {
                for (int j = 0; j < result.data.GetLength(1); j++)
                {
                    result.data[i, j] = 0;
                    for (int k = 0; k < mat1.data.GetLength(1); k++)
                        result.data[i, j] = result.data[i, j] + mat1.data[i, k] * mat2.data[k, j];
                }
            }
            return result;
        }
	}
}
