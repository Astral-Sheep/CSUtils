using System;
using System.Collections.Generic;

namespace Com.Surbon.CSUtils.Math
{
	/// <summary>
	/// Representation of a quaternion as a + bi + cj + dk
	/// </summary>
	public struct Quaternion
	{
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

		#endregion OPERATORS

		#region INSTANCE

		public Quaternion GetConjugate() => new Quaternion(a, -b, -c, -d);

		#endregion INSTANCE
	}
}
