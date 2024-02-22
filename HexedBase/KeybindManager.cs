using UnityEngine;

namespace HexedBase
{
    internal class KeybindManager
    {
        private static bool isCameraZoomed = false;
        public static void Update()
        {
            if (Input.GetKeyInt(KeyCode.LeftAlt) && !isCameraZoomed && !Input.GetKeyInt(KeyCode.Tab))
            {
                isCameraZoomed = true;
                Camera.main.fieldOfView = 10;
            }
            else if (Input.GetKeyUpInt(KeyCode.LeftAlt) && isCameraZoomed || !Application.isFocused)
            {
                isCameraZoomed = false;
                Camera.main.fieldOfView = 60;
            }
        }
    }
}
