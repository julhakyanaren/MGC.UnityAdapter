using System;
using UnityEngine;

namespace MGC.UnityAdapter
{
    /// <summary>
    /// Provides Unity-friendly extension methods for reading and modifying a <see cref="GameObject"/> position.
    ///
    /// This utility focuses on two coordinate spaces:
    /// - World space (global): <see cref="Transform.position"/>
    /// - Local space (relative to parent): <see cref="Transform.localPosition"/>
    ///
    /// Methods are grouped by intent:
    /// - Get* : reads current position components (X/Y/Z) or grouped components (XY/XZ/YZ/XYZ).
    /// - Set* : sets position components while preserving the remaining components.
    /// - Add* : applies a delta (offset) to position components while preserving the remaining components.
    ///
    /// Notes:
    /// - "Set" methods treat input values as coordinates.
    /// - "Add" methods treat input values as deltas (offsets).
    /// - Tuple overloads are provided as a lightweight API option without allocating a new <see cref="Vector3"/> explicitly.
    /// - This class does not clamp or normalize values; it assigns exactly what you pass.
    /// </summary>
    public static class GameObjectPosition
    {
        /// <summary>
        /// Gets the world-space X coordinate of the <see cref="GameObject"/> position.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <returns>World-space X coordinate.</returns>
        public static float GetPositionX(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.position.x;
        }
        /// <summary>
        /// Gets the world-space Y coordinate of the <see cref="GameObject"/> position.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <returns>World-space Y coordinate.</returns>
        public static float GetPositionY(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.position.y;
        }
        /// <summary>
        /// Gets the world-space Z coordinate of the <see cref="GameObject"/> position.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <returns>World-space Z coordinate.</returns>
        public static float GetPositionZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.position.z;
        }

