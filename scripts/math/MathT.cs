using System;

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
