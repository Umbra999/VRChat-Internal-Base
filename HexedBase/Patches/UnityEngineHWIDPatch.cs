using CoreRuntime.Manager;
using Il2CppInterop.Runtime;
using UnityEngine;
using VRC.Core;

namespace HexedBase.Patches
{
    internal class UnityEngineHWIDPatch
    {
        private delegate IntPtr _deviceUniqueIdentifierDelegate();

        public static void ApplyPatch()
        {
            OriginalLenght = SystemInfo.deviceUniqueIdentifier.Length;
            if (OriginalLenght == 0)
            {
                Console.WriteLine("Failed to get HWID lenght");
                return;
            }

            if (APIUser.CurrentUser != null)
            {
                Console.WriteLine($"Already logged in, skipping HWID Spoof");
                return;
            }

            IntPtr mainmethod = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetDeviceUniqueIdentifier");

            HookManager.Detour<_deviceUniqueIdentifierDelegate>(mainmethod, Patch);
        }

        private static IntPtr Patch()
        {
            if (FakeHWID == null)
            {
                FakeHWID = new(IL2CPP.ManagedStringToIl2Cpp(GenerateHWID()));
                Console.WriteLine($"New HWID: {FakeHWID.ToString()}");
            }

            return FakeHWID.Pointer;
        }

        private static Il2CppSystem.Object FakeHWID;
        private static int OriginalLenght = 0;
        private static readonly System.Random random = new(Environment.TickCount);

        private static string GenerateHWID()
        {
            byte[] bytes = new byte[OriginalLenght / 2];
            random.NextBytes(bytes);
            return string.Join("", bytes.Select(it => it.ToString("x2")));
        }
    }
}
