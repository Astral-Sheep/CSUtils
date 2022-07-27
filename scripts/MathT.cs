using System;

namespace Com.Surbon.CSUtils
{
	/// <summary>
	/// Provides structs and methods for mathematical operations
	/// </summary>
	public static class MathT
	{
		#pragma warning disable CS0659 // Le type se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
		#pragma warning disable CS0661 // Le type définit l'opérateur == ou l'opérateur != mais ne se substitue pas à Object.GetHashCode()

		/// <summary>
		/// Representation of a vector in a 2 dimensional space.
		/// </summary>
		public struct Vector2
		{
			public float x;
			public float y;

			public Vector2(float pX = 0f, float pY = 0f)
			{
				x = pX;
				y = pY;
			}

			#region OPERATORS

			public static Vector2 operator +(Vector2 vector1, Vector2 vector2) => new Vector2(vector1.x + vector2.x, vector1.y + vector2.y);

			public static Vector2 operator -(Vector2 vector1, Vector2 vector2) => new Vector2(vector1.x - vector2.x, vector1.y - vector2.y);

			public static Vector2 operator *(Vector2 vector1, Vector2 vector2) => new Vector2(vector1.x * vector2.x, vector1.y * vector2.y);

			public static Vector2 operator *(Vector2 vector, float scalar) => new Vector2(vector.x * scalar, vector.y * scalar);

			public static Vector2 operator *(float scalar, Vector2 vector) => vector * scalar;

			public static Vector2 operator /(Vector2 vector1, Vector2 vector2) => new Vector2(vector1.x / vector2.x, vector1.y / vector2.y);

			public static Vector2 operator /(Vector2 vector, float scalar) => new Vector2(vector.x / scalar, vector.y / scalar);

			public static bool operator ==(Vector2 vector1, Vector2 vector2) => vector1.x == vector2.x && vector1.y == vector2.y;

			public static bool operator !=(Vector2 vector1, Vector2 vector2) => vector1.x != vector2.x || vector1.y != vector2.y;

			#endregion OPERATORS

			#region INSTANCE

			/// <summary>
			/// Returns the vector with absolute values.
			/// </summary>
			public Vector2 Abs() => new Vector2(MathF.Abs(x), MathF.Abs(y));

			/// <summary>
			/// Returns the vector's angle in radians.
			/// </summary>
			public float Angle() => MathF.Atan2(y, x);

			/// <summary>
			/// Returns the angle between the vector (this) and the given vector.
			/// </summary>
			public float AngleTo(Vector2 vector) => MathF.Atan2(Cross(vector), Dot(vector));

			/// <summary>
			/// Returns the angle between the line passing through both points, and the x axis.
			/// </summary>
			public float AngleToPoint(Vector2 vector) => (vector - this).Angle();

			/// <summary>
			/// Rounds up the length of the vector.
			/// </summary>
			public void CeilLength()
			{
				float l = x * x + y * y;

				if (l != 0)
				{
					l = MathF.Sqrt(l);
					x /= l / MathF.Ceiling(l);
					y /= l / MathF.Ceiling(l);
				}
			}

			/// <summary>
			/// Rounds up the values of the vector.
			/// </summary>
			public void CeilValues()
			{
				x = MathF.Ceiling(x);
				y = MathF.Ceiling(y);
			}

			/// <summary>
			/// Sets the length of the vector between min and max.
			/// </summary>
			public void ClampLength(float min, float max)
			{
				float l = x * x + y * y;

				if (l != 0)
				{
					l = MathF.Sqrt(l);
					float lClamped = Clamp(l, min, max);
					x /= l / lClamped;
					y /= l / lClamped;
				}
			}

			/// <summary>
			/// Clamps x between minX and maxX, and y between minY and maxY.
			/// </summary>
			public void ClampValues(float minX, float maxX, float minY, float maxY)
			{
				x = Clamp(x, minX, maxX);
				y = Clamp(y, minY, maxY);
			}

			/// <summary>
			/// Clamps x and y between min and max.
			/// </summary>
			public void ClampValuesUniform(float min, float max)
			{
				ClampValues(min, max, min, max);
			}

