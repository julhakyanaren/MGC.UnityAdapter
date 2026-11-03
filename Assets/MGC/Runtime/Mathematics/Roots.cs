// -----------------------------------------------------------------------------
// Source: MGC.Core.Mathematics.Roots
// Copied to: MGC.UnityAdapter.Mathematics
// Purpose: Unity package includes only selected math utilities from Core.
// Sync note: Keep this file in sync with the Core implementation.
// -----------------------------------------------------------------------------

using System;

namespace MGC.UnityAdapter.Mathematics
{
    /// <summary>
    /// Provides utility methods for computing real-valued roots of arbitrary degree,
    /// including validated roots and iterative Newton–Raphson approximations.
    /// </summary>
    /// <remarks>
    /// This class focuses on real-number root extraction and explicitly documents
    /// cases where a real solution does not exist (for example, even-degree roots
    /// of negative values).
    ///
    /// Important notes for users:
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// Some methods accept the root degree as <see cref="double"/>. In such cases,
    /// only values that are mathematically equivalent to integers are treated as
    /// integer degrees. Non-integer degrees applied to negative radicands do not
    /// produce real results.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// Methods based on numerical iteration (Newton–Raphson) provide approximate
    /// solutions and may be sensitive to the choice of initial values, tolerance,
    /// and maximum iteration count.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// For strict algebraic root behavior and full control over domain constraints,
    /// prefer overloads that use an integer root degree.
    /// </description>
    /// </item>
    /// </list>
    /// </remarks>

    public static class Roots
    {
        /// <summary>
        /// Computes the n-th root of a real number with validation for real-valued solutions.
        /// </summary>
        /// <param name="x">
        /// The radicand (the value under the root sign).
        /// </param>
        /// <param name="n">
        /// The degree of the root. Must not be zero.
        /// </param>
        /// <returns>
        /// The real n-th root of <paramref name="x"/> when a real solution exists.
        /// </returns>
        /// <remarks>
        /// This method is intended for safe real-valued root extraction and performs
        /// additional validation compared to a direct call to <see cref="Math.Pow(double,double)"/>.
        ///
        /// Behavior notes:
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// If <paramref name="x"/> is negative, <paramref name="n"/> must represent an integer
        /// degree for a real solution to exist.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Even integer degrees applied to negative values have no real solution and
        /// result in an exception.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The root degree <paramref name="n"/> is provided as <see cref="double"/>.
        /// Values that are not mathematically equivalent to integers are treated as
        /// non-integer degrees.
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="n"/> is zero, or if a real solution does not exist
        /// for the given combination of <paramref name="x"/> and <paramref name="n"/>.
        /// </exception>

        public static double SafeRoot(double x, double n)
        {
            if (n == 0.0)
            {
                throw new ArgumentException("The root degree cannot be zero.", nameof(n));
            }
            if (x < 0)
            {
                if (!NumberSets.IsInteger(n))
                {
                    throw new ArgumentException("For negative radicand, the root degree must be an integer.", nameof(n));
                }

                int k = (int)Math.Round(n);
                if (NumberSets.IsEven(k))
                {
                    throw new ArgumentException("There is no real solution for even-degree roots of negative numbers.", nameof(x));
                }
                double absRoot = Math.Pow(-x, 1.0 / k);
                return -absRoot;
            }
            return Math.Pow(x, 1.0 / n);
        }

        /// <summary>
        /// Computes the n-th root of a real number using an integer root degree.
        /// </summary>
        /// <param name="x">
        /// The radicand (the value under the root sign).
        /// </param>
        /// <param name="n">
        /// The degree of the root. Must not be zero.
        /// </param>
        /// <returns>
        /// The real n-th root of <paramref name="x"/> when it exists.
        /// If <paramref name="x"/> is negative and <paramref name="n"/> is even,
        /// the method returns <see cref="double.NaN"/> to indicate the absence
        /// of a real solution.
        /// </returns>
        /// <remarks>
        /// This method performs minimal validation and mirrors common mathematical
        /// conventions:
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// Odd integer degrees allow real roots of negative values.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// Even integer degrees applied to negative values yield no real solution
        /// and return <see cref="double.NaN"/> instead of throwing an exception.
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="n"/> is zero, since a root of degree zero is undefined.
        /// </exception>

        public static double Root(double x, int n)
        {
            if (n == 0)
            {
                throw new ArgumentException("The root degree cannot be zero.", nameof(n));
            }
            if (x < 0)
            {
                if ((n & 1) == 0)
                {
                    return double.NaN;
                }
                return -Math.Pow(-x, 1.0 / n);
            }
            return Math.Pow(x, 1.0 / n);
        }

