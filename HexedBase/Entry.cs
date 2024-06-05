using CoreRuntime.Interfaces;
using CoreRuntime.Manager;
using HexedBase.Modules;
using HexedBase.Patches;
using System.Collections;
using UnityEngine;

namespace HexedBase
{
    public class Entry : HexedCheat
    {
        public override void OnLoad(string[] args)
        {
            // Entry thats getting called by HexedLoader, this is alawys the startpoint of the cheat
            Console.WriteLine("Hexed Base Cheat successfully loaded!");

            // Specify our main function hooks to let the loader know about the games base functions, it takes any method that matches the original unity function struct
            MonoManager.PatchUpdate(typeof(VRCApplication).GetMethod(nameof(VRCApplication.Update))); // Update is needed to work with IEnumerators, hooking it will enable the CoroutineManager
            MonoManager.PatchOnApplicationQuit(typeof(VRCApplicationSetup).GetMethod(nameof(VRCApplicationSetup.OnApplicationQuit))); // Optional Hook to enable the OnApplicationQuit callback

            // Apply our custom Hooked functions
            UnityEngineHWIDPatch.ApplyPatch();
            OnNetworkReadyPatch.ApplyPatch();

            // Start a delayed function
            CoroutineManager.RunCoroutine(PrintLateHello());
        }

        public override void OnApplicationQuit()
        {
            // Function is hooked, so its getting called in our callback
            Console.WriteLine("Game Closed! Bye!");
        }

        public override void OnUpdate()
        {
            // Methods that need to run every frame, added a simple hotkey zoom module
            KeybindManager.Update();
        }

        public override void OnFixedUpdate()
        {
            // Function is not hooked, won't get called
        }

        public override void OnLateUpdate()
        {
            // Function is not hooked, won't get called
        }

        public override void OnGUI()
        {
            // Function is not hooked, won't get called
        }

        private static IEnumerator PrintLateHello()
        {
            // Example on calling a simple print after a 5 second delay
            yield return new WaitForSeconds(5);

            Console.WriteLine("Hello from a delayed function!");
        }
    }
}
