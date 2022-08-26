using System;

namespace Com.Surbon.CSUtils.Math
{
	#pragma warning disable CS0659 // Le type se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
	#pragma warning disable CS0661 // Le type définit l'opérateur == ou l'opérateur != mais ne se substitue pas à Object.GetHashCode()
	#pragma warning disable CS1591 // Commentaire XML manquant pour le type ou le membre visible publiquement

	/// <summary>
	/// Representation of a straight line in a 3 dimensional space.
	/// </summary>
	public struct Line3
	{
		public static readonly Line3 AxisX = new Line3(new Vector3(1, 0, 0), new Vector3(0, 0, 0));
		public static readonly Line3 AxisY = new Line3(new Vector3(0, 1, 0), new Vector3(0, 0, 0));
		public static readonly Line3 AxisZ = new Line3(new Vector3(0, 0, 1), new Vector3(0, 0, 0));

		#region PROPERTIES

		/// <summary>
		/// The direction of the line as a vector.
		/// </summary>
		public Vector3 Direction
		{
			get => n;
			set
			{
				n = value;
			}
		}

		/// <summary>
		/// The point of the line with x = 0.
		/// </summary>
		public Vector3 Origin
		{
			get => p;
			set
			{
				p = new Vector3(0, value.y - value.x * n.y, value.z - value.x * n.z);
			}
		}

		/// <summary>
		/// The constants of the x line in the parametric equation.
		/// </summary>
		public (float x, float a) ParametricX
		{
			get => (p.x, n.x);
			set
			{
				n.x = value.a;
				p = new Vector3(0, p.y - value.x * n.y, p.z - value.x * n.z);
			}
		}

		/// <summary>
		/// The constants of the y line in the parametric equation.
		/// </summary>
		public (float y, float b) ParametricY
		{
			get => (p.y, n.y);
			set
			{
				n.y = value.b;
				p.y = value.y;
			}
		}

		/// <summary>
		/// The constants of the z line in the parametric equation.
		/// </summary>
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

		public override string ToString() => $"Origin : {p} | Direction : {n}";

		#endregion INSTANCE
	}
}
