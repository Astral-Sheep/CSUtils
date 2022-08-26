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

		public Matrix(int lines, int columns, params float[] numbers)
		{
			_rows = lines;
			_columns = columns;

			if (numbers.Length != lines * columns)
				throw new TypeInitializationException("The number of rows and columns doesn't correspond to the number of values given.", new ArgumentException());

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

		public Matrix(int lines, int columns)
		{
			_rows = lines;
			_columns = columns;
			values = new float[lines, columns];
		}

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

		#endregion OPERATOR

		#region INSTANCE

		public bool SameSize(Matrix matrix) => Rows == matrix.Rows && Columns == matrix.Columns;

		#endregion INSTANCE
	}
}
