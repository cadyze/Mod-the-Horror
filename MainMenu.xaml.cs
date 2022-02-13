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
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Mod_the_Horror
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        bool isFileSettingCreate = true;
        public MainMenu()
        {
            InitializeComponent();
        }

        private void fileSettingBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isFileSettingCreate)
            {
                fileSettingBtn.Content = "EDIT FILE";
            }
            else {
                fileSettingBtn.Content = "CREATE FILE";
            }
            isFileSettingCreate = !isFileSettingCreate;
        }

        private void CharacterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isFileSettingCreate)
            {
                //CREATING IN HERE
                string potentialPath = FileManager.ChooseDirectory();
                string spritePath = System.IO.Path.Combine(potentialPath, "charSprites");
                if (!potentialPath.Equals(""))
                {
                    CreateCharacterWindow newCharWindow = new CreateCharacterWindow();
                    newCharWindow.Owner = this;
                    newCharWindow.ShowDialog();
                }
            }
            else 
            {
                //EDITING IN HERE
                string potentialPath = FileManager.ChooseItoFile();
                if (!potentialPath.Equals(""))
                {
                    CreateCharacterWindow newCharWindow = new CreateCharacterWindow();
                    string? directory = System.IO.Path.GetDirectoryName(potentialPath);
                    if(directory != null) newCharWindow.UpdateCurrentDirectory(directory);

                    //Read the data from the file given.
                    var itoFileLines = System.IO.File.ReadAllLines(potentialPath);
                    foreach (string line in itoFileLines) {
                        if (line.Contains("name=")) newCharWindow.txtBox_name.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("author=")) newCharWindow.txtBox_author.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("contact=")) newCharWindow.txtBox_contact.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("strength=")) newCharWindow.txtBox_strength.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("dexterity=")) newCharWindow.txtBox_dexterity.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("perception=")) newCharWindow.txtBox_perception.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("charisma=")) newCharWindow.txtBox_charisma.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("knowledge=")) newCharWindow.txtBox_knowledge.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("luck=")) newCharWindow.txtBox_luck.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("sprite_icon=")) newCharWindow.UpdateSpriteIconPath(ItoWriter.ExtractInfo(line));
                        if (line.Contains("sprite_back=")) newCharWindow.UpdateSpriteBackPath(ItoWriter.ExtractInfo(line));
                        if (line.Contains("sprite_house=")) newCharWindow.UpdateSpriteHousePath(ItoWriter.ExtractInfo(line));
                        if (line.Contains("sprite_chibi=")) newCharWindow.UpdateSpriteChibiPath(ItoWriter.ExtractInfo(line));
                        if (line.Contains("portrait_a=")) newCharWindow.UpdateSpritePortraitPath(ItoWriter.ExtractInfo(line));
                        if (line.Contains("name_a=")) newCharWindow.txtBox_aName.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("perkpack_a=")) newCharWindow.txtBox_aPerkPack.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("perkpack_b=")) newCharWindow.txtBox_bPerkPack.Text = ItoWriter.ExtractInfo(line);

                        if (line.Contains("menu_tag="))
                        {
                            string tagInfo = ItoWriter.ExtractInfo(line);
                            try {
                                int startPos = tagInfo.IndexOf("--") + 2;
                                int endPos = tagInfo.LastIndexOf("--");
                                tagInfo = tagInfo.Substring(startPos, endPos - startPos);
                            }
                            catch(ArgumentOutOfRangeException) {
                                tagInfo = "";
                            }
                            newCharWindow.txtBox_menuTag.Text = tagInfo;

                        }
                        if (line.Contains("menu_desc=")) {
                            //Menu description format: menu_desc="Name: _NAME_#_AGE_ / _GENDER_ #<<_DESCRIPTION_"
                            string descInfo = ItoWriter.ExtractInfo(line);
                            string charName = "";
                            string charGender = "";
                            string charAge = "";
                            if (descInfo.Contains("Name:")) {
                                int startPos = descInfo.IndexOf("Name:") + 6;
                                int endPos = descInfo.IndexOf("#");
                                charName = descInfo.Substring(startPos, endPos - startPos);
                                descInfo = descInfo.Substring(endPos + 1);
                            }
                            if (descInfo.Contains("/"))
                            {
                                try
                                {
                                    int startPos = 0;
                                    int endPos = descInfo.IndexOf("/") - 1;
                                    charAge = descInfo.Substring(startPos, endPos - startPos);
                                    descInfo = descInfo.Substring(endPos + 2);

                                    startPos = 1;
                                    endPos = descInfo.IndexOf("#");
                                    charGender = descInfo.Substring(startPos, endPos - startPos);
                                    descInfo = descInfo.Substring(descInfo.LastIndexOf("#") + 1);
                                }
                                catch (ArgumentOutOfRangeException)
                                {
                                    descInfo = "";
                                    charName = "";
                                    charGender = "";
                                    charAge = "";
                                }
                            }
                            newCharWindow.txtBox_menuDesc.Text = descInfo;
                            newCharWindow.txtBox_age.Text = charAge;
                            newCharWindow.txtBox_gender.Text = charGender;
                            newCharWindow.txtBox_fullName.Text = charName;
                        } 
                    }

                    newCharWindow.ShowDialog();
                }
            }
            //if (!System.IO.File.Exists(spritePath)) {
            //    MessageBox.Show("Choose new folder where sprites are located.", "Sprite folder not found!");
            //    spritePath = ChooseDirectory();
            //    System.Diagnostics.Debug.WriteLine(spritePath);
            ////}
            //System.IO.Path.GetFileName(spritePath);
        }

        private void EventBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EnemyBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MysteryBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
