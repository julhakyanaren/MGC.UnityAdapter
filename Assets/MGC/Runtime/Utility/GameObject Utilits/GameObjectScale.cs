using System;
using UnityEngine;

namespace MGC.UnityAdapter
{
    /// <summary>
    /// Provides Unity-friendly extension methods for working with a GameObject scale.
    ///
    /// This utility focuses on two scale spaces:
    ///
    /// 1) World scale (lossyScale)
    ///    - Retrieved via <see cref="Transform.lossyScale"/>.
    ///    - Represents the effective scale in world space after applying the full parent hierarchy.
    ///    - Note: it is read-only (there is no direct, reliable "set world scale" API),
    ///      so this class exposes only getters for world scale.
    ///
    /// 2) Local scale (localScale)
    ///    - Read and written via <see cref="Transform.localScale"/>.
    ///    - Represents the scale relative to the parent transform.
    ///    - This class provides setters and incremental operations for local scale:
    ///      - Set (absolute) methods: SetLocalScale*
    ///      - Additive (delta) methods: AddLocalScale*
    ///      - Multiplicative (factor) methods: MultiplyLocalScale*
    ///      - Reset helper: ResetLocalScale (sets localScale to (1,1,1))
    ///
    /// Design notes:
    /// - These methods do not validate scale values (negative scale is allowed in Unity and can be useful for mirroring).
    /// - Tuple overloads are provided for convenience and interoperability with non-Unity math code.
    /// </summary>
    public static class GameObjectScale
    {
        /// <summary>
        /// Gets the world-space X scale component (effective scale after hierarchy), via <see cref="Transform.lossyScale"/>.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>World-space X scale component.</returns>
        public static float GetWorldScaleX(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.lossyScale.x;
        }
        /// <summary>
        /// Gets the world-space Y scale component (effective scale after hierarchy), via <see cref="Transform.lossyScale"/>.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>World-space Y scale component.</returns>
        public static float GetWorldScaleY(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.lossyScale.y;
        }
        /// <summary>
        /// Gets the world-space Z scale component (effective scale after hierarchy), via <see cref="Transform.lossyScale"/>.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>World-space Z scale component.</returns>
        public static float GetWorldScaleZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.lossyScale.z;
        }

