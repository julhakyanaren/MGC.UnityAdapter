using UnityEngine;

namespace MGC.UnityAdapter
{
    /// <summary>
    /// Provides Unity-friendly extension methods for reading and modifying Rigidbody and Rigidbody2D
    /// velocity-related state: linear velocity, angular velocity, and (for Rigidbody2D) inertia.
    ///
    /// Included groups:
    /// - Get* methods for accessing velocity components, tuples, speed (magnitude), and angular velocity.
    /// - Set* methods for direct assignment of velocities and (for Rigidbody2D) inertia.
    /// - Add* methods for incremental changes (delta-based updates) to velocities and (for Rigidbody2D) inertia.
    ///
    /// Notes:
    /// - Linear velocity is expressed in units per second (world units / second).
    /// - Rigidbody.angularVelocity is expressed as a Vector3 in radians per second.
    /// - Rigidbody2D.angularVelocity is expressed in degrees per second.
    /// - Rigidbody2D.inertia represents the rotational inertia used by the 2D physics solver.
    ///
    /// Fluent API:
    /// - All methods return the same Rigidbody/Rigidbody2D instance to allow chaining for Set*/Add* methods.
    /// </summary>
    public static class RigidbodyVelocity
    {
        /// <summary>
        /// Gets the current 2D linear velocity vector.
        /// </summary>
        public static Vector2 GetVelocity(this Rigidbody2D rigidbody)
        {
            return rigidbody.velocity;
        }

        /// <summary>
        /// Gets the X component of the current 2D linear velocity.
        /// </summary>
        public static float GetVelocityX(this Rigidbody2D rigidbody)
        {
            return rigidbody.velocity.x;
        }
        /// <summary>
        /// Gets the Y component of the current 2D linear velocity.
        /// </summary>
        public static float GetVelocityY(this Rigidbody2D rigidbody)
        {
            return rigidbody.velocity.y;
        }

        /// <summary>
        /// Gets the current 2D linear velocity as a tuple (x, y).
        /// </summary>
        public static (float x, float y) GetVelocityXY(this Rigidbody2D rigidbody)
        {
            return (rigidbody.velocity.x, rigidbody.velocity.y);
        }

        /// <summary>
        /// Gets the current 2D linear speed (magnitude of velocity).
        /// </summary>
        public static float GetSpeedLinar(this Rigidbody2D rigidbody)
        {
            return rigidbody.velocity.magnitude;
        }

        /// <summary>
        /// Gets the current 2D angular velocity in degrees per second.
        /// </summary>
        public static float GetAngularVelocity(this Rigidbody2D rigidbody)
        {
            return rigidbody.angularVelocity;
        }

        /// <summary>
        /// Gets the current 2D rotational inertia value used by the solver.
        /// </summary>
        public static float GetInertia(this Rigidbody2D rigidbody)
        {
            return rigidbody.inertia;
        }

        /// <summary>
        /// Gets the current 3D linear velocity vector.
        /// </summary>
        public static Vector3 GetVelocity(this Rigidbody rigidbody)
        {
            return rigidbody.velocity;
        }

        /// <summary>
        /// Gets the X component of the current 3D linear velocity.
        /// </summary>
        public static float GetVelocityX(this Rigidbody rigidbody)
        {
            return rigidbody.velocity.x;
        }
        /// <summary>
        /// Gets the Y component of the current 3D linear velocity.
        /// </summary>
        public static float GetVelocityY(this Rigidbody rigidbody)
        {
            return rigidbody.velocity.y;
        }
        /// <summary>
        /// Gets the Z component of the current 3D linear velocity.
        /// </summary>
        public static float GetVelocityZ(this Rigidbody rigidbody)
        {
            return rigidbody.velocity.z;
        }

