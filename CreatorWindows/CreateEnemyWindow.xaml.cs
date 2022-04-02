using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Shapes;
using System.Windows.Xps;

namespace Mod_the_Horror
{
    public enum Enemy_ComboBox { 
        DamageType,
        Location,
        EnemyType,
        CanRun
    }
    /// <summary>
    /// Interaction logic for CreateEnemyWindow.xaml
    /// </summary>
    public partial class CreateEnemyWindow : Window
    {
        string fileName = "";
        string currentDirectoryPath = "";
        string currentSpriteDirectoryPath = "";
        string frame1Path = "";
        string frame2Path = "";
        private Dictionary<string, string> canRunDecoder = new Dictionary<string, string>()
        {
            { "0", "No" },
            { "1", "Yes" }
        };

        private Dictionary<string, string> damageTypeDecoder = new Dictionary<string, string>()
        {
            { "STA", "Stamina" },
            { "REA", "Reason" },
            { "ALL", "Stamina + Reason" },
            { "DOOM", "Doom" }
        };

        public void LoadMod(string path) {
            string? directory = System.IO.Path.GetDirectoryName(path);
            if (directory != null) UpdateCurrentDirectory(directory);
            UpdateCurrentItoName(System.IO.Path.GetFileName(path));
            ReadItoInformation(System.IO.File.ReadAllLines(path));
        }

        public void InitializeMod(string modName, string path)
        {
            string rootDirectory = FileManager.CreateDirectory(modName, path);
            UpdateCurrentDirectory(rootDirectory);
            string spriteDirectory = FileManager.CreateDirectory("enemy_art", rootDirectory);
            UpdateCurrentSpriteDirectory(spriteDirectory);
            UpdateCurrentItoName($"{modName}.ito");
        }

