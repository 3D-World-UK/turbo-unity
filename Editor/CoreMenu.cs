using UnityEditor;

namespace com.turbo.editor
{
    public class CoreMenu
    {
        //Menu Items...

        [MenuItem("Turbo/Configure Project")]
        private static void CP() => CoreEditorAPI.ConfigureProject();
    }
}
