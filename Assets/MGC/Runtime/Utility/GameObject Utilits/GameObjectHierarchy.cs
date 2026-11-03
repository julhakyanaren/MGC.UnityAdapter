using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MGC.UnityAdapter
{
    /// <summary>
    /// Provides hierarchy-related extension methods for <see cref="GameObject"/> and <see cref="Transform"/>.
    ///
    /// This class focuses on common operations when working with Unity's Transform hierarchy:
    /// - Parent access and reassignment (including clearing parent).
    /// - Relationship checks (has parent, has children, child-of / parent-of, direct vs recursive).
    /// - Child enumeration and retrieval (count, list building, safe try-get).
    /// - Name-based search among children (first match, multiple matches, optional recursion).
    /// - Sibling order utilities (get/set index, move up/down, swap).
    /// - Root / top-most parent utilities and path helpers (path building and path-based lookup).
    /// - Active state helpers (self vs in-hierarchy, toggle, recursive activation).
    /// - Child creation and child cleanup (detach, destroy, ensure child).
    ///
    /// Notes:
    /// - "World" vs "Local" for parenting: Unity's <see cref="Transform.SetParent(Transform, bool)"/> uses
    ///   <paramref name="worldPositionStays"/> to decide whether to keep world transforms (true) or keep local transforms (false).
    /// - Methods that search by name use exact string comparison on <see cref="UnityEngine.Object.name"/>.
    /// - Path methods treat a path starting with the scene root name as an absolute path; otherwise it is relative to the start transform.
    /// </summary>
    public static class GameObjectHierarchy
    {
        /// <summary>
        /// Gets the parent <see cref="Transform"/> of the specified <see cref="GameObject"/>.
        /// </summary>
        /// <param name="gameObject">Target game object.</param>
        /// <returns>Parent transform, or null if the object has no parent.</returns>
        public static Transform GetParentTransform(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            return gameObject.transform.parent;
        }
        /// <summary>
        /// Gets the parent <see cref="Transform"/> of the specified <see cref="Transform"/>.
        /// </summary>
        /// <param name="transform">Target transform.</param>
        /// <returns>Parent transform, or null if there is no parent.</returns>
        public static Transform GetParentTransform(this Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return transform.parent;
        }

        /// <summary>
        /// Gets the parent <see cref="GameObject"/> of the specified <see cref="GameObject"/>.
        /// </summary>
        /// <param name="gameObject">Target game object.</param>
        /// <returns>Parent game object, or null if the object has no parent.</returns>
        public static GameObject GetParentGameObject(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Transform parent = gameObject.transform.parent;

            if (parent == null)
            {
                return null;
            }

            return parent.gameObject;
        }
        /// <summary>
        /// Gets the parent <see cref="GameObject"/> of the specified <see cref="Transform"/>.
        /// </summary>
        /// <param name="transform">Target transform.</param>
        /// <returns>Parent game object, or null if there is no parent.</returns>
        public static GameObject GetParentGameObject(this Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            Transform parent = transform.parent;

            if (parent == null)
            {
                return null;
            }

            return parent.gameObject;
        }


        /// <summary>
        /// Sets the specified <paramref name="parent"/> as the parent of <paramref name="gameObject"/>.
        /// </summary>
        /// <param name="gameObject">Target game object.</param>
        /// <param name="parent">New parent game object.</param>
        /// <param name="worldPositionStays">
        /// If true, the world position/rotation/scale are preserved and local values are recomputed.
        /// If false, local values are preserved and the world transform may change.
        /// </param>
        public static void SetParentTo(this GameObject gameObject, GameObject parent, bool worldPositionStays = true)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }
            gameObject.transform.SetParent(parent.transform, worldPositionStays);
        }
        /// <summary>
        /// Sets the specified <paramref name="parent"/> as the parent of <paramref name="gameObject"/>.
        /// </summary>
        /// <param name="gameObject">Target game object.</param>
        /// <param name="parent">New parent transform.</param>
        /// <param name="worldPositionStays">
        /// If true, the world position/rotation/scale are preserved and local values are recomputed.
        /// If false, local values are preserved and the world transform may change.
        /// </param>
        public static void SetParentTo(this GameObject gameObject, Transform parent, bool worldPositionStays = true)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }
            gameObject.transform.SetParent(parent, worldPositionStays);
        }

        /// <summary>
        /// Clears the parent of the specified <see cref="GameObject"/> (sets parent to null).
        /// </summary>
        /// <param name="gameObject">Target game object.</param>
        /// <param name="worldPositionStays">
        /// If true, the world position/rotation/scale are preserved and local values are recomputed.
        /// If false, local values are preserved and the world transform may change.
        /// </param>
        public static void ClearParent(this GameObject gameObject, bool worldPositionStays = true)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            gameObject.transform.SetParent(null, worldPositionStays);
        }

        /// <summary>
        /// Returns true if the specified game object has a parent.
        /// </summary>
        /// <param name="gameObject">Target game object.</param>
        /// <returns>True if <see cref="Transform.parent"/> is not null.</returns>
        public static bool HasParent(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.parent != null;
        }
        /// <summary>
        /// Returns true if the specified game object has at least one child.
        /// </summary>
        /// <param name="gameObject">Target game object.</param>
        /// <returns>True if <see cref="Transform.childCount"/> is greater than zero.</returns>
        public static bool HasChildren(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.childCount > 0;
        }

        /// <summary>
        /// Returns true if <paramref name="gameObject"/> is a descendant of <paramref name="parent"/>
        /// (i.e., parent, grandparent, etc.).
        /// </summary>
        /// <param name="gameObject">Target game object.</param>
        /// <param name="parent">Potential ancestor.</param>
        /// <returns>True if <paramref name="parent"/> is found in the parent chain.</returns>
        public static bool IsChildOf(this GameObject gameObject, GameObject parent)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            Transform current = gameObject.transform.parent;

            while (current != null)
            {
                if (current.gameObject == parent)
                {
                    return true;
                }

                current = current.parent;
            }

            return false;
        }
        /// <summary>
        /// Returns true if <paramref name="gameObject"/> is a descendant of <paramref name="parent"/>.
        /// </summary>
        /// <param name="gameObject">Target game object.</param>
        /// <param name="parent">Potential ancestor transform.</param>
        /// <returns>True if <paramref name="parent"/> is found in the parent chain.</returns>
        public static bool IsChildOf(this GameObject gameObject, Transform parent)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            return gameObject.transform.IsChildOf(parent);
        }
        /// <summary>
        /// Returns true if <paramref name="transform"/> is a descendant of <paramref name="parent"/>
        /// (i.e., parent, grandparent, etc.).
        /// </summary>
        /// <param name="transform">Target transform.</param>
        /// <param name="parent">Potential ancestor transform.</param>
        /// <returns>True if <paramref name="parent"/> is found in the parent chain.</returns>
        public static bool IsChildOf(this Transform transform, Transform parent)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            Transform current = transform.parent;

            while (current != null)
            {
                if (current == parent)
                {
                    return true;
                }

                current = current.parent;
            }

            return false;
        }
        /// <summary>
        /// Returns true if <paramref name="transform"/> is a descendant of <paramref name="parent"/>.
        /// </summary>
        /// <param name="transform">Target transform.</param>
        /// <param name="parent">Potential ancestor game object.</param>
        /// <returns>True if <paramref name="parent"/> is found in the parent chain.</returns>
        public static bool IsChildOf(this Transform transform, GameObject parent)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            return transform.IsChildOf(parent.transform);
        }

        /// <summary>
        /// Returns true if <paramref name="gameObject"/> is a direct child of <paramref name="parent"/>
        /// (i.e., its immediate parent).
        /// </summary>
        /// <param name="gameObject">Target game object.</param>
        /// <param name="parent">Potential direct parent.</param>
        /// <returns>True if the direct parent matches <paramref name="parent"/>.</returns>
        public static bool IsDirectChildOf(this GameObject gameObject, GameObject parent)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            Transform directParent = gameObject.transform.parent;

            if (directParent == null)
            {
                return false;
            }

            return directParent.gameObject == parent;
        }
        /// <summary>
        /// Returns true if <paramref name="gameObject"/> is a direct child of <paramref name="parent"/>.
        /// </summary>
        /// <param name="gameObject">Target game object.</param>
        /// <param name="parent">Potential direct parent transform.</param>
        /// <returns>True if the direct parent matches <paramref name="parent"/>.</returns>
        public static bool IsDirectChildOf(this GameObject gameObject, Transform parent)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            Transform directParent = gameObject.transform.parent;

            if (directParent == null)
            {
                return false;
            }

            return directParent == parent;
        }
        /// <summary>
        /// Returns true if <paramref name="transform"/> is a direct child of <paramref name="parent"/>.
        /// </summary>
        /// <param name="transform">Target transform.</param>
        /// <param name="parent">Potential direct parent transform.</param>
        /// <returns>True if the direct parent matches <paramref name="parent"/>.</returns>
        public static bool IsDirectChildOf(this Transform transform, Transform parent)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            Transform directParent = transform.parent;

            if (directParent == null)
            {
                return false;
            }

            return directParent == parent;
        }
        /// <summary>
        /// Returns true if <paramref name="transform"/> is a direct child of <paramref name="parent"/>.
        /// </summary>
        /// <param name="transform">Target transform.</param>
        /// <param name="parent">Potential direct parent game object.</param>
        /// <returns>True if the direct parent matches <paramref name="parent"/>.</returns>
        public static bool IsDirectChildOf(this Transform transform, GameObject parent)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            Transform directParent = transform.parent;

            if (directParent == null)
            {
                return false;
            }

            return directParent.gameObject == parent;
        }

        /// <summary>
        /// Returns true if <paramref name="parent"/> is an ancestor of <paramref name="child"/>.
        /// </summary>
        /// <param name="parent">Potential ancestor.</param>
        /// <param name="child">Potential descendant.</param>
        /// <returns>True if <paramref name="child"/> is a descendant of <paramref name="parent"/>.</returns>
        public static bool IsParentOf(this GameObject parent, GameObject child)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }
            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            return child.IsChildOf(parent);
        }
        /// <summary>
        /// Returns true if <paramref name="parent"/> is an ancestor of <paramref name="child"/>.
        /// </summary>
        /// <param name="parent">Potential ancestor.</param>
        /// <param name="child">Potential descendant transform.</param>
        /// <returns>True if <paramref name="child"/> is a descendant of <paramref name="parent"/>.</returns>
        public static bool IsParentOf(this GameObject parent, Transform child)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }
            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            return child.IsChildOf(parent);
        }
        /// <summary>
        /// Returns true if <paramref name="parent"/> is an ancestor of <paramref name="child"/>.
        /// </summary>
        /// <param name="parent">Potential ancestor transform.</param>
        /// <param name="child">Potential descendant transform.</param>
        /// <returns>True if <paramref name="child"/> is a descendant of <paramref name="parent"/>.</returns>
        public static bool IsParentOf(this Transform parent, Transform child)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }
            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            return child.IsChildOf(parent);
        }
        /// <summary>
        /// Returns true if <paramref name="parent"/> is an ancestor of <paramref name="child"/>.
        /// </summary>
        /// <param name="parent">Potential ancestor transform.</param>
        /// <param name="child">Potential descendant game object.</param>
        /// <returns>True if <paramref name="child"/> is a descendant of <paramref name="parent"/>.</returns>
        public static bool IsParentOf(this Transform parent, GameObject child)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }
            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            return child.IsChildOf(parent);
        }

        /// <summary>
        /// Returns true if <paramref name="parent"/> is the direct parent of <paramref name="child"/>.
        /// </summary>
        /// <param name="parent">Potential direct parent.</param>
        /// <param name="child">Potential direct child.</param>
        /// <returns>True if <paramref name="child"/>'s immediate parent matches <paramref name="parent"/>.</returns>
        public static bool IsDirectParentOf(this GameObject parent, GameObject child)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }
            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            return child.IsDirectChildOf(parent);
        }
        /// <summary>
        /// Returns true if <paramref name="parent"/> is the direct parent of <paramref name="child"/>.
        /// </summary>
        /// <param name="parent">Potential direct parent.</param>
        /// <param name="child">Potential direct child transform.</param>
        /// <returns>True if <paramref name="child"/>'s immediate parent matches <paramref name="parent"/>.</returns>
        public static bool IsDirectParentOf(this GameObject parent, Transform child)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }
            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            return child.IsDirectChildOf(parent);
        }
        /// <summary>
        /// Returns true if <paramref name="parent"/> is the direct parent of <paramref name="child"/>.
        /// </summary>
        /// <param name="parent">Potential direct parent transform.</param>
        /// <param name="child">Potential direct child transform.</param>
        /// <returns>True if <paramref name="child"/>'s immediate parent matches <paramref name="parent"/>.</returns>
        public static bool IsDirectParentOf(this Transform parent, Transform child)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }
            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            return child.IsDirectChildOf(parent);
        }
        /// <summary>
        /// Returns true if <paramref name="parent"/> is the direct parent of <paramref name="child"/>.
        /// </summary>
        /// <param name="parent">Potential direct parent transform.</param>
        /// <param name="child">Potential direct child game object.</param>
        /// <returns>True if <paramref name="child"/>'s immediate parent matches <paramref name="parent"/>.</returns>
        public static bool IsDirectParentOf(this Transform parent, GameObject child)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }
            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            return child.IsDirectChildOf(parent);
        }

        /// <summary>
        /// Gets the number of direct children for the specified game object.
        /// </summary>
        /// <param name="gameObject">Target game object.</param>
        /// <returns>The value of <see cref="Transform.childCount"/>.</returns>
        public static int GetChildCount(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            return gameObject.transform.childCount;
        }

        /// <summary>
        /// Fills the provided list with direct child game objects.
        /// </summary>
        /// <param name="gameObject">Parent game object.</param>
        /// <param name="children">List to fill (cleared first).</param>
        /// <param name="includeInactive">If false, children with <see cref="GameObject.activeSelf"/> == false are skipped.</param>
        public static void GetChildrenList(this GameObject gameObject, List<GameObject> children, bool includeInactive = false)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            if (children == null)
            {
                throw new ArgumentNullException(nameof(children));
            }

            children.Clear();

            Transform parent = gameObject.transform;
            int count = parent.childCount;

            for (int i = 0; i < count; i++)
            {
                GameObject childObject = parent.GetChild(i).gameObject;

                if (!includeInactive && !childObject.activeSelf)
                {
                    continue;
                }

                children.Add(childObject);
            }
        }
        /// <summary>
        /// Creates and returns a list of direct child game objects.
        /// </summary>
        /// <param name="gameObject">Parent game object.</param>
        /// <param name="includeInactive">If false, children with <see cref="GameObject.activeSelf"/> == false are skipped.</param>
        /// <returns>A new list containing the direct children.</returns>
        public static List<GameObject> GetChildrenList(this GameObject gameObject, bool includeInactive = false)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            List<GameObject> children = new List<GameObject>();
            gameObject.GetChildrenList(children, includeInactive);
            return children;
        }

        /// <summary>
        /// Fills the provided list with direct child game objects, ensuring uniqueness.
        /// </summary>
        /// <param name="gameObject">Parent game object.</param>
        /// <param name="children">List to fill (cleared first).</param>
        /// <param name="includeInactive">If false, children with <see cref="GameObject.activeSelf"/> == false are skipped.</param>
        /// <remarks>
        /// Direct children are inherently unique in Unity, but this method keeps a "unique" variant as API convenience.
        /// </remarks>
        public static void GetChildrenListUnique(this GameObject gameObject, List<GameObject> children, bool includeInactive = false)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            if (children == null)
            {
                throw new ArgumentNullException(nameof(children));
            }

            children.Clear();

            HashSet<GameObject> unique = new HashSet<GameObject>();

            Transform parent = gameObject.transform;
            int count = parent.childCount;

            for (int i = 0; i < count; i++)
            {
                GameObject childObject = parent.GetChild(i).gameObject;

                if (!includeInactive && !childObject.activeSelf)
                {
                    continue;
                }

                if (unique.Add(childObject))
                {
                    children.Add(childObject);
                }
            }
        }
        /// <summary>
        /// Creates and returns a list of direct child game objects, ensuring uniqueness.
        /// </summary>
        /// <param name="gameObject">Parent game object.</param>
        /// <param name="includeInactive">If false, children with <see cref="GameObject.activeSelf"/> == false are skipped.</param>
        /// <returns>A new list containing the direct children.</returns>
        public static List<GameObject> GetChildrenListUnique(this GameObject gameObject, bool includeInactive = false)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            List<GameObject> children = new List<GameObject>();
            gameObject.GetChildrenListUnique(children, includeInactive);
            return children;
        }

        /// <summary>
        /// Gets a direct child game object at the specified index.
        /// </summary>
        /// <param name="gameObject">Parent game object.</param>
        /// <param name="index">Child index in [0, childCount).</param>
        /// <returns>Child game object at the specified index.</returns>
        public static GameObject GetChildAt(this GameObject gameObject, int index)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Transform parent = gameObject.transform;

            if (index < 0 || index >= parent.childCount)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return parent.GetChild(index).gameObject;
        }

        /// <summary>
        /// Tries to get a direct child game object at the specified index.
        /// </summary>
        /// <param name="gameObject">Parent game object.</param>
        /// <param name="index">Child index.</param>
        /// <param name="child">Output child if found; otherwise null.</param>
        /// <returns>True if the index is valid; otherwise false.</returns>
        public static bool TryGetChildAt(this GameObject gameObject, int index, out GameObject child)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            child = null;

            Transform parent = gameObject.transform;
            int count = parent.childCount;

            if (index < 0 || index >= count)
            {
                return false;
            }

            child = parent.GetChild(index).gameObject;
            return true;
        }
        /// <summary>
        /// Tries to get a direct child transform at the specified index.
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="index">Child index.</param>
        /// <param name="child">Output child if found; otherwise null.</param>
        /// <returns>True if the index is valid; otherwise false.</returns>
        public static bool TryGetChildAt(this Transform transform, int index, out Transform child)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            child = null;

            int count = transform.childCount;

            if (index < 0 || index >= count)
            {
                return false;
            }

            child = transform.GetChild(index);
            return true;
        }

        /// <summary>
        /// Finds the first direct child by name. Optionally searches recursively in the subtree.
        /// </summary>
        /// <param name="gameObject">Parent game object.</param>
        /// <param name="name">Child name to match exactly.</param>
        /// <param name="recursive">If true, searches recursively through descendants.</param>
        /// <returns>The first matching child game object, or null if not found.</returns>
        public static GameObject FindFirstChildByName(this GameObject gameObject, string name, bool recursive = false)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name must not be null or empty.", nameof(name));
            }

            Transform parent = gameObject.transform;
            int count = parent.childCount;

            for (int i = 0; i < count; i++)
            {
                Transform child = parent.GetChild(i);

                if (child.name == name)
                {
                    return child.gameObject;
                }
            }

            if (!recursive)
            {
                return null;
            }

            for (int i = 0; i < count; i++)
            {
                Transform child = parent.GetChild(i);
                GameObject result = child.gameObject.FindFirstChildByName(name, true);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }
        /// <summary>
        /// Finds the first direct child by name. Optionally searches recursively in the subtree.
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="name">Child name to match exactly.</param>
        /// <param name="recursive">If true, searches recursively through descendants.</param>
        /// <returns>The first matching child transform, or null if not found.</returns>
        public static Transform FindFirstChildByName(this Transform transform, string name, bool recursive = false)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name must not be null or empty.", nameof(name));
            }

            int count = transform.childCount;

            for (int i = 0; i < count; i++)
            {
                Transform child = transform.GetChild(i);

                if (child.name == name)
                {
                    return child;
                }
            }

            if (!recursive)
            {
                return null;
            }

            for (int i = 0; i < count; i++)
            {
                Transform child = transform.GetChild(i);
                Transform result = child.FindFirstChildByName(name, true);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Tries to find the first child by name (optionally recursively).
        /// </summary>
        /// <param name="gameObject">Parent game object.</param>
        /// <param name="name">Child name to match exactly.</param>
        /// <param name="recursive">If true, searches recursively through descendants.</param>
        /// <param name="child">Output found child game object; otherwise null.</param>
        /// <returns>True if found; otherwise false.</returns>
        public static bool TryFindFirstChildByName(this GameObject gameObject, string name, bool recursive, out GameObject child)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            child = gameObject.FindFirstChildByName(name, recursive);
            return child != null;
        }
        /// <summary>
        /// Tries to find the first child by name (optionally recursively).
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="name">Child name to match exactly.</param>
        /// <param name="recursive">If true, searches recursively through descendants.</param>
        /// <param name="child">Output found child transform; otherwise null.</param>
        /// <returns>True if found; otherwise false.</returns>
        public static bool TryFindFirstChildByName(this Transform transform, string name, bool recursive, out Transform child)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            child = transform.FindFirstChildByName(name, recursive);
            return child != null;
        }

        /// <summary>
        /// Finds all direct children that match the given name.
        /// </summary>
        /// <param name="gameObject">Parent game object.</param>
        /// <param name="name">Child name to match exactly.</param>
        /// <returns>List of matching direct children (may be empty).</returns>
        public static List<GameObject> FindFirstChildrenByName(this GameObject gameObject, string name)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name must not be null or empty.", nameof(name));
            }

            List<GameObject> result = new List<GameObject>();

            Transform parent = gameObject.transform;
            int count = parent.childCount;

            for (int i = 0; i < count; i++)
            {
                Transform child = parent.GetChild(i);

                if (child.name == name)
                {
                    result.Add(child.gameObject);
                }
            }

            return result;
        }

        /// <summary>
        /// Tries to find all direct children that match the given name.
        /// </summary>
        /// <param name="gameObject">Parent game object.</param>
        /// <param name="name">Child name to match exactly.</param>
        /// <param name="children">Output list of matching direct children.</param>
        /// <returns>True if at least one match exists; otherwise false.</returns>
        public static bool TryFindFirstChildrenByName(this GameObject gameObject, string name, out List<GameObject> children)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name must not be null or empty.", nameof(name));
            }

            children = new List<GameObject>();

            Transform parent = gameObject.transform;
            int count = parent.childCount;

            for (int i = 0; i < count; i++)
            {
                Transform child = parent.GetChild(i);

                if (child.name == name)
                {
                    children.Add(child.gameObject);
                }
            }

            return children.Count > 0;
        }

        /// <summary>
        /// Finds direct children that match the given name and writes unique results into <paramref name="result"/>.
        /// </summary>
        /// <param name="gameObject">Parent game object.</param>
        /// <param name="name">Child name to match exactly.</param>
        /// <param name="result">List to fill (cleared first).</param>
        public static void FindChildrenByNameUnique(this GameObject gameObject, string name, List<GameObject> result)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name must not be null or empty.", nameof(name));
            }
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            result.Clear();

            HashSet<GameObject> unique = new HashSet<GameObject>();

            Transform parent = gameObject.transform;
            int count = parent.childCount;

            for (int i = 0; i < count; i++)
            {
                Transform child = parent.GetChild(i);

                if (child.name != name)
                {
                    continue;
                }

                GameObject childObject = child.gameObject;

                if (unique.Add(childObject))
                {
                    result.Add(childObject);
                }
            }
        }
        /// <summary>
        /// Finds direct children that match the given name and returns a unique list.
        /// </summary>
        /// <param name="gameObject">Parent game object.</param>
        /// <param name="name">Child name to match exactly.</param>
        /// <returns>List of matching direct children (unique).</returns>
        public static List<GameObject> FindChildrenByNameUnique(this GameObject gameObject, string name)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            List<GameObject> result = new List<GameObject>();
            gameObject.FindChildrenByNameUnique(name, result);
            return result;
        }

        /// <summary>
        /// Tries to find direct children that match the given name and returns a unique list.
        /// </summary>
        /// <param name="gameObject">Parent game object.</param>
        /// <param name="name">Child name to match exactly.</param>
        /// <param name="children">Output list of matching direct children.</param>
        /// <returns>True if at least one match exists; otherwise false.</returns>
        public static bool TryFindChildrenByNameUnique(this GameObject gameObject, string name, out List<GameObject> children)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            children = new List<GameObject>();
            gameObject.FindChildrenByNameUnique(name, children);

            return children.Count > 0;
        }

        /// <summary>
        /// Executes an action for each direct child of the specified game object.
        /// </summary>
        /// <param name="gameObject">Parent game object.</param>
        /// <param name="action">Action to execute per child.</param>
        /// <param name="includeInactive">If false, children with <see cref="GameObject.activeSelf"/> == false are skipped.</param>
        public static void ForEachChild(this GameObject gameObject, Action<GameObject> action, bool includeInactive = true)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Transform parent = gameObject.transform;
            int count = parent.childCount;

            for (int i = 0; i < count; i++)
            {
                GameObject childObject = parent.GetChild(i).gameObject;

                if (!includeInactive && !childObject.activeSelf)
                {
                    continue;
                }

                action(childObject);
            }
        }
        /// <summary>
        /// Executes an action for each direct child of the specified game object.
        /// </summary>
        /// <param name="gameObject">Parent game object.</param>
        /// <param name="action">Action to execute per child transform.</param>
        /// <param name="includeInactive">If false, children with <see cref="GameObject.activeSelf"/> == false are skipped.</param>
        public static void ForEachChild(this GameObject gameObject, Action<Transform> action, bool includeInactive = true)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Transform parent = gameObject.transform;
            int count = parent.childCount;

            for (int i = 0; i < count; i++)
            {
                Transform child = parent.GetChild(i);

                if (!includeInactive && !child.gameObject.activeSelf)
                {
                    continue;
                }

                action(child);
            }
        }
        /// <summary>
        /// Executes an action for each direct child of the specified transform.
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="action">Action to execute per child game object.</param>
        /// <param name="includeInactive">If false, children with <see cref="GameObject.activeSelf"/> == false are skipped.</param>
        public static void ForEachChild(this Transform transform, Action<GameObject> action, bool includeInactive = true)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            int count = transform.childCount;

            for (int i = 0; i < count; i++)
            {
                GameObject childObject = transform.GetChild(i).gameObject;

                if (!includeInactive && !childObject.activeSelf)
                {
                    continue;
                }

                action(childObject);
            }
        }
        /// <summary>
        /// Executes an action for each direct child of the specified transform.
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="action">Action to execute per child transform.</param>
        /// <param name="includeInactive">If false, children with <see cref="GameObject.activeSelf"/> == false are skipped.</param>
        public static void ForEachChild(this Transform transform, Action<Transform> action, bool includeInactive = true)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            int count = transform.childCount;

            for (int i = 0; i < count; i++)
            {
                Transform child = transform.GetChild(i);

                if (!includeInactive && !child.gameObject.activeSelf)
                {
                    continue;
                }

                action(child);
            }
        }

        /// <summary>
        /// Gets the sibling index of the specified game object under its parent.
        /// </summary>
        public static int GetSiblingIndex(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            return gameObject.transform.GetSiblingIndex();
        }
        /// <summary>
        /// Gets the sibling index of the specified transform under its parent.
        /// </summary>
        public static int GetSiblingIndex(this Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return transform.GetSiblingIndex();
        }

        /// <summary>
        /// Sets the sibling index of the specified game object under its parent.
        /// </summary>
        public static void SetSiblingIndex(this GameObject gameObject, int index)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            gameObject.transform.SetSiblingIndex(index);
        }
        /// <summary>
        /// Sets the sibling index of the specified transform under its parent.
        /// </summary>
        public static void SetSiblingIndex(this Transform transform, int index)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            transform.SetSiblingIndex(index);
        }

        /// <summary>
        /// Moves the specified game object to be the first sibling under its parent.
        /// </summary>
        public static void SetAsFirstSibling(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            gameObject.transform.SetAsFirstSibling();
        }
        /// <summary>
        /// Moves the specified transform to be the first sibling under its parent.
        /// </summary>
        public static void SetAsFirstSibling(this Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            transform.SetAsFirstSibling();
        }

        /// <summary>
        /// Moves the specified game object to be the last sibling under its parent.
        /// </summary>
        public static void SetAsLastSibling(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            gameObject.transform.SetAsLastSibling();
        }
        /// <summary>
        /// Moves the specified transform to be the last sibling under its parent.
        /// </summary>
        public static void SetAsLastSibling(this Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            transform.SetAsLastSibling();
        }

        /// <summary>
        /// Moves the specified game object up by one position in its sibling order.
        /// </summary>
        /// <returns>True if the object moved; false if it is already the first sibling.</returns>
        public static bool MoveUp(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            return gameObject.transform.MoveUp();
        }
        /// <summary>
        /// Moves the specified transform up by one position in its sibling order.
        /// </summary>
        /// <returns>True if the transform moved; false if it is already the first sibling.</returns>
        public static bool MoveUp(this Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            int index = transform.GetSiblingIndex();

            if (index <= 0)
            {
                return false;
            }

            transform.SetSiblingIndex(index - 1);
            return true;
        }

        /// <summary>
        /// Moves the specified game object down by one position in its sibling order.
        /// </summary>
        /// <returns>True if the object moved; false if it is already the last sibling or has no parent.</returns>
        public static bool MoveDown(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            return gameObject.transform.MoveDown();
        }
        /// <summary>
        /// Moves the specified transform down by one position in its sibling order.
        /// </summary>
        /// <returns>True if the transform moved; false if it is already the last sibling or has no parent.</returns>
        public static bool MoveDown(this Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            Transform parent = transform.parent;

            if (parent == null)
            {
                return false;
            }

            int index = transform.GetSiblingIndex();
            int lastIndex = parent.childCount - 1;

            if (index >= lastIndex)
            {
                return false;
            }

            transform.SetSiblingIndex(index + 1);
            return true;
        }

        /// <summary>
        /// Swaps sibling positions of two game objects under the same parent.
        /// </summary>
        public static void SwapSiblings(this GameObject a, GameObject b)
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            a.transform.SwapSiblings(b.transform);
        }
        /// <summary>
        /// Swaps sibling positions of two transforms under the same parent.
        /// </summary>
        /// <remarks>
        /// Both transforms must have a parent and share the same parent.
        /// </remarks>
        public static void SwapSiblings(this Transform a, Transform b)
        {
            if (a == null)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b == null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            Transform parentA = a.parent;
            Transform parentB = b.parent;

            if (parentA == null || parentB == null)
            {
                throw new ArgumentException("Both transforms must have a parent to swap sibling positions.");
            }
            if (parentA != parentB)
            {
                throw new ArgumentException("Both transforms must share the same parent to swap sibling positions.");
            }

            int indexA = a.GetSiblingIndex();
            int indexB = b.GetSiblingIndex();

            if (indexA == indexB)
            {
                return;
            }

            a.SetSiblingIndex(indexB);
            b.SetSiblingIndex(indexA);
        }

        /// <summary>
        /// Gets the root transform of the hierarchy containing the specified game object.
        /// </summary>
        public static Transform GetRoot(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            return gameObject.transform.root;
        }
        /// <summary>
        /// Gets the root transform of the hierarchy containing the specified transform.
        /// </summary>
        public static Transform GetRoot(this Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return transform.root;
        }

        /// <summary>
        /// Gets the top-most parent by walking up <see cref="Transform.parent"/> until null.
        /// </summary>
        public static Transform GetTopMostParent(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Transform current = gameObject.transform;

            while (current.parent != null)
            {
                current = current.parent;
            }

            return current;
        }
        /// <summary>
        /// Gets the top-most parent by walking up <see cref="Transform.parent"/> until null.
        /// </summary>
        public static Transform GetTopMostParent(this Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            Transform current = transform;

            while (current.parent != null)
            {
                current = current.parent;
            }

            return current;
        }

        /// <summary>
        /// Builds a path string from the root to the specified game object (e.g. "Root/Player/Weapon").
        /// </summary>
        /// <param name="gameObject">Target game object.</param>
        /// <param name="separator">Separator character used between path parts.</param>
        /// <returns>Path string from root to the object.</returns>
        public static string GetPathFromRoot(this GameObject gameObject, char separator = '/')
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            return gameObject.transform.GetPathFromRoot(separator);
        }
        /// <summary>
        /// Builds a path string from the root to the specified transform (e.g. "Root/Player/Weapon").
        /// </summary>
        /// <param name="transform">Target transform.</param>
        /// <param name="separator">Separator character used between path parts.</param>
        /// <returns>Path string from root to the transform.</returns>
        public static string GetPathFromRoot(this Transform transform, char separator = '/')
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            List<string> parts = new List<string>();
            Transform current = transform;

            while (current != null)
            {
                parts.Add(current.name);
                current = current.parent;
            }

            parts.Reverse();

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < parts.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(separator);
                }

                sb.Append(parts[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Finds a child by a path relative to the specified game object or absolute from root.
        /// </summary>
        /// <param name="gameObject">Start game object.</param>
        /// <param name="path">Path string (e.g. "Root/Player/Weapon" or "Player/Weapon").</param>
        /// <param name="separator">Separator character used between path parts.</param>
        /// <returns>Found game object, or null if not found.</returns>
        public static GameObject FindByPath(this GameObject gameObject, string path, char separator = '/')
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Transform result = gameObject.transform.FindByPathTransform(path, separator);

            if (result == null)
            {
                return null;
            }

            return result.gameObject;
        }
        /// <summary>
        /// Finds a child by a path relative to the specified transform or absolute from root.
        /// </summary>
        /// <param name="transform">Start transform.</param>
        /// <param name="path">Path string (e.g. "Root/Player/Weapon" or "Player/Weapon").</param>
        /// <param name="separator">Separator character used between path parts.</param>
        /// <returns>Found transform, or null if not found.</returns>
        public static Transform FindByPath(this Transform transform, string path, char separator = '/')
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return transform.FindByPathTransform(path, separator);
        }

        /// <summary>
        /// Tries to find a child by a path relative to the specified game object or absolute from root.
        /// </summary>
        public static bool TryFindByPath(this GameObject gameObject, string path, out GameObject result, char separator = '/')
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            Transform found = gameObject.transform.FindByPathTransform(path, separator);

            if (found == null)
            {
                result = null;
                return false;
            }

            result = found.gameObject;
            return true;
        }
        /// <summary>
        /// Tries to find a child by a path relative to the specified transform or absolute from root.
        /// </summary>
        public static bool TryFindByPath(this Transform transform, string path, out Transform result, char separator = '/')
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            result = transform.FindByPathTransform(path, separator);
            return result != null;
        }

        /// <summary>
        /// Internal path traversal helper.
        /// </summary>
        /// <remarks>
        /// If the first non-empty path segment matches the hierarchy root name, the search starts from the root.
        /// Otherwise, the path is treated as relative to <paramref name="start"/>.
        /// Empty segments are ignored.
        /// </remarks>
        private static Transform FindByPathTransform(this Transform start, string path, char separator)
        {
            if (start == null)
            {
                throw new ArgumentNullException(nameof(start));
            }
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path must not be null or empty.", nameof(path));
            }

            string[] parts = path.Split(separator);

            int beginIndex = 0;

            while (beginIndex < parts.Length && string.IsNullOrEmpty(parts[beginIndex]))
            {
                beginIndex++;
            }

            if (beginIndex >= parts.Length)
            {
                return null;
            }

            Transform current = start;
            Transform root = start.root;

            if (parts[beginIndex] == root.name)
            {
                current = root;
                beginIndex++;
            }

            for (int i = beginIndex; i < parts.Length; i++)
            {
                string part = parts[i];

                if (string.IsNullOrEmpty(part))
                {
                    continue;
                }

                Transform next = null;
                int childCount = current.childCount;

                for (int c = 0; c < childCount; c++)
                {
                    Transform child = current.GetChild(c);

                    if (child.name == part)
                    {
                        next = child;
                        break;
                    }
                }

                if (next == null)
                {
                    return null;
                }

                current = next;
            }

            return current;
        }

        /// <summary>
        /// Returns the value of <see cref="GameObject.activeSelf"/>.
        /// </summary>
        public static bool IsActiveSelf(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            return gameObject.activeSelf;
        }
        /// <summary>
        /// Returns the value of <see cref="GameObject.activeInHierarchy"/>.
        /// </summary>
        public static bool IsActiveInHierarchy(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            return gameObject.activeInHierarchy;
        }

        /// <summary>
        /// Sets active state for the specified game object.
        /// </summary>
        /// <param name="gameObject">Target game object.</param>
        /// <param name="active">Desired active state.</param>
        /// <remarks>
        /// This is a thin wrapper over <see cref="GameObject.SetActive(bool)"/>.
        /// Extension methods are only used if an instance method is not applicable;
        /// in this case Unity's instance method is called.
        /// </remarks>
        public static void SetActive(this GameObject gameObject, bool active)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            gameObject.SetActive(active);
        }
        /// <summary>
        /// Sets active state for the specified game object and all its descendants.
        /// </summary>
        /// <param name="gameObject">Target game object.</param>
        /// <param name="active">Desired active state.</param>
        public static void SetActiveRecursively(this GameObject gameObject, bool active)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            gameObject.SetActive(active);

            Transform transform = gameObject.transform;
            int count = transform.childCount;

            for (int i = 0; i < count; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                child.SetActiveRecursively(active);
            }
        }

        /// <summary>
        /// Toggles <see cref="GameObject.activeSelf"/> for the specified game object.
        /// </summary>
        public static void ToggleActive(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            gameObject.SetActive(!gameObject.activeSelf);
        }

        /// <summary>
        /// Creates a new empty child <see cref="GameObject"/> under the specified parent.
        /// </summary>
        /// <param name="gameObject">Parent game object.</param>
        /// <param name="name">New child's name.</param>
        /// <param name="worldPositionStays">
        /// Passed to <see cref="Transform.SetParent(Transform, bool)"/>.
        /// If false, child's local transform is reset to identity (zero position, identity rotation, one scale).
        /// </param>
        /// <returns>The created child game object.</returns>
        public static GameObject CreateChild(this GameObject gameObject, string name, bool worldPositionStays = false)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name must not be null or empty.", nameof(name));
            }

            GameObject child = new GameObject(name);
            child.transform.SetParent(gameObject.transform, worldPositionStays);

            if (!worldPositionStays)
            {
                child.transform.localPosition = Vector3.zero;
                child.transform.localRotation = Quaternion.identity;
                child.transform.localScale = Vector3.one;
            }

            return child;
        }
        /// <summary>
        /// Creates a new empty child <see cref="GameObject"/> under the specified parent.
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="name">New child's name.</param>
        /// <param name="worldPositionStays">
        /// Passed to <see cref="Transform.SetParent(Transform, bool)"/>.
        /// If false, child's local transform is reset to identity (zero position, identity rotation, one scale).
        /// </param>
        /// <returns>The created child game object.</returns>
        public static GameObject CreateChild(this Transform transform, string name, bool worldPositionStays = false)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name must not be null or empty.", nameof(name));
            }

            GameObject child = new GameObject(name);
            child.transform.SetParent(transform, worldPositionStays);

            if (!worldPositionStays)
            {
                child.transform.localPosition = Vector3.zero;
                child.transform.localRotation = Quaternion.identity;
                child.transform.localScale = Vector3.one;
            }

            return child;
        }

        /// <summary>
        /// Detaches all direct children from the specified game object.
        /// </summary>
        public static void DetachChildren(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            gameObject.transform.DetachChildren();
        }
        /// <summary>
        /// Detaches all direct children from the specified transform.
        /// </summary>
        public static void DetachChildren(this Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            transform.DetachChildren();
        }

        /// <summary>
        /// Destroys all direct children of the specified game object.
        /// </summary>
        /// <param name="gameObject">Parent game object.</param>
        /// <param name="includeInactive">If false, children with <see cref="GameObject.activeSelf"/> == false are skipped.</param>
        /// <param name="immediate">If true, uses <see cref="UnityEngine.Object.DestroyImmediate(UnityEngine.Object)"/>.</param>
        public static void DestroyChildren(this GameObject gameObject, bool includeInactive = true, bool immediate = false)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            gameObject.transform.DestroyChildren(includeInactive, immediate);
        }
        /// <summary>
        /// Destroys all direct children of the specified transform.
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="includeInactive">If false, children with <see cref="GameObject.activeSelf"/> == false are skipped.</param>
        /// <param name="immediate">If true, uses <see cref="UnityEngine.Object.DestroyImmediate(UnityEngine.Object)"/>.</param>
        public static void DestroyChildren(this Transform transform, bool includeInactive = true, bool immediate = false)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Transform child = transform.GetChild(i);
                GameObject childObject = child.gameObject;

                if (!includeInactive && !childObject.activeSelf)
                {
                    continue;
                }

                if (immediate)
                {
                    UnityEngine.Object.DestroyImmediate(childObject);
                }
                else
                {
                    UnityEngine.Object.Destroy(childObject);
                }
            }
        }

        /// <summary>
        /// Recursively destroys all descendants of the specified transform (post-order).
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="includeInactive">If false, children with <see cref="GameObject.activeSelf"/> == false are skipped.</param>
        /// <param name="immediate">If true, uses <see cref="UnityEngine.Object.DestroyImmediate(UnityEngine.Object)"/>.</param>
        public static void DestroyChildrenRecursive(this Transform transform, bool includeInactive = true, bool immediate = false)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Transform child = transform.GetChild(i);
                GameObject childObject = child.gameObject;

                if (!includeInactive && !childObject.activeSelf)
                {
                    continue;
                }

                child.DestroyChildrenRecursive(includeInactive, immediate);

                if (immediate)
                {
                    UnityEngine.Object.DestroyImmediate(childObject);
                }
                else
                {
                    UnityEngine.Object.Destroy(childObject);
                }
            }
        }
        /// <summary>
        /// Recursively destroys all descendants of the specified game object.
        /// </summary>
        public static void DestroyChildrenRecursive(this GameObject gameObject, bool includeInactive = true, bool immediate = false)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }

            gameObject.transform.DestroyChildrenRecursive(includeInactive, immediate);
        }

        /// <summary>
        /// Ensures a direct child with the specified name exists; if not found, creates it.
        /// </summary>
        /// <param name="gameObject">Parent game object.</param>
        /// <param name="name">Child name to find or create.</param>
        /// <param name="worldPositionStays">Passed to the create operation.</param>
        /// <returns>Existing or newly created child.</returns>
        public static GameObject EnsureChild(this GameObject gameObject, string name, bool worldPositionStays = false)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name must not be null or empty.", nameof(name));
            }

            Transform parent = gameObject.transform;
            int count = parent.childCount;

            for (int i = 0; i < count; i++)
            {
                Transform child = parent.GetChild(i);

                if (child.name == name)
                {
                    return child.gameObject;
                }
            }

            return gameObject.CreateChild(name, worldPositionStays);
        }
        /// <summary>
        /// Ensures a direct child with the specified name exists; if not found, creates it.
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="name">Child name to find or create.</param>
        /// <param name="worldPositionStays">Passed to the create operation.</param>
        /// <returns>Existing or newly created child.</returns>
        public static GameObject EnsureChild(this Transform transform, string name, bool worldPositionStays = false)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name must not be null or empty.", nameof(name));
            }

            int count = transform.childCount;

            for (int i = 0; i < count; i++)
            {
                Transform child = transform.GetChild(i);

                if (child.name == name)
                {
                    return child.gameObject;
                }
            }

            return transform.CreateChild(name, worldPositionStays);
        }

        /// <summary>
        /// Tries to get an existing direct child by name without creating it.
        /// </summary>
        /// <param name="gameObject">Parent game object.</param>
        /// <param name="name">Child name to match exactly.</param>
        /// <param name="child">Output found child; otherwise null.</param>
        /// <returns>True if found; otherwise false.</returns>
        public static bool TryEnsureChild(this GameObject gameObject, string name, out GameObject child)
        {
            if (gameObject == null)
            {
                throw new ArgumentNullException(nameof(gameObject));
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name must not be null or empty.", nameof(name));
            }

            child = null;

            Transform parent = gameObject.transform;
            int count = parent.childCount;

            for (int i = 0; i < count; i++)
            {
                Transform c = parent.GetChild(i);

                if (c.name == name)
                {
                    child = c.gameObject;
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// Tries to get an existing direct child by name without creating it.
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="name">Child name to match exactly.</param>
        /// <param name="child">Output found child; otherwise null.</param>
        /// <returns>True if found; otherwise false.</returns>
        public static bool TryEnsureChild(this Transform transform, string name, out GameObject child)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name must not be null or empty.", nameof(name));
            }

            child = null;

            int count = transform.childCount;

            for (int i = 0; i < count; i++)
            {
                Transform c = transform.GetChild(i);

                if (c.name == name)
                {
                    child = c.gameObject;
                    return true;
                }
            }

            return false;
        }

    }
}
