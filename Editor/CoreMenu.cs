using UnityEditor;

namespace com.turbo.editor
{
    public class CoreMenu
    {
        //Menu Items...

#if TURBO
        [MenuItem("Turbo/Make Turbo")]
        private static void MT() => CoreEditorAPI.MakeTurbo();
#else
        [MenuItem("Turbo/Configure Project Settings")]
        private static void CPS() => CoreEditorAPI.ConfigureProjectSettings();
#endif
    }
}
