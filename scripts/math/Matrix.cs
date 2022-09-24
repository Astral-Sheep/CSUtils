using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Surbon.CSUtils.Math
{
	#pragma warning disable CS0659 // Le type se substitue à Object.Equals(object o) mais pas à Object.GetHashCode()
	#pragma warning disable CS0661 // Le type définit l'opérateur == ou l'opérateur != mais ne se substitue pas à Object.GetHashCode()
	#pragma warning disable CS1591 // Commentaire XML manquant pour le type ou le membre visible publiquement

	/// <summary>
	/// Representation of a matrix
	/// </summary>
	public struct Matrix
	{
		#region PROPERTIES

		/// <summary>
		/// The number of columns in the <see cref="Matrix"/>.
		/// </summary>
		public int Columns
		{
			get => _columns;
			private set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException("The number of columns must be greater than 0.");

				_columns = value;
			}
		}

		/// <summary>
		/// The number of rows in the <see cref="Matrix"/>.
		/// </summary>
		public int Rows
		{
			get => _rows;
			private set
			{
				if (value < 0)
					throw new ArgumentOutOfRangeException("The number of rows must be greater than 0.");

				_rows = value;
			}
		}

		/// <summary>
		/// The value at the given line and column
		/// </summary>
		public float this[int line, int column]
		{
			get
			{
				if (line < 0 || line >= Rows)
					throw new ArgumentOutOfRangeException($"The row must greater than or equal to 0 and lesser than {Rows}");

				if (column < 0 || column >= Columns)
					throw new ArgumentOutOfRangeException($"The column must greater than or equal to 0 and lesser than {Columns}");

				return values[line, column];
			}
			set
			{
				if (line < 0 || line >= Rows)
					throw new ArgumentOutOfRangeException($"The row must greater than or equal to 0 and lesser than {Rows}");
				
				if (column < 0 || column >= Columns)
					throw new ArgumentOutOfRangeException($"The column must greater than or equal to 0 and lesser than {Columns}");

				values[line, column] = value;
			}
		}

		#endregion PROPERTIES

		private int _columns;
		private int _rows;
		private float[,] values;

		/// <summary>
		/// Creates a <see cref="Matrix"/> lines x columns with the given <see cref="float"/>.
		/// </summary>
		public Matrix(int lines, int columns, params float[] numbers)
		{
			_rows = lines;
			_columns = columns;

			if (numbers.Length != lines * columns)
				throw new TypeInitializationException("The number of rows and columns doesn't correspond to the number of values given.",
					new ArgumentException());

			values = new float[lines, columns];
			int index = 0;

			for (int i = 0; i < lines; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					values[i, j] = numbers[index];
					++index;
				}
			}
		}

		/// <summary>
		/// Creates a <see cref="Matrix"/> lines x columns with its values set to 0.
		/// </summary>
		public Matrix(int lines, int columns)
		{
			_rows = lines;
			_columns = columns;
			values = new float[lines, columns];
		}

		/// <summary>
		/// Creates a <see cref="Matrix"/> with its values set to the values of the given <see cref="Matrix"/>.
		/// </summary>
		public Matrix(Matrix matrix)
		{
			_rows = matrix.Rows;
			_columns = matrix.Columns;

			values = new float[_rows, _columns];

			for (int i = 0; i < _rows; i++)
			{
				for (int j = 0; j < _columns; j++)
				{
					values[i, j] = matrix[i, j];
				}
			}
		}

		#region OPERATOR

		/// <summary>
		/// Adds both <see cref="Matrix"/>.
		/// </summary>
		/// <returns>m1 + m2.</returns>
		public static Matrix operator+(Matrix m1, Matrix m2)
		{
			if (!m1.SameSize(m2))
				throw new ArgumentException("Both matrices must have the same size.");

			Matrix matrix = new Matrix(m1.Rows, m1.Columns);

			for (int i = 0; i < m1.Rows; i++)
			{
				for (int j = 0; j < m1.Columns; j++)
				{
					matrix[i, j] = m1[i, j] + m2[i, j];
				}
			}

			return matrix;
		}

		/// <summary>
		/// Subtract the second <see cref="Matrix"/> to the first one.
		/// </summary>
		/// <returns>m1 - m2.</returns>
		public static Matrix operator-(Matrix m1, Matrix m2)
		{
			if (!m1.SameSize(m2))
				throw new ArgumentException("Both matrices must have the same size.");

			Matrix matrix = new Matrix(m1.Rows, m1.Columns);

			for (int i = 0; i < m1.Rows; i++)
			{
				for (int j = 0; j < m1.Columns; j++)
				{
					matrix[i, j] = m1[i, j] - m2[i, j];
				}
			}

			return matrix;
		}

		/// <summary>
		/// Multiplies the <see cref="Matrix"/> with the <see cref="float"/>.
		/// </summary>
		/// <returns>scalar * m.</returns>
		public static Matrix operator*(float scalar, Matrix m)
		{
			Matrix matrix = new Matrix(m.Rows, m.Columns);

			for (int i = 0; i < m.Rows; i++)
			{
				for (int j = 0; j < m.Columns; j++)
				{
					matrix[i, j] = scalar * m[i, j];
				}
			}

			return matrix;
		}

		/// <summary>
		/// Multiplies the <see cref="Matrix"/> with the <see cref="float"/>.
		/// </summary>
		/// <returns>m * scalar.</returns>
		public static Matrix operator *(Matrix m, float scalar)
		{
			Matrix matrix = new Matrix(m.Rows, m.Columns);

			for (int i = 0; i < m.Rows; i++)
			{
				for (int j = 0; j < m.Columns; j++)
				{
					matrix[i, j] = m[i, j] * scalar;
				}
			}

			return matrix;
		}

		/// <summary>
		/// Multiplies both <see cref="Matrix" />.
		/// </summary>
		/// <returns>m1 * m2.</returns>
		public static Matrix operator*(Matrix m1, Matrix m2)
		{
			if (m1.Columns != m2.Rows)
				throw new ArgumentException("The number of columns of the first matrix must be equal to the number of rows in the second matrix.");

			Matrix matrix = new Matrix(m1.Rows, m2.Columns);
			float n = 0;

			for (int i = 0; i < matrix.Rows; i++)
			{
				for (int j = 0; j < matrix.Columns; j++)
				{
					n = 0;

					for (int k = 0; k < m1.Columns; k++)
					{
						n += m1[i, k] * m2[k, j];
					}

					matrix[i, j] = n;
				}
			}

			return matrix;
		}

		/// <summary>
		/// Divides the <see cref="Matrix"/> by the <see cref="float"/>.
		/// </summary>
		/// <returns>m / scalar.</returns>
		public static Matrix operator/(Matrix m, float scalar)
		{
			Matrix matrix = new Matrix(m.Rows, m.Columns);

			for (int i = 0; i < m.Rows; i++)
			{
				for (int j = 0; j < m.Columns; j++)
				{
					matrix[i, j] = m[i, j] / scalar;
				}
			}

			return matrix;
		}

		/// <summary>
		/// Multiplies the first <see cref="Matrix"/> by the invert of the second <see cref="Matrix"/>.
		/// </summary>
		/// <returns>m1 * m2^(-1)</returns>
		public static Matrix operator/(Matrix m1, Matrix m2)
		{
			return m1 * m2.Inverted();
		}

		/// <summary>
		/// Says if both <see cref="Matrix"/> have the same values.
		/// </summary>
		public static bool operator==(Matrix m1, Matrix m2)
		{
			if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
				return false;

			for (int i = 0; i < m1.Rows; i++)
			{
				for (int j = 0; j < m1.Columns; j++)
				{
					if (m1[i, j] != m2[i, j])
						return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Says if both <see cref="Matrix"/> have different values.
		/// </summary>
		public static bool operator!=(Matrix m1, Matrix m2)
		{
			if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
				return true;

			for (int i = 0; i < m1.Rows; i++)
			{
				for (int j = 0; j < m1.Columns; j++)
				{
					if (m1[i, j] != m2[i, j])
						return true;
				}
			}

			return false;
		}

		#endregion OPERATOR

		#region INSTANCE

		public override bool Equals(object obj)
		{
			if (obj is Matrix)
				return (Matrix)obj == this;

			return false;
		}

		/// <summary>
		/// Returns the adjugate of the <see cref="Matrix"/>.
		/// </summary>
		public Matrix GetAdjugate() => GetCofactor().Transposed();

		/// <summary>
		/// Returns the cofactor of the <see cref="Matrix"/>.
		/// </summary>
		public Matrix GetCofactor()
		{
			if (!IsSquare())
				throw new Exception("The cofactor can only be computed on a square matrix.");

			Matrix matrix = new Matrix(Rows, Columns);

			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns; j++)
				{
					matrix[i, j] = GetSub(i, j).GetDeterminant() * ((i + 1) * (j + 1) % 2 == 0 ? 1 : -1);
				}
			}

			return matrix;
		}

		/// <summary>
		/// Returns the determinant of the <see cref="Matrix"/>.
		/// </summary>
		public float GetDeterminant()
		{
			if (!IsSquare())
				throw new Exception("The determinant can only be computed on a square matrix.");

			if (Rows == 1) { return values[0, 0]; }
			else if (Rows == 2) { return values[0, 0] * values[1, 1] - values[0, 1] * values[1, 0]; }
			else
			{
				float det = 0;

				for (int i = 0; i < Columns; i++)
				{
					det += values[0, i] * GetSub(0, i).GetDeterminant() * (i % 2 == 0 ? 1 : -1);
				}

				return det;
			}
		}

		/// <summary>
		/// Returns the sub <see cref="Matrix"/> which is the <see cref="Matrix"/> without the given row and column.
		/// </summary>
		/// <param name="row">The row to delete.</param>
		/// <param name="column">The column to delete.</param>
		public Matrix GetSub(int row, int column)
		{
			if (row < 0 || row >= Rows || column < 0 || column >= Columns)
				throw new ArgumentOutOfRangeException($"The row must be in [0,{Rows}[ and the column must be in [0,{Columns}].");

			Matrix matrix = new Matrix(Rows - 1, Columns - 1);

			for (int i = 0; i < Rows; i++)
			{
				if (i == row) { continue; }

				for (int j = 0; j < Columns; j++)
				{
					if (j == column) { continue; }

					matrix[i, j] = values[i, j];
				}
			}

			return matrix;
		}

		/// <summary>
		/// Sets the <see cref="Matrix"/> to its inverse.
		/// </summary>
		public void Invert()
		{
			if (!IsSquare())
				throw new Exception("The invert matrix can only be computed on a square matrix.");

			float det = GetDeterminant();

			if (det == 0) { return; }

			Matrix invert = (1f / det) * GetAdjugate();

			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns; j++)
				{
					values[i, j] = invert[i, j];
				}
			}
		}

		/// <summary>
		/// Returns the inverse of this <see cref="Matrix"/>.
		/// </summary>
		public Matrix Inverted()
		{
			if (!IsSquare())
				throw new Exception("The invert matrix can only be computed on a square matrix.");

			float det = GetDeterminant();

			if (det == 0)
				throw new DivideByZeroException("The determinant is equal to zero.");

			return (1f / det) * GetAdjugate();
		}

		/// <summary>
		/// Says if the <see cref="Matrix"/> is invertible.
		/// </summary>
		public bool IsInvertible() => IsSquare() ? GetDeterminant() != 0 : false;

		/// <summary>
		/// Says if the <see cref="Matrix"/> is a square <see cref="Matrix"/>.
		/// </summary>
		public bool IsSquare() => Rows == Columns;

		/// <summary>
		/// Says if the <see cref="Matrix"/> is skew-symmetric (equal to the opposite of it's transposed <see cref="Matrix"/>).
		/// </summary>
		public bool IsSkewSymmetric()
		{
			if (!IsSquare())
				return false;

			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns; j++)
				{
					if (values[i, j] != -values[j, i])
						return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Says if the <see cref="Matrix"/> is symmetric (equal to it's transposed <see cref="Matrix"/>).
		/// </summary>
		public bool IsSymmetric()
		{
			if (!IsSquare())
				return false;

			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns; j++)
				{
					if (values[i, j] != values[j, i])
						return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Returns this <see cref="Matrix"/> to the given power.
		/// </summary>
		public Matrix Pow(int pow)
		{
			if (!IsSquare())
				throw new Exception($"Only square matrices can be given to the power of {pow}");

			Matrix matrix = Identity(Rows);

			if (pow >= 0)
			{
				for (int i = 0; i < pow; i++)
				{
					matrix *= this;
				}
			}
			else
			{
				Matrix invert = Inverted();

				for (int i = pow; i < 0; i++)
				{
					matrix *= invert;
				}
			}

			return matrix;
		}

		/// <summary>
		/// Says if this <see cref="Matrix"/> and the given <see cref="Matrix"/> have the same size.
		/// </summary>
		public bool SameSize(Matrix matrix) => Rows == matrix.Rows && Columns == matrix.Columns;

		/// <summary>
		/// Sets this <see cref="Matrix"/> to it's transposed <see cref="Matrix"/>.
		/// </summary>
		public void Transpose()
		{
			if (!IsSquare())
				throw new Exception("Only square matrices can be transposed.");

			float n;

			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns; j++)
				{
					n = values[i, j];
					values[i, j] = values[j, i];
					values[j, i] = n;
				}
			}
		}

		/// <summary>
		/// Returns this <see cref="Matrix"/>'s transposed <see cref="Matrix"/>.
		/// </summary>
		public Matrix Transposed()
		{
			if (!IsSquare())
				throw new Exception("The transposed matrix can only be computed on a square matrix.");

			Matrix matrix = new Matrix(Rows, Columns);

			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns; j++)
				{
					matrix[j, i] = values[i, j];
				}
			}

			return matrix;
		}

		#endregion INSTANCE

		#region STATIC

		/// <summary>
		/// Returns the identity <see cref="Matrix"/> to the given size.
		/// </summary>
		public static Matrix Identity(int size)
		{
			Matrix identity = new Matrix(size, size);

			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					identity[i, j] = i == j ? 1 : 0;
				}
			}

			return identity;
		}

		#endregion STATIC
	}
}
