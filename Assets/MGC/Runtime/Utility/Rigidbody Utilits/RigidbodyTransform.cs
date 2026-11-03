using UnityEngine;

namespace MGC.UnityAdapter
{
    /// <summary>
    /// Unity-friendly extension methods for setting and moving
    /// <see cref="Rigidbody"/> / <see cref="Rigidbody2D"/> position and rotation,
    /// supporting both teleport-style setters and physics-aware movement.
    /// </summary>
    ///
    /// <remarks>
    /// <para>
    /// The API distinguishes two operation categories:
    /// </para>
    ///
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///       <b>Direct setters (teleport-style):</b>
    ///       <c>SetPosition*</c> / <c>SetRotation*</c> assign
    ///       <c>Rigidbody.position</c> / <c>Rigidbody.rotation</c> directly.
    ///       Applied immediately and may bypass some physics interactions.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       <b>Physics-aware movement:</b>
    ///       <c>MovePosition*</c> / <c>MoveRotation*</c> schedule the change
    ///       for the next physics step and participate in collision solving.
    ///     </description>
    ///   </item>
    /// </list>
    ///
    /// <para>
    /// Rotation helpers are provided for both <see cref="Rigidbody2D"/> (Z-axis, degrees,
    /// with Rad/Deg convenience methods) and <see cref="Rigidbody"/> (Quaternion-based,
    /// with Euler helpers for common axis and pair-axis use cases).
    /// </para>
    ///
    /// <para>
    /// Incremental rotation via <c>AddRotation*</c> / <c>AddMoveRotation*</c>
    /// applies a local-space delta using quaternion multiplication
    /// (<c>current * delta</c>) and is more stable than modifying Euler components.
    /// </para>
    ///
    /// <para>
    /// All methods return the same Rigidbody/Rigidbody2D instance to allow fluent chaining.
    /// </para>
    /// </remarks>
    public static class RigidbodyTransform
    {
        /// <summary>
        /// Gets the current 2D rotation in degrees (around the Z axis).
        /// </summary>
        public static float GetRotation(this Rigidbody2D rigidbody)
        {
            return rigidbody.rotation;
        }
        /// <summary>
        /// Gets the current 2D rotation in radians (around the Z axis).
        /// Unity stores Rigidbody2D.rotation in degrees, this method converts degrees to radians.
        /// </summary>
        public static float GetRotationRad(this Rigidbody2D rigidbody)
        {
            return rigidbody.rotation * Mathf.Deg2Rad;
        }

        /// <summary>
        /// Sets the 2D rotation in degrees (teleport-style).
        /// This assigns Rigidbody2D.rotation directly.
        /// </summary>
        /// <param name="rotationDegrees">Rotation angle in degrees.</param>
        public static Rigidbody2D SetRotation(this Rigidbody2D rigidbody, float rotationDegrees)
        {
            rigidbody.rotation = rotationDegrees;
            return rigidbody;
        }
        /// <summary>
        /// Sets the 2D rotation in radians (teleport-style).
        /// This converts radians to degrees and assigns Rigidbody2D.rotation directly.
        /// </summary>
        /// <param name="rotationRadians">Rotation angle in radians.</param>
        public static Rigidbody2D SetRotationRad(this Rigidbody2D rigidbody, float rotationRadians)
        {
            float rotationDegrees = rotationRadians * Mathf.Rad2Deg;

            rigidbody.rotation = rotationDegrees;
            return rigidbody;
        }

        /// <summary>
        /// Adds an incremental 2D rotation in degrees (teleport-style).
        /// This modifies Rigidbody2D.rotation directly by adding <paramref name="addDegrees"/>.
        /// </summary>
        /// <param name="addDegrees">Rotation delta in degrees.</param>
        public static Rigidbody2D AddRotation(this Rigidbody2D rigidbody, float addDegrees)
        {
            float rotation = rigidbody.rotation + addDegrees;

            rigidbody.rotation = rotation;
            return rigidbody;
        }
        /// <summary>
        /// Adds an incremental 2D rotation in radians (teleport-style).
        /// This converts radians to degrees and adds it to Rigidbody2D.rotation directly.
        /// </summary>
        /// <param name="addRadians">Rotation delta in radians.</param>
        public static Rigidbody2D AddRotationRad(this Rigidbody2D rigidbody, float addRadians)
        {
            float rotationDegrees = rigidbody.rotation + addRadians * Mathf.Rad2Deg;

            rigidbody.rotation = rotationDegrees;
            return rigidbody;
        }

        /// <summary>
        /// Moves the 2D rotation in degrees using physics-aware scheduling.
        /// Calls Rigidbody2D.MoveRotation.
        /// </summary>
        /// <param name="rotationDegrees">Target rotation in degrees.</param>
        public static Rigidbody2D MoveRotation(this Rigidbody2D rigidbody, float rotationDegrees)
        {
            rigidbody.MoveRotation(rotationDegrees);
            return rigidbody;
        }
        /// <summary>
        /// Moves the 2D rotation in radians using physics-aware scheduling.
        /// This converts radians to degrees and calls Rigidbody2D.MoveRotation.
        /// </summary>
        /// <param name="rotationRadians">Target rotation in radians.</param>
        public static Rigidbody2D MoveRotationRad(this Rigidbody2D rigidbody, float rotationRadians)
        {
            float rotationDegrees = rotationRadians * Mathf.Rad2Deg;

            rigidbody.MoveRotation(rotationDegrees);
            return rigidbody;
        }

        /// <summary>
        /// Adds an incremental 2D rotation in degrees using physics-aware scheduling.
        /// This reads the current rotation, adds <paramref name="addDegrees"/>, and calls Rigidbody2D.MoveRotation.
        /// </summary>
        /// <param name="addDegrees">Rotation delta in degrees.</param>
        public static Rigidbody2D AddMoveRotation(this Rigidbody2D rigidbody, float addDegrees)
        {
            float rotation = rigidbody.rotation + addDegrees;

            rigidbody.MoveRotation(rotation);
            return rigidbody;
        }
        /// <summary>
        /// Adds an incremental 2D rotation in radians using physics-aware scheduling.
        /// This converts radians to degrees, adds to the current rotation, and calls Rigidbody2D.MoveRotation.
        /// </summary>
        /// <param name="addRadians">Rotation delta in radians.</param>
        public static Rigidbody2D AddMoveRotationRad(this Rigidbody2D rigidbody, float addRadians)
        {
            float rotationDegrees = rigidbody.rotation + addRadians * Mathf.Rad2Deg;

            rigidbody.MoveRotation(rotationDegrees);
            return rigidbody;
        }


        /// <summary>
        /// Gets the current 3D rotation as a quaternion.
        /// </summary>
        public static Quaternion GetRotation(this Rigidbody rigidbody)
        {
            return rigidbody.rotation;
        }
        
        /// <summary>
        /// Gets the current 3D rotation as Euler angles in degrees.
        /// </summary>
        public static Vector3 GetRotationEuler(this Rigidbody rigidbody)
        {
            return rigidbody.rotation.eulerAngles;
        }

        /// <summary>
        /// Gets the X Euler angle (in degrees) of the Rigidbody rotation.
        /// </summary>
        public static float GetRotationEulerX(this Rigidbody rigidbody)
        {
            return rigidbody.rotation.eulerAngles.x;
        }
        /// <summary>
        /// Gets the Y Euler angle (in degrees) of the Rigidbody rotation.
        /// </summary>
        public static float GetRotationEulerY(this Rigidbody rigidbody)
        {
            return rigidbody.rotation.eulerAngles.y;
        }
        /// <summary>
        /// Gets the Z Euler angle (in degrees) of the Rigidbody rotation.
        /// </summary>
        public static float GetRotationEulerZ(this Rigidbody rigidbody)
        {
            return rigidbody.rotation.eulerAngles.z;
        }

