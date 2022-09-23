using System;

namespace Com.Surbon.CSUtils.Math
{
	#pragma warning disable CS0661 // Le type définit l'opérateur == ou l'opérateur != mais ne se substitue pas à Object.GetHashCode()
	#pragma warning disable CS0659 // Le type se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
	#pragma warning disable CS1591 // Commentaire XML manquant pour le type ou le membre visible publiquement
	/// <summary>
	/// Representation of a quaternion as a + bi + cj + dk.
	/// </summary>
	public struct Quaternion
	{
		/// <summary>
		/// The vector component of the quaternion (b, c, d).
		/// </summary>
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

		/// <summary>
		/// The real part of the quaternion.
		/// </summary>
		public float a;
		/// <summary>
		/// The i imaginary part of the quaternion.
		/// </summary>
		public float b;
		/// <summary>
		/// The j imaginary part of the quaternion.
		/// </summary>
		public float c;
		/// <summary>
		/// The k imaginary part of the quaternion.
		/// </summary>
		public float d;

		/// <summary>
		/// Creates a quaternion as a + bi + cj + dk.
		/// </summary>
		/// <param name="a">The real part of the quaternion.</param>
		/// <param name="b">The i imaginary part of the quaternion.</param>
		/// <param name="c">The j imaginary part of the quaternion.</param>
		/// <param name="d">The k imaginary part of the quaternion.</param>
		public Quaternion(float a, float b, float c, float d)
		{
			this.a = a;
			this.b = b;
			this.c = c;
			this.d = d;
		}

		/// <summary>
		/// Creates a quaternion as a + bi + cj + dk.
		/// </summary>
		/// <param name="v">The vector component as (b, c, d).</param>
		public Quaternion(float a, Vector3 v)
		{
			this.a = a;
			b = v.x;
			c = v.y;
			d = v.z;
		}

		#region OPERATORS

		/// <summary>
		/// Sets the values of the quaternion to the opposite values.
		/// </summary>
		public static Quaternion operator-(Quaternion q)
		{
			return new Quaternion(-q.a, -q.b, -q.c, -q.d);
		}

		/// <summary>
		/// Adds the given quaternions.
		/// </summary>
		public static Quaternion operator+(Quaternion q1, Quaternion q2)
		{
			return new Quaternion(q1.a + q2.a, q1.b + q2.b, q1.c + q2.c, q1.d + q2.d);
		}

		/// <summary>
		/// Subtract the second quaternion to the first one.
		/// </summary>
		public static Quaternion operator-(Quaternion q1, Quaternion q2)
		{
			return new Quaternion(q1.a - q2.a, q1.b - q2.b, q1.c - q2.c, q1.d - q2.d);
		}

		/// <summary>
		/// Multiplies the given quaternions.
		/// </summary>
		public static Quaternion operator*(Quaternion q1, Quaternion q2)
		{
			return new Quaternion(
				q1.a * q2.a - q1.b * q2.b - q1.c * q2.c - q1.d * q2.d,
				q1.a * q2.b + q1.b * q2.a + q1.c * q2.d - q1.d * q2.c,
				q1.a * q2.c + q1.c * q2.a + q1.d * q2.b - q1.b * q2.d,
				q1.a * q2.d + q1.d * q2.a + q1.b * q2.c - q1.c * q2.b
				);
		}

		/// <summary>
		/// Multiplies the given quaternion with the given scalar.
		/// </summary>
		public static Quaternion operator*(Quaternion q, float lambda)
		{
			return new Quaternion(q.a * lambda, q.b * lambda, q.c * lambda, q.d * lambda);
		}

		/// <summary>
		/// Multiplies the given quaternion with the given scalar.
		/// </summary>
		public static Quaternion operator*(float lambda, Quaternion q)
		{
			return new Quaternion(lambda * q.a, lambda * q.b, lambda * q.c, lambda * q.d);
		}

		/// <summary>
		/// Multiplies the first quaternion by the inverse of the second quaternion.
		/// </summary>
		public static Quaternion operator/(Quaternion q1, Quaternion q2)
		{
			return q1 * q2.GetInverse();
		}

		/// <summary>
		/// Says if the given quaternions are equal.
		/// </summary>
		public static bool operator==(Quaternion q1, Quaternion q2)
		{
			return q1.a == q2.a && q1.b == q2.b && q1.c == q2.c && q1.d == q2.d;
		}

		/// <summary>
		/// Says if the given quaternions are different.
		/// </summary>
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

		/// <summary>
		/// Returns the conjugate of the quaternion (q*).
		/// </summary>
		public Quaternion GetConjugate() => new Quaternion(a, -b, -c, -d);

		/// <summary>
		/// Returns the inverse of the quaternion(q^-1).
		/// </summary>
		public Quaternion GetInverse()
		{
			return (1f / (a * a + b * b + c * c + d * d)) * new Quaternion(a, -b, -c, -d);
		}

		/// <summary>
		/// Returns the norm of the quaternion.
		/// </summary>
		public float GetNorm()
		{
			return MathF.Sqrt(a * a + b * b + c * c + d * d);
		}

		/// <summary>
		/// Returns the squared norm of the quaternion.
		/// </summary>
		public float GetNormSquared()
		{
			return a * a + b * b + c * c + d * d;
		}

		/// <summary>
		/// Rotates the quaternion on the given axis.
		/// </summary>
		public void Rotate(float angle, Vector3 axis)
		{
			float cos = MathF.Cos(angle);
			Vector3 sinAxis = MathF.Sin(angle) * axis;

			float a1 = -sinAxis.x * b - sinAxis.y * c - sinAxis.z * d;
			float b1 = cos * b + sinAxis.y * d - sinAxis.z * c;
			float c1 = cos * c + sinAxis.z * b - sinAxis.x * d;
			float d1 = cos * d + sinAxis.x * c - sinAxis.y * b;

			sinAxis *= -1;
			a = a1 * cos - b1 * sinAxis.x - c1 * sinAxis.y - d1 * sinAxis.z;
			b = a1 * sinAxis.x + b1 * cos + c1 * sinAxis.z - d1 * sinAxis.y;
			c = a1 * sinAxis.y + c1 * cos + d1 * sinAxis.x - b1 * sinAxis.z;
			d = a1 * sinAxis.z + d1 * cos + b1 * sinAxis.y - c1 * sinAxis.x;
		}

		/// <summary>
		/// Returns the quaternion rotated on the given axis.
		/// </summary>
		public Quaternion Rotated(float angle, Vector3 axis)
		{
			float cos = MathF.Cos(angle);
			Vector3 sinAxis = MathF.Sin(angle) * axis;
			return new Quaternion(cos, sinAxis) *
				new Quaternion(0, new Vector3(b, c, d)) *
				new Quaternion(cos, sinAxis * -1);
		}

		#endregion INSTANCE
	}
}
