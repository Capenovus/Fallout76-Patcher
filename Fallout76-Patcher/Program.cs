﻿using System.Diagnostics;
using System.Drawing;

namespace Fallout76_Patcher
{
    internal class Program
    {
        static void Main()
        {
            if (!File.Exists("SeventySix.esm") || !File.Exists("fcrc32.bat") || !File.Exists("fcrc32.py")) 
            {
                Console.WriteLine("[ERROR] ".Pastel(Colors.INFO) + "Oopsie WHoopsie an ewwow occuwwed, pwease read the instructions on Github first ^w^");
                Console.Write("Pwess any kwey to contiwue...");
                Console.ReadKey();
                return;
            }

            List<Tuple<string, List<Tuple<byte[], byte[]>>, bool>> organizedPatches = new();

            Patch.Patches.ForEach(x => {
                if (organizedPatches.Where(z => z.Item1.Equals(x.Item1)).Select(z => z).ToList().Count > 0)
                    organizedPatches.Where(z => z.Item1.Equals(x.Item1)).Select(z => z).First().Item2.Add(new(x.Item2, x.Item3));
                else organizedPatches.Add(new(x.Item1, new() { new(x.Item2, x.Item3) }, x.Item4));
            });

            while(true)
            {
                Console.Clear();
                for (int i = 0; i < organizedPatches.Count; i++)
                {
                    string line = $"({i}) {organizedPatches[i].Item1}";
                    Console.WriteLine(line);
                    if (i == organizedPatches.Count - 1)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"({i + 1}) Author's Favorites");
                        Console.WriteLine($"({i + 2}) All of them lol");
                        Console.WriteLine($"({i + 3}) Exit");
                    };
                }

                Console.Write("\nSelection: ");
                string? selection = Console.ReadLine();
                if (!int.TryParse(selection, out int sel)) continue;
                if (sel > organizedPatches.Count + 2 || sel < 0) continue;
                if (sel == organizedPatches.Count + 2) break;
                else if (sel == organizedPatches.Count + 1) organizedPatches.Patch();
                else if (sel == organizedPatches.Count) organizedPatches.Where(z => z.Item3).Select(z => z).ToList().Patch();
                else organizedPatches.Where(z => z.Item1.Equals(organizedPatches[sel].Item1)).Select(z => z).ToList().Patch();

                Console.WriteLine("\nFinished Writing Patches".Pastel(Colors.SUCCESS));
                Console.Write("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }

    public static class Patcher
    {
        public static void Patch(this List<Tuple<string, List<Tuple<byte[], byte[]>>, bool>> patches)
        {
            try
            {
                if (!File.Exists("SeventySix.esm.original")) File.Copy("SeventySix.esm", "SeventySix.esm.original");
                byte[] contents = File.ReadAllBytes("SeventySix.esm");
                string title = Console.Title;
                foreach (var patchlist in patches)
                {
                    Console.WriteLine("[STATUS] ".Pastel(Colors.STATUS) + $"Patching {patchlist.Item1}");

                    bool missing = false;
                    foreach (var patch in patchlist.Item2)
                    {
                        Console.Title = $"Patching {patchlist.Item1} ({patchlist.Item2.IndexOf(patch)}/{patchlist.Item2.Count})";

                        long[] positions = contents.Locate(patch.Item1).ToArray();
                        if (positions.Length == 0) missing = true;
                        foreach (int position in positions)
                        {
                            if (patch.Item1.Length != patch.Item2.Length) break;
                            using FileStream fs = new("SeventySix.esm", FileMode.Open);
                            fs.Seek(position, SeekOrigin.Begin);
                            fs.Write(patch.Item2, 0, patch.Item2.Length);
                            fs.Dispose();
                            fs.Close();
                        }
                    }

                    if (missing)
                        Console.WriteLine("[WARNING] ".Pastel(Colors.WARNING) + "Not all patches could be applied");
                    else Console.WriteLine("[INFO] ".Pastel(Colors.INFO) + $"{patchlist.Item1} patched");
                }
                Console.Title = title;
                Console.WriteLine("[STATUS] ".Pastel(Colors.STATUS) + "Replacing CRC Sig");
                if (!CRCPatch()) Console.WriteLine("[ERROR] ".Pastel(Colors.ERROR) + "Failed to patch CRC Sig");
                Console.WriteLine("[INFO] ".Pastel(Colors.INFO) + "DONE");
            }
            catch (Exception e)
            {
                Console.WriteLine("[ERROR] ".Pastel(Colors.ERROR) + $"{e.Message}");
            }
        }

        private static bool CRCPatch()
        {

            Process fcrc32 = new();
            fcrc32.StartInfo.FileName = "fcrc32.bat";
            fcrc32.StartInfo.UseShellExecute = false;
            fcrc32.StartInfo.RedirectStandardOutput = true;
            fcrc32.Start();
            fcrc32.WaitForExit();
            string output = fcrc32.StandardOutput.ReadToEnd();

            if (!output.Contains("New CRC-32 successfully verified")) 
            {
                return false;
            }

            return true;
        }
    }

    public static class Colors
    {
        public readonly static Color INFO = Color.FromArgb(10, 196, 209);
        public readonly static Color STATUS = Color.FromArgb(198, 41, 255);
        public readonly static Color SUCCESS = Color.FromArgb(0, 252, 50);
        public readonly static Color WARNING = Color.FromArgb(226, 232, 104);
        public readonly static Color ERROR = Color.FromArgb(255, 21, 0);
    }
}