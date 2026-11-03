using System;
using UnityEngine;

namespace MGC.UnityAdapter
{
    /// <summary>
    /// <para>
    /// Provides Unity-friendly angle conversion, angle normalization, and Euler delta helpers.
    /// </para>
    ///
    /// <para>
    /// This class is part of the UnityAdapter layer and focuses on:
    /// </para>
    ///
    /// <list type="bullet">
    ///   <item>
    ///     <description>Degrees ↔ radians conversions for scalar angles and angle vectors.</description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Optional per-axis conversions for <see cref="UnityEngine.Vector2"/> / <see cref="UnityEngine.Vector3"/>
    ///       using <see cref="RotationType2D"/> / <see cref="RotationType3D"/>.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Euler angle helpers for adding rotation deltas (in degrees), including tuple overloads for MGC.Core interop.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Angle normalization to common circular ranges:
    ///       <list type="bullet">
    ///         <item>
    ///           <description><c>[0, 360)</c> via <see cref="NormalizeDeg360(float)"/> and overloads.</description>
    ///         </item>
    ///         <item>
    ///           <description><c>[-180, 180)</c> via <see cref="NormalizeDeg180(float)"/> and overloads.</description>
    ///         </item>
    ///         <item>
    ///           <description><c>[0, 2π)</c> via <see cref="NormalizeRadTau(float)"/> and overloads.</description>
    ///         </item>
    ///         <item>
    ///           <description><c>[-π, π)</c> via <see cref="NormalizeRadPi(float)"/> and overloads.</description>
    ///         </item>
    ///       </list>
    ///     </description>
    ///   </item>
    /// </list>
    ///
    /// <para>Notes:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///       <see cref="UnityEngine.Transform.eulerAngles"/> uses degrees and stores Euler angles in a <see cref="UnityEngine.Vector3"/> (x, y, z).
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       This utility does not perform quaternion composition; it operates on Euler values and unit conversions.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Unity <see cref="UnityEngine.Vector2"/> / <see cref="UnityEngine.Vector3"/> and <see cref="UnityEngine.Mathf"/> operate on <see cref="System.Single"/>.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Tuple overloads accept <see cref="System.Double"/> values for MGC.Core interop and cast to <see cref="System.Single"/> internally.
    ///       This may cause precision loss; extremely large values may overflow to <c>+/-Infinity</c>.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>All methods are static and stateless.</description>
    ///   </item>
    /// </list>
    /// </summary>
    public static class AngleConverter
    {
        private const float FullTurnDeg = 360f;
        private const float HalfTurnDeg = 180f;

        /// <summary>
        /// Full turn constant in radians: 2π.
        /// </summary>
        public const float Tau = 6.283185307179586476925f;

        private static float ToFloatChecked(double value, string context)
        {
            float f = (float)value;

            if (float.IsNaN(f) || float.IsInfinity(f))
            {
                Debug.LogWarning($"AngleConverter: double->float cast produced {f} (value={value}) in {context}.");
            }

            return f;
        }

        /// <summary>
        /// Specifies which axis/axes of a Vector3 (x, y, z) should be processed.
        /// </summary>
        public enum RotationType3D
        {
            /// <summary>Process X only.</summary>
            X,

            /// <summary>Process Y only.</summary>
            Y,

            /// <summary>Process Z only.</summary>
            Z,

            /// <summary>Process X and Y.</summary>
            XY,

            /// <summary>Process X and Z.</summary>
            XZ,

            /// <summary>Process Y and Z.</summary>
            YZ,

            /// <summary>Process X, Y, and Z.</summary>
            XYZ
        }
        /// <summary>
        /// Specifies which axis/axes of a Vector2 (x, y) should be processed.
        /// </summary>
        public enum RotationType2D
        {
            /// <summary>Process X only.</summary>
            X,

            /// <summary>Process Y only.</summary>
            Y,

            /// <summary>Process X and Y.</summary>
            XY
        }

