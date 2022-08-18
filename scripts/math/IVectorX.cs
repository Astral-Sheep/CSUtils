using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Surbon.CSUtils.Math
{
	#pragma warning disable CS1591 // Commentaire XML manquant pour le type ou le membre visible publiquement

	public interface IVectorX<T>
	{
		float AngleTo(T vector);
		void Rotate(float angle);
		T Rotated(float angle);
	}
}
