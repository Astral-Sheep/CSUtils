using System;

namespace Com.Surbon.CSUtils.Math
{
	#pragma warning disable CS0659 // Le type se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
	#pragma warning disable CS0661 // Le type définit l'opérateur == ou l'opérateur != mais ne se substitue pas à Object.GetHashCode()
	#pragma warning disable CS1591 // Commentaire XML manquant pour le type ou le membre visible publiquement

	/// <summary>
	/// Representation of a rectangle in a 2 dimensional space.
	/// </summary>
	public struct Rectangle
	{
		#region PROPERTIES

		/// <summary>
		/// The width of the rectangle (on the x axis).
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
		/// The height of the rectangle (on the y axis).
		/// </summary>
		public float Height
		{
			get => h;
			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException("The width must be greater than 0.");

				h = value;
			}
		}

		/// <summary>
		/// The point on the top left of the rectangle.
		/// </summary>
		public Vector2 Origin
		{
			get => o;
			set { o = value; }
		}

		/// <summary>
		/// The perimeter of the rectangle.
		/// </summary>
		public float Perimeter => 2f * w + 2f * h;

		/// <summary>
		/// The area of the rectangle.
		/// </summary>
		public float Area => w * h;

		#endregion PROPERTIES

		private Vector2 o;
		private float w;
		private float h;

		public Rectangle(Vector2 origin, float width, float height)
		{
			o = origin;
			w = width;
			h = height;
		}

		public Rectangle(Rectangle rectangle)
		{
			o = rectangle.Origin;
			w = rectangle.Width;
			h = rectangle.Height;
		}

		#region OPERATORS

		public static bool operator ==(Rectangle rect1, Rectangle rect2)
		{
			return rect1.Origin == rect2.Origin && rect1.Width == rect2.Width && rect1.Height == rect2.Height;
		}

		public static bool operator !=(Rectangle rect1, Rectangle rect2)
		{
			return rect1.Origin != rect2.Origin || rect1.Width != rect2.Width || rect1.Height != rect2.Height;
		}

		#endregion OPERATORS

		#region INSTANCE

		public override bool Equals(object obj)
		{
			if (obj is Rectangle)
				return (Rectangle)obj == this;

			return false;
		}

		/// <summary>
		/// Says if the given point is in the boundaries of the rectangle.
		/// </summary>
		public bool IsIn(Vector2 point) => point.x >= o.x && point.x <= o.x + w && point.y >= o.y && point.y <= o.y + h;

		/// <summary>
		/// Says if the given point is on the rectangle.
		/// </summary>
		public bool Has(Vector2 point)
		{
			return (point.x >= o.x && point.x <= o.x + w && (point.y == o.y || point.y == o.y + h)) ||
				(point.y >= o.y && point.y <= o.y + h && (point.x == o.x || point.x == o.x + w));
		}

		public override string ToString() => $"Origin : {o} | Width : {w} | Height : {h}";

		#endregion INSTANCE
	}
}
