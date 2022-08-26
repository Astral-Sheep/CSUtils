using System;

namespace Com.Surbon.CSUtils.Math
{
	#pragma warning disable CS0659 // Le type se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
	#pragma warning disable CS0661 // Le type définit l'opérateur == ou l'opérateur != mais ne se substitue pas à Object.GetHashCode()
	#pragma warning disable CS1591 // Commentaire XML manquant pour le type ou le membre visible publiquement

	/// <summary>
	/// Representation of a straight line in a N dimensional space.
	/// </summary>
	public struct LineN
	{
		public static LineN AxisW = new LineN(new VectorN(0, 0, 0, 1), new VectorN(0, 0, 0, 0));

		public readonly int Dimension;

		/// <summary>
		/// The direction of the line as a vector.
		/// </summary>
		public VectorN Direction
		{
			get => n;
			set
			{
				if (value.Size == Dimension)
					n = value;
			}
		}

		/// <summary>
		/// The point of the line with x = 0.
		/// </summary>
		public VectorN Origin
		{
			get => p;
			set
			{
				if (value.Size == Dimension)
				{
					p[0] = 0f;

					for (int i = 1; i < Dimension; i++)
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

			Dimension = direction.Size;
			n = direction;
			p = origin;
		}

		#region OPERATORS

		public static bool operator ==(LineN line1, LineN line2)
		{
			if (line1.Dimension == line2.Dimension)
			{
				return line1.Direction == line2.Direction && line1.Origin == line2.Origin;
			}

			return false;
		}

		public static bool operator !=(LineN line1, LineN line2)
		{
			if (line1.Dimension == line2.Dimension)
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
			VectorN point = new VectorN(Dimension);

			for (int i = 0; i < Dimension; i++)
			{
				point[i] = p[i] + n[i] * t;
			}

			return point;
		}

		/// <summary>
		/// Says if both lines are parallel.
		/// </summary>
		public bool IsParallel(LineN line)
		{
			if (line.Dimension != Dimension)
				throw new ArgumentException("Both lines must have the same Size.");

			float lRatio = n[0] / line.Direction[0];

			for (int i = 1; i < Dimension; i++)
			{
				if (n[i] / line.Direction[i] != lRatio)
					return false;
			}

			return true;
		}

		public override string ToString() => $"Origin : {p} | Direction : {n}";

		#endregion INSTANCE
	}
}
