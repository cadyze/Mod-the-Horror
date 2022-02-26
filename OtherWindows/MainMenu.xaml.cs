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
                    newCharWindow.ReadItoInformation(System.IO.File.ReadAllLines(potentialPath));
                    newCharWindow.ShowDialog();
                }
            }
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
                    newEventWindow.ReadItoInformation(System.IO.File.ReadAllLines(potentialPath));
                    newEventWindow.ShowDialog();
                }
            }
        }

        private void EnemyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isFileSettingCreate)
            {
                if (!txtBox_fileName.Text.Equals(""))
                {
                    //CREATING IN HERE
                    string potentialPath = FileManager.ChooseDirectory();
                    if (!potentialPath.Equals(""))
                    {
                        CreateEnemyWindow newEnemyWindow = new CreateEnemyWindow();
                        string eventDirectory = FileManager.CreateDirectory($"{txtBox_fileName.Text}", potentialPath);
                        newEnemyWindow.UpdateCurrentDirectory(eventDirectory);
                        string spriteDirectory = FileManager.CreateDirectory("enemy_art", eventDirectory);
                        newEnemyWindow.UpdateCurrentSpriteDirectory(spriteDirectory);
                        newEnemyWindow.UpdateCurrentItoName($"{txtBox_fileName.Text}.ito");
                        newEnemyWindow.ShowDialog();
                    }
                }
            }
            else
            {
                //EDITING IN HERE
                string potentialPath = FileManager.ChooseItoFile();
                if (!potentialPath.Equals(""))
                {
                    CreateEnemyWindow newEnemyWindow = new CreateEnemyWindow();
                    string? directory = System.IO.Path.GetDirectoryName(potentialPath);
                    if (directory != null) newEnemyWindow.UpdateCurrentDirectory(directory);
                    newEnemyWindow.UpdateCurrentItoName(System.IO.Path.GetFileName(potentialPath));
                    newEnemyWindow.ReadItoInformation(System.IO.File.ReadAllLines(potentialPath));
                    newEnemyWindow.ShowDialog();
                }
            }

        }
        private void MysteryBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
