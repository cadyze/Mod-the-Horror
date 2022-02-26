using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

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
            string charSpritesDirectoryName, string? sprIconName, string? sprBackName, string? sprHouseName,
            string? sprChibiName, string? sprPortraitName, string? sprPortraitBName) {

            string directoryPath = locationToSave;
            string charSpritesDirectoryPath = charSpritesDirectoryName;
            string spriteIconPath = (sprIconName != null && !sprIconName.Equals("")) ? System.IO.Path.Combine(charSpritesDirectoryPath, $"{sprIconName}") : "";
            string spriteBackPath = (sprBackName != null && !sprBackName.Equals("")) ? System.IO.Path.Combine(charSpritesDirectoryPath, $"{sprBackName}") : "";
            string spriteHousePath = (sprHouseName != null && !sprHouseName.Equals("")) ? System.IO.Path.Combine(charSpritesDirectoryPath, $"{sprHouseName}") : "";
            string spriteChibiPath = (sprChibiName != null && !sprChibiName.Equals("")) ? System.IO.Path.Combine(charSpritesDirectoryPath, $"{sprChibiName}") : "";
            string spritePortraitPath = (sprPortraitName != null && !sprPortraitName.Equals("")) ?  System.IO.Path.Combine(charSpritesDirectoryPath, $"{sprPortraitName}") : "";
            string spritePortraitBPath = (sprPortraitBName != null && !sprPortraitBName.Equals("")) ? System.IO.Path.Combine(charSpritesDirectoryPath, $"{sprPortraitBName}") : "";

            string fileName = itoFileName;
            string pathName = System.IO.Path.Combine(directoryPath, fileName);
            //Trace.WriteLine(pathName);

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
                $"\nportrait_a=\"{spritePortraitBPath}\"" +
                $"\nname_a=\"{name_a}\"" +
                $"\nmenu_tag=\"--{menu_tag}--\"" +
                $"\nmenu_desc=\"{fullName}#{age} / {gender}# #{menu_desc}\"" +
                $"\nperkpack_a=\"{perkpack_a}\"" +
                $"\nperkpack_b=\"{perkpack_b}\"";

            TextWriter tw = new StreamWriter(pathName);
            tw.WriteLine(charInfo);
            tw.Close();

            return directoryPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locationToSave"></param>
        /// <param name="itoFileName">Should already include the .ito extension</param>
        /// <param name="name"></param>
        /// <param name="author"></param>
        /// <param name="evnLocation"></param>
        /// <param name="contact"></param>
        /// <param name="flavor"></param>
        /// <param name="numOptions"></param>
        /// <param name="evnSpriteDirectoryName"></param>
        /// <param name="evnSpriteName"></param>
        /// <param name="desc"></param>
        /// <param name="eventOptions"></param>
        public static void WriteEvent(string locationToSave, string itoFileName, string name, string author, string evnLocation, string contact,
            string flavor, int numOptions, string evnSpriteDirectoryName, string? evnSpriteName, string desc, List<EventOption> eventOptions) {
            
            string spritePath = (evnSpriteName != null && !evnSpriteName.Equals("")) ? System.IO.Path.Combine(evnSpriteDirectoryName, evnSpriteName) : "";
            string eventInfo = "[event]" +
                $"\nname=\"{name}\"" +
                $"\nlocation=\"{evnLocation}\"" +
                $"\nauthor=\"{author}\"" +
                $"\ncontact=\"{contact}\"" +
                $"\nflavor=\"{flavor}\"" +
                $"\noptions=\"{numOptions}\"" +
                $"\nimage=\"{spritePath}\"" +
                $"\nabout=\"{desc}\"";

            switch (numOptions) {
                case 1:
                    EventOption optionA = eventOptions[0];
                    eventInfo = eventInfo + ConvertOptionToIto(optionA, 'a');
                    break;
                case 2:
                    EventOption optionB = eventOptions[1];
                    eventInfo = eventInfo + ConvertOptionToIto(optionB, 'b');
                    goto case 1;
                case 3:
                    EventOption optionC = eventOptions[2];
                    eventInfo = eventInfo + ConvertOptionToIto(optionC, 'c');
                    goto case 2;
            }

            TextWriter tw = new StreamWriter(System.IO.Path.Combine(locationToSave, itoFileName));
            tw.WriteLine(eventInfo);
            tw.Close();
        }

        public static void WriteEnemy(string locationToSave, string itoFileName, string name, string subtitle,
            string type, string location, string author, bool canRun, string intro, int health, int power,
            string dmgType, int dmgValue, string hitMsg1, string hitMsg2, string hitMsg3, int experience,
            string prizeType, string prizeName, int frameFrequency, string spriteDirectoryName, string? frame1Name, string? frame2Name) {

            string frame1Path = (frame1Name != null && !frame1Name.Equals("")) ? System.IO.Path.Combine(spriteDirectoryName, frame1Name) : "";
            string frame2Path = (frame2Name != null && !frame2Name.Equals("")) ? System.IO.Path.Combine(spriteDirectoryName, frame2Name) : "";
            int runValue = canRun ? 1 : 0;
            string enemyInfo = "[enemy]\n" +
                $"name=\"{name}\"\n" +
                $"subtitle=\"{subtitle}\"\n" +
                $"type=\"{type}\"\n" +
                $"location=\"{location}\"\n" +
                $"author=\"{author}\"\n\n" +
                $"intro=\"{intro}\"\n" +
                $"can_run=\"{runValue}\"\n" +
                $"health=\"{health}\"\n" +
                $"power=\"{power}\"\n" +
                $"damagevalue=\"{dmgValue}\"\n" +
                $"damagetype=\"{dmgType}\"\n" +
                $"exp=\"{experience}\"\n" +
                $"prize_type=\"{prizeType}\"\n" +
                $"prize_name=\"{prizeName}\"\n" +
                $"hit01=\"{hitMsg1}\"\n" +
                $"hit02=\"{hitMsg2}\"\n" +
                $"hit03=\"{hitMsg3}\"\n" +
                $"art01=\"{frame1Path}\"\n" +
                $"art02=\"{frame2Path}\"\n" +
                $"artfreq=\"{frameFrequency}\"\n";

            TextWriter tw = new StreamWriter(System.IO.Path.Combine(locationToSave, itoFileName));
            tw.WriteLine(enemyInfo);
            tw.Close();
        }


        public static string ConvertOptionToIto(EventOption option, char optionLetter) {
            string optionInfo = "\n" +
                $"option{optionLetter}=\"{option.optionDesc}\"\n" +
                $"test{optionLetter}=\"{option.statTest}\"\n" +
                $"success{optionLetter}=\"{option.successText}\"\n" +
                $"winprize{optionLetter}=\"{option.successPrize}\"\n" +
                $"winnumber{optionLetter}=\"{option.winPrizeAmt}\"\n" +
                $"failure{optionLetter}=\"{option.failureText}\"\n" +
                $"failureprize{optionLetter}=\"{option.failurePrize}\"\n" +
                $"failprizenum{optionLetter}=\"{option.losePrizeAmt}\"\n";
            return optionInfo;
        }

        /// <summary>
        /// Will extract the information stored in a line of an .ito file.
        /// </summary>
        /// <param name="line">The requested line to extract information from.</param>
        /// <returns>The information stored in the line given.</returns>
        public static string ExtractInfo(string line) {
            int startIndex = line.IndexOf("\"") + 1;
            int lastIndex = line.LastIndexOf("\"");
            return line.Substring(startIndex, lastIndex - startIndex);
        }
    }
}
