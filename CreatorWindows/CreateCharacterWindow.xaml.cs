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
using System.Reflection;
using Mod_the_Horror.Classes;

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
        private string spritePortraitAPath = "";
        private string spritePortraitBPath = "";
        private string currentDirectoryPath = "";
        private string currentSpriteDirectoryPath = "";
        private string itoFileName = "";
        private CharacterMod? currentMod;

        public void LoadMod(string path) {
            string? directory = System.IO.Path.GetDirectoryName(path);
            if (directory != null) UpdateCurrentDirectory(directory);
            UpdateCurrentItoName(System.IO.Path.GetFileName(path));
            currentMod = new CharacterMod(File.ReadAllLines(path));
            UpdateUI();
        }

        public void InitializeMod(string modName, string path) {
            currentMod = new CharacterMod();
            string rootDirectory = FileManager.CreateDirectory(modName, path);
            UpdateCurrentDirectory(rootDirectory);
            string spriteDirectory = FileManager.CreateDirectory("char_sprites", rootDirectory);
            UpdateCurrentSpriteDirectory(spriteDirectory);
            UpdateCurrentItoName($"{modName}.ito");
        }

        public void UpdateUI() {
            if (currentMod != null)
            {
                txtBox_name.Text = currentMod.name;
                txtBox_author.Text = currentMod.author;
                txtBox_contact.Text = currentMod.contact;
                txtBox_dexterity.Text = currentMod.dexterity.ToString();
                txtBox_perception.Text = currentMod.perception.ToString();
                txtBox_charisma.Text = currentMod.charisma.ToString();
                txtBox_knowledge.Text = currentMod.knowledge.ToString();
                txtBox_luck.Text = currentMod.luck.ToString();
                UpdateSpriteIconPath(currentMod.sprite_icon);
                UpdateSpriteBackPath(currentMod.sprite_back);
                UpdateSpriteHousePath(currentMod.sprite_house);
                UpdateSpriteChibiPath(currentMod.sprite_chibi);
                UpdateSpritePortraitAPath(currentMod.portrait_a);
                UpdateSpritePortraitBPath(currentMod.portrait_b);
                txtBox_aName.Text = currentMod.name_a;
                txtBox_bName.Text = currentMod.name_b;
                txtBox_menuTag.Text = currentMod.occupation;
                txtBox_menuDesc.Text = currentMod.description;
                txtBox_fullName.Text = currentMod.full_name;
                UIManager.UpdateComboBox(comboBox_aPerkPack, currentMod.perkpack_a);
                UIManager.UpdateComboBox(comboBox_bPerkPack, currentMod.perkpack_b);
            }
        }

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
            if(!newPath.Equals("")) FileManager.UpdateImage(img_spriteIcon, System.IO.Path.Combine(currentDirectoryPath, spriteIconPath));
        }

        public void UpdateSpriteChibiPath(string newPath) {
            spriteChibiPath = newPath;
            string? spriteLocation = System.IO.Path.GetDirectoryName(newPath);
            if (currentSpriteDirectoryPath.Equals("") && spriteLocation != null) UpdateCurrentSpriteDirectory(System.IO.Path.Combine(currentDirectoryPath, spriteLocation));
            if (!newPath.Equals("")) FileManager.UpdateImage(img_spriteChibi, System.IO.Path.Combine(currentDirectoryPath, spriteChibiPath));
        }

        public void UpdateSpriteBackPath(string newPath) {
            spriteBackPath = newPath;
            string? spriteLocation = System.IO.Path.GetDirectoryName(newPath);
            if (currentSpriteDirectoryPath.Equals("") && spriteLocation != null) UpdateCurrentSpriteDirectory(System.IO.Path.Combine(currentDirectoryPath, spriteLocation));
            if (!newPath.Equals("")) FileManager.UpdateImage(img_spriteBack, System.IO.Path.Combine(currentDirectoryPath, spriteBackPath));
        }

        public void UpdateSpriteHousePath(string newPath) {
            spriteHousePath = newPath;
            string? spriteLocation = System.IO.Path.GetDirectoryName(newPath);
            if (currentSpriteDirectoryPath.Equals("") && spriteLocation != null) UpdateCurrentSpriteDirectory(System.IO.Path.Combine(currentDirectoryPath, spriteLocation));
            if (!newPath.Equals("")) FileManager.UpdateImage(img_spriteHouse, System.IO.Path.Combine(currentDirectoryPath, spriteHousePath));
        }

        public void UpdateSpritePortraitAPath(string newPath) {
            spritePortraitAPath = newPath;
            string? spriteLocation = System.IO.Path.GetDirectoryName(newPath);
            if (currentSpriteDirectoryPath.Equals("") && spriteLocation != null) UpdateCurrentSpriteDirectory(System.IO.Path.Combine(currentDirectoryPath, spriteLocation));
            if (!newPath.Equals("")) FileManager.UpdateImage(img_spritePortrait, System.IO.Path.Combine(currentDirectoryPath, spritePortraitAPath));
        }
        public void UpdateSpritePortraitBPath(string newPath)
        {
            spritePortraitBPath = newPath;
            string? spriteLocation = System.IO.Path.GetDirectoryName(newPath);
            if (currentSpriteDirectoryPath.Equals("") && spriteLocation != null) UpdateCurrentSpriteDirectory(System.IO.Path.Combine(currentDirectoryPath, spriteLocation));
            if (!newPath.Equals("")) FileManager.UpdateImage(img_spritePortraitB, System.IO.Path.Combine(currentDirectoryPath, spritePortraitBPath));
        }

        public CreateCharacterWindow()
        {
            InitializeComponent();

            //Initalizes and creates all of the perk pack combo boxes.
            ComboBox[] perkPacks = { comboBox_aPerkPack, comboBox_bPerkPack };
            UIManager.InitializeComboBox(perkPacks, @"GameInformation\Perkpacks.txt"); 
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
            string? sprPortraitName = System.IO.Path.GetFileName(spritePortraitAPath);
            string? sprPortraitBName = System.IO.Path.GetFileName(spritePortraitBPath);

            string perkPackA = (string)comboBox_aPerkPack.SelectedItem;
            string perkPackB = (string)comboBox_bPerkPack.SelectedItem;

            //Parses are safe here as the textboxes can only accept numbers
            ItoWriter.WriteCustomCharacter(currentDirectoryPath, itoFileName, txtBox_name.Text, txtBox_author.Text, txtBox_contact.Text,
                int.Parse(txtBox_strength.Text), int.Parse(txtBox_dexterity.Text), int.Parse(txtBox_perception.Text),
                int.Parse(txtBox_charisma.Text), int.Parse(txtBox_knowledge.Text), int.Parse(txtBox_luck.Text),
                txtBox_aName.Text, txtBox_menuTag.Text, txtBox_menuDesc.Text, perkPackA, perkPackB,
                txtBox_fullName.Text, txtBox_gender.Text, int.Parse(txtBox_age.Text), System.IO.Path.GetFileName(currentSpriteDirectoryPath), 
                sprIconName, sprBackName, sprHouseName, sprChibiName, sprPortraitName, sprPortraitBName);

            //Save images
            FileManager.SaveImage(img_spriteIcon, System.IO.Path.Combine(currentSpriteDirectoryPath, sprIconName));
            FileManager.SaveImage(img_spriteBack, System.IO.Path.Combine(currentSpriteDirectoryPath, sprBackName));
            FileManager.SaveImage(img_spriteHouse, System.IO.Path.Combine(currentSpriteDirectoryPath, sprHouseName));
            FileManager.SaveImage(img_spriteChibi, System.IO.Path.Combine(currentSpriteDirectoryPath, sprChibiName));
            FileManager.SaveImage(img_spritePortrait, System.IO.Path.Combine(currentSpriteDirectoryPath, sprPortraitName));
            FileManager.SaveImage(img_spritePortraitB, System.IO.Path.Combine(currentSpriteDirectoryPath, sprPortraitBName));
        }

        private void btn_spriteIcon_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = FileManager.ImportSprite();
            if (!potentialPath.Equals(""))
            {
                _hasDirtyData = true;
                spriteIconPath = potentialPath;
                FileManager.UpdateImage(img_spriteIcon, spriteIconPath);
            }
        }

        private void btn_spriteBack_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = FileManager.ImportSprite();
            if (!potentialPath.Equals(""))
            {
                _hasDirtyData = true;
                spriteBackPath = potentialPath;
                FileManager.UpdateImage(img_spriteBack, spriteBackPath);
            }

        }

        private void btn_spriteHouse_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = FileManager.ImportSprite();
            if (!potentialPath.Equals(""))
            {
                _hasDirtyData = true;
                spriteHousePath = potentialPath;
                FileManager.UpdateImage(img_spriteHouse, spriteHousePath);
            }
        }

        private void btn_spritePortrait_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = FileManager.ImportSprite();
            if (!potentialPath.Equals(""))
            {
                _hasDirtyData = true;
                spritePortraitAPath = potentialPath;
                FileManager.UpdateImage(img_spritePortrait, spritePortraitAPath);
            }
        }

        private void btn_spriteChibi_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = FileManager.ImportSprite();
            if (!potentialPath.Equals(""))
            {
                _hasDirtyData = true;
                spriteChibiPath = potentialPath;
                FileManager.UpdateImage(img_spriteChibi, spriteChibiPath);
            }

        }

        private void btn_spritePortraitB_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = FileManager.ImportSprite();
            if (!potentialPath.Equals(""))
            {
                _hasDirtyData = true;
                spritePortraitBPath = potentialPath;
                FileManager.UpdateImage(img_spritePortraitB, spritePortraitBPath);
            }

        }
    }
}
