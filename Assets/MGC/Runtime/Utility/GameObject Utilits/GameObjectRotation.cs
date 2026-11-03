using System;
using UnityEngine;

namespace MGC.UnityAdapter
{
    /// <summary>
    /// Provides Unity extension methods for working with GameObject rotation in degrees (Euler angles).
    ///
    /// This utility is focused on convenience and safety when reading, setting and adding rotations
    /// using familiar axis angles (X, Y, Z) and common axis pairs (XY, XZ, YZ).
    ///
    /// World vs Local:
    /// - World rotation methods operate on Transform.eulerAngles / Transform.rotation.
    ///   They represent the object's orientation in world space.
    /// - Local rotation methods operate on Transform.localEulerAngles / Transform.localRotation.
    ///   They represent the object's orientation relative to its parent transform.
    ///
    /// Notes about Euler angles in Unity:
    /// - All angles are in degrees.
    /// - Euler angles are not uniquely defined; Unity may represent the same orientation with different
    ///   Euler triplets. Values can appear to "jump" when converted back from quaternions.
    /// - This class does not normalize angles automatically. If you need normalization,
    ///   call your normalization utilities explicitly in user code.
    /// </summary>
    public static class GameObjectRotation
    {
        /// <summary>
        /// Gets the world-space rotation angle around the X axis in degrees.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>World Euler X angle (degrees).</returns>
        public static float GetRotationAngleX(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.eulerAngles.x;
        }
        /// <summary>
        /// Gets the world-space rotation angle around the Y axis in degrees.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>World Euler Y angle (degrees).</returns>
        public static float GetRotationAngleY(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.eulerAngles.y;
        }
        /// <summary>
        /// Gets the world-space rotation angle around the Z axis in degrees.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>World Euler Z angle (degrees).</returns>
        public static float GetRotationAngleZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.eulerAngles.z;
        }

