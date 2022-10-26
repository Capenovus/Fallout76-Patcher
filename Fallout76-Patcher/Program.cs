using System.Diagnostics;

namespace Fallout76_Patcher
{
    internal class Program
    {
        static void Main()
        {
            if (!File.Exists("SeventySix.esm") || !File.Exists("fcrc32.bat") || !File.Exists("fcrc32.py")) return;

            foreach (var patch in Patch.Patches)
            {
                int[] positions = File.ReadAllBytes("SeventySix.esm").Locate(patch.Item2);

                Console.WriteLine($"Patching {patch.Item1}...");

                foreach (int position in positions)
                {
                    if (patch.Item2.Length != patch.Item3.Length) break;
                    using FileStream fs = new("SeventySix.esm", FileMode.Open);
                    fs.Seek(position, SeekOrigin.Begin);
                    fs.Write(patch.Item3, 0, patch.Item3.Length);
                    fs.Dispose();
                    fs.Close();
                }
            }

            Process fcrc32 = new();
            fcrc32.StartInfo.FileName = "fcrc32.bat";
            fcrc32.StartInfo.UseShellExecute = false;
            fcrc32.Start();
            fcrc32.WaitForExit();

            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}