			/// <summary>
			/// Returns the cross product of the vector (this) and the given vector.
			/// </summary>
			public float Cross(Vector2 vector) => x * vector.y - y * vector.x;

			/// <summary>
			/// Returns the distance between the vector (this) and the given vector.
			/// </summary>
			public float Distance(Vector2 vector) => MathF.Sqrt((x - vector.x) * (x - vector.x) + (y - vector.x) * (y - vector.y));

			/// <summary>
			/// Returns the distance squared of the vector (this) and the given vector.
			/// </summary>
			public float DistanceSquared(Vector2 vector) => (x - vector.x) * (x - vector.x) + (y - vector.x) * (y - vector.y);

			/// <summary>
			/// Returns the dot product of the vector (this) and the given vector.
			/// </summary>
			public float Dot(Vector2 vector) => x * vector.x + y * vector.y;

			public override bool Equals(object obj)
			{
				if (obj is Vector2)
					return (Vector2)obj == this;
				return false;
			}

			/// <summary>
			/// Rounds the length of the vector downward.
			/// </summary>
			public void FloorLength()
			{
				float l = x * x + y * y;

				if (l != 0)
				{
					l = MathF.Sqrt(l);
					x /= l / MathF.Floor(l);
					y /= l / MathF.Floor(l);
				}
			}

			/// <summary>
			/// Rounds the values of the vector downward.
			/// </summary>
			public void FloorValues()
			{
				x = MathF.Floor(x);
				y = MathF.Floor(y);
			}

			/// <summary>
			/// Says if the vector is normalized (if the length is equal to 1).
			/// </summary>
			public bool IsNormalized() => LengthSquared() == 1f;

			/// <summary>
			/// Returns the length of the vector.
			/// </summary>
			public float Length() => MathF.Sqrt(x * x + y * y);

			/// <summary>
			/// Returns the length squared of the vector.
			/// </summary>
			public float LengthSquared() => x * x + y * y;

			/// <summary>
			/// Performs a modulus operation on x and y, where the result is in ]-b, 0].
			/// </summary>
			public Vector2 NegMod(float mod) => new Vector2(Congruence(x, mod), Congruence(y, mod));

			/// <summary>
			/// Performs a modulus operation on x and y, where the result is in ]modv.x, 0] for x, and ]modv.y, 0] for y.
			/// </summary>
			public Vector2 NegModv(Vector2 modv) => new Vector2(Congruence(x, modv.x), Congruence(y, modv.y));

			/// <summary>
			/// Sets the length of the vector to the given length.
			/// </summary>
			public void Normalize(float length = 1)
			{
				float l = x * x + y * y;

				if (l != 0)
				{
					l = MathF.Sqrt(l) / length;
					x /= l;
					y /= l;
				}
			}

			/// <summary>
			/// Returns the normalized vector.
			/// </summary>
			public Vector2 Normalized()
			{
				float l = LengthSquared();

				if (l == 0)
					throw new InvalidOperationException("The vector's length must be greater than 0");

				l = MathF.Sqrt(l);
				return new Vector2(x / l, y / l);
			}

			/// <summary>
			/// Performs a modulus operation on x and y, where the result is in [0, b[.
			/// </summary>
			public Vector2 PosMod(float mod) => new Vector2(Congruence(x, mod), Congruence(y, mod));

			/// <summary>
			/// Performs a modulus operation on x and y, where the result is in [0, modv.x[ for x, and [0, modv.y[ for y.
			/// </summary>
			public Vector2 PosModV(Vector2 modv) => new Vector2(Congruence(x, modv.x), Congruence(y, modv.y));

			/// <summary>
			/// Returns the vector with x and y to the power of pow.
			/// </summary>
			public Vector2 Pow(float pow) => new Vector2(MathF.Pow(x, pow), MathF.Pow(y, pow));

			/// <summary>
			/// Rotates the vector by phi radians.
			/// </summary>
			/// <param name="phi"></param>
			public void Rotate(float phi)
			{
				float sin = MathF.Sin(phi);
				float cos = MathF.Cos(phi);
				x = x * cos - y * sin;
				y = x * sin + y * cos;
			}

