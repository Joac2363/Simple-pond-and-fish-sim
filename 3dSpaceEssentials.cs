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
        public static double operator *(Vector a, Vector b)
        {
			return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static Vector operator /(Vector a, double b)
        {
            double x = a.x / b;
            double y = a.y / b;
            double z = a.z / b;
            Vector vec = new Vector(x, y, z);

            return vec;
        }
        // The math in the following functions look wierd, but they work. Use radians.
        public void RotateX(double angle)
        {
			double oldY = y;
            y = Math.Cos(angle) * y + -Math.Sin(angle) * z;
            z = Math.Sin(angle) * oldY + Math.Cos(angle) * z;

        }
        public void RotateY(double angle)
        {
			double oldX = x;
			x = Math.Cos(angle) * x + Math.Sin(angle) * z;
            z = -Math.Sin(angle) * oldX + Math.Cos(angle) * z;
        }
        public void RotateZ(double angle)
        {
			double oldX = x;
            x = Math.Cos(angle) * x + -Math.Sin(angle) * y;
            y = Math.Sin(angle) * oldX + Math.Cos(angle) * y;
        }
        
		public double GetAngle(Vector other)
		{
			if (other == new Vector(0,0,0) || this == new Vector(0, 0, 0))
			{
				return 0;
			}
			return Math.Acos( (this * other) / (this.Len()  * other.Len()) );
		}

        public static bool operator ==(Vector a, Vector b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }
        public static bool operator !=(Vector a, Vector b)
        {
            return a.x != b.x || a.y != b.y || a.z != b.z;
        }

        public override string ToString() // Used for logging (WriteLine(Vector))
        {
            return $"x;y;z -> {x} ; {y} ; {z}";
        }


    }

}