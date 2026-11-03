using System;
using UnityEngine;

namespace MGC.UnityAdapter
{
    /// <summary>
    /// <para>
    /// Provides Unity-friendly extension methods for configuring and querying
    /// <see cref="Rigidbody"/> and <see cref="Rigidbody2D"/> components.
    /// </para>
    ///
    /// <para>
    /// This class is part of the UnityAdapter layer and focuses on:
    /// </para>
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///       Mass helpers with unit conversion via <see cref="MassConverter"/>
    ///       (kg, g, lb, oz, tonne).
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Fluent configuration helpers for common <see cref="Rigidbody"/> / <see cref="Rigidbody2D"/> properties:
    ///       <list type="bullet">
    ///         <item><description><c>drag</c> / <c>angularDrag</c></description></item>
    ///         <item><description>gravity (3D)</description></item>
    ///         <item><description>kinematic (3D/2D)</description></item>
    ///         <item><description>interpolation (3D/2D)</description></item>
    ///         <item><description>collision detection mode (3D/2D)</description></item>
    ///       </list>
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Query helpers (<c>Get*</c> methods) for the same properties to support UI and debug flows.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Constraint helpers with two semantic modes:
    ///       <list type="number">
    ///         <item>
    ///           <description>
    ///             <b>Accumulate</b>: <c>Freeze*</c> / <c>Unfreeze*</c> add or remove constraints
    ///             without affecting other flags.
    ///           </description>
    ///         </item>
    ///         <item>
    ///           <description>
    ///             <b>Overwrite</b>: <c>SetFreeze*</c> clears only the relevant constraint group
    ///             and then applies the requested axis.
    ///           </description>
    ///         </item>
    ///       </list>
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       2D rotation freeze helpers via <see cref="RigidbodyConstraints2D.FreezeRotation"/>.
    ///     </description>
    ///   </item>
    /// </list>
    ///
    /// <para>Notes:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///       All methods validate that the target rigidbody is not <c>null</c>
    ///       and throw <see cref="ArgumentNullException"/>.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       This class does not implement physics; it only configures Unity components.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Unity mass values are expressed in kilograms (<c>kg</c>).
    ///     </description>
    ///   </item>
    /// </list>
    /// </summary>

    public static class RigidbodyExtensions
    {
        /// <summary>
        /// Specifies which axis/axes should be processed for 3D (x, y, z).
        /// </summary>
        public enum FreezeAxis3D
        {
            /// <summary>
            /// Process X only.
            /// </summary>
            X,

            /// <summary>
            /// Process Y only.
            /// </summary>
            Y,

            /// <summary>
            /// Process Z only.
            /// </summary>
            Z,

            /// <summary>
            /// Process X and Y.
            /// </summary>
            XY,

            /// <summary>
            /// Process X and Z.
            /// </summary>
            XZ,

            /// <summary>
            /// Process Y and Z.
            /// </summary>
            YZ,

            /// <summary>
            /// Process X, Y, and Z.
            /// </summary>
            XYZ
        }
        /// <summary>
        /// Specifies which axis/axes should be processed for 2D (x, y).
        /// </summary>
        public enum FreezeAxis2D
        {
            /// <summary>
            /// Process X only.
            /// </summary>
            x,

            /// <summary>
            /// Process Y only.
            /// </summary>
            y,

            /// <summary>
            /// Process X and Y.
            /// </summary>
            xy
        }
        /// <summary>
        /// Defines high-level sleeping behavior presets for a Rigidbody.
        ///
        /// Sleeping is an optimization mechanism in Unity physics that disables
        /// simulation for bodies that are considered at rest.
        /// </summary>
        public enum SleepMode
        {
            /// <summary>
            /// Disables sleeping behavior as much as possible.
            ///
            /// Internally this sets <see cref="Rigidbody.sleepThreshold"/> to zero
            /// and wakes the rigidbody immediately.
            ///
            /// Use with caution: this may increase CPU usage.
            /// </summary>
            NeverSleep,

            /// <summary>
            /// The rigidbody starts awake and is allowed to sleep automatically
            /// according to Unity's default sleeping rules.
            ///
            /// This is Unity's default behavior.
            /// </summary>
            StartAwake,

            /// <summary>
            /// The rigidbody starts in a sleeping state but may be woken up
            /// automatically by collisions or forces.
            /// </summary>
            StartAsleep
        }

