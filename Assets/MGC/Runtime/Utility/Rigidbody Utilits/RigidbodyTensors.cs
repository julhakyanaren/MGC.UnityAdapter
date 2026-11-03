using UnityEngine;

namespace MGC.UnityAdapter
{
    /// <summary>
    /// Provides Unity-friendly extension methods for working with
    /// <see cref="Rigidbody"/> inertia properties (moment of inertia tensor
    /// and its orientation).
    ///
    /// This class exposes controlled access to:
    /// <list type="bullet">
    /// <item><see cref="Rigidbody.inertiaTensor"/></item>
    /// <item><see cref="Rigidbody.inertiaTensorRotation"/></item>
    /// </list>
    ///
    /// Inertia Tensor (<see cref="Vector3"/>)
    /// Represents the principal moments of inertia of the rigid body,
    /// expressed in the local inertia space.
    ///
    /// Conceptually:
    /// <list type="bullet">
    /// <item>
    /// X, Y, and Z components describe resistance to angular acceleration
    /// around the principal axes.
    /// </item>
    /// <item>
    /// Larger values indicate greater resistance to rotation around
    /// the corresponding axis.
    /// </item>
    /// </list>
    ///
    /// Notes:
    /// <list type="bullet">
    /// <item>All components must be non-negative.</item>
    /// <item>
    /// Unity computes this automatically from attached colliders,
    /// but it can be overridden for custom physical behavior.
    /// </item>
    /// </list>
    ///
    /// Inertia Tensor Rotation (<see cref="Quaternion"/>)
    /// Defines the orientation of the inertia tensor relative to the
    /// <see cref="Rigidbody"/> local space.
    ///
    /// This allows the principal inertia axes to be rotated independently
    /// of the <see cref="Transform"/> rotation.
    ///
    /// Operation Semantics
    ///
    /// Get*:
    /// <list type="bullet">
    /// <item>Reads the current inertia tensor values or orientation.</item>
    /// </list>
    ///
    /// Set* (absolute assignment):
    /// <list type="bullet">
    /// <item>
    /// Directly replaces <see cref="Rigidbody.inertiaTensor"/> or
    /// <see cref="Rigidbody.inertiaTensorRotation"/>.
    /// </item>
    /// <item>Equivalent to teleport-style assignment.</item>
    /// </list>
    ///
    /// Add* (incremental modification):
    /// <list type="bullet">
    /// <item>
    /// For inertia tensor: component-wise addition
    /// (<c>current + delta</c>).
    /// </item>
    /// <item>
    /// For inertia tensor rotation: local-space rotation using quaternion
    /// multiplication (<c>current * delta</c>).
    /// </item>
    /// </list>
    ///
    /// Reset:
    /// <list type="bullet">
    /// <item>
    /// <c>ResetInertiaTensor</c> restores Unity's automatically
    /// calculated inertia tensor.
    /// </item>
    /// </list>
    ///
    /// Important Notes:
    /// <list type="bullet">
    /// <item>
    /// <c>inertiaTensorRotation.eulerAngles</c> is a derived representation
    /// and may wrap or change discontinuously.
    /// </item>
    /// <item>
    /// For stable incremental orientation changes, prefer quaternion-based
    /// <c>AddInertiaTensorRotation</c> methods.
    /// </item>
    /// </list>
    ///
    /// Fluent API:
    /// All methods return the same <see cref="Rigidbody"/> instance,
    /// enabling method chaining.
    /// </summary>

    public static class RigidbodyTensors
    {
        /// <summary>
        /// Gets the inertia tensor (principal moments of inertia).
        /// </summary>
        public static Vector3 GetInertiaTensor(this Rigidbody rigidbody)
        {
            return rigidbody.inertiaTensor;
        }

        /// <summary>
        /// Gets the X component of the inertia tensor.
        /// </summary>
        public static float GetInertiaTensorX(this Rigidbody rigidbody)
        {
            return rigidbody.inertiaTensor.x;
        }
        /// <summary>
        /// Gets the Y component of the inertia tensor.
        /// </summary>
        public static float GetInertiaTensorY(this Rigidbody rigidbody)
        {
            return rigidbody.inertiaTensor.y;
        }
        /// <summary>
        /// Gets the Z component of the inertia tensor.
        /// </summary>
        public static float GetInertiaTensorZ(this Rigidbody rigidbody)
        {
            return rigidbody.inertiaTensor.z;
        }

        /// <summary>
        /// Gets the inertia tensor X and Y components.
        /// </summary>
        public static (float x, float y) GetInertiaTensorXY(this Rigidbody rigidbody)
        {
            return (rigidbody.inertiaTensor.x, rigidbody.inertiaTensor.y);
        }
        /// <summary>
        /// Gets the inertia tensor X and Z components.
        /// </summary>
        public static (float x, float z) GetInertiaTensorXZ(this Rigidbody rigidbody)
        {
            return (rigidbody.inertiaTensor.x, rigidbody.inertiaTensor.z);
        }
        /// <summary>
        /// Gets the inertia tensor Y and Z components.
        /// </summary>
        public static (float y, float z) GetInertiaTensorYZ(this Rigidbody rigidbody)
        {
            return (rigidbody.inertiaTensor.y, rigidbody.inertiaTensor.z);
        }

        /// <summary>
        /// Gets the inertia tensor as a tuple (x, y, z).
        /// </summary>
        public static (float x, float y, float z) GetInertiaTensorXYZ(this Rigidbody rigidbody)
        {
            return (rigidbody.inertiaTensor.x, rigidbody.inertiaTensor.y, rigidbody.inertiaTensor.z);
        }

        /// <summary>
        /// Gets the inertia tensor orientation as a quaternion.
        /// </summary>
        public static Quaternion GetInertiaTensorRotation(this Rigidbody rigidbody)
        {
            return rigidbody.inertiaTensorRotation;
        }
        /// <summary>
        /// Gets the inertia tensor orientation as Euler angles (degrees).
        /// </summary>
        public static Vector3 GetInertiaTensorRotationEuler(this Rigidbody rigidbody)
        {
            return rigidbody.inertiaTensorRotation.eulerAngles;
        }

        /// <summary>
        /// Sets the inertia tensor (absolute assignment).
        /// </summary>
        public static Rigidbody SetInertiaTensor(this Rigidbody rigidbody, Vector3 inertiaTensor)
        {
            rigidbody.inertiaTensor = inertiaTensor;
            return rigidbody;
        }
        /// <summary>
        /// Sets the inertia tensor using component values.
        /// </summary>
        public static Rigidbody SetInertiaTensor(this Rigidbody rigidbody, float inertiaX, float inertiaY, float inertiaZ)
        {
            rigidbody.inertiaTensor = new Vector3(inertiaX, inertiaY, inertiaZ);
            return rigidbody;
        }

        /// <summary>
        /// Adds a delta to the inertia tensor (component-wise).
        /// </summary>
        public static Rigidbody AddInertiaTensor(this Rigidbody rigidbody, Vector3 delta)
        {
            rigidbody.inertiaTensor += delta;
            return rigidbody;
        }

        /// <summary>
        /// Adds a local-space rotation to the inertia tensor orientation.
        /// </summary>
        public static Rigidbody AddInertiaTensorRotation(this Rigidbody rigidbody, Quaternion deltaRotation)
        {
            rigidbody.inertiaTensorRotation *= deltaRotation;
            return rigidbody;
        }
        /// <summary>
        /// Adds a local-space Euler rotation (degrees) to the inertia tensor orientation.
        /// </summary>
        public static Rigidbody AddInertiaTensorRotationEuler(this Rigidbody rigidbody, Vector3 addEulerDegrees)
        {
            rigidbody.inertiaTensorRotation *= Quaternion.Euler(addEulerDegrees);
            return rigidbody;
        }

        /// <summary>
        /// Resets the inertia tensor to Unity's automatically calculated values.
        /// </summary>
        public static Rigidbody ResetInertiaTensor(this Rigidbody rigidbody)
        {
            rigidbody.ResetInertiaTensor();
            return rigidbody;
        }
    }
}