        /// <summary>
        /// Gets the X and Y components of the current 3D linear velocity as a tuple (x, y).
        /// </summary>
        public static (float x, float y) GetVelocityXY(this Rigidbody rigidbody)
        {
            return (rigidbody.velocity.x, rigidbody.velocity.y);
        }
        /// <summary>
        /// Gets the X and Z components of the current 3D linear velocity as a tuple (x, z).
        /// </summary>
        public static (float x, float z) GetVelocityXZ(this Rigidbody rigidbody)
        {
            return (rigidbody.velocity.x, rigidbody.velocity.z);
        }
        /// <summary>
        /// Gets the Y and Z components of the current 3D linear velocity as a tuple (y, z).
        /// </summary>
        public static (float y, float z) GetVelocityYZ(this Rigidbody rigidbody)
        {
            return (rigidbody.velocity.y, rigidbody.velocity.z);
        }

        /// <summary>
        /// Gets the X, Y and Z components of the current 3D linear velocity as a tuple (x, y, z).
        /// </summary>
        public static (float x, float y, float z) GetVelocityXYZ(this Rigidbody rigidbody)
        {
            return (rigidbody.velocity.x, rigidbody.velocity.y, rigidbody.velocity.z);
        }

        /// <summary>
        /// Gets the current 3D linear speed (magnitude of velocity).
        /// </summary>
        public static float GetSpeedLinar(this Rigidbody rigidbody)
        {
            return rigidbody.velocity.magnitude;
        }

        /// <summary>
        /// Gets the current 3D angular velocity vector in radians per second.
        /// </summary>
        public static Vector3 GetAngularVelocity(this Rigidbody rigidbody)
        {
            return rigidbody.angularVelocity;
        }

        /// <summary>
        /// Gets the X component of the current 3D angular velocity (radians per second).
        /// </summary>
        public static float GetAngularVelocityX(this Rigidbody rigidbody)
        {
            return rigidbody.angularVelocity.x;
        }
        /// <summary>
        /// Gets the Y component of the current 3D angular velocity (radians per second).
        /// </summary>
        public static float GetAngularVelocityY(this Rigidbody rigidbody)
        {
            return rigidbody.angularVelocity.y;
        }
        /// <summary>
        /// Gets the Z component of the current 3D angular velocity (radians per second).
        /// </summary>
        public static float GetAngularVelocityZ(this Rigidbody rigidbody)
        {
            return rigidbody.angularVelocity.z;
        }

        /// <summary>
        /// Gets the X and Y components of the current 3D angular velocity as a tuple (x, y).
        /// </summary>
        public static (float x, float y) GetAngularVelocityXY(this Rigidbody rigidbody)
        {
            return (rigidbody.angularVelocity.x, rigidbody.angularVelocity.y);
        }
        /// <summary>
        /// Gets the X and Z components of the current 3D angular velocity as a tuple (x, z).
        /// </summary>
        public static (float x, float z) GetAngularVelocityXZ(this Rigidbody rigidbody)
        {
            return (rigidbody.angularVelocity.x, rigidbody.angularVelocity.z);
        }
        /// <summary>
        /// Gets the Y and Z components of the current 3D angular velocity as a tuple (y, z).
        /// </summary>
        public static (float y, float z) GetAngularVelocityYZ(this Rigidbody rigidbody)
        {
            return (rigidbody.angularVelocity.y, rigidbody.angularVelocity.z);
        }

        /// <summary>
        /// Gets the X, Y and Z components of the current 3D angular velocity as a tuple (x, y, z).
        /// </summary>
        public static (float x, float y, float z) GetAngularVelocityXYZ(this Rigidbody rigidbody)
        {
            return (rigidbody.angularVelocity.x, rigidbody.angularVelocity.y, rigidbody.angularVelocity.z);
        }

        /// <summary>
        /// Sets the 2D linear velocity by assigning Rigidbody2D.velocity directly.
        /// </summary>
        public static Rigidbody2D SetVelocity(this Rigidbody2D rigidbody, Vector2 velocity)
        {
            rigidbody.velocity = velocity;
            return rigidbody;
        }
        /// <summary>
        /// Sets the 2D linear velocity from X and Y components by assigning Rigidbody2D.velocity directly.
        /// </summary>
        public static Rigidbody2D SetVelocity(this Rigidbody2D rigidbody, float velocityX, float velocityY)
        {
            rigidbody.velocity = new Vector2(velocityX, velocityY);
            return rigidbody;
        }
        /// <summary>
        /// Sets the 2D linear velocity from a tuple (x, y) by assigning Rigidbody2D.velocity directly.
        /// </summary>
        public static Rigidbody2D SetVelocity(this Rigidbody2D rigidbody, (float x, float y) velocity)
        {
            rigidbody.velocity = new Vector2(velocity.x, velocity.y);
            return rigidbody;
        }

