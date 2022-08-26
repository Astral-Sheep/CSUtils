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

		public RectParallelepiped(Vector3 origin, float width, float height, float length)
		{
			o = origin;
			w = width;
			h = height;
			l = length;
		}

		#region OPERATORS

		public static bool operator==(RectParallelepiped rect1, RectParallelepiped rect2)
		{
			return rect1.Origin == rect2.Origin && rect1.Width == rect2.Width && rect1.Height == rect2.Height && rect1.Length == rect2.Length;
		}

		public static bool operator!=(RectParallelepiped rect1, RectParallelepiped rect2)
		{
			return rect1.Origin != rect2.Origin || rect1.Width != rect2.Width || rect1.Height != rect2.Height || rect1.Length != rect2.Length;
		}

		#endregion OPERATORS

		#region INSTANCE

		public override bool Equals(object obj)
		{
			if (obj is RectParallelepiped)
				return (RectParallelepiped)obj == this;

			return false;
		}

		/// <summary>
		/// Says if the given point is in the parallelepiped.
		/// </summary>
		public bool IsIn(Vector3 point)
		{
			return point.x >= o.x && point.x <= o.x + w && point.y >= o.y && point.y <= o.y + h && point.z >= o.z && point.z <= o.z + l;
		}

		/// <summary>
		/// Says if the given point is on one of the parallelepiped's faces.
		/// </summary>
		public bool Has(Vector3 point)
		{
			return ((point.x == o.x || point.x == o.x + w) && point.y >= o.y && point.y <= o.y + h && point.z >= o.z && point.z <= o.z + l) ||
				((point.y == o.y || point.y == o.y + h) && point.x >= o.x && point.x <= o.x + w && point.z >= o.z && point.z <= o.z + l) ||
				((point.z == o.z || point.z == o.z + l) && point.x >= o.x && point.x <= o.x + w && point.y >= o.y && point.y <= o.y + h);
		}

		public override string ToString() => $"Origin : {o} | Width : {w} | Height {h} | Length {l}";

		#endregion INSTANCE
	}
}