        /// <summary>
        /// Gets the X and Y Euler angles (in degrees) of the Rigidbody rotation.
        /// </summary>
        public static (float x, float y) GetRotationEulerXY(this Rigidbody rigidbody)
        {
            Vector3 euler = rigidbody.rotation.eulerAngles;

            return (euler.x, euler.y);
        }
        /// <summary>
        /// Gets the X and Z Euler angles (in degrees) of the Rigidbody rotation.
        /// </summary>
        public static (float x, float z) GetRotationEulerXZ(this Rigidbody rigidbody)
        {
            Vector3 euler = rigidbody.rotation.eulerAngles;

            return (euler.x, euler.z);
        }
        /// <summary>
        /// Gets the Y and Z Euler angles (in degrees) of the Rigidbody rotation.
        /// </summary>
        public static (float y, float z) GetRotationEulerYZ(this Rigidbody rigidbody)
        {
            Vector3 euler = rigidbody.rotation.eulerAngles;

            return (euler.y, euler.z);
        }

        /// <summary>
        /// Sets the 3D rotation (teleport-style) by assigning Rigidbody.rotation directly.
        /// </summary>
        /// <param name="rotation">Target rotation quaternion.</param>
        public static Rigidbody SetRotation(this Rigidbody rigidbody, Quaternion rotation)
        {
            rigidbody.rotation = rotation;
            return rigidbody;
        }

        /// <summary>
        /// Sets the 3D rotation from Euler angles in degrees (teleport-style).
        /// This converts Euler angles to a quaternion and assigns Rigidbody.rotation directly.
        /// </summary>
        /// <param name="eulerDegrees">Euler angles in degrees.</param>
        public static Rigidbody SetRotationEuler(this Rigidbody rigidbody, Vector3 eulerDegrees)
        {
            Quaternion rotation = Quaternion.Euler(eulerDegrees);

            rigidbody.rotation = rotation;
            return rigidbody;
        }
        /// <summary>
        /// Sets the X Euler angle (in degrees) of the Rigidbody rotation (teleport-style),
        /// preserving the current Y and Z Euler angles.
        /// </summary>
        public static Rigidbody SetRotationEulerX(this Rigidbody rigidbody, float eulerXDegrees)
        {
            Vector3 euler = rigidbody.rotation.eulerAngles;
            float eulerY = euler.y;
            float eulerZ = euler.z;

            rigidbody.rotation = Quaternion.Euler(eulerXDegrees, eulerY, eulerZ);
            return rigidbody;
        }
        /// <summary>
        /// Sets the Y Euler angle (in degrees) of the Rigidbody rotation (teleport-style),
        /// preserving the current X and Z Euler angles.
        /// </summary>
        public static Rigidbody SetRotationEulerY(this Rigidbody rigidbody, float eulerYDegrees)
        {
            Vector3 euler = rigidbody.rotation.eulerAngles;
            float eulerX = euler.x;
            float eulerZ = euler.z;

            rigidbody.rotation = Quaternion.Euler(eulerX, eulerYDegrees, eulerZ);
            return rigidbody;
        }
        /// <summary>
        /// Sets the Z Euler angle (in degrees) of the Rigidbody rotation (teleport-style),
        /// preserving the current X and Y Euler angles.
        /// </summary>
        public static Rigidbody SetRotationEulerZ(this Rigidbody rigidbody, float eulerZDegrees)
        {
            Vector3 euler = rigidbody.rotation.eulerAngles;
            float eulerX = euler.x;
            float eulerY = euler.y;

            rigidbody.rotation = Quaternion.Euler(eulerX, eulerY, eulerZDegrees);
            return rigidbody;
        }

        /// <summary>
        /// Sets the X and Y Euler angles (in degrees) of the Rigidbody rotation (teleport-style),
        /// preserving the current Z Euler angle.
        /// </summary>
        public static Rigidbody SetRotationEulerXY(this Rigidbody rigidbody, float eulerXDegrees, float eulerYDegrees)
        {
            Vector3 euler = rigidbody.rotation.eulerAngles;
            float eulerZ = euler.z;

            rigidbody.rotation = Quaternion.Euler(eulerXDegrees, eulerYDegrees, eulerZ);
            return rigidbody;
        }
        /// <summary>
        /// Sets the X and Y Euler angles (in degrees) of the Rigidbody rotation (teleport-style),
        /// preserving the current Z Euler angle.
        /// </summary>
        public static Rigidbody SetRotationEulerXY(this Rigidbody rigidbody, (float x, float y) eulerDegrees)
        {
            return SetRotationEulerXY(rigidbody, eulerDegrees.x, eulerDegrees.y);
        }

        /// <summary>
        /// Sets the X and Z Euler angles (in degrees) of the Rigidbody rotation (teleport-style),
        /// preserving the current Y Euler angle.
        /// </summary>
        public static Rigidbody SetRotationEulerXZ(this Rigidbody rigidbody, float eulerXDegrees, float eulerZDegrees)
        {
            Vector3 euler = rigidbody.rotation.eulerAngles;
            float eulerY = euler.y;

            rigidbody.rotation = Quaternion.Euler(eulerXDegrees, eulerY, eulerZDegrees);
            return rigidbody;
        }
        /// <summary>
        /// Sets the X and Z Euler angles (in degrees) of the Rigidbody rotation (teleport-style),
        /// preserving the current Y Euler angle.
        /// </summary>
        public static Rigidbody SetRotationEulerXZ(this Rigidbody rigidbody, (float x, float z) eulerDegrees)
        {
            return SetRotationEulerXZ(rigidbody, eulerDegrees.x, eulerDegrees.z);
        }

        /// <summary>
        /// Sets the Y and Z Euler angles (in degrees) of the Rigidbody rotation (teleport-style),
        /// preserving the current X Euler angle.
        /// </summary>
        public static Rigidbody SetRotationEulerYZ(this Rigidbody rigidbody, float eulerYDegrees, float eulerZDegrees)
        {
            Vector3 euler = rigidbody.rotation.eulerAngles;
            float eulerX = euler.x;

            rigidbody.rotation = Quaternion.Euler(eulerX, eulerYDegrees, eulerZDegrees);
            return rigidbody;
        }
        /// <summary>
        /// Sets the Y and Z Euler angles (in degrees) of the Rigidbody rotation (teleport-style),
        /// preserving the current X Euler angle.
        /// </summary>
        public static Rigidbody SetRotationEulerYZ(this Rigidbody rigidbody, (float y, float z) eulerDegrees)
        {
            return SetRotationEulerYZ(rigidbody, eulerDegrees.y, eulerDegrees.z);
        }

        /// <summary>
        /// Adds an incremental 3D rotation (teleport-style).
        /// The delta is applied in local space using quaternion multiplication: current * delta.
        /// </summary>
        /// <param name="deltaRotation">Rotation delta quaternion.</param>
        public static Rigidbody AddRotation(this Rigidbody rigidbody, Quaternion deltaRotation)
        {
            Quaternion rotation = rigidbody.rotation * deltaRotation;

            rigidbody.rotation = rotation;
            return rigidbody;
        }

        /// <summary>
        /// Adds an incremental 3D rotation from Euler angles in degrees (teleport-style).
        /// The delta is applied in local space using quaternion multiplication: current * delta.
        /// </summary>
        /// <param name="addEulerDegrees">Euler rotation delta in degrees.</param>
        public static Rigidbody AddRotationEuler(this Rigidbody rigidbody, Vector3 addEulerDegrees)
        {
            Quaternion delta = Quaternion.Euler(addEulerDegrees);
            Quaternion rotation = rigidbody.rotation * delta;

            rigidbody.rotation = rotation;
            return rigidbody;
        }
        /// <summary>
        /// Adds a local-space Euler delta around the X axis (in degrees) (teleport-style).
        /// Internally applies quaternion multiplication: current * delta.
        /// </summary>
        public static Rigidbody AddRotationEulerX(this Rigidbody rigidbody, float addEulerXDegrees)
        {
            Quaternion delta = Quaternion.Euler(addEulerXDegrees, 0.0f, 0.0f);
            Quaternion rotation = rigidbody.rotation * delta;

            rigidbody.rotation = rotation;
            return rigidbody;
        }
        /// <summary>
        /// Adds a local-space Euler delta around the Y axis (in degrees) (teleport-style).
        /// Internally applies quaternion multiplication: current * delta.
        /// </summary>
        public static Rigidbody AddRotationEulerY(this Rigidbody rigidbody, float addEulerYDegrees)
        {
            Quaternion delta = Quaternion.Euler(0.0f, addEulerYDegrees, 0.0f);
            Quaternion rotation = rigidbody.rotation * delta;

            rigidbody.rotation = rotation;
            return rigidbody;
        }
        /// <summary>
        /// Adds a local-space Euler delta around the Z axis (in degrees) (teleport-style).
        /// Internally applies quaternion multiplication: current * delta.
        /// </summary>
        public static Rigidbody AddRotationEulerZ(this Rigidbody rigidbody, float addEulerZDegrees)
        {
            Quaternion delta = Quaternion.Euler(0.0f, 0.0f, addEulerZDegrees);
            Quaternion rotation = rigidbody.rotation * delta;

            rigidbody.rotation = rotation;
            return rigidbody;
        }

