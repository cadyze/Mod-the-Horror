using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Mod_the_Horror
{
    class FileManager
    {


        /// <summary>
        /// 
        /// </summary>
        /// <returns>the path of the ito file chosen by the user</returns>
        public static string ChooseItoFile()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "character";
            dlg.DefaultExt = ".ito";
            dlg.Filter = "Ito Files|*.ito";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                return dlg.FileName;
            }
            return "";
        }

        public static string ChooseDirectory()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return dialog.FileName;
            }
            return "";
        }


        public static string CreateDirectory(string nameOfFolder, string folderPath = "")
        {
            //If the path isn't given, set to the default folder.
            if (folderPath.Equals("")) folderPath = Settings.defaultFolderPath;

            string directoryPath = System.IO.Path.Combine(folderPath, nameOfFolder);
            if (!System.IO.Directory.Exists(directoryPath)) System.IO.Directory.CreateDirectory(directoryPath);
            return directoryPath;
        }


        public static string ImportSprite()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "sprite_icon";
            dlg.DefaultExt = ".png";
            dlg.Filter = "Images|*.png;*.jpg*.jpeg";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                return dlg.FileName;
            }
            return "";
        }

        public static void SaveImage(Image imageToSave, string filePath)
        {
            var pngEncoder = new PngBitmapEncoder();
            BitmapSource bitmapSource = (BitmapSource)imageToSave.Source;
            if (bitmapSource != null)
            {
                pngEncoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                if (!System.IO.File.Exists(filePath))
                {
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        pngEncoder.Save(stream);
                        stream.Close();
                    }
                }
            }
        }

        public static void UpdateImage(Image imgToChange, string imgPath)
        {
            try
            {
                //Change the image of sprite to highlight the change.
                Uri uriSource = new Uri(imgPath);
                imgToChange.Source = new BitmapImage(uriSource);
            }
            catch (FileNotFoundException) {
                Trace.WriteLine("File exception thrown!");
            }
        }
    }
}