        /// <summary>
        /// Gets the world-space (X, Y) scale components, via <see cref="Transform.lossyScale"/>.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>Tuple (x, y) containing world-space X and Y scale components.</returns>
        public static (float x, float y) GetWorldScaleXY(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 lossyScale = gameObject.transform.lossyScale;
            return (lossyScale.x, lossyScale.y);
        }
        /// <summary>
        /// Gets the world-space (X, Z) scale components, via <see cref="Transform.lossyScale"/>.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>Tuple (x, z) containing world-space X and Z scale components.</returns>
        public static (float x, float z) GetWorldScaleXZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 lossyScale = gameObject.transform.lossyScale;
            return (lossyScale.x, lossyScale.z);
        }
        /// <summary>
        /// Gets the world-space (Y, Z) scale components, via <see cref="Transform.lossyScale"/>.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>Tuple (y, z) containing world-space Y and Z scale components.</returns>
        public static (float y, float z) GetWorldScaleYZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 lossyScale = gameObject.transform.lossyScale;
            return (lossyScale.y, lossyScale.z);
        }

        /// <summary>
        /// Gets the world-space (X, Y, Z) scale components as a tuple, via <see cref="Transform.lossyScale"/>.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>Tuple (x, y, z) containing world-space scale components.</returns>
        public static (float x, float y, float z) GetWorldScaleXYZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 lossyScale = gameObject.transform.lossyScale;
            return (lossyScale.x, lossyScale.y, lossyScale.z);
        }

        /// <summary>
        /// Gets the world-space scale vector (effective scale after hierarchy), via <see cref="Transform.lossyScale"/>.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>World-space scale as a Vector3.</returns>
        public static Vector3 GetWorldScale3D(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.lossyScale;
        }

        /// <summary>
        /// Gets the local-space X scale component, via <see cref="Transform.localScale"/>.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>Local-space X scale component.</returns>
        public static float GetLocalScaleX(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.localScale.x;
        }
        /// <summary>
        /// Gets the local-space Y scale component, via <see cref="Transform.localScale"/>.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>Local-space Y scale component.</returns>
        public static float GetLocalScaleY(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.localScale.y;
        }
        /// <summary>
        /// Gets the local-space Z scale component, via <see cref="Transform.localScale"/>.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>Local-space Z scale component.</returns>
        public static float GetLocalScaleZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.localScale.z;
        }

        /// <summary>
        /// Gets the local-space (X, Y) scale components.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>Tuple (x, y) containing local-space X and Y scale components.</returns>
        public static (float x, float y) GetLocalScaleXY(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;
            return (localScale.x, localScale.y);
        }
        /// <summary>
        /// Gets the local-space (X, Z) scale components.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>Tuple (x, z) containing local-space X and Z scale components.</returns>
        public static (float x, float z) GetLocalScaleXZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;
            return (localScale.x, localScale.z);
        }
        /// <summary>
        /// Gets the local-space (Y, Z) scale components.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>Tuple (y, z) containing local-space Y and Z scale components.</returns>
        public static (float y, float z) GetLocalScaleYZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;
            return (localScale.y, localScale.z);
        }

        /// <summary>
        /// Gets the local-space (X, Y, Z) scale components as a tuple.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>Tuple (x, y, z) containing local-space scale components.</returns>
        public static (float x, float y, float z) GetLocalScaleXYZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;
            return (localScale.x, localScale.y, localScale.z);
        }

        /// <summary>
        /// Gets the local-space scale vector, via <see cref="Transform.localScale"/>.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <returns>Local-space scale as a Vector3.</returns>
        public static Vector3 GetLocalScale3D(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.localScale;
        }

        /// <summary>
        /// Sets the local-space X scale component while preserving Y and Z.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="scaleX">New local-space X scale component.</param>
        public static void SetLocalScaleX(this GameObject gameObject, float scaleX)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;

            float x = scaleX;
            float y = localScale.y;
            float z = localScale.z;

            gameObject.transform.localScale = new Vector3(x, y, z);
        }
        /// <summary>
        /// Sets the local-space Y scale component while preserving X and Z.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="scaleY">New local-space Y scale component.</param>
        public static void SetLocalScaleY(this GameObject gameObject, float scaleY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;

            float x = localScale.x;
            float y = scaleY;
            float z = localScale.z;

            gameObject.transform.localScale = new Vector3(x, y, z);
        }
        /// <summary>
        /// Sets the local-space Z scale component while preserving X and Y.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="scaleZ">New local-space Z scale component.</param>
        public static void SetLocalScaleZ(this GameObject gameObject, float scaleZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;

            float x = localScale.x;
            float y = localScale.y;
            float z = scaleZ;

            gameObject.transform.localScale = new Vector3(x, y, z);
        }

        /// <summary>
        /// Sets the local-space X and Y scale components while preserving Z.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="scaleX">New local-space X scale component.</param>
        /// <param name="scaleY">New local-space Y scale component.</param>
        public static void SetLocalScaleXY(this GameObject gameObject, float scaleX, float scaleY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;

            float x = scaleX;
            float y = scaleY;
            float z = localScale.z;

            gameObject.transform.localScale = new Vector3(x, y, z);
        }
        /// <summary>
        /// Sets the local-space X and Y scale components (tuple overload) while preserving Z.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="scale">Tuple (x, y) containing the new local-space X and Y scale components.</param>
        public static void SetLocalScaleXY(this GameObject gameObject, (float x, float y) scale)
        {
            SetLocalScaleXY(gameObject, scale.x, scale.y);
        }

        /// <summary>
        /// Sets the local-space X and Z scale components while preserving Y.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="scaleX">New local-space X scale component.</param>
        /// <param name="scaleZ">New local-space Z scale component.</param>
        public static void SetLocalScaleXZ(this GameObject gameObject, float scaleX, float scaleZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;

            float x = scaleX;
            float y = localScale.y;
            float z = scaleZ;

            gameObject.transform.localScale = new Vector3(x, y, z);
        }
        /// <summary>
        /// Sets the local-space X and Z scale components (tuple overload) while preserving Y.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="scale">Tuple (x, z) containing the new local-space X and Z scale components.</param>
        public static void SetLocalScaleXZ(this GameObject gameObject, (float x, float z) scale)
        {
            SetLocalScaleXZ(gameObject, scale.x, scale.z);
        }

        /// <summary>
        /// Sets the local-space Y and Z scale components while preserving X.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="scaleY">New local-space Y scale component.</param>
        /// <param name="scaleZ">New local-space Z scale component.</param>
        public static void SetLocalScaleYZ(this GameObject gameObject, float scaleY, float scaleZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;

            float x = localScale.x;
            float y = scaleY;
            float z = scaleZ;

            gameObject.transform.localScale = new Vector3(x, y, z);
        }
        /// <summary>
        /// Sets the local-space Y and Z scale components (tuple overload) while preserving X.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="scale">Tuple (y, z) containing the new local-space Y and Z scale components.</param>
        public static void SetLocalScaleYZ(this GameObject gameObject, (float y, float z) scale)
        {
            SetLocalScaleYZ(gameObject, scale.y, scale.z);
        }

        /// <summary>
        /// Sets the local-space scale to the specified (X, Y, Z) components.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="scaleX">New local-space X scale component.</param>
        /// <param name="scaleY">New local-space Y scale component.</param>
        /// <param name="scaleZ">New local-space Z scale component.</param>
        public static void SetLocalScaleXYZ(this GameObject gameObject, float scaleX, float scaleY, float scaleZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            float x = scaleX;
            float y = scaleY;
            float z = scaleZ;

            gameObject.transform.localScale = new Vector3(x, y, z);
        }
        /// <summary>
        /// Sets the local-space scale (tuple overload).
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="scale">Tuple (x, y, z) containing the new local-space scale components.</param>
        public static void SetLocalScaleXYZ(this GameObject gameObject, (float x, float y, float z) scale)
        {
            SetLocalScaleXYZ(gameObject, scale.x, scale.y, scale.z);
        }

        /// <summary>
        /// Sets the local-space scale vector.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="scale">New local-space scale as a Vector3.</param>
        public static void SetLocalScale3D(this GameObject gameObject, Vector3 scale)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            gameObject.transform.localScale = scale;
        }

        /// <summary>
        /// Adds a delta to the local-space X scale component while preserving Y and Z.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="deltaX">Delta to add to local-space X scale component.</param>
        public static void AddLocalScaleX(this GameObject gameObject, float deltaX)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;

            float x = localScale.x + deltaX;
            float y = localScale.y;
            float z = localScale.z;

            gameObject.transform.localScale = new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds a delta to the local-space Y scale component while preserving X and Z.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="deltaY">Delta to add to local-space Y scale component.</param>
        public static void AddLocalScaleY(this GameObject gameObject, float deltaY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;

            float x = localScale.x;
            float y = localScale.y + deltaY;
            float z = localScale.z;

            gameObject.transform.localScale = new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds a delta to the local-space Z scale component while preserving X and Y.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="deltaZ">Delta to add to local-space Z scale component.</param>
        public static void AddLocalScaleZ(this GameObject gameObject, float deltaZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;

            float x = localScale.x;
            float y = localScale.y;
            float z = localScale.z + deltaZ;

            gameObject.transform.localScale = new Vector3(x, y, z);
        }

        /// <summary>
        /// Adds deltas to local-space X and Y scale components while preserving Z.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="deltaX">Delta to add to local-space X scale component.</param>
        /// <param name="deltaY">Delta to add to local-space Y scale component.</param>
        public static void AddLocalScaleXY(this GameObject gameObject, float deltaX, float deltaY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;

            float x = localScale.x + deltaX;
            float y = localScale.y + deltaY;
            float z = localScale.z;

            gameObject.transform.localScale = new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds deltas to local-space X and Y scale components (tuple overload) while preserving Z.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="delta">Tuple (x, y) containing deltas to add to local-space X and Y scale components.</param>
        public static void AddLocalScaleXY(this GameObject gameObject, (float x, float y) delta)
        {
            AddLocalScaleXY(gameObject, delta.x, delta.y);
        }

        /// <summary>
        /// Adds deltas to local-space X and Z scale components while preserving Y.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="deltaX">Delta to add to local-space X scale component.</param>
        /// <param name="deltaZ">Delta to add to local-space Z scale component.</param>
        public static void AddLocalScaleXZ(this GameObject gameObject, float deltaX, float deltaZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;

            float x = localScale.x + deltaX;
            float y = localScale.y;
            float z = localScale.z + deltaZ;

            gameObject.transform.localScale = new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds deltas to local-space X and Z scale components (tuple overload) while preserving Y.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="delta">Tuple (x, z) containing deltas to add to local-space X and Z scale components.</param>
        public static void AddLocalScaleXZ(this GameObject gameObject, (float x, float z) delta)
        {
            AddLocalScaleXZ(gameObject, delta.x, delta.z);
        }

        /// <summary>
        /// Adds deltas to local-space Y and Z scale components while preserving X.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="deltaY">Delta to add to local-space Y scale component.</param>
        /// <param name="deltaZ">Delta to add to local-space Z scale component.</param>
        public static void AddLocalScaleYZ(this GameObject gameObject, float deltaY, float deltaZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;

            float x = localScale.x;
            float y = localScale.y + deltaY;
            float z = localScale.z + deltaZ;

            gameObject.transform.localScale = new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds deltas to local-space Y and Z scale components (tuple overload) while preserving X.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="delta">Tuple (y, z) containing deltas to add to local-space Y and Z scale components.</param>
        public static void AddLocalScaleYZ(this GameObject gameObject, (float y, float z) delta)
        {
            AddLocalScaleYZ(gameObject, delta.y, delta.z);
        }

        /// <summary>
        /// Adds deltas to local-space X, Y, and Z scale components.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="deltaX">Delta to add to local-space X scale component.</param>
        /// <param name="deltaY">Delta to add to local-space Y scale component.</param>
        /// <param name="deltaZ">Delta to add to local-space Z scale component.</param>
        public static void AddLocalScaleXYZ(this GameObject gameObject, float deltaX, float deltaY, float deltaZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;

            float x = localScale.x + deltaX;
            float y = localScale.y + deltaY;
            float z = localScale.z + deltaZ;

            gameObject.transform.localScale = new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds deltas to local-space X, Y, and Z scale components (tuple overload).
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="delta">Tuple (x, y, z) containing deltas to add to local-space scale components.</param>
        public static void AddLocalScaleXYZ(this GameObject gameObject, (float x, float y, float z) delta)
        {
            AddLocalScaleXYZ(gameObject, delta.x, delta.y, delta.z);
        }

        /// <summary>
        /// Adds a delta vector to local-space scale.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="addScale">Delta vector to add to local-space scale.</param>
        public static void AddLocalScale3D(this GameObject gameObject, Vector3 addScale)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;
            localScale += addScale;

            gameObject.transform.localScale = localScale;
        }

        /// <summary>
        /// Multiplies the local-space X scale component by a factor while preserving Y and Z.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="factorX">Multiplier for local-space X scale component.</param>
        public static void MultiplyLocalScaleX(this GameObject gameObject, float factorX)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;

            float x = localScale.x * factorX;
            float y = localScale.y;
            float z = localScale.z;

            gameObject.transform.localScale = new Vector3(x, y, z);
        }
        /// <summary>
        /// Multiplies the local-space Y scale component by a factor while preserving X and Z.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="factorY">Multiplier for local-space Y scale component.</param>
        public static void MultiplyLocalScaleY(this GameObject gameObject, float factorY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;

            float x = localScale.x;
            float y = localScale.y * factorY;
            float z = localScale.z;

            gameObject.transform.localScale = new Vector3(x, y, z);
        }
        /// <summary>
        /// Multiplies the local-space Z scale component by a factor while preserving X and Y.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="factorZ">Multiplier for local-space Z scale component.</param>
        public static void MultiplyLocalScaleZ(this GameObject gameObject, float factorZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;

            float x = localScale.x;
            float y = localScale.y;
            float z = localScale.z * factorZ;

            gameObject.transform.localScale = new Vector3(x, y, z);
        }

        /// <summary>
        /// Multiplies the local-space X and Y scale components by factors while preserving Z.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="factorX">Multiplier for local-space X scale component.</param>
        /// <param name="factorY">Multiplier for local-space Y scale component.</param>
        public static void MultiplyLocalScaleXY(this GameObject gameObject, float factorX, float factorY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;

            float x = localScale.x * factorX;
            float y = localScale.y * factorY;
            float z = localScale.z;

            gameObject.transform.localScale = new Vector3(x, y, z);
        }
        /// <summary>
        /// Multiplies the local-space X and Y scale components (tuple overload) while preserving Z.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="factor">Tuple (x, y) containing multipliers for local-space X and Y scale components.</param>
        public static void MultiplyLocalScaleXY(this GameObject gameObject, (float x, float y) factor)
        {
            MultiplyLocalScaleXY(gameObject, factor.x, factor.y);
        }

        /// <summary>
        /// Multiplies the local-space X and Z scale components by factors while preserving Y.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="factorX">Multiplier for local-space X scale component.</param>
        /// <param name="factorZ">Multiplier for local-space Z scale component.</param>
        public static void MultiplyLocalScaleXZ(this GameObject gameObject, float factorX, float factorZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;

            float x = localScale.x * factorX;
            float y = localScale.y;
            float z = localScale.z * factorZ;

            gameObject.transform.localScale = new Vector3(x, y, z);
        }
        /// <summary>
        /// Multiplies the local-space X and Z scale components (tuple overload) while preserving Y.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="factor">Tuple (x, z) containing multipliers for local-space X and Z scale components.</param>
        public static void MultiplyLocalScaleXZ(this GameObject gameObject, (float x, float z) factor)
        {
            MultiplyLocalScaleXZ(gameObject, factor.x, factor.z);
        }

        /// <summary>
        /// Multiplies the local-space Y and Z scale components by factors while preserving X.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="factorY">Multiplier for local-space Y scale component.</param>
        /// <param name="factorZ">Multiplier for local-space Z scale component.</param>
        public static void MultiplyLocalScaleYZ(this GameObject gameObject, float factorY, float factorZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;

            float x = localScale.x;
            float y = localScale.y * factorY;
            float z = localScale.z * factorZ;

            gameObject.transform.localScale = new Vector3(x, y, z);
        }
        /// <summary>
        /// Multiplies the local-space Y and Z scale components (tuple overload) while preserving X.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="factor">Tuple (y, z) containing multipliers for local-space Y and Z scale components.</param>
        public static void MultiplyLocalScaleYZ(this GameObject gameObject, (float y, float z) factor)
        {
            MultiplyLocalScaleYZ(gameObject, factor.y, factor.z);
        }

        /// <summary>
        /// Multiplies the local-space X, Y, and Z scale components by factors.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="factorX">Multiplier for local-space X scale component.</param>
        /// <param name="factorY">Multiplier for local-space Y scale component.</param>
        /// <param name="factorZ">Multiplier for local-space Z scale component.</param>
        public static void MultiplyLocalScaleXYZ(this GameObject gameObject, float factorX, float factorY, float factorZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;

            float x = localScale.x * factorX;
            float y = localScale.y * factorY;
            float z = localScale.z * factorZ;

            gameObject.transform.localScale = new Vector3(x, y, z);
        }
        /// <summary>
        /// Multiplies the local-space X, Y, and Z scale components (tuple overload).
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="factor">Tuple (x, y, z) containing multipliers for local-space scale components.</param>
        public static void MultiplyLocalScaleXYZ(this GameObject gameObject, (float x, float y, float z) factor)
        {
            MultiplyLocalScaleXYZ(gameObject, factor.x, factor.y, factor.z);
        }

        /// <summary>
        /// Multiplies the entire local-space scale vector by a uniform factor.
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        /// <param name="factor">Uniform multiplier applied to X, Y, and Z local scale components.</param>
        public static void MultiplyLocalScale3D(this GameObject gameObject, float factor)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 localScale = gameObject.transform.localScale;
            localScale *= factor;

            gameObject.transform.localScale = localScale;
        }

        /// <summary>
        /// Resets the local scale to (1, 1, 1).
        /// </summary>
        /// <param name="gameObject">Target GameObject.</param>
        public static void ResetLocalScale(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            gameObject.transform.localScale = Vector3.one;
        }
    }
}
