using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;

namespace Mod_the_Horror
{
    /// <summary>
    /// Interaction logic for CreateCharacterWindow.xaml
    /// </summary>
    public partial class CreateCharacterWindow : Window
    {
        private static bool _hasDirtyData;
        private string spriteIconPath = "";
        private string spriteChibiPath = "";
        private string spriteBackPath = "";
        private string spriteHousePath = "";
        private string spritePortraitPath = "";
        private string currentDirectoryPath = "";
        private string currentSpriteDirectoryPath = "";
        private string itoFileName = "";

        public void UpdateCurrentItoName(string path) {
            itoFileName = System.IO.Path.GetFileName(path);
            //string? directoryPath = System.IO.Path.GetDirectoryName(path);
            //if (directoryPath != null) UpdateCurrentDirectory(directoryPath);
        }
        public void UpdateCurrentDirectory(string newPath) {
            currentDirectoryPath = newPath;
        }

        public void UpdateCurrentSpriteDirectory(string newPath) {
            currentSpriteDirectoryPath = newPath;
        }

        public void UpdateSpriteIconPath(string newPath) {
            spriteIconPath = newPath;
            string? spriteLocation = System.IO.Path.GetDirectoryName(newPath);
            Trace.WriteLine(newPath);
            if (currentSpriteDirectoryPath.Equals("") && spriteLocation != null) UpdateCurrentSpriteDirectory(System.IO.Path.Combine(currentDirectoryPath, spriteLocation));
            if(!newPath.Equals("")) UpdateImage(img_spriteIcon, System.IO.Path.Combine(currentDirectoryPath, spriteIconPath));
        }

        public void UpdateSpriteChibiPath(string newPath) {
            spriteChibiPath = newPath;
            string? spriteLocation = System.IO.Path.GetDirectoryName(newPath);
            if (currentSpriteDirectoryPath.Equals("") && spriteLocation != null) UpdateCurrentSpriteDirectory(System.IO.Path.Combine(currentDirectoryPath, spriteLocation));
            if (!newPath.Equals("")) UpdateImage(img_spriteChibi, System.IO.Path.Combine(currentDirectoryPath, spriteChibiPath));
        }

        public void UpdateSpriteBackPath(string newPath) {
            spriteBackPath = newPath;
            string? spriteLocation = System.IO.Path.GetDirectoryName(newPath);
            if (currentSpriteDirectoryPath.Equals("") && spriteLocation != null) UpdateCurrentSpriteDirectory(System.IO.Path.Combine(currentDirectoryPath, spriteLocation));
            if (!newPath.Equals("")) UpdateImage(img_spriteBack, System.IO.Path.Combine(currentDirectoryPath, spriteBackPath));
        }

        public void UpdateSpriteHousePath(string newPath) {
            spriteHousePath = newPath;
            string? spriteLocation = System.IO.Path.GetDirectoryName(newPath);
            if (currentSpriteDirectoryPath.Equals("") && spriteLocation != null) UpdateCurrentSpriteDirectory(System.IO.Path.Combine(currentDirectoryPath, spriteLocation));
            if (!newPath.Equals("")) UpdateImage(img_spriteHouse, System.IO.Path.Combine(currentDirectoryPath, spriteHousePath));
        }

        public void UpdateSpritePortraitPath(string newPath) {
            spritePortraitPath = newPath;
            string? spriteLocation = System.IO.Path.GetDirectoryName(newPath);
            if (currentSpriteDirectoryPath.Equals("") && spriteLocation != null) UpdateCurrentSpriteDirectory(System.IO.Path.Combine(currentDirectoryPath, spriteLocation));
            if (!newPath.Equals("")) UpdateImage(img_spritePortrait, System.IO.Path.Combine(currentDirectoryPath, spritePortraitPath));
        }

        public CreateCharacterWindow()
        {
            InitializeComponent();
        }

        private void OnTextBoxChange(object sender, TextChangedEventArgs e)
        {
            _hasDirtyData = true;
        }

