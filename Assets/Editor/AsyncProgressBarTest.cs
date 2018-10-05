using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class AsyncProgressBarTest {
	
        [MenuItem("Test/Show")]
        static void Show()
        {
            AsyncProgressBar.Display("Background tasks (1/2)", Random.Range(0f, 1f));
        }

        [MenuItem("Test/Hide")]
        static void Hide()
        {
            AsyncProgressBar.Clear();
        }
    }

}
