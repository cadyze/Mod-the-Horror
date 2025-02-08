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
using Mod_the_Horror.CreatorWindows;

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

        private void EditMod_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = FileManager.ChooseItoFile();
            if (!potentialPath.Equals(""))
            {
                ModType loadedModType = ItoWriter.ReadItoType(potentialPath);
                switch (loadedModType) {
                    case ModType.CHARACTER:
                        CreateCharacterWindow characterWindow = new CreateCharacterWindow();
                        characterWindow.LoadMod(potentialPath);
                        characterWindow.ShowDialog();
                        break;
                    case ModType.EVENT:
                        CreateEventWindow eventWindow = new CreateEventWindow();
                        eventWindow.LoadMod(potentialPath);
                        eventWindow.ShowDialog();
                        break;
                    case ModType.ENEMY:
                        CreateEnemyWindow enemyWindow = new CreateEnemyWindow();
                        enemyWindow.LoadMod(potentialPath);
                        enemyWindow.ShowDialog();
                        break;
                    case ModType.MYSTERY:
                        CreateMysteryWindow mysteryWindow = new CreateMysteryWindow();
                        mysteryWindow.LoadMod(potentialPath);
                        mysteryWindow.ShowDialog();
                        break;
                    default:
                        break;
                }
            }
        }

        private void CreateMod_Click(object sender, RoutedEventArgs e)
        {
            CreatorWindows.CreateNewMod newModWindow = new CreatorWindows.CreateNewMod();
            newModWindow.ShowDialog();
        }

        private void EditProject_Click(object sender, RoutedEventArgs e)
        {
            //EDITING IN HERE
            string potentialPath = FileManager.ChooseDirectory();
            if (!potentialPath.Equals(""))
            {
                OtherWindows.CreateProject createProjectWindow = new OtherWindows.CreateProject();
                createProjectWindow.InitializeModList(potentialPath);
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
