using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nidavellir.Scriptables.Audio;
using UnityEditor;
using UnityEngine;

namespace Nidavellir.Editor
{
    public class AudioDataCreator : MonoBehaviour
    {
        [MenuItem("Assets/Create/Data/Audio/Create SfxData")]
        public static void CreateSfxData()
        {
            foreach (var selectedAudioClip in Selection.objects)
            {
                CreateSfxDataFromAudioClip(selectedAudioClip);
            }
        }
        
        [MenuItem("Assets/Create/Data/Audio/Create MusicData")]
        public static void CreateMusicData()
        {
            foreach (var selectedAudioClip in Selection.objects)
            {
                CreateMusicDataFromAudioClip(selectedAudioClip);
            }
        }

        [MenuItem("Assets/Create/Data/Audio/Create SfxData", true)]
        [MenuItem("Assets/Create/Data/Audio/Create MusicData", true)]
        public static bool CreateSfxDataValidation()
        {
            return Selection.objects.All(o => o is AudioClip);
        }

        private static void CreateSfxDataFromAudioClip(Object audioClip)
        {
            var directoryPath = AssetCreationUtils.EnsureDirectoryExists(audioClip, "Sfx");
            var sfxDataAssetPath = $"{directoryPath}/{audioClip.name}.asset";

            if (File.Exists(sfxDataAssetPath))
                return;
            
            var sfxData = ScriptableObject.CreateInstance<SfxData>();
            sfxData.Init(audioClip as AudioClip);
            CreateAsset(sfxData, sfxDataAssetPath);
        }
        
        private static void CreateMusicDataFromAudioClip(Object audioClip)
        {
            var directoryPath = AssetCreationUtils.EnsureDirectoryExists(audioClip, "Music");
            var musicDataAssetPath = $"{directoryPath}/{audioClip.name}.asset";

            if (File.Exists(musicDataAssetPath))
                return;
            
            var musicData = ScriptableObject.CreateInstance<MusicData>();
            musicData.Init(audioClip as AudioClip);
            CreateAsset(musicData, musicDataAssetPath);
        }
        
        private static void CreateAsset(ScriptableObject so, string path)
        {
            AssetDatabase.CreateAsset(so, path);
            AssetDatabase.SaveAssets();
            EditorGUIUtility.PingObject(so);
        }
    }
}