			/// <summary>
			/// Returns the vector rotated by phi radians.
			/// </summary>
			public Vector2 Rotated(float phi)
			{
				float sin = MathF.Sin(phi);
				float cos = MathF.Cos(phi);

				return new Vector2(x * cos - y * sin, x * sin + y * cos);
			}

			/// <summary>
			/// Rounds the length of the vector.
			/// </summary>
			public void RoundLength()
			{
				float l = x * x + y * y;

				if (l != 0)
				{
					l = MathF.Sqrt(l);
					x /= l / MathF.Round(l);
					y /= l / MathF.Round(l);
				}
			}

			/// <summary>
			/// Rounds the values of the vector.
			/// </summary>
			public void RoundValues()
			{
				x = MathF.Round(x);
				y = MathF.Round(y);
			}

			/// <summary>
			/// Returns the signs of the vector as (sign x, sign y).
			/// </summary>
			public Vector2 Sign() => new Vector2(MathF.Sign(x), MathF.Sign(y));

			#endregion INSTANCE

			#region STATIC

			/// <summary>
			/// Returns the polar coordinates of the vector given in cartesian coordinates
			/// </summary>
			/// <param name="vector">Cartesian coordinates as (x, y)</param>
			/// <returns>Polar coordinates as (r, th)</returns>
			public static Vector2 CartesianToPolar(Vector2 vector) => new Vector2(vector.Length(), vector.Angle());

			/// <summary>
			/// Returns the polar coordinates of the vector given in cartesian coordinates
			/// </summary>
			/// <param name="vector">Cartesian coordinates as (x, y)</param>
			/// <returns>Polar coordinates as (r, th)</returns>
			public static Vector2 CartesianToPolar(float x, float y) => new Vector2(MathF.Sqrt(x * x + y * y), MathF.Atan2(y, x));

			/// <summary>
			/// Returns the cartesian coordinates of the vector given in polar coordinates
			/// </summary>
			/// <param name="vector">Polar coordinates as (r, th)</param>
			/// <returns>Cartesian coordinates as (x, y)</returns>
			public static Vector2 PolarToCartesian(Vector2 vector) => new Vector2(vector.x * MathF.Cos(vector.y), vector.x * MathF.Sin(vector.y));

			/// <summary>
			/// Returns the cartesian coordinates of the vector given in polar coordinates
			/// </summary>
			/// <param name="r">Radius</param>
			/// <param name="th">Angle</param>
			/// <returns>Cartesian coordinates as (x, y)</returns>
			public static Vector2 PolarToCartesian(float r, float th) => new Vector2(r * MathF.Cos(th), r * MathF.Sin(th));

			#endregion STATIC
		}

		public struct Vector3
		{
			public enum ANGLE
			{
				AZIMUTHAL = 0,
				POLAR = 1
			}

			public float x;
			public float y;
			public float z;

			public Vector3(float pX = 0f, float pY = 0f, float pZ = 0f)
			{
				x = pX;
				y = pY;
				z = pZ;
			}

			#region OPERATORS

			public static Vector3 operator +(Vector3 vector1, Vector3 vector2)
			{
				return new Vector3(vector1.x + vector2.x, vector1.y + vector2.y, vector1.z + vector2.z);
			}

			public static Vector3 operator -(Vector3 vector1, Vector3 vector2)
			{
				return new Vector3(vector1.x - vector2.x, vector1.y - vector2.y, vector1.z - vector2.z);
			}

			public static Vector3 operator *(Vector3 vector1, Vector3 vector2)
			{
				return new Vector3(vector1.x * vector2.x, vector1.y * vector2.y, vector1.z * vector2.z);
			}

			public static Vector3 operator *(Vector3 vector, float scalar)
			{
				return new Vector3(vector.x * scalar, vector.y * scalar, vector.z * scalar);
			}

			public static Vector3 operator *(float scalar, Vector3 vector)
			{
				return vector * scalar;
			}

