using System;

namespace Com.Surbon.CSUtils.Math
{
	#pragma warning disable CS0659 // Le type se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
	#pragma warning disable CS0661 // Le type définit l'opérateur == ou l'opérateur != mais ne se substitue pas à Object.GetHashCode()
	#pragma warning disable CS1591 // Commentaire XML manquant pour le type ou le membre visible publiquement

	/// <summary>
	/// Representation of a vector in a 2 dimensional space.
	/// </summary>
	public struct Vector2
	{
		/// <summary>
		/// Shorthand for writing Vector2(0, -1).
		/// </summary>
		public static Vector2 Down => new Vector2(0, -1);
		/// <summary>
		/// Shorthand for writing Vector2(-1, 0).
		/// </summary>
		public static Vector2 Left => new Vector2(-1, 0);
		/// <summary>
		/// Shorthand for writing Vector2(-1, -1).
		/// </summary>
		public static Vector2 NegOne => new Vector2(-1, -1);
		/// <summary>
		/// Shorthand for writing Vector2(1, 1).
		/// </summary>
		public static Vector2 One => new Vector2(1, 1);
		/// <summary>
		/// Shorthand for writing Vector2(1, 0).
		/// </summary>
		public static Vector2 Right => new Vector2(1, 0);
		/// <summary>
		/// Shorthand for writing Vector2(0, 1).
		/// </summary>
		public static Vector2 Up => new Vector2(0, 1);
		/// <summary>
		/// Shorthand for writing Vector2(0, 0).
		/// </summary>
		public static Vector2 Zero => new Vector2(0, 0);

		/// <summary>
		/// The position of the <see cref="Vector2"/> on the x axis.
		/// </summary>
		public float x;
		/// <summary>
		/// The position of the <see cref="Vector2"/> on the y axis.
		/// </summary>
		public float y;

		/// <summary>
		/// Creates a <see cref="Vector2"/> as (x, y).
		/// </summary>
		public Vector2(float x = 0f, float y = 0f)
		{
			this.x = x;
			this.y = y;
		}

		/// <summary>
		/// Creates a <see cref="Vector2"/> with its values set to the given <see cref="Vector2"/>.
		/// </summary>
		public Vector2(Vector2 vector)
		{
			x = vector.x;
			y = vector.y;
		}

		#region OPERATORS

		/// <summary>
		/// Adds the given <see cref="Vector2"/>.
		/// </summary>
		/// <returns>vector1 + vector2.</returns>
		public static Vector2 operator +(Vector2 vector1, Vector2 vector2) => new Vector2(vector1.x + vector2.x, vector1.y + vector2.y);

		/// <summary>
		/// Sets the values of the <see cref="Vector2"/> to the opposite values (equivalent to vector * -1).
		/// </summary>
		/// <returns>-vector.</returns>
		public static Vector2 operator-(Vector2 vector) => new Vector2(-vector.x, -vector.y);

		/// <summary>
		/// Subtract the second <see cref="Vector2"/> to the first one.
		/// </summary>
		/// <returns>vector1 - vector2.</returns>
		public static Vector2 operator -(Vector2 vector1, Vector2 vector2) => new Vector2(vector1.x - vector2.x, vector1.y - vector2.y);

		/// <summary>
		/// Multiplies the values of the first <see cref="Vector2"/> by the values of the second one.
		/// </summary>
		/// <returns>vector1 * vector2.</returns>
		public static Vector2 operator *(Vector2 vector1, Vector2 vector2) => new Vector2(vector1.x * vector2.x, vector1.y * vector2.y);

		/// <summary>
		/// Multiplies the <see cref="Vector2"/> with the <see cref="float"/>.
		/// </summary>
		/// <returns>vector * scalar.</returns>
		public static Vector2 operator *(Vector2 vector, float scalar) => new Vector2(vector.x * scalar, vector.y * scalar);

		/// <summary>
		/// Multiplies the <see cref="Vector2"/> with the <see cref="float"/>.
		/// </summary>
		/// <returns>vector * scalar.</returns>
		public static Vector2 operator *(float scalar, Vector2 vector) => vector * scalar;

