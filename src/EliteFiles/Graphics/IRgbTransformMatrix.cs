namespace EliteFiles.Graphics
{
    /// <summary>
    /// Represents an RGB colour transformation matrix.
    /// </summary>
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
