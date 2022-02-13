using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace Mod_the_Horror
{
    public class ItoWriter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="locationToSave"></param>
        /// <param name="name"></param>
        /// <param name="author"></param>
        /// <param name="contact"></param>
        /// <param name="strength"></param>
        /// <param name="dexterity"></param>
        /// <param name="perception"></param>
        /// <param name="charisma"></param>
        /// <param name="knowledge"></param>
        /// <param name="luck"></param>
        /// <param name="name_a"></param>
        /// <param name="menu_tag"></param>
        /// <param name="menu_desc"></param>
        /// <param name="perkpack_a"></param>
        /// <param name="perkpack_b"></param>
        /// <param name="fullName"></param>
        /// <param name="gender"></param>
        /// <param name="age"></param>
        /// <param name="charSpritesDirectoryName"></param>
        /// <param name="sprIconName"></param>
        /// <param name="sprBackName"></param>
        /// <param name="sprHouseName"></param>
        /// <param name="sprChibiName"></param>
        /// <param name="sprPortraitName"></param>
        /// <returns></returns>
        public static string WriteCustomCharacter(string locationToSave, string itoFileName, string name, string author, string contact, 
            int strength, int dexterity, int perception, int charisma, int knowledge, int luck, 
            string name_a, string menu_tag, string menu_desc, string perkpack_a, string perkpack_b,
            string fullName, string gender, int age, 
            string charSpritesDirectoryName, string sprIconName, string sprBackName, string sprHouseName,
            string sprChibiName, string sprPortraitName) {

            string directoryPath = locationToSave;
            string charSpritesDirectoryPath = charSpritesDirectoryName;

            string spriteIconPath = System.IO.Path.Combine(charSpritesDirectoryPath, $"{sprIconName}.png");
            string spriteBackPath = System.IO.Path.Combine(charSpritesDirectoryPath, $"{sprBackName}.png");
            string spriteHousePath = System.IO.Path.Combine(charSpritesDirectoryPath, $"{sprHouseName}.png");
            string spriteChibiPath = System.IO.Path.Combine(charSpritesDirectoryPath, $"{sprChibiName}.png");
            string spritePortraitPath = System.IO.Path.Combine(charSpritesDirectoryPath, $"{sprPortraitName}.png");

            string fileName = itoFileName;
            string pathName = System.IO.Path.Combine(directoryPath, fileName);

            //Write the .ito file containing the character information.
            string charInfo = "[character]" +
                $"\nname=\"{name}\"" +
                $"\nauthor=\"{author}\"" +
                $"\ncontact=\"{contact}\"" +
                $"\nstrength=\"{strength}\"" +
                $"\ndexterity=\"{dexterity}\"" +
                $"\nperception=\"{perception}\"" +
                $"\ncharisma=\"{charisma}\"" +
                $"\nknowledge=\"{knowledge}\"" +
                $"\nluck=\"{luck}\"" +
                $"\nsprite_icon=\"{spriteIconPath}\"" +
                $"\nsprite_back=\"{spriteBackPath}\"" +
                $"\nsprite_house=\"{spriteHousePath}\"" +
                $"\nsprite_chibi=\"{spriteChibiPath}\"" +
                $"\nportrait_a=\"{spritePortraitPath}\"" +
                $"\nname_a=\"{name_a}\"" +
                $"\nmenu_tag=\"{menu_tag}\"" +
                $"\nmenu_desc=\"Name: {fullName}#{age} / {gender} #{menu_desc}\"" +
                $"\nperkpack_a=\"{perkpack_a}\"" +
                $"\nperkpack_b=\"{perkpack_b}\"";

            //if (!System.IO.File.Exists(pathName)) {
            TextWriter sw = new StreamWriter(pathName);
            sw.WriteLine(charInfo);
            sw.Close();

            return directoryPath;
        }

        public static string ExtractInfo(string line) {
            int startIndex = line.IndexOf("\"") + 1;
            int lastIndex = line.LastIndexOf("\"");
            return line.Substring(startIndex, lastIndex - startIndex);
        }
    }
}
