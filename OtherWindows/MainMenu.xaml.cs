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
                        newCharWindow.InitializeMod(txtBox_fileName.Text, potentialPath);
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
                    newCharWindow.LoadMod(potentialPath);
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
                        newEventWindow.InitializeMod(txtBox_fileName.Text, potentialPath);
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
                    newEventWindow.LoadMod(potentialPath);
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
                        newEnemyWindow.InitializeMod(txtBox_fileName.Text, potentialPath);
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
                    newEnemyWindow.LoadMod(potentialPath);
                    newEnemyWindow.ShowDialog();
                }
            }

        }
        private void MysteryBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditProject_Click(object sender, RoutedEventArgs e)
        {
            //EDITING IN HERE
            string potentialPath = FileManager.ChooseDirectory();
            if (!potentialPath.Equals(""))
            {
                OtherWindows.CreateProject createProjectWindow = new OtherWindows.CreateProject();
                createProjectWindow.InitializeModList(potentialPath);
                Trace.WriteLine(potentialPath);
                createProjectWindow.ShowDialog();
            }
        }

        private void CreateProject_Click(object sender, RoutedEventArgs e)
        {
            string projectName = txtBox_projectName.Text;
            projectName.Replace(' ', '_');
            if (projectName.Length != 0) {
                string potentialPath = FileManager.ChooseDirectory();
                if (!potentialPath.Equals("")) {

                    string projectPath = FileManager.CreateDirectory(projectName, potentialPath);

                    OtherWindows.CreateProject createProjectWindow = new OtherWindows.CreateProject();
                    createProjectWindow.InitializeProject(projectPath);
                    createProjectWindow.ShowDialog();
                }
            }
        }
    }
}