        /// <summary>
        /// Adds a local-space Euler delta around X and Y axes (in degrees) (teleport-style).
        /// Internally applies quaternion multiplication: current * delta.
        /// </summary>
        public static Rigidbody AddRotationEulerXY(this Rigidbody rigidbody, float addEulerXDegrees, float addEulerYDegrees)
        {
            Quaternion delta = Quaternion.Euler(addEulerXDegrees, addEulerYDegrees, 0.0f);
            Quaternion rotation = rigidbody.rotation * delta;

            rigidbody.rotation = rotation;
            return rigidbody;
        }
        /// <summary>
        /// Adds a local-space Euler delta around X and Y axes (in degrees) (teleport-style) using a tuple.
        /// </summary>
        public static Rigidbody AddRotationEulerXY(this Rigidbody rigidbody, (float x, float y) addEulerDegrees)
        {
            return AddRotationEulerXY(rigidbody, addEulerDegrees.x, addEulerDegrees.y);
        }

        /// <summary>
        /// Adds a local-space Euler delta around X and Z axes (in degrees) (teleport-style).
        /// Internally applies quaternion multiplication: current * delta.
        /// </summary>
        public static Rigidbody AddRotationEulerXZ(this Rigidbody rigidbody, float addEulerXDegrees, float addEulerZDegrees)
        {
            Quaternion delta = Quaternion.Euler(addEulerXDegrees, 0.0f, addEulerZDegrees);
            Quaternion rotation = rigidbody.rotation * delta;

            rigidbody.rotation = rotation;
            return rigidbody;
        }
        /// <summary>
        /// Adds a local-space Euler delta around X and Z axes (in degrees) (teleport-style) using a tuple.
        /// </summary>
        public static Rigidbody AddRotationEulerXZ(this Rigidbody rigidbody, (float x, float z) addEulerDegrees)
        {
            return AddRotationEulerXZ(rigidbody, addEulerDegrees.x, addEulerDegrees.z);
        }

        /// <summary>
        /// Adds a local-space Euler delta around Y and Z axes (in degrees) (teleport-style).
        /// Internally applies quaternion multiplication: current * delta.
        /// </summary>
        public static Rigidbody AddRotationEulerYZ(this Rigidbody rigidbody, float addEulerYDegrees, float addEulerZDegrees)
        {
            Quaternion delta = Quaternion.Euler(0.0f, addEulerYDegrees, addEulerZDegrees);
            Quaternion rotation = rigidbody.rotation * delta;

            rigidbody.rotation = rotation;
            return rigidbody;
        }
        /// <summary>
        /// Adds a local-space Euler delta around Y and Z axes (in degrees) (teleport-style) using a tuple.
        /// </summary>
        public static Rigidbody AddRotationEulerYZ(this Rigidbody rigidbody, (float y, float z) addEulerDegrees)
        {
            return AddRotationEulerYZ(rigidbody, addEulerDegrees.y, addEulerDegrees.z);
        }

        /// <summary>
        /// Moves the 3D rotation using physics-aware scheduling.
        /// Calls Rigidbody.MoveRotation.
        /// </summary>
        /// <param name="rotation">Target rotation quaternion.</param>
        public static Rigidbody MoveRotation(this Rigidbody rigidbody, Quaternion rotation)
        {
            rigidbody.MoveRotation(rotation);
            return rigidbody;
        }

        /// <summary>
        /// Moves the 3D rotation from Euler angles in degrees using physics-aware scheduling.
        /// This converts Euler angles to a quaternion and calls Rigidbody.MoveRotation.
        /// </summary>
        /// <param name="eulerDegrees">Target Euler angles in degrees.</param>
        public static Rigidbody MoveRotationEuler(this Rigidbody rigidbody, Vector3 eulerDegrees)
        {
            Quaternion rotation = Quaternion.Euler(eulerDegrees);

            rigidbody.MoveRotation(rotation);
            return rigidbody;
        }
        /// <summary>
        /// Moves the rotation by setting the X Euler angle (in degrees) using physics-aware scheduling,
        /// preserving the current Y and Z Euler angles. Calls Rigidbody.MoveRotation.
        /// </summary>
        public static Rigidbody MoveRotationEulerX(this Rigidbody rigidbody, float eulerXDegrees)
        {
            Vector3 euler = rigidbody.rotation.eulerAngles;
            float eulerY = euler.y;
            float eulerZ = euler.z;

            rigidbody.MoveRotation(Quaternion.Euler(eulerXDegrees, eulerY, eulerZ));
            return rigidbody;
        }
        /// <summary>
        /// Moves the rotation by setting the Y Euler angle (in degrees) using physics-aware scheduling,
        /// preserving the current X and Z Euler angles. Calls Rigidbody.MoveRotation.
        /// </summary>
        public static Rigidbody MoveRotationEulerY(this Rigidbody rigidbody, float eulerYDegrees)
        {
            Vector3 euler = rigidbody.rotation.eulerAngles;
            float eulerX = euler.x;
            float eulerZ = euler.z;

            rigidbody.MoveRotation(Quaternion.Euler(eulerX, eulerYDegrees, eulerZ));
            return rigidbody;
        }
        /// <summary>
        /// Moves the rotation by setting the Z Euler angle (in degrees) using physics-aware scheduling,
        /// preserving the current X and Y Euler angles. Calls Rigidbody.MoveRotation.
        /// </summary>
        public static Rigidbody MoveRotationEulerZ(this Rigidbody rigidbody, float eulerZDegrees)
        {
            Vector3 euler = rigidbody.rotation.eulerAngles;
            float eulerX = euler.x;
            float eulerY = euler.y;

            rigidbody.MoveRotation(Quaternion.Euler(eulerX, eulerY, eulerZDegrees));
            return rigidbody;
        }

        /// <summary>
        /// Moves the rotation by setting X and Y Euler angles (in degrees) using physics-aware scheduling,
        /// preserving the current Z Euler angle. Calls Rigidbody.MoveRotation.
        /// </summary>
        public static Rigidbody MoveRotationEulerXY(this Rigidbody rigidbody, float eulerXDegrees, float eulerYDegrees)
        {
            Vector3 euler = rigidbody.rotation.eulerAngles;
            float eulerZ = euler.z;

            rigidbody.MoveRotation(Quaternion.Euler(eulerXDegrees, eulerYDegrees, eulerZ));
            return rigidbody;
        }
        /// <summary>
        /// Moves the rotation by setting X and Y Euler angles (in degrees) using physics-aware scheduling,
        /// preserving the current Z Euler angle. Calls Rigidbody.MoveRotation.
        /// </summary>
        public static Rigidbody MoveRotationEulerXY(this Rigidbody rigidbody, (float x, float y) eulerDegrees)
        {
            return MoveRotationEulerXY(rigidbody, eulerDegrees.x, eulerDegrees.y);
        }

        /// <summary>
        /// Moves the rotation by setting X and Z Euler angles (in degrees) using physics-aware scheduling,
        /// preserving the current Y Euler angle. Calls Rigidbody.MoveRotation.
        /// </summary>
        public static Rigidbody MoveRotationEulerXZ(this Rigidbody rigidbody, float eulerXDegrees, float eulerZDegrees)
        {
            Vector3 euler = rigidbody.rotation.eulerAngles;
            float eulerY = euler.y;

            rigidbody.MoveRotation(Quaternion.Euler(eulerXDegrees, eulerY, eulerZDegrees));
            return rigidbody;
        }
        /// <summary>
        /// Moves the rotation by setting X and Z Euler angles (in degrees) using physics-aware scheduling,
        /// preserving the current Y Euler angle. Calls Rigidbody.MoveRotation.
        /// </summary>
        public static Rigidbody MoveRotationEulerXZ(this Rigidbody rigidbody, (float x, float z) eulerDegrees)
        {
            return MoveRotationEulerXZ(rigidbody, eulerDegrees.x, eulerDegrees.z);
        }

