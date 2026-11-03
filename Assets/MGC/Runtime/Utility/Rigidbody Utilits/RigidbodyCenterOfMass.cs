using System;
using UnityEngine;

namespace MGC.UnityAdapter
{
    /// <summary>
    /// Provides Unity-friendly extension methods for accessing and modifying
    /// the center of mass of Rigidbody and Rigidbody2D components.
    ///
    /// This class exposes both local and world-space representations of the center of mass:
    ///
    /// Local center of mass:
    /// - Stored in Rigidbody.centerOfMass / Rigidbody2D.centerOfMass
    /// - Expressed in the local coordinate space of the Rigidbody
    /// - Does not change automatically when the object moves or rotates
    /// - Can be explicitly modified to affect rotational stability and torque response
    ///
    /// World center of mass:
    /// - Exposed via Rigidbody.worldCenterOfMass / Rigidbody2D.worldCenterOfMass
    /// - Computed automatically by Unity from the local center of mass and Transform
    /// - Read-only and updated every physics step
    ///
    /// All setter and adder methods operate only on the local center of mass.
    /// World center of mass methods are provided for inspection and diagnostics only.
    ///
    /// All methods return the same Rigidbody or Rigidbody2D instance to support fluent chaining.
    /// </summary>
    public static class RigidbodyCenterOfMass
    {
        /// <summary>
        /// Returns the local center of mass of a 2D rigidbody in local space.
        /// </summary>
        public static Vector2 GetLocalCenterOfMass(this Rigidbody2D rigidbody)
        {
            return rigidbody.centerOfMass;
        }

        /// <summary>
        /// Returns the X component of the local center of mass of a 2D rigidbody.
        /// </summary>
        public static float GetLocalCenterOfMassX(this Rigidbody2D rigidbody)
        {
            return rigidbody.centerOfMass.x;
        }
        /// <summary>
        /// Returns the Y component of the local center of mass of a 2D rigidbody.
        /// </summary>
        public static float GetLocalCenterOfMassY(this Rigidbody2D rigidbody)
        {
            return rigidbody.centerOfMass.y;
        }

        /// <summary>
        /// Returns the local center of mass of a 2D rigidbody as an (x, y) tuple.
        /// </summary>
        public static (float x, float y) GetLocalCenterOfMassXY(this Rigidbody2D rigidbody)
        {
            return (rigidbody.centerOfMass.x, rigidbody.centerOfMass.y);
        }

        /// <summary>
        /// Returns the world-space center of mass of a 2D rigidbody.
        /// </summary>
        public static Vector2 GetWorldCenterOfMass(this Rigidbody2D rigidbody)
        {
            return rigidbody.worldCenterOfMass;
        }

        /// <summary>
        /// Returns the X component of the world-space center of mass of a 2D rigidbody.
        /// </summary>
        public static float GetWorldCenterOfMassX(this Rigidbody2D rigidbody)
        {
            return rigidbody.worldCenterOfMass.x;
        }
        /// <summary>
        /// Returns the Y component of the world-space center of mass of a 2D rigidbody.
        /// </summary>
        public static float GetWorldCenterOfMassY(this Rigidbody2D rigidbody)
        {
            return rigidbody.worldCenterOfMass.y;
        }

        /// <summary>
        /// Returns the world-space center of mass of a 2D rigidbody as an (x, y) tuple.
        /// </summary>
        public static (float x, float y) GetWorldCenterOfMassXY(this Rigidbody2D rigidbody)
        {
            return (rigidbody.worldCenterOfMass.x, rigidbody.worldCenterOfMass.y);
        }


        /// <summary>
        /// Returns the local center of mass of a 3D rigidbody in local space.
        /// </summary>
        public static Vector3 GetLocalCenterOfMass(this Rigidbody rigidbody)
        {
            return rigidbody.centerOfMass;
        }