        /// <summary>
        /// Sets the X component of the 2D linear velocity, preserving the current Y component.
        /// </summary>
        public static Rigidbody2D SetVelocityX(this Rigidbody2D rigidbody, float velocityX)
        {
            float x = velocityX;
            float y = rigidbody.velocity.y;

            rigidbody.velocity = new Vector2(x, y);
            return rigidbody;
        }
        /// <summary>
        /// Sets the Y component of the 2D linear velocity, preserving the current X component.
        /// </summary>
        public static Rigidbody2D SetVelocityY(this Rigidbody2D rigidbody, float velocityY)
        {
            float x = rigidbody.velocity.x;
            float y = rigidbody.velocity.y;

            rigidbody.velocity = new Vector2(x, y);
            return rigidbody;
        }

        /// <summary>
        /// Sets the 2D angular velocity in degrees per second by assigning Rigidbody2D.angularVelocity directly.
        /// </summary>
        public static Rigidbody2D SetAngularVelocity(this Rigidbody2D rigidbody, float angularVelocity)
        {
            rigidbody.angularVelocity = angularVelocity;
            return rigidbody;
        }

        /// <summary>
        /// Sets the 2D rotational inertia value by assigning Rigidbody2D.inertia directly.
        /// </summary>
        public static Rigidbody2D SetInertia(this Rigidbody2D rigidbody, float inertia)
        {
            rigidbody.inertia = inertia;
            return rigidbody;
        }

        /// <summary>
        /// Sets the 3D linear velocity by assigning Rigidbody.velocity directly.
        /// </summary>
        public static Rigidbody SetVelocity(this Rigidbody rigidbody, Vector3 velocity)
        {
            rigidbody.velocity = velocity;
            return rigidbody;
        }

        /// <summary>
        /// Sets the X component of the 3D linear velocity, preserving Y and Z.
        /// </summary>
        public static Rigidbody SetVelocityX(this Rigidbody rigidbody, float velocityX)
        {
            float x = velocityX;
            float y = rigidbody.velocity.y;
            float z = rigidbody.velocity.z;

            rigidbody.velocity = new Vector3(x, y, z);
            return rigidbody;
        }
        /// <summary>
        /// Sets the Y component of the 3D linear velocity, preserving X and Z.
        /// </summary>
        public static Rigidbody SetVelocityY(this Rigidbody rigidbody, float velocityY)
        {
            float x = rigidbody.velocity.x;
            float y = velocityY;
            float z = rigidbody.velocity.z;

            rigidbody.velocity = new Vector3(x, y, z);
            return rigidbody;
        }
        /// <summary>
        /// Sets the Z component of the 3D linear velocity, preserving X and Y.
        /// </summary>
        public static Rigidbody SetVelocityZ(this Rigidbody rigidbody, float velocityZ)
        {
            float x = rigidbody.velocity.x;
            float y = rigidbody.velocity.y;
            float z = velocityZ;

            rigidbody.velocity = new Vector3(x, y, z);
            return rigidbody;
        }

        /// <summary>
        /// Sets the X and Y components of the 3D linear velocity, preserving Z.
        /// </summary>
        public static Rigidbody SetVelocityXY(this Rigidbody rigidbody, float velocityX, float velocityY)
        {
            float x = velocityX;
            float y = velocityY;
            float z = rigidbody.velocity.z;

            rigidbody.velocity = new Vector3(x, y, z);
            return rigidbody;
        }
        /// <summary>
        /// Sets the X and Y components of the 3D linear velocity from a tuple (x, y), preserving Z.
        /// </summary>
        public static Rigidbody SetVelocityXY(this Rigidbody rigidbody, (float x, float y) velocity)
        {
            return SetVelocityXY(rigidbody, velocity.x, velocity.y);
        }