        /// <summary>
        /// Moves the rotation by setting Y and Z Euler angles (in degrees) using physics-aware scheduling,
        /// preserving the current X Euler angle. Calls Rigidbody.MoveRotation.
        /// </summary>
        public static Rigidbody MoveRotationEulerYZ(this Rigidbody rigidbody, float eulerYDegrees, float eulerZDegrees)
        {
            Vector3 euler = rigidbody.rotation.eulerAngles;
            float eulerX = euler.x;

            rigidbody.MoveRotation(Quaternion.Euler(eulerX, eulerYDegrees, eulerZDegrees));
            return rigidbody;
        }
        /// <summary>
        /// Moves the rotation by setting Y and Z Euler angles (in degrees) using physics-aware scheduling,
        /// preserving the current X Euler angle. Calls Rigidbody.MoveRotation.
        /// </summary>
        public static Rigidbody MoveRotationEulerYZ(this Rigidbody rigidbody, (float y, float z) eulerDegrees)
        {
            return MoveRotationEulerYZ(rigidbody, eulerDegrees.y, eulerDegrees.z);
        }

        /// <summary>
        /// Adds an incremental 3D rotation using physics-aware scheduling.
        /// The delta is applied in local space using quaternion multiplication: current * delta,
        /// then Rigidbody.MoveRotation is called with the resulting target rotation.
        /// </summary>
        /// <param name="deltaRotation">Rotation delta quaternion.</param>
        public static Rigidbody AddMoveRotation(this Rigidbody rigidbody, Quaternion deltaRotation)
        {
            Quaternion rotation = rigidbody.rotation * deltaRotation;

            rigidbody.MoveRotation(rotation);
            return rigidbody;
        }

        /// <summary>
        /// Adds an incremental 3D rotation from Euler angles in degrees using physics-aware scheduling.
        /// The delta is applied in local space using quaternion multiplication: current * delta,
        /// then Rigidbody.MoveRotation is called with the resulting target rotation.
        /// </summary>
        /// <param name="addEulerDegrees">Euler rotation delta in degrees.</param>
        public static Rigidbody AddMoveRotationEuler(this Rigidbody rigidbody, Vector3 addEulerDegrees)
        {
            Quaternion delta = Quaternion.Euler(addEulerDegrees);
            Quaternion rotation = rigidbody.rotation * delta;

            rigidbody.MoveRotation(rotation);
            return rigidbody;
        }
        /// <summary>
        /// Adds a local-space Euler delta around the X axis (in degrees) using physics-aware scheduling.
        /// Internally applies quaternion multiplication: current * delta, then calls Rigidbody.MoveRotation.
        /// </summary>
        public static Rigidbody AddMoveRotationEulerX(this Rigidbody rigidbody, float addEulerXDegrees)
        {
            Quaternion delta = Quaternion.Euler(addEulerXDegrees, 0.0f, 0.0f);
            Quaternion rotation = rigidbody.rotation * delta;

            rigidbody.MoveRotation(rotation);
            return rigidbody;
        }
        /// <summary>
        /// Adds a local-space Euler delta around the Y axis (in degrees) using physics-aware scheduling.
        /// Internally applies quaternion multiplication: current * delta, then calls Rigidbody.MoveRotation.
        /// </summary>
        public static Rigidbody AddMoveRotationEulerY(this Rigidbody rigidbody, float addEulerYDegrees)
        {
            Quaternion delta = Quaternion.Euler(0.0f, addEulerYDegrees, 0.0f);
            Quaternion rotation = rigidbody.rotation * delta;

            rigidbody.MoveRotation(rotation);
            return rigidbody;
        }
        /// <summary>
        /// Adds a local-space Euler delta around the Z axis (in degrees) using physics-aware scheduling.
        /// Internally applies quaternion multiplication: current * delta, then calls Rigidbody.MoveRotation.
        /// </summary>
        public static Rigidbody AddMoveRotationEulerZ(this Rigidbody rigidbody, float addEulerZDegrees)
        {
            Quaternion delta = Quaternion.Euler(0.0f, 0.0f, addEulerZDegrees);
            Quaternion rotation = rigidbody.rotation * delta;

            rigidbody.MoveRotation(rotation);
            return rigidbody;
        }

        /// <summary>
        /// Adds a local-space Euler delta around X and Y axes (in degrees) using physics-aware scheduling.
        /// Internally applies quaternion multiplication: current * delta, then calls Rigidbody.MoveRotation.
        /// </summary>
        public static Rigidbody AddMoveRotationEulerXY(this Rigidbody rigidbody, float addEulerXDegrees, float addEulerYDegrees)
        {
            Quaternion delta = Quaternion.Euler(addEulerXDegrees, addEulerYDegrees, 0.0f);
            Quaternion rotation = rigidbody.rotation * delta;

            rigidbody.MoveRotation(rotation);
            return rigidbody;
        }
        /// <summary>
        /// Adds a local-space Euler delta around X and Y axes (in degrees) using physics-aware scheduling with a tuple.
        /// </summary>
        public static Rigidbody AddMoveRotationEulerXY(this Rigidbody rigidbody, (float x, float y) addEulerDegrees)
        {
            return AddMoveRotationEulerXY(rigidbody, addEulerDegrees.x, addEulerDegrees.y);
        }

        /// <summary>
        /// Adds a local-space Euler delta around X and Z axes (in degrees) using physics-aware scheduling.
        /// Internally applies quaternion multiplication: current * delta, then calls Rigidbody.MoveRotation.
        /// </summary>
        public static Rigidbody AddMoveRotationEulerXZ(this Rigidbody rigidbody, float addEulerXDegrees, float addEulerZDegrees)
        {
            Quaternion delta = Quaternion.Euler(addEulerXDegrees, 0.0f, addEulerZDegrees);
            Quaternion rotation = rigidbody.rotation * delta;

            rigidbody.MoveRotation(rotation);
            return rigidbody;
        }
        /// <summary>
        /// Adds a local-space Euler delta around X and Z axes (in degrees) using physics-aware scheduling with a tuple.
        /// </summary>
        public static Rigidbody AddMoveRotationEulerXZ(this Rigidbody rigidbody, (float x, float z) addEulerDegrees)
        {
            return AddMoveRotationEulerXZ(rigidbody, addEulerDegrees.x, addEulerDegrees.z);
        }

        /// <summary>
        /// Adds a local-space Euler delta around Y and Z axes (in degrees) using physics-aware scheduling.
        /// Internally applies quaternion multiplication: current * delta, then calls Rigidbody.MoveRotation.
        /// </summary>
        public static Rigidbody AddMoveRotationEulerYZ(this Rigidbody rigidbody, float addEulerYDegrees, float addEulerZDegrees)
        {
            Quaternion delta = Quaternion.Euler(0.0f, addEulerYDegrees, addEulerZDegrees);
            Quaternion rotation = rigidbody.rotation * delta;

            rigidbody.MoveRotation(rotation);
            return rigidbody;
        }
        /// <summary>
        /// Adds a local-space Euler delta around Y and Z axes (in degrees) using physics-aware scheduling with a tuple.
        /// </summary>
        public static Rigidbody AddMoveRotationEulerYZ(this Rigidbody rigidbody, (float y, float z) addEulerDegrees)
        {
            return AddMoveRotationEulerYZ(rigidbody, addEulerDegrees.y, addEulerDegrees.z);
        }

        /// <summary>
        /// Gets the current 2D position.
        /// </summary>
        public static Vector2 GetPosition(this Rigidbody2D rigidbody)
        {
            return rigidbody.position;
        }
        /// <summary>
        /// Gets the X coordinate of the current 2D position.
        /// </summary>
        public static float GetPositionX(this Rigidbody2D rigidbody)
        {
            return rigidbody.position.x;
        }
        /// <summary>
        /// Gets the Y coordinate of the current 2D position.
        /// </summary>
        public static float GetPositionY(this Rigidbody2D rigidbody)
        {
            return rigidbody.position.y;
        }
        /// <summary>
        /// Gets the current 2D position as a tuple (x, y).
        /// </summary>
        public static (float x, float y) GetPositionXY(this Rigidbody2D rigidbody)
        {
            return (rigidbody.position.x, rigidbody.position.y);
        }