        /// <summary>
        /// Returns the X component of the local center of mass of a 3D rigidbody.
        /// </summary>
        public static float GetLocalCenterOfMassX(this Rigidbody rigidbody)
        {
            return rigidbody.centerOfMass.x;
        }
        /// <summary>
        /// Returns the Y component of the local center of mass of a 3D rigidbody.
        /// </summary>
        public static float GetLocalCenterOfMassY(this Rigidbody rigidbody)
        {
            return rigidbody.centerOfMass.y;
        }
        /// <summary>
        /// Returns the Z component of the local center of mass of a 3D rigidbody.
        /// </summary>
        public static float GetLocalCenterOfMassZ(this Rigidbody rigidbody)
        {
            return rigidbody.centerOfMass.z;
        }

        /// <summary>
        /// Returns the X and Y components of the local center of mass of a 3D rigidbody.
        /// </summary>
        public static (float x, float y) GetLocalCenterOfMassXY(this Rigidbody rigidbody)
        {
            return (rigidbody.centerOfMass.x, rigidbody.centerOfMass.y);
        }
        /// <summary>
        /// Returns the X and Z components of the local center of mass of a 3D rigidbody.
        /// </summary>
        public static (float x, float z) GetLocalCenterOfMassXZ(this Rigidbody rigidbody)
        {
            return (rigidbody.centerOfMass.x, rigidbody.centerOfMass.z);
        }
        /// <summary>
        /// Returns the Y and Z components of the local center of mass of a 3D rigidbody.
        /// </summary>
        public static (float y, float z) GetLocalCenterOfMassYZ(this Rigidbody rigidbody)
        {
            return (rigidbody.centerOfMass.y, rigidbody.centerOfMass.z);
        }

        /// <summary>
        /// Returns the local center of mass of a 3D rigidbody as an (x, y, z) tuple.
        /// </summary>
        public static (float x, float y, float z) GetLocalCenterOfMassXYZ(this Rigidbody rigidbody)
        {
            return (rigidbody.centerOfMass.x, rigidbody.centerOfMass.y, rigidbody.centerOfMass.z);
        }

        /// <summary>
        /// Returns the world-space center of mass of a 3D rigidbody.
        /// </summary>
        public static Vector3 GetWorldCenterOfMass(this Rigidbody rigidbody)
        {
            return rigidbody.worldCenterOfMass;
        }

        /// <summary>
        /// Returns the X component of the world-space center of mass of a 3D rigidbody.
        /// </summary>
        public static float GetWorldCenterOfMassX(this Rigidbody rigidbody)
        {
            return rigidbody.worldCenterOfMass.x;
        }
        /// <summary>
        /// Returns the Y component of the world-space center of mass of a 3D rigidbody.
        /// </summary>
        public static float GetWorldCenterOfMassY(this Rigidbody rigidbody)
        {
            return rigidbody.worldCenterOfMass.y;
        }
        /// <summary>
        /// Returns the Z component of the world-space center of mass of a 3D rigidbody.
        /// </summary>
        public static float GetWorldCenterOfMassZ(this Rigidbody rigidbody)
        {
            return rigidbody.worldCenterOfMass.z;
        }

        /// <summary>
        /// Returns the X and Y components of the world-space center of mass of a 3D rigidbody.
        /// </summary>
        public static (float x, float y) GetWorldCenterOfMassXY(this Rigidbody rigidbody)
        {
            return (rigidbody.worldCenterOfMass.x, rigidbody.worldCenterOfMass.y);
        }
        /// <summary>
        /// Returns the X and Z components of the world-space center of mass of a 3D rigidbody.
        /// </summary>
        public static (float x, float z) GetWorldCenterOfMassXZ(this Rigidbody rigidbody)
        {
            return (rigidbody.worldCenterOfMass.x, rigidbody.worldCenterOfMass.z);
        }
        /// <summary>
        /// Returns the Y and Z components of the world-space center of mass of a 3D rigidbody.
        /// </summary>
        public static (float y, float z) GetWorldCenterOfMassYZ(this Rigidbody rigidbody)
        {
            return (rigidbody.worldCenterOfMass.y, rigidbody.worldCenterOfMass.z);
        }

        /// <summary>
        /// Returns the world-space center of mass of a 3D rigidbody as an (x, y, z) tuple.
        /// </summary>
        public static (float x, float y, float z) GetWorldCenterOfMassXYZ(this Rigidbody rigidbody)
        {
            return (rigidbody.worldCenterOfMass.x, rigidbody.worldCenterOfMass.y, rigidbody.worldCenterOfMass.z);
        }

