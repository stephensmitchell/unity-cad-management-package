// using System.Diagnostics;

// public void RunCommand(string command, string arguments)
// {
//     Process process = new Process();
//     ProcessStartInfo startInfo = new ProcessStartInfo
//     {
//         FileName = command,
//         Arguments = arguments,
//         RedirectStandardOutput = true,
//         RedirectStandardError = true,
//         UseShellExecute = false,
//         CreateNoWindow = true
//     };

//     process.StartInfo = startInfo;
//     process.OutputDataReceived += (sender, args) => UnityEngine.Debug.Log(args.Data);
//     process.ErrorDataReceived += (sender, args) => UnityEngine.Debug.LogError(args.Data);
    
//     process.Start();
//     process.BeginOutputReadLine();
//     process.BeginErrorReadLine();
// }

// // Example usage:
// // RunCommand("git", "status");
// // RunCommand("python", "--version");

// using System.Diagnostics;
// using UnityEngine;

// public void LaunchApplication(string path)
// {
//     try
//     {
//         Process.Start(path);
//     }
//     catch (System.Exception ex)
//     {
//         // Log an error if the path is invalid or app fails to start
//         Debug.LogError($"Failed to launch application at '{path}'. Error: {ex.Message}");
//     }
// }

// // Example usage:
// // LaunchApplication("C:\\Program Files\\Blender Foundation\\Blender 4.1\\blender.exe");
// // LaunchApplication("notepad.exe");