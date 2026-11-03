using System;
using UnityEngine;

namespace MGC.UnityAdapter
{
    /// <summary>
    /// Provides Unity-friendly extension methods for working with Behaviour components
    /// on GameObject and Transform.
    ///
    /// This utility focuses on controlling the enabled state of Behaviour-derived components:
    /// - State queries (IsComponentEnabled)
    /// - Enabling and disabling single components (EnableComponent / DisableComponent)
    /// - Toggling enabled state (ToggleComponent)
    /// - Bulk operations on all components of a given type (EnableAll / DisableAll / SetAllEnabled)
    ///
    /// Design notes:
    /// - Methods return a boolean indicating whether the state actually changed.
    /// - All operations are safe: if a component is missing, methods return false instead of throwing.
    /// - Behaviour constraint ensures access to the enabled property at compile time.
    /// - Transform overloads delegate to the underlying GameObject for consistency.
    /// </summary>
    public static class GameObjectBehaviours
    {
        /// <summary>
        /// Returns enabled state of a Behaviour component if present, otherwise false.
        /// </summary>
        public static bool IsComponentEnabled<T>(this GameObject gameObject) where T : Behaviour
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            if (!gameObject.TryGetComponent<T>(out T component))
            {
                return false;
            }

            return component.enabled;
        }
        /// <summary>
        /// Returns enabled state of a Behaviour component on Transform's GameObject if present, otherwise false.
        /// </summary>
        public static bool IsComponentEnabled<T>(this Transform transform) where T : Behaviour
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return transform.gameObject.IsComponentEnabled<T>();
        }

        /// <summary>
        /// Enables a Behaviour component if present and currently disabled.
        /// </summary>
        public static bool EnableComponent<T>(this GameObject gameObject) where T : Behaviour
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            if (!gameObject.TryGetComponent<T>(out T component))
            {
                return false;
            }

            if (component.enabled)
            {
                return false;
            }

            component.enabled = true;
            return true;
        }
        /// <summary>
        /// Enables a Behaviour component on Transform's GameObject.
        /// </summary>
        public static bool EnableComponent<T>(this Transform transform) where T : Behaviour
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return transform.gameObject.EnableComponent<T>();
        }

        /// <summary>
        /// Disables a Behaviour component if present and currently enabled.
        /// </summary>
        public static bool DisableComponent<T>(this GameObject gameObject) where T : Behaviour
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            if (!gameObject.TryGetComponent<T>(out T component))
            {
                return false;
            }

            if (!component.enabled)
            {
                return false;
            }

            component.enabled = false;
            return true;
        }
        /// <summary>
        /// Disables a Behaviour component on Transform's GameObject.
        /// </summary>
        public static bool DisableComponent<T>(this Transform transform) where T : Behaviour
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return transform.gameObject.DisableComponent<T>();
        }

        /// <summary>
        /// Enables all Behaviour components of type T on the GameObject.
        /// Returns true if at least one component changed state.
        /// </summary>
        public static bool EnableAll<T>(this GameObject gameObject) where T : Behaviour
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            T[] components = gameObject.GetComponents<T>();

            bool changed = false;

            for (int i = 0; i < components.Length; i++)
            {
                T component = components[i];

                if (!component.enabled)
                {
                    component.enabled = true;
                    changed = true;
                }
            }

            return changed;
        }
        /// <summary>
        /// Enables all Behaviour components of type T on Transform's GameObject.
        /// </summary>
        public static bool EnableAll<T>(this Transform transform) where T : Behaviour
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return transform.gameObject.EnableAll<T>();
        }

        /// <summary>
        /// Disables all Behaviour components of type T on the GameObject.
        /// Returns true if at least one component changed state.
        /// </summary>
        public static bool DisableAll<T>(this GameObject gameObject) where T : Behaviour
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            T[] components = gameObject.GetComponents<T>();

            bool changed = false;

            for (int i = 0; i < components.Length; i++)
            {
                T component = components[i];

                if (component.enabled)
                {
                    component.enabled = false;
                    changed = true;
                }
            }

            return changed;
        }
        /// <summary>
        /// Disables all Behaviour components of type T on Transform's GameObject.
        /// </summary>
        public static bool DisableAll<T>(this Transform transform) where T : Behaviour
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return transform.gameObject.DisableAll<T>();
        }

        /// <summary>
        /// Sets enabled state for a Behaviour component of type T if it exists.
        /// Returns true only when the state actually changes.
        /// </summary>
        public static bool SetComponentEnabled<T>(this GameObject gameObject, bool enabled) where T : Behaviour
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            if (!gameObject.TryGetComponent<T>(out T component))
            {
                return false;
            }

            if (component.enabled == enabled)
            {
                return false;
            }

            component.enabled = enabled;
            return true;
        }
        /// <summary>
        /// Sets enabled state for a Behaviour component of type T on Transform's GameObject.
        /// </summary>
        public static bool SetComponentEnabled<T>(this Transform transform, bool enabled) where T : Behaviour
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return transform.gameObject.SetComponentEnabled<T>(enabled);
        }

        /// <summary>
        /// Sets enabled state for all Behaviour components of type T on the GameObject.
        /// Returns true if at least one component changed state.
        /// </summary>
        public static bool SetAllEnabled<T>(this GameObject gameObject, bool enabled) where T : Behaviour
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            T[] components = gameObject.GetComponents<T>();

            bool changed = false;

            for (int i = 0; i < components.Length; i++)
            {
                T component = components[i];

                if (component.enabled != enabled)
                {
                    component.enabled = enabled;
                    changed = true;
                }
            }

            return changed;
        }
        /// <summary>
        /// Sets enabled state for all Behaviour components of type T on Transform's GameObject.
        /// </summary>
        public static bool SetAllEnabled<T>(this Transform transform, bool enabled) where T : Behaviour
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return transform.gameObject.SetAllEnabled<T>(enabled);
        }

        /// <summary>
        /// Toggles enabled state of a Behaviour component of type T if present.
        /// </summary>
        /// <returns>True if component exists and was toggled; otherwise false.</returns>
        public static bool ToggleComponent<T>(this GameObject gameObject) where T : Behaviour
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            if (!gameObject.TryGetComponent<T>(out T component))
            {
                return false;
            }

            component.enabled = !component.enabled;
            return true;
        }
        /// <summary>
        /// Toggles enabled state of a Behaviour component of type T on Transform's GameObject.
        /// </summary>
        public static bool ToggleComponent<T>(this Transform transform) where T : Behaviour
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return transform.gameObject.ToggleComponent<T>();
        }
    }
}