        /// <summary>
        /// Sets the local center of mass of a 2D rigidbody.
        /// </summary>
        public static Rigidbody2D SetLocalCenterOfMass(this Rigidbody2D rigidbody, Vector2 position)
        {

            rigidbody.centerOfMass = position;
            return rigidbody;
        }

        /// <summary>
        /// Sets the X component of the local center of mass of a 2D rigidbody.
        /// </summary>
        public static Rigidbody2D SetLocalCenterOfMassX(this Rigidbody2D rigidbody, float positionX)
        {
            float x = positionX;
            float y = rigidbody.centerOfMass.y;

            rigidbody.centerOfMass = new Vector2(x, y);
            return rigidbody;
        }
        /// <summary>
        /// Sets the Y component of the local center of mass of a 2D rigidbody.
        /// </summary>
        public static Rigidbody2D SetLocalCenterOfMassY(this Rigidbody2D rigidbody, float positionY)
        {
            float x = rigidbody.centerOfMass.x;
            float y = positionY;

            rigidbody.centerOfMass = new Vector2(x, y);
            return rigidbody;
        }

        /// <summary>
        /// Sets the local center of mass of a 2D rigidbody using X and Y values.
        /// </summary>
        public static Rigidbody2D SetLocalCenterOfMassXY(this Rigidbody2D rigidbody, float positionX, float positionY)
        {
            float x = positionX;
            float y = positionY;

            rigidbody.centerOfMass = new Vector2(x, y);
            return rigidbody;
        }
        /// <summary>
        /// Sets the local center of mass of a 2D rigidbody using a tuple (x, y).
        /// </summary>
        public static Rigidbody2D SetLocalCenterOfMassXY(this Rigidbody2D rigidbody, (float x, float y) position)
        {
            float x = position.x;
            float y = position.y;

            rigidbody.centerOfMass = new Vector2(x, y);
            return rigidbody;
        }

        /// <summary>
        /// Adds a delta to the local center of mass of a 2D rigidbody.
        /// </summary>
        public static Rigidbody2D AddLocalCenterOfMass(this Rigidbody2D rigidbody, Vector2 position)
        {
            rigidbody.centerOfMass += position;
            return rigidbody;
        }

        /// <summary>
        /// Adds a delta to the X component of the local center of mass of a 2D rigidbody.
        /// </summary>
        public static Rigidbody2D AddLocalCenterOfMassX(this Rigidbody2D rigidbody, float positionX)
        {
            float x = rigidbody.centerOfMass.x + positionX;
            float y = rigidbody.centerOfMass.y;

            rigidbody.centerOfMass = new Vector2(x, y);
            return rigidbody;
        }
        /// <summary>
        /// Adds a delta to the Y component of the local center of mass of a 2D rigidbody.
        /// </summary>
        public static Rigidbody2D AddLocalCenterOfMassY(this Rigidbody2D rigidbody, float positionY)
        {
            float x = rigidbody.centerOfMass.x;
            float y = rigidbody.centerOfMass.y + positionY;

            rigidbody.centerOfMass = new Vector2(x, y);
            return rigidbody;
        }

        /// <summary>
        /// Adds deltas to the X and Y components of the local center of mass of a 2D rigidbody.
        /// </summary>
        public static Rigidbody2D AddLocalCenterOfMassXY(this Rigidbody2D rigidbody, float positionX, float positionY)
        {
            float x = rigidbody.centerOfMass.x + positionX;
            float y = rigidbody.centerOfMass.y + positionY;

            rigidbody.centerOfMass = new Vector2(x, y);
            return rigidbody;
        }
        /// <summary>
        /// Adds deltas to the local center of mass of a 2D rigidbody using a tuple (x, y).
        /// </summary>
        public static Rigidbody2D AddLocalCenterOfMassXY(this Rigidbody2D rigidbody, (float x, float y) position)
        {
            float x = rigidbody.centerOfMass.x + position.x;
            float y = rigidbody.centerOfMass.y + position.y;

            rigidbody.centerOfMass = new Vector2(x, y);
            return rigidbody;
        }

