using System;

namespace Com.Surbon.CSUtils.Math
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
}
