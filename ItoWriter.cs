using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace Mod_the_Horror
{
    public class ItoWriter
    {
        public static void WriteCustomCharacter(string name, string author, string contact, 
            int strength, int dexterity, int perception, int charisma, int knowledge, int luck, 
            string name_a, string menu_tag, string menu_desc, string perkpack_a, string perkpack_b,
            string fullName, string gender, int age,
            FrameworkElement? sprite_icon = null, FrameworkElement? sprite_back = null, FrameworkElement? sprite_house = null, 
            FrameworkElement? sprite_chibi = null, FrameworkElement? portrait_a = null) {

            string directoryPath = CreateDirectory("ITO TEST");
            string spriteDirectoryPath = CreateDirectory("char_sprites", directoryPath);

            string spriteIconPath = System.IO.Path.Combine(spriteDirectoryPath, "spr_icon.png");
            string spriteChibiPath = System.IO.Path.Combine(spriteDirectoryPath, "spr_back.png");
            string spriteBackPath = System.IO.Path.Combine(spriteDirectoryPath, "spr_house.png");
            string spriteHousePath = System.IO.Path.Combine(spriteDirectoryPath, "spr_chibi.png");
            string spritePortraitPath = System.IO.Path.Combine(spriteDirectoryPath, "spr_portrait_a.png");


            string fileName = "Poopoo.ito";
            string pathName = System.IO.Path.Combine(directoryPath, fileName);

            //Save the images to the path listed by the user.
            Dictionary<string, FrameworkElement> imgsToSave = new Dictionary<string, FrameworkElement>();
            if (sprite_icon != null) imgsToSave.Add(spriteIconPath, sprite_icon);
            if (sprite_back != null) imgsToSave.Add(spriteBackPath, sprite_back);
            if (sprite_house != null) imgsToSave.Add(spriteHousePath, sprite_house);
            if (sprite_chibi != null) imgsToSave.Add(spriteChibiPath, sprite_chibi);
            if (portrait_a != null) imgsToSave.Add(spritePortraitPath, portrait_a);
            SavePNG(imgsToSave);

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
                $"\nsprite_icon=\"char_sprites\\spr_icon.png\"" +
                $"\nsprite_back=\"char_sprites\\spr_back.png\"" +
                $"\nsprite_house=\"char_sprites\\spr_house.png\"" +
                $"\nsprite_chibi=\"char_sprites\\spr_chibi.png\"" +
                $"\nportrait_a=\"char_sprites\\spr_portrait_a.png\"" +
                $"\nname_a=\"{name_a}\"" +
                $"\nmenu_tag=\"{menu_tag}\"" +
                $"\nmenu_desc=\"Name: {fullName}#{age} / {gender} #{menu_desc}\"" +
                $"\nperkpack_a=\"{perkpack_a}\"" +
                $"\nperkpack_b=\"{perkpack_b}\"";

            Console.WriteLine($"Writing to file at path: {pathName}");
            if (!System.IO.File.Exists(pathName)) {
                TextWriter sw = new StreamWriter(pathName);
                sw.WriteLine(charInfo);
                sw.Close();
            }


        }
        private static void SaveUsingEncoder(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            Size visualSize = new Size(visual.ActualWidth, visual.ActualHeight);
            visual.Measure(visualSize);
            visual.Arrange(new Rect(visualSize));
            bitmap.Render(visual);
            BitmapFrame frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);

            using (var stream = File.Create(fileName))
            {
                encoder.Save(stream);
            }
        }

        private static void SavePNG(Dictionary<string, FrameworkElement> imgsToSave) {
            foreach (KeyValuePair<string, FrameworkElement> pair in imgsToSave) {
                SavePNG(pair.Key, pair.Value);
            }
        }

        private static void SavePNG(string fileName, FrameworkElement img) {
            var encoder = new PngBitmapEncoder();
            SaveUsingEncoder(img, fileName, encoder);
        }

        /**
         * Return the path of the newly created directory at the folder path given.
         */
        private static string CreateDirectory(string nameOfFolder, string folderPath = "") {
            //If the path isn't given, set to the default folder.
            if(folderPath.Equals("")) folderPath = Settings.defaultFolderPath;

            string directoryPath = System.IO.Path.Combine(folderPath, nameOfFolder);
            if(!System.IO.Directory.Exists(directoryPath)) System.IO.Directory.CreateDirectory(directoryPath);
            return directoryPath;
        }
    }
}