        /// <summary>
        /// Sets the X and Z components of the 3D linear velocity, preserving Y.
        /// </summary>
        public static Rigidbody SetVelocityXZ(this Rigidbody rigidbody, float velocityX, float velocityZ)
        {
            float x = velocityX;
            float y = rigidbody.velocity.y;
            float z = velocityZ;

            rigidbody.velocity = new Vector3(x, y, z);
            return rigidbody;
        }
        /// <summary>
        /// Sets the X and Z components of the 3D linear velocity from a tuple (x, z), preserving Y.
        /// </summary>
        public static Rigidbody SetVelocityXZ(this Rigidbody rigidbody, (float x, float z) velocity)
        {
            return SetVelocityXZ(rigidbody, velocity.x, velocity.z);
        }

        /// <summary>
        /// Sets the Y and Z components of the 3D linear velocity, preserving X.
        /// </summary>
        public static Rigidbody SetVelocityYZ(this Rigidbody rigidbody, float velocityY, float velocityZ)
        {
            float x = rigidbody.velocity.x;
            float y = velocityY;
            float z = velocityZ;

            rigidbody.velocity = new Vector3(x, y, z);
            return rigidbody;
        }
        /// <summary>
        /// Sets the Y and Z components of the 3D linear velocity from a tuple (y, z), preserving X.
        /// </summary>
        public static Rigidbody SetVelocityYZ(this Rigidbody rigidbody, (float y, float z) velocity)
        {
            return SetVelocityYZ(rigidbody, velocity.y, velocity.z);
        }

        /// <summary>
        /// Sets the X, Y and Z components of the 3D linear velocity by assigning Rigidbody.velocity directly.
        /// </summary>
        public static Rigidbody SetVelocityXYZ(this Rigidbody rigidbody, float velocityX, float velocityY, float velocityZ)
        {
            float x = velocityX;
            float y = velocityY;
            float z = velocityZ;

            rigidbody.velocity = new Vector3(x, y, z);
            return rigidbody;
        }
        /// <summary>
        /// Sets the 3D linear velocity from a tuple (x, y, z) by assigning Rigidbody.velocity directly.
        /// </summary>
        public static Rigidbody SetVelocityXYZ(this Rigidbody rigidbody, (float x, float y, float z) velocity)
        {
            return SetVelocityXYZ(rigidbody, velocity.x, velocity.y, velocity.z);
        }

        /// <summary>
        /// Sets the 3D angular velocity vector in radians per second by assigning Rigidbody.angularVelocity directly.
        /// </summary>
        public static Rigidbody SetAngularVelocity(this Rigidbody rigidbody, Vector3 velocity)
        {
            rigidbody.angularVelocity = velocity;
            return rigidbody;
        }

        /// <summary>
        /// Sets the X component of the 3D angular velocity, preserving Y and Z.
        /// </summary>
        public static Rigidbody SetAngularVelocityX(this Rigidbody rigidbody, float velocityX)
        {
            float x = velocityX;
            float y = rigidbody.angularVelocity.y;
            float z = rigidbody.angularVelocity.z;

            rigidbody.angularVelocity = new Vector3(x, y, z);
            return rigidbody;
        }
        /// <summary>
        /// Sets the Y component of the 3D angular velocity, preserving X and Z.
        /// </summary>
        public static Rigidbody SetAngularVelocityY(this Rigidbody rigidbody, float velocityY)
        {
            float x = rigidbody.angularVelocity.x;
            float y = velocityY;
            float z = rigidbody.angularVelocity.z;

            rigidbody.angularVelocity = new Vector3(x, y, z);
            return rigidbody;
        }
        /// <summary>
        /// Sets the Z component of the 3D angular velocity, preserving X and Y.
        /// </summary>
        public static Rigidbody SetAngularVelocityZ(this Rigidbody rigidbody, float velocityZ)
        {
            float x = rigidbody.angularVelocity.x;
            float y = rigidbody.angularVelocity.y;
            float z = velocityZ;

            rigidbody.angularVelocity = new Vector3(x, y, z);
            return rigidbody;
        }

