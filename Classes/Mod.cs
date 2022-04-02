using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mod_the_Horror.Classes
{
    public class Mod

    {
        public string pathToPreview = "";
        public string previewName = "";
        public string previewDescription = "";
        public string pathToMod;
        public ModType modType;

        public Mod(ModType modType, string preview, string name, string description, string path) {
            pathToMod = path;
            pathToPreview = preview;
            previewDescription = description;
            previewName = name;
            this.modType = modType;
        }
        public Mod(string path, ModType modType)
        {
            pathToMod = path;
            this.modType = modType;
            string[] lines = System.IO.File.ReadAllLines(path);
            switch (modType) {
                case ModType.CHARACTER:
                    foreach (string line in lines) {
                        if (line.Length > 5 && line.Substring(0, 5).Equals("name=")) previewName = $"[CHARACTER] {ItoWriter.ExtractInfo(line)}";
                        if (line.Contains("menu_desc=")) previewDescription = ItoWriter.ExtractInfo(line);
                        if (line.Contains("sprite_house=")) pathToPreview = ItoWriter.ExtractInfo(line);
                    }
                    break;
                case ModType.ENEMY:
                    foreach (string line in lines)
                    {
                        if (line.Length > 5 && line.Substring(0, 5).Equals("name=")) previewName = $"[ENEMY] {ItoWriter.ExtractInfo(line)}";
                        if (line.Contains("subtitle=")) previewDescription = ItoWriter.ExtractInfo(line);
                        if (line.Contains("art01=")) pathToPreview = ItoWriter.ExtractInfo(line);
                    }
                    break;
                case ModType.EVENT:
                    foreach (string line in lines)
                    {
                        if (line.Length > 5 && line.Substring(0, 5).Equals("name=")) previewName = $"[EVENT] {ItoWriter.ExtractInfo(line)}";
                        if (line.Contains("about=")) previewDescription = ItoWriter.ExtractInfo(line);
                        if (line.Contains("image=")) pathToPreview = ItoWriter.ExtractInfo(line);
                    }
                    break;
                case ModType.MYSTERY:
                    foreach (string line in lines)
                    {
                        if (line.Length > 5 && line.Substring(0, 5).Equals("name=")) previewName = $"[MYSTERY] {ItoWriter.ExtractInfo(line)}";
                        if (line.Contains("description=")) previewDescription = ItoWriter.ExtractInfo(line);
                        if (line.Contains("background=")) pathToPreview = ItoWriter.ExtractInfo(line);
                    }
                    break;
                default:
                    pathToPreview = "";
                    previewName = "";
                    previewDescription = "";
                    break;
            }
        }




        public string PreviewPath { 
            get { return pathToPreview; }
            set {
                pathToPreview = value;
            }
        }
        public string ModName
        {
            get { return previewName; }
            set
            {
                previewName = value;
            }
        }
    }
}