        private void OnNumberBoxChanged(object sender, TextChangedEventArgs e) {
            _hasDirtyData = true;
            TextBox numberBox = (TextBox)sender;
            int numberTyped = 0;
            Int32.TryParse(numberBox.Text, out numberTyped);

            if (numberTyped > 99) numberBox.Text = "99";
            else if (numberTyped < 0) numberBox.Text = "0";
            else numberBox.Text = $"{numberTyped}";
        }

        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            string? sprIconName = System.IO.Path.GetFileName(spriteIconPath);
            string? sprBackName = System.IO.Path.GetFileName(spriteBackPath);
            string? sprHouseName = System.IO.Path.GetFileName(spriteHousePath);
            string? sprChibiName = System.IO.Path.GetFileName(spriteChibiPath);
            string? sprPortraitName = System.IO.Path.GetFileName(spritePortraitPath);

            //Parses are safe here as the textboxes can only accept numbers
            ItoWriter.WriteCustomCharacter(currentDirectoryPath, itoFileName, txtBox_name.Text, txtBox_author.Text, txtBox_contact.Text,
                int.Parse(txtBox_strength.Text), int.Parse(txtBox_dexterity.Text), int.Parse(txtBox_perception.Text),
                int.Parse(txtBox_charisma.Text), int.Parse(txtBox_knowledge.Text), int.Parse(txtBox_luck.Text),
                txtBox_aName.Text, txtBox_menuTag.Text, txtBox_menuDesc.Text, txtBox_aPerkPack.Text, txtBox_bPerkPack.Text,
                txtBox_name.Text, txtBox_gender.Text, int.Parse(txtBox_age.Text), System.IO.Path.GetFileName(currentSpriteDirectoryPath), 
                sprIconName, sprBackName, sprHouseName, sprChibiName, sprPortraitName);

            //Save images
            SaveImage(img_spriteIcon, System.IO.Path.Combine(currentSpriteDirectoryPath, sprIconName));
            SaveImage(img_spriteBack, System.IO.Path.Combine(currentSpriteDirectoryPath, sprBackName));
            SaveImage(img_spriteHouse, System.IO.Path.Combine(currentSpriteDirectoryPath, sprHouseName));
            SaveImage(img_spriteChibi, System.IO.Path.Combine(currentSpriteDirectoryPath, sprChibiName));
            SaveImage(img_spritePortrait, System.IO.Path.Combine(currentSpriteDirectoryPath, sprPortraitName));
        }

        void SaveImage(Image imageToSave, string filePath) {
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

        private void UpdateImage(Image imgToChange, string imgPath)
        {
            //Change the image of sprite to highlight the change.
            Uri uriSource = new Uri(imgPath);
            imgToChange.Source = new BitmapImage(uriSource);
        }

        private void btn_spriteIcon_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = FileManager.ImportSprite();
            if (!potentialPath.Equals(""))
            {
                _hasDirtyData = true;
                spriteIconPath = potentialPath;
                UpdateImage(img_spriteIcon, spriteIconPath);
            }
        }

        private void btn_spriteBack_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = FileManager.ImportSprite();
            if (!potentialPath.Equals(""))
            {
                _hasDirtyData = true;
                spriteBackPath = potentialPath;
                UpdateImage(img_spriteBack, spriteBackPath);
            }

        }

        private void btn_spriteHouse_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = FileManager.ImportSprite();
            if (!potentialPath.Equals(""))
            {
                _hasDirtyData = true;
                spriteHousePath = potentialPath;
                UpdateImage(img_spriteHouse, spriteHousePath);
            }
        }

        private void btn_spritePortrait_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = FileManager.ImportSprite();
            if (!potentialPath.Equals(""))
            {
                _hasDirtyData = true;
                spritePortraitPath = potentialPath;
                UpdateImage(img_spritePortrait, spritePortraitPath);
            }
        }

        private void btn_spriteChibi_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = FileManager.ImportSprite();
            if (!potentialPath.Equals(""))
            {
                _hasDirtyData = true;
                spriteChibiPath = potentialPath;
                UpdateImage(img_spriteChibi, spriteChibiPath);
            }

        }
    }
}
