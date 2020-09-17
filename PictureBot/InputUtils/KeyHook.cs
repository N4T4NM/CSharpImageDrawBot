using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PictureBot.Utils
{
    public class KeyHook
    {
        private static LowLevelKeyboardProc _proc = HookCallback;
        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static IntPtr HookID;

        public delegate void KeyHookEvent(Keys key);
        public static event KeyHookEvent onKeyHooked;

        public static void AddHookToCurrentProcess()
        {
            HookID = SetHook(_proc);
        }

        public static bool RemoveHookFromCurrentProcess()
        {
            return UnhookWindowsHookEx(HookID);
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)0x0100)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                var key = (Keys)vkCode;
                try
                {
                    onKeyHooked(key);
                }
                catch (Exception ex) { }
            }

            return CallNextHookEx(HookID, nCode, wParam, lParam);
        }

        static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process currentProcess = Process.GetCurrentProcess())
            using (ProcessModule cProcessModule = currentProcess.MainModule)
            {
                return SetWindowsHookEx(13, proc, GetModuleHandle(cProcessModule.ModuleName), 0);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
