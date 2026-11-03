using UnityEngine;

namespace MGC.UnityAdapter
{
    /// <summary>
    /// <para>
    /// Provides utility methods for converting between Unity vectors
    /// (<see cref="Vector2"/> / <see cref="Vector3"/>, float-based)
    /// and tuple-based vectors used in the Core layer (float/double tuples).
    /// </para>
    ///
    /// <para><b>Purpose:</b></para>
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///       Keep the Core layer independent from Unity-specific types.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Allow Unity-side code to easily convert to and from Core-friendly tuples.
    ///     </description>
    ///   </item>
    /// </list>
    ///
    /// <para><b>Notes:</b></para>
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///       Unity <see cref="Vector2"/> and <see cref="Vector3"/> are <see cref="float"/>-based.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Core tuples often use <see cref="double"/> for higher precision.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Converting to Unity vectors requires explicit casts to <see cref="float"/>.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       This class contains only Unity-to-tuple and tuple-to-Unity conversions
    ///       (no generic numeric helpers).
    ///     </description>
    ///   </item>
    /// </list>
    /// </summary>
    public static class VectorConverter
    {
        /// <summary>
        /// Converts Unity Vector2 to a float tuple (x, y).
        /// </summary>
        public static (float x, float y) ToFloat2(this Vector2 vector)
        {
            return (vector.x, vector.y);
        }
        /// <summary>
        /// Converts Unity Vector3 to a float tuple (x, y, z).
        /// </summary>
        public static (float x, float y, float z) ToFloat3(this Vector3 vector)
        {
            return (vector.x, vector.y, vector.z);
        }
        /// <summary>
        /// Converts Unity Vector2 to a float tuple (x, y, z) by supplying z.
        /// Useful when Core APIs expect 3D tuples but Unity code operates in 2D.
        /// </summary>
        public static (float x, float y, float z) ToFloat3(this Vector2 vector, float z = 0.0f)
        {
            return (vector.x, vector.y, z);
        }

        /// <summary>
        /// Converts Unity Vector2 to a double tuple (x, y).
        /// </summary>
        public static (double x, double y) ToDouble2(this Vector2 vector)
        {
            return (vector.x, vector.y);
        }
        /// <summary>
        /// Converts Unity Vector3 to a double tuple (x, y, z).
        /// </summary>
        public static (double x, double y, double z) ToDouble3(this Vector3 vector)
        {
            return (vector.x, vector.y, vector.z);
        }
        /// <summary>
        /// Converts Unity Vector2 to a double tuple (x, y, z) by supplying z.
        /// Useful when Core APIs expect 3D tuples but Unity code operates in 2D.
        /// </summary>
        public static (double x, double y, double z) ToDouble3(this Vector2 vector, double z = 0.0)
        {
            return (vector.x, vector.y, z);
        }

        /// <summary>
        /// Converts a float tuple (x, y) to Unity Vector2.
        /// </summary>
        public static Vector2 ToVector2(this (float x, float y) tuple)
        {
            return new Vector2(tuple.x, tuple.y);
        }
        /// <summary>
        /// Converts a double tuple (x, y) to Unity Vector2.
        /// Values are cast to float because Unity vectors are float-based.
        /// </summary>
        public static Vector2 ToVector2(this (double x, double y) tuple)
        {
            return new Vector2((float)tuple.x, (float)tuple.y);
        }

        /// <summary>
        /// Converts a float tuple (x, y, z) to Unity Vector3.
        /// </summary>
        public static Vector3 ToVector3(this (float x, float y, float z) tuple)
        {
            return new Vector3(tuple.x, tuple.y, tuple.z);
        }
        /// <summary>
        /// Converts a double tuple (x, y, z) to Unity Vector3.
        /// Values are cast to float because Unity vectors are float-based.
        /// </summary>
        public static Vector3 ToVector3(this (double x, double y, double z) tuple)
        {
            return new Vector3((float)tuple.x, (float)tuple.y, (float)tuple.z);
        }
        /// <summary>
        /// Converts a double tuple (x, y) to Unity Vector3 by supplying z.
        /// Useful for passing 2D Core results into Transform/3D Unity APIs.
        /// </summary>
        public static Vector3 ToVector3(this (double x, double y) tuple, double z = 0.0)
        {
            return new Vector3((float)tuple.x, (float)tuple.y, (float)z);
        }
        /// <summary>
        /// Converts a float tuple (x, y) to Unity Vector3 by supplying z.
        /// Useful for passing 2D Core results into Transform/3D Unity APIs.
        /// </summary>
        public static Vector3 ToVector3(this (float x, float y) tuple, float z = 0.0f)
        {
            return new Vector3(tuple.x, tuple.y, z);
        }
        
        /// <summary>
        /// Converts a double tuple (x, y, z) to Unity Vector2 by ignoring the z component.
        /// Useful when Core returns 3D values but Unity code operates in 2D.
        /// </summary>
        public static Vector2 ToVector2(this (double x, double y, double z) tuple)
        {
            return new Vector2((float)tuple.x, (float)tuple.y);
        }
        /// <summary>
        /// Converts a float tuple (x, y, z) to Unity Vector2 by ignoring the z component.
        /// </summary>
        public static Vector2 ToVector2(this (float x, float y, float z) tuple)
        {
            return new Vector2(tuple.x, tuple.y);
        }

        /// <summary>
        /// Creates a 3D double tuple (x, y, z) from a 2D double tuple (x, y) by supplying z.
        /// This is a small helper for cases where Core APIs expect a 3D tuple.
        /// </summary>
        public static (double x, double y, double z) WithZ(this (double x, double y) tuple, double z = 0.0)
        {
            return (tuple.x, tuple.y, z);
        }
        /// <summary>
        /// Creates a 3D float tuple (x, y, z) from a 2D float tuple (x, y) by supplying z.
        /// </summary>
        public static (float x, float y, float z) WithZ(this (float x, float y) tuple, float z = 0.0f)
        {
            return (tuple.x, tuple.y, z);
        }
    }
}
