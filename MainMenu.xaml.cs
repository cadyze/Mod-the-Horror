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
                txtBox_fileName.Visibility = Visibility.Hidden;
                label_fileName.Visibility = Visibility.Hidden;
            }
            else {
                fileSettingBtn.Content = "CREATE FILE";
                txtBox_fileName.Visibility = Visibility.Visible;
                label_fileName.Visibility = Visibility.Visible;
            }
            isFileSettingCreate = !isFileSettingCreate;
        }

        private void CharacterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isFileSettingCreate)
            {
                if (!txtBox_fileName.Text.Equals(""))
                {
                    //CREATING IN HERE
                    string potentialPath = FileManager.ChooseDirectory();
                    if (!potentialPath.Equals(""))
                    {
                        CreateCharacterWindow newCharWindow = new CreateCharacterWindow();
                        string characterDirectory = FileManager.CreateDirectory($"{txtBox_fileName.Text}", potentialPath);
                        newCharWindow.UpdateCurrentDirectory(characterDirectory);
                        string spriteDirectory = FileManager.CreateDirectory("char_sprites", characterDirectory);
                        newCharWindow.UpdateCurrentSpriteDirectory(spriteDirectory);
                        newCharWindow.UpdateCurrentItoName($"{txtBox_fileName.Text}.ito");
                        newCharWindow.ShowDialog();
                    }
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
                    newCharWindow.UpdateCurrentItoName(System.IO.Path.GetFileName(potentialPath));
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
                            //Menu description format: menu_desc="Name: _NAME_#_AGE_ / _GENDER_ #_DESCRIPTION_"
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
            if (isFileSettingCreate) {

                if (!txtBox_fileName.Text.Equals(""))
                {
                    //CREATING IN HERE
                    string potentialPath = FileManager.ChooseDirectory();
                    if (!potentialPath.Equals(""))
                    {
                        CreateEventWindow newEventWindow = new CreateEventWindow();
                        string eventDirectory = FileManager.CreateDirectory($"{txtBox_fileName.Text}", potentialPath);
                        newEventWindow.UpdateCurrentDirectory(eventDirectory);
                        string spriteDirectory = FileManager.CreateDirectory("event_art", eventDirectory);
                        newEventWindow.UpdateCurrentSpriteDirectory(spriteDirectory);
                        newEventWindow.UpdateCurrentItoName($"{txtBox_fileName.Text}.ito");
                        newEventWindow.ShowDialog();
                    }
                }
            }
            else
            {
                //EDITING IN HERE
                string potentialPath = FileManager.ChooseItoFile();
                if (!potentialPath.Equals(""))
                {
                    CreateEventWindow newEventWindow = new CreateEventWindow();
                    string? directory = System.IO.Path.GetDirectoryName(potentialPath);
                    if (directory != null) newEventWindow.UpdateCurrentDirectory(directory);
                    newEventWindow.UpdateCurrentItoName(System.IO.Path.GetFileName(potentialPath));
                    //Read the data from the file given.
                    var itoFileLines = System.IO.File.ReadAllLines(potentialPath);
                    foreach (string line in itoFileLines)
                    {
                        if (line.Contains("name=")) newEventWindow.txtBox_name.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("location=")) newEventWindow.txtBox_location.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("author=")) newEventWindow.txtBox_author.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("contact=")) newEventWindow.txtBox_contact.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("flavor=")) newEventWindow.txtBox_flavor.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("options=")) newEventWindow.txtBox_numOptions.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("image=")) newEventWindow.UpdateEventImagePath(ItoWriter.ExtractInfo(line));
                        if (line.Contains("about=")) newEventWindow.txtBox_description.Text = ItoWriter.ExtractInfo(line);

                        if (line.Contains("optiona=")) newEventWindow.txtBox_optionA.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("testa=")) newEventWindow.txtBox_testStatA.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("successa=")) newEventWindow.txtBox_successTextA.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("winprizea=")) newEventWindow.txtBox_successPrizeA.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("winnumbera=")) newEventWindow.txtBox_winAmtA.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("failurea=")) newEventWindow.txtBox_failTextA.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("failprizea=")) newEventWindow.txtBox_failPrizeA.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("failnumbera=")) newEventWindow.txtBox_failAmtA.Text = ItoWriter.ExtractInfo(line);

                        if (line.Contains("optionb=")) newEventWindow.txtBox_optionB.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("testb=")) newEventWindow.txtBox_testStatB.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("successb=")) newEventWindow.txtBox_successTextB.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("winprizeb=")) newEventWindow.txtBox_successPrizeB.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("winnumberb=")) newEventWindow.txtBox_winAmtB.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("failureb=")) newEventWindow.txtBox_failTextB.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("failprizeb=")) newEventWindow.txtBox_failPrizeB.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("failnumberb=")) newEventWindow.txtBox_failAmtB.Text = ItoWriter.ExtractInfo(line);

                        if (line.Contains("optionc=")) newEventWindow.txtBox_optionC.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("testc=")) newEventWindow.txtBox_testStatC.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("successc=")) newEventWindow.txtBox_successTextC.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("winprizec=")) newEventWindow.txtBox_successPrizeC.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("winnumberc=")) newEventWindow.txtBox_winAmtC.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("failurec=")) newEventWindow.txtBox_failTextC.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("failprizec=")) newEventWindow.txtBox_failPrizeC.Text = ItoWriter.ExtractInfo(line);
                        if (line.Contains("failnumberc=")) newEventWindow.txtBox_failAmtC.Text = ItoWriter.ExtractInfo(line);

                    }
                    newEventWindow.ShowDialog();
                }
            }
        }

        private void EnemyBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MysteryBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
