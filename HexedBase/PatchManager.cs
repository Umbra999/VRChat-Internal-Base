using CoreRuntime.Manager;
using System;
using VRC.SDKBase;

namespace HexedBase
{
    internal class PatchManager
    {
        private delegate void _OnNetworkReadyDelegate(IntPtr instance);
        private static _OnNetworkReadyDelegate originalMethod;

        // Create a hook using the HookManager, if you need to know more about hooking read it up on the internet since its a complex task i wont explain here so much
        public static void ApplyPatch()
        {
            originalMethod = HookManager.Detour<_OnNetworkReadyDelegate>(typeof(VRC.Player).GetMethod(nameof(VRC.Player.OnNetworkReady)), Patch);
        }

        private static void Patch(IntPtr instance)
        {
            originalMethod(instance);

            VRC.Player player = instance == IntPtr.Zero ? null : new VRC.Player(instance);

            if (player == null) return;

            Console.WriteLine(player.prop_String_0 + " is ready");
        }
    }
}
