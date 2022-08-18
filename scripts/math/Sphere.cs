﻿using System;

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
		public static readonly Sphere UNIT = new Sphere(new Vector3(0, 0, 0), 1);

		#region PROPERTIES

		public Vector3 Origin
		{
			get => o;
			set
			{
				o = value;
			}
		}

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

		public float Area
		{
			get => 4f * MathF.PI * r * r;
			set
			{
				r = MathF.Sqrt(value / (4f * MathF.PI));
			}
		}

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

		public Sphere(Vector3 origin, float radius)
		{
			o = origin;
			r = radius;
		}

		#region OPERATORS

		public static bool operator ==(Sphere sphere1, Sphere sphere2) => sphere1.Origin == sphere2.Origin && sphere1.Radius == sphere2.Radius;

		public static bool operator !=(Sphere sphere1, Sphere sphere2) => sphere1.Origin != sphere2.Origin || sphere1.Radius != sphere2.Radius;

		#endregion OPERATORS

		#region INSTANCE

		/// <summary>
		/// Says if to points are antipodal on this circle.
		/// </summary>
		public bool AreAntipodal(Vector3 p1, Vector3 p2)
		{
			if (Contains(p1) && Contains(p2))
				return (p1 - p2).Length() == Diameter;

			return false;
		}

		/// <summary>
		/// Says if the point is on the sphere.
		/// </summary>
		public bool Contains(Vector3 point)
		{
			return (point - o).LengthSquared() == r * r;
		}

		public override bool Equals(object obj)
		{
			if (obj is Sphere)
				return (Sphere)obj == this;

			return false;
		}

		/// <summary>
		/// Returns the point on the circle with the given angles.
		/// </summary>
		/// <param name="phi">The azimuth angle.</param>
		/// <param name="th">The polar angle</param>
		public Vector3 GetPoint(float phi, float th)
		{
			return Vector3.SphericToCartesian(r, phi, th);
		}

		public override string ToString()
		{
			return $"(x - {Origin.x})² + (y - {Origin.y})² + (z - {Origin.z})² = {Radius}²";
		}

		#endregion INSTANCE
	}
}