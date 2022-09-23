using System;
using System.Collections.Generic;

namespace Com.Surbon.CSUtils.Math
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

		/// <summary>
		/// The center of the circle.
		/// </summary>
		public Vector2 Origin
		{
			get => o;
			set
			{
				o = value;
			}
		}

		/// <summary>
		/// The radius of the circle.
		/// </summary>
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

		/// <summary>
		/// The diameter of the circle.
		/// </summary>
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

		/// <summary>
		/// The surface of the circle.
		/// </summary>
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

		/// <summary>
		/// The perimeter of the circle.
		/// </summary>
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

		public Circle(Circle circle)
		{
			o = circle.Origin;
			r = circle.Radius;
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

			if (o.y - circle.Origin.y == 0)
				throw new DivideByZeroException("The subtraction of the origin of both circle has 0 as y.");

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
		public bool Contains(Vector3 point) => (point.x - o.x) * (point.x - o.x) + (point.y - o.y) * (point.y - o.y) == r * r;

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
		public override string ToString() => $"(x - {o.x})² + (y - {o.y})² = {r}²";

		#endregion INSTANCE
	}
}
