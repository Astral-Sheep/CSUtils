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
		/// <summary>
		/// Shorthand for writing Vector3(0, 0, -1).
		/// </summary>
		public static Vector3 Back => new Vector3(0, 0, -1);
		/// <summary>
		/// Shorthand for writing Vector3(0, -1, 0).
		/// </summary>
		public static Vector3 Down => new Vector3(0, -1, 0);
		/// <summary>
		/// Shorthand for writing Vector3(0, 0, 1).
		/// </summary>
		public static Vector3 Front => new Vector3(0, 0, 1);
		/// <summary>
		/// Shorthand for writing Vector3(-1, 0, 0).
		/// </summary>
		public static Vector3 Left => new Vector3(-1, 0, 0);
		/// <summary>
		/// Shorthand for writing Vector3(-1, -1, -1).
		/// </summary>
		public static Vector3 NegOne => new Vector3(-1, -1, -1);
		/// <summary>
		/// Shorthand for writing Vector3(1, 1, 1).
		/// </summary>
		public static Vector3 One => new Vector3(1, 1, 1);
		/// <summary>
		/// Shorthand for writing Vector3(1, 0, 0).
		/// </summary>
		public static Vector3 Right => new Vector3(1, 0, 0);
		/// <summary>
		/// Shorthand for writing Vector3(0, 1, 0).
		/// </summary>
		public static Vector3 Up => new Vector3(0, 1, 0);
		/// <summary>
		/// Shorthand for writing Vector3(0, 0, 0).
		/// </summary>
		public static Vector3 Zero => new Vector3(0, 0, 0);

		/// <summary>
		/// Represents the angle types in a 3-dimensional space.
		/// </summary>
		public enum AngleType
		{
			AZIMUTHAL = 0,
			POLAR = 1
		}

		/// <summary>
		/// Represents the axis in a 3-dimensional space.
		/// </summary>
		public enum Axis
		{
			X = 0,
			Y = 1,
			Z = 2
		}

		/// <summary>
		/// The position of the <see cref="Vector3"/> on the x axis.
		/// </summary>
		public float x;
		/// <summary>
		/// The position of the <see cref="Vector3"/> on the y axis.
		/// </summary>
		public float y;
		/// <summary>
		/// The position of the <see cref="Vector3"/> on the z axis.
		/// </summary>
		public float z;

		/// <summary>
		/// Creates a <see cref="Vector3"/> as (x, y, z).
		/// </summary>
		public Vector3(float x = 0f, float y = 0f, float z = 0f)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		/// <summary>
		/// Creates a <see cref="Vector3"/> with its values set to the given <see cref="Vector3"/>.
		/// </summary>
		public Vector3(Vector3 vector)
		{
			x = vector.x;
			y = vector.y;
			z = vector.z;
		}

		#region OPERATORS

		/// <summary>
		/// Adds both <see cref="Vector3"/>.
		/// </summary>
		/// <returns>vector1 + vector2.</returns>
		public static Vector3 operator +(Vector3 vector1, Vector3 vector2) => new Vector3(
			vector1.x + vector2.x,
			vector1.y + vector2.y,
			vector1.z + vector2.z
			);

		/// <summary>
		/// Sets the values of the <see cref="Vector3"/> to the opposite values (equivalent to vector * -1).
		/// </summary>
		/// <returns>-vector.</returns>
		public static Vector3 operator -(Vector3 vector) => new Vector3(-vector.x, -vector.y, -vector.z);

		/// <summary>
		/// Subtract the second <see cref="Vector3"/> to the first one.
		/// </summary>
		/// <returns>vector1 - vector2.</returns>
		public static Vector3 operator -(Vector3 vector1, Vector3 vector2) => new Vector3(
			vector1.x - vector2.x,
			vector1.y - vector2.y,
			vector1.z - vector2.z
			);

		/// <summary>
		/// Multiplies the values of the first <see cref="Vector3"/> by the values of the second one.
		/// </summary>
		/// <returns>vector1 * vector2.</returns>
		public static Vector3 operator *(Vector3 vector1, Vector3 vector2) => new Vector3(
			vector1.x * vector2.x,
			vector1.y * vector2.y,
			vector1.z * vector2.z
			);

		/// <summary>
		/// Multiplies the <see cref="Vector3"/> with the <see cref="float"/>.
		/// </summary>
		/// <returns>vector * scalar.</returns>
		public static Vector3 operator *(Vector3 vector, float scalar) => new Vector3(
			vector.x * scalar,
			vector.y * scalar,
			vector.z * scalar
			);

		/// <summary>
		/// Multiplies the <see cref="Vector3"/> with the <see cref="float"/>.
		/// </summary>
		/// <returns>scalar * vector.</returns>
		public static Vector3 operator *(float scalar, Vector3 vector) => vector * scalar;

		/// <summary>
		/// Divides the values of the first <see cref="Vector3"/> by the values of the second one.
		/// </summary>
		/// <returns>vector1 / vector2.</returns>
		public static Vector3 operator /(Vector3 vector1, Vector3 vector2) => new Vector3(
			vector1.x / vector2.x,
			vector1.y / vector2.y,
			vector1.z / vector2.z
			);

		/// <summary>
		/// Divides the <see cref="Vector3"/> by the <see cref="float"/>.
		/// </summary>
		/// <returns>vector / scalar.</returns>
		public static Vector3 operator /(Vector3 vector, float scalar) => new Vector3(
			vector.x / scalar,
			vector.y / scalar,
			vector.z / scalar
			);

		/// <summary>
		/// Says if both <see cref="Vector3"/> have the same values.
		/// </summary>
		public static bool operator ==(Vector3 vector1, Vector3 vector2) => vector1.x == vector2.x &&
			vector1.y == vector2.y &&
			vector1.z == vector2.z;

		/// <summary>
		/// Says if both <see cref="Vector3"/> have different values.
		/// </summary>
		public static bool operator !=(Vector3 vector1, Vector3 vector2) => vector1.x != vector2.x ||
			vector1.y != vector2.y ||
			vector1.z != vector2.z;

		#endregion OPERATORS

		#region INSTANCE
		
		/// <summary>
		/// Returns the <see cref="Vector3"/> with absolute values.
		/// </summary>
		public Vector3 Abs() => new Vector3(MathF.Abs(x), MathF.Abs(y), MathF.Abs(z));

		/// <summary>
		/// Returns the angle corresponding to the given <see cref="AngleType"/>.
		/// </summary>
		/// <returns>The angle in radians.</returns>
		public float Angle(AngleType type)
		{
			switch (type)
			{
				case AngleType.AZIMUTHAL:
					return MathF.Atan2(y, x);
				case AngleType.POLAR:
					return MathF.Atan2(z, new Vector2(x, y).Length());
				default:
					throw new Exception("How tf did you get there?");
			}
		}

		/// <summary>
		/// Returns the angle on the given <see cref="Axis"/>.
		/// </summary>
		/// <returns>The angle in radians.</returns>
		public float Angle(Axis axis)
		{
			switch(axis)
			{
				case Axis.X:
					return MathF.Atan2(y, -z);
				case Axis.Y:
					return MathF.Atan2(x, z);
				case Axis.Z:
					return MathF.Atan2(y, x);
				default:
					throw new Exception("How tf did you get there?");
			}
		}

		/// <summary>
		/// Rounds up the length of the <see cref="Vector3"/>.
		/// </summary>
		public void CeilLength()
		{
			float l = x * x + y * y + z * z;

			if (l != 0)
			{
				l = MathF.Sqrt(l);
				l /= MathF.Ceiling(l);
				x /= l;
				y /= l;
				z /= l;
			}
		}

		/// <summary>
		/// Rounds up the values of the <see cref="Vector3"/>.
		/// </summary>
		public void CeilValues()
		{
			x = MathF.Ceiling(x);
			y = MathF.Ceiling(y);
			z = MathF.Ceiling(z);
		}

		/// <summary>
		/// Clamps the length of the <see cref="Vector3"/> between min and max.
		/// </summary>
		public void ClampLength(float min, float max)
		{
			float l = x * x + y * y + z * z;

			if (l != 0)
			{
				l = MathF.Sqrt(l);
				l /= MathT.Clamp(l, min, max);
				x /= l;
				y /= l;
				z /= l;
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
		public void ClampValuesUniform(float min, float max) => ClampValues(min, max, min, max, min, max);

		/// <summary>
		/// Returns the cross product of the <see cref="Vector3"/> (this) and the given <see cref="Vector3"/>.
		/// </summary>
		public Vector3 Cross(Vector3 vector) => new Vector3(
				y * vector.z - z * vector.y,
				z * vector.x - x * vector.z,
				x * vector.y - y * vector.x
				);

		/// <summary>
		/// Returns the distance between the <see cref="Vector3"/> (this) and the given <see cref="Vector3"/>.
		/// </summary>
		public float Distance(Vector3 vector)
		{
			if (vector == this)
				return 0;

			return MathF.Sqrt((x - vector.x) * (x - vector.x) + (y - vector.y) * (y - vector.y) + (z - vector.z) * (z - vector.z));
		}

		/// <summary>
		/// Returns the distance squared between the <see cref="Vector3"/> (this) and the given <see cref="Vector3"/>.
		/// </summary>
		public float DistanceSquared(Vector3 vector)
		{
			if (vector == this)
				return 0;

			return (x - vector.x) * (x - vector.x) + (y - vector.y) * (y - vector.y) + (z - vector.z) * (z - vector.z);
		}

		/// <summary>
		/// Returns the dot product of the <see cref="Vector3"/> (this) and the given the <see cref="Vector3"/>.
		/// </summary>
		public float Dot(Vector3 vector) => x * vector.x + y * vector.y + z * vector.z;

		public override bool Equals(object obj)
		{
			if (obj is Vector3)
				return (Vector3)obj == this;

			return false;
		}

		/// <summary>
		/// Rounds the length of the <see cref="Vector3"/> downward.
		/// </summary>
		public void FloorLength()
		{
			float l = x * x + y * y + z * z;

			if (l != 0)
			{
				l = MathF.Sqrt(l);
				l /= MathF.Floor(l);
				x /= l;
				y /= l;
				z /= l;
			}
		}

		/// <summary>
		/// Rounds the values of the <see cref="Vector3"/> downward.
		/// </summary>
		public void FloorValues()
		{
			x = MathF.Floor(x);
			y = MathF.Floor(y);
			z = MathF.Floor(z);
		}

		/// <summary>
		/// Says if the <see cref="Vector3"/> is normalized (the length is equal to 1).
		/// </summary>
		public bool IsNormalized() => LengthSquared() == 1;

		/// <summary>
		/// Returns the length of the <see cref="Vector3"/>.
		/// </summary>
		public float Length() => MathF.Sqrt(x * x + y * y + z * z);

		/// <summary>
		/// Returns the squared length of the <see cref="Vector3"/>.
		/// </summary>
		public float LengthSquared() => x * x + y * y + z * z;

		/// <summary>
		/// Lerps the <see cref="Vector3"/> between this and to by weight (weight is clamped between 0 and 1).
		/// </summary>
		public Vector3 Lerp(Vector3 to, float weight) => LerpUnclamped(to, MathT.Clamp(weight, 0, 1));

		/// <summary>
		/// Lerps the <see cref="Vector3"/> between this and to by a random number between 0 and 1.
		/// </summary>
		public Vector3 LerpRand(Vector3 to) => LerpUnclamped(to, (float)new Random().NextDouble());

		/// <summary>
		/// Lerps the <see cref="Vector3"/> between this and to by weight.
		/// </summary>
		public Vector3 LerpUnclamped(Vector3 to, float weight) => new Vector3(
				x + weight * (to.x - x),
				y + weight * (to.y - y),
				z + weight * (to.z - z)
				);

		/// <summary>
		/// Performs a modulus operation on x, y and z, where the result is in ]-mod, 0].
		/// </summary>
		public Vector3 NegMod(float mod) => new Vector3(
			MathT.Mod(x, mod, false),
			MathT.Mod(y, mod, false),
			MathT.Mod(z, mod, false)
			);

		/// <summary>
		/// Performs a modulus operation on x, y and z, where the result is in ]-modv.x, 0] for x, ]-modv.y, 0] for y, and]-modv.z, 0] for z.
		/// </summary>
		public Vector3 NegModv(Vector3 modv) => new Vector3(
			MathT.Mod(x, modv.x, false),
			MathT.Mod(y, modv.y, false),
			MathT.Mod(z, modv.z, false)
			);

		/// <summary>
		/// Sets the length of the <see cref="Vector3"/> to the given length.
		/// </summary>
		public void Normalize(float length = 1f)
		{
			if (length == 0)
				throw new DivideByZeroException("The length must be different from zero.");

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
		/// Returns the <see cref="Vector3"/> with its length set to the given length.
		/// </summary>
		public Vector3 Normalized(float length = 1)
		{
			if (length == 0)
				throw new DivideByZeroException("The length must be different from zero.");

			float l = x * x + y * y + z * z;

			if (l == 0)
				return new Vector3(0, 0, 0);

			l = MathF.Sqrt(l) / length;
			return new Vector3(x / l, y / l, z / l);
		}

		/// <summary>
		/// Performs a modulus operation on x, y and z, where the result is in [0, mod[.
		/// </summary>
		public Vector3 PosMod(float mod) => new Vector3(
			MathT.Mod(x, mod),
			MathT.Mod(y, mod),
			MathT.Mod(z, mod)
			);

		/// <summary>
		/// Performs a modulus operation on x and y, where the result is in [0, modv.x[ for x, [0, modv.y[ for y, and [0, modv.z[ for z.
		/// </summary>
		public Vector3 PosModv(Vector3 modv) => new Vector3(
			MathT.Mod(x, modv.x),
			MathT.Mod(y, modv.y),
			MathT.Mod(z, modv.z)
			);

		/// <summary>
		/// Returns the <see cref="Vector3"/> with x, y and z to the power of pow.
		/// </summary>
		public Vector3 Pow(float pow) => new Vector3(MathF.Pow(x, pow), MathF.Pow(y, pow), MathF.Pow(z, pow));

		/// <summary>
		/// Rotates the <see cref="Vector3"/> by value radians on the given angle
		/// </summary>
		public void Rotate(float phi, AngleType angle)
		{
			if (x * x + y * y + z * z == 0)
				return;

			switch (angle)
			{
				case AngleType.AZIMUTHAL:
					float sin = MathF.Sin(phi);
					float cos = MathF.Cos(phi);
					x = x * sin - y * cos;
					y = x * cos + y * sin;
					break;
				case AngleType.POLAR:
					Vector3 vector = CartesianToSpheric(this);
					vector.z += phi;
					vector = SphericToCartesian(vector);
					x = vector.x;
					y = vector.y;
					z = vector.z;
					break;
				default:
					throw new Exception("How tf did you get there?");
			}
		}

		/// <summary>
		/// Rotates the <see cref="Vector3"/> on the given <see cref="Axis"/> by the given angle.
		/// </summary>
		/// <param name="phi">The angle in radians.</param>
		/// <param name="axis">The axis on which to rotate.</param>
		public void Rotate(float phi, Axis axis)
		{
			if (x * x + y * y + z * z == 0)
				return;

			float angle;
			float length;

			switch(axis)
			{
				case Axis.X:
					angle = MathF.Atan2(y, -z) + phi;
					length = MathF.Sqrt(y * y + z * z);
					z = -MathF.Cos(angle) * length;
					y = MathF.Sin(angle) * length;
					break;
				case Axis.Y:
					angle = MathF.Atan2(x, z) + phi;
					length = MathF.Sqrt(x * x + z * z);
					z = MathF.Cos(angle) * length;
					x = MathF.Sin(angle) * length;
					break;
				case Axis.Z:
					angle = MathF.Atan2(y, x) + phi;
					length = MathF.Sqrt(x * x + y * y);
					x = MathF.Cos(angle) * length;
					y = MathF.Sin(angle) * length;
					break;
				default:
					throw new Exception("How tf did you get there?");
			}
		}

		/// <summary>
		/// Returns the <see cref="Vector3"/> rotated by phi radians on the given <see cref="AngleType"/>.
		/// </summary>
		/// <param name="phi">The angle in radians.</param>
		public Vector3 Rotated(float phi, AngleType angle)
		{
			if (x * x + y * y + z * z == 0)
				return new Vector3();

			Vector3 vector = new Vector3();

			switch (angle)
			{
				case AngleType.AZIMUTHAL:
					float sin = MathF.Sin(phi);
					float cos = MathF.Cos(phi);
					vector.x = x * sin - y * cos;
					vector.y = x * cos + y * sin;
					vector.z = z;
					break;
				case AngleType.POLAR:
					vector = CartesianToSpheric(this);
					vector.z += phi;
					vector = SphericToCartesian(vector);
					break;
				default:
					throw new Exception("How tf did you get there?");
			}

			return vector;
		}

		/// <summary>
		/// Returns the <see cref="Vector3"/> rotated by phi radians on the given <see cref="Axis"/>.
		/// </summary>
		/// <param name="phi">The angle in radians.</param>
		public Vector3 Rotated(float phi, Axis axis)
		{
			if (x * x + y * y + z * z == 0)
				return new Vector3();

			float angle;
			float length;

			switch (axis)
			{
				case Axis.X:
					angle = MathF.Atan2(y, -z) + phi;
					length = MathF.Sqrt(y * y + z * z);
					return new Vector3(x, MathF.Sin(angle) * length, -MathF.Cos(angle) * length);
				case Axis.Y:
					angle = MathF.Atan2(x, z) + phi;
					length = MathF.Sqrt(x * x + z * z);
					return new Vector3(MathF.Sin(angle) * length, y, MathF.Cos(angle) * length);
				case Axis.Z:
					angle = MathF.Atan2(y, x) + phi;
					length = MathF.Sqrt(x * x + y * y);
					return new Vector3(MathF.Cos(angle) * length, MathF.Sin(angle) * length, z);
				default:
					throw new Exception("How tf did you get there?");
			}
		}

		/// <summary>
		/// Rounds the length of the <see cref="Vector3"/>.
		/// </summary>
		public void RoundLength()
		{
			float l = x * x + y * y + z * z;

			if (l != 0)
			{
				l = MathF.Sqrt(l);
				l /= MathF.Round(l);
				x /= l;
				y /= l;
				z /= l;
			}
		}

		/// <summary>
		/// Rounds the values of the <see cref="Vector3"/>.
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

		public override string ToString() => $"({x}, {y}, {z})";

		#endregion INSTANCE

		#region STATIC

		/// <summary>
		/// Returns the cylindric coordinates of the <see cref="Vector3"/> from the cartesian coordinates.
		/// </summary>
		/// <param name="vector">Cartesian coordinates as (x, y, z).</param>
		/// <returns>Cylindric coordinates as (r, phi, z) with phi in radians.</returns>
		public static Vector3 CartesianToCylindric(Vector3 vector) => new Vector3(
			MathF.Sqrt(vector.x * vector.x + vector.y * vector.y),
			MathF.Atan2(vector.y, vector.x),
			vector.z
			);

		/// <summary>
		/// Returns the cylindric coordinates of the <see cref="Vector3"/> given in cartesian coordinates.
		/// </summary>
		/// <param name="x">Cartesian coordinate x.</param>
		/// <param name="y">Cartesian coordinate y.</param>
		/// <param name="z">Cartesian coordinate z.</param>
		/// <returns>Cylindric coordinates as (r, phi, z) with phi in radians.</returns>
		public static Vector3 CartesianToCylindric(float x, float y, float z) => new Vector3(
			MathF.Sqrt(x * x + y * y),
			MathF.Atan2(y, x),
			z
			);

		/// <summary>
		/// Returns the spheric coordinates of the <see cref="Vector3"/> from the cartesian coordinates.
		/// </summary>
		/// <param name="vector">Cartesian coordinates as (x, y, z).</param>
		/// <returns>Spheric coordinates as (rho, phi, th) with phi and th in radians.</returns>
		public static Vector3 CartesianToSpheric(Vector3 vector) => new Vector3(
				vector.Length(),
				MathF.Atan2(vector.y, vector.x),
				MathF.Atan2(vector.z, new Vector2(vector.x, vector.y).Length())
				);

		/// <summary>
		/// Returns the spheric coordinates of the <see cref="Vector3"/> from the cartesian coordinates.
		/// </summary>
		/// <param name="x">Cartesian coordinate x.</param>
		/// <param name="y">Cartesian coordinate y.</param>
		/// <param name="z">Cartesian coordinate z.</param>
		/// <returns>Spheric coordinates as (rho, phi, th) with phi and th in radians.</returns>
		public static Vector3 CartesianToSpheric(float x, float y, float z) => new Vector3(
				new Vector3(x, y, z).Length(),
				MathF.Atan2(y, x),
				MathF.Atan2(z, new Vector2(x, y).Length())
				);

		/// <summary>
		/// Returns the cartesian coordinates of the <see cref="Vector3"/> given in cylindric coordinates.
		/// </summary>
		/// <param name="vector">Cylindric coordinates as (r, phi, z) with phi in radians.</param>
		/// <returns>Cartesian coordinates as (x, y, z).</returns>
		public static Vector3 CylindricToCartesian(Vector3 vector) => new Vector3(
			vector.x * MathF.Cos(vector.y),
			vector.x * MathF.Sin(vector.y),
			vector.z
			);

		/// <summary>
		/// Returns the cartesian coordinates of the <see cref="Vector3"/> given in cylindric coordinates
		/// </summary>
		/// <param name="r">Polar radius.</param>
		/// <param name="phi">Azimuth angle in radians.</param>
		/// <param name="z">Cartesian coordinate z.</param>
		/// <returns>Cartesian coordinates as (x, y, z).</returns>
		public static Vector3 CylindricToCartesian(float r, float phi, float z) => new Vector3(
			r * MathF.Cos(phi),
			r * MathF.Sin(phi),
			z
			);

		/// <summary>
		/// Returns the spheric coordinates of the <see cref="Vector3"/> given in cylindric coordinates.
		/// </summary>
		/// <param name="vector">Cylindric coordinates as (r, phi, z) with phi in radians.</param>
		/// <returns>Spheric coordinates as (rho, phi, th) with phi and th in radians.</returns>
		public static Vector3 CylindricToSpheric(Vector3 vector) => new Vector3(
				new Vector2(vector.x, vector.z).Length(),
				vector.y,
				MathF.Atan2(vector.z, vector.x)
				);

		/// <summary>
		/// Returns the spheric coordinates of the <see cref="Vector3"/> given in cylindric coordinates.
		/// </summary>
		/// <param name="r">Polar radius.</param>
		/// <param name="phi">Azimuth angle in radians.</param>
		/// <param name="z">Cartesian coordinate z.</param>
		/// <returns>Spheric coordinates as (rho, phi, th) with phi and th in radians.</returns>
		public static Vector3 CylindricToSpheric(float r, float phi, float z) => new Vector3(
				new Vector2(r, z).Length(),
				phi,
				MathF.Atan2(z, r)
				);

		/// <summary>
		/// Returns the cartesian coordinates of the <see cref="Vector3"/> given in spheric coordinates.
		/// </summary>
		/// <param name="vector">Spheric coordinates as (rho, phi, th) with phi and th in radians.</param>
		/// <returns>Cartesian coordinates as (x, y, z).</returns>
		public static Vector3 SphericToCartesian(Vector3 vector) => new Vector3(
				vector.x * MathF.Cos(vector.y) * MathF.Sin(vector.z),
				vector.x * MathF.Sin(vector.y) * MathF.Sin(vector.z),
				vector.x * MathF.Cos(vector.z)
				);

		/// <summary>
		/// Returns the cartesian coordinates of the <see cref="Vector3"/> given in spheric coordinates.
		/// </summary>
		/// <param name="rho">Spheric radius.</param>
		/// <param name="phi">Azimuth angle in radians.</param>
		/// <param name="th">Polar angle in radians.</param>
		/// <returns>Cartesian coordinates as (x, y, z).</returns>
		public static Vector3 SphericToCartesian(float rho, float phi, float th) => new Vector3(
				rho * MathF.Cos(phi) * MathF.Sin(th),
				rho * MathF.Sin(phi) * MathF.Sin(th),
				rho * MathF.Cos(th)
				);

		/// <summary>
		/// Returns the cylindric coordinates of the <see cref="Vector3"/> given in spheric coordinates.
		/// </summary>
		/// <param name="vector">Spheric coordinates as (rho, phi, th) with phi and th in radians.</param>
		/// <returns>Cylindric coordinates as (r, phi, z) with phi in radians.</returns>
		public static Vector3 SphericToCylindric(Vector3 vector) => new Vector3(
				vector.x * MathF.Sin(vector.z),
				vector.y,
				vector.x * MathF.Cos(vector.z)
				);

		/// <summary>
		/// Returns the cylindric coordinates of the <see cref="Vector3"/> given in spheric coordinates.
		/// </summary>
		/// <param name="rho">Spheric radius</param>
		/// <param name="phi">Azimuth angle in radians.</param>
		/// <param name="th">Polar angle in radians.</param>
		/// <returns>Cylindric coordinates as (r, phi, z) with phi in radians.</returns>
		public static Vector3 SphericToCylindric(float rho, float phi, float th) => new Vector3(
				rho * MathF.Sin(th),
				phi,
				rho * MathF.Cos(th)
				);

		#endregion STATIC
	}
}
