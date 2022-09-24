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
		/// <summary>
		/// Shortand for writing Line2(0, 0).
		/// </summary>
		public static Line2 AxisX => new Line2(0, 0);
		/// <summary>
		/// Shortand for writing Line2(Vector2(0, 0), Vector2(1, 0)).
		/// </summary>
		public static Line2 AxisY => new Line2(new Vector2(0, 0), new Vector2(0, 1));

		#region PROPERTIES

		/// <summary>
		/// The <see cref="Line2"/> as ax + by + c = 0.
		/// </summary>
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

		/// <summary>
		/// The <see cref="Line2"/> given with it's angle in radians and it's origin.
		/// </summary>
		public (float phi, float p) NormalForm
		{
			get => (n.Angle(), p);
			set
			{
				n = Vector2.PolarToCartesian(MathF.Cos(value.phi), MathF.Sin(value.phi));
				p = value.p;
			}
		}

		/// <summary>
		/// The <see cref="Line2"/> given as mx + p = y.
		/// </summary>
		public (float m, float p) SlopeInterceptForm
		{
			get => (n.y / n.x, p / MathF.Sin(n.Angle()));
			set
			{
				n = new Vector2(1, value.m).Normalized();
				p = value.p * MathF.Sin(n.Angle());
			}
		}

		#endregion PROPERTIES

		private Vector2 n;
		private float p;

		/// <summary>
		/// Creates a <see cref="Line2"/> with two <see cref="Vector2"/>.
		/// </summary>
		public Line2(Vector2 a, Vector2 b)
		{
			n = a - b;
			p = (b.y - b.x * (n.y / n.x)) * MathF.Sin(n.Angle());
		}

		/// <summary>
		/// Creates a <see cref="Line2"/> with its values set to the given <see cref="Line2"/>.
		/// </summary>
		/// <param name="line"></param>
		public Line2(Line2 line)
		{
			(float phi, float p) normal = line.NormalForm;
			n = Vector2.PolarToCartesian(1, normal.phi);
			p = normal.p;
		}

		/// <summary>
		/// Creates a <see cref="Line2"/> with its normal form (xcos(phi) + ysin(phi) - p = 0 with phi the angle of the normal segment in radians).
		/// </summary>
		/// <param name="m">The normal segment</param>
		/// <param name="b">The y-intercept</param>
		public Line2(Vector2 m, float b)
		{
			if (m.LengthSquared() == 0)
				throw new ArgumentOutOfRangeException("m must have a length greater than 0.");

			n = m;
			p = b;
		}

		/// <summary>
		/// Creates a <see cref="Line2"/> with the slope-intercept form (ax + b = y).
		/// </summary>
		/// <param name="a">The slope</param>
		/// <param name="b">The y-intercept</param>
		public Line2(float a, float b)
		{
			n = new Vector2(1, a);
			p = b;
		}

		/// <summary>
		/// Creates a <see cref="Line2"/> with the cartesian coordinates (ax + by = c).
		/// </summary>
		public Line2(float a, float b, float c)
		{
			n = new Vector2(1, -a / b);
			p = c / b;
		}

		#region OPERATORS

		/// <summary>
		/// Says if both <see cref="Line2"/> have the same values.
		/// </summary>
		public static bool operator ==(Line2 line1, Line2 line2) => line1.SlopeInterceptForm == line2.SlopeInterceptForm;

		/// <summary>
		/// Says if both <see cref="Line2"/> have different values.
		/// </summary>
		public static bool operator !=(Line2 line1, Line2 line2) => line1.SlopeInterceptForm != line2.SlopeInterceptForm;

		#endregion OPERATORS

		#region INSTANCE

		/// <summary>
		/// Returns the intersection of two <see cref="Line2"/>. If there's no intersection, the <see cref="Vector2"/> has <see cref="float.NegativeInfinity"/> as coordinates.
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
		/// Says if the given <see cref="Line2"/> is parallel to this one.
		/// </summary>
		public bool IsParallel(Line2 line) => n.Cross(Vector2.PolarToCartesian(1, line.NormalForm.phi)) == 0;

		/// <summary>
		/// Says if the given <see cref="Line2"/> is secant to this one.
		/// </summary>
		public bool IsSecant(Line2 line)
		{
			(float a, float b) line1 = SlopeInterceptForm;
			(float a, float b) line2 = line.SlopeInterceptForm;

			return line2.a - line1.a != 0 && (line2.b * line1.a) * (line2.a - line1.a) != 0;
		}

		/// <summary>
		/// Rotates the <see cref="Line2"/> by phi radians.
		/// </summary>
		public void Rotate(float phi) => n.Rotate(phi);

		/// <summary>
		/// Returns the <see cref="Line2"/> rotated by phi radians.
		/// </summary>
		public Line2 Rotated(float phi) => new Line2(n.Rotated(phi), p);

		public override string ToString() => $"{SlopeInterceptForm.m}x + {SlopeInterceptForm.p} = y";

		#endregion INSTANCE
	}
}