        /// <summary>
        /// Sets the 2D position (teleport-style) by assigning Rigidbody2D.position directly.
        /// </summary>
        /// <param name="position">Target position.</param>
        public static Rigidbody2D SetPosition(this Rigidbody2D rigidbody, Vector2 position)
        {
            rigidbody.position = position;
            return rigidbody;
        }

        /// <summary>
        /// Sets the X coordinate of the 2D position (teleport-style), preserving Y.
        /// </summary>
        /// <param name="positionX">Target X position.</param>
        public static Rigidbody2D SetPositionX(this Rigidbody2D rigidbody, float positionX)
        {
            float positionY = rigidbody.position.y;

            rigidbody.position = new Vector2(positionX, positionY);
            return rigidbody;
        }
        /// <summary>
        /// Sets the Y coordinate of the 2D position (teleport-style), preserving X.
        /// </summary>
        /// <param name="positionY">Target Y position.</param>
        public static Rigidbody2D SetPositionY(this Rigidbody2D rigidbody, float positionY)
        {
            float positionX = rigidbody.position.x;

            rigidbody.position = new Vector2(positionX, positionY);
            return rigidbody;
        }

        /// <summary>
        /// Sets the 2D position from a tuple (x, y) (teleport-style).
        /// </summary>
        /// <param name="position">Target position tuple.</param>
        public static Rigidbody2D SetPositionXY(this Rigidbody2D rigidbody, (float x, float y) position)
        {
            rigidbody.position = new Vector2(position.x, position.y);
            return rigidbody;
        }
        /// <summary>
        /// Sets the 2D position from X and Y values (teleport-style).
        /// </summary>
        /// <param name="positionX">Target X position.</param>
        /// <param name="positionY">Target Y position.</param>
        public static Rigidbody2D SetPositionXY(this Rigidbody2D rigidbody, float positionX, float positionY)
        {
            rigidbody.position = new Vector2(positionX, positionY);
            return rigidbody;
        }

        /// <summary>
        /// Adds to the X coordinate of the 2D position (teleport-style), preserving Y.
        /// </summary>
        /// <param name="addX">Delta to add to X.</param>
        public static Rigidbody2D AddPositionX(this Rigidbody2D rigidbody, float addX)
        {
            float positionX = rigidbody.position.x + addX;
            float positionY = rigidbody.position.y;

            rigidbody.position = new Vector2(positionX, positionY);
            return rigidbody;
        }
        /// <summary>
        /// Adds to the Y coordinate of the 2D position (teleport-style), preserving X.
        /// </summary>
        /// <param name="addY">Delta to add to Y.</param>
        public static Rigidbody2D AddPositionY(this Rigidbody2D rigidbody, float addY)
        {
            float positionX = rigidbody.position.x;
            float positionY = rigidbody.position.y + addY;

            rigidbody.position = new Vector2(positionX, positionY);
            return rigidbody;
        }

        /// <summary>
        /// Adds to both X and Y coordinates of the 2D position (teleport-style).
        /// </summary>
        /// <param name="addX">Delta to add to X.</param>
        /// <param name="addY">Delta to add to Y.</param>
        public static Rigidbody2D AddPositionXY(this Rigidbody2D rigidbody, float addX, float addY)
        {
            float positionX = rigidbody.position.x + addX;
            float positionY = rigidbody.position.y + addY;

            rigidbody.position = new Vector2(positionX, positionY);
            return rigidbody;
        }
        /// <summary>
        /// Adds to both X and Y coordinates of the 2D position (teleport-style) using a tuple delta.
        /// </summary>
        /// <param name="addPos">Tuple delta (x, y).</param>
        public static Rigidbody2D AddPositionXY(this Rigidbody2D rigidbody, (float x, float y) addPos)
        {
            return AddPositionXY(rigidbody, addPos.x, addPos.y);
        }
        /// <summary>
        /// Adds to both X and Y coordinates of the 2D position (teleport-style) using a Vector2 delta.
        /// </summary>
        /// <param name="addPos">Vector delta.</param>
        public static Rigidbody2D AddPositionXY(this Rigidbody2D rigidbody, Vector2 addPos)
        {
            return AddPositionXY(rigidbody, addPos.x, addPos.y);
        }

        /// <summary>
        /// Moves the 2D position using physics-aware scheduling.
        /// Calls Rigidbody2D.MovePosition.
        /// </summary>
        /// <param name="position">Target position.</param>
        public static Rigidbody2D MovePosition(this Rigidbody2D rigidbody, Vector2 position)
        {
            rigidbody.MovePosition(position);
            return rigidbody;
        }

        /// <summary>
        /// Moves the X coordinate of the 2D position using physics-aware scheduling, preserving Y.
        /// Calls Rigidbody2D.MovePosition.
        /// </summary>
        /// <param name="positionX">Target X position.</param>
        public static Rigidbody2D MovePositionX(this Rigidbody2D rigidbody, float positionX)
        {
            float positionY = rigidbody.position.y;

            rigidbody.MovePosition(new Vector2(positionX, positionY));
            return rigidbody;
        }
        /// <summary>
        /// Moves the Y coordinate of the 2D position using physics-aware scheduling, preserving X.
        /// Calls Rigidbody2D.MovePosition.
        /// </summary>
        /// <param name="positionY">Target Y position.</param>
        public static Rigidbody2D MovePositionY(this Rigidbody2D rigidbody, float positionY)
        {
            float positionX = rigidbody.position.x;

            rigidbody.MovePosition(new Vector2(positionX, positionY));
            return rigidbody;
        }

        /// <summary>
        /// Moves the 2D position using physics-aware scheduling.
        /// Calls Rigidbody2D.MovePosition.
        /// </summary>
        /// <param name="positionX">Target X position.</param>
        /// <param name="positionY">Target Y position.</param>
        public static Rigidbody2D MovePositionXY(this Rigidbody2D rigidbody, float positionX, float positionY)
        {
            rigidbody.MovePosition(new Vector2(positionX, positionY));
            return rigidbody;
        }
        /// <summary>
        /// Moves the 2D position using physics-aware scheduling from a tuple (x, y).
        /// Calls Rigidbody2D.MovePosition.
        /// </summary>
        /// <param name="position">Target position tuple.</param>
        public static Rigidbody2D MovePositionXY(this Rigidbody2D rigidbody, (float x, float y) position)
        {
            return MovePositionXY(rigidbody, position.x, position.y);
        }

        /// <summary>
        /// Adds to the X coordinate of the 2D position using physics-aware scheduling, preserving Y.
        /// Calls Rigidbody2D.MovePosition.
        /// </summary>
        /// <param name="addX">Delta to add to X.</param>
        public static Rigidbody2D AddMovePositionX(this Rigidbody2D rigidbody, float addX)
        {
            float positionX = rigidbody.position.x + addX;
            float positionY = rigidbody.position.y;

            rigidbody.MovePosition(new Vector2(positionX, positionY));
            return rigidbody;
        }
        /// <summary>
        /// Adds to the Y coordinate of the 2D position using physics-aware scheduling, preserving X.
        /// Calls Rigidbody2D.MovePosition.
        /// </summary>
        /// <param name="addY">Delta to add to Y.</param>
        public static Rigidbody2D AddMovePositionY(this Rigidbody2D rigidbody, float addY)
        {
            float positionX = rigidbody.position.x;
            float positionY = rigidbody.position.y + addY;

            rigidbody.MovePosition(new Vector2(positionX, positionY));
            return rigidbody;
        }

