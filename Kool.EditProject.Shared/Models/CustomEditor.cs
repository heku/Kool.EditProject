using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static Kool.EditProject.EditProjectPackage;

namespace Kool.EditProject.Models;

internal sealed class CustomEditor : IFileEditor
{
    private const int SW_RESTORE = 9;

    [DllImport("User32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);
    [DllImport("User32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    [DllImport("User32.dll")]
    private static extern bool IsIconic(IntPtr hWnd);

    private static readonly Dictionary<string, int> EditingFiles = new(StringComparer.OrdinalIgnoreCase);

    public void OpenFile(string file)
    {
        if (EditingFiles.TryGetValue(file, out var id))
        {
            var hWnd = Process.GetProcessById(id)?.MainWindowHandle ?? IntPtr.Zero;
            if (hWnd != IntPtr.Zero)
            {
                if (IsIconic(hWnd)) // Minimized
                {
                    ShowWindow(hWnd, SW_RESTORE);
                }
                else
                {
                    SetForegroundWindow(hWnd);
                }
            }
        }
        else
        {
            var process = new Process
            {
                EnableRaisingEvents = true,
                StartInfo = new ProcessStartInfo(Options.EditorExe, Options.EditorArg.Replace("$FILE", file))
            };
            process.Exited += (_, _) => EditingFiles.Remove(file);
            process.Start();
            EditingFiles[file] = process.Id; // MainWindowHandle maybe zero here, so log Id instead.
        }
    }
}