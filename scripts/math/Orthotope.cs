using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Surbon.CSUtils.Math
{
	#pragma warning disable CS0659 // Le type se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
	#pragma warning disable CS0661 // Le type définit l'opérateur == ou l'opérateur != mais ne se substitue pas à Object.GetHashCode()
	#pragma warning disable CS1591 // Commentaire XML manquant pour le type ou le membre visible publiquement

	public struct Orthotope
	{
		public int Dimension => o.Size;

		/// <summary>
		/// The length in the given dimension.
		/// </summary>
		public float this[int dimension]
		{
			get
			{
				if (dimension < 0 || dimension >= Dimension)
					throw new ArgumentOutOfRangeException($"The index must be greater than or equal to zero and lesser than {Dimension}");

				return l[dimension];
			}
			set
			{
				if (dimension < 0 || dimension >= Dimension)
					throw new ArgumentOutOfRangeException($"The index must be greater than or equal to zero and lesser than {Dimension}");

				if (value < 0)
					throw new ArgumentOutOfRangeException("The length must be greater than 0.");

				l[dimension] = value;
			}
		}

		public VectorN Origin
		{
			get => o;
			set
			{
				if (value.Size != Dimension)
					throw new ArgumentException($"The vector must be in {Dimension} dimensions.");
			}
		}

		private VectorN o;
		private float[] l;

		public Orthotope(VectorN origin, params float[] lengths)
		{
			if (origin.Size != lengths.Length)
				throw new ArgumentException($"There are {lengths.Length} dimensions but the origin is in {origin.Size} dimensions.");

			o = origin;
			l = lengths;
		}
	}
}