        /// <summary>
        /// Sets the X and Y components of the 3D angular velocity, preserving Z.
        /// </summary>
        public static Rigidbody SetAngularVelocityXY(this Rigidbody rigidbody, float velocityX, float velocityY)
        {
            float x = velocityX;
            float y = velocityY;
            float z = rigidbody.angularVelocity.z;

            rigidbody.angularVelocity = new Vector3(x, y, z);
            return rigidbody;
        }
        /// <summary>
        /// Sets the X and Y components of the 3D angular velocity from a tuple (x, y), preserving Z.
        /// </summary>
        public static Rigidbody SetAngularVelocityXY(this Rigidbody rigidbody, (float x, float y) velocity)
        {
            return SetAngularVelocityXY(rigidbody, velocity.x, velocity.y);
        }

        /// <summary>
        /// Sets the X and Z components of the 3D angular velocity, preserving Y.
        /// </summary>
        public static Rigidbody SetAngularVelocityXZ(this Rigidbody rigidbody, float velocityX, float velocityZ)
        {
            float x = velocityX;
            float y = rigidbody.angularVelocity.y;
            float z = velocityZ;

            rigidbody.angularVelocity = new Vector3(x, y, z);
            return rigidbody;
        }
        /// <summary>
        /// Sets the X and Z components of the 3D angular velocity from a tuple (x, z), preserving Y.
        /// </summary>
        public static Rigidbody SetAngularVelocityXZ(this Rigidbody rigidbody, (float x, float z) velocity)
        {
            return SetAngularVelocityXZ(rigidbody, velocity.x, velocity.z);
        }

        /// <summary>
        /// Sets the Y and Z components of the 3D angular velocity, preserving X.
        /// </summary>
        public static Rigidbody SetAngularVelocityYZ(this Rigidbody rigidbody, float velocityY, float velocityZ)
        {
            float x = rigidbody.angularVelocity.x;
            float y = velocityY;
            float z = velocityZ;

            rigidbody.angularVelocity = new Vector3(x, y, z);
            return rigidbody;
        }
        /// <summary>
        /// Sets the Y and Z components of the 3D angular velocity from a tuple (y, z), preserving X.
        /// </summary>
        public static Rigidbody SetAngularVelocityYZ(this Rigidbody rigidbody, (float y, float z) velocity)
        {
            return SetAngularVelocityYZ(rigidbody, velocity.y, velocity.z);
        }

        /// <summary>
        /// Sets the X, Y and Z components of the 3D angular velocity by assigning Rigidbody.angularVelocity directly.
        /// </summary>
        public static Rigidbody SetAngularVelocityXYZ(this Rigidbody rigidbody, float velocityX, float velocityY, float velocityZ)
        {
            float x = velocityX;
            float y = velocityY;
            float z = velocityZ;

            rigidbody.angularVelocity = new Vector3(x, y, z);
            return rigidbody;
        }
        /// <summary>
        /// Sets the 3D angular velocity from a tuple (x, y, z) by assigning Rigidbody.angularVelocity directly.
        /// </summary>
        public static Rigidbody SetAngularVelocityXYZ(this Rigidbody rigidbody, (float x, float y, float z) velocity)
        {
            return SetAngularVelocityXYZ(rigidbody, velocity.x, velocity.y, velocity.z);
        }

        /// <summary>
        /// Adds a delta to the 2D linear velocity.
        /// Equivalent to: rigidbody.velocity += delta.
        /// </summary>
        public static Rigidbody2D AddVelocity(this Rigidbody2D rigidbody, Vector2 delta)
        {
            rigidbody.velocity += delta;
            return rigidbody;
        }
        /// <summary>
        /// Adds deltas to the X and Y components of the 2D linear velocity.
        /// </summary>
        public static Rigidbody2D AddVelocity(this Rigidbody2D rigidbody, float addX, float addY)
        {
            Vector2 current = rigidbody.velocity;

            rigidbody.velocity = new Vector2(
                current.x + addX,
                current.y + addY
            );
            return rigidbody;
        }
        /// <summary>
        /// Adds a tuple delta (x, y) to the 2D linear velocity.
        /// </summary>
        public static Rigidbody2D AddVelocity(this Rigidbody2D rigidbody, (float x, float y) delta)
        {
            return AddVelocity(rigidbody, delta.x, delta.y);
        }

