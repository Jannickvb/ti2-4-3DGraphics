using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasterizer
{
	struct Vector3
	{
		public float[] data;
		public float x { get { return data[0]; } set { data[0] = value; } }
		public float y { get { return data[1]; } set { data[1] = value; } }
		public float z { get { return data[2]; } set { data[2] = value; } }

		public float length { get { return (float)Math.Sqrt(x * x + y * y + z * z); } }

		public Vector3(float x, float y, float z)
		{
			data = new float[3];
			this.x = x;
			this.y = y;
			this.z = z;
		}


		public Vector3 normalize()
		{
			throw new NotImplementedException();
		}

		public static Vector3 operator *(Vector3 vec, float f)
		{
			throw new NotImplementedException();
		}
		public static Vector3 operator *(float f, Vector3 vec)
		{
			throw new NotImplementedException();
		}
		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			throw new NotImplementedException();
		}
		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			throw new NotImplementedException();
		}

		public Vector3 cross(Vector3 other)
		{
			throw new NotImplementedException();
		}
		public float dot(Vector3 other)
		{
			throw new NotImplementedException();
		}

	}
}
