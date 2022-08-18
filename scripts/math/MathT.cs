using System;
using System.Collections.Generic;

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
					if (value < 0)
						throw new ArgumentOutOfRangeException("The area must be greater than 0.");

					r = MathF.Sqrt(value / MathF.PI);
				}
			}

			public float Perimeter
			{
				get => 2f * MathF.PI * r;
				set
				{
					if (value < 0)
						throw new ArgumentOutOfRangeException("The perimeter must be greater than 0.");

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
		/// Representation of a sphere in a 3 dimensional space.
		/// </summary>
		public struct Sphere
		{
			public static readonly Sphere UNIT = new Sphere(new Vector3(0, 0, 0), 1);

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

					r = value / 2f;
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

			public static bool operator ==(Sphere sphere1, Sphere sphere2) => sphere1.Origin == sphere2.Origin && sphere1.Radius == sphere2.Radius;

			public static bool operator !=(Sphere sphere1, Sphere sphere2) => sphere1.Origin != sphere2.Origin || sphere1.Radius != sphere2.Radius;

			#endregion OPERATORS

			#region INSTANCE

			/// <summary>
			/// Says if to points are antipodal on this circle.
			/// </summary>
			public bool AreAntipodal(Vector3 p1, Vector3 p2)
			{
				if (Contains(p1) && Contains(p2))
					return (p1 - p2).Length() == Diameter;

				return false;
			}

			/// <summary>
			/// Says if the point is on the sphere.
			/// </summary>
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

			/// <summary>
			/// Returns the point on the circle with the given angles.
			/// </summary>
			/// <param name="phi">The azimuth angle.</param>
			/// <param name="th">The polar angle</param>
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
		/// Representation of a sphere in a N dimensional space.
		/// </summary>
		public struct NSphere
		{
			public readonly int Size;

			#region PROPERTIES

			public VectorN Origin
			{
				get => o;
				set
				{
					if (value.Size != o.Size)
						throw new ArgumentException("Both vectors must have the same dimensions.");

					o = value;
				}
			}

			public float Radius
			{
				get => r;
				set
				{
					if (value < 0)
						throw new ArgumentOutOfRangeException("The radius must be greater than 0.");

					r = value;
				}
			}

			public float Diameter
			{
				get => 2f * r;
				set
				{
					if (value < 0)
						throw new ArgumentOutOfRangeException("The diameter must be greater than 0.");

					r = value / 2f;
				}
			}

			public float Area
			{
				get
				{
					if (Size % 2 == 1)
					{
						float lSum = 0;

						for (int i = 2; i <= Size - 1; i += 2)
						{
							lSum += i;
						}

						return (PosPow(2f * MathF.PI, (Size + 1) / 2) * PosPow(r, Size)) / lSum;
					}
					else
					{
						float lSum = 0;

						for (int i = 1; i <= Size - 1; i += 2)
						{
							lSum += i;
						}

						return (2f * PosPow(2f * MathF.PI, Size / 2) * PosPow(r, Size)) / lSum;
					}
				}
				set
				{
					if (value < 0)
						throw new ArgumentOutOfRangeException("The area must be greater than 0.");

					if (Size % 2 == 0)
					{
						float lSum = 0;

						for (int i = 2; i <= Size - 1; i += 2)
						{
							lSum += i;
						}

						r = NRoot((value * lSum) / PosPow(2f * MathF.PI, Size / 2), Size);
					}
				}
			}

			public float Volume
			{
				get
				{
					if (Size % 2 == 0)
					{
						float lSum = 0;

						for (int i = 2; i <= Size; i += 2)
						{
							lSum += i;
						}

						return (PosPow(2f * MathF.PI, Size / 2) * PosPow(r, Size)) / lSum;
					}
					else
					{
						float lSum = 0;

						for (int i = 1; i <= Size; i += 2)
						{
							lSum += i;
						}

						return (2f * PosPow(2f * MathF.PI, (Size - 1) / 2) * PosPow(r, Size)) / lSum;
					}
				}
				set
				{
					if (value < 0)
						throw new ArgumentOutOfRangeException("The volume must be greater than 0.");

					if (Size % 2 == 0)
					{
						float lSum = 0;

						for (int i = 2; i <= Size; i += 2)
						{
							lSum += i;
						}

						r = NRoot((value * lSum) / PosPow(2f * MathF.PI, Size / 2), Size);
					}
					else
					{
						float lSum = 0;

						for (int i = 1; i < Size; i += 2)
						{
							lSum += i;
						}

						r = NRoot((value * lSum) / (2f * PosPow(2f * MathF.PI, (Size - 1) / 2)), Size);
					}
				}
			}

			#endregion PROPERTIES

			private VectorN o;
			private float r;

			public NSphere(VectorN origin, float radius)
			{
				o = origin;
				r = radius;
				Size = o.Size;
			}

			#region OPERATORS

			public static bool operator ==(NSphere sphere1, NSphere sphere2) => sphere1.Origin == sphere2.Origin && sphere1.Radius == sphere2.Radius;

			public static bool operator !=(NSphere sphere1, NSphere sphere2) => sphere1.Origin != sphere2.Origin || sphere1.Radius != sphere2.Radius;

			#endregion OPERATORS

			#region INSTANCE

			/// <summary>
			/// Says if the n-sphere contains the given point.
			/// </summary>
			public bool Contains(VectorN vector)
			{
				if (vector.Size != Size)
					throw new ArgumentOutOfRangeException("The point must be in the same dimension as the sphere.");

				return (vector - o).LengthSquared() == r * r;
			}

			public override bool Equals(object obj)
			{
				if (obj is NSphere)
					return (NSphere)obj == this;

				return false;
			}

			#endregion INSTANCE
		}

		/// <summary>
		/// Representation of a rectangle in a 2 dimensional space.
		/// </summary>
		public struct Rectangle
		{
			#region PROPERTIES

			public float Width
			{
				get => w;
				set
				{
					if (value < 0)
						throw new ArgumentOutOfRangeException("The width must be greater than 0.");

					w = value;
				}
			}

			public float Height
			{
				get => h;
				set
				{
					if (value < 0)
						throw new ArgumentOutOfRangeException("The width must be greater than 0.");

					h = value;
				}
			}

			public Vector2 Origin
			{
				get => o;
				set { o = value; }
			}

			public float Perimeter => 2f * w + 2f * h;

			public float Area => w * h;

			#endregion PROPERTIES

			private Vector2 o;
			private float w;
			private float h;

			public Rectangle(Vector2 origin, float width, float height)
			{
				o = origin;
				w = width;
				h = height;
			}

			#region OPERATORS

			public static bool operator ==(Rectangle rect1, Rectangle rect2)
			{
				return rect1.Origin == rect2.Origin && rect1.Width == rect2.Width && rect1.Height == rect2.Height;
			}

			public static bool operator !=(Rectangle rect1, Rectangle rect2)
			{
				return rect1.Origin != rect2.Origin || rect1.Width != rect2.Width || rect1.Height != rect2.Height;
			}

			#endregion OPERATORS

			#region INSTANCE

			public override bool Equals(object obj)
			{
				if (obj is Rectangle)
					return (Rectangle)obj == this;

				return false;
			}

			/// <summary>
			/// Says if the given point is in the boundaries of the rectangle.
			/// </summary>
			public bool IsIn(Vector2 point) => point.x >= o.x && point.x <= o.x + w && point.y >= o.y && point.y <= o.y + h;

			/// <summary>
			/// Says if the given point is on the rectangle.
			/// </summary>
			public bool Has(Vector2 point)
			{
				return (point.x >= o.x && point.x <= o.x + w && (point.y == o.y || point.y == o.y + h)) || (point.y >= o.y && point.y <= o.y + h && (point.x == o.x || point.x == o.x + w));
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
		/// Returns the factorial of the given number.
		/// </summary>
		public static int Factorial(int value)
		{
			if (value < 0)
				throw new ArgumentOutOfRangeException("The value must be greater than 0.");

			int lFact = 1;

			for (int i = 1; i <= value; i++)
			{
				lFact *= i;
			}

			return lFact;
		}

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
		/// Returns the nth root of the given number.
		/// </summary>
		public static float NRoot(float value, float n) => MathF.Exp((1f / n) * MathF.Log(value));

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
