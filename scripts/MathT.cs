using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Surbon.CSUtils
{
	/// <summary>
	/// Provides structs and methods for mathematical operations
	/// </summary>
	public static class MathT
	{
		#pragma warning disable CS0659 // Le type se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
		#pragma warning disable CS0661 // Le type définit l'opérateur == ou l'opérateur != mais ne se substitue pas à Object.GetHashCode()
		#pragma warning disable CS1591 // Commentaire XML manquant pour le type ou le membre visible publiquement

		/// <summary>
		/// Representation of a vector in a 2 dimensional space.
		/// </summary>
		public struct Vector2
		{
			public static readonly Vector2 Down = new Vector2(0, 1);
			public static readonly Vector2 Left = new Vector2(-1, 0);
			public static readonly Vector2 NegOne = new Vector2(-1, -1);
			public static readonly Vector2 One = new Vector2(1, 1);
			public static readonly Vector2 Right = new Vector2(1, 0);
			public static readonly Vector2 Up = new Vector2(0, -1);

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
			/// Lerp the vector between this and to by weight (weight is clamped between 0 and 1).
			/// </summary>
			public Vector2 Lerp(Vector2 to, float weight) => LerpUnclamped(to, Clamp(weight, 0, 1));

			/// <summary>
			/// Lerp the vector between this and to by a random number between 0 and 1.
			/// </summary>
			public Vector2 LerpRand(Vector2 to) => LerpUnclamped(to, (float)new Random().NextDouble());

			/// <summary>
			/// Lerp the vector between this and to by weight.
			/// </summary>
			public Vector2 LerpUnclamped(Vector2 to, float weight) => new Vector2(x + weight * (to.x - x), y + weight * (to.y - y));

			/// <summary>
			/// Performs a modulus operation on x and y, where the result is in ]-mod, 0].
			/// </summary>
			public Vector2 NegMod(float mod) => new Vector2(Congruence(x, mod, false), Congruence(y, mod, false));

			/// <summary>
			/// Performs a modulus operation on x and y, where the result is in ]-modv.x, 0] for x, and ]-modv.y, 0] for y.
			/// </summary>
			public Vector2 NegModv(Vector2 modv) => new Vector2(Congruence(x, modv.x, false), Congruence(y, modv.y, false));

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
			/// Performs a modulus operation on x and y, where the result is in [0, mod[.
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

		/// <summary>
		/// Representation of a vector in a 3 dimensional space
		/// </summary>
		public struct Vector3
		{
			public static readonly Vector3 Backward = new Vector3(0, 0, -1);
			public static readonly Vector3 Down = new Vector3(0, 1, 0);
			public static readonly Vector3 Forward = new Vector3(0, 0, 1);
			public static readonly Vector3 Left = new Vector3(-1, 0, 0);
			public static readonly Vector3 NegOne = new Vector3(-1, -1, -1);
			public static readonly Vector3 One = new Vector3(1, 1, 1);
			public static readonly Vector3 Right = new Vector3(1, 0, 0);
			public static readonly Vector3 Up = new Vector3(0, -1, 0);

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

			/// <summary>
			/// Returns the vector with absolute values.
			/// </summary>
			public Vector3 Abs() => new Vector3(MathF.Abs(x), MathF.Abs(y), MathF.Abs(z));

			/// <summary>
			/// Returns the azimuthal angle and the polar angle.
			/// </summary>
			/// <returns>Angles as (azimuthal, polar)</returns>
			public (float phi, float theta) Angles() => (MathF.Atan2(y, x), MathF.Atan2(z, new Vector2(x, y).Length()));

			/// <summary>
			/// Returns the angle between the vector (this) and the given vector.
			/// </summary>
			public float AngleTo(Vector3 vector) => MathF.Atan2(Cross(vector).Length(), Dot(vector));

			/// <summary>
			/// Rounds up the length of the vector.
			/// </summary>
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

			/// <summary>
			/// Rounds up the values of the vector.
			/// </summary>
			public void CeilValues()
			{
				x = MathF.Ceiling(x);
				y = MathF.Ceiling(y);
				z = MathF.Ceiling(z);
			}

			/// <summary>
			/// Clamps the length of the vector between min and max.
			/// </summary>
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

			/// <summary>
			/// Clamps x between minX and maxX, y between minY and maxY, and z between minZ and maxZ.
			/// </summary>
			public void ClampValues(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
			{
				x = Clamp(x, minX, maxX);
				y = Clamp(y, minY, maxY);
				z = Clamp(z, minZ, maxZ);
			}

			/// <summary>
			/// Clamps x, y and z between min and max.
			/// </summary>
			public void ClampValuesUniform(float min, float max)
			{
				ClampValues(min, max, min, max, min, max);
			}

			/// <summary>
			/// Returns the cross product of the vector (this) and the given vector.
			/// </summary>
			public Vector3 Cross(Vector3 vector)
			{
				return new Vector3(
					y * vector.z - z * vector.y,
					z * vector.x - x * vector.z,
					x * vector.y - y * vector.x
				);
			}

			/// <summary>
			/// Returns the distance between the vector (this) and the given vector.
			/// </summary>
			public float Distance(Vector3 vector)
			{
				return MathF.Sqrt((x - vector.x) * (x - vector.x) + (y - vector.y) * (y - vector.y) + (z - vector.z) * (z - vector.z));
			}

			/// <summary>
			/// Returns the distance squared between the vector (this) and the given vector.
			/// </summary>
			public float DistanceSquared(Vector3 vector)
			{
				return (x - vector.x) * (x - vector.x) + (y - vector.y) * (y - vector.y) + (z - vector.z) * (z - vector.z);
			}

			/// <summary>
			/// Returns the dot product of the vector (this) and the given the vector.
			/// </summary>
			public float Dot(Vector3 vector) => x * vector.x + y * vector.y + z * vector.z;

			public override bool Equals(object obj)
			{
				if (obj is Vector3)
					return (Vector3)obj == this;
				return false;
			}

			/// <summary>
			/// Rounds the length of the vector downward.
			/// </summary>
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

			/// <summary>
			/// Rounds the values of the vector downward.
			/// </summary>
			public void FloorValues()
			{
				x = MathF.Floor(x);
				y = MathF.Floor(y);
				z = MathF.Floor(z);
			}

			/// <summary>
			/// Says if the vector is normalized (the length is equal to 1).
			/// </summary>
			public bool IsNormalized() => LengthSquared() == 1;

			/// <summary>
			/// Returns the length of the vector.
			/// </summary>
			public float Length() => MathF.Sqrt(x * x + y * y + z * z);

			/// <summary>
			/// Returns the squared length of the vector.
			/// </summary>
			public float LengthSquared() => x * x + y * y + z * z;

			/// <summary>
			/// Lerp the vector between this and to by weight (weight is clamped between 0 and 1).
			/// </summary>
			public Vector3 Lerp(Vector3 to, float weight)
			{
				return LerpUnclamped(to, Clamp(weight, 0, 1));
			}

			/// <summary>
			/// Lerp the vector between this and to by a random number between 0 and 1.
			/// </summary>
			public Vector3 LerpRand(Vector3 to)
			{
				return LerpUnclamped(to, (float)(new Random().NextDouble()));
			}

			/// <summary>
			/// Lerp the vector between this and to by weight.
			/// </summary>
			public Vector3 LerpUnclamped(Vector3 to, float weight)
			{
				return new Vector3(
					x + weight * (to.x - x),
					y + weight * (to.y - y),
					z + weight * (to.z - z)
					);
			}

			/// <summary>
			/// Performs a modulus operation on x, y and z, where the result is in ]-mod, 0].
			/// </summary>
			public Vector3 NegMod(float mod)
			{
				return new Vector3(Congruence(x, mod, false), Congruence(y, mod, false), Congruence(z, mod, false));
			}

			/// <summary>
			/// Performs a modulus operation on x, y and z, where the result is in ]-modv.x, 0] for x, ]-modv.y, 0] for y, and]-modv.z, 0] for z.
			/// </summary>
			public Vector3 NegModv(Vector3 modv)
			{
				return new Vector3(Congruence(x, modv.x, false), Congruence(y, modv.y, false), Congruence(z, modv.z, false));
			}

			/// <summary>
			/// Sets the length of the vector to length.
			/// </summary>
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

			/// <summary>
			/// Returns the vector with its length set to 1.
			/// </summary>
			public Vector3 Normalized()
			{
				float l = x * x + y * y + z * z;

				if (l == 0)
					throw new InvalidOperationException("The vector's length must be greater than 0");

				l = MathF.Sqrt(l);
				return new Vector3(x / l, y / l, z / l);
			}

			/// <summary>
			/// Performs a modulus operation on x, y and z, where the result is in [0, mod[.
			/// </summary>
			public Vector3 PosMod(float mod)
			{
				return new Vector3(Congruence(x, mod), Congruence(y, mod), Congruence(z, mod));
			}

			/// <summary>
			/// Performs a modulus operation on x and y, where the result is in [0, modv.x[ for x, [0, modv.y[ for y, and [0, modv.z[ for z.
			/// </summary>
			public Vector3 PosModv(Vector3 modv)
			{
				return new Vector3(Congruence(x, modv.x), Congruence(y, modv.y), Congruence(z, modv.z));
			}

			/// <summary>
			/// Returns the vector with x, y and z to the power of pow.
			/// </summary>
			public Vector3 Pow(float pow) => new Vector3(MathF.Pow(x, pow), MathF.Pow(y, pow), MathF.Pow(z, pow));

			/// <summary>
			/// Rotates the vector by value radians on the given angle
			/// </summary>
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

			/// <summary>
			/// Returns the vector rotated by value radians on the given angle.
			/// </summary>
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

			/// <summary>
			/// Rounds the length of the vector.
			/// </summary>
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

			/// <summary>
			/// Rounds the values of the vector.
			/// </summary>
			public void RoundValues()
			{
				x = MathF.Round(x);
				y = MathF.Round(y);
				z = MathF.Round(z);
			}

			/// <summary>
			/// Returns a <see cref="Vector3"/> with the sign of the values.
			/// </summary>
			public Vector3 Sign() => new Vector3(MathF.Sign(x), MathF.Sign(y), MathF.Sign(z));

			#endregion INSTANCE

			#region STATIC

			/// <summary>
			/// Returns the cylindric coordinates of the vector from the cartesian coordinates
			/// </summary>
			/// <param name="vector">Cartesian coordinates as (x, y, z)</param>
			/// <returns>Cylindric coordinates as (r, phi, z)</returns>
			public static Vector3 CartesianToCylindric(Vector3 vector)
			{
				Vector2 lPolarVector = Vector2.CartesianToPolar(vector.x, vector.y);

				return new Vector3(lPolarVector.x, lPolarVector.y, vector.z);
			}

			/// <summary>
			/// Returns the cylindric coordinates of the vector given in cartesian coordinates
			/// </summary>
			/// <param name="x">Cartesian coordinate x</param>
			/// <param name="y">Cartesian coordinate y</param>
			/// <param name="z">Cartesian coordinate z</param>
			/// <returns>Cylindric coordinates as (r, phi, z)</returns>
			public static Vector3 CartesianToCylindric(float x, float y, float z)
			{
				Vector2 lPolarVector = Vector2.CartesianToPolar(x, y);

				return new Vector3(lPolarVector.x, lPolarVector.y, z);
			}

			/// <summary>
			/// Returns the spheric coordinates of the vector from the cartesian coordinates
			/// </summary>
			/// <param name="vector">Cartesian coordinates as (x, y, z)</param>
			/// <returns>Spheric coordinates as (rho, phi, th)</returns>
			public static Vector3 CartesianToSpheric(Vector3 vector)
			{
				return new Vector3(
					vector.Length(),
					MathF.Atan2(vector.y, vector.x),
					MathF.Atan2(vector.z, new Vector2(vector.x, vector.y).Length())
					);
			}

			/// <summary>
			/// Returns the spheric coordinates of the vector from the cartesian coordinates
			/// </summary>
			/// <param name="x">Cartesian coordinate x</param>
			/// <param name="y">Cartesian coordinate y</param>
			/// <param name="z">Cartesian coordinate z</param>
			/// <returns>Spheric coordinates as (rho, phi, th)</returns>
			public static Vector3 CartesianToSpheric(float x, float y, float z)
			{
				return new Vector3(
					new Vector3(x, y, z).Length(),
					MathF.Atan2(y, x),
					MathF.Atan2(z, new Vector2(x, y).Length())
					);
			}

			/// <summary>
			/// Returns the cartesian coordinates of the vector given in cylindric coordinates
			/// </summary>
			/// <param name="vector">Cylindric coordinates as (r, phi, z)</param>
			/// <returns>Cartesian coordinates as (x, y, z)</returns>
			public static Vector3 CylindricToCartesian(Vector3 vector)
			{
				Vector2 lCartesianVector = Vector2.PolarToCartesian(vector.x, vector.y);

				return new Vector3(lCartesianVector.x, lCartesianVector.y, vector.z);
			}

			/// <summary>
			/// Returns the cartesian coordinates of the vector given in cylindric coordinates
			/// </summary>
			/// <param name="r">Polar radius</param>
			/// <param name="phi">Azimuth angle</param>
			/// <param name="z">Cartesian coordinate z</param>
			/// <returns>Cartesian coordinates as (x, y, z)</returns>
			public static Vector3 CylindricToCartesian(float r, float phi, float z)
			{
				Vector2 lCartesianVector = Vector2.PolarToCartesian(r, phi);

				return new Vector3(lCartesianVector.x, lCartesianVector.y, z);
			}

			/// <summary>
			/// Returns the spheric coordinates of the vector given in cylindric coordinates
			/// </summary>
			/// <param name="vector">Cylindric coordinates as (r, phi, z)</param>
			/// <returns>Spheric coordinates as (rho, phi, th)</returns>
			public static Vector3 CylindricToSpheric(Vector3 vector)
			{
				return new Vector3(
					new Vector2(vector.x, vector.z).Length(),
					vector.y,
					MathF.Atan2(vector.z, vector.x)
					);
			}

			/// <summary>
			/// Returns the spheric coordinates of the vector given in cylindric coordinates
			/// </summary>
			/// <param name="r">Polar radius</param>
			/// <param name="phi">Azimuth angle</param>
			/// <param name="z">Cartesian coordinate z</param>
			/// <returns>Spheric coordinates as (rho, phi, th)</returns>
			public static Vector3 CylindricToSpheric(float r, float phi, float z)
			{
				return new Vector3(
					new Vector2(r, z).Length(),
					phi,
					MathF.Atan2(z, r)
					);
			}

			/// <summary>
			/// Returns the cartesian coordinates of the vector given in spheric coordinates
			/// </summary>
			/// <param name="vector">Spheric coordinates as (rho, phi, th)</param>
			/// <returns>Cartesian coordinates as (x, y, z)</returns>
			public static Vector3 SphericToCartesian(Vector3 vector)
			{
				return new Vector3(
					vector.x * MathF.Cos(vector.y) * MathF.Sin(vector.z),
					vector.x * MathF.Sin(vector.y) * MathF.Sin(vector.z),
					vector.x * MathF.Cos(vector.z)
					);
			}

			/// <summary>
			/// Returns the cartesian coordinates of the vector given in spheric coordinates
			/// </summary>
			/// <param name="rho">Spheric radius</param>
			/// <param name="phi">Azimuth angle</param>
			/// <param name="th">Polar angle</param>
			/// <returns>Cartesian coordinates as (x, y, z)</returns>
			public static Vector3 SphericToCartesian(float rho, float phi, float th)
			{
				return new Vector3(
					rho * MathF.Cos(phi) * MathF.Sin(th),
					rho * MathF.Sin(phi) * MathF.Sin(th),
					rho * MathF.Cos(th)
					);
			}

			/// <summary>
			/// Returns the cylindric coordinates of the vector given in spheric coordinates
			/// </summary>
			/// <param name="vector">Spheric coordinates as (rho, phi, th)</param>
			/// <returns>Cylindric coordinates as (r, phi, z)</returns>
			public static Vector3 SphericToCylindric(Vector3 vector)
			{
				return new Vector3(
					vector.x * MathF.Sin(vector.z),
					vector.y,
					vector.x * MathF.Cos(vector.z)
					);
			}

			/// <summary>
			/// Returns the cylindric coordinates of the vector given in spheric coordinates
			/// </summary>
			/// <param name="rho">Spheric radius</param>
			/// <param name="phi">Azimuth angle</param>
			/// <param name="th">Polar angle</param>
			/// <returns>Cylindric coordinates as (r, phi, z)</returns>
			public static Vector3 SphericToCylindric(float rho, float phi, float th)
			{
				return new Vector3(
					rho * MathF.Sin(th),
					phi,
					rho * MathF.Cos(th)
					);
			}

			#endregion STATIC
		}

		/// <summary>
		/// Representation of a vector in a N dimensional space
		/// </summary>
		public struct VectorN
		{
			public readonly int Size;

			/// <summary>
			/// Returns the coordinate on the given axis. Example: x for 0, z for 2, w for 3...
			/// </summary>
			public float this[int index]
			{
				get => values[index];
				set
				{
					values[index] = value;
				}
			}

			private float[] values;

			public VectorN(params float[] pValues)
			{
				values = pValues == null ? new float[4] : pValues;
				Size = values.Length;
			}

			public VectorN(int size)
			{
				values = new float[size];
				Size = size;
			}

			#region OPERATORS

			private static VectorN Operate(VectorN vector1, VectorN vector2, Func<float, float, float> operation)
			{
				if (vector1.Size != vector2.Size)
					throw new InvalidOperationException("Both vectors must have the same dimensions.");

				int dimension = vector1.Size;

				float[] lValues = new float[dimension];

				for (int i = 0; i < dimension; i++)
				{
					lValues[i] = operation(vector1[i], vector2[i]);
				}

				return new VectorN(lValues);
			}

			private static VectorN Operate(VectorN vector, float scalar, Func<float, float, float> operation)
			{
				int dimension = vector.Size;

				float[] lValues = new float[dimension];

				for (int i = 0; i < dimension; i++)
				{
					lValues[i] = operation(vector[i], scalar);
				}

				return new VectorN(lValues);
			}

			public static VectorN operator +(VectorN vector1, VectorN vector2)
			{
				return Operate(vector1, vector2, delegate(float n1, float n2)
				{
					return n1 + n2;
				});
			}

			public static VectorN operator -(VectorN vector1, VectorN vector2)
			{
				return Operate(vector1, vector2, delegate(float n1, float n2)
				{
					return n1 - n2;
				});
			}

			public static VectorN operator *(VectorN vector1, VectorN vector2)
			{
				return Operate(vector1, vector2, delegate (float n1, float n2)
				{
					return n1 * n2;
				});
			}

			public static VectorN operator *(VectorN vector, float scalar)
			{
				return Operate(vector, scalar, delegate (float n1, float n2)
				{
					return n1 * n2;
				});
			}

			public static VectorN operator *(float scalar, VectorN vector)
			{
				return vector * scalar;
			}

			public static VectorN operator /(VectorN vector1, VectorN vector2)
			{
				return Operate(vector1, vector2, delegate (float n1, float n2)
				{
					return n1 / n2;
				});
			}

			public static VectorN operator /(VectorN vector, float scalar)
			{
				return Operate(vector, scalar, delegate (float n1, float n2)
				{
					return n1 / n2;
				});
			}

			private static bool IsEqual(VectorN vector1, VectorN vector2, bool equality)
			{
				if (vector1.Size == vector2.Size)
				{
					for (int i = 0; i < vector1.Size; i++)
					{
						if (vector1[i] != vector2[i])
							return !equality;
					}

					return equality;
				}

				return !equality;
			}

			public static bool operator ==(VectorN vector1, VectorN vector2)
			{
				return IsEqual(vector1, vector2, true);
			}

			public static bool operator !=(VectorN vector1, VectorN vector2)
			{
				return IsEqual(vector1, vector2, false);
			}

			#endregion OPERATORS

			#region INSTANCE

			/// <summary>
			/// Returns the vector with absolute values.
			/// </summary>
			public VectorN Abs()
			{
				float[] lValues = new float[Size];

				for (int i = 0; i < Size; i++)
				{
					lValues[i] = MathF.Abs(values[i]);
				}

				return new VectorN(lValues);
			}

			/// <summary>
			/// Rounds up the length of the vector.
			/// </summary>
			public void CeilLength()
			{
				float l = LengthSquared();

				if (l != 0)
				{
					l = MathF.Sqrt(l);
					float ceil = MathF.Ceiling(l);

					for (int i = 0; i < Size; i++)
					{
						values[i] /= l / ceil;
					}
				}
			}

			/// <summary>
			/// Rounds up the values of the vector.
			/// </summary>
			public void CeilValues()
			{
				for (int i = 0; i < Size; i++)
				{
					values[i] = MathF.Ceiling(values[i]);
				}
			}

			/// <summary>
			/// Clamps the length of the vector between min and max.
			/// </summary>
			public void ClampLength(float min, float max)
			{
				float l = LengthSquared();

				if (l != 0)
				{
					l = MathF.Sqrt(l);
					float clamp = Clamp(l, min, max);

					for (int i = 0; i < Size; i++)
					{
						values[i] /= l / clamp;
					}
				}
			}

			/// <summary>
			/// Clamps the values of the vector between the corresponding min and max in range.
			/// </summary>
			public void ClampValues(params (float min, float max)[] range)
			{
				if (range.Length != Size)
					throw new ArgumentException($"Range must have {Size} elements in it (the same dimensions as the vector).");

				for (int i = 0; i < Size; i++)
				{
					values[i] = Clamp(values[i], range[i].min, range[i].max);
				}
			}

			/// <summary>
			/// Clamps the values of the vector between min and max.
			/// </summary>
			public void ClampValuesUniform(float min, float max)
			{
				for (int i = 0; i < Size; i++)
				{
					values[i] = Clamp(values[i], min, max);
				}
			}

			/// <summary>
			/// Returns the cross product between the vector (this) and the given vector.
			/// </summary>
			public VectorN Cross(VectorN vector)
			{
				//if (Dimension != vector.Dimension)
				//	throw new ArgumentException("Both vectors must have the same dimension.");

				//VectorN result = new VectorN(Dimension);
				//float length = MathF.Floor(Dimension / 2f);
				//float coord = 0f;

				//for (int i = 0; i < Dimension; i++)
				//{
				//	coord = 0f;

				//	for (int j = 0; j < length; j++)
				//	{
				//		coord += this[Congruence(i + j, Dimension) + 1] - vector[Congruence()]
				//	}

				//	result[i] = coord;
				//}

				throw new NotImplementedException("Don't use it, it's a work in progress.");
			}

			/// <summary>
			/// Returns the distance between the vector (this) and the given vector.
			/// </summary>
			public float Distance(VectorN vector)
			{
				if (Size != vector.Size)
					throw new ArgumentException("Both vectors must have the same dimension.");

				float l = 0f;

				for (int i = 0; i < Size; i++)
				{
					l += (this[i] - vector[i]) * (this[i] - vector[i]);
				}

				return MathF.Sqrt(l);
			}

			/// <summary>
			/// Returns the squared distance between the vector (this) and the given vector.
			/// </summary>
			public float DistanceSquared(VectorN vector)
			{
				if (Size != vector.Size)
					throw new ArgumentException("Both vectors must have the same dimension.");

				float l = 0f;

				for (int i = 0; i < Size; i++)
				{
					l += (this[i] - vector[i]) * (this[i] - vector[i]);
				}

				return l;
			}

			/// <summary>
			/// Returns the dot product of the vector (this) and the given vector.
			/// </summary>
			public float Dot(VectorN vector)
			{
				if (Size != vector.Size)
					throw new ArgumentException("Both vectors must have the same dimension.");

				float result = 0f;

				for (int i = 0; i < Size; i++)
				{
					result += this[i] * vector[i];
				}

				return result;
			}

			public override bool Equals(object obj)
			{
				if (obj is VectorN)
					return (VectorN)obj == this;
				return false;
			}

			/// <summary>
			/// Rounds the length of the vector downward.
			/// </summary>
			public void FloorLength()
			{
				float l = LengthSquared();

				if (l != 0)
				{
					l = MathF.Sqrt(l);
					float floor = MathF.Floor(l);

					for (int i = 0; i < Size; i++)
					{
						values[i] /= l / floor;
					}
				}
			}

			/// <summary>
			/// Rounds the values of the vector downward.
			/// </summary>
			public void FloorValues()
			{
				for (int i = 0; i < Size; i++)
				{
					values[i] = MathF.Floor(values[i]);
				}
			}

			/// <summary>
			/// Says if the vector is normalized (if the length is equal to 1).
			/// </summary>
			public bool IsNormalized() => LengthSquared() == 1;

			/// <summary>
			/// Lerp between this and to by weight (weight is clamped between 0 and 1).
			/// </summary>
			public VectorN Lerp(VectorN to, float weight)
			{
				return LerpUnclamped(to, Clamp(weight, 0, 1));
			}

			/// <summary>
			/// Lerp between this and to by a random number between 0 and 1.
			/// </summary>
			public VectorN LerpRand(VectorN to)
			{
				return LerpUnclamped(to, (float)new Random().NextDouble());
			}

			/// <summary>
			/// Lerp between this and to by weight.
			/// </summary>
			public VectorN LerpUnclamped(VectorN to, float weight)
			{
				if (Size != to.Size)
					throw new ArgumentException("Both vectors must have the same dimension.");

				VectorN result = new VectorN(Size);

				for (int i = 0; i < Size; i++)
				{
					result[i] = values[i] + weight * (to[i] - values[i]);
				}

				return result;
			}

			/// <summary>
			/// Returns the length of the vector.
			/// </summary>
			public float Length()
			{
				float l = 0f;

				for (int i = 0; i < Size; i++)
				{
					l += values[i] * values[i];
				}

				return MathF.Sqrt(l);
			}

			/// <summary>
			/// Returns the squared length of the vector.
			/// </summary>
			public float LengthSquared()
			{
				float l = 0f;

				for (int i = 0; i < Size; i++)
				{
					l += values[i] * values[i];
				}

				return l;
			}

			/// <summary>
			/// Performs a modulus operation on each value, where the result is in ]-mod, 0].
			/// </summary>
			public VectorN NegMod(float mod)
			{
				VectorN result = new VectorN(Size);

				for (int i = 0; i < Size; i++)
				{
					result[i] = Congruence(values[i], mod, false);
				}

				return result;
			}

			/// <summary>
			/// Performs a modulus operation on each value, where the result is in ]-modv[i], 0].
			/// </summary>
			public VectorN NegModv(VectorN modv)
			{
				if (Size != modv.Size)
					throw new ArgumentException("Both vectors must have the same dimension.");

				VectorN result = new VectorN(Size);

				for (int i = 0; i < Size; i++)
				{
					result[i] = Congruence(values[i], modv[i], false);
				}

				return result;
			}

			/// <summary>
			/// Sets the length of the vector to length.
			/// </summary>
			public void Normalize(float length)
			{
				float l = LengthSquared();

				if (l != 0)
				{
					l = MathF.Sqrt(l);

					for (int i = 0; i < Size; i++)
					{
						values[i] /= l / length;
					}
				}
			}

			/// <summary>
			/// Returns the normalized vector.
			/// </summary>
			public VectorN Normalized()
			{
				VectorN result = new VectorN(Size);
				float l = LengthSquared();

				if (l == 0)
					throw new InvalidOperationException("The vector's length must be greater than 0");

				l = MathF.Sqrt(l);

				for (int i = 0; i < Size; i++)
				{
					result[i] = values[i] / l;
				}

				return result;
			}

			/// <summary>
			/// Performs a modulus operation on each value, where the result is in [0, mod[.
			/// </summary>
			public VectorN PosMod(float mod)
			{
				VectorN result = new VectorN(Size);

				for (int i = 0; i < Size; i++)
				{
					result[i] = Congruence(values[i], mod);
				}

				return result;
			}

			/// <summary>
			/// Performs a modulus operation on each value, where the result is in [0, modv[i][.
			/// </summary>
			public VectorN PosModv(VectorN modv)
			{
				if (Size != modv.Size)
					throw new ArgumentException("Both vectors must have the same dimension.");

				VectorN result = new VectorN(Size);

				for (int i = 0; i < Size; i++)
				{
					result[i] = Congruence(values[i], modv[i]);
				}

				return result;
			}

			/// <summary>
			/// Returns the vector with its values to the power of pow.
			/// </summary>
			public VectorN Pow(float pow)
			{
				VectorN result = new VectorN(Size);

				for (int i = 0; i < Size; i++)
				{
					result[i] = MathF.Pow(values[i], pow);
				}

				return result;
			}

			/// <summary>
			/// Rounds the length of the vector.
			/// </summary>
			public void RoundLength()
			{
				float l = LengthSquared();

				if (l != 0)
				{
					l = MathF.Sqrt(l);
					float round = MathF.Round(l);

					for (int i = 0; i < Size; i++)
					{
						values[i] /= l / round;
					}
				}
			}

			/// <summary>
			/// Rounds the values of the vector.
			/// </summary>
			public void RoundValues()
			{
				for (int i = 0; i < Size; i++)
				{
					values[i] = MathF.Round(values[i]);
				}
			}

			#endregion INSTANCE
		}

		/// <summary>
		/// Representation of a straight line in a 2 dimensional space.
		/// </summary>
		public struct Line2
		{
			public static readonly Line2 AxisX = new Line2(0, 0);
			public static readonly Line2 AxisY = new Line2(new Vector2(0, 0), new Vector2(0, 1));

			public (float a, float b, float c) CartesianForm
			{
				get
				{
					float angle = n.Angle();
					return (MathF.Cos(angle), MathF.Sin(angle), p);
				}
				set
				{
					n = new Vector2(value.a, value.b).Normalized();
					p = value.c;
				}
			}

			public (float phi, float p) NormalForm
			{
				get => (n.Angle(), p);
				set
				{
					n = Vector2.PolarToCartesian(MathF.Cos(value.phi), MathF.Sin(value.phi));
					p = value.p;
				}
			}

			public (float m, float p) SlopeInterceptForm
			{
				get => (n.y / n.x, p / MathF.Sin(n.Angle()));
				set
				{
					n = new Vector2(1, value.m).Normalized();
					p = value.p * MathF.Sin(n.Angle());
				}
			}

			private Vector2 n;
			private float p;

			/// <summary>
			/// The line deduced by two points.
			/// </summary>
			public Line2(Vector2 a, Vector2 b)
			{
				n = a - b;
				p = (b.y - b.x * (n.y / n.x)) * MathF.Sin(n.Angle());
			}

			/// <summary>
			/// The line given in the normal form (xcos(phi) + ysin(phi) - p = 0 with phi the angle of the normal segment).
			/// </summary>
			/// <param name="m">The normal segment</param>
			/// <param name="b">The y-intercept</param>
			public Line2(Vector2 m, float b)
			{
				if (m.LengthSquared() == 0)
					throw new ArgumentOutOfRangeException("m must have length greater than 0.");

				n = m;
				p = b;
			}

			/// <summary>
			/// The line given in the slope-intercept form (ax + b = y).
			/// </summary>
			/// <param name="a">The slope</param>
			/// <param name="b">The y-intercept</param>
			public Line2(float a, float b)
			{
				n = new Vector2(1, a);
				p = b;
			}

			/// <summary>
			/// The line given in cartesian coordinates (ax + by = c).
			/// </summary>
			public Line2(float a, float b, float c)
			{
				n = new Vector2(1, -a / b);
				p = c / b;
			}

			#region OPERATORS

			public static bool operator ==(Line2 line1, Line2 line2) => line1.SlopeInterceptForm == line2.SlopeInterceptForm;

			public static bool operator !=(Line2 line1, Line2 line2) => line1.SlopeInterceptForm != line2.SlopeInterceptForm;

			#endregion OPERATORS

			#region INSTANCE

			/// <summary>
			/// Returns the intersection of two lines. If there's no intersection, the point has <see cref="float.NegativeInfinity"/> as coordinates.
			/// </summary>
			public Vector2 Intersection(Line2 line)
			{
				(float a, float b) line1 = SlopeInterceptForm;
				(float a, float b) line2 = line.SlopeInterceptForm;

				if (line2.a - line1.a == 0 || (line2.b * line1.a) * (line2.a - line1.a) == 0)
					return new Vector2(float.NegativeInfinity, float.NegativeInfinity);

				return new Vector2(
					(line1.b - line2.b) / (line2.a - line1.a),
					(line2.a * line1.b) / ((line2.b * line1.a) * (line2.a - line1.a))
					);
			}

			public override bool Equals(object obj)
			{
				if (obj is Line2)
					return (Line2)obj == this;

				return false;
			}

			/// <summary>
			/// Says if the given line is parallel to this one.
			/// </summary>
			public bool IsParallel(Line2 line)
			{
				return n.Cross(Vector2.PolarToCartesian(1, line.NormalForm.phi)) == 0;
			}

			/// <summary>
			/// Says if the given line is secant to this one.
			/// </summary>
			public bool IsSecant(Line2 line)
			{
				(float a, float b) line1 = SlopeInterceptForm;
				(float a, float b) line2 = line.SlopeInterceptForm;

				return line2.a - line1.a != 0 && (line2.b * line1.a) * (line2.a - line1.a) != 0;
			}

			/// <summary>
			/// Rotates the line by phi radians.
			/// </summary>
			public void Rotate(float phi)
			{
				float angle = n.Angle() + phi;
				n = new Vector2(MathF.Cos(angle), MathF.Sin(angle));
			}

			/// <summary>
			/// Returns the line rotated by phi radians.
			/// </summary>
			public Line2 Rotated(float phi)
			{
				float angle = n.Angle() + phi;
				return new Line2(new Vector2(MathF.Cos(angle), MathF.Sin(angle)), p);
			}

			#endregion INSTANCE
		}

		/// <summary>
		/// Representation of a straight line in a 3 dimensional space.
		/// </summary>
		public struct Line3
		{
			public static readonly Line3 AxisX = new Line3(new Vector3(1, 0, 0), new Vector3(0, 0, 0));
			public static readonly Line3 AxisY = new Line3(new Vector3(0, 1, 0), new Vector3(0, 0, 0));
			public static readonly Line3 AxisZ = new Line3(new Vector3(0, 0, 1), new Vector3(0, 0, 0));

			#region PROPERTIES

			public Vector3 Direction
			{
				get => n;
				set
				{
					n = value;
				}
			}

			public Vector3 Origin
			{
				get => p;
				set
				{
					p = new Vector3(0, value.y - value.x * n.y, value.z - value.x * n.z);
				}
			}

			public (float x, float a) ParametricX
			{
				get => (p.x, n.x);
				set
				{
					n.x = value.a;
					p = new Vector3(0, p.y - value.x * n.y, p.z - value.x * n.z);
				}
			}

			public (float y, float b) ParametricY
			{
				get => (p.y, n.y);
				set
				{
					n.y = value.b;
					p.y = value.y;
				}
			}

			public (float z, float c) ParametricZ
			{
				get => (p.z, n.z);
				set
				{
					n.z = value.c;
					p.z = value.z;
				}
			}

			#endregion PROPERTIES

			private Vector3 n;
			private Vector3 p;

			public Line3(Vector3 direction, Vector3 point)
			{
				if (direction.LengthSquared() == 0)
					throw new ArgumentOutOfRangeException("distance must have a length greater than 0.");

				n = direction;
				p = new Vector3(0, point.y - point.x * n.y, point.z - point.x * n.z);
			}

			#region OPERATORS

			public static bool operator ==(Line3 line1, Line3 line2) => line1.Direction == line2.Direction && line1.Origin == line2.Origin;

			public static bool operator !=(Line3 line1, Line3 line2) => line1.Direction != line2.Direction || line1.Origin != line2.Origin;

			#endregion OPERATORS

			#region INSTANCE

			/// <summary>
			/// Returns the point on the line to the given value (it's calculated from the parametric equation of the line).
			/// </summary>
			public Vector3 GetPoint(float t)
			{
				return new Vector3(p.x + n.x * t, p.y + n.y * t, p.z + n.z * t);
			}

			public override bool Equals(object obj)
			{
				if (obj is Line3)
					return (Line3)obj == this;

				return false;
			}

			/// <summary>
			/// Returns the intersection between this and the given line (if there's no intersection, the point's values are set to negative infinity).
			/// </summary>
			public Vector3 Intersection(Line3 line)
			{
				float t = (line.ParametricX.a * (p.y - line.ParametricY.y) - line.ParametricY.b * (p.x - line.ParametricX.x)) /
					(n.x * line.ParametricY.b - line.ParametricX.a * n.y);
				float T = (n.x * (line.ParametricY.y - p.y) - n.y * (line.ParametricX.x - p.x)) /
					(line.ParametricX.a * n.y - n.x * line.ParametricY.b);

				if (p.z + n.z * t != line.ParametricZ.z + line.ParametricZ.c * T)
					return new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

				return GetPoint(t);
			}

			/// <summary>
			/// Says if both lines are on the same plan.
			/// </summary>
			public bool IsCoplanar(Line3 line) => IsParallel(line) || IsSecant(line);

			/// <summary>
			/// Says if both lines are parallel.
			/// </summary>
			public bool IsParallel(Line3 line)
			{
				return n.x / line.Direction.x == n.y / line.Direction.y && n.z / line.Direction.z == n.z / line.Direction.z;
			}

			/// <summary>
			/// Says if both lines are secant.
			/// </summary>
			public bool IsSecant(Line3 line)
			{
				return Intersection(line) != new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
			}

			#endregion INSTANCE
		}

		/// <summary>
		/// Representation of a straight line in a N dimensional space.
		/// </summary>
		public struct LineN
		{
			public static LineN AxisW = new LineN(new VectorN(0, 0, 0, 1), new VectorN(0, 0, 0, 0));

			public readonly int Size;

			public VectorN Direction
			{
				get => n;
				set
				{
					if (value.Size == Size)
						n = value;
				}
			}

			public VectorN Origin
			{
				get => p;
				set
				{
					if (value.Size == Size)
					{
						p[0] = 0f;

						for (int i = 1; i < Size; i++)
						{
							p[i] = value[i] - value[0] * n[i];
						}
					}
				}
			}

			private VectorN n;
			private VectorN p;

			public LineN(VectorN direction, VectorN origin)
			{
				if (direction.Size != origin.Size)
					throw new ArgumentException("The direction and the origin must have the same Size.");

				Size = direction.Size;
				n = direction;
				p = origin;
			}

			#region OPERATORS

			public static bool operator ==(LineN line1, LineN line2)
			{
				if (line1.Size == line2.Size)
				{
					return line1.Direction == line2.Direction && line1.Origin == line2.Origin;
				}

				return false;
			}

			public static bool operator !=(LineN line1, LineN line2)
			{
				if (line1.Size == line2.Size)
				{
					return line1.Direction != line2.Direction || line1.Origin != line2.Origin;
				}

				return true;
			}

			#endregion OPERATORS

			#region INSTANCE

			public override bool Equals(object obj)
			{
				if (obj is LineN)
					return (LineN)obj == this;

				return false;
			}

			/// <summary>
			/// Returns the point at the given value (the point is calculated with parametric equations).
			/// </summary>
			public VectorN GetPoint(float t)
			{
				float[] coordinates = new float[Size];

				for (int i = 0; i < Size; i++)
				{
					coordinates[i] = p[i] + n[i] * t;
				}

				return new VectorN(coordinates);
			}

			/// <summary>
			/// Says if both lines are parallel.
			/// </summary>
			public bool IsParallel(LineN line)
			{
				if (line.Size != Size)
					throw new ArgumentException("Both lines must have the same Size.");

				float lRatio = n[0] / line.Direction[0];

				for (int i = 1; i < Size; i++)
				{
					if (n[i] / line.Direction[i] != lRatio)
						return false;
				}

				return true;
			}

			#endregion INSTANCE
		}

		/// <summary>
		/// Representation of a circle.
		/// </summary>
		public struct Circle
		{
			public static readonly Circle TRIGONOMETRIC = new Circle(new Vector2(0, 0), 1f);

			#region PROPERTIES

			public Vector2 Origin
			{
				get => o;
				set
				{
					o = value;
				}
			}

			public float Radius
			{
				get => r;
				set
				{
					if (value <= 0)
						throw new ArgumentOutOfRangeException("The radius must be greater than 0.");

					r = value;
				}
			}

			public float Diameter
			{
				get => 2f * r;
				set
				{
					if (value <= 0)
						throw new ArgumentOutOfRangeException("The diameter must be greater than 0.");

					r = value / 2f;
				}
			}

			public float Area
			{
				get => MathF.PI * r * r;
				set
				{
					r = MathF.Sqrt(value / MathF.PI);
				}
			}

			public float Perimeter
			{
				get => 2f * MathF.PI * r;
				set
				{
					r = value / (2f * MathF.PI);
				}
			}

			#endregion PROPERTIES

			private Vector2 o;
			private float r;

			public Circle(Vector2 origin, float radius)
			{
				o = origin;
				r = radius;
			}

			#region OPERATOR

			public static bool operator ==(Circle circle1, Circle circle2)
			{
				return circle1.Origin == circle2.Origin && circle1.Radius == circle2.Radius;
			}

			public static bool operator !=(Circle circle1, Circle circle2)
			{
				return circle1.Origin != circle2.Origin || circle1.Radius != circle2.Radius;
			}

			#endregion OPERATOR

			#region INSTANCE

			/// <summary>
			/// Returns the intersection points between the given circle and this circle.
			/// </summary>
			public List<Vector2> CircleIntersect(Circle circle)
			{
				List<Vector2> lPoints = new List<Vector2>();

				float z = (o.x - circle.Origin.x) / (o.y - circle.Origin.y);
				float n = (circle.Radius * circle.Radius - r * r - circle.Origin.x * circle.Origin.x + o.x * o.x - circle.Origin.y * circle.Origin.y + o.y * o.y) /
					(2f * (o.y - circle.Origin.y));
				float a = 1f + z * z;
				float b = 2f * (o.y - n) * z * -o.x;
				float c = o.x * o.x + (o.y - n) * (o.y - n) - r * r;

				float delta = b * b - 4 * a * c;

				if (delta >= 0)
				{
					float x;
					float sqrtd = MathF.Sqrt(delta);

					for (int i = 0; i < (delta == 0 ? 1 : 2); i++)
					{
						x = (-b - sqrtd) / (2f * a);
						lPoints.Add(new Vector2(x, MathF.Sqrt(r - (o.x - x) * (o.x - x)) + o.y));
					}
				}

				return lPoints;
			}

			/// <summary>
			/// Says if the given point is on the circle
			/// </summary>
			public bool Contains(Vector3 point)
			{
				return (point.x - o.x) * (point.x - o.x) + (point.y - o.y) * (point.y - o.y) == r * r;
			}

			public override bool Equals(object obj)
			{
				if (obj is Circle)
					return (Circle)obj == this;

				return false;
			}

			/// <summary>
			/// Returns the point on the circle at the given angle.
			/// </summary>
			/// <param name="angle">The angle of the point in radians.</param>
			public Vector2 GetPoint(float angle) => Vector2.PolarToCartesian(r, angle) + o;

			/// <summary>
			/// Returns a list of points corresponding to an arc on the circle.
			/// </summary>
			/// <param name="minAngle">The starting angle of the arc in radians.</param>
			/// <param name="maxAngle">The ending angle of the arc in radians (if it's greater than minAngle the arc is clockwise).</param>
			/// <param name="nPoints">The number of points wanted on the arc.</param>
			public List<Vector2> GetArc(float minAngle, float maxAngle, int nPoints = 100)
			{
				List<Vector2> lPoints = new List<Vector2>(nPoints);

				for (int i = 0; i < nPoints; i++)
				{
					lPoints.Add(Vector2.PolarToCartesian(r, minAngle + (maxAngle - minAngle) * (i / (nPoints - 1))));
				}

				return lPoints;
			}

			/// <summary>
			/// Returns the intersection points between the given line and the circle.
			/// </summary>
			public List<Vector2> LineIntersect(Line2 line)
			{
				List<Vector2> lPoints = new List<Vector2>();

				(float m, float p) lLine = line.SlopeInterceptForm;
				float a = lLine.m * lLine.m + 1;
				float b = 2f * lLine.m * (lLine.p - o.y) - 2f * o.x;
				float c = o.x * o.x + (lLine.p - o.y) * (lLine.p - o.y) - r * r;
				float delta = (b * b) - (4f * a * c);

				if (delta >= 0)
				{
					float x;
					float sqrtd = MathF.Sqrt(delta);

					for (int i = 0; i < (delta == 0 ? 1 : 2); i++)
					{
						x = (-b + (i % 2 == 0 ? -sqrtd : sqrtd)) / (2f * a);
						lPoints.Add(new Vector2(x, lLine.m * x + lLine.p));
					}
				}

				return lPoints;
			}

			/// <summary>
			/// Returns the equation of the circle
			/// </summary>
			public override string ToString()
			{
				return $"(x - {o.x})² + (y - {o.y})² = {r * r}";
			}

			#endregion INSTANCE
		}

		/// <summary>
		/// Representation of a sphere.
		/// </summary>
		public struct Sphere
		{
			public static readonly Sphere UNITY = new Sphere(new Vector3(0, 0, 0), 1);

			#region PROPERTIES

			public Vector3 Origin
			{
				get => o;
				set
				{
					o = value;
				}
			}

			public float Radius
			{
				get => r;
				set
				{
					if (value < 0)
						throw new ArgumentOutOfRangeException("Radius must be greater than 0.");

					r = value;
				}
			}

			public float Diameter
			{
				get => 2f * r;
				set
				{
					if (value < 0)
						throw new ArgumentOutOfRangeException("Diameter must be greater than 0.");
				}
			}

			public float Area
			{
				get => 4f * MathF.PI * r * r;
				set
				{
					r = MathF.Sqrt(value / (4f * MathF.PI));
				}
			}

			public float Volume
			{
				get => (4f * MathF.PI * r * r * r) / 3f;
				set
				{
					r = MathF.Cbrt((value * 3f) / (4f * MathF.PI));
				}
			}

			#endregion PROPERTIES

			private Vector3 o;
			private float r;

			public Sphere(Vector3 origin, float radius)
			{
				o = origin;
				r = radius;
			}

			#region OPERATORS

			public static bool operator==(Sphere sphere1, Sphere sphere2) => sphere1.Origin == sphere2.Origin && sphere1.Radius == sphere2.Radius;

			public static bool operator!=(Sphere sphere1, Sphere sphere2) => sphere1.Origin != sphere2.Origin || sphere1.Radius != sphere2.Radius;

			#endregion OPERATORS

			#region INSTANCE

			public bool AreAntipodal(Vector3 p1, Vector3 p2)
			{
				if (Contains(p1) && Contains(p2))
					return (p1 - p2).Length() == Diameter;

				return false;
			}

			public bool Contains(Vector3 point)
			{
				return (point - o).LengthSquared() == r * r;
			}

			public override bool Equals(object obj)
			{
				if (obj is Sphere)
					return (Sphere)obj == this;

				return false;
			}

			public Vector3 GetPoint(float phi, float th)
			{
				return Vector3.SphericToCartesian(r, phi, th);
			}

			public override string ToString()
			{
				return $"(x - {Origin.x})² + (y - {Origin.y})² + (z - {Origin.z})² = {Radius}²";
			}

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

		/// <summary>
		/// Linearly interpolates between to values by a normalized ratio (clamped between 0 and 1).
		/// </summary>
		public static float Lerp(float a, float b, float ratio) => LerpUnclamped(a, b, Math.Clamp(ratio, 0, 1));

		/// <summary>
		/// Linearly interpolates between to values by a random normalized ratio (between 0 and 1).
		/// </summary>
		public static float LerpRand(float a, float b) => LerpUnclamped(a, b, (float)new Random().NextDouble());

		/// <summary>
		/// Linearly interpolates between to values by a given ratio.
		/// </summary>
		public static float LerpUnclamped(float a, float b, float ratio) => a + (b - a) * ratio;

		/// <summary>
		/// Returns a to the power of -b.
		/// </summary>
		public static float NegPow(float a, int b)
		{
			float pow = 1f;

			for (int i = 0; i < b; i++)
			{
				pow /= a;
			}

			return a;
		}

		/// <summary>
		/// Returns a to the power of b.
		/// </summary>
		public static float PosPow(float a, int b)
		{
			float pow = 1f;

			for (int i = 0; i < b; i++)
			{
				pow *= a;
			}

			return pow;
		}

		/// <summary>
		/// Returns a to the power of b.
		/// </summary>
		public static float Pow(float a, int b)
		{
			return b < 0 ? NegPow(a, -b) : PosPow(a, b);
		}
	}
}
