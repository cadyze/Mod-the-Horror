using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public CreateEnemyWindow()
        {
            InitializeComponent();
        }
        private void OnSaveClicked(object sender, RoutedEventArgs e)
        {
            string type = ComboBoxItemToString((ComboBoxItem)comboBox_type.SelectedItem).ToLower();
            string location = ComboBoxItemToString((ComboBoxItem)comboBox_location.SelectedItem).ToLower();
            string dmgType = ComboBoxItemToString((ComboBoxItem)comboBox_dmgType.SelectedItem).ToUpper();
            switch (dmgType) {
                case "STAMINA":
                    dmgType = "STA";
                    break;
                case "REASON":
                    dmgType = "REA";
                    break;
                case "BOTH OF ABOVE":
                    dmgType = "ALL";
                    break;
                case "DOOM":
                    dmgType = "DOOM";
                    break;
            }
            string canRunString = ComboBoxItemToString((ComboBoxItem)comboBox_canRun.SelectedItem).ToUpper();
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
                txtBox_prizeType.Text, txtBox_prizeName.Text, frameFreq, spriteDirectoryName, frame1Name, frame2Name);
        }

        private string ComboBoxItemToString(ComboBoxItem boxItem) {
            string? itemString = (string?)boxItem.Content;
            if (itemString == null) return "";
            else return itemString.ToLower();
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

        public void UpdateComboBox(ComboBox comboBox, string itemName)
        {
            Trace.WriteLine(itemName.ToUpper());
            comboBox.SelectedValue = itemName.ToUpper();
            Trace.WriteLine(comboBox.SelectedValue.ToString());
        }
        public void UpdateComboBox(ComboBox comboBox, string itemName, Enemy_ComboBox cbType)
        {
            //Handles the special types of combo boxes.
            switch (cbType) {
                case Enemy_ComboBox.CanRun:
                    if (itemName.Equals("0")) comboBox.SelectedValue = "No";
                    else comboBox.SelectedValue = "Yes";
                    break;
                case Enemy_ComboBox.DamageType:
                    switch (itemName) {
                        case "STA":
                            comboBox.SelectedValue = "Stamina";
                            //comboBox.SelectedIndex = comboBox.Items.IndexOf("Stamina");
                            break;
                        case "REA":
                            comboBox.SelectedValue = "Reason";
                            //comboBox.SelectedIndex = comboBox.Items.IndexOf("Reason");
                            break;
                        case "ALL":
                            comboBox.SelectedValue = "Both of Above";
                            //comboBox.SelectedIndex = comboBox.Items.IndexOf("Both of Above");
                            break;
                        case "DOOM":
                            comboBox.SelectedValue = "Doom";
                            //comboBox.SelectedIndex = comboBox.Items.IndexOf("Doom");
                            break;
                    }
                    break;
            }
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
    }
}
