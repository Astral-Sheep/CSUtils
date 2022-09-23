using System;
using System.Collections.Generic;

namespace Com.Surbon.CSUtils.Math
{
	#pragma warning disable CS0661 // Le type définit l'opérateur == ou l'opérateur != mais ne se substitue pas à Object.GetHashCode()
	/// <summary>
	/// Representation of a quaternion as a + bi + cj + dk
	/// </summary>
	public struct Quaternion
	{
		public Vector3 Vector
		{
			get => new Vector3(b, c, d);
			set
			{
				b = value.x;
				c = value.y;
				d = value.z;
			}
		}

		public float a;
		public float b;
		public float c;
		public float d;

		public Quaternion(float a, float b, float c, float d)
		{
			this.a = a;
			this.b = b;
			this.c = c;
			this.d = d;
		}

		#region OPERATORS

		public static Quaternion operator+(Quaternion q1, Quaternion q2)
		{
			return new Quaternion(q1.a + q2.a, q1.b + q2.b, q1.c + q2.c, q1.d + q2.d);
		}

		public static Quaternion operator-(Quaternion q1, Quaternion q2)
		{
			return new Quaternion(q1.a - q2.a, q1.b - q2.b, q1.c - q2.c, q1.d - q2.d);
		}

		public static Quaternion operator*(Quaternion q1, Quaternion q2)
		{
			return new Quaternion(
				q1.a * q2.a - q1.b * q2.b - q1.c * q2.c - q1.d * q2.d,
				q1.a * q2.b + q1.b * q2.a + q1.c * q2.d - q1.d * q2.c,
				q1.a * q2.c + q1.c * q2.a + q1.d * q2.b - q1.b * q2.d,
				q1.a * q2.d + q1.d * q2.a + q1.b * q2.c - q1.c * q2.b
				);
		}

		public static Quaternion operator*(Quaternion q, float lambda)
		{
			return new Quaternion(q.a * lambda, q.b * lambda, q.c * lambda, q.d * lambda);
		}

		public static Quaternion operator*(float lambda, Quaternion q)
		{
			return new Quaternion(lambda * q.a, lambda * q.b, lambda * q.c, lambda * q.d);
		}

		public static Quaternion operator/(Quaternion q1, Quaternion q2)
		{
			return q1 * q2.GetInverse();
		}

		public static bool operator==(Quaternion q1, Quaternion q2)
		{
			return q1.a == q2.a && q1.b == q2.b && q1.c == q2.c && q1.d == q2.d;
		}

		public static bool operator!=(Quaternion q1, Quaternion q2)
		{
			return q1.a != q2.a || q1.b != q2.b || q1.c != q2.c || q1.d != q2.d;
		}

		#endregion OPERATORS

		#region INSTANCE

		public override bool Equals(object obj)
		{
			if (obj is Quaternion)
				return (Quaternion)obj == this;

			return false;
		}

		public Quaternion GetConjugate() => new Quaternion(a, -b, -c, -d);

		public Quaternion GetInverse()
		{
			return (1f / (a * a + b * b + c * c + d * d)) * new Quaternion(a, -b, -c, -d);
		}

		public float GetNorm()
		{
			return MathF.Sqrt(a * a + b * b + c * c + d * d);
		}

		public float GetNormSquared()
		{
			return a * a + b * b + c * c + d * d;
		}

		#endregion INSTANCE
	}
}