        /// <summary>
        /// Converts an angle from degrees to radians.
        /// </summary>
        /// <param name="degrees">Angle in degrees.</param>
        /// <returns>Angle in radians.</returns>
        public static float DegToRad(float degrees)
        {
            return Mathf.Deg2Rad * degrees;
        }
        /// <summary>
        /// Converts selected components of a Vector2 from degrees to radians.
        /// Components not selected by <paramref name="rotationType"/> are returned unchanged.
        /// </summary>
        /// <param name="degXY">Vector2 where each component represents an angle in degrees.</param>
        /// <param name="rotationType">Which component(s) to convert.</param>
        /// <returns>A Vector2 with selected component(s) converted to radians.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="rotationType"/> is not supported.
        /// </exception>
        public static Vector2 DegToRad(Vector2 degXY, RotationType2D rotationType)
        {
            float x = degXY.x;
            float y = degXY.y;

            switch (rotationType)
            {
                case RotationType2D.X:
                    x = x.ToRadians();
                    break;

                case RotationType2D.Y:
                    y = y.ToRadians();
                    break;

                case RotationType2D.XY:
                    x = x.ToRadians();
                    y = y.ToRadians();
                    break;

                default:
                    Debug.LogError($"{rotationType} is incorrect Rotation type for Vector2. Method: DegToRad");
                    throw new ArgumentOutOfRangeException(nameof(rotationType));
            }

            return new Vector2(x, y);
        }
        /// <summary>
        /// Converts selected components of a Vector3 from degrees to radians.
        /// Components not selected by <paramref name="rotationType"/> are returned unchanged.
        /// </summary>
        /// <param name="degXYZ">Vector3 where each component represents an angle in degrees.</param>
        /// <param name="rotationType">Which component(s) to convert.</param>
        /// <returns>A Vector3 with selected component(s) converted to radians.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="rotationType"/> is not supported.
        /// </exception>
        public static Vector3 DegToRad(Vector3 degXYZ, RotationType3D rotationType)
        {
            float x = degXYZ.x;
            float y = degXYZ.y;
            float z = degXYZ.z;

            switch (rotationType)
            {
                case RotationType3D.X:
                    x = x.ToRadians();
                    break;

                case RotationType3D.Y:
                    y = y.ToRadians();
                    break;

                case RotationType3D.Z:
                    z = z.ToRadians();
                    break;

                case RotationType3D.XY:
                    x = x.ToRadians();
                    y = y.ToRadians();
                    break;

                case RotationType3D.XZ:
                    x = x.ToRadians();
                    z = z.ToRadians();
                    break;

                case RotationType3D.YZ:
                    y = y.ToRadians();
                    z = z.ToRadians();
                    break;

                case RotationType3D.XYZ:
                    x = x.ToRadians();
                    y = y.ToRadians();
                    z = z.ToRadians();
                    break;

                default:
                    Debug.LogError($"{rotationType} is incorrect Rotation type for Vector3. Method: DegToRad");
                    throw new ArgumentOutOfRangeException(nameof(rotationType));
            }

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Converts an angle from radians to degrees.
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <returns>Angle in degrees.</returns>
        public static float RadToDeg(float radians)
        {
            return Mathf.Rad2Deg * radians;
        }
        /// <summary>
        /// Converts selected components of a Vector2 from radians to degrees.
        /// Components not selected by <paramref name="rotationType"/> are returned unchanged.
        /// </summary>
        /// <param name="radXY">Vector2 where each component represents an angle in radians.</param>
        /// <param name="rotationType">Which component(s) to convert.</param>
        /// <returns>A Vector2 with selected component(s) converted to degrees.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="rotationType"/> is not supported.
        /// </exception>
        public static Vector2 RadToDeg(Vector2 radXY, RotationType2D rotationType)
        {
            float x = radXY.x;
            float y = radXY.y;

            switch (rotationType)
            {
                case RotationType2D.X:
                    x = x.ToDegrees();
                    break;

                case RotationType2D.Y:
                    y = y.ToDegrees();
                    break;

                case RotationType2D.XY:
                    x = x.ToDegrees();
                    y = y.ToDegrees();
                    break;

                default:
                    Debug.LogError($"{rotationType} is incorrect Rotation type for Vector2. Method: RadToDeg");
                    throw new ArgumentOutOfRangeException(nameof(rotationType));
            }

            return new Vector2(x, y);
        }
        /// <summary>
        /// Converts selected components of a Vector3 from radians to degrees.
        /// Components not selected by <paramref name="rotationType"/> are returned unchanged.
        /// </summary>
        /// <param name="radXYZ">Vector3 where each component represents an angle in radians.</param>
        /// <param name="rotationType">Which component(s) to convert.</param>
        /// <returns>A Vector3 with selected component(s) converted to degrees.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="rotationType"/> is not supported.
        /// </exception>
        public static Vector3 RadToDeg(Vector3 radXYZ, RotationType3D rotationType)
        {
            float x = radXYZ.x;
            float y = radXYZ.y;
            float z = radXYZ.z;

            switch (rotationType)
            {
                case RotationType3D.X:
                    x = x.ToDegrees();
                    break;

                case RotationType3D.Y:
                    y = y.ToDegrees();
                    break;

                case RotationType3D.Z:
                    z = z.ToDegrees();
                    break;

                case RotationType3D.XY:
                    x = x.ToDegrees();
                    y = y.ToDegrees();
                    break;

                case RotationType3D.XZ:
                    x = x.ToDegrees();
                    z = z.ToDegrees();
                    break;

                case RotationType3D.YZ:
                    y = y.ToDegrees();
                    z = z.ToDegrees();
                    break;

                case RotationType3D.XYZ:
                    x = x.ToDegrees();
                    y = y.ToDegrees();
                    z = z.ToDegrees();
                    break;

                default:
                    Debug.LogError($"{rotationType} is incorrect Rotation type for Vector3. Method: RadToDeg");
                    throw new ArgumentOutOfRangeException(nameof(rotationType));
            }

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Adds a delta rotation (degrees) to the X component of a Vector2 angle vector.
        /// </summary>
        /// <param name="eulerDegXY">Current Euler angles (degrees) represented as (x, y).</param>
        /// <param name="xDeg">Delta to add to X (degrees).</param>
        /// <returns>New Vector2 with updated X component.</returns>
        public static Vector2 AddDegX(this Vector2 eulerDegXY, float xDeg)
        {
            float x = eulerDegXY.x + xDeg;
            float y = eulerDegXY.y;

            return new Vector2(x, y);
        }
        /// <summary>
        /// Adds a delta rotation (degrees) to the X component of a Vector3 Euler angle vector.
        /// </summary>
        /// <param name="eulerDegXYZ">Current Euler angles (degrees) represented as (x, y, z).</param>
        /// <param name="xDeg">Delta to add to X (degrees).</param>
        /// <returns>New Vector3 with updated X component.</returns>
        public static Vector3 AddDegX(this Vector3 eulerDegXYZ, float xDeg)
        {
            float x = eulerDegXYZ.x + xDeg;
            float y = eulerDegXYZ.y;
            float z = eulerDegXYZ.z;

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Adds a delta rotation (degrees) to the Y component of a Vector2 angle vector.
        /// </summary>
        /// <param name="eulerDegXY">Current Euler angles (degrees) represented as (x, y).</param>
        /// <param name="yDeg">Delta to add to Y (degrees).</param>
        /// <returns>New Vector2 with updated Y component.</returns>
        public static Vector2 AddDegY(this Vector2 eulerDegXY, float yDeg)
        {
            float x = eulerDegXY.x;
            float y = eulerDegXY.y + yDeg;

            return new Vector2(x, y);
        }
        /// <summary>
        /// Adds a delta rotation (degrees) to the Y component of a Vector3 Euler angle vector.
        /// </summary>
        /// <param name="eulerDegXYZ">Current Euler angles (degrees) represented as (x, y, z).</param>
        /// <param name="yDeg">Delta to add to Y (degrees).</param>
        /// <returns>New Vector3 with updated Y component.</returns>
        public static Vector3 AddDegY(this Vector3 eulerDegXYZ, float yDeg)
        {
            float x = eulerDegXYZ.x;
            float y = eulerDegXYZ.y + yDeg;
            float z = eulerDegXYZ.z;

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Adds a delta rotation (degrees) to the Z component of a Vector3 Euler angle vector.
        /// </summary>
        /// <param name="eulerDegXYZ">Current Euler angles (degrees) represented as (x, y, z).</param>
        /// <param name="zDeg">Delta to add to Z (degrees).</param>
        /// <returns>New Vector3 with updated Z component.</returns>
        public static Vector3 AddDegZ(this Vector3 eulerDegXYZ, float zDeg)
        {
            float x = eulerDegXYZ.x;
            float y = eulerDegXYZ.y;
            float z = eulerDegXYZ.z + zDeg;

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Adds delta rotations (degrees) to the X and Y components of a Vector2 angle vector.
        /// </summary>
        /// <param name="eulerDegXY">Current Euler angles (degrees) represented as (x, y).</param>
        /// <param name="xDeg">Delta to add to X (degrees).</param>
        /// <param name="yDeg">Delta to add to Y (degrees).</param>
        /// <returns>New Vector2 with updated X and Y components.</returns>
        public static Vector2 AddDegXY(this Vector2 eulerDegXY, float xDeg, float yDeg)
        {
            float x = eulerDegXY.x + xDeg;
            float y = eulerDegXY.y + yDeg;

            return new Vector2(x, y);
        }
        /// <summary>
        /// Adds angular offsets (in degrees) to the X and Y components of a 2D Euler angle vector,
        /// using a tuple as the source of angular increments.
        ///
        /// Precision note:
        /// - The input tuple uses double for MGC.Core interop.
        /// - Values are cast to float internally to match Unity's Vector2/Mathf APIs.
        /// - This cast may cause precision loss for large magnitudes or very small deltas.
        /// - If the cast produces NaN/Infinity, a warning is logged.
        /// </summary>
        /// <param name="eulerDegXY">Current Euler angles (degrees) represented as (x, y).</param>
        /// <param name="deg">Delta rotation tuple (degrees) represented as (x, y).</param>
        /// <returns>New Vector2 with updated X and Y components.</returns>
        public static Vector2 AddDegXY(this Vector2 eulerDegXY, (double x, double y) deg)
        {
            float xDeg = ToFloatChecked(deg.x, "AddDegXY(Vector2, (double x, double y)) -> x");
            float yDeg = ToFloatChecked(deg.y, "AddDegXY(Vector2, (double x, double y)) -> y");

            return AddDegXY(eulerDegXY, xDeg, yDeg);
        }
        /// <summary>
        /// Adds delta rotations (degrees) to the X and Y components of a Vector3 Euler angle vector.
        /// Z is preserved.
        /// </summary>
        /// <param name="eulerDegXYZ">Current Euler angles (degrees) represented as (x, y, z).</param>
        /// <param name="xDeg">Delta to add to X (degrees).</param>
        /// <param name="yDeg">Delta to add to Y (degrees).</param>
        /// <returns>New Vector3 with updated X and Y components.</returns>
        public static Vector3 AddDegXY(this Vector3 eulerDegXYZ, float xDeg, float yDeg)
        {
            float x = eulerDegXYZ.x + xDeg;
            float y = eulerDegXYZ.y + yDeg;
            float z = eulerDegXYZ.z;

            return new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds angular offsets (in degrees) to the X and Y components of a 3D Euler angle vector,
        /// using a tuple as the source of angular increments. The Z component remains unchanged.
        ///
        /// Precision note:
        /// - The input tuple uses double for MGC.Core interop.
        /// - Values are cast to float internally to match Unity's Vector3/Mathf APIs.
        /// - This cast may cause precision loss for large magnitudes or very small deltas.
        /// - If the cast produces NaN/Infinity, a warning is logged.
        /// </summary>
        /// <param name="eulerDegXYZ">Current Euler angles (degrees) represented as (x, y, z).</param>
        /// <param name="deg">Delta rotation tuple (degrees) represented as (x, y).</param>
        /// <returns>New Vector3 with updated X and Y components; Z preserved.</returns>
        public static Vector3 AddDegXY(this Vector3 eulerDegXYZ, (double x, double y) deg)
        {
            float xDeg = ToFloatChecked(deg.x, "AddDegXY(Vector3, (double x, double y)) -> x");
            float yDeg = ToFloatChecked(deg.y, "AddDegXY(Vector3, (double x, double y)) -> y");

            return AddDegXY(eulerDegXYZ, xDeg, yDeg);
        }

        /// <summary>
        /// Adds delta rotations (degrees) to the X and Z components of a Vector3 Euler angle vector.
        /// Y is preserved.
        /// </summary>
        /// <param name="eulerDegXYZ">Current Euler angles (degrees) represented as (x, y, z).</param>
        /// <param name="xDeg">Delta to add to X (degrees).</param>
        /// <param name="zDeg">Delta to add to Z (degrees).</param>
        /// <returns>New Vector3 with updated X and Z components; Y preserved.</returns>
        public static Vector3 AddDegXZ(this Vector3 eulerDegXYZ, float xDeg, float zDeg)
        {
            float x = eulerDegXYZ.x + xDeg;
            float y = eulerDegXYZ.y;
            float z = eulerDegXYZ.z + zDeg;

            return new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds angular offsets (in degrees) to the X and Z components of a 3D Euler angle vector,
        /// using a tuple as the source of angular increments. The Y component remains unchanged.
        ///
        /// Precision note:
        /// - The input tuple uses double for MGC.Core interop.
        /// - Values are cast to float internally to match Unity's Vector3/Mathf APIs.
        /// - This cast may cause precision loss for large magnitudes or very small deltas.
        /// - If the cast produces NaN/Infinity, a warning is logged.
        /// </summary>
        /// <param name="eulerDegXYZ">Current Euler angles (degrees) represented as (x, y, z).</param>
        /// <param name="deg">Delta rotation tuple (degrees) represented as (x, z).</param>
        /// <returns>New Vector3 with updated X and Z components; Y preserved.</returns>
        public static Vector3 AddDegXZ(this Vector3 eulerDegXYZ, (double x, double z) deg)
        {
            float xDeg = ToFloatChecked(deg.x, "AddDegXZ(Vector3, (double x, double z)) -> x");
            float zDeg = ToFloatChecked(deg.z, "AddDegXZ(Vector3, (double x, double z)) -> z");

            return AddDegXZ(eulerDegXYZ, xDeg, zDeg);
        }

        /// <summary>
        /// Adds delta rotations (degrees) to the Y and Z components of a Vector3 Euler angle vector.
        /// X is preserved.
        /// </summary>
        /// <param name="eulerDegXYZ">Current Euler angles (degrees) represented as (x, y, z).</param>
        /// <param name="yDeg">Delta to add to Y (degrees).</param>
        /// <param name="zDeg">Delta to add to Z (degrees).</param>
        /// <returns>New Vector3 with updated Y and Z components; X preserved.</returns>
        public static Vector3 AddDegYZ(this Vector3 eulerDegXYZ, float yDeg, float zDeg)
        {
            float x = eulerDegXYZ.x;
            float y = eulerDegXYZ.y + yDeg;
            float z = eulerDegXYZ.z + zDeg;

            return new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds angular offsets (in degrees) to the Y and Z components of a 3D Euler angle vector,
        /// using a tuple as the source of angular increments. The X component remains unchanged.
        ///
        /// Precision note:
        /// - The input tuple uses double for MGC.Core interop.
        /// - Values are cast to float internally to match Unity's Vector3/Mathf APIs.
        /// - This cast may cause precision loss for large magnitudes or very small deltas.
        /// - If the cast produces NaN/Infinity, a warning is logged.
        /// </summary>
        /// <param name="eulerDegXYZ">Current Euler angles (degrees) represented as (x, y, z).</param>
        /// <param name="deg">Delta rotation tuple (degrees) represented as (y, z).</param>
        /// <returns>New Vector3 with updated Y and Z components; X preserved.</returns>
        public static Vector3 AddDegYZ(this Vector3 eulerDegXYZ, (double y, double z) deg)
        {
            float yDeg = ToFloatChecked(deg.y, "AddDegYZ(Vector3, (double y, double z)) -> y");
            float zDeg = ToFloatChecked(deg.z, "AddDegYZ(Vector3, (double y, double z)) -> z");

            return AddDegYZ(eulerDegXYZ, yDeg, zDeg);
        }

        /// <summary>
        /// Adds delta rotations (degrees) to the X, Y, and Z components of a Vector3 Euler angle vector.
        /// </summary>
        /// <param name="eulerDegXYZ">Current Euler angles (degrees) represented as (x, y, z).</param>
        /// <param name="xDeg">Delta to add to X (degrees).</param>
        /// <param name="yDeg">Delta to add to Y (degrees).</param>
        /// <param name="zDeg">Delta to add to Z (degrees).</param>
        /// <returns>New Vector3 with updated X, Y, and Z components.</returns>
        public static Vector3 AddDegXYZ(this Vector3 eulerDegXYZ, float xDeg = 0f, float yDeg = 0f, float zDeg = 0f)
        {
            float x = eulerDegXYZ.x + xDeg;
            float y = eulerDegXYZ.y + yDeg;
            float z = eulerDegXYZ.z + zDeg;

            return new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds angular offsets (in degrees) to the X, Y, and Z components of a 3D Euler angle vector,
        /// using a tuple as the source of angular increments.
        ///
        /// Precision note:
        /// - The input tuple uses double for MGC.Core interop.
        /// - Values are cast to float internally to match Unity's Vector3/Mathf APIs.
        /// - This cast may cause precision loss for large magnitudes or very small deltas.
        /// - If the cast produces NaN/Infinity, a warning is logged.
        /// </summary>
        /// <param name="eulerDegXYZ">Current Euler angles (degrees) represented as (x, y, z).</param>
        /// <param name="deg">Delta rotation tuple (degrees) represented as (x, y, z).</param>
        /// <returns>New Vector3 with updated X, Y, and Z components.</returns>
        public static Vector3 AddDegXYZ(this Vector3 eulerDegXYZ, (double x, double y, double z) deg)
        {
            float xDeg = ToFloatChecked(deg.x, "AddDegXYZ(Vector3, (double x, double y, double z)) -> x");
            float yDeg = ToFloatChecked(deg.y, "AddDegXYZ(Vector3, (double x, double y, double z)) -> y");
            float zDeg = ToFloatChecked(deg.z, "AddDegXYZ(Vector3, (double x, double y, double z)) -> z");

            return AddDegXYZ(eulerDegXYZ, xDeg, yDeg, zDeg);
        }

        /// <summary>
        /// Normalizes an angle in degrees to the range [0, 360).
        /// </summary>
        /// <param name="deg">Angle in degrees.</param>
        /// <returns>Normalized angle in degrees within [0, 360).</returns>
        public static float NormalizeDeg360(float deg)
        {
            float result = deg % FullTurnDeg;

            if (result < 0f)
            {
                result += FullTurnDeg;
            }

            return result;
        }
        /// <summary>
        /// Normalizes each component of a Vector2 (degrees) to the range [0, 360).
        /// </summary>
        /// <param name="degXY">Angle vector (degrees) represented as (x, y).</param>
        /// <returns>Normalized Vector2 with each component in [0, 360).</returns>
        public static Vector2 NormalizeDeg360(Vector2 degXY)
        {
            float x = NormalizeDeg360(degXY.x);
            float y = NormalizeDeg360(degXY.y);

            return new Vector2(x, y);
        }
        /// <summary>
        /// Normalizes each component of a Vector3 (degrees) to the range [0, 360).
        /// </summary>
        /// <param name="degXYZ">Euler angle vector (degrees) represented as (x, y, z).</param>
        /// <returns>Normalized Vector3 with each component in [0, 360).</returns>
        public static Vector3 NormalizeDeg360(Vector3 degXYZ)
        {
            float x = NormalizeDeg360(degXYZ.x);
            float y = NormalizeDeg360(degXYZ.y);
            float z = NormalizeDeg360(degXYZ.z);

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Normalizes an angle in degrees to the range [-180, 180).
        /// </summary>
        /// <param name="deg">Angle in degrees.</param>
        /// <returns>Normalized angle in degrees within [-180, 180).</returns>
        public static float NormalizeDeg180(float deg)
        {
            float result = NormalizeDeg360(deg);

            if (result >= HalfTurnDeg)
            {
                result -= FullTurnDeg;
            }

            return result;
        }
        /// <summary>
        /// Normalizes each component of a Vector2 (degrees) to the range [-180, 180).
        /// </summary>
        /// <param name="degXY">Angle vector (degrees) represented as (x, y).</param>
        /// <returns>Normalized Vector2 with each component in [-180, 180).</returns>
        public static Vector2 NormalizeDeg180(Vector2 degXY)
        {
            float x = NormalizeDeg180(degXY.x);
            float y = NormalizeDeg180(degXY.y);

            return new Vector2(x, y);
        }
        /// <summary>
        /// Normalizes each component of a Vector3 (degrees) to the range [-180, 180).
        /// </summary>
        /// <param name="degXYZ">Euler angle vector (degrees) represented as (x, y, z).</param>
        /// <returns>Normalized Vector3 with each component in [-180, 180).</returns>
        public static Vector3 NormalizeDeg180(Vector3 degXYZ)
        {
            float x = NormalizeDeg180(degXYZ.x);
            float y = NormalizeDeg180(degXYZ.y);
            float z = NormalizeDeg180(degXYZ.z);

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Normalizes an angle in radians to the range [0, 2π).
        /// </summary>
        /// <param name="rad">Angle in radians.</param>
        /// <returns>Normalized angle in radians within [0, 2π).</returns>
        public static float NormalizeRadTau(float rad)
        {
            float result = rad % Tau;

            if (result < 0f)
            {
                result += Tau;
            }

            return result;
        }
        /// <summary>
        /// Normalizes each component of a Vector2 (radians) to the range [0, 2π).
        /// </summary>
        /// <param name="radXY">Angle vector (radians) represented as (x, y).</param>
        /// <returns>Normalized Vector2 with each component in [0, 2π).</returns>
        public static Vector2 NormalizeRadTau(Vector2 radXY)
        {
            float x = NormalizeRadTau(radXY.x);
            float y = NormalizeRadTau(radXY.y);

            return new Vector2(x, y);
        }
        /// <summary>
        /// Normalizes each component of a Vector3 (radians) to the range [0, 2π).
        /// </summary>
        /// <param name="radXYZ">Angle vector (radians) represented as (x, y, z).</param>
        /// <returns>Normalized Vector3 with each component in [0, 2π).</returns>
        public static Vector3 NormalizeRadTau(Vector3 radXYZ)
        {
            float x = NormalizeRadTau(radXYZ.x);
            float y = NormalizeRadTau(radXYZ.y);
            float z = NormalizeRadTau(radXYZ.z);

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Normalizes an angle in radians to the range [-π, π).
        /// </summary>
        /// <param name="rad">Angle in radians.</param>
        /// <returns>Normalized angle in radians within [-π, π).</returns>
        public static float NormalizeRadPi(float rad)
        {
            float result = NormalizeRadTau(rad);

            if (result >= Mathf.PI)
            {
                result -= Tau;
            }

            return result;
        }
        /// <summary>
        /// Normalizes each component of a Vector2 (radians) to the range [-π, π).
        /// </summary>
        /// <param name="radXY">Angle vector (radians) represented as (x, y).</param>
        /// <returns>Normalized Vector2 with each component in [-π, π).</returns>
        public static Vector2 NormalizeRadPi(Vector2 radXY)
        {
            float x = NormalizeRadPi(radXY.x);
            float y = NormalizeRadPi(radXY.y);

            return new Vector2(x, y);
        }
        /// <summary>
        /// Normalizes each component of a Vector3 (radians) to the range [-π, π).
        /// </summary>
        /// <param name="radXYZ">Angle vector (radians) represented as (x, y, z).</param>
        /// <returns>Normalized Vector3 with each component in [-π, π).</returns>
        public static Vector3 NormalizeRadPi(Vector3 radXYZ)
        {
            float x = NormalizeRadPi(radXYZ.x);
            float y = NormalizeRadPi(radXYZ.y);
            float z = NormalizeRadPi(radXYZ.z);

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Extension method that normalizes an angle in degrees to the range [0, 360).
        /// </summary>
        /// <param name="deg">Angle in degrees.</param>
        /// <returns>Normalized angle in degrees within [0, 360).</returns>
        public static float Normalize360(this float deg)
        {
            return NormalizeDeg360(deg);
        }
        /// <summary>
        /// Extension method that normalizes each component of a Vector2 (degrees) to the range [0, 360).
        /// </summary>
        /// <param name="degXY">Angle vector (degrees) represented as (x, y).</param>
        /// <returns>Normalized Vector2 with each component in [0, 360).</returns>
        public static Vector2 Normalize360(this Vector2 degXY)
        {
            return NormalizeDeg360(degXY);
        }
        /// <summary>
        /// Extension method that normalizes each component of a Vector3 (degrees) to the range [0, 360).
        /// </summary>
        /// <param name="degXYZ">Euler angle vector (degrees) represented as (x, y, z).</param>
        /// <returns>Normalized Vector3 with each component in [0, 360).</returns>
        public static Vector3 Normalize360(this Vector3 degXYZ)
        {
            return NormalizeDeg360(degXYZ);
        }

        /// <summary>
        /// Extension method that normalizes an angle in degrees to the range [-180, 180).
        /// </summary>
        /// <param name="deg">Angle in degrees.</param>
        /// <returns>Normalized angle in degrees within [-180, 180).</returns>
        public static float Normalize180(this float deg)
        {
            return NormalizeDeg180(deg);
        }
        /// <summary>
        /// Extension method that normalizes each component of a Vector2 (degrees) to the range [-180, 180).
        /// </summary>
        /// <param name="degXY">Angle vector (degrees) represented as (x, y).</param>
        /// <returns>Normalized Vector2 with each component in [-180, 180).</returns>
        public static Vector2 Normalize180(this Vector2 degXY)
        {
            return NormalizeDeg180(degXY);
        }
        /// <summary>
        /// Extension method that normalizes each component of a Vector3 (degrees) to the range [-180, 180).
        /// </summary>
        /// <param name="degXYZ">Euler angle vector (degrees) represented as (x, y, z).</param>
        /// <returns>Normalized Vector3 with each component in [-180, 180).</returns>
        public static Vector3 Normalize180(this Vector3 degXYZ)
        {
            return NormalizeDeg180(degXYZ);
        }

        /// <summary>
        /// Extension method that normalizes an angle in radians to the range [0, 2π).
        /// </summary>
        /// <param name="rad">Angle in radians.</param>
        /// <returns>Normalized angle in radians within [0, 2π).</returns>
        public static float NormalizeTau(this float rad)
        {
            return NormalizeRadTau(rad);
        }
        /// <summary>
        /// Extension method that normalizes each component of a Vector2 (radians) to the range [0, 2π).
        /// </summary>
        /// <param name="radXY">Angle vector (radians) represented as (x, y).</param>
        /// <returns>Normalized Vector2 with each component in [0, 2π).</returns>
        public static Vector2 NormalizeTau(this Vector2 radXY)
        {
            return NormalizeRadTau(radXY);
        }
        /// <summary>
        /// Extension method that normalizes each component of a Vector3 (radians) to the range [0, 2π).
        /// </summary>
        /// <param name="radXYZ">Angle vector (radians) represented as (x, y, z).</param>
        /// <returns>Normalized Vector3 with each component in [0, 2π).</returns>
        public static Vector3 NormalizeTau(this Vector3 radXYZ)
        {
            return NormalizeRadTau(radXYZ);
        }

        /// <summary>
        /// Extension method that normalizes an angle in radians to the range [-π, π).
        /// </summary>
        /// <param name="rad">Angle in radians.</param>
        /// <returns>Normalized angle in radians within [-π, π).</returns>
        public static float NormalizePi(this float rad)
        {
            return NormalizeRadPi(rad);
        }
        /// <summary>
        /// Extension method that normalizes each component of a Vector2 (radians) to the range [-π, π).
        /// </summary>
        /// <param name="radXY">Angle vector (radians) represented as (x, y).</param>
        /// <returns>Normalized Vector2 with each component in [-π, π).</returns>
        public static Vector2 NormalizePi(this Vector2 radXY)
        {
            return NormalizeRadPi(radXY);
        }
        /// <summary>
        /// Extension method that normalizes each component of a Vector3 (radians) to the range [-π, π).
        /// </summary>
        /// <param name="radXYZ">Angle vector (radians) represented as (x, y, z).</param>
        /// <returns>Normalized Vector3 with each component in [-π, π).</returns>
        public static Vector3 NormalizePi(this Vector3 radXYZ)
        {
            return NormalizeRadPi(radXYZ);
        }

        /// <summary>
        /// Extension method that converts degrees to radians.
        /// </summary>
        /// <param name="degrees">Angle in degrees.</param>
        /// <returns>Angle in radians.</returns>
        public static float ToRadians(this float degrees)
        {
            return DegToRad(degrees);
        }
        /// <summary>
        /// Extension method that converts radians to degrees.
        /// </summary>
        /// <param name="radians">Angle in radians.</param>
        /// <returns>Angle in degrees.</returns>
        public static float ToDegrees(this float radians)
        {
            return RadToDeg(radians);
        }

        /// <summary>
        /// Normalizes an angle in degrees into the signed range (-180, 180].
        /// </summary>
        /// <remarks>
        /// Useful for shortest-direction differences and steering-like logic.
        /// </remarks>
        /// <param name="angle">Input angle in degrees.</param>
        /// <returns>
        /// An equivalent signed angle in the range (-180, 180].
        /// Positive values represent counter-clockwise rotation,
        /// negative values represent clockwise rotation.
        /// </returns>
        public static float NormalizeDegSigned(float angle)
        {
            float wrapped = NormalizeDeg360(angle);

            if (wrapped > HalfTurnDeg)
            {
                wrapped -= FullTurnDeg;
            }

            return wrapped;
        }
        /// <summary>
        /// Normalizes an angle in radians into the signed range (-π, π].
        /// </summary>
        /// <remarks>
        /// Useful for shortest-direction differences in radians.
        /// </remarks>
        /// <param name="angle">Input angle in radians.</param>
        /// <returns>
        /// An equivalent signed angle in the range (-π, π].
        /// Positive values represent counter-clockwise rotation.
        /// </returns>
        public static float NormalizeRadSigned(float angle)
        {
            float wrapped = NormalizeRadTau(angle);

            if (wrapped > Mathf.PI)
            {
                wrapped -= Tau;
            }

            return wrapped;
        }

        /// <summary>
        /// Computes the shortest signed angular difference between two angles in degrees.
        /// </summary>
        /// <remarks>
        /// The result is the signed shortest difference (to - from) in the range (-180, 180].
        /// </remarks>
        /// <param name="from">Starting angle in degrees.</param>
        /// <param name="to">Target angle in degrees.</param>
        /// <returns>Signed shortest difference in degrees.</returns>
        public static float DeltaAngleDeg(float from, float to)
        {
            return Mathf.DeltaAngle(from, to);
        }
        /// <summary>
        /// Computes the shortest signed angular difference between two angles in radians.
        /// </summary>
        /// <remarks>
        /// The result is the signed shortest difference (to - from) in the range (-π, π].
        /// </remarks>
        /// <param name="from">Starting angle in radians.</param>
        /// <param name="to">Target angle in radians.</param>
        /// <returns>Signed shortest difference in radians.</returns>
        public static float DeltaAngleRad(float from, float to)
        {
            float diff = to - from;
            return NormalizeRadSigned(diff);
        }

        /// <summary>
        /// Linearly interpolates between two angles in degrees along the shortest arc.
        /// </summary>
        /// <remarks>
        /// The interpolation factor <paramref name="t"/> is not clamped.
        /// A value of 0 returns <paramref name="from"/>, and 1 returns <paramref name="to"/>
        /// along the shortest angular path.
        /// </remarks>
        /// <param name="from">Starting angle in degrees.</param>
        /// <param name="to">Target angle in degrees.</param>
        /// <param name="t">Interpolation factor (not clamped).</param>
        /// <returns>Interpolated angle in degrees.</returns>
        public static float LerpAngleDeg(float from, float to, float t)
        {
            return Mathf.LerpAngle(from, to, t);
        }
        /// <summary>
        /// Linearly interpolates between two angles in radians along the shortest arc.
        /// </summary>
        /// <remarks>
        /// The interpolation factor <paramref name="t"/> is not clamped.
        /// The result is normalized to [0, 2π).
        /// </remarks>
        /// <param name="from">Starting angle in radians.</param>
        /// <param name="to">Target angle in radians.</param>
        /// <param name="t">Interpolation factor (not clamped).</param>
        /// <returns>Interpolated angle in radians normalized to [0, 2π).</returns>
        public static float LerpAngleRad(float from, float to, float t)
        {
            float delta = DeltaAngleRad(from, to);
            float result = from + delta * t;

            return NormalizeRadTau(result);
        }

        /// <summary>
        /// Gradually moves an angle in degrees toward a target angle,
        /// limiting the maximum change by <paramref name="maxDelta"/>.
        /// </summary>
        /// <remarks>
        /// Uses Unity's shortest-path behavior.
        /// </remarks>
        /// <param name="current">Current angle in degrees.</param>
        /// <param name="target">Target angle in degrees.</param>
        /// <param name="maxDelta">Maximum allowed change in degrees for a single step.</param>
        /// <returns>
        /// New angle in degrees moved toward <paramref name="target"/> by at most <paramref name="maxDelta"/>.
        /// </returns>
        public static float MoveTowardsAngleDeg(float current, float target, float maxDelta)
        {
            return Mathf.MoveTowardsAngle(current, target, maxDelta);
        }
        /// <summary>
        /// Gradually moves an angle in radians toward a target angle,
        /// limiting the maximum change by <paramref name="maxDelta"/>.
        /// </summary>
        /// <remarks>
        /// Movement follows the shortest direction using the signed delta in (-π, π].
        /// The result is normalized to [0, 2π).
        /// </remarks>
        /// <param name="current">Current angle in radians.</param>
        /// <param name="target">Target angle in radians.</param>
        /// <param name="maxDelta">Maximum allowed change in radians for a single step.</param>
        /// <returns>
        /// New angle in radians moved toward <paramref name="target"/> by at most <paramref name="maxDelta"/>
        /// and normalized to [0, 2π).
        /// </returns>
        public static float MoveTowardsAngleRad(float current, float target, float maxDelta)
        {
            float delta = DeltaAngleRad(current, target);

            if (maxDelta <= 0f || Mathf.Abs(delta) <= maxDelta)
            {
                return NormalizeRadTau(target);
            }

            float step = Mathf.Clamp(delta, -maxDelta, maxDelta);
            float result = current + step;

            return NormalizeRadTau(result);
        }

        /// <summary>
        /// Determines whether an angle in degrees lies within a circular range
        /// [<paramref name="start"/>, <paramref name="end"/>], inclusive, accounting for wrap-around.
        /// </summary>
        /// <param name="angle">Angle to test in degrees.</param>
        /// <param name="start">Range start in degrees.</param>
        /// <param name="end">Range end in degrees.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="angle"/> lies within the specified range; otherwise <c>false</c>.
        /// </returns>
        public static bool IsAngleBetweenDeg(float angle, float start, float end)
        {
            angle = NormalizeDeg360(angle);
            start = NormalizeDeg360(start);
            end = NormalizeDeg360(end);

            if (start <= end)
            {
                return angle >= start && angle <= end;
            }

            return angle >= start || angle <= end;
        }
        /// <summary>
        /// Determines whether an angle in radians lies within a circular range
        /// [<paramref name="start"/>, <paramref name="end"/>], inclusive, accounting for wrap-around.
        /// </summary>
        /// <param name="angle">Angle to test in radians.</param>
        /// <param name="start">Range start in radians.</param>
        /// <param name="end">Range end in radians.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="angle"/> lies within the specified range; otherwise <c>false</c>.
        /// </returns>
        public static bool IsAngleBetweenRad(float angle, float start, float end)
        {
            angle = NormalizeRadTau(angle);
            start = NormalizeRadTau(start);
            end = NormalizeRadTau(end);

            if (start <= end)
            {
                return angle >= start && angle <= end;
            }

            return angle >= start || angle <= end;
        }

        /// <summary>
        /// Determines whether two angles in degrees are approximately equal,
        /// accounting for circular wrap-around using the shortest difference.
        /// </summary>
        /// <param name="a">First angle in degrees.</param>
        /// <param name="b">Second angle in degrees.</param>
        /// <param name="toleranceDeg">
        /// Maximum allowed absolute difference in degrees along the shortest arc.
        /// </param>
        /// <returns>
        /// <c>true</c> if the shortest difference is less than or equal to <paramref name="toleranceDeg"/>; otherwise <c>false</c>.
        /// </returns>
        public static bool ApproximatelyEqualDeg(float a, float b, float toleranceDeg = 0.0001f)
        {
            float delta = DeltaAngleDeg(a, b);
            return Mathf.Abs(delta) <= toleranceDeg;
        }
        /// <summary>
        /// Determines whether two angles in radians are approximately equal,
        /// accounting for circular wrap-around using the shortest difference.
        /// </summary>
        /// <param name="a">First angle in radians.</param>
        /// <param name="b">Second angle in radians.</param>
        /// <param name="toleranceRad">
        /// Maximum allowed absolute difference in radians along the shortest arc.
        /// </param>
        /// <returns>
        /// <c>true</c> if the shortest difference is less than or equal to <paramref name="toleranceRad"/>; otherwise <c>false</c>.
        /// </returns>
        public static bool ApproximatelyEqualRad(float a, float b, float toleranceRad = 1e-6f)
        {
            float delta = DeltaAngleRad(a, b);
            return Mathf.Abs(delta) <= toleranceRad;
        }
    }
}
