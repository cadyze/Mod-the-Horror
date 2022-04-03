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
using Mod_the_Horror.Classes;

namespace Mod_the_Horror
{
    public enum ModType { 
        CHARACTER,
        EVENT,
        ENEMY,
        MYSTERY,
        ERROR
    }

    public class ItoWriter
    {
        public static void WriteCustomCharacter(string locationToSave, string itoFileName, string name, string author, string contact,
            int strength, int dexterity, int perception, int charisma, int knowledge, int luck,
            string name_a, string menu_tag, string menu_desc, string perkpack_a, string perkpack_b,
            string fullName, string gender, int age,
            string iconPath, string backPath, string housePath,
            string chibiPath, string portraitAPath, string portraitBPath) {

            string relativeIconPath = iconPath.Equals("") ? "" : Path.GetRelativePath(locationToSave, iconPath);
            string relativeBackPath = backPath.Equals("") ? "" : Path.GetRelativePath(locationToSave, backPath);
            string relativeHousePath = housePath.Equals("") ? "" : Path.GetRelativePath(locationToSave, housePath);
            string relativeChibiPath = chibiPath.Equals("") ? "" : Path.GetRelativePath(locationToSave, chibiPath);
            string relativePortraitAPath = portraitAPath.Equals("") ? "" : Path.GetRelativePath(locationToSave, portraitAPath);
            string relativePortraitABath = portraitBPath.Equals("") ? "" : Path.GetRelativePath(locationToSave, portraitBPath);

            if (!itoFileName.Contains(".ito")) itoFileName = itoFileName + ".ito";

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
                $"\nsprite_icon=\"{relativeIconPath}\"" +
                $"\nsprite_back=\"{relativeBackPath}\"" +
                $"\nsprite_house=\"{relativeHousePath}\"" +
                $"\nsprite_chibi=\"{relativeChibiPath}\"" +
                $"\nportrait_a=\"{relativePortraitAPath}\"" +
                $"\nportrait_b=\"{relativePortraitABath}\"" +
                $"\nname_a=\"{name_a}\"" +
                $"\nmenu_tag=\"--{menu_tag}--\"" +
                $"\nmenu_desc=\"{fullName}#{age} / {gender}# #{menu_desc}\"" +
                $"\nperkpack_a=\"{perkpack_a}\"" +
                $"\nperkpack_b=\"{perkpack_b}\"";

            TextWriter tw = new StreamWriter(Path.Combine(locationToSave, itoFileName));
            tw.WriteLine(charInfo);
            tw.Close();
        }

        public static void WriteEvent(string locationToSave, string itoFileName, string name, string author, string evnLocation, string contact,
            string flavor, int numOptions, string spritePath, string desc, List<EventOption> eventOptions) {

            string relativeSpritePath = spritePath.Equals("") ? "" : Path.GetRelativePath(locationToSave, spritePath);

            string eventInfo = "[event]" +
                $"\nname=\"{name}\"" +
                $"\nlocation=\"{evnLocation}\"" +
                $"\nauthor=\"{author}\"" +
                $"\ncontact=\"{contact}\"" +
                $"\nflavor=\"{flavor}\"" +
                $"\noptions=\"{numOptions}\"" +
                $"\nimage=\"{relativeSpritePath}\"" +
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
            string prizeType, string prizeName, int frameFrequency, string frame1Path, string frame2Path) {

            string relativeFrame1Path = frame1Path.Equals("") ? "" : Path.GetRelativePath(locationToSave, frame1Path);
            string relativeFrame2Path = frame2Path.Equals("") ? "" : Path.GetRelativePath(locationToSave, frame2Path);

            if (!itoFileName.Contains(".ito")) itoFileName = itoFileName + ".ito";

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
                $"art01=\"{relativeFrame1Path}\"\n" +
                $"art02=\"{relativeFrame2Path}\"\n" +
                $"artfreq=\"{frameFrequency}\"\n";
            
            TextWriter tw = new StreamWriter(Path.Combine(locationToSave, itoFileName));
            tw.WriteLine(enemyInfo);
            tw.Close();
        }

        public static void WriteMystery(string locationToSave, string itoFileName, string name, string author, string description, string endLocation,
            string mysterySummary, string[] introPages, MysteryEnding[] endings, InvestigationTurn[] investigationTurns,
            string iconPath, string mysteryArtPath, string backgroundPath)
        {
            string relativeMysteryArtPath = mysteryArtPath.Equals("") ? "" : Path.GetRelativePath(locationToSave, mysteryArtPath);
            string relativeIconPath = iconPath.Equals("") ? "" : Path.GetRelativePath(locationToSave, iconPath);
            string relativeBackgroundPath = backgroundPath.Equals("") ? "" : Path.GetRelativePath(locationToSave, backgroundPath);

            if (!itoFileName.Contains(".ito")) itoFileName = itoFileName + ".ito";

            string mysteryInfo = "[mystery]\n" +
                $"name=\"{name}\"\n" +
                $"author=\"{author}\"\n" +
                $"art=\"{relativeIconPath}\"\n" +
                $"description=\"{description}\"\n" +

                $"[intro]\n" +
                $"art=\"{relativeMysteryArtPath}\"\n" +
                $"text_one=\"{introPages[0]}\"\n" +
                $"text_two=\"{introPages[1]}\"\n" +
                $"text_three=\"{introPages[2]}\"\n" +

                $"[progress]\n" +
                $"location=\"{endLocation}\"\n" +
                $"background=\"{relativeBackgroundPath}\"\n";

            foreach (InvestigationTurn turn in investigationTurns) {
                string relativeEventPath = turn.forcedEvent.Equals("") ? "" : Path.GetRelativePath(locationToSave, turn.forcedEvent);
                mysteryInfo = mysteryInfo +
                    $"{turn.progressNum}_loc=\"{turn.location}\"\n" +
                    $"{turn.progressNum}_txt=\"{turn.precedingText}\"\n" +
                    $"{turn.progressNum}_frc=\"{relativeEventPath}\"\n";
            }

            foreach (MysteryEnding end in endings) {
                string relativeEndArtPath = end.pathToImage.Equals("") ? "" : Path.GetRelativePath(locationToSave, end.pathToImage);
                mysteryInfo = mysteryInfo +
                    $"[ending_{end.EndingLetter.ToLower()}]\n" +
                    $"end_title=\"{end.endingTitle}\"\n" +
                    $"end_img=\"{relativeEndArtPath}\"\n" +
                    $"end_txta=\"{end.pageA}\"\n" +
                    $"end_txtb=\"{end.pageB}\"\n" +
                    $"end_txtc=\"{end.pageC}\"\n" +
                    $"end_txtd=\"{end.pageD}\"\n";
            }

            mysteryInfo = mysteryInfo + 
                $"[big_ending]\n" +
                $"end_txt=\"{mysterySummary}\"";

            TextWriter tw = new StreamWriter(Path.Combine(locationToSave, itoFileName));
            tw.WriteLine(mysteryInfo);
            tw.Close();
        }

        public static ModType ReadItoType(string path) {
            string[] allLines = System.IO.File.ReadAllLines(path);
            foreach (string line in allLines) {
                if (line.Contains("character")) return ModType.CHARACTER;
                if (line.Contains("event")) return ModType.EVENT;
                if (line.Contains("enemy")) return ModType.ENEMY;
                if (line.Contains("[mystery]")) return ModType.MYSTERY;
            }
            return ModType.ERROR;
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
