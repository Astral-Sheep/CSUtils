using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Surbon.CSUtils.Math
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
		public static float Lerp(float a, float b, float ratio) => LerpUnclamped(a, b, Clamp(ratio, 0, 1));

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