        /// <summary>
        /// Gets the world-space X and Y coordinates of the <see cref="GameObject"/> position.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <returns>Tuple (x, y) of world-space coordinates.</returns>
        public static (float x, float y) GetPositionXY(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 position = gameObject.transform.position;
            return (position.x, position.y);
        }
        /// <summary>
        /// Gets the world-space X and Z coordinates of the <see cref="GameObject"/> position.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <returns>Tuple (x, z) of world-space coordinates.</returns>
        public static (float x, float z) GetPositionXZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 position = gameObject.transform.position;
            return (position.x, position.z);
        }
        /// <summary>
        /// Gets the world-space Y and Z coordinates of the <see cref="GameObject"/> position.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <returns>Tuple (y, z) of world-space coordinates.</returns>
        public static (float y, float z) GetPositionYZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 position = gameObject.transform.position;
            return (position.y, position.z);
        }

        /// <summary>
        /// Gets the world-space position as a <see cref="Vector3"/>.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <returns>World-space position vector.</returns>
        public static Vector3 GetPosition3D(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.position;
        }
        /// <summary>
        /// Gets the world-space X, Y, and Z coordinates of the <see cref="GameObject"/> position.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <returns>Tuple (x, y, z) of world-space coordinates.</returns>
        public static (float x, float y, float z) GetPositionXYZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 position = gameObject.transform.position;
            return (position.x, position.y, position.z);
        }

        /// <summary>
        /// Gets the local-space X coordinate of the <see cref="GameObject"/> position.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <returns>Local-space X coordinate.</returns>
        public static float GetLocalPositionX(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.localPosition.x;
        }
        /// <summary>
        /// Gets the local-space Y coordinate of the <see cref="GameObject"/> position.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <returns>Local-space Y coordinate.</returns>
        public static float GetLocalPositionY(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.localPosition.y;
        }
        /// <summary>
        /// Gets the local-space Z coordinate of the <see cref="GameObject"/> position.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <returns>Local-space Z coordinate.</returns>
        public static float GetLocalPositionZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.localPosition.z;
        }

        /// <summary>
        /// Gets the local-space X and Y coordinates of the <see cref="GameObject"/> position.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <returns>Tuple (x, y) of local-space coordinates.</returns>
        public static (float x, float y) GetLocalPositionXY(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 position = gameObject.transform.localPosition;
            return (position.x, position.y);
        }
        /// <summary>
        /// Gets the local-space X and Z coordinates of the <see cref="GameObject"/> position.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <returns>Tuple (x, z) of local-space coordinates.</returns>
        public static (float x, float z) GetLocalPositionXZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 position = gameObject.transform.localPosition;
            return (position.x, position.z);
        }
        /// <summary>
        /// Gets the local-space Y and Z coordinates of the <see cref="GameObject"/> position.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <returns>Tuple (y, z) of local-space coordinates.</returns>
        public static (float y, float z) GetLocalPositionYZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 position = gameObject.transform.localPosition;
            return (position.y, position.z);
        }

        /// <summary>
        /// Gets the local-space position as a <see cref="Vector3"/>.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <returns>Local-space position vector.</returns>
        public static Vector3 GetLocalPosition3D(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.localPosition;
        }
        /// <summary>
        /// Gets the local-space X, Y, and Z coordinates of the <see cref="GameObject"/> position.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <returns>Tuple (x, y, z) of local-space coordinates.</returns>
        public static (float x, float y, float z) GetLocalPositionXYZ(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 position = gameObject.transform.localPosition;
            return (position.x, position.y, position.z);
        }

        /// <summary>
        /// Sets the world-space X coordinate while preserving Y and Z.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionX">New world-space X coordinate.</param>
        public static void SetPositionX(this GameObject gameObject, float positionX)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 position = gameObject.transform.position;

            float x = positionX;
            float y = position.y;
            float z = position.z;

            gameObject.transform.position = new Vector3(x, y, z);
        }
        /// <summary>
        /// Sets the world-space Y coordinate while preserving X and Z.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionY">New world-space Y coordinate.</param>
        public static void SetPositionY(this GameObject gameObject, float positionY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 position = gameObject.transform.position;

            float x = position.x;
            float y = positionY;
            float z = position.z;

            gameObject.transform.position = new Vector3(x, y, z);
        }
        /// <summary>
        /// Sets the world-space Z coordinate while preserving X and Y.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionZ">New world-space Z coordinate.</param>
        public static void SetPositionZ(this GameObject gameObject, float positionZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 position = gameObject.transform.position;

            float x = position.x;
            float y = position.y;
            float z = positionZ;

            gameObject.transform.position = new Vector3(x, y, z);
        }

        /// <summary>
        /// Sets the world-space X and Y coordinates while preserving Z.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionX">New world-space X coordinate.</param>
        /// <param name="positionY">New world-space Y coordinate.</param>
        public static void SetPositionXY(this GameObject gameObject, float positionX, float positionY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 position = gameObject.transform.position;

            float x = positionX;
            float y = positionY;
            float z = position.z;

            gameObject.transform.position = new Vector3(x, y, z);
        }
        /// <summary>
        /// Sets the world-space X and Y coordinates while preserving Z.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="position">Tuple (x, y) of world-space coordinates.</param>
        public static void SetPositionXY(this GameObject gameObject, (float x, float y) position)
        {
            SetPositionXY(gameObject, position.x, position.y);
        }

        /// <summary>
        /// Sets the world-space X and Z coordinates while preserving Y.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionX">New world-space X coordinate.</param>
        /// <param name="positionZ">New world-space Z coordinate.</param>
        public static void SetPositionXZ(this GameObject gameObject, float positionX, float positionZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 position = gameObject.transform.position;

            float x = positionX;
            float y = position.y;
            float z = positionZ;

            gameObject.transform.position = new Vector3(x, y, z);
        }
        /// <summary>
        /// Sets the world-space X and Z coordinates while preserving Y.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="position">Tuple (x, z) of world-space coordinates.</param>
        public static void SetPositionXZ(this GameObject gameObject, (float x, float z) position)
        {
            SetPositionXZ(gameObject, position.x, position.z);
        }

        /// <summary>
        /// Sets the world-space Y and Z coordinates while preserving X.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionY">New world-space Y coordinate.</param>
        /// <param name="positionZ">New world-space Z coordinate.</param>
        public static void SetPositionYZ(this GameObject gameObject, float positionY, float positionZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 position = gameObject.transform.position;

            float x = position.x;
            float y = positionY;
            float z = positionZ;

            gameObject.transform.position = new Vector3(x, y, z);
        }
        /// <summary>
        /// Sets the world-space Y and Z coordinates while preserving X.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="position">Tuple (y, z) of world-space coordinates.</param>
        public static void SetPositionYZ(this GameObject gameObject, (float y, float z) position)
        {
            SetPositionYZ(gameObject, position.y, position.z);
        }

        /// <summary>
        /// Sets the world-space X, Y, and Z coordinates.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionX">New world-space X coordinate.</param>
        /// <param name="positionY">New world-space Y coordinate.</param>
        /// <param name="positionZ">New world-space Z coordinate.</param>
        public static void SetPositionXYZ(this GameObject gameObject, float positionX, float positionY, float positionZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            float x = positionX;
            float y = positionY;
            float z = positionZ;

            gameObject.transform.position = new Vector3(x, y, z);
        }
        /// <summary>
        /// Sets the world-space X, Y, and Z coordinates.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="position">Tuple (x, y, z) of world-space coordinates.</param>
        public static void SetPositionXYZ(this GameObject gameObject, (float x, float y, float z) position)
        {
            SetPositionXYZ(gameObject, position.x, position.y, position.z);
        }

        /// <summary>
        /// Sets the world-space position using a <see cref="Vector3"/>.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="position">World-space position vector.</param>
        public static void SetPosition3D(this GameObject gameObject, Vector3 position)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            gameObject.transform.position = position;
        }

        /// <summary>
        /// Sets the local-space X coordinate while preserving Y and Z.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionX">New local-space X coordinate.</param>
        public static void SetLocalPositionX(this GameObject gameObject, float positionX)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 position = gameObject.transform.localPosition;

            float x = positionX;
            float y = position.y;
            float z = position.z;

            gameObject.transform.localPosition = new Vector3(x, y, z);
        }
        /// <summary>
        /// Sets the local-space Y coordinate while preserving X and Z.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionY">New local-space Y coordinate.</param>
        public static void SetLocalPositionY(this GameObject gameObject, float positionY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 position = gameObject.transform.localPosition;

            float x = position.x;
            float y = positionY;
            float z = position.z;

            gameObject.transform.localPosition = new Vector3(x, y, z);
        }
        /// <summary>
        /// Sets the local-space Z coordinate while preserving X and Y.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionZ">New local-space Z coordinate.</param>
        public static void SetLocalPositionZ(this GameObject gameObject, float positionZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 position = gameObject.transform.localPosition;

            float x = position.x;
            float y = position.y;
            float z = positionZ;

            gameObject.transform.localPosition = new Vector3(x, y, z);
        }

        /// <summary>
        /// Sets the local-space X and Y coordinates while preserving Z.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionX">New local-space X coordinate.</param>
        /// <param name="positionY">New local-space Y coordinate.</param>
        public static void SetLocalPositionXY(this GameObject gameObject, float positionX, float positionY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 position = gameObject.transform.localPosition;

            float x = positionX;
            float y = positionY;
            float z = position.z;

            gameObject.transform.localPosition = new Vector3(x, y, z);
        }
        /// <summary>
        /// Sets the local-space X and Y coordinates while preserving Z.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="position">Tuple (x, y) of local-space coordinates.</param>
        public static void SetLocalPositionXY(this GameObject gameObject, (float x, float y) position)
        {
            SetLocalPositionXY(gameObject, position.x, position.y);
        }

        /// <summary>
        /// Sets the local-space X and Z coordinates while preserving Y.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionX">New local-space X coordinate.</param>
        /// <param name="positionZ">New local-space Z coordinate.</param>
        public static void SetLocalPositionXZ(this GameObject gameObject, float positionX, float positionZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 position = gameObject.transform.localPosition;

            float x = positionX;
            float y = position.y;
            float z = positionZ;

            gameObject.transform.localPosition = new Vector3(x, y, z);
        }
        /// <summary>
        /// Sets the local-space X and Z coordinates while preserving Y.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="position">Tuple (x, z) of local-space coordinates.</param>
        public static void SetLocalPositionXZ(this GameObject gameObject, (float x, float z) position)
        {
            SetLocalPositionXZ(gameObject, position.x, position.z);
        }

        /// <summary>
        /// Sets the local-space Y and Z coordinates while preserving X.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionY">New local-space Y coordinate.</param>
        /// <param name="positionZ">New local-space Z coordinate.</param>
        public static void SetLocalPositionYZ(this GameObject gameObject, float positionY, float positionZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 position = gameObject.transform.localPosition;

            float x = position.x;
            float y = positionY;
            float z = positionZ;

            gameObject.transform.localPosition = new Vector3(x, y, z);
        }
        /// <summary>
        /// Sets the local-space Y and Z coordinates while preserving X.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="position">Tuple (y, z) of local-space coordinates.</param>
        public static void SetLocalPositionYZ(this GameObject gameObject, (float y, float z) position)
        {
            SetLocalPositionYZ(gameObject, position.y, position.z);
        }

        /// <summary>
        /// Sets the local-space X, Y, and Z coordinates.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionX">New local-space X coordinate.</param>
        /// <param name="positionY">New local-space Y coordinate.</param>
        /// <param name="positionZ">New local-space Z coordinate.</param>
        public static void SetLocalPositionXYZ(this GameObject gameObject, float positionX, float positionY, float positionZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            gameObject.transform.localPosition = new Vector3(positionX, positionY, positionZ);
        }
        /// <summary>
        /// Sets the local-space X, Y, and Z coordinates.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="position">Tuple (x, y, z) of local-space coordinates.</param>
        public static void SetLocalPositionXYZ(this GameObject gameObject, (float x, float y, float z) position)
        {
            SetLocalPositionXYZ(gameObject, position.x, position.y, position.z);
        }

        /// <summary>
        /// Sets the local-space position using a <see cref="Vector3"/>.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="position">Local-space position vector.</param>
        public static void SetLocalPosition3D(this GameObject gameObject, Vector3 position)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            gameObject.transform.localPosition = position;
        }

        /// <summary>
        /// Adds a delta to the world-space X coordinate while preserving Y and Z.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionX">Delta (offset) to add to world-space X.</param>
        public static void AddPositionX(this GameObject gameObject, float positionX)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 position = gameObject.transform.position;

            float x = position.x + positionX;
            float y = position.y;
            float z = position.z;

            gameObject.transform.position = new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds a delta to the world-space Y coordinate while preserving X and Z.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionY">Delta (offset) to add to world-space Y.</param>
        public static void AddPositionY(this GameObject gameObject, float positionY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 position = gameObject.transform.position;

            float x = position.x;
            float y = position.y + positionY;
            float z = position.z;

            gameObject.transform.position = new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds a delta to the world-space Z coordinate while preserving X and Y.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionZ">Delta (offset) to add to world-space Z.</param>
        public static void AddPositionZ(this GameObject gameObject, float positionZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 position = gameObject.transform.position;

            float x = position.x;
            float y = position.y;
            float z = position.z + positionZ;

            gameObject.transform.position = new Vector3(x, y, z);
        }

        /// <summary>
        /// Adds deltas to the world-space X and Y coordinates while preserving Z.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionX">Delta (offset) to add to world-space X.</param>
        /// <param name="positionY">Delta (offset) to add to world-space Y.</param>
        public static void AddPositionXY(this GameObject gameObject, float positionX, float positionY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 position = gameObject.transform.position;

            float x = position.x + positionX;
            float y = position.y + positionY;
            float z = position.z;

            gameObject.transform.position = new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds deltas to the world-space X and Y coordinates while preserving Z.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="position">Tuple (x, y) of deltas (offsets).</param>
        public static void AddPositionXY(this GameObject gameObject, (float x, float y) position)
        {
            AddPositionXY(gameObject, position.x, position.y);
        }

        /// <summary>
        /// Adds deltas to the world-space X and Z coordinates while preserving Y.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionX">Delta (offset) to add to world-space X.</param>
        /// <param name="positionZ">Delta (offset) to add to world-space Z.</param>
        public static void AddPositionXZ(this GameObject gameObject, float positionX, float positionZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 position = gameObject.transform.position;

            float x = position.x + positionX;
            float y = position.y;
            float z = position.z + positionZ;

            gameObject.transform.position = new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds deltas to the world-space X and Z coordinates while preserving Y.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="position">Tuple (x, z) of deltas (offsets).</param>
        public static void AddPositionXZ(this GameObject gameObject, (float x, float z) position)
        {
            AddPositionXZ(gameObject, position.x, position.z);
        }

        /// <summary>
        /// Adds deltas to the world-space Y and Z coordinates while preserving X.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionY">Delta (offset) to add to world-space Y.</param>
        /// <param name="positionZ">Delta (offset) to add to world-space Z.</param>
        public static void AddPositionYZ(this GameObject gameObject, float positionY, float positionZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 position = gameObject.transform.position;

            float x = position.x;
            float y = position.y + positionY;
            float z = position.z + positionZ;

            gameObject.transform.position = new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds deltas to the world-space Y and Z coordinates while preserving X.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="position">Tuple (y, z) of deltas (offsets).</param>
        public static void AddPositionYZ(this GameObject gameObject, (float y, float z) position)
        {
            AddPositionYZ(gameObject, position.y, position.z);
        }

        /// <summary>
        /// Adds deltas to the world-space X, Y, and Z coordinates.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionX">Delta (offset) to add to world-space X.</param>
        /// <param name="positionY">Delta (offset) to add to world-space Y.</param>
        /// <param name="positionZ">Delta (offset) to add to world-space Z.</param>
        public static void AddPositionXYZ(this GameObject gameObject, float positionX, float positionY, float positionZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 position = gameObject.transform.position;

            float x = position.x + positionX;
            float y = position.y + positionY;
            float z = position.z + positionZ;

            gameObject.transform.position = new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds deltas to the world-space X, Y, and Z coordinates.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="position">Tuple (x, y, z) of deltas (offsets).</param>
        public static void AddPositionXYZ(this GameObject gameObject, (float x, float y, float z) position)
        {
            AddPositionXYZ(gameObject, position.x, position.y, position.z);
        }

        /// <summary>
        /// Adds a world-space delta vector to the current position.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="addPosition">Delta (offset) in world space.</param>
        public static void AddPosition3D(this GameObject gameObject, Vector3 addPosition)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 position = gameObject.transform.position;
            position += addPosition;
            gameObject.transform.position = position;
        }

        /// <summary>
        /// Adds a delta to the local-space X coordinate while preserving Y and Z.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionX">Delta (offset) to add to local-space X.</param>
        public static void AddLocalPositionX(this GameObject gameObject, float positionX)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 position = gameObject.transform.localPosition;

            float x = position.x + positionX;
            float y = position.y;
            float z = position.z;

            gameObject.transform.localPosition = new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds a delta to the local-space Y coordinate while preserving X and Z.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionY">Delta (offset) to add to local-space Y.</param>
        public static void AddLocalPositionY(this GameObject gameObject, float positionY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 position = gameObject.transform.localPosition;

            float x = position.x;
            float y = position.y + positionY;
            float z = position.z;

            gameObject.transform.localPosition = new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds a delta to the local-space Z coordinate while preserving X and Y.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionZ">Delta (offset) to add to local-space Z.</param>
        public static void AddLocalPositionZ(this GameObject gameObject, float positionZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 position = gameObject.transform.localPosition;

            float x = position.x;
            float y = position.y;
            float z = position.z + positionZ;

            gameObject.transform.localPosition = new Vector3(x, y, z);
        }

        /// <summary>
        /// Adds deltas to the local-space X and Y coordinates while preserving Z.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionX">Delta (offset) to add to local-space X.</param>
        /// <param name="positionY">Delta (offset) to add to local-space Y.</param>
        public static void AddLocalPositionXY(this GameObject gameObject, float positionX, float positionY)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 position = gameObject.transform.localPosition;

            float x = position.x + positionX;
            float y = position.y + positionY;
            float z = position.z;

            gameObject.transform.localPosition = new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds deltas to the local-space X and Y coordinates while preserving Z.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="position">Tuple (x, y) of deltas (offsets).</param>
        public static void AddLocalPositionXY(this GameObject gameObject, (float x, float y) position)
        {
            AddLocalPositionXY(gameObject, position.x, position.y);
        }

        /// <summary>
        /// Adds deltas to the local-space X and Z coordinates while preserving Y.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionX">Delta (offset) to add to local-space X.</param>
        /// <param name="positionZ">Delta (offset) to add to local-space Z.</param>
        public static void AddLocalPositionXZ(this GameObject gameObject, float positionX, float positionZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 position = gameObject.transform.localPosition;

            float x = position.x + positionX;
            float y = position.y;
            float z = position.z + positionZ;

            gameObject.transform.localPosition = new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds deltas to the local-space X and Z coordinates while preserving Y.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="position">Tuple (x, z) of deltas (offsets).</param>
        public static void AddLocalPositionXZ(this GameObject gameObject, (float x, float z) position)
        {
            AddLocalPositionXZ(gameObject, position.x, position.z);
        }

        /// <summary>
        /// Adds deltas to the local-space Y and Z coordinates while preserving X.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionY">Delta (offset) to add to local-space Y.</param>
        /// <param name="positionZ">Delta (offset) to add to local-space Z.</param>
        public static void AddLocalPositionYZ(this GameObject gameObject, float positionY, float positionZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 position = gameObject.transform.localPosition;

            float x = position.x;
            float y = position.y + positionY;
            float z = position.z + positionZ;

            gameObject.transform.localPosition = new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds deltas to the local-space Y and Z coordinates while preserving X.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="position">Tuple (y, z) of deltas (offsets).</param>
        public static void AddLocalPositionYZ(this GameObject gameObject, (float y, float z) position)
        {
            AddLocalPositionYZ(gameObject, position.y, position.z);
        }

        /// <summary>
        /// Adds deltas to the local-space X, Y, and Z coordinates.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="positionX">Delta (offset) to add to local-space X.</param>
        /// <param name="positionY">Delta (offset) to add to local-space Y.</param>
        /// <param name="positionZ">Delta (offset) to add to local-space Z.</param>
        public static void AddLocalPositionXYZ(this GameObject gameObject, float positionX, float positionY, float positionZ)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Vector3 position = gameObject.transform.localPosition;

            float x = position.x + positionX;
            float y = position.y + positionY;
            float z = position.z + positionZ;

            gameObject.transform.localPosition = new Vector3(x, y, z);
        }
        /// <summary>
        /// Adds deltas to the local-space X, Y, and Z coordinates.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="position">Tuple (x, y, z) of deltas (offsets).</param>
        public static void AddLocalPositionXYZ(this GameObject gameObject, (float x, float y, float z) position)
        {
            AddLocalPositionXYZ(gameObject, position.x, position.y, position.z);
        }

        /// <summary>
        /// Adds a local-space delta vector to the current local position.
        /// </summary>
        /// <param name="gameObject">Target <see cref="GameObject"/>.</param>
        /// <param name="addPosition">Delta (offset) in local space.</param>
        public static void AddLocalPosition3D(this GameObject gameObject, Vector3 addPosition)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            Vector3 position = gameObject.transform.localPosition;
            position += addPosition;
            gameObject.transform.localPosition = position;
        }
    }
}
