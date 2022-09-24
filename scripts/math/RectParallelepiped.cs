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

		/// <summary>
		/// The width of the <see cref="RectParallelepiped"/> (on the x axis).
		/// </summary>
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

		/// <summary>
		/// The height of the <see cref="RectParallelepiped"/> (on the y axis).
		/// </summary>
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

		/// <summary>
		/// The length of the <see cref="RectParallelepiped"/> (on the z axis).
		/// </summary>
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

		/// <summary>
		/// The <see cref="Vector3"/> on the front top left of the <see cref="RectParallelepiped"/>.
		/// </summary>
		public Vector3 Origin
		{
			get => o;
			set { o = value; }
		}

		/// <summary>
		/// The surface of the <see cref="RectParallelepiped"/>.
		/// </summary>
		public float Area => 2f * w * h + l * (w + h);

		/// <summary>
		/// The volume of the <see cref="RectParallelepiped"/>.
		/// </summary>
		public float Volume => w * h * l;

		#endregion PROPERTIES

		private Vector3 o;
		private float w;
		private float h;
		private float l;

		/// <summary>
		/// Creates a <see cref="RectParallelepiped"/> with the given width, height and length, and with its top left point on the given origin.
		/// </summary>
		public RectParallelepiped(Vector3 origin, float width, float height, float length)
		{
			o = origin;
			w = width;
			h = height;
			l = length;
		}

		/// <summary>
		/// Creates a <see cref="RectParallelepiped"/> with its values set to the values of the given <see cref="RectParallelepiped"/>.
		/// </summary>
		public RectParallelepiped(RectParallelepiped rectParallelepiped)
		{
			o = rectParallelepiped.Origin;
			w = rectParallelepiped.Width;
			h = rectParallelepiped.Height;
			l = rectParallelepiped.Length;
		}

		#region OPERATORS

		/// <summary>
		/// Says if both <see cref="RectParallelepiped"/> have the same values.
		/// </summary>
		public static bool operator==(RectParallelepiped rect1, RectParallelepiped rect2) => rect1.Origin == rect2.Origin &&
			rect1.Width == rect2.Width &&
			rect1.Height == rect2.Height &&
			rect1.Length == rect2.Length;

		/// <summary>
		/// Says if both <see cref="RectParallelepiped"/> have different values.
		/// </summary>
		public static bool operator!=(RectParallelepiped rect1, RectParallelepiped rect2) => rect1.Origin != rect2.Origin ||
			rect1.Width != rect2.Width ||
			rect1.Height != rect2.Height ||
			rect1.Length != rect2.Length;

		#endregion OPERATORS

		#region INSTANCE

		public override bool Equals(object obj) => (obj is RectParallelepiped rect) && (rect == this);

		/// <summary>
		/// Says if the given <see cref="Vector3"/> is within the <see cref="RectParallelepiped"/>.
		/// </summary>
		public bool IsIn(Vector3 point) => point.x >= o.x && point.x <= o.x + w &&
			point.y >= o.y && point.y <= o.y + h &&
			point.z >= o.z && point.z <= o.z + l;

		/// <summary>
		/// Says if the given <see cref="Vector3"/> is on one of the <see cref="RectParallelepiped"/>'s faces.
		/// </summary>
		public bool Has(Vector3 point) =>
			((point.x == o.x || point.x == o.x + w) && point.y >= o.y && point.y <= o.y + h && point.z >= o.z && point.z <= o.z + l) ||
				((point.y == o.y || point.y == o.y + h) && point.x >= o.x && point.x <= o.x + w && point.z >= o.z && point.z <= o.z + l) ||
				((point.z == o.z || point.z == o.z + l) && point.x >= o.x && point.x <= o.x + w && point.y >= o.y && point.y <= o.y + h);

		public override string ToString() => $"Origin : {o} | Width : {w} | Height {h} | Length {l}";

		#endregion INSTANCE
	}
}
