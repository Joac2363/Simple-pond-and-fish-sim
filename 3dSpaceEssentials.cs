using System;

namespace SpaceEssentials
{
	public class Vector
	{
		public double x;
		public double y;
		public double z;

		public Vector(double X, double Y, double Z) 
		{
			x = X; y = Y; z = Z;
		}
			
		public double Len()
		{
			return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
		}

		public Vector Normalize()
		{
			double len = Len();
			return new Vector(x, y, z) / len;
		}

		public static Vector operator +(Vector a, Vector b)
		{
			double x = a.x + b.x;
			double y = a.y + b.y;
			double z = a.z + b.z;	
			Vector vec = new Vector(x,y,z);
			
			return vec;
		}

		public static Vector operator -(Vector a, Vector b)
		{
			double x = a.x - b.x;
			double y = a.y - b.y;
			double z = a.z - b.z;
			Vector vec = new Vector(x, y, z);

			return vec;
		}

		public static Vector operator *(Vector a, double b)
		{
			double x = a.x * b;
			double y = a.y * b;
			double z = a.z * b;
			Vector vec = new Vector(x, y, z);

			return vec;
		}

        public static Vector operator /(Vector a, double b)
        {
            double x = a.x / b;
            double y = a.y / b;
            double z = a.z / b;
            Vector vec = new Vector(x, y, z);

            return vec;
        }
		public void print()
		{
			string str = string.Format("x,y,z : {0}, {1}, {2}", x, y, z);


			Console.WriteLine(str);
		}

    }

}