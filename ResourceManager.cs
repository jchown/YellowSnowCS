using System;
using System.Collections.Generic;
using System.Windows;

namespace YellowSnow
{
    static class ResourceManager
    {
        /// <summary>
        /// Switches a set of dictionaries in the application resource dictionary.
        /// </summary>
        /// <param name="oldDictionaries">Old dictionaries to remove, can be null if none to remove.</param>
        /// <param name="newDictionaries">New dictionaries to add, can be null if none to add.</param>
        public static void SwitchDictionaries(IEnumerable<ResourceDictionary> oldDictionaries, IEnumerable<ResourceDictionary> newDictionaries)
        {
            RemoveDictionaries(oldDictionaries);
            AddDictionaries(newDictionaries);
        }

        /// <summary>
        /// Removes dictionaries from the application resources
        /// </summary>
        /// <param name="oldDictionaries">
        /// Dictionaries to remove
        /// </param>
        private static void RemoveDictionaries(IEnumerable<ResourceDictionary> oldDictionaries)
        {
            if (oldDictionaries == null)
            {
                return;
            }

            foreach (var resourceDictionary in oldDictionaries)
            {
                Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
            }
        }

        /// <summary>
        /// Adds dictionaries to the application resources
        /// </summary>
        /// <param name="newDictionaries">
        /// The new dictionaries to add.
        /// </param>
        private static void AddDictionaries(IEnumerable<ResourceDictionary> newDictionaries)
        {
            if (newDictionaries == null)
            {
                return;
            }

            foreach (var resourceDictionary in newDictionaries)
            {
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            }
        }
    }
}