			public static Vector3 operator /(Vector3 vector1, Vector3 vector2)
			{
				return new Vector3(vector1.x / vector2.x, vector1.y / vector2.y, vector1.z / vector2.z);
			}

			public static Vector3 operator /(Vector3 vector, float scalar)
			{
				return new Vector3(vector.x / scalar, vector.y / scalar, vector.z / scalar);
			}

			public static bool operator == (Vector3 vector1, Vector3 vector2)
			{
				return vector1.x == vector2.x && vector1.y == vector2.y && vector1.z == vector2.z;
			}

			public static bool operator != (Vector3 vector1, Vector3 vector2)
			{
				return vector1.x != vector2.x || vector1.y != vector2.y || vector1.z != vector2.z;
			}

			#endregion OPERATORS

			#region INSTANCE

			public Vector3 Abs() => new Vector3(MathF.Abs(x), MathF.Abs(y), MathF.Abs(z));

			public (float phi, float theta) Angles() => (MathF.Atan2(y, x), MathF.Atan2(z, new Vector2(x, y).Length()));

			public float AngleTo(Vector3 vector) => MathF.Atan2(Cross(vector).Length(), Dot(vector));

			public void CeilLength()
			{
				float l = x * x + y * y + z * z;

				if (l != 0)
				{
					l = MathF.Sqrt(l);
					float ceil = MathF.Ceiling(l);
					x /= l / ceil;
					y /= l / ceil;
					z /= l / ceil;
				}
			}

			public void CeilValues()
			{
				x = MathF.Ceiling(x);
				y = MathF.Ceiling(y);
				z = MathF.Ceiling(z);
			}

			public void ClampLength(float min, float max)
			{
				float l = x * x + y * y + z * z;

				if (l != 0)
				{
					l = MathF.Sqrt(l);
					float clamped = Clamp(l, min, max);
					x /= l / clamped;
					y /= l / clamped;
					z /= l / clamped;
				}
			}