        /// <summary>
        /// Calculates the local center of mass for a group of 2D objects relative to a parent transform,
        /// using the centers of their Collider2D components.
        ///
        /// The calculation is performed as a simple arithmetic mean of all valid collider centers:
        /// - Each collider contributes equally.
        /// - Object size and shape are NOT taken into account.
        /// - All calculations are converted from world space into the local space of the parent transform.
        ///
        /// Conceptually, this represents a geometric center of collider positions,
        /// not a true mass-weighted center of mass.
        /// </summary>
        /// <param name="parts">
        /// Array of GameObjects representing parts of a composite object.
        /// Null elements are ignored.
        /// </param>
        /// <param name="parentTransform">
        /// Transform that defines the local coordinate space for the result.
        /// Typically the transform that owns the Rigidbody2D.
        /// </param>
        /// <param name="requireColliders">
        /// If true, every non-null GameObject must have a Collider2D.
        /// If false, objects without Collider2D are ignored.
        /// </param>
        /// <returns>
        /// Local-space center point (Vector2) representing the average of collider centers.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if parts or parentTransform is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if requireColliders is true and a part has no Collider2D,
        /// or if no valid colliders are found.
        /// </exception>
        public static Vector2 CalculateLocalCenterOfMass2D
            (GameObject[] parts, Transform parentTransform, bool requireColliders = true)
        {
            if (parts == null)
            {
                throw new ArgumentNullException(nameof(parts));
            }
            if (parentTransform == null)
            {
                throw new ArgumentNullException(nameof(parentTransform));
            }

            Vector2 sum = Vector2.zero;
            int count = 0;

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == null)
                {
                    continue;
                }

                Collider2D collider = parts[i].GetComponent<Collider2D>();

                if (collider == null)
                {
                    if (requireColliders)
                    {
                        throw new ArgumentException("All parts must have Collider2D.");
                    }
                    continue;
                }

                Vector2 worldCenter = collider.bounds.center;
                Vector3 localCenter3 = parentTransform.InverseTransformPoint(worldCenter);
                Vector2 localCenter = new Vector2(localCenter3.x, localCenter3.y);

                sum += localCenter;
                count++;
            }

            if (count == 0)
            {
                throw new ArgumentException("No valid colliders found to calculate center of mass.");
            }