        /// <summary>
        /// Adds a delta to the X component of the 2D linear velocity, preserving Y.
        /// </summary>
        public static Rigidbody2D AddVelocityX(this Rigidbody2D rigidbody, float addX)
        {
            Vector2 current = rigidbody.velocity;

            rigidbody.velocity = new Vector2(
                current.x + addX,
                current.y
            );
            return rigidbody;
        }
        /// <summary>
        /// Adds a delta to the Y component of the 2D linear velocity, preserving X.
        /// </summary>
        public static Rigidbody2D AddVelocityY(this Rigidbody2D rigidbody, float addY)
        {
            Vector2 current = rigidbody.velocity;

            rigidbody.velocity = new Vector2(
                current.x,
                current.y + addY
            );
            return rigidbody;
        }

        /// <summary>
        /// Adds a delta to the 2D angular velocity in degrees per second.
        /// </summary>
        public static Rigidbody2D AddAngularVelocity(this Rigidbody2D rigidbody, float addAngularVelocity)
        {
            rigidbody.angularVelocity += addAngularVelocity;
            return rigidbody;
        }

        /// <summary>
        /// Adds a delta to the 2D inertia value used by the solver.
        /// </summary>
        public static Rigidbody2D AddInertia(this Rigidbody2D rigidbody, float addInertia)
        {
            rigidbody.inertia += addInertia;
            return rigidbody;
        }

        /// <summary>
        /// Adds a delta to the 3D linear velocity.
        /// Equivalent to: rigidbody.velocity += delta.
        /// </summary>
        public static Rigidbody AddVelocity(this Rigidbody rigidbody, Vector3 delta)
        {
            rigidbody.velocity += delta;
            return rigidbody;
        }
        /// <summary>
        /// Adds deltas to the X, Y and Z components of the 3D linear velocity.
        /// </summary>
        public static Rigidbody AddVelocity(this Rigidbody rigidbody, float addX, float addY, float addZ)
        {
            Vector3 current = rigidbody.velocity;

            rigidbody.velocity = new Vector3(
                current.x + addX,
                current.y + addY,
                current.z + addZ
            );
            return rigidbody;
        }
        /// <summary>
        /// Adds a tuple delta (x, y, z) to the 3D linear velocity.
        /// </summary>
        public static Rigidbody AddVelocity(this Rigidbody rigidbody, (float x, float y, float z) delta)
        {
            return AddVelocity(rigidbody, delta.x, delta.y, delta.z);
        }

        /// <summary>
        /// Adds a delta to the X component of the 3D linear velocity, preserving Y and Z.
        /// </summary>
        public static Rigidbody AddVelocityX(this Rigidbody rigidbody, float addX)
        {
            Vector3 current = rigidbody.velocity;

            rigidbody.velocity = new Vector3(
                current.x + addX,
                current.y,
                current.z
            );
            return rigidbody;
        }
        /// <summary>
        /// Adds a delta to the Y component of the 3D linear velocity, preserving X and Z.
        /// </summary>
        public static Rigidbody AddVelocityY(this Rigidbody rigidbody, float addY)
        {
            Vector3 current = rigidbody.velocity;

            rigidbody.velocity = new Vector3(
                current.x,
                current.y + addY,
                current.z
            );
            return rigidbody;
        }
        /// <summary>
        /// Adds a delta to the Z component of the 3D linear velocity, preserving X and Y.
        /// </summary>
        public static Rigidbody AddVelocityZ(this Rigidbody rigidbody, float addZ)
        {
            Vector3 current = rigidbody.velocity;

            rigidbody.velocity = new Vector3(
                current.x,
                current.y,
                current.z + addZ
            );
            return rigidbody;
        }

