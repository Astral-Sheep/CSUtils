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

		/// <summary>
		/// Representation of a vector in a 3 dimensional space
		/// </summary>
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

			public VectorN(int dimension)
			{
				values = new float[dimension];
				Size = dimension;
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
