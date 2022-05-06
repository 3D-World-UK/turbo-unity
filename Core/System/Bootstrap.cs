using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.Exceptions;

namespace com.turbo.core
{
    public class Bootstrap
    {
#if TURBO
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Init() => Execute();

        public static void Execute()
        {

            var op = Addressables.LoadAssetAsync<AppContext>("AppContext");
            op.Completed += HandleAppContext;

        }

        public static void HandleAppContext(AsyncOperationHandle<AppContext> op)
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Starting Application");
                GameObject ctx = new GameObject("App Context");
                ctx.AddComponent<AppContextContainer>();
                Object.DontDestroyOnLoad(ctx);
                ctx.GetComponent<AppContextContainer>().context = op.Result;
            }
            else
            {
                Debug.Log("Unable to start Turbo without AppContext. Please run 'Turbo -> Configure Project' from the main menu");
            }
            Addressables.Release(op);
        }
#endif
    }
}
