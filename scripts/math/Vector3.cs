using System;

namespace Com.Surbon.CSUtils.Math
{
	#pragma warning disable CS0659 // Le type se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
	#pragma warning disable CS0661 // Le type définit l'opérateur == ou l'opérateur != mais ne se substitue pas à Object.GetHashCode()
	#pragma warning disable CS1591 // Commentaire XML manquant pour le type ou le membre visible publiquement

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

		public static bool operator ==(Vector3 vector1, Vector3 vector2)
		{
			return vector1.x == vector2.x && vector1.y == vector2.y && vector1.z == vector2.z;
		}

		public static bool operator !=(Vector3 vector1, Vector3 vector2)
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
				float clamped = MathT.Clamp(l, min, max);
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
			x = MathT.Clamp(x, minX, maxX);
			y = MathT.Clamp(y, minY, maxY);
			z = MathT.Clamp(z, minZ, maxZ);
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
			return LerpUnclamped(to, MathT.Clamp(weight, 0, 1));
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
			return new Vector3(MathT.Congruence(x, mod, false), MathT.Congruence(y, mod, false), MathT.Congruence(z, mod, false));
		}

		/// <summary>
		/// Performs a modulus operation on x, y and z, where the result is in ]-modv.x, 0] for x, ]-modv.y, 0] for y, and]-modv.z, 0] for z.
		/// </summary>
		public Vector3 NegModv(Vector3 modv)
		{
			return new Vector3(MathT.Congruence(x, modv.x, false), MathT.Congruence(y, modv.y, false), MathT.Congruence(z, modv.z, false));
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
			return new Vector3(MathT.Congruence(x, mod), MathT.Congruence(y, mod), MathT.Congruence(z, mod));
		}

		/// <summary>
		/// Performs a modulus operation on x and y, where the result is in [0, modv.x[ for x, [0, modv.y[ for y, and [0, modv.z[ for z.
		/// </summary>
		public Vector3 PosModv(Vector3 modv)
		{
			return new Vector3(MathT.Congruence(x, modv.x), MathT.Congruence(y, modv.y), MathT.Congruence(z, modv.z));
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
}