        /// <summary>
        /// Gets rigidbody mass converted from kilograms (Unity) to the specified unit.
        /// </summary>
        /// <param name="rigidbody">Target rigidbody.</param>
        /// <param name="unit">Requested mass unit for the returned value.</param>
        /// <returns>Mass expressed in <paramref name="unit"/>.</returns>
        public static float GetMass(this Rigidbody rigidbody, MassConverter.MassUnits unit)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            return MassConverter.MassConvert(rigidbody.mass, MassConverter.MassUnits.kg, unit);
        }
        /// <summary>
        /// Gets rigidbody2D mass converted from kilograms (Unity) to the specified unit.
        /// </summary>
        /// <param name="rigidbody">Target rigidbody2D.</param>
        /// <param name="unit">Requested mass unit for the returned value.</param>
        /// <returns>Mass expressed in <paramref name="unit"/>.</returns>
        public static float GetMass(this Rigidbody2D rigidbody, MassConverter.MassUnits unit)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            return MassConverter.MassConvert(rigidbody.mass, MassConverter.MassUnits.kg, unit);
        }

        /// <summary>
        /// Gets the linear drag value of a 3D rigidbody.
        ///
        /// Linear drag simulates resistance (e.g., air resistance) and affects
        /// translational motion by damping velocity over time.
        /// </summary>
        /// <param name="rigidbody">Target rigidbody.</param>
        /// <returns>Current linear drag value.</returns>
        public static float GetDrag(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            return rigidbody.drag;
        }
        /// <summary>
        /// Gets the linear drag value of a 2D rigidbody.
        ///
        /// Linear drag simulates resistance (e.g., air resistance) and affects
        /// translational motion by damping velocity over time.
        /// </summary>
        /// <param name="rigidbody">Target rigidbody2D.</param>
        /// <returns>Current linear drag value.</returns>
        public static float GetDrag(this Rigidbody2D rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            return rigidbody.drag;
        }

        /// <summary>
        /// Gets the angular drag value of a 3D rigidbody.
        ///
        /// Angular drag damps rotational motion and reduces angular velocity
        /// over time.
        /// </summary>
        /// <param name="rigidbody">Target rigidbody.</param>
        /// <returns>Current angular drag value.</returns>
        public static float GetAngularDrag(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            return rigidbody.angularDrag;
        }
        /// <summary>
        /// Gets the angular drag value of a 2D rigidbody.
        ///
        /// Angular drag damps rotational motion and reduces angular velocity
        /// over time.
        /// </summary>
        /// <param name="rigidbody">Target rigidbody2D.</param>
        /// <returns>Current angular drag value.</returns>
        public static float GetAngularDrag(this Rigidbody2D rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            return rigidbody.angularDrag;
        }

        /// <summary>
        /// Sets rigidbody mass using the specified unit. Unity stores mass in kilograms (kg).
        /// </summary>
        /// <param name="rigidbody">Target rigidbody.</param>
        /// <param name="mass">Mass value expressed in <paramref name="unit"/>.</param>
        /// <param name="unit">Unit of the input mass.</param>
        /// <returns>The same rigidbody instance for fluent chaining.</returns>
        public static Rigidbody SetMass(this Rigidbody rigidbody, float mass, MassConverter.MassUnits unit)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.mass = MassConverter.MassConvert(mass, unit, MassConverter.MassUnits.kg);
            return rigidbody;
        }
        /// <summary>
        /// Sets rigidbody2D mass using the specified unit. Unity stores mass in kilograms (kg).
        /// </summary>
        /// <param name="rigidbody">Target rigidbody2D.</param>
        /// <param name="mass">Mass value expressed in <paramref name="unit"/>.</param>
        /// <param name="unit">Unit of the input mass.</param>
        /// <returns>The same rigidbody2D instance for fluent chaining.</returns>
        public static Rigidbody2D SetMass(this Rigidbody2D rigidbody, float mass, MassConverter.MassUnits unit)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.mass = MassConverter.MassConvert(mass, unit, MassConverter.MassUnits.kg);
            return rigidbody;
        }

        /// <summary>
        /// Sets linear drag for a 3D rigidbody.
        /// </summary>
        public static Rigidbody SetDrag(this Rigidbody rigidbody, float drag)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.drag = drag;
            return rigidbody;
        }
        /// <summary>
        /// Sets linear drag for a 2D rigidbody.
        /// </summary>
        public static Rigidbody2D SetDrag(this Rigidbody2D rigidbody, float drag)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.drag = drag;
            return rigidbody;
        }

        /// <summary>
        /// Sets angular drag for a 3D rigidbody.
        /// </summary>
        public static Rigidbody SetAngularDrag(this Rigidbody rigidbody, float angularDrag)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.angularDrag = angularDrag;
            return rigidbody;
        }
        /// <summary>
        /// Sets angular drag for a 2D rigidbody.
        /// </summary>
        public static Rigidbody2D SetAngularDrag(this Rigidbody2D rigidbody, float angularDrag)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.angularDrag = angularDrag;
            return rigidbody;
        }

        /// <summary>
        /// Adds (accumulates) rotation freeze constraints for the specified axes (3D).
        /// This method does not clear other constraint flags.
        /// </summary>
        public static Rigidbody FreezeRotation3D(this Rigidbody rigidbody, FreezeAxis3D axis)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            switch (axis)
            {
                case FreezeAxis3D.X:
                    {
                        rigidbody.constraints |= RigidbodyConstraints.FreezeRotationX;
                        break;
                    }
                case FreezeAxis3D.Y:
                    {
                        rigidbody.constraints |= RigidbodyConstraints.FreezeRotationY;
                        break;
                    }
                case FreezeAxis3D.Z:
                    {
                        rigidbody.constraints |= RigidbodyConstraints.FreezeRotationZ;
                        break;
                    }
                case FreezeAxis3D.XY:
                    {
                        rigidbody.constraints |= RigidbodyConstraints.FreezeRotationX;
                        rigidbody.constraints |= RigidbodyConstraints.FreezeRotationY;
                        break;
                    }
                case FreezeAxis3D.XZ:
                    {
                        rigidbody.constraints |= RigidbodyConstraints.FreezeRotationX;
                        rigidbody.constraints |= RigidbodyConstraints.FreezeRotationZ;
                        break;
                    }
                case FreezeAxis3D.YZ:
                    {
                        rigidbody.constraints |= RigidbodyConstraints.FreezeRotationY;
                        rigidbody.constraints |= RigidbodyConstraints.FreezeRotationZ;
                        break;
                    }
                case FreezeAxis3D.XYZ:
                    {
                        rigidbody.constraints |= RigidbodyConstraints.FreezeRotationX;
                        rigidbody.constraints |= RigidbodyConstraints.FreezeRotationY;
                        rigidbody.constraints |= RigidbodyConstraints.FreezeRotationZ;
                        break;
                    }
                default:
                    {
                        Debug.LogError($"{axis} is incorrect axis type. Method: FreezeRotation3D");
                        throw new ArgumentOutOfRangeException(nameof(axis));
                    }
            }

            return rigidbody;
        }
        /// <summary>
        /// Adds (accumulates) position freeze constraints for the specified axes (3D).
        /// This method does not clear other constraint flags.
        /// </summary>
        public static Rigidbody FreezePosition3D(this Rigidbody rigidbody, FreezeAxis3D axis)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            switch (axis)
            {
                case FreezeAxis3D.X:
                    {
                        rigidbody.constraints |= RigidbodyConstraints.FreezePositionX;
                        break;
                    }
                case FreezeAxis3D.Y:
                    {
                        rigidbody.constraints |= RigidbodyConstraints.FreezePositionY;
                        break;
                    }
                case FreezeAxis3D.Z:
                    {
                        rigidbody.constraints |= RigidbodyConstraints.FreezePositionZ;
                        break;
                    }
                case FreezeAxis3D.XY:
                    {
                        rigidbody.constraints |= RigidbodyConstraints.FreezePositionX;
                        rigidbody.constraints |= RigidbodyConstraints.FreezePositionY;
                        break;
                    }
                case FreezeAxis3D.XZ:
                    {
                        rigidbody.constraints |= RigidbodyConstraints.FreezePositionX;
                        rigidbody.constraints |= RigidbodyConstraints.FreezePositionZ;
                        break;
                    }
                case FreezeAxis3D.YZ:
                    {
                        rigidbody.constraints |= RigidbodyConstraints.FreezePositionY;
                        rigidbody.constraints |= RigidbodyConstraints.FreezePositionZ;
                        break;
                    }
                case FreezeAxis3D.XYZ:
                    {
                        rigidbody.constraints |= RigidbodyConstraints.FreezePositionX;
                        rigidbody.constraints |= RigidbodyConstraints.FreezePositionY;
                        rigidbody.constraints |= RigidbodyConstraints.FreezePositionZ;
                        break;
                    }
                default:
                    {
                        Debug.LogError($"{axis} is incorrect axis type. Method: FreezePosition3D");
                        throw new ArgumentOutOfRangeException(nameof(axis));
                    }
            }

            return rigidbody;
        }

        /// <summary>
        /// Overwrites rotation freeze constraints (3D) for the specified axes.
        /// Clears only FreezeRotationX/Y/Z flags and then applies <paramref name="axis"/>.
        /// </summary>
        public static Rigidbody SetFreezeRotation3D(this Rigidbody rigidbody, FreezeAxis3D axis)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.constraints &=
                ~(RigidbodyConstraints.FreezeRotationX |
                  RigidbodyConstraints.FreezeRotationY |
                  RigidbodyConstraints.FreezeRotationZ);

            return rigidbody.FreezeRotation3D(axis);
        }
        /// <summary>
        /// Overwrites position freeze constraints (3D) for the specified axes.
        /// Clears only FreezePositionX/Y/Z flags and then applies <paramref name="axis"/>.
        /// </summary>
        public static Rigidbody SetFreezePosition3D(this Rigidbody rigidbody, FreezeAxis3D axis)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.constraints &=
                ~(RigidbodyConstraints.FreezePositionX |
                  RigidbodyConstraints.FreezePositionY |
                  RigidbodyConstraints.FreezePositionZ);

            return rigidbody.FreezePosition3D(axis);
        }

        /// <summary>
        /// Removes (unfreezes) rotation constraints for the specified axes (3D).
        /// This method does not affect other constraint flags.
        /// </summary>
        public static Rigidbody UnfreezeRotation3D(this Rigidbody rigidbody, FreezeAxis3D axis)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            switch (axis)
            {
                case FreezeAxis3D.X:
                    {
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationX;
                        break;
                    }
                case FreezeAxis3D.Y:
                    {
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationY;
                        break;
                    }
                case FreezeAxis3D.Z:
                    {
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
                        break;
                    }
                case FreezeAxis3D.XY:
                    {
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationX;
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationY;
                        break;
                    }
                case FreezeAxis3D.XZ:
                    {
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationX;
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
                        break;
                    }
                case FreezeAxis3D.YZ:
                    {
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationY;
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
                        break;
                    }
                case FreezeAxis3D.XYZ:
                    {
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationX;
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationY;
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezeRotationZ;
                        break;
                    }
                default:
                    {
                        Debug.LogError($"{axis} is incorrect axis type. Method: UnfreezeRotation3D");
                        throw new ArgumentOutOfRangeException(nameof(axis));
                    }
            }

            return rigidbody;
        }
        /// <summary>
        /// Removes (unfreezes) position constraints for the specified axes (3D).
        /// This method does not affect other constraint flags.
        /// </summary>
        public static Rigidbody UnfreezePosition3D(this Rigidbody rigidbody, FreezeAxis3D axis)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            switch (axis)
            {
                case FreezeAxis3D.X:
                    {
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionX;
                        break;
                    }
                case FreezeAxis3D.Y:
                    {
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
                        break;
                    }
                case FreezeAxis3D.Z:
                    {
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionZ;
                        break;
                    }
                case FreezeAxis3D.XY:
                    {
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionX;
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
                        break;
                    }
                case FreezeAxis3D.XZ:
                    {
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionX;
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionZ;
                        break;
                    }
                case FreezeAxis3D.YZ:
                    {
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionZ;
                        break;
                    }
                case FreezeAxis3D.XYZ:
                    {
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionX;
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
                        rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionZ;
                        break;
                    }
                default:
                    {
                        Debug.LogError($"{axis} is incorrect axis type. Method: UnfreezePosition3D");
                        throw new ArgumentOutOfRangeException(nameof(axis));
                    }
            }

            return rigidbody;
        }

        /// <summary>
        /// Adds (accumulates) position freeze constraints for the specified axes (2D).
        /// This method does not clear other constraint flags.
        /// </summary>
        public static Rigidbody2D FreezePosition2D(this Rigidbody2D rigidbody, FreezeAxis2D axis)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            switch (axis)
            {
                case FreezeAxis2D.x:
                    {
                        rigidbody.constraints |= RigidbodyConstraints2D.FreezePositionX;
                        break;
                    }
                case FreezeAxis2D.y:
                    {
                        rigidbody.constraints |= RigidbodyConstraints2D.FreezePositionY;
                        break;
                    }
                case FreezeAxis2D.xy:
                    {
                        rigidbody.constraints |= RigidbodyConstraints2D.FreezePositionX;
                        rigidbody.constraints |= RigidbodyConstraints2D.FreezePositionY;
                        break;
                    }
                default:
                    {
                        Debug.LogError($"{axis} is incorrect axis type. Method: FreezePosition2D");
                        throw new ArgumentOutOfRangeException(nameof(axis));
                    }
            }

            return rigidbody;
        }

        /// <summary>
        /// Overwrites position freeze constraints (2D) for the specified axes.
        /// Clears only FreezePositionX/Y flags and then applies <paramref name="axis"/>.
        /// </summary>
        public static Rigidbody2D SetFreezePosition2D(this Rigidbody2D rigidbody, FreezeAxis2D axis)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.constraints &=
                ~(RigidbodyConstraints2D.FreezePositionX |
                  RigidbodyConstraints2D.FreezePositionY);

            return rigidbody.FreezePosition2D(axis);
        }

        /// <summary>
        /// Removes (unfreezes) position constraints for the specified axes (2D).
        /// This method does not affect other constraint flags.
        /// </summary>
        public static Rigidbody2D UnfreezePosition2D(this Rigidbody2D rigidbody, FreezeAxis2D axis)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            switch (axis)
            {
                case FreezeAxis2D.x:
                    {
                        rigidbody.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                        break;
                    }
                case FreezeAxis2D.y:
                    {
                        rigidbody.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
                        break;
                    }
                case FreezeAxis2D.xy:
                    {
                        rigidbody.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
                        rigidbody.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
                        break;
                    }
                default:
                    {
                        Debug.LogError($"{axis} is incorrect axis type. Method: UnfreezePosition2D");
                        throw new ArgumentOutOfRangeException(nameof(axis));
                    }
            }

            return rigidbody;
        }

        /// <summary>
        /// Adds (accumulates) 2D rotation freeze constraint (FreezeRotation).
        /// </summary>
        public static Rigidbody2D FreezeRotation2D(this Rigidbody2D rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.constraints |= RigidbodyConstraints2D.FreezeRotation;

            return rigidbody;
        }

        /// <summary>
        /// Removes (unfreezes) 2D rotation freeze constraint (FreezeRotation).
        /// </summary>
        public static Rigidbody2D UnfreezeRotation2D(this Rigidbody2D rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.constraints &= ~RigidbodyConstraints2D.FreezeRotation;

            return rigidbody;
        }

        /// <summary>
        /// Overwrites 2D rotation freeze constraint (FreezeRotation).
        /// When <paramref name="freeze"/> is true, FreezeRotation is enabled; otherwise it is disabled.
        /// </summary>
        public static Rigidbody2D SetFreezeRotation2D(this Rigidbody2D rigidbody, bool freeze)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            if (freeze)
            {
                rigidbody.constraints |= RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                rigidbody.constraints &= ~RigidbodyConstraints2D.FreezeRotation;
            }

            return rigidbody;
        }

        /// <summary>
        /// Clears all constraints for a 3D rigidbody (sets constraints to None).
        /// </summary>
        public static Rigidbody ClearConstraints3D(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.constraints = RigidbodyConstraints.None;

            return rigidbody;
        }
        /// <summary>
        /// Clears all constraints for a 2D rigidbody (sets constraints to None).
        /// </summary>
        public static Rigidbody2D ClearConstraints2D(this Rigidbody2D rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.constraints = RigidbodyConstraints2D.None;

            return rigidbody;
        }

        /// <summary>
        /// Sets whether gravity is enabled for a 3D rigidbody.
        /// </summary>
        /// <param name="rigidbody">Target rigidbody.</param>
        /// <param name="useGravity">True to enable gravity; false to disable it.</param>
        /// <returns>The same rigidbody instance for fluent chaining.</returns>
        public static Rigidbody SetUseGravity(this Rigidbody rigidbody, bool useGravity)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.useGravity = useGravity;
            return rigidbody;
        }
        /// <summary>
        /// Sets the gravity scale of a 2D rigidbody.
        /// 
        /// gravityScale multiplies the global Physics2D gravity for this body.
        /// - 0 disables gravity effect for this rigidbody2D.
        /// - 1 uses global gravity as-is.
        /// - Negative values invert gravity direction.
        /// </summary>
        /// <param name="rigidbody">Target rigidbody2D.</param>
        /// <param name="gravityScale">New gravity scale value.</param>
        /// <returns>The same rigidbody2D instance for fluent chaining.</returns>
        public static Rigidbody2D SetGravityScale(this Rigidbody2D rigidbody, float gravityScale)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.gravityScale = gravityScale;
            return rigidbody;
        }

        /// <summary>
        /// Enables gravity for a 3D rigidbody.
        /// </summary>
        public static Rigidbody GravityOn(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.useGravity = true;
            return rigidbody;
        }
        /// <summary>
        /// Disables gravity for a 3D rigidbody.
        /// </summary>
        public static Rigidbody GravityOff(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.useGravity = false;
            return rigidbody;
        }

        /// <summary>
        /// Gets whether gravity is enabled for a 3D rigidbody.
        /// </summary>
        /// <param name="rigidbody">Target rigidbody.</param>
        /// <returns>
        /// True if gravity affects this rigidbody; otherwise false.
        /// </returns>
        public static bool GetUseGravity(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            return rigidbody.useGravity;
        }
        /// <summary>
        /// Gets the gravity scale of a 2D rigidbody.
        /// 
        /// gravityScale multiplies the global Physics2D gravity for this body.
        /// - 0 disables gravity effect for this rigidbody2D.
        /// - 1 uses global gravity as-is.
        /// - Negative values invert gravity direction.
        /// </summary>
        /// <param name="rigidbody">Target rigidbody2D.</param>
        /// <returns>Current gravity scale value.</returns>
        public static float GetGravityScale(this Rigidbody2D rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            return rigidbody.gravityScale;
        }

        /// <summary>
        /// Sets whether a 3D rigidbody is kinematic.
        ///
        /// When kinematic is true, the rigidbody is not driven by the physics solver
        /// and is typically moved via scripts.
        /// </summary>
        /// <param name="rigidbody">Target rigidbody.</param>
        /// <param name="isKinematic">True to enable kinematic mode; false to disable it.</param>
        /// <returns>The same rigidbody instance for fluent chaining.</returns>
        public static Rigidbody SetIsKinematic(this Rigidbody rigidbody, bool isKinematic)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.isKinematic = isKinematic;
            return rigidbody;
        }
        /// <summary>
        /// Sets whether a 2D rigidbody is kinematic.
        ///
        /// When kinematic is true, the rigidbody2D is not driven by the physics solver
        /// and is typically moved via scripts.
        /// </summary>
        /// <param name="rigidbody">Target rigidbody2D.</param>
        /// <param name="isKinematic">True to enable kinematic mode; false to disable it.</param>
        /// <returns>The same rigidbody2D instance for fluent chaining.</returns>
        public static Rigidbody2D SetIsKinematic(this Rigidbody2D rigidbody, bool isKinematic)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.isKinematic = isKinematic;
            return rigidbody;
        }

        /// <summary>
        /// Enables kinematic mode for a 3D rigidbody.
        /// </summary>
        public static Rigidbody KinematicOn(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.isKinematic = true;
            return rigidbody;
        }
        /// <summary>
        /// Enables kinematic mode for a 2D rigidbody.
        /// </summary>
        public static Rigidbody2D KinematicOn(this Rigidbody2D rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.isKinematic = true;
            return rigidbody;
        }

        /// <summary>
        /// Gets whether the rigidbody is kinematic (3D).
        ///
        /// A kinematic rigidbody is not driven by the physics solver and is typically
        /// moved via scripts. It can still affect dynamic bodies through collisions,
        /// depending on Unity settings and usage.
        /// </summary>
        /// <param name="rigidbody">Target rigidbody.</param>
        /// <returns>
        /// True if the rigidbody is kinematic; otherwise false.
        /// </returns>
        public static bool GetIsKinematic(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            return rigidbody.isKinematic;
        }
        /// <summary>
        /// Gets whether the rigidbody is kinematic (2D).
        ///
        /// A kinematic rigidbody2D is not affected by forces in the usual way and is typically
        /// moved via scripts. It can still generate contacts/collisions depending on setup.
        /// </summary>
        /// <param name="rigidbody">Target rigidbody2D.</param>
        /// <returns>
        /// True if the rigidbody2D is kinematic; otherwise false.
        /// </returns>
        public static bool GetIsKinematic(this Rigidbody2D rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            return rigidbody.isKinematic;
        }

        /// <summary>
        /// Disables kinematic mode for a 3D rigidbody.
        /// </summary>
        public static Rigidbody KinematicOff(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.isKinematic = false;
            return rigidbody;
        }
        /// <summary>
        /// Disables kinematic mode for a 2D rigidbody.
        /// </summary>
        public static Rigidbody2D KinematicOff(this Rigidbody2D rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.isKinematic = false;
            return rigidbody;
        }

        /// <summary>
        /// Sets 3D interpolation to None.
        /// </summary>
        public static Rigidbody InterpolateNone(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.interpolation = RigidbodyInterpolation.None;
            return rigidbody;
        }
        /// <summary>
        /// Sets 2D interpolation to None.
        /// </summary>
        public static Rigidbody2D InterpolateNone(this Rigidbody2D rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.interpolation = RigidbodyInterpolation2D.None;
            return rigidbody;
        }

        /// <summary>
        /// Sets 3D interpolation to Interpolate.
        /// </summary>
        public static Rigidbody InterpolateOn(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            return rigidbody;
        }
        /// <summary>
        /// Sets 2D interpolation to Interpolate.
        /// </summary>
        public static Rigidbody2D InterpolateOn(this Rigidbody2D rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
            return rigidbody;
        }

        /// <summary>
        /// Sets 3D interpolation to Extrapolate.
        /// </summary>
        public static Rigidbody Extrapolate(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.interpolation = RigidbodyInterpolation.Extrapolate;
            return rigidbody;
        }
        /// <summary>
        /// Sets 2D interpolation to Extrapolate.
        /// </summary>
        public static Rigidbody2D Extrapolate(this Rigidbody2D rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.interpolation = RigidbodyInterpolation2D.Extrapolate;
            return rigidbody;
        }

        /// <summary>
        /// Gets the interpolation mode of a 3D rigidbody.
        ///
        /// Interpolation helps smooth motion when rendering between physics steps.
        /// </summary>
        /// <param name="rigidbody">Target rigidbody.</param>
        /// <returns>Current interpolation mode.</returns>
        public static RigidbodyInterpolation GetInterpolation(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            return rigidbody.interpolation;
        }
        /// <summary>
        /// Gets the interpolation mode of a 2D rigidbody.
        ///
        /// Interpolation helps smooth motion when rendering between physics steps.
        /// </summary>
        /// <param name="rigidbody">Target rigidbody2D.</param>
        /// <returns>Current interpolation mode.</returns>
        public static RigidbodyInterpolation2D GetInterpolation(this Rigidbody2D rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            return rigidbody.interpolation;
        }

        /// <summary>
        /// Sets 3D collision detection mode to Continuous.
        /// </summary>
        public static Rigidbody ContinuousCollision(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            return rigidbody;
        }
        /// <summary>
        /// Sets 2D collision detection mode to Continuous.
        /// </summary>
        public static Rigidbody2D ContinuousCollision(this Rigidbody2D rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            return rigidbody;
        }

        /// <summary>
        /// Sets 3D collision detection mode to Discrete.
        /// </summary>
        public static Rigidbody DiscreteCollision(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            return rigidbody;
        }
        /// <summary>
        /// Sets 2D collision detection mode to Discrete.
        /// </summary>
        public static Rigidbody2D DiscreteCollision(this Rigidbody2D rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
            return rigidbody;
        }

        /// <summary>
        /// Sets 3D collision detection mode to ContinuousDynamic.
        /// </summary>
        public static Rigidbody DynamicContinuousCollision(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            return rigidbody;
        }

        /// <summary>
        /// Sets 3D collision detection mode to ContinuousSpeculative.
        /// </summary>
        public static Rigidbody SpeculativeContinuousCollision(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            return rigidbody;
        }

        /// <summary>
        /// Gets the collision detection mode of a 3D rigidbody.
        ///
        /// Collision detection mode controls how Unity detects collisions for fast-moving objects.
        /// </summary>
        /// <param name="rigidbody">Target rigidbody.</param>
        /// <returns>Current collision detection mode.</returns>
        public static CollisionDetectionMode GetCollisionDetectionMode(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            return rigidbody.collisionDetectionMode;
        }
        /// <summary>
        /// Gets the collision detection mode of a 2D rigidbody.
        ///
        /// Collision detection mode controls how Unity detects collisions for fast-moving objects.
        /// </summary>
        /// <param name="rigidbody">Target rigidbody2D.</param>
        /// <returns>Current collision detection mode.</returns>
        public static CollisionDetectionMode2D GetCollisionDetectionMode(this Rigidbody2D rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            return rigidbody.collisionDetectionMode;
        }

        /// <summary>
        /// Gets the current constraints of a 3D rigidbody.
        /// </summary>
        /// <param name="rigidbody">Target rigidbody.</param>
        /// <returns>Current <see cref="RigidbodyConstraints"/> flags.</returns>
        public static RigidbodyConstraints GetConstraints3D(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            return rigidbody.constraints;
        }
        /// <summary>
        /// Gets the current constraints of a 2D rigidbody.
        /// </summary>
        /// <param name="rigidbody">Target rigidbody2D.</param>
        /// <returns>Current <see cref="RigidbodyConstraints2D"/> flags.</returns>
        public static RigidbodyConstraints2D GetConstraints2D(this Rigidbody2D rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            return rigidbody.constraints;
        }

        /// <summary>
        /// Gets the sleep threshold of a 3D rigidbody.
        /// 
        /// Lower values make the rigidbody less likely to sleep.
        /// Setting it to 0 disables sleeping in practice.
        /// </summary>
        public static float GetSleepThreshold(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            return rigidbody.sleepThreshold;
        }

        /// <summary>
        /// Sets the sleep threshold of a 3D rigidbody.
        /// 
        /// Lower values make the rigidbody less likely to sleep.
        /// Setting it to 0 disables sleeping in practice.
        /// </summary>
        public static Rigidbody SetSleepThreshold(this Rigidbody rigidbody, float sleepThreshold)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.sleepThreshold = sleepThreshold;
            return rigidbody;
        }

        /// <summary>
        /// Forces the 3D rigidbody to sleep immediately.
        /// 
        /// Useful when you want to start the object in a sleeping state.
        /// </summary>
        public static Rigidbody SleepNow(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.Sleep();
            return rigidbody;
        }
        /// <summary>
        /// Forces the 2D rigidbody to sleep immediately.
        /// </summary>
        public static Rigidbody2D SleepNow(this Rigidbody2D rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.Sleep();
            return rigidbody;
        }

        /// <summary>
        /// Wakes up the 3D rigidbody.
        /// 
        /// Useful when you want to ensure the object starts awake or after manual sleep.
        /// </summary>
        public static Rigidbody WakeUpNow(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.WakeUp();
            return rigidbody;
        }
        /// <summary>
        /// Wakes up the 2D rigidbody.
        /// </summary>
        public static Rigidbody2D WakeUpNow(this Rigidbody2D rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            rigidbody.WakeUp();
            return rigidbody;
        }

        /// <summary>
        /// Gets whether the 3D rigidbody is currently sleeping.
        /// </summary>
        public static bool GetIsSleeping(this Rigidbody rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            return rigidbody.IsSleeping();
        }
        /// <summary>
        /// Gets whether the 2D rigidbody is currently sleeping.
        /// </summary>
        public static bool GetIsSleeping(this Rigidbody2D rigidbody)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            return rigidbody.IsSleeping();
        }

        /// <summary>
        /// Applies a high-level sleeping behavior preset to a 3D rigidbody.
        ///
        /// This method configures the initial sleep state and sleep threshold
        /// according to the selected <see cref="SleepMode"/>.
        /// </summary>
        /// <param name="rigidbody">Target rigidbody.</param>
        /// <param name="mode">Sleeping behavior preset.</param>
        /// <returns>The same rigidbody instance for fluent chaining.</returns>
        public static Rigidbody ApplySleepMode(this Rigidbody rigidbody, SleepMode mode)
        {
            if (rigidbody == null)
            {
                throw new ArgumentNullException(nameof(rigidbody));
            }

            switch (mode)
            {
                case SleepMode.NeverSleep:
                    {
                        rigidbody.sleepThreshold = 0f;
                        rigidbody.WakeUp();
                        break;
                    }

                case SleepMode.StartAwake:
                    {
                        rigidbody.WakeUp();
                        break;
                    }

                case SleepMode.StartAsleep:
                    {
                        rigidbody.Sleep();
                        break;
                    }
            }

            return rigidbody;
        }
    }
}
