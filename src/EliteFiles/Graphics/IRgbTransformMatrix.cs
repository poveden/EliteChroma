namespace EliteFiles.Graphics
{
    /// <summary>
    /// Represents an RGB colour transformation matrix.
    /// </summary>
    /// <remarks>
    /// RGB transformations are applied as a matrix multiplication:
    /// <code>
    /// :                            ┌          ┐
    /// :                            │ r₁ r₂ r₃ │
    /// : [ R' G' B' ] = [ R G B ] × │ g₁ g₂ g₃ │
    /// :                            │ b₁ b₂ b₃ │
    /// :                            └          ┘
    /// Out-of-bounds values for R', G' and B' are allowed
    /// during the multiplication, but they will be clamped
    /// back to the interval [0..1].
    /// </code>
    /// </remarks>
    public interface IRgbTransformMatrix
    {
        /// <summary>
        /// Gets the matrix value at the given row and column.
        /// </summary>
        /// <param name="row">The matrix row.</param>
        /// <param name="col">The matrix column.</param>
        /// <returns>The matrix value.</returns>
        double this[int row, int col] { get; }
    }
}