		/// <summary>
		/// Divides the values of the first <see cref="Vector2"/> by the values of the second <see cref="Vector2"/>.
		/// </summary>
		/// <returns>vector1 / vector2.</returns>
		public static Vector2 operator /(Vector2 vector1, Vector2 vector2) => new Vector2(vector1.x / vector2.x, vector1.y / vector2.y);

		/// <summary>
		/// Divides the values of the <see cref="Vector2"/> by the <see cref="float"/>.
		/// </summary>
		/// <returns>vector / scalar.</returns>
		public static Vector2 operator /(Vector2 vector, float scalar) => new Vector2(vector.x / scalar, vector.y / scalar);

		/// <summary>
		/// Says if both vectors have the same values.
		/// </summary>
		public static bool operator ==(Vector2 vector1, Vector2 vector2) => vector1.x == vector2.x && vector1.y == vector2.y;

		/// <summary>
		/// Says if both vector have different values.
		/// </summary>
		public static bool operator !=(Vector2 vector1, Vector2 vector2) => vector1.x != vector2.x || vector1.y != vector2.y;

		#endregion OPERATORS

		#region INSTANCE

		/// <summary>
		/// Returns the <see cref="Vector2"/> with absolute values.
		/// </summary>
		public Vector2 Abs() => new Vector2(MathF.Abs(x), MathF.Abs(y));

		/// <summary>
		/// Returns the <see cref="Vector2"/>'s angle in radians.
		/// </summary>
		public float Angle() => MathF.Atan2(y, x);

		/// <summary>
		/// Returns the angle between the <see cref="Vector2"/> (this) and the given <see cref="Vector2"/>.
		/// </summary>
		public float AngleTo(Vector2 vector) => MathF.Atan2(Cross(vector), Dot(vector));

		/// <summary>
		/// Returns the angle between the line passing through both <see cref="Vector2"/>, and the x axis.
		/// </summary>
		public float AngleToPoint(Vector2 vector) => (vector - this).Angle();

		/// <summary>
		/// Rounds up the length of the <see cref="Vector2"/>.
		/// </summary>
		public void CeilLength()
		{
			float l = x * x + y * y;

			if (l != 0)
			{
				l = MathF.Sqrt(l);
				l /= MathF.Ceiling(l);
				x /= l;
				y /= l;
			}
		}

		/// <summary>
		/// Rounds up the values of the <see cref="Vector2"/>.
		/// </summary>
		public void CeilValues()
		{
			x = MathF.Ceiling(x);
			y = MathF.Ceiling(y);
		}

		/// <summary>
		/// Sets the length of the <see cref="Vector2"/> between min and max.
		/// </summary>
		public void ClampLength(float min, float max)
		{
			float l = x * x + y * y;

			if (l != 0)
			{
				l = MathF.Sqrt(l);
				l /= MathT.Clamp(l, min, max);
				x /= l;
				y /= l;
			}
		}

		/// <summary>
		/// Clamps x between minX and maxX, and y between minY and maxY.
		/// </summary>
		public void ClampValues(float minX, float maxX, float minY, float maxY)
		{
			x = MathT.Clamp(x, minX, maxX);
			y = MathT.Clamp(y, minY, maxY);
		}

		/// <summary>
		/// Clamps x and y between min and max.
		/// </summary>
		public void ClampValuesUniform(float min, float max)
		{
			ClampValues(min, max, min, max);
		}

		/// <summary>
		/// Returns the cross product of the <see cref="Vector2"/> (this) and the given <see cref="Vector2"/>.
		/// </summary>
		public float Cross(Vector2 vector) => x * vector.y - y * vector.x;

		/// <summary>
		/// Returns the distance between the <see cref="Vector2"/> (this) and the given <see cref="Vector2"/>.
		/// </summary>
		public float Distance(Vector2 vector) => MathF.Sqrt((x - vector.x) * (x - vector.x) + (y - vector.x) * (y - vector.y));

