using System;

namespace Com.Surbon.CSUtils.Math
{
	#pragma warning disable CS0659 // Le type se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
	#pragma warning disable CS0661 // Le type définit l'opérateur == ou l'opérateur != mais ne se substitue pas à Object.GetHashCode()
	#pragma warning disable CS1591 // Commentaire XML manquant pour le type ou le membre visible publiquement

	/// <summary>
	/// Representation of a sphere in a 3 dimensional space.
	/// </summary>
	public struct Sphere
	{
		/// <summary>
		/// Shorthand for writing Sphere(Vector3(0, 0, 0), 1).
		/// </summary>
		public static Sphere UNIT => new Sphere(new Vector3(0, 0, 0), 1);

		#region PROPERTIES

		/// <summary>
		/// The center of the <see cref="Sphere"/>.
		/// </summary>
		public Vector3 Origin
		{
			get => o;
			set
			{
				o = value;
			}
		}

		/// <summary>
		/// The radius of the <see cref="Sphere"/>.
		/// </summary>
		public float Radius
		{
			get => r;
			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException("Radius must be greater than 0.");

				r = value;
			}
		}

		/// <summary>
		/// The diamater of the <see cref="Sphere"/>.
		/// </summary>
		public float Diameter
		{
			get => 2f * r;
			set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException("Diameter must be greater than 0.");

				r = value / 2f;
			}
		}

		/// <summary>
		/// The surface of the <see cref="Sphere"/>.
		/// </summary>
		public float Area
		{
			get => 4f * MathF.PI * r * r;
			set
			{
				r = MathF.Sqrt(value / (4f * MathF.PI));
			}
		}

		/// <summary>
		/// The volume of the <see cref="Sphere"/>.
		/// </summary>
		public float Volume
		{
			get => (4f * MathF.PI * r * r * r) / 3f;
			set
			{
				r = MathF.Cbrt((value * 3f) / (4f * MathF.PI));
			}
		}

		#endregion PROPERTIES

		private Vector3 o;
		private float r;

		/// <summary>
		/// Creates a <see cref="Sphere"/> of the given radius at the given origin.
		/// </summary>
		public Sphere(Vector3 origin, float radius)
		{
			o = origin;
			r = radius;
		}

		/// <summary>
		/// Creates a <see cref="Sphere"/> with its values se to the values of the given <see cref="Sphere"/>.
		/// </summary>
		public Sphere(Sphere sphere)
		{
			o = sphere.Origin;
			r = sphere.Radius;
		}

		#region OPERATORS

		/// <summary>
		/// Says if both <see cref="Sphere"/> have the same values.
		/// </summary>
		public static bool operator ==(Sphere sphere1, Sphere sphere2) => sphere1.Origin == sphere2.Origin && sphere1.Radius == sphere2.Radius;

		/// <summary>
		/// Says if both <see cref="Sphere"/> have different values.
		/// </summary>
		public static bool operator !=(Sphere sphere1, Sphere sphere2) => sphere1.Origin != sphere2.Origin || sphere1.Radius != sphere2.Radius;

		#endregion OPERATORS

		#region INSTANCE

		/// <summary>
		/// Says if to <see cref="Vector3"/> are antipodal on this <see cref="Sphere"/>.
		/// </summary>
		public bool AreAntipodal(Vector3 p1, Vector3 p2) => Contains(p1) && Contains(p2) ? (p1 - p2).Length() == Diameter : false;

		/// <summary>
		/// Says if the <see cref="Vector3"/> is on the <see cref="Sphere"/>.
		/// </summary>
		public bool Contains(Vector3 point) => (point - o).LengthSquared() == r * r;

		public override bool Equals(object obj)
		{
			if (obj is Sphere)
				return (Sphere)obj == this;

			return false;
		}

		/// <summary>
		/// Returns the <see cref="Vector3"/> on the <see cref="Sphere"/> with the given angles.
		/// </summary>
		/// <param name="phi">The azimuth angle in radians.</param>
		/// <param name="th">The polar angle in radians.</param>
		public Vector3 GetPoint(float phi, float th) => o + Vector3.SphericToCartesian(r, phi, th);

		public override string ToString() => $"(x - {Origin.x})² + (y - {Origin.y})² + (z - {Origin.z})² = {Radius}²";

		#endregion INSTANCE
	}
}
