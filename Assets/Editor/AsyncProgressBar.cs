using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Editor
{
    [InitializeOnLoad]
    public static class AsyncProgressBar
    {
        private static Type tyAsyncProgressBar;
        private static PropertyInfo piProgress;
        private static PropertyInfo piProgressInfo;
        private static PropertyInfo piIsShowing;
        private static MethodInfo miDisplay;
        private static MethodInfo miClear;
 
        //Zero Width Space
        private static readonly string TitlePrefix = char.ConvertFromUtf32(0x0000200B);

        static AsyncProgressBar()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            if ((tyAsyncProgressBar = assembly.GetType("UnityEditor.AsyncProgressBar")) == null)
                return;

            piProgress = tyAsyncProgressBar.GetProperty("progress");
            piProgressInfo = tyAsyncProgressBar.GetProperty("progressInfo");
            piIsShowing = tyAsyncProgressBar.GetProperty("isShowing");
            miDisplay = tyAsyncProgressBar.GetMethod("Display");
            miClear = tyAsyncProgressBar.GetMethod("Clear");

//            Clear();
        }

        public static float progress => (float) piProgress.GetValue(null, null);

        public static string progressInfo => (string) piProgressInfo.GetValue(null, null);

        public static bool isShowing => (bool) piIsShowing.GetValue(null, null);

        public static void Display(string message, float newProgress)
        {
            if (isShowing && !progressInfo.StartsWith(TitlePrefix))
                return;
            
            message = TitlePrefix + message.Substring(0, Mathf.Min(30, message.Length));

            miDisplay.Invoke(null, new object[] {message, newProgress});
        }

        public static void Clear()
        {
            if (!isShowing || !progressInfo.StartsWith(TitlePrefix))
                return;

            miClear.Invoke(null, new object[0]);
        }
    }
} 