        /// <summary>
        /// Gets the world-space Euler rotation angles (X, Y) in degrees as a tuple.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>(x, y) world Euler angles (degrees).</returns>
        public static (float x, float y) GetRotationAngleXY(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 euler = gameObject.transform.eulerAngles;
            return (euler.x, euler.y);
        }
        /// <summary>
        /// Gets the world-space Euler rotation angles (X, Z) in degrees as a tuple.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>(x, z) world Euler angles (degrees).</returns>
        public static (float x, float z) GetRotationAngleXZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 euler = gameObject.transform.eulerAngles;
            return (euler.x, euler.z);
        }
        /// <summary>
        /// Gets the world-space Euler rotation angles (Y, Z) in degrees as a tuple.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>(y, z) world Euler angles (degrees).</returns>
        public static (float y, float z) GetRotationAngleYZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 euler = gameObject.transform.eulerAngles;
            return (euler.y, euler.z);
        }

        /// <summary>
        /// Gets the world-space Euler rotation angles (X, Y, Z) in degrees as a Vector3.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>World Euler angles (degrees).</returns>
        public static Vector3 GetRotationAngle3D(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.eulerAngles;
        }

        /// <summary>
        /// Gets the world-space Euler rotation angles (X, Y, Z) in degrees as a tuple.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>(x, y, z) world Euler angles (degrees).</returns>
        public static (float x, float y, float z) GetRotationAngleXYZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 euler = gameObject.transform.eulerAngles;
            return (euler.x, euler.y, euler.z);
        }

        /// <summary>
        /// Gets the local-space rotation angle around the X axis in degrees.
        /// Local rotation is measured relative to the parent transform.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>Local Euler X angle (degrees).</returns>
        public static float GetLocalRotationAngleX(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.localEulerAngles.x;
        }
        /// <summary>
        /// Gets the local-space rotation angle around the Y axis in degrees.
        /// Local rotation is measured relative to the parent transform.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>Local Euler Y angle (degrees).</returns>
        public static float GetLocalRotationAngleY(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.localEulerAngles.y;
        }
        /// <summary>
        /// Gets the local-space rotation angle around the Z axis in degrees.
        /// Local rotation is measured relative to the parent transform.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>Local Euler Z angle (degrees).</returns>
        public static float GetLocalRotationAngleZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.localEulerAngles.z;
        }

        /// <summary>
        /// Gets the local-space Euler rotation angles (X, Y) in degrees as a tuple.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>(x, y) local Euler angles (degrees).</returns>
        public static (float x, float y) GetLocalRotationAngleXY(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 euler = gameObject.transform.localEulerAngles;
            return (euler.x, euler.y);
        }
        /// <summary>
        /// Gets the local-space Euler rotation angles (X, Z) in degrees as a tuple.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>(x, z) local Euler angles (degrees).</returns>
        public static (float x, float z) GetLocalRotationAngleXZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 euler = gameObject.transform.localEulerAngles;
            return (euler.x, euler.z);
        }
        /// <summary>
        /// Gets the local-space Euler rotation angles (Y, Z) in degrees as a tuple.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>(y, z) local Euler angles (degrees).</returns>
        public static (float y, float z) GetLocalRotationAngleYZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 euler = gameObject.transform.localEulerAngles;
            return (euler.y, euler.z);
        }

        /// <summary>
        /// Gets the local-space Euler rotation angles (X, Y, Z) in degrees as a Vector3.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>Local Euler angles (degrees).</returns>
        public static Vector3 GetLocalRotationAngle3D(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.localEulerAngles;
        }

        /// <summary>
        /// Gets the local-space Euler rotation angles (X, Y, Z) in degrees as a tuple.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>(x, y, z) local Euler angles (degrees).</returns>
        public static (float x, float y, float z) GetLocalRotationAngleXYZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 euler = gameObject.transform.localEulerAngles;
            return (euler.x, euler.y, euler.z);
        }

        /// <summary>
        /// Sets the world-space rotation X angle (degrees) while preserving the current world Y and Z angles.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleX">New world Euler X angle (degrees).</param>
        public static void SetRotationAngleX(this GameObject gameObject, float angleX)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 euler = gameObject.transform.eulerAngles;

            float x = angleX;
            float y = euler.y;
            float z = euler.z;

            gameObject.transform.rotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Sets the world-space rotation Y angle (degrees) while preserving the current world X and Z angles.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleY">New world Euler Y angle (degrees).</param>
        public static void SetRotationAngleY(this GameObject gameObject, float angleY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 euler = gameObject.transform.eulerAngles;

            float x = euler.x;
            float y = angleY;
            float z = euler.z;

            gameObject.transform.rotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Sets the world-space rotation Z angle (degrees) while preserving the current world X and Y angles.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleZ">New world Euler Z angle (degrees).</param>
        public static void SetRotationAngleZ(this GameObject gameObject, float angleZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 euler = gameObject.transform.eulerAngles;

            float x = euler.x;
            float y = euler.y;
            float z = angleZ;

            gameObject.transform.rotation = Quaternion.Euler(x, y, z);
        }

        /// <summary>
        /// Sets the world-space rotation angles (X, Y) in degrees while preserving the current world Z angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleX">New world Euler X angle (degrees).</param>
        /// <param name="angleY">New world Euler Y angle (degrees).</param>
        public static void SetRotationAngleXY(this GameObject gameObject, float angleX, float angleY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 euler = gameObject.transform.eulerAngles;

            float x = angleX;
            float y = angleY;
            float z = euler.z;

            gameObject.transform.rotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Sets the world-space rotation angles (X, Y) in degrees while preserving the current world Z angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angle">(x, y) world Euler angles (degrees).</param>
        public static void SetRotationAngleXY(this GameObject gameObject, (float x, float y) angle)
        {
            SetRotationAngleXY(gameObject, angle.x, angle.y);
        }

        /// <summary>
        /// Sets the world-space rotation angles (X, Z) in degrees while preserving the current world Y angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleX">New world Euler X angle (degrees).</param>
        /// <param name="angleZ">New world Euler Z angle (degrees).</param>
        public static void SetRotationAngleXZ(this GameObject gameObject, float angleX, float angleZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 euler = gameObject.transform.eulerAngles;

            float x = angleX;
            float y = euler.y;
            float z = angleZ;

            gameObject.transform.rotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Sets the world-space rotation angles (X, Z) in degrees while preserving the current world Y angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angle">(x, z) world Euler angles (degrees).</param>
        public static void SetRotationAngleXZ(this GameObject gameObject, (float x, float z) angle)
        {
            SetRotationAngleXZ(gameObject, angle.x, angle.z);
        }

        /// <summary>
        /// Sets the world-space rotation angles (Y, Z) in degrees while preserving the current world X angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleY">New world Euler Y angle (degrees).</param>
        /// <param name="angleZ">New world Euler Z angle (degrees).</param>
        public static void SetRotationAngleYZ(this GameObject gameObject, float angleY, float angleZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 euler = gameObject.transform.eulerAngles;

            float x = euler.x;
            float y = angleY;
            float z = angleZ;

            gameObject.transform.rotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Sets the world-space rotation angles (Y, Z) in degrees while preserving the current world X angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angle">(y, z) world Euler angles (degrees).</param>
        public static void SetRotationAngleYZ(this GameObject gameObject, (float y, float z) angle)
        {
            SetRotationAngleYZ(gameObject, angle.y, angle.z);
        }

        /// <summary>
        /// Sets the world-space rotation angles (X, Y, Z) in degrees.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleX">World Euler X angle (degrees).</param>
        /// <param name="angleY">World Euler Y angle (degrees).</param>
        /// <param name="angleZ">World Euler Z angle (degrees).</param>
        public static void SetRotationAngleXYZ(this GameObject gameObject, float angleX, float angleY, float angleZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            float x = angleX;
            float y = angleY;
            float z = angleZ;

            gameObject.transform.rotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Sets the world-space rotation angles (X, Y, Z) in degrees.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angle">(x, y, z) world Euler angles (degrees).</param>
        public static void SetRotationAngleXYZ(this GameObject gameObject, (float x, float y, float z) angle)
        {
            SetRotationAngleXYZ(gameObject, angle.x, angle.y, angle.z);
        }

        /// <summary>
        /// Sets the world-space rotation angles (X, Y, Z) in degrees.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angles">World Euler angles (degrees).</param>
        public static void SetRotationAngle3D(this GameObject gameObject, Vector3 angles)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            gameObject.transform.rotation = Quaternion.Euler(angles);
        }

        /// <summary>
        /// Sets the local-space rotation X angle (degrees) while preserving the current local Y and Z angles.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleX">New local Euler X angle (degrees).</param>
        public static void SetLocalRotationAngleX(this GameObject gameObject, float angleX)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 euler = gameObject.transform.localEulerAngles;

            float x = angleX;
            float y = euler.y;
            float z = euler.z;

            gameObject.transform.localRotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Sets the local-space rotation Y angle (degrees) while preserving the current local X and Z angles.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleY">New local Euler Y angle (degrees).</param>
        public static void SetLocalRotationAngleY(this GameObject gameObject, float angleY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 euler = gameObject.transform.localEulerAngles;

            float x = euler.x;
            float y = angleY;
            float z = euler.z;

            gameObject.transform.localRotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Sets the local-space rotation Z angle (degrees) while preserving the current local X and Y angles.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleZ">New local Euler Z angle (degrees).</param>
        public static void SetLocalRotationAngleZ(this GameObject gameObject, float angleZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 euler = gameObject.transform.localEulerAngles;

            float x = euler.x;
            float y = euler.y;
            float z = angleZ;

            gameObject.transform.localRotation = Quaternion.Euler(x, y, z);
        }

        /// <summary>
        /// Sets the local-space rotation angles (X, Y) in degrees while preserving the current local Z angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleX">New local Euler X angle (degrees).</param>
        /// <param name="angleY">New local Euler Y angle (degrees).</param>
        public static void SetLocalRotationAngleXY(this GameObject gameObject, float angleX, float angleY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 euler = gameObject.transform.localEulerAngles;

            float x = angleX;
            float y = angleY;
            float z = euler.z;

            gameObject.transform.localRotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Sets the local-space rotation angles (X, Y) in degrees while preserving the current local Z angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angle">(x, y) local Euler angles (degrees).</param>
        public static void SetLocalRotationAngleXY(this GameObject gameObject, (float x, float y) angle)
        {
            SetLocalRotationAngleXY(gameObject, angle.x, angle.y);
        }

        /// <summary>
        /// Sets the local-space rotation angles (X, Z) in degrees while preserving the current local Y angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleX">New local Euler X angle (degrees).</param>
        /// <param name="angleZ">New local Euler Z angle (degrees).</param>
        public static void SetLocalRotationAngleXZ(this GameObject gameObject, float angleX, float angleZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 euler = gameObject.transform.localEulerAngles;

            float x = angleX;
            float y = euler.y;
            float z = angleZ;

            gameObject.transform.localRotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Sets the local-space rotation angles (X, Z) in degrees while preserving the current local Y angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angle">(x, z) local Euler angles (degrees).</param>
        public static void SetLocalRotationAngleXZ(this GameObject gameObject, (float x, float z) angle)
        {
            SetLocalRotationAngleXZ(gameObject, angle.x, angle.z);
        }

        /// <summary>
        /// Sets the local-space rotation angles (Y, Z) in degrees while preserving the current local X angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleY">New local Euler Y angle (degrees).</param>
        /// <param name="angleZ">New local Euler Z angle (degrees).</param>
        public static void SetLocalRotationAngleYZ(this GameObject gameObject, float angleY, float angleZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 euler = gameObject.transform.localEulerAngles;

            float x = euler.x;
            float y = angleY;
            float z = angleZ;

            gameObject.transform.localRotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Sets the local-space rotation angles (Y, Z) in degrees while preserving the current local X angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angle">(y, z) local Euler angles (degrees).</param>
        public static void SetLocalRotationAngleYZ(this GameObject gameObject, (float y, float z) angle)
        {
            SetLocalRotationAngleYZ(gameObject, angle.y, angle.z);
        }

        /// <summary>
        /// Sets the local-space rotation angles (X, Y, Z) in degrees.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleX">Local Euler X angle (degrees).</param>
        /// <param name="angleY">Local Euler Y angle (degrees).</param>
        /// <param name="angleZ">Local Euler Z angle (degrees).</param>
        public static void SetLocalRotationAngleXYZ(this GameObject gameObject, float angleX, float angleY, float angleZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            gameObject.transform.localRotation = Quaternion.Euler(angleX, angleY, angleZ);
        }
        /// <summary>
        /// Sets the local-space rotation angles (X, Y, Z) in degrees.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angle">(x, y, z) local Euler angles (degrees).</param>
        public static void SetLocalRotationAngleXYZ(this GameObject gameObject, (float x, float y, float z) angle)
        {
            SetLocalRotationAngleXYZ(gameObject, angle.x, angle.y, angle.z);
        }

        /// <summary>
        /// Sets the local-space rotation angles (X, Y, Z) in degrees.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angles">Local Euler angles (degrees).</param>
        public static void SetLocalRotationAngle3D(this GameObject gameObject, Vector3 angles)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            gameObject.transform.localRotation = Quaternion.Euler(angles);
        }

        /// <summary>
        /// Adds a delta to the world-space X rotation angle (degrees) while preserving the current world Y and Z angles.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleX">Delta angle to add to world Euler X (degrees).</param>
        public static void AddRotationAngleX(this GameObject gameObject, float angleX)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 euler = gameObject.transform.eulerAngles;

            float x = euler.x + angleX;
            float y = euler.y;
            float z = euler.z;

            gameObject.transform.rotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Adds a delta to the world-space Y rotation angle (degrees) while preserving the current world X and Z angles.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleY">Delta angle to add to world Euler Y (degrees).</param>
        public static void AddRotationAngleY(this GameObject gameObject, float angleY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 euler = gameObject.transform.eulerAngles;

            float x = euler.x;
            float y = euler.y + angleY;
            float z = euler.z;

            gameObject.transform.rotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Adds a delta to the world-space Z rotation angle (degrees) while preserving the current world X and Y angles.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleZ">Delta angle to add to world Euler Z (degrees).</param>
        public static void AddRotationAngleZ(this GameObject gameObject, float angleZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 euler = gameObject.transform.eulerAngles;

            float x = euler.x;
            float y = euler.y;
            float z = euler.z + angleZ;

            gameObject.transform.rotation = Quaternion.Euler(x, y, z);
        }

        /// <summary>
        /// Adds deltas to the world-space rotation angles (X, Y) in degrees while preserving the current world Z angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleX">Delta for world Euler X (degrees).</param>
        /// <param name="angleY">Delta for world Euler Y (degrees).</param>
        public static void AddRotationAngleXY(this GameObject gameObject, float angleX, float angleY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 euler = gameObject.transform.eulerAngles;

            float x = euler.x + angleX;
            float y = euler.y + angleY;
            float z = euler.z;

            gameObject.transform.rotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Adds deltas to the world-space rotation angles (X, Y) in degrees while preserving the current world Z angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angle">(x, y) delta angles (degrees).</param>
        public static void AddRotationAngleXY(this GameObject gameObject, (float x, float y) angle)
        {
            AddRotationAngleXY(gameObject, angle.x, angle.y);
        }

        /// <summary>
        /// Adds deltas to the world-space rotation angles (X, Z) in degrees while preserving the current world Y angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleX">Delta for world Euler X (degrees).</param>
        /// <param name="angleZ">Delta for world Euler Z (degrees).</param>
        public static void AddRotationAngleXZ(this GameObject gameObject, float angleX, float angleZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 euler = gameObject.transform.eulerAngles;

            float x = euler.x + angleX;
            float y = euler.y;
            float z = euler.z + angleZ;

            gameObject.transform.rotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Adds deltas to the world-space rotation angles (X, Z) in degrees while preserving the current world Y angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angle">(x, z) delta angles (degrees).</param>
        public static void AddRotationAngleXZ(this GameObject gameObject, (float x, float z) angle)
        {
            AddRotationAngleXZ(gameObject, angle.x, angle.z);
        }

        /// <summary>
        /// Adds deltas to the world-space rotation angles (Y, Z) in degrees while preserving the current world X angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleY">Delta for world Euler Y (degrees).</param>
        /// <param name="angleZ">Delta for world Euler Z (degrees).</param>
        public static void AddRotationAngleYZ(this GameObject gameObject, float angleY, float angleZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 euler = gameObject.transform.eulerAngles;

            float x = euler.x;
            float y = euler.y + angleY;
            float z = euler.z + angleZ;

            gameObject.transform.rotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Adds deltas to the world-space rotation angles (Y, Z) in degrees while preserving the current world X angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angle">(y, z) delta angles (degrees).</param>
        public static void AddRotationAngleYZ(this GameObject gameObject, (float y, float z) angle)
        {
            AddRotationAngleYZ(gameObject, angle.y, angle.z);
        }

        /// <summary>
        /// Adds deltas to the world-space rotation angles (X, Y, Z) in degrees.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleX">Delta for world Euler X (degrees).</param>
        /// <param name="angleY">Delta for world Euler Y (degrees).</param>
        /// <param name="angleZ">Delta for world Euler Z (degrees).</param>
        public static void AddRotationAngleXYZ(this GameObject gameObject, float angleX, float angleY, float angleZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 euler = gameObject.transform.eulerAngles;

            float x = euler.x + angleX;
            float y = euler.y + angleY;
            float z = euler.z + angleZ;

            gameObject.transform.rotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Adds deltas to the world-space rotation angles (X, Y, Z) in degrees.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angle">(x, y, z) delta angles (degrees).</param>
        public static void AddRotationAngleXYZ(this GameObject gameObject, (float x, float y, float z) angle)
        {
            AddRotationAngleXYZ(gameObject, angle.x, angle.y, angle.z);
        }

        /// <summary>
        /// Adds deltas to the world-space rotation angles (X, Y, Z) in degrees.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angles">Delta Euler angles (degrees).</param>
        public static void AddRotationAngle3D(this GameObject gameObject, Vector3 angles)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 euler = gameObject.transform.eulerAngles;
            euler += angles;
            gameObject.transform.rotation = Quaternion.Euler(euler);
        }

        /// <summary>
        /// Adds a delta to the local-space X rotation angle (degrees) while preserving the current local Y and Z angles.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleX">Delta angle to add to local Euler X (degrees).</param>
        public static void AddLocalRotationAngleX(this GameObject gameObject, float angleX)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 euler = gameObject.transform.localEulerAngles;

            float x = euler.x + angleX;
            float y = euler.y;
            float z = euler.z;

            gameObject.transform.localRotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Adds a delta to the local-space Y rotation angle (degrees) while preserving the current local X and Z angles.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleY">Delta angle to add to local Euler Y (degrees).</param>
        public static void AddLocalRotationAngleY(this GameObject gameObject, float angleY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 euler = gameObject.transform.localEulerAngles;

            float x = euler.x;
            float y = euler.y + angleY;
            float z = euler.z;

            gameObject.transform.localRotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Adds a delta to the local-space Z rotation angle (degrees) while preserving the current local X and Y angles.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleZ">Delta angle to add to local Euler Z (degrees).</param>
        public static void AddLocalRotationAngleZ(this GameObject gameObject, float angleZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 euler = gameObject.transform.localEulerAngles;

            float x = euler.x;
            float y = euler.y;
            float z = euler.z + angleZ;

            gameObject.transform.localRotation = Quaternion.Euler(x, y, z);
        }

        /// <summary>
        /// Adds deltas to the local-space rotation angles (X, Y) in degrees while preserving the current local Z angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleX">Delta for local Euler X (degrees).</param>
        /// <param name="angleY">Delta for local Euler Y (degrees).</param>
        public static void AddLocalRotationAngleXY(this GameObject gameObject, float angleX, float angleY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 euler = gameObject.transform.localEulerAngles;

            float x = euler.x + angleX;
            float y = euler.y + angleY;
            float z = euler.z;

            gameObject.transform.localRotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Adds deltas to the local-space rotation angles (X, Y) in degrees while preserving the current local Z angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angle">(x, y) delta angles (degrees).</param>
        public static void AddLocalRotationAngleXY(this GameObject gameObject, (float x, float y) angle)
        {
            AddLocalRotationAngleXY(gameObject, angle.x, angle.y);
        }

        /// <summary>
        /// Adds deltas to the local-space rotation angles (X, Z) in degrees while preserving the current local Y angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleX">Delta for local Euler X (degrees).</param>
        /// <param name="angleZ">Delta for local Euler Z (degrees).</param>
        public static void AddLocalRotationAngleXZ(this GameObject gameObject, float angleX, float angleZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 euler = gameObject.transform.localEulerAngles;

            float x = euler.x + angleX;
            float y = euler.y;
            float z = euler.z + angleZ;

            gameObject.transform.localRotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Adds deltas to the local-space rotation angles (X, Z) in degrees while preserving the current local Y angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angle">(x, z) delta angles (degrees).</param>
        public static void AddLocalRotationAngleXZ(this GameObject gameObject, (float x, float z) angle)
        {
            AddLocalRotationAngleXZ(gameObject, angle.x, angle.z);
        }

        /// <summary>
        /// Adds deltas to the local-space rotation angles (Y, Z) in degrees while preserving the current local X angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleY">Delta for local Euler Y (degrees).</param>
        /// <param name="angleZ">Delta for local Euler Z (degrees).</param>
        public static void AddLocalRotationAngleYZ(this GameObject gameObject, float angleY, float angleZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 euler = gameObject.transform.localEulerAngles;

            float x = euler.x;
            float y = euler.y + angleY;
            float z = euler.z + angleZ;

            gameObject.transform.localRotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Adds deltas to the local-space rotation angles (Y, Z) in degrees while preserving the current local X angle.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angle">(y, z) delta angles (degrees).</param>
        public static void AddLocalRotationAngleYZ(this GameObject gameObject, (float y, float z) angle)
        {
            AddLocalRotationAngleYZ(gameObject, angle.y, angle.z);
        }

        /// <summary>
        /// Adds deltas to the local-space rotation angles (X, Y, Z) in degrees.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angleX">Delta for local Euler X (degrees).</param>
        /// <param name="angleY">Delta for local Euler Y (degrees).</param>
        /// <param name="angleZ">Delta for local Euler Z (degrees).</param>
        public static void AddLocalRotationAngleXYZ(this GameObject gameObject, float angleX, float angleY, float angleZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 euler = gameObject.transform.localEulerAngles;

            float x = euler.x + angleX;
            float y = euler.y + angleY;
            float z = euler.z + angleZ;

            gameObject.transform.localRotation = Quaternion.Euler(x, y, z);
        }
        /// <summary>
        /// Adds deltas to the local-space rotation angles (X, Y, Z) in degrees.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angle">(x, y, z) delta angles (degrees).</param>
        public static void AddLocalRotationAngleXYZ(this GameObject gameObject, (float x, float y, float z) angle)
        {
            AddLocalRotationAngleXYZ(gameObject, angle.x, angle.y, angle.z);
        }

        /// <summary>
        /// Adds deltas to the local-space rotation angles (X, Y, Z) in degrees.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="angles">Delta Euler angles (degrees).</param>
        public static void AddLocalRotationAngle3D(this GameObject gameObject, Vector3 angles)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 euler = gameObject.transform.localEulerAngles;
            euler += angles;
            gameObject.transform.localRotation = Quaternion.Euler(euler);
        }
    }
}
