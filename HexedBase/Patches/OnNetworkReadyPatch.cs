using CoreRuntime.Manager;

namespace HexedBase.Patches
{
    internal class OnNetworkReadyPatch
    {
        private delegate void _OnNetworkReadyDelegate(IntPtr instance);
        private static _OnNetworkReadyDelegate originalMethod;

        // Create a hook using the HookManager, if you need to know more about hooking read it up on the internet since its a complex task i wont explain here
        public static void ApplyPatch()
        {
            originalMethod = HookManager.Detour<_OnNetworkReadyDelegate>(typeof(VRCPlayer).GetMethod(nameof(VRCPlayer.Awake)), Patch);
        }

        private static void Patch(IntPtr instance)
        {
            // Call the original method as prefix so we can call our method after, alternative way is to call the og method as postfix to edit data before
            originalMethod(instance);

            // Cast our Pointer to a valid Player like its orginally used
            VRCPlayer player = instance == IntPtr.Zero ? null : new VRCPlayer(instance);

            if (player == null) return;

            // Console Message if a Console is attached or redirected
            Console.WriteLine(player.prop_String_0 + " is ready");
        }
    }
}