        /// <summary>
        /// Attempts to compute the n-th root of a real number using the
        /// Newton–Raphson iterative method.
        /// </summary>
        /// <param name="x">
        /// The radicand (the value under the root sign).
        /// </param>
        /// <param name="n">
        /// The degree of the root. Must be a positive integer.
        /// </param>
        /// <param name="result">
        /// When this method returns, contains the computed approximation of the real
        /// n-th root of <paramref name="x"/> if the operation succeeded; otherwise,
        /// contains <see cref="double.NaN"/>.
        /// </param>
        /// <param name="tolerance">
        /// The desired precision of the result. Iterations stop when the change
        /// between successive approximations becomes less than this value.
        /// </param>
        /// <param name="maxIterations">
        /// The maximum number of iterations to perform.
        /// </param>
        /// <returns>
        /// <c>true</c> if a real-valued root exists for the given inputs and the
        /// computation completed; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method uses an iterative numerical approach and returns an approximation.
        /// Convergence behavior depends on the input value, root degree, tolerance,
        /// and iteration limit.
        ///
        /// Important notes:
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// The method does not guarantee convergence within the specified number
        /// of iterations for all inputs.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// If a real root does not exist (for example, even-degree root of a negative value),
        /// the method returns <c>false</c>.
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>

        public static bool TryNewtonRoot(double x, int n, out double result, double tolerance = 1e-10, int maxIterations = 100)
        {
            result = double.NaN;
            if (n <= 0)
            {
                return false;
            }
            if (tolerance <= 0.0)
            {
                return false;
            }
            if (maxIterations <= 0)
            {
                return false;
            }
            if (double.IsNaN(x) || double.IsInfinity(x))
            {
                return false;
            }

            if (x == 0.0)
            {
                result = 0.0;
                return true;
            }

            if (n == 1)
            {
                result = x;
                return true;
            }

            bool isNegative = x < 0.0;
            if (isNegative && (n & 1) == 0)
            {
                return false;
            }
            double absX = isNegative ? -x : x;
            double y = absX >= 1.0 ? absX : 1.0;
            for (int i = 0; i < maxIterations; i++)
            {
                double yPow = Math.Pow(y, n - 1);
                if (yPow == 0.0)
                {
                    y = 0.0;
                    break;
                }
                double yNext = ((n - 1) * y + absX / yPow) / n;

                if (Math.Abs(yNext - y) <= tolerance)
                {
                    y = yNext;
                    break;
                }

                y = yNext;
            }

            result = isNegative ? -y : y;
            return true;
        }

        /// <summary>
        /// Computes the n-th root of a real number using the Newton–Raphson
        /// iterative method.
        /// </summary>
        /// <param name="x">
        /// The radicand (the value under the root sign).
        /// </param>
        /// <param name="n">
        /// The degree of the root. Must be a positive integer.
        /// </param>
        /// <param name="tolerance">
        /// The desired precision of the result.
        /// </param>
        /// <param name="maxIterations">
        /// The maximum number of iterations to perform.
        /// </param>
        /// <returns>
        /// An approximation of the real n-th root of <paramref name="x"/>.
        /// </returns>
        /// <remarks>
        /// This method provides a numerical approximation of the real root.
        /// For negative values of <paramref name="x"/>, only odd root degrees
        /// produce real solutions.
        ///
        /// The result is subject to numerical precision limits and may differ
        /// slightly from the exact mathematical value.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="n"/>, <paramref name="tolerance"/>, or
        /// <paramref name="maxIterations"/> is not positive.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if a real root does not exist for the given inputs.
        /// </exception>

        public static double NewtonRoot(double x, int n, double tolerance = 1e-10, int maxIterations = 100)
        {
            if (n <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "The root degree must be positive.");
            }
            if (tolerance <= 0.0)
            {
                throw new ArgumentOutOfRangeException(nameof(tolerance), "Tolerance must be greater than zero.");
            }
            if (maxIterations <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxIterations), "Max iterations must be greater than zero.");
            }
            if (double.IsNaN(x) || double.IsInfinity(x))
            {
                throw new ArgumentException("The radicand must be a finite value.", nameof(x));
            }
            if (x == 0.0)
            {
                return 0.0;
            }
            if (n == 1)
            {
                return x;
            }

            bool isNegative = x < 0.0;

            if (isNegative && (n & 1) == 0)
            {
                throw new ArgumentException("Even root of a negative value has no real solution.", nameof(x));
            }
            double absX = 0;
            if (isNegative)
            {
                absX = -x;
            }
            else
            {
                absX = x;
            }

            double y = 0;
            if (absX >= 1.0)
            {
                y = absX;
            }
            else
            {
                y = 1.0;
            }

            for (int i = 0; i < maxIterations; i++)
            {
                double yPow = Math.Pow(y, n - 1);
                if (yPow == 0.0)
                {
                    y = 0.0;
                    break;
                }
                double yNext = ((n - 1) * y + absX / yPow) / n;

                if (Math.Abs(yNext - y) <= tolerance)
                {
                    y = yNext;
                    break;
                }

                y = yNext;
            }
            if (isNegative)
            {
                return -y;
            }
            else
            {
                return y;
            }
        }
    }
}
