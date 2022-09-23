using System;

namespace Com.Surbon.CSUtils.Math
{
	#pragma warning disable CS0659 // Le type se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
	#pragma warning disable CS0661 // Le type définit l'opérateur == ou l'opérateur != mais ne se substitue pas à Object.GetHashCode()
	#pragma warning disable CS1591 // Commentaire XML manquant pour le type ou le membre visible publiquement

	/// <summary>
	/// Representation of a sphere in a N dimensional space.
	/// </summary>
	public struct NSphere
	{
		/// <summary>
		/// The dimension of the sphere.
		/// </summary>
		public readonly int Dimension;

		#region PROPERTIES

		/// <summary>
		/// The center of the sphere.
		/// </summary>
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

		/// <summary>
		/// The radius of the sphere.
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
		/// The diameter of the sphere.
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
		/// The surface of the sphere.
		/// </summary>
		public float Area
		{
			get
			{
				if (Dimension % 2 == 1)
				{
					float lSum = 0;

					for (int i = 2; i <= Dimension - 1; i += 2)
					{
						lSum += i;
					}

					return (MathT.PosPow(2f * MathF.PI, (Dimension + 1) / 2) * MathT.PosPow(r, Dimension)) / lSum;
				}
				else
				{
					float lSum = 0;

					for (int i = 1; i <= Dimension - 1; i += 2)
					{
						lSum += i;
					}

					return (2f * MathT.PosPow(2f * MathF.PI, Dimension / 2) * MathT.PosPow(r, Dimension)) / lSum;
				}
			}
			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException("The area must be greater than 0.");

				if (Dimension % 2 == 0)
				{
					float lSum = 0;

					for (int i = 2; i <= Dimension - 1; i += 2)
					{
						lSum += i;
					}

					r = MathT.NRoot((value * lSum) / MathT.PosPow(2f * MathF.PI, Dimension / 2), Dimension);
				}
			}
		}

		/// <summary>
		/// The volume of the sphere.
		/// </summary>
		public float Volume
		{
			get
			{
				if (Dimension % 2 == 0)
				{
					float lSum = 0;

					for (int i = 2; i <= Dimension; i += 2)
					{
						lSum += i;
					}

					return (MathT.PosPow(2f * MathF.PI, Dimension / 2) * MathT.PosPow(r, Dimension)) / lSum;
				}
				else
				{
					float lSum = 0;

					for (int i = 1; i <= Dimension; i += 2)
					{
						lSum += i;
					}

					return (2f * MathT.PosPow(2f * MathF.PI, (Dimension - 1) / 2) * MathT.PosPow(r, Dimension)) / lSum;
				}
			}
			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException("The volume must be greater than 0.");

				if (Dimension % 2 == 0)
				{
					float lSum = 0;

					for (int i = 2; i <= Dimension; i += 2)
					{
						lSum += i;
					}

					r = MathT.NRoot((value * lSum) / MathT.PosPow(2f * MathF.PI, Dimension / 2), Dimension);
				}
				else
				{
					float lSum = 0;

					for (int i = 1; i < Dimension; i += 2)
					{
						lSum += i;
					}

					r = MathT.NRoot((value * lSum) / (2f * MathT.PosPow(2f * MathF.PI, (Dimension - 1) / 2)), Dimension);
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
			Dimension = o.Size;
		}

		public NSphere(NSphere nSphere)
		{
			Dimension = nSphere.Dimension;
			o = nSphere.Origin;
			r = nSphere.Radius;
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
			if (vector.Size != Dimension)
				throw new ArgumentOutOfRangeException("The point must be in the same dimension as the sphere.");

			return (vector - o).LengthSquared() == r * r;
		}

		public override bool Equals(object obj)
		{
			if (obj is NSphere)
				return (NSphere)obj == this;

			return false;
		}

		public override string ToString() => $"Origin : {o} | Radius : {r}";

		#endregion INSTANCE
	}
}
