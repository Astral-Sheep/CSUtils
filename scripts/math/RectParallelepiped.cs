using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Surbon.CSUtils.Math
{
	#pragma warning disable CS0659 // Le type se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
	#pragma warning disable CS0661 // Le type définit l'opérateur == ou l'opérateur != mais ne se substitue pas à Object.GetHashCode()
	#pragma warning disable CS1591 // Commentaire XML manquant pour le type ou le membre visible publiquement

	/// <summary>
	/// Representation of a rectangular parallelepiped.
	/// </summary>
	public struct RectParallelepiped
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
					throw new ArgumentOutOfRangeException("The height must be greater than 0.");

				h = value;
			}
		}

		public float Length
		{
			get => l;
			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException("The length must be greater than 0.");

				l = value;
			}
		}

		public Vector3 Origin
		{
			get => o;
			set { o = value; }
		}

		public float Area => 2f * w * h + l * (w + h);

		public float Volume => w * h * l;

		#endregion PROPERTIES

		private Vector3 o;
		private float w;
		private float h;
		private float l;
	}
}
