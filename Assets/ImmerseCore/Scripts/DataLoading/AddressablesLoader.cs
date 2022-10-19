using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Immerse.Core.Data
{
    public static class AddressablesLoader
    {
        private static readonly List<AsyncOperationHandle> _handles = new();
        
        public static async Task<T> LoadJson<T>(string path)
        {
            try
            {
                TextAsset textAsset = await LoadAsset<TextAsset>(path);
                T data = JsonConvert.DeserializeObject<T>(textAsset.text);
                return data;
            }
            catch (JsonSerializationException e)
            {
                Debug.LogError($"Can't deserialize json at line {e.LineNumber}:{e.LinePosition}: {e.Message}.");
                throw;
            }
            catch (AssetLoadingException e)
            {
                Debug.LogError($"Loading assets at path {path} failed: {e.Message}");
                throw;
            }
        }

        public static async Task<T> LoadAsset<T>(string path) where T : UnityEngine.Object
        {
            try
            {
                AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(path);
                
                await handle.Task;

                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    _handles.Add(handle);
                    return handle.Result;
                }

                throw handle.OperationException;
            }
            catch (AssetLoadingException e)
            {
                Debug.LogError($"Loading assets at path {path} failed: {e.Message}");
                throw;
            }
        }

        public static void Release()
        {
            foreach (AsyncOperationHandle handle in _handles)
            {
                Addressables.Release(handle);
            }

            _handles.Clear();
            Resources.UnloadUnusedAssets();
        }
    }
}