        /// <summary>
        /// Adds to both X and Y coordinates of the 2D position using physics-aware scheduling.
        /// Calls Rigidbody2D.MovePosition.
        /// </summary>
        /// <param name="addX">Delta to add to X.</param>
        /// <param name="addY">Delta to add to Y.</param>
        public static Rigidbody2D AddMovePositionXY(this Rigidbody2D rigidbody, float addX, float addY)
        {
            float positionX = rigidbody.position.x + addX;
            float positionY = rigidbody.position.y + addY;

            rigidbody.MovePosition(new Vector2(positionX, positionY));
            return rigidbody;
        }
        /// <summary>
        /// Adds to both X and Y coordinates of the 2D position using physics-aware scheduling
        /// with a Vector2 delta.
        /// Calls Rigidbody2D.MovePosition.
        /// </summary>
        /// <param name="addPos">Vector delta.</param>
        public static Rigidbody2D AddMovePositionXY(this Rigidbody2D rigidbody, Vector2 addPos)
        {
            return AddMovePositionXY(rigidbody, addPos.x, addPos.y);
        }
        /// <summary>
        /// Adds to both X and Y coordinates of the 2D position using physics-aware scheduling
        /// with a tuple (x, y) delta.
        /// Calls Rigidbody2D.MovePosition.
        /// </summary>
        /// <param name="addPos">Tuple delta (x, y).</param>
        public static Rigidbody2D AddMovePositionXY(this Rigidbody2D rigidbody, (float x, float y) addPos)
        {
            return AddMovePositionXY(rigidbody, addPos.x, addPos.y);
        }

        /// <summary>
        /// Gets the current 3D position.
        /// </summary>
        public static Vector3 GetPosition(this Rigidbody rigidbody)
        {
            return rigidbody.position;
        }

        /// <summary>
        /// Gets the X coordinate of the current 3D position.
        /// </summary>
        public static float GetPositionX(this Rigidbody rigidbody)
        {
            return rigidbody.position.x;
        }
        /// <summary>
        /// Gets the Y coordinate of the current 3D position.
        /// </summary>
        public static float GetPositionY(this Rigidbody rigidbody)
        {
            return rigidbody.position.y;
        }
        /// <summary>
        /// Gets the Z coordinate of the current 3D position.
        /// </summary>
        public static float GetPositionZ(this Rigidbody rigidbody)
        {
            return rigidbody.position.z;
        }

        /// <summary>
        /// Gets the current 3D position as a tuple (x, y).
        /// </summary>
        public static (float x, float y) GetPositionXY(this Rigidbody rigidbody)
        {
            return (rigidbody.position.x, rigidbody.position.y);
        }
        /// <summary>
        /// Gets the current 3D position as a tuple (x, z).
        /// </summary>
        public static (float x, float z) GetPositionXZ(this Rigidbody rigidbody)
        {
            return (rigidbody.position.x, rigidbody.position.z);
        }
        /// <summary>
        /// Gets the current 3D position as a tuple (y, z).
        /// </summary>
        public static (float y, float z) GetPositionYZ(this Rigidbody rigidbody)
        {
            return (rigidbody.position.y, rigidbody.position.z);
        }

        /// <summary>
        /// Gets the current 3D position as a tuple (x, y, z).
        /// </summary>
        public static (float x, float y, float z) GetPositionXYZ(this Rigidbody rigidbody)
        {
            return (rigidbody.position.x, rigidbody.position.y, rigidbody.position.z);
        }

        /// <summary>
        /// Sets the 3D position (teleport-style) by assigning Rigidbody.position directly.
        /// </summary>
        /// <param name="position">Target position.</param>
        public static Rigidbody SetPosition(this Rigidbody rigidbody, Vector3 position)
        {
            rigidbody.position = position;
            return rigidbody;
        }

        /// <summary>
        /// Sets the X coordinate of the 3D position (teleport-style), preserving Y and Z.
        /// </summary>
        /// <param name="positionX">Target X position.</param>
        public static Rigidbody SetPositionX(this Rigidbody rigidbody, float positionX)
        {
            float positionY = rigidbody.position.y;
            float positionZ = rigidbody.position.z;

            rigidbody.position = new Vector3(positionX, positionY, positionZ);
            return rigidbody;
        }
        /// <summary>
        /// Sets the Y coordinate of the 3D position (teleport-style), preserving X and Z.
        /// </summary>
        /// <param name="positionY">Target Y position.</param>
        public static Rigidbody SetPositionY(this Rigidbody rigidbody, float positionY)
        {
            float positionX = rigidbody.position.x;
            float positionZ = rigidbody.position.z;

            rigidbody.position = new Vector3(positionX, positionY, positionZ);
            return rigidbody;
        }
        /// <summary>
        /// Sets the Z coordinate of the 3D position (teleport-style), preserving X and Y.
        /// </summary>
        /// <param name="positionZ">Target Z position.</param>
        public static Rigidbody SetPositionZ(this Rigidbody rigidbody, float positionZ)
        {
            float positionX = rigidbody.position.x;
            float positionY = rigidbody.position.y;

            rigidbody.position = new Vector3(positionX, positionY, positionZ);
            return rigidbody;
        }

        /// <summary>
        /// Sets the X and Y coordinates of the 3D position (teleport-style), preserving Z.
        /// </summary>
        /// <param name="positionX">Target X position.</param>
        /// <param name="positionY">Target Y position.</param>
        public static Rigidbody SetPositionXY(this Rigidbody rigidbody, float positionX, float positionY)
        {
            float positionZ = rigidbody.position.z;

            rigidbody.position = new Vector3(positionX, positionY, positionZ);
            return rigidbody;
        }
        /// <summary>
        /// Sets the X and Y coordinates of the 3D position (teleport-style) from a tuple (x, y), preserving Z.
        /// </summary>
        /// <param name="position">Target position tuple.</param>
        public static Rigidbody SetPositionXY(this Rigidbody rigidbody, (float x, float y) position)
        {
            return SetPositionXY(rigidbody, position.x, position.y);
        }
        /// <summary>
        /// Sets the X and Z coordinates of the 3D position (teleport-style), preserving Y.
        /// </summary>
        /// <param name="positionX">Target X position.</param>
        /// <param name="positionZ">Target Z position.</param>
        public static Rigidbody SetPositionXZ(this Rigidbody rigidbody, float positionX, float positionZ)
        {
            float positionY = rigidbody.position.y;

            rigidbody.position = new Vector3(positionX, positionY, positionZ);
            return rigidbody;
        }

        /// <summary>
        /// Sets the X and Z coordinates of the 3D position (teleport-style) from a tuple (x, z), preserving Y.
        /// </summary>
        /// <param name="position">Target position tuple.</param>
        public static Rigidbody SetPositionXZ(this Rigidbody rigidbody, (float x, float z) position)
        {
            return SetPositionXZ(rigidbody, position.x, position.z);
        }
        /// <summary>
        /// Sets the Y and Z coordinates of the 3D position (teleport-style), preserving X.
        /// </summary>
        /// <param name="positionY">Target Y position.</param>
        /// <param name="positionZ">Target Z position.</param>
        public static Rigidbody SetPositionYZ(this Rigidbody rigidbody, float positionY, float positionZ)
        {
            float positionX = rigidbody.position.x;

            rigidbody.position = new Vector3(positionX, positionY, positionZ);
            return rigidbody;
        }
        /// <summary>
        /// Sets the Y and Z coordinates of the 3D position (teleport-style) from a tuple (y, z), preserving X.
        /// </summary>
        /// <param name="position">Target position tuple.</param>
        public static Rigidbody SetPositionYZ(this Rigidbody rigidbody, (float y, float z) position)
        {
            return SetPositionYZ(rigidbody, position.y, position.z);
        }

        /// <summary>
        /// Sets the 3D position from a tuple (x, y, z) (teleport-style).
        /// </summary>
        /// <param name="position">Target position tuple.</param>
        public static Rigidbody SetPositionXYZ(this Rigidbody rigidbody, (float x, float y, float z) position)
        {
            rigidbody.position = new Vector3(position.x, position.y, position.z);
            return rigidbody;
        }

        /// <summary>
        /// Adds to the X coordinate of the 3D position (teleport-style), preserving Y and Z.
        /// </summary>
        /// <param name="addX">Delta to add to X.</param>
        public static Rigidbody AddPositionX(this Rigidbody rigidbody, float addX)
        {
            float positionX = rigidbody.position.x + addX;
            float positionY = rigidbody.position.y;
            float positionZ = rigidbody.position.z;

            rigidbody.position = new Vector3(positionX, positionY, positionZ);
            return rigidbody;
        }
        /// <summary>
        /// Adds to the Y coordinate of the 3D position (teleport-style), preserving X and Z.
        /// </summary>
        /// <param name="addY">Delta to add to Y.</param>
        public static Rigidbody AddPositionY(this Rigidbody rigidbody, float addY)
        {
            float positionX = rigidbody.position.x;
            float positionY = rigidbody.position.y + addY;
            float positionZ = rigidbody.position.z;

            rigidbody.position = new Vector3(positionX, positionY, positionZ);
            return rigidbody;
        }
        /// <summary>
        /// Adds to the Z coordinate of the 3D position (teleport-style), preserving X and Y.
        /// </summary>
        /// <param name="addZ">Delta to add to Z.</param>
        public static Rigidbody AddPositionZ(this Rigidbody rigidbody, float addZ)
        {
            float positionX = rigidbody.position.x;
            float positionY = rigidbody.position.y;
            float positionZ = rigidbody.position.z + addZ;

            rigidbody.position = new Vector3(positionX, positionY, positionZ);
            return rigidbody;
        }

        /// <summary>
        /// Adds to X and Y coordinates of the 3D position (teleport-style), preserving Z.
        /// </summary>
        /// <param name="addX">Delta to add to X.</param>
        /// <param name="addY">Delta to add to Y.</param>
        public static Rigidbody AddPositionXY(this Rigidbody rigidbody, float addX, float addY)
        {
            float positionX = rigidbody.position.x + addX;
            float positionY = rigidbody.position.y + addY;
            float positionZ = rigidbody.position.z;

            rigidbody.position = new Vector3(positionX, positionY, positionZ);
            return rigidbody;
        }
        /// <summary>
        /// Adds to X and Y coordinates of the 3D position (teleport-style) using a tuple delta (x, y), preserving Z.
        /// </summary>
        /// <param name="addPos">Tuple delta (x, y).</param>
        public static Rigidbody AddPositionXY(this Rigidbody rigidbody, (float x, float y) addPos)
        {
            return AddPositionXY(rigidbody, addPos.x, addPos.y);
        }
        /// <summary>
        /// Adds to X and Y coordinates of the 3D position (teleport-style) using a Vector2 delta (x, y), preserving Z.
        /// </summary>
        /// <param name="addPos">Vector2 delta.</param>
        public static Rigidbody AddPositionXY(this Rigidbody rigidbody, Vector2 addPos)
        {
            return AddPositionXY(rigidbody, addPos.x, addPos.y);
        }

        /// <summary>
        /// Adds to X and Z coordinates of the 3D position (teleport-style), preserving Y.
        /// </summary>
        /// <param name="addX">Delta to add to X.</param>
        /// <param name="addZ">Delta to add to Z.</param>
        public static Rigidbody AddPositionXZ(this Rigidbody rigidbody, float addX, float addZ)
        {
            float positionX = rigidbody.position.x + addX;
            float positionY = rigidbody.position.y;
            float positionZ = rigidbody.position.z + addZ;

            rigidbody.position = new Vector3(positionX, positionY, positionZ);
            return rigidbody;
        }
        /// <summary>
        /// Adds to X and Z coordinates of the 3D position (teleport-style) using a tuple delta (x, z), preserving Y.
        /// </summary>
        /// <param name="addPos">Tuple delta (x, z).</param>
        public static Rigidbody AddPositionXZ(this Rigidbody rigidbody, (float x, float z) addPos)
        {
            return AddPositionXZ(rigidbody, addPos.x, addPos.z);
        }

        /// <summary>
        /// Adds to Y and Z coordinates of the 3D position (teleport-style), preserving X.
        /// </summary>
        /// <param name="addY">Delta to add to Y.</param>
        /// <param name="addZ">Delta to add to Z.</param>
        public static Rigidbody AddPositionYZ(this Rigidbody rigidbody, float addY, float addZ)
        {
            float positionX = rigidbody.position.x;
            float positionY = rigidbody.position.y + addY;
            float positionZ = rigidbody.position.z + addZ;

            rigidbody.position = new Vector3(positionX, positionY, positionZ);
            return rigidbody;
        }
        /// <summary>
        /// Adds to Y and Z coordinates of the 3D position (teleport-style) using a tuple delta (y, z), preserving X.
        /// </summary>
        /// <param name="addPos">Tuple delta (y, z).</param>
        public static Rigidbody AddPositionYZ(this Rigidbody rigidbody, (float y, float z) addPos)
        {
            return AddPositionYZ(rigidbody, addPos.y, addPos.z);
        }

        /// <summary>
        /// Adds to X, Y and Z coordinates of the 3D position (teleport-style).
        /// </summary>
        /// <param name="addX">Delta to add to X.</param>
        /// <param name="addY">Delta to add to Y.</param>
        /// <param name="addZ">Delta to add to Z.</param>
        public static Rigidbody AddPositionXYZ(this Rigidbody rigidbody, float addX, float addY, float addZ)
        {
            float positionX = rigidbody.position.x + addX;
            float positionY = rigidbody.position.y + addY;
            float positionZ = rigidbody.position.z + addZ;

            rigidbody.position = new Vector3(positionX, positionY, positionZ);
            return rigidbody;
        }

        /// <summary>
        /// Adds to X, Y and Z coordinates of the 3D position (teleport-style) using a tuple delta (x, y, z).
        /// </summary>
        /// <param name="addPos">Tuple delta (x, y, z).</param>
        public static Rigidbody AddPositionXYZ(this Rigidbody rigidbody, (float x, float y, float z) addPos)
        {
            return AddPositionXYZ(rigidbody, addPos.x, addPos.y, addPos.z);
        }
        /// <summary>
        /// Adds to the 3D position (teleport-style) using a Vector3 delta.
        /// Equivalent to: rigidbody.position += position.
        /// </summary>
        /// <param name="position">Vector3 delta.</param>
        public static Rigidbody AddPositionXYZ(this Rigidbody rigidbody, Vector3 position)
        {
            rigidbody.position += position;
            return rigidbody;
        }

        /// <summary>
        /// Moves the 3D position using physics-aware scheduling.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="position">Target position.</param>
        public static Rigidbody MovePosition(this Rigidbody rigidbody, Vector3 position)
        {
            rigidbody.MovePosition(position);
            return rigidbody;
        }

        /// <summary>
        /// Moves the X coordinate of the 3D position using physics-aware scheduling, preserving Y and Z.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="positionX">Target X position.</param>
        public static Rigidbody MovePositionX(this Rigidbody rigidbody, float positionX)
        {
            float positionY = rigidbody.position.y;
            float positionZ = rigidbody.position.z;

            rigidbody.MovePosition(new Vector3(positionX, positionY, positionZ));
            return rigidbody;
        }
        /// <summary>
        /// Moves the Y coordinate of the 3D position using physics-aware scheduling, preserving X and Z.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="positionY">Target Y position.</param>
        public static Rigidbody MovePositionY(this Rigidbody rigidbody, float positionY)
        {
            float positionX = rigidbody.position.x;
            float positionZ = rigidbody.position.z;

            rigidbody.MovePosition(new Vector3(positionX, positionY, positionZ));
            return rigidbody;
        }
        /// <summary>
        /// Moves the Z coordinate of the 3D position using physics-aware scheduling, preserving X and Y.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="positionZ">Target Z position.</param>
        public static Rigidbody MovePositionZ(this Rigidbody rigidbody, float positionZ)
        {
            float positionX = rigidbody.position.x;
            float positionY = rigidbody.position.y;

            rigidbody.MovePosition(new Vector3(positionX, positionY, positionZ));
            return rigidbody;
        }

        /// <summary>
        /// Moves X and Y coordinates of the 3D position using physics-aware scheduling, preserving Z.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="positionX">Target X position.</param>
        /// <param name="positionY">Target Y position.</param>
        public static Rigidbody MovePositionXY(this Rigidbody rigidbody, float positionX, float positionY)
        {
            float positionZ = rigidbody.position.z;

            rigidbody.MovePosition(new Vector3(positionX, positionY, positionZ));
            return rigidbody;
        }
        /// <summary>
        /// Moves X and Y coordinates of the 3D position using physics-aware scheduling from a tuple (x, y), preserving Z.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="position">Target position tuple.</param>
        public static Rigidbody MovePositionXY(this Rigidbody rigidbody, (float x, float y) position)
        {
            return MovePositionXY(rigidbody, position.x, position.y);
        }

