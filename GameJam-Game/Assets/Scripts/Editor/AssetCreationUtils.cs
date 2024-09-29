using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Nidavellir.Editor
{
    public static class AssetCreationUtils
    {
        private static IReadOnlyList<string> s_baseDataDirectories = new List<string>
        {
            "Assets",
            "Data",
            "Audio"
        };
        
        public static string EnsureDirectoryExists(Object asset, string destinationDirectory)
        {
            var subDirectories = GetAssetSubDirectories(asset, destinationDirectory);
            var dataDirectories = new List<string>(s_baseDataDirectories);
            dataDirectories.Add(destinationDirectory);
            dataDirectories.AddRange(subDirectories);
            var directoryPath = Path.Combine(dataDirectories.ToArray());

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            return directoryPath;
        }
        
        private static List<string> GetAssetSubDirectories(Object asset, string destinationDirectory)
        {
            var path = AssetDatabase.GetAssetPath(asset);
            path = Path.GetDirectoryName(path);
            
            var directories = path.Split(Path.DirectorySeparatorChar);
            var subDirectories = new List<string>();
            for (var i = directories.Length - 1; i >= 0; i--)
            {
                var currentDir = directories[i];
                if (currentDir.Equals(destinationDirectory) || s_baseDataDirectories.Contains(currentDir))
                    return subDirectories;

                subDirectories.Add(currentDir);
            }

            return subDirectories;
        }
    }
}