        public void ReadItoInformation(string[] lines) {
            foreach (string line in lines)
            {
                if (line.Length > 5 && line.Substring(0, 5).Equals("name=")) txtBox_name.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("subtitle=")) txtBox_subtitle.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("location=")) UIManager.UpdateComboBox(comboBox_location, ItoWriter.ExtractInfo(line));
                if (line.Length > 5 && line.Substring(0, 5).Equals("type=")) UIManager.UpdateComboBox(comboBox_type, ItoWriter.ExtractInfo(line));
                if (line.Contains("intro=")) txtBox_introduction.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("author=")) txtBox_author.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("can_run=")) UIManager.UpdateComboBox(comboBox_canRun, ItoWriter.ExtractInfo(line), canRunDecoder);
                if (line.Contains("health=")) txtBox_health.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("power=")) txtBox_power.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("damagevalue=")) txtBox_dmgValue.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("damagetype=")) UIManager.UpdateComboBox(comboBox_dmgType, ItoWriter.ExtractInfo(line), damageTypeDecoder);
                if (line.Contains("hit01=")) txtBox_hitMsgA.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("hit02=")) txtBox_hitMsgB.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("hit03=")) txtBox_hitMsgC.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("art01=")) UpdateFrame1(ItoWriter.ExtractInfo(line));
                if (line.Contains("art02=")) UpdateFrame2(ItoWriter.ExtractInfo(line));
                if (line.Contains("artfreq=")) txtBox_frequency.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("exp=")) txtBox_experience.Text = ItoWriter.ExtractInfo(line);
                if (line.Contains("prize_type="))
                {
                    string info = ItoWriter.ExtractInfo(line);
                    if (info.Equals("")) comboBox_prizeType.SelectedItem = "NONE";
                    else UIManager.UpdateComboBox(comboBox_prizeType, info);
                }
                if (line.Contains("prize_name=")) UIManager.UpdateComboBox(comboBox_prizeItem, ItoWriter.ExtractInfo(line));
            }
        }
        public CreateEnemyWindow()
        {
            InitializeComponent();

            //Creates and initializes combo boxes
            UIManager.InitializeComboBox(comboBox_location, @"GameInformation\EnemyLocations.txt");
            UIManager.InitializeComboBox(comboBox_type, @"GameInformation\EnemyTypes.txt");
            UIManager.InitializeComboBox(comboBox_prizeType, @"GameInformation\EnemyPrizes.txt");
            comboBox_prizeType.Items.Add("NONE");

            UIManager.InitializeComboBox(comboBox_prizeItem, @"GameInformation\ObtainableItems.txt");
            UIManager.InitializeComboBox(comboBox_canRun, @"GameInformation\EnemyEscapeOptions.txt", canRunDecoder);
            UIManager.InitializeComboBox(comboBox_dmgType, @"GameInformation\EnemyDamageTypes.txt", damageTypeDecoder);
        }

        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            string type = (string)comboBox_type.SelectedItem;
            string location = (string)comboBox_location.SelectedItem;
            string dmgType = UIManager.GetDecoderKey(damageTypeDecoder, (string)comboBox_dmgType.SelectedItem);

            string prizeType = ((string)comboBox_prizeType.SelectedItem).ToLower();
            prizeType = prizeType.Equals("NONE", StringComparison.OrdinalIgnoreCase) ? "" : prizeType;

            string prize = prizeType.Equals("ITEM", StringComparison.OrdinalIgnoreCase) ? ((string)comboBox_prizeItem.SelectedItem).ToUpper() : txtBox_prizeName.Text;

            string canRunString = UIManager.GetDecoderKey(canRunDecoder, (string)comboBox_canRun.SelectedItem);
            bool canRun = true;
            if (canRunString != null) {
                if (canRunString.Equals("Yes", StringComparison.OrdinalIgnoreCase)) canRun = true;
                else if(canRunString.Equals("No", StringComparison.OrdinalIgnoreCase)) canRun = false;
            }

            string? frame1Name = System.IO.Path.GetFileName(frame1Path);
            string? frame2Name = System.IO.Path.GetFileName(frame2Path);
            string? spriteDirectoryName = System.IO.Path.GetFileName(currentSpriteDirectoryPath);

            int health, power, experience, dmgValue, frameFreq;
            health = int.Parse(txtBox_health.Text);
            power = int.Parse(txtBox_power.Text);
            experience = int.Parse(txtBox_experience.Text);
            dmgValue = int.Parse(txtBox_dmgValue.Text);
            frameFreq = int.Parse(txtBox_frequency.Text);

            FileManager.SaveImage(img_frame1, System.IO.Path.Combine(currentSpriteDirectoryPath, frame1Name));
            FileManager.SaveImage(img_frame2, System.IO.Path.Combine(currentSpriteDirectoryPath, frame2Name));

            ItoWriter.WriteEnemy(currentDirectoryPath, fileName, txtBox_name.Text, txtBox_subtitle.Text,
                type, location, txtBox_author.Text, canRun, txtBox_introduction.Text, health, power,
                dmgType, dmgValue, txtBox_hitMsgA.Text, txtBox_hitMsgB.Text, txtBox_hitMsgC.Text, experience,
                prizeType, prize, frameFreq, spriteDirectoryName, frame1Name, frame2Name);
        }



        private void ImportFrame1_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = FileManager.ImportSprite();
            if (!potentialPath.Equals(""))
            {
                //_hasDirtyData = true;
                frame1Path = potentialPath;
                FileManager.UpdateImage(img_frame1, frame1Path);
            }
        }
        private void ImportFrame2_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = FileManager.ImportSprite();
            if (!potentialPath.Equals(""))
            {
                //_hasDirtyData = true;
                frame2Path = potentialPath;
                FileManager.UpdateImage(img_frame2, frame2Path);
            }
        }

        internal void UpdateCurrentDirectory(string directory)
        {
            currentDirectoryPath = directory;
        }

        internal void UpdateCurrentItoName(string path)
        {
            fileName = System.IO.Path.GetFileName(path);
        }

        public void UpdateCurrentSpriteDirectory(string newPath)
        {
            currentSpriteDirectoryPath = newPath;
        }

        public void UpdateFrame1(string imagePath)
        {
            frame1Path = imagePath;
            string? spriteLocation = System.IO.Path.GetDirectoryName(imagePath);
            if (currentSpriteDirectoryPath.Equals("") && spriteLocation != null) UpdateCurrentSpriteDirectory(System.IO.Path.Combine(currentDirectoryPath, spriteLocation));
            if (!imagePath.Equals("")) FileManager.UpdateImage(img_frame1, System.IO.Path.Combine(currentDirectoryPath, frame1Path));
        }
        public void UpdateFrame2(string imagePath)
        {
            frame2Path = imagePath;
            string? spriteLocation = System.IO.Path.GetDirectoryName(imagePath);
            if (currentSpriteDirectoryPath.Equals("") && spriteLocation != null) UpdateCurrentSpriteDirectory(System.IO.Path.Combine(currentDirectoryPath, spriteLocation));
            if (!imagePath.Equals("")) FileManager.UpdateImage(img_frame2, System.IO.Path.Combine(currentDirectoryPath, frame2Path));
        }

        private void OnNumBoxChanged(object sender, TextChangedEventArgs e)
        {
            TextBox numberBox = (TextBox)sender;
            int numberTyped = 0;
            try
            {
                numberTyped = int.Parse(numberBox.Text);
                if (numberTyped > 99) numberBox.Text = "99";
                else if (numberTyped < 1) numberBox.Text = "1";
                else numberBox.Text = $"{numberTyped}";
            }
            catch (FormatException)
            {
                numberBox.Text = "1";
            }
        }

        private void comboBox_prizeType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string prizeType = ((string)comboBox_prizeType.SelectedItem).ToUpper();
            switch (prizeType){
                case "ITEM":
                    comboBox_prizeItem.Visibility = Visibility.Visible;
                    break;
                case "NONE":
                    txtBox_prizeName.Visibility = Visibility.Hidden;
                    comboBox_prizeItem.Visibility = Visibility.Hidden;
                    break;
            }
        }
    }
}