        /// <summary>
        /// Adds deltas to the X and Y components of the 3D linear velocity, preserving Z.
        /// </summary>
        public static Rigidbody AddVelocityXY(this Rigidbody rigidbody, float addX, float addY)
        {
            Vector3 current = rigidbody.velocity;

            rigidbody.velocity = new Vector3(
                current.x + addX,
                current.y + addY,
                current.z
            );
            return rigidbody;
        }
        /// <summary>
        /// Adds a tuple delta (x, y) to the X and Y components of the 3D linear velocity, preserving Z.
        /// </summary>
        public static Rigidbody AddVelocityXY(this Rigidbody rigidbody, (float x, float y) delta)
        {
            return AddVelocityXY(rigidbody, delta.x, delta.y);
        }

        /// <summary>
        /// Adds deltas to the X and Z components of the 3D linear velocity, preserving Y.
        /// </summary>
        public static Rigidbody AddVelocityXZ(this Rigidbody rigidbody, float addX, float addZ)
        {
            Vector3 current = rigidbody.velocity;

            rigidbody.velocity = new Vector3(
                current.x + addX,
                current.y,
                current.z + addZ
            );
            return rigidbody;
        }
        /// <summary>
        /// Adds a tuple delta (x, z) to the X and Z components of the 3D linear velocity, preserving Y.
        /// </summary>
        public static Rigidbody AddVelocityXZ(this Rigidbody rigidbody, (float x, float z) delta)
        {
            return AddVelocityXZ(rigidbody, delta.x, delta.z);
        }

        /// <summary>
        /// Adds deltas to the Y and Z components of the 3D linear velocity, preserving X.
        /// </summary>
        public static Rigidbody AddVelocityYZ(this Rigidbody rigidbody, float addY, float addZ)
        {
            Vector3 current = rigidbody.velocity;

            rigidbody.velocity = new Vector3(
                current.x,
                current.y + addY,
                current.z + addZ
            );
            return rigidbody;
        }
        /// <summary>
        /// Adds a tuple delta (y, z) to the Y and Z components of the 3D linear velocity, preserving X.
        /// </summary>
        public static Rigidbody AddVelocityYZ(this Rigidbody rigidbody, (float y, float z) delta)
        {
            return AddVelocityYZ(rigidbody, delta.y, delta.z);
        }

        /// <summary>
        /// Adds a delta to the 3D angular velocity (radians per second).
        /// Equivalent to: rigidbody.angularVelocity += delta.
        /// </summary>
        public static Rigidbody AddAngularVelocity(this Rigidbody rigidbody, Vector3 delta)
        {
            rigidbody.angularVelocity += delta;
            return rigidbody;
        }
        /// <summary>
        /// Adds deltas to the X, Y and Z components of the 3D angular velocity (radians per second).
        /// </summary>
        public static Rigidbody AddAngularVelocity(this Rigidbody rigidbody, float addX, float addY, float addZ)
        {
            Vector3 current = rigidbody.angularVelocity;

            rigidbody.angularVelocity = new Vector3(
                current.x + addX,
                current.y + addY,
                current.z + addZ
            );
            return rigidbody;
        }
        /// <summary>
        /// Adds a tuple delta (x, y, z) to the 3D angular velocity (radians per second).
        /// </summary>
        public static Rigidbody AddAngularVelocity(this Rigidbody rigidbody, (float x, float y, float z) delta)
        {
            return AddAngularVelocity(rigidbody, delta.x, delta.y, delta.z);
        }

