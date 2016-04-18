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
			throw new NotImplementedException();
		}

		public static Matrix rotation(float angle, Vector3 axis)
		{
			throw new NotImplementedException();
		}
		public static Matrix translate(Vector3 offset)
		{
			throw new NotImplementedException();
		}



		public static Vector3 operator * (Matrix mat, Vector3 vec)
		{
			throw new NotImplementedException();
		}
		public static Matrix operator * (Matrix mat1, Matrix mat2)
		{
			throw new NotImplementedException();
		}
	}
}