		/// <summary>
		/// Returns the distance squared of the <see cref="Vector2"/> (this) and the given <see cref="Vector2"/>.
		/// </summary>
		public float DistanceSquared(Vector2 vector) => (x - vector.x) * (x - vector.x) + (y - vector.x) * (y - vector.y);

		/// <summary>
		/// Returns the dot product of the <see cref="Vector2"/> (this) and the given <see cref="Vector2"/>.
		/// </summary>
		public float Dot(Vector2 vector) => x * vector.x + y * vector.y;

		public override bool Equals(object obj)
		{
			if (obj is Vector2)
				return (Vector2)obj == this;

			return false;
		}

		/// <summary>
		/// Rounds the length of the <see cref="Vector2"/> downward.
		/// </summary>
		public void FloorLength()
		{
			float l = x * x + y * y;

			if (l != 0)
			{
				l = MathF.Sqrt(l);
				l /= MathF.Floor(l);
				x /= l;
				y /= l;
			}
		}

		/// <summary>
		/// Rounds the values of the <see cref="Vector2"/> downward.
		/// </summary>
		public void FloorValues()
		{
			x = MathF.Floor(x);
			y = MathF.Floor(y);
		}

		/// <summary>
		/// Says if the <see cref="Vector2"/> is normalized (if the length is equal to 1).
		/// </summary>
		public bool IsNormalized() => LengthSquared() == 1f;

		/// <summary>
		/// Returns the length of the <see cref="Vector2"/>.
		/// </summary>
		public float Length() => MathF.Sqrt(x * x + y * y);

		/// <summary>
		/// Returns the length squared of the <see cref="Vector2"/>.
		/// </summary>
		public float LengthSquared() => x * x + y * y;

		/// <summary>
		/// Lerps the <see cref="Vector2"/> between this and to by weight (weight is clamped between 0 and 1).
		/// </summary>
		public Vector2 Lerp(Vector2 to, float weight) => LerpUnclamped(to, MathT.Clamp(weight, 0, 1));

		/// <summary>
		/// Lerps the <see cref="Vector2"/> between this and to by a random <see cref="float"/> between 0 and 1.
		/// </summary>
		public Vector2 LerpRand(Vector2 to) => LerpUnclamped(to, (float)new Random().NextDouble());

		/// <summary>
		/// Lerps the <see cref="Vector2"/> between this and to by weight.
		/// </summary>
		public Vector2 LerpUnclamped(Vector2 to, float weight) => new Vector2(x + weight * (to.x - x), y + weight * (to.y - y));

		/// <summary>
		/// Performs a modulus operation on x and y, where the result is in ]-mod, 0].
		/// </summary>
		public Vector2 NegMod(float mod) => new Vector2(MathT.Congruence(x, mod, false), MathT.Congruence(y, mod, false));

		/// <summary>
		/// Performs a modulus operation on x and y, where the result is in ]-modv.x, 0] for x, and ]-modv.y, 0] for y.
		/// </summary>
		public Vector2 NegModv(Vector2 modv) => new Vector2(MathT.Congruence(x, modv.x, false), MathT.Congruence(y, modv.y, false));

		/// <summary>
		/// Sets the length of the <see cref="Vector2"/> to the given length.
		/// </summary>
		public void Normalize(float length = 1)
		{
			if (length == 0)
				throw new DivideByZeroException("The length must be different from zero.");

			float l = x * x + y * y;

			if (l != 0)
			{
				l = MathF.Sqrt(l) / length;
				x /= l;
				y /= l;
			}
		}

		/// <summary>
		/// Returns the <see cref="Vector2"/> with its length set to the given length.
		/// </summary>
		public Vector2 Normalized(float length = 1)
		{
			if (length == 0)
				throw new DivideByZeroException("The length must be different from zero.");

			float l = x * x + y * y;

			if(l == 0)
				return new Vector2(0, 0);
			
			l = MathF.Sqrt(l) / length;
			return new Vector2(x / l, y / l);
		}

