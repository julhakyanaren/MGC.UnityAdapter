using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGC.UnityAdapter
{
    /// <summary>
    /// Provides Unity-friendly extension methods for working with components on GameObject and Transform.
    ///
    /// This utility focuses on common component workflows:
    /// - Existence checks (HasComponent)
    /// - Safe retrieval (TryGetComponentSafe)
    /// - Required retrieval (GetRequiredComponent / GetRequiredComponents)
    /// - Get-or-add patterns (GetOrAddComponent)
    /// - Removal with optional immediate destruction (RemoveComponent)
    /// - Bulk retrieval into arrays or user-provided lists (GetComponentsSafe)
    /// - Manual parent/children traversal for finding a component (GetComponentInParents / GetComponentInChildrenRecursive)
    ///
    /// Design notes:
    /// - Methods that accept a List&lt;T&gt; use it to avoid allocations and GC pressure.
    /// - "Safe" methods never throw due to missing components; "Required" methods throw when missing.
    /// - Parent/children search methods are implemented without Unity's built-in helpers,
    ///   so behavior is explicit and controlled (includeSelf, includeInactive, recursive).
    /// </summary>
    public static class GameObjectComponents
    {
        /// <summary>
        /// Returns true if the GameObject has a component of type T.
        /// </summary>
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            return gameObject.TryGetComponent<T>(out _);
        }
        /// <summary>
        /// Returns true if the Transform has a component of type T (on the same GameObject).
        /// </summary>
        public static bool HasComponent<T>(this Transform transform) where T : Component
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return transform.TryGetComponent<T>(out _);
        }

        /// <summary>
        /// Tries to get a component from GameObject. If GameObject is null, returns false and sets component to null.
        /// </summary>
        public static bool TryGetComponentSafe<T>(this GameObject gameObject, out T component) where T : Component
        {
            if (gameObject == null)
            {
                component = null;
                return false;
            }

            return gameObject.TryGetComponent(out component);
        }
        /// <summary>
        /// Tries to get a component from Transform. If Transform is null, returns false and sets component to null.
        /// </summary>
        public static bool TryGetComponentSafe<T>(this Transform transform, out T component) where T : Component
        {
            if (transform == null)
            {
                component = null;
                return false;
            }

            return transform.TryGetComponent(out component);
        }

        /// <summary>
        /// Gets a component of type T or throws if it is missing.
        /// </summary>
        /// <exception cref="MissingComponentException">
        /// Thrown if the required component is not found on the GameObject.
        /// </exception>
        public static T GetRequiredComponent<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            T component = gameObject.GetComponent<T>();

            if (component == null)
            {
                throw new MissingComponentException(
                    $"Required component '{typeof(T).Name}' was not found on GameObject '{gameObject.name}'."
                );
            }

            return component;
        }
        /// <summary>
        /// Gets a component of type T from Transform's GameObject or throws if it is missing.
        /// </summary>
        /// <exception cref="MissingComponentException">
        /// Thrown if the required component is not found on the Transform's GameObject.
        /// </exception>
        public static T GetRequiredComponent<T>(this Transform transform) where T : Component
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            T component = transform.GetComponent<T>();

            if (component == null)
            {
                throw new MissingComponentException(
                    $"Required component '{typeof(T).Name}' was not found on GameObject '{transform.name}'."
                );
            }

            return component;
        }

        /// <summary>
        /// Returns the component of type T if present; otherwise adds it and returns the new instance.
        /// </summary>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            if (!gameObject.TryGetComponent<T>(out T component))
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }
        /// <summary>
        /// Returns the component of type T if present on Transform's GameObject; otherwise adds it.
        /// </summary>
        public static T GetOrAddComponent<T>(this Transform transform) where T : Component
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return transform.gameObject.GetOrAddComponent<T>();
        }

        /// <summary>
        /// Removes (destroys) a component of type T from the GameObject if it exists.
        /// If immediate is true, DestroyImmediate is used; otherwise Destroy is scheduled.
        /// </summary>
        /// <returns>True if a component was found and scheduled/destroyed; otherwise false.</returns>
        public static bool RemoveComponent<T>(this GameObject gameObject, bool immediate = false) where T : Component
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            if (!gameObject.TryGetComponent<T>(out T component))
            {
                return false;
            }

            if (immediate)
            {
                UnityEngine.Object.DestroyImmediate(component);
            }
            else
            {
                UnityEngine.Object.Destroy(component);
            }

            return true;
        }
        /// <summary>
        /// Removes (destroys) a component of type T from Transform's GameObject if it exists.
        /// </summary>
        public static bool RemoveComponent<T>(this Transform transform, bool immediate = false) where T : Component
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return transform.gameObject.RemoveComponent<T>(immediate);
        }

        /// <summary>
        /// Gets all components of type T from the GameObject.
        /// Returns an empty array if none exist.
        /// </summary>
        public static T[] GetComponentsSafe<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            return gameObject.GetComponents<T>();
        }
        /// <summary>
        /// Gets all components of type T from Transform's GameObject.
        /// Returns an empty array if none exist.
        /// </summary>
        public static T[] GetComponentsSafe<T>(this Transform transform) where T : Component
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return transform.GetComponents<T>();
        }

        /// <summary>
        /// Fills the provided list with all components of type T on the GameObject.
        /// The list is cleared before filling.
        /// </summary>
        /// <remarks>
        /// The list parameter exists to avoid allocations (no new List, no LINQ).
        /// </remarks>
        public static List<T> GetComponentsSafe<T>(this GameObject gameObject, List<T> result) where T : Component
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            result.Clear();

            T[] components = gameObject.GetComponents<T>();

            for (int i = 0; i < components.Length; i++)
            {
                result.Add(components[i]);
            }

            return result;
        }
        /// <summary>
        /// Fills the provided list with all components of type T on Transform's GameObject.
        /// </summary>
        public static List<T> GetComponentsSafe<T>(this Transform transform, List<T> result) where T : Component
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return transform.gameObject.GetComponentsSafe(result);
        }

        /// <summary>
        /// Gets all components of type T on the GameObject into result, and throws if none exist.
        /// </summary>
        /// <exception cref="MissingComponentException">
        /// Thrown if no components of type T were found on the GameObject.
        /// </exception>
        public static List<T> GetRequiredComponents<T>(this GameObject gameObject, List<T> result) where T : Component
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            gameObject.GetComponentsSafe(result);

            if (result.Count == 0)
            {
                throw new MissingComponentException(
                    $"{gameObject.name} does not have component {typeof(T).Name}"
                );
            }

            return result;
        }
        /// <summary>
        /// Gets all components of type T on Transform's GameObject into result, and throws if none exist.
        /// </summary>
        public static List<T> GetRequiredComponents<T>(this Transform transform, List<T> result) where T : Component
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return transform.gameObject.GetRequiredComponents<T>(result);
        }

        /// <summary>
        /// Searches upwards through parents for a component of type T.
        /// </summary>
        /// <param name="includeSelf">If true, checks the current GameObject first.</param>
        /// <returns>The first found component, or null if not found.</returns>
        public static T GetComponentInParents<T>(this GameObject gameObject, bool includeSelf = true)
            where T : Component
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Transform current = gameObject.transform;

            if (!includeSelf)
            {
                current = current.parent;
            }

            while (current != null)
            {
                if (current.TryGetComponent<T>(out T component))
                {
                    return component;
                }

                current = current.parent;
            }

            return null;
        }
        /// <summary>
        /// Searches upwards through parents for a component of type T starting from Transform.
        /// </summary>
        public static T GetComponentInParents<T>(this Transform transform, bool includeSelf = true)
            where T : Component
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return transform.gameObject.GetComponentInParents<T>(includeSelf);
        }

        /// <summary>
        /// Try-version of GetComponentInParents.
        /// </summary>
        public static bool TryGetComponentInParents<T>(this GameObject gameObject, out T component, bool includeSelf = true)
            where T : Component
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            component = gameObject.GetComponentInParents<T>(includeSelf);
            return component != null;
        }
        /// <summary>
        /// Try-version of GetComponentInParents for Transform.
        /// </summary>
        public static bool TryGetComponentInParents<T>(this Transform transform, out T component, bool includeSelf = true)
            where T : Component
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            component = transform.GetComponentInParents<T>(includeSelf);
            return component != null;
        }

        /// <summary>
        /// Searches recursively in children for a component of type T.
        /// </summary>
        /// <param name="includeInactive">If false, inactive GameObjects are skipped.</param>
        /// <param name="includeSelf">If true, checks the start GameObject first.</param>
        /// <returns>The first found component, or null if not found.</returns>
        public static T GetComponentInChildrenRecursive<T>(this GameObject gameObject, bool includeInactive = false, bool includeSelf = true)
            where T : Component
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Transform start = gameObject.transform;

            if (includeSelf)
            {
                if (start.TryGetComponent<T>(out T component))
                {
                    return component;
                }
            }

            int count = start.childCount;

            for (int i = 0; i < count; i++)
            {
                Transform child = start.GetChild(i);
                T found = FindInChildrenRecursive<T>(child, includeInactive);

                if (found != null)
                {
                    return found;
                }
            }

            return null;
        }
        /// <summary>
        /// Searches recursively in children for a component of type T starting from Transform.
        /// </summary>
        public static T GetComponentInChildrenRecursive<T>(this Transform transform, bool includeInactive = false, bool includeSelf = true)
            where T : Component
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return transform.gameObject.GetComponentInChildrenRecursive<T>(includeInactive, includeSelf);
        }

        /// <summary>
        /// Try-version of GetComponentInChildrenRecursive.
        /// </summary>
        public static bool TryGetComponentInChildrenRecursive<T>(this GameObject gameObject, out T component, bool includeInactive = false, bool includeSelf = true)
            where T : Component
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            component = gameObject.GetComponentInChildrenRecursive<T>(includeInactive, includeSelf);
            return component != null;
        }
        /// <summary>
        /// Try-version of GetComponentInChildrenRecursive for Transform.
        /// </summary>
        public static bool TryGetComponentInChildrenRecursive<T>(this Transform transform, out T component, bool includeInactive = false, bool includeSelf = true)
            where T : Component
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            component = transform.GetComponentInChildrenRecursive<T>(includeInactive, includeSelf);
            return component != null;
        }

        private static T FindInChildrenRecursive<T>(Transform transform, bool includeInactive)
            where T : Component
        {
            if (transform == null)
            {
                return null;
            }

            GameObject go = transform.gameObject;

            if (!includeInactive && !go.activeSelf)
            {
                return null;
            }

            if (transform.TryGetComponent<T>(out T component))
            {
                return component;
            }

            int count = transform.childCount;

            for (int i = 0; i < count; i++)
            {
                Transform child = transform.GetChild(i);
                T found = FindInChildrenRecursive<T>(child, includeInactive);

                if (found != null)
                {
                    return found;
                }
            }

            return null;
        }
    }
}