        /// <summary>
        /// Moves X and Z coordinates of the 3D position using physics-aware scheduling, preserving Y.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="positionX">Target X position.</param>
        /// <param name="positionZ">Target Z position.</param>
        public static Rigidbody MovePositionXZ(this Rigidbody rigidbody, float positionX, float positionZ)
        {
            float positionY = rigidbody.position.y;

            rigidbody.MovePosition(new Vector3(positionX, positionY, positionZ));
            return rigidbody;
        }
        /// <summary>
        /// Moves X and Z coordinates of the 3D position using physics-aware scheduling from a tuple (x, z), preserving Y.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="position">Target position tuple.</param>
        public static Rigidbody MovePositionXZ(this Rigidbody rigidbody, (float x, float z) position)
        {
            return MovePositionXZ(rigidbody, position.x, position.z);
        }

        /// <summary>
        /// Moves Y and Z coordinates of the 3D position using physics-aware scheduling, preserving X.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="positionY">Target Y position.</param>
        /// <param name="positionZ">Target Z position.</param>
        public static Rigidbody MovePositionYZ(this Rigidbody rigidbody, float positionY, float positionZ)
        {
            float positionX = rigidbody.position.x;

            rigidbody.MovePosition(new Vector3(positionX, positionY, positionZ));
            return rigidbody;
        }
        /// <summary>
        /// Moves Y and Z coordinates of the 3D position using physics-aware scheduling from a tuple (y, z), preserving X.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="position">Target position tuple.</param>
        public static Rigidbody MovePositionYZ(this Rigidbody rigidbody, (float y, float z) position)
        {
            return MovePositionYZ(rigidbody, position.y, position.z);
        }

        /// <summary>
        /// Moves the 3D position using physics-aware scheduling from a tuple (x, y, z).
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="position">Target position tuple.</param>
        public static Rigidbody MovePositionXYZ(this Rigidbody rigidbody, (float x, float y, float z) position)
        {
            rigidbody.MovePosition(new Vector3(position.x, position.y, position.z));
            return rigidbody;
        }

        /// <summary>
        /// Adds to the X coordinate of the 3D position using physics-aware scheduling, preserving Y and Z.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="addX">Delta to add to X.</param>
        public static Rigidbody AddMovePositionX(this Rigidbody rigidbody, float addX)
        {
            float positionX = rigidbody.position.x + addX;
            float positionY = rigidbody.position.y;
            float positionZ = rigidbody.position.z;

            rigidbody.MovePosition(new Vector3(positionX, positionY, positionZ));
            return rigidbody;
        }
        /// <summary>
        /// Adds to the Y coordinate of the 3D position using physics-aware scheduling, preserving X and Z.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="addY">Delta to add to Y.</param>
        public static Rigidbody AddMovePositionY(this Rigidbody rigidbody, float addY)
        {
            float positionX = rigidbody.position.x;
            float positionY = rigidbody.position.y + addY;
            float positionZ = rigidbody.position.z;

            rigidbody.MovePosition(new Vector3(positionX, positionY, positionZ));
            return rigidbody;
        }
        /// <summary>
        /// Adds to the Z coordinate of the 3D position using physics-aware scheduling, preserving X and Y.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="addZ">Delta to add to Z.</param>
        public static Rigidbody AddMovePositionZ(this Rigidbody rigidbody, float addZ)
        {
            float positionX = rigidbody.position.x;
            float positionY = rigidbody.position.y;
            float positionZ = rigidbody.position.z + addZ;

            rigidbody.MovePosition(new Vector3(positionX, positionY, positionZ));
            return rigidbody;
        }

        /// <summary>
        /// Adds to X and Y coordinates of the 3D position using physics-aware scheduling, preserving Z.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="addX">Delta to add to X.</param>
        /// <param name="addY">Delta to add to Y.</param>
        public static Rigidbody AddMovePositionXY(this Rigidbody rigidbody, float addX, float addY)
        {
            float positionX = rigidbody.position.x + addX;
            float positionY = rigidbody.position.y + addY;
            float positionZ = rigidbody.position.z;

            rigidbody.MovePosition(new Vector3(positionX, positionY, positionZ));
            return rigidbody;
        }
        /// <summary>
        /// Adds to X and Y coordinates of the 3D position using physics-aware scheduling with a Vector2 delta,
        /// preserving Z.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="addPos">Vector2 delta (x, y).</param>
        public static Rigidbody AddMovePositionXY(this Rigidbody rigidbody, Vector2 addPos)
        {
            return AddMovePositionXY(rigidbody, addPos.x, addPos.y);
        }
        /// <summary>
        /// Adds to X and Y coordinates of the 3D position using physics-aware scheduling with a tuple (x, y) delta,
        /// preserving Z.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="addPos">Tuple delta (x, y).</param>
        public static Rigidbody AddMovePositionXY(this Rigidbody rigidbody, (float x, float y) addPos)
        {
            return AddMovePositionXY(rigidbody, addPos.x, addPos.y);
        }

        /// <summary>
        /// Adds to X and Z coordinates of the 3D position using physics-aware scheduling, preserving Y.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="addX">Delta to add to X.</param>
        /// <param name="addZ">Delta to add to Z.</param>
        public static Rigidbody AddMovePositionXZ(this Rigidbody rigidbody, float addX, float addZ)
        {
            float positionX = rigidbody.position.x + addX;
            float positionY = rigidbody.position.y;
            float positionZ = rigidbody.position.z + addZ;

            rigidbody.MovePosition(new Vector3(positionX, positionY, positionZ));
            return rigidbody;
        }
        /// <summary>
        /// Adds to X and Z coordinates of the 3D position using physics-aware scheduling with a tuple (x, z) delta,
        /// preserving Y.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="addPos">Tuple delta (x, z).</param>
        public static Rigidbody AddMovePositionXZ(this Rigidbody rigidbody, (float x, float z) addPos)
        {
            return AddMovePositionXZ(rigidbody, addPos.x, addPos.z);
        }

        /// <summary>
        /// Adds to Y and Z coordinates of the 3D position using physics-aware scheduling, preserving X.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="addY">Delta to add to Y.</param>
        /// <param name="addZ">Delta to add to Z.</param>
        public static Rigidbody AddMovePositionYZ(this Rigidbody rigidbody, float addY, float addZ)
        {
            float positionX = rigidbody.position.x;
            float positionY = rigidbody.position.y + addY;
            float positionZ = rigidbody.position.z + addZ;

            rigidbody.MovePosition(new Vector3(positionX, positionY, positionZ));
            return rigidbody;
        }
        /// <summary>
        /// Adds to Y and Z coordinates of the 3D position using physics-aware scheduling with a tuple (y, z) delta,
        /// preserving X.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="addPos">Tuple delta (y, z).</param>
        public static Rigidbody AddMovePositionYZ(this Rigidbody rigidbody, (float y, float z) addPos)
        {
            return AddMovePositionYZ(rigidbody, addPos.y, addPos.z);
        }

        /// <summary>
        /// Adds to X, Y and Z coordinates of the 3D position using physics-aware scheduling.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="addX">Delta to add to X.</param>
        /// <param name="addY">Delta to add to Y.</param>
        /// <param name="addZ">Delta to add to Z.</param>
        public static Rigidbody AddMovePositionXYZ(this Rigidbody rigidbody, float addX, float addY, float addZ)
        {
            float positionX = rigidbody.position.x + addX;
            float positionY = rigidbody.position.y + addY;
            float positionZ = rigidbody.position.z + addZ;

            rigidbody.MovePosition(new Vector3(positionX, positionY, positionZ));
            return rigidbody;
        }
        /// <summary>
        /// Adds to X, Y and Z coordinates of the 3D position using physics-aware scheduling with a tuple (x, y, z) delta.
        /// Calls Rigidbody.MovePosition.
        /// </summary>
        /// <param name="addPos">Tuple delta (x, y, z).</param>
        public static Rigidbody AddMovePositionXYZ(this Rigidbody rigidbody, (float x, float y, float z) addPos)
        {
            return AddMovePositionXYZ(rigidbody, addPos.x, addPos.y, addPos.z);
        }        
    }
}