            return sum / count;
        }
        /// <summary>
        /// Calculates the local center of mass for a group of 2D objects and returns it as an (x, y) tuple.
        ///
        /// This method is functionally equivalent to CalculateLocalCenterOfMass2D,
        /// but exposes the result in tuple form for API consistency with non-Unity core code.
        ///
        /// The center is computed as:
        /// (sum of local collider centers) / (number of valid colliders)
        /// </summary>
        /// <param name="parts">
        /// Array of GameObjects representing parts of a composite object.
        /// </param>
        /// <param name="parentTransform">
        /// Transform that defines the local coordinate space.
        /// </param>
        /// <param name="requireColliders">
        /// If true, all parts must have Collider2D components.
        /// If false, parts without colliders are skipped.
        /// </param>
        /// <returns>
        /// Local-space center of mass as (x, y).
        /// </returns>
        public static (float x, float y) CalculateLocalCenterOfMassXY
            (GameObject[] parts, Transform parentTransform, bool requireColliders = true)
        {
            if (parts == null)
            {
                throw new ArgumentNullException(nameof(parts));
            }
            if (parentTransform == null)
            {
                throw new ArgumentNullException(nameof(parentTransform));
            }

            Vector2 sum = Vector2.zero;
            int count = 0;

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == null)
                {
                    continue;
                }

                Collider2D collider = parts[i].GetComponent<Collider2D>();

                if (collider == null)
                {
                    if (requireColliders)
                    {
                        throw new ArgumentException("All parts must have Collider2D.");
                    }
                    continue;
                }

                Vector2 worldCenter = collider.bounds.center;
                Vector3 localCenter3 = parentTransform.InverseTransformPoint(worldCenter);
                Vector2 localCenter = new Vector2(localCenter3.x, localCenter3.y);

                sum += localCenter;
                count++;
            }

            if (count == 0)
            {
                throw new ArgumentException("No valid colliders found to calculate center of mass.");
            }

            sum /= count;
            return VectorConverter.ToFloat2(sum);
        }

        /// <summary>
        /// Calculates the local center of mass for a group of 3D objects relative to a parent transform,
        /// using the centers of their Collider components.
        ///
        /// The method:
        /// - Uses collider.bounds.center (world space)
        /// - Converts each center into parent local space
        /// - Computes the arithmetic mean of all valid centers
        ///
        /// This represents a geometric center of colliders, not a true physical center of mass.
        /// All colliders are treated as having equal weight.
        /// </summary>
        /// <param name="parts">
        /// Array of GameObjects forming a composite 3D object.
        /// </param>
        /// <param name="parentTransform">
        /// Transform that defines the local coordinate space.
        /// Usually the transform of the Rigidbody.
        /// </param>
        /// <param name="requireColliders">
        /// If true, every part must have a Collider.
        /// If false, parts without colliders are ignored.
        /// </param>
        /// <returns>
        /// Local-space center point as Vector3.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if parts or parentTransform is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if no valid colliders are found or a required collider is missing.
        public static Vector3 CalculateLocalCenterOfMass3D
            (GameObject[] parts, Transform parentTransform, bool requireColliders = true)
        {
            if (parts == null)
            {
                throw new ArgumentNullException(nameof(parts));
            }
            if (parentTransform == null)
            {
                throw new ArgumentNullException(nameof(parentTransform));
            }

            Vector3 sum = Vector3.zero;
            int count = 0;

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == null)
                {
                    continue;
                }

                Collider collider = parts[i].GetComponent<Collider>();

                if (collider == null)
                {
                    if (requireColliders)
                    {
                        throw new ArgumentException("All parts must have Collider.");
                    }
                    continue;
                }

                Vector3 worldCenter = collider.bounds.center;
                Vector3 localCenter = parentTransform.InverseTransformPoint(worldCenter);

                sum += localCenter;
                count++;
            }

            if (count == 0)
            {
                throw new ArgumentException("No valid colliders found to calculate center of mass.");
            }

            return sum / count;
        }
        /// <summary>
        /// Calculates the local center of mass for a group of 3D objects and returns it as an (x, y, z) tuple.
        ///
        /// This overload mirrors CalculateLocalCenterOfMass3D but exposes the result in tuple form,
        /// making it suitable for use in non-Unity physics or math layers.
        ///
        /// The center is computed as an unweighted average of collider centers.
        /// </summary>
        /// <param name="parts">
        /// Array of GameObjects representing composite parts.
        /// </param>
        /// <param name="parentTransform">
        /// Transform defining the local reference frame.
        /// </param>
        /// <param name="requireColliders">
        /// If true, all parts must have Collider components.
        /// </param>
        /// <returns>
        /// Local-space center of mass as (x, y, z).
        /// </returns>
        public static (float x, float y, float z) CalculateLocalCenterOfMassXYZ
            (GameObject[] parts, Transform parentTransform, bool requireColliders = true)
        {
            if (parts == null)
            {
                throw new ArgumentNullException(nameof(parts));
            }
            if (parentTransform == null)
            {
                throw new ArgumentNullException(nameof(parentTransform));
            }

            Vector3 sum = Vector3.zero;
            int count = 0;

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == null)
                {
                    continue;
                }

                Collider collider = parts[i].GetComponent<Collider>();

                if (collider == null)
                {
                    if (requireColliders)
                    {
                        throw new ArgumentException("All parts must have Collider.");
                    }
                    continue;
                }

                Vector3 worldCenter = collider.bounds.center;
                Vector3 localCenter = parentTransform.InverseTransformPoint(worldCenter);

                sum += localCenter;
                count++;
            }

            if (count == 0)
            {
                throw new ArgumentException("No valid colliders found to calculate center of mass.");
            }

            sum /= count;
            return VectorConverter.ToFloat3(sum);
        }

    }
}