			public void ClampValues(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
			{
				x = Clamp(x, minX, maxX);
				y = Clamp(y, minY, maxY);
				z = Clamp(z, minZ, maxZ);
			}

			public void ClampValuesUniform(float min, float max)
			{
				ClampValues(min, max, min, max, min, max);
			}

			public Vector3 Cross(Vector3 vector)
			{
				return new Vector3(
					y * vector.z - z * vector.y,
					z * vector.x - x * vector.z,
					x * vector.y - y * vector.x
				);
			}

			public float Distance(Vector3 vector)
			{
				return MathF.Sqrt((x - vector.x) * (x - vector.x) + (y - vector.y) * (y - vector.y) + (z - vector.z) * (z - vector.z));
			}

			public float DistanceSquared(Vector3 vector)
			{
				return (x - vector.x) * (x - vector.x) + (y - vector.y) * (y - vector.y) + (z - vector.z) * (z - vector.z);
			}

			public float Dot(Vector3 vector) => x * vector.x + y * vector.y + z * vector.z;

			public override bool Equals(object obj)
			{
				if (obj is Vector3)
					return (Vector3)obj == this;
				return false;
			}

			public void FloorLength()
			{
				float l = x * x + y * y + z * z;

				if (l != 0)
				{
					l = MathF.Sqrt(l);
					float floor = MathF.Floor(l);
					x /= l / floor;
					y /= l / floor;
					z /= l / floor;
				}
			}

			public void FloorValues()
			{
				x = MathF.Floor(x);
				y = MathF.Floor(y);
				z = MathF.Floor(z);
			}

			public bool IsNormalized() => LengthSquared() == 1;

			public float Length() => MathF.Sqrt(x * x + y * y + z * z);

			public float LengthSquared() => x * x + y * y + z * z;

			public Vector3 Lerp(Vector3 to, float weight)
			{
				return new Vector3(
					x + weight * (to.x - x),
					y + weight * (to.y - y),
					z + weight * (to.z - z)
					);
			}

			public void Normalize(float length = 1f)
			{
				float l = x * x + y * y + z * z;

				if (l != 0)
				{
					l = MathF.Sqrt(l) / length;
					x /= l;
					y /= l;
					z /= l;
				}
			}

			public Vector3 Normalized()
			{
				float l = x * x + y * y + z * z;

				if (l == 0)
					throw new InvalidOperationException("The vector's length must be greater than 0");

				l = MathF.Sqrt(l);
				return new Vector3(x / l, y / l, z / l);
			}

			public Vector3 PosMod(float mod)
			{
				return new Vector3(Congruence(x, mod), Congruence(y, mod), Congruence(z, mod));
			}

			public Vector3 PosModv(Vector3 modv)
			{
				return new Vector3(Congruence(x, modv.x), Congruence(y, modv.y), Congruence(z, modv.z));
			}

			public void Rotate(float value, ANGLE angle = ANGLE.AZIMUTHAL)
			{
				switch (angle)
				{
					case ANGLE.AZIMUTHAL:
						float phi = MathF.Atan2(y, x);
						float sin = MathF.Sin(phi);
						float cos = MathF.Cos(phi);
						x = x * sin - y * cos;
						y = x * cos + y * sin;
						break;
					case ANGLE.POLAR:
						float theta = MathF.Atan2(z, new Vector2(x, y).Length());
						throw new NotImplementedException("I'll do it later");
					default:
						throw new Exception("How tf did you get there ?");
				}
			}

			public Vector3 Rotated(float value, ANGLE angle = ANGLE.AZIMUTHAL)
			{
				Vector3 vector = new Vector3();

				switch (angle)
				{
					case ANGLE.AZIMUTHAL:
						float phi = MathF.Atan2(y, x);
						float sin = MathF.Sin(phi);
						float cos = MathF.Cos(phi);
						vector.x = x * sin - y * cos;
						vector.y = x * cos + y * sin;
						vector.z = z;
						break;
					case ANGLE.POLAR:
						float theta = MathF.Atan2(z, new Vector2(x, y).Length());
						throw new NotImplementedException("I'll do it later");
					default:
						throw new Exception("How tf did you get there ?");
				}

				return vector;
			}

			public void RoundLength()
			{
				float l = x * x + y * y + z * z;

				if (l != 0)
				{
					l = MathF.Sqrt(l);
					float round = MathF.Round(l);
					x /= l / round;
					y /= l / round;
					z /= l / round;
				}
			}

			public void RoundValues()
			{
				x = MathF.Round(x);
				y = MathF.Round(y);
				z = MathF.Round(z);
			}

			public Vector3 Sign() => new Vector3(MathF.Sign(x), MathF.Sign(y), MathF.Sign(z));

			#endregion INSTANCE
		}

		/// <summary>
		/// Clamps value between min and max.
		/// </summary>
		public static float Clamp(float value, float min, float max)
		{
			if (value <= min) return min;
			else if (value >= max) return max;
			return value;
		}

		/// <summary>
		/// Returns a modulo b.
		/// </summary>
		/// <param name="a">The dividend.</param>
		/// <param name="b">The divisor.</param>
		/// <param name="isPos">If true, the remainder is negative.</param>
		public static float Congruence(float a, float b, bool isPos = true) => EuclidianRemainder(a, b) - (isPos ? 0 : b);


		/// <summary>
		/// Returns a modulo b.
		/// </summary>
		/// <param name="a">The dividend.</param>
		/// <param name="b">The divisor.</param>
		/// <param name="isPos">If true, the remainder is negative.</param>
		public static int Congruence(int a, int b, bool isPos = true) => EuclidianRemainder(a, b) - (isPos ? 0 : b);

		/// <summary>
		/// Returns the euclidian quotient of a / b.
		/// </summary>
		public static float EuclidianQuotient(float a, float b) => MathF.Floor(a / b);

		/// <summary>
		/// Returns the euclidian remainder of a / b.
		/// </summary>
		public static float EuclidianRemainder(float a, float b) => a - EuclidianQuotient(a, b) * b;

		/// <summary>
		/// Returns the euclidian remainder of a / b.
		/// </summary>
		public static int EuclidianRemainder(int a, int b) => a - (a / b) * b;
	}
}