		/// <summary>
		/// Performs a modulus operation on x and y, where the result is in [0, mod[.
		/// </summary>
		public Vector2 PosMod(float mod) => new Vector2(MathT.Congruence(x, mod), MathT.Congruence(y, mod));

		/// <summary>
		/// Performs a modulus operation on x and y, where the result is in [0, modv.x[ for x, and [0, modv.y[ for y.
		/// </summary>
		public Vector2 PosModv(Vector2 modv) => new Vector2(MathT.Congruence(x, modv.x), MathT.Congruence(y, modv.y));

		/// <summary>
		/// Returns the <see cref="Vector2"/> with x and y to the power of pow.
		/// </summary>
		public Vector2 Pow(float pow) => new Vector2(MathF.Pow(x, pow), MathF.Pow(y, pow));

		/// <summary>
		/// Rotates the <see cref="Vector2"/> by phi radians.
		/// </summary>
		/// <param name="phi">The angle in radians.</param>
		public void Rotate(float phi)
		{
			float sin = MathF.Sin(phi);
			float cos = MathF.Cos(phi);
			x = x * cos - y * sin;
			y = x * sin + y * cos;
		}

		/// <summary>
		/// Returns the <see cref="Vector2"/> rotated by phi radians.
		/// </summary>
		/// <param name="phi">The angle in radians.</param>
		public Vector2 Rotated(float phi)
		{
			float sin = MathF.Sin(phi);
			float cos = MathF.Cos(phi);

			return new Vector2(x * cos - y * sin, x * sin + y * cos);
		}

		/// <summary>
		/// Rounds the length of the <see cref="Vector2"/>.
		/// </summary>
		public void RoundLength()
		{
			float l = x * x + y * y;

			if (l != 0)
			{
				l = MathF.Sqrt(l);
				l /= MathF.Round(l);
				x /= l;
				y /= l;
			}
		}

		/// <summary>
		/// Rounds the values of the <see cref="Vector2"/>.
		/// </summary>
		public void RoundValues()
		{
			x = MathF.Round(x);
			y = MathF.Round(y);
		}

		/// <summary>
		/// Returns the signs of the <see cref="Vector2"/> as (sign x, sign y).
		/// </summary>
		public Vector2 Sign() => new Vector2(MathF.Sign(x), MathF.Sign(y));

		public override string ToString() => $"({x}, {y})";

		#endregion INSTANCE

		#region STATIC

		/// <summary>
		/// Returns the polar coordinates of the <see cref="Vector2"/> given in cartesian coordinates.
		/// </summary>
		/// <param name="vector">Cartesian coordinates as (x, y).</param>
		/// <returns>Polar coordinates as (r, th) with th in radians.</returns>
		public static Vector2 CartesianToPolar(Vector2 vector) => new Vector2(vector.Length(), vector.Angle());

		/// <summary>
		/// Returns the polar coordinates of the <see cref="Vector2"/> given in cartesian coordinates.
		/// </summary>
		/// <returns>Polar coordinates as (r, th) with th in radians.</returns>
		public static Vector2 CartesianToPolar(float x, float y) => new Vector2(MathF.Sqrt(x * x + y * y), MathF.Atan2(y, x));

		/// <summary>
		/// Returns the cartesian coordinates of the <see cref="Vector2"/> given in polar coordinates.
		/// </summary>
		/// <param name="vector">Polar coordinates as (r, th) with th in radians.</param>
		/// <returns>Cartesian coordinates as (x, y).</returns>
		public static Vector2 PolarToCartesian(Vector2 vector) => new Vector2(vector.x * MathF.Cos(vector.y), vector.x * MathF.Sin(vector.y));

		/// <summary>
		/// Returns the cartesian coordinates of the <see cref="Vector2"/> given in polar coordinates.
		/// </summary>
		/// <param name="r">Radius.</param>
		/// <param name="th">Angle in radians.</param>
		/// <returns>Cartesian coordinates as (x, y).</returns>
		public static Vector2 PolarToCartesian(float r, float th) => new Vector2(r * MathF.Cos(th), r * MathF.Sin(th));

		#endregion STATIC
	}
}
