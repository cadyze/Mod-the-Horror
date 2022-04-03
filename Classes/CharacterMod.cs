using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mod_the_Horror.Classes
{
    public class CharacterMod
    {
        public string name = "";
        public string author = "";
        public string contact = "";
        public int strength = 0;
        public int dexterity = 0;
        public int perception = 0;
        public int charisma = 0;
        public int knowledge = 0;
        public int luck = 0;
        public string sprite_icon = "";
        public string sprite_back = "";
        public string sprite_house = "";
        public string sprite_chibi = "";
        public string portrait_a = "";
        public string name_a = "";
        public string portrait_b = "";
        public string name_b = "";
        public string occupation = "";
        public string japanese_script = "";
        public string full_name = "";
        public int age;
        public string sex = "";
        public string description = "";
        public string perkpack_a = "";
        public string perkpack_b = "";

        public CharacterMod() { 
        
        }
        public CharacterMod(string[] itoInformation)
        {
            foreach (string line in itoInformation)
            {
                if (line.Length > 5 && line.Substring(0, 5).Equals("name=")) name = ItoWriter.ExtractInfo(line);
                if (line.Contains("author=")) author = ItoWriter.ExtractInfo(line);
                if (line.Contains("contact=")) contact = ItoWriter.ExtractInfo(line);
                if (line.Contains("strength=")) strength = int.Parse(ItoWriter.ExtractInfo(line));
                if (line.Contains("dexterity=")) dexterity = int.Parse(ItoWriter.ExtractInfo(line));
                if (line.Contains("perception=")) perception = int.Parse(ItoWriter.ExtractInfo(line));
                if (line.Contains("charisma=")) charisma = int.Parse(ItoWriter.ExtractInfo(line));
                if (line.Contains("knowledge=")) knowledge = int.Parse(ItoWriter.ExtractInfo(line));
                if (line.Contains("luck=")) luck = int.Parse(ItoWriter.ExtractInfo(line));
                if (line.Contains("sprite_icon=")) sprite_icon = ItoWriter.ExtractInfo(line);
                if (line.Contains("sprite_back=")) sprite_back = ItoWriter.ExtractInfo(line);
                if (line.Contains("sprite_house=")) sprite_house = ItoWriter.ExtractInfo(line);
                if (line.Contains("sprite_chibi=")) sprite_chibi = ItoWriter.ExtractInfo(line);
                if (line.Contains("portrait_a=")) portrait_a = ItoWriter.ExtractInfo(line);
                if (line.Contains("portrait_b=")) portrait_b = ItoWriter.ExtractInfo(line);
                if (line.Contains("name_a=")) name_a = ItoWriter.ExtractInfo(line);
                if (line.Contains("name_b=")) name_b = ItoWriter.ExtractInfo(line);
                if (line.Contains("perkpack_a=")) perkpack_a = ItoWriter.ExtractInfo(line);
                if (line.Contains("perkpack_b=")) perkpack_b = ItoWriter.ExtractInfo(line);

                if (line.Contains("menu_tag="))
                {
                    string tagInfo = ItoWriter.ExtractInfo(line);
                    try
                    {
                        int startPos = tagInfo.IndexOf("--") + 2;
                        int endPos = tagInfo.LastIndexOf("--");
                        tagInfo = tagInfo.Substring(startPos, endPos - startPos);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        tagInfo = "";
                    }
                    occupation = tagInfo;

                }
                if (line.Contains("menu_desc="))
                {
                    //Menu description format: menu_desc="NAME#AGE / GENDER # #DESCRIPTION"
                    string descInfo = ItoWriter.ExtractInfo(line);
                    try
                    {
                        int endPos = descInfo.IndexOf("#");
                        string charName = descInfo.Substring(0, endPos);
                        descInfo = descInfo.Substring(descInfo.IndexOf("#") + 1);

                        endPos = descInfo.IndexOf("/");
                        string charAge = descInfo.Substring(0, endPos - 1);
                        descInfo = descInfo.Substring(endPos + 2);

                        endPos = descInfo.IndexOf("#");
                        string charGender = descInfo.Substring(0, endPos);
                        descInfo = descInfo.Substring(descInfo.IndexOf("#", 1) + 1);
                        descInfo = descInfo.Replace('#', '\n');

                        description = descInfo;
                        age = int.Parse(charAge);
                        sex = charGender;
                        full_name = charName;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        description = "";
                        age = 0;
                        sex = "";
                        full_name = "";
                    }
                }
            }

        }
    }
}
