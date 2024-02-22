using CoreRuntime.Manager;
using System;
using VRC.Economy;
using VRC.SDKBase;

namespace HexedBase
{
    internal class PatchManager
    {
        private delegate bool _CreateCellDelegate(IntPtr __0, IntPtr __1);
        private static _CreateCellDelegate originalMethod;

        public static void ApplyStorePatch()
        {
            // Create a hook using the HookManager, if you need to know more about hooking read it up on the internet since its a complex task i wont explain here so much
            // We create a Hook inside the Store class on the DoesPlayerOwnProduct method which returns a boolean which is a c# default and takes 2 arguments which are not c# default so we specify them as IntPtr and cast them in our hook
            originalMethod = HookManager.Detour<_CreateCellDelegate>(typeof(Store).GetMethod(nameof(Store.DoesPlayerOwnProduct)), Patch);
        }

        private static bool Patch(IntPtr __0, IntPtr __1)
        {
            // Cast our Pointer to a valid VRCPlayerApi like its orginally used
            VRCPlayerApi Player = __0 == IntPtr.Zero ? null : new VRCPlayerApi(__0);

            // If the player belongs to us we return true to tell the game we own the requested product from the store
            if (Player != null && Player.isLocal) return true;

            // If it ends up here the player is not our player so we just return the original method
            return originalMethod(__0, __1);
        }
    }
}
