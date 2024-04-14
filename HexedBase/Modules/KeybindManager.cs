using UnityEngine;

namespace HexedBase.Modules
{
    internal class KeybindManager
    {
        private static float ogFOV = 60;
        private static bool isCameraZoomed = false;
        public static void Update()
        {
            if (Input.GetKeyInt(KeyCode.LeftAlt) && !isCameraZoomed && !Input.GetKeyInt(KeyCode.Tab))
            {
                ogFOV = Camera.main.fieldOfView;
                isCameraZoomed = true;
                Camera.main.fieldOfView = 10;
            }
            else if (Input.GetKeyUpInt(KeyCode.LeftAlt) && isCameraZoomed || !Application.isFocused)
            {
                isCameraZoomed = false;
                Camera.main.fieldOfView = ogFOV;
            }
        }
    }
}