        /// <summary>
        /// Adds a delta to the X component of the 3D angular velocity, preserving Y and Z.
        /// </summary>
        public static Rigidbody AddAngularVelocityX(this Rigidbody rigidbody, float addX)
        {
            Vector3 current = rigidbody.angularVelocity;

            rigidbody.angularVelocity = new Vector3(
                current.x + addX,
                current.y,
                current.z
            );
            return rigidbody;
        }
        /// <summary>
        /// Adds a delta to the Y component of the 3D angular velocity, preserving X and Z.
        /// </summary>
        public static Rigidbody AddAngularVelocityY(this Rigidbody rigidbody, float addY)
        {
            Vector3 current = rigidbody.angularVelocity;

            rigidbody.angularVelocity = new Vector3(
                current.x,
                current.y + addY,
                current.z
            );
            return rigidbody;
        }
        /// <summary>
        /// Adds a delta to the Z component of the 3D angular velocity, preserving X and Y.
        /// </summary>
        public static Rigidbody AddAngularVelocityZ(this Rigidbody rigidbody, float addZ)
        {
            Vector3 current = rigidbody.angularVelocity;

            rigidbody.angularVelocity = new Vector3(
                current.x,
                current.y,
                current.z + addZ
            );
            return rigidbody;
        }

        /// <summary>
        /// Adds deltas to the X and Y components of the 3D angular velocity, preserving Z.
        /// </summary>
        public static Rigidbody AddAngularVelocityXY(this Rigidbody rigidbody, float addX, float addY)
        {
            Vector3 current = rigidbody.angularVelocity;

            rigidbody.angularVelocity = new Vector3(
                current.x + addX,
                current.y + addY,
                current.z
            );
            return rigidbody;
        }
        /// <summary>
        /// Adds deltas to the X and Y components of the 3D angular velocity, preserving Z, using a tuple (x, y).
        /// </summary>
        public static Rigidbody AddAngularVelocityXY(this Rigidbody rigidbody, (float x, float y) delta)
        {
            return AddAngularVelocityXY(rigidbody, delta.x, delta.y);
        }

        /// <summary>
        /// Adds deltas to the X and Z components of the 3D angular velocity, preserving Y.
        /// </summary>
        public static Rigidbody AddAngularVelocityXZ(this Rigidbody rigidbody, float addX, float addZ)
        {
            Vector3 current = rigidbody.angularVelocity;

            rigidbody.angularVelocity = new Vector3(
                current.x + addX,
                current.y,
                current.z + addZ
            );
            return rigidbody;
        }
        /// <summary>
        /// Adds deltas to the X and Z components of the 3D angular velocity, preserving Y, using a tuple (x, z).
        /// </summary>
        public static Rigidbody AddAngularVelocityXZ(this Rigidbody rigidbody, (float x, float z) delta)
        {
            return AddAngularVelocityXZ(rigidbody, delta.x, delta.z);
        }

        /// <summary>
        /// Adds deltas to the Y and Z components of the 3D angular velocity, preserving X.
        /// </summary>
        public static Rigidbody AddAngularVelocityYZ(this Rigidbody rigidbody, float addY, float addZ)
        {
            Vector3 current = rigidbody.angularVelocity;

            rigidbody.angularVelocity = new Vector3(
                current.x,
                current.y + addY,
                current.z + addZ
            );
            return rigidbody;
        }
        /// <summary>
        /// Adds deltas to the Y and Z components of the 3D angular velocity, preserving X, using a tuple (y, z).
        /// </summary>
        public static Rigidbody AddAngularVelocityYZ(this Rigidbody rigidbody, (float y, float z) delta)
        {
            return AddAngularVelocityYZ(rigidbody, delta.y, delta.z);
        }

        /// <summary>
        /// Adds deltas to the X, Y and Z components of the 3D angular velocity.
        /// </summary>
        public static Rigidbody AddAngularVelocityXYZ(this Rigidbody rigidbody, float addX, float addY, float addZ)
        {
            Vector3 current = rigidbody.angularVelocity;

            rigidbody.angularVelocity = new Vector3(
                current.x + addX,
                current.y + addY,
                current.z + addZ
            );
            return rigidbody;
        }
        /// <summary>
        /// Adds deltas to the X, Y and Z components of the 3D angular velocity using a tuple (x, y, z).
        /// </summary>
        public static Rigidbody AddAngularVelocityXYZ(this Rigidbody rigidbody, (float x, float y, float z) delta)
        {
            return AddAngularVelocityXYZ(rigidbody, delta.x, delta.y, delta.z);
        }

    }
}
