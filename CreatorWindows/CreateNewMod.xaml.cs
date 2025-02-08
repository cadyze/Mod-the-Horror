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

namespace Mod_the_Horror.CreatorWindows
{
    /// <summary>
    /// Interaction logic for CreateNewMod.xaml
    /// </summary>
    public partial class CreateNewMod : Window
    {
        public string chosenEventDirectory = "";
        public string chosenEnemyDirectory = "";
        public string chosenCharDirectory = "";
        public string chosenMysteryDirectory = "";
        public bool createNewFolder = false;

        public CreateNewMod()
        {
            InitializeComponent();
        }

        private void NewChar_Click(object sender, RoutedEventArgs e)
        {
            string modName = txtBox_modName.Text;
            modName = modName.Replace(' ', '_');

            if (modName.Length != 0)
            {
                if (!chosenCharDirectory.Equals(""))
                {
                    LoadCharWindow(modName, chosenCharDirectory);
                }
                else
                {
                    string potentialPath = FileManager.ChooseDirectory();
                    if (!potentialPath.Equals(""))
                    {
                        LoadCharWindow(modName, potentialPath);
                    }
                }
            }
        }

        private void LoadCharWindow(string modName, string potentialPath)
        {
            if (createNewFolder) potentialPath = FileManager.CreateDirectory(modName, potentialPath);
            CreateCharacterWindow newCreateWindow = new CreateCharacterWindow();
            newCreateWindow.InitializeMod(modName, potentialPath);
            Close();
            newCreateWindow.ShowDialog();
        }

        private void NewEvent_Click(object sender, RoutedEventArgs e)
        {
            string modName = txtBox_modName.Text;
            modName = modName.Replace(' ', '_');

            if (modName.Length != 0)
            {
                if (!chosenEventDirectory.Equals(""))
                {
                    LoadEventWindow(modName, chosenEventDirectory);
                }
                else
                {
                    string potentialPath = FileManager.ChooseDirectory();
                    if (!potentialPath.Equals(""))
                    {
                        LoadEventWindow(modName, potentialPath);
                    }
                }
            }
        }

        private void LoadEventWindow(string modName, string potentialPath)
        {
            if (createNewFolder) potentialPath =  FileManager.CreateDirectory(modName, potentialPath);
            CreateEventWindow newCreateWindow = new CreateEventWindow();
            newCreateWindow.InitializeMod(modName, potentialPath);
            Close();
            newCreateWindow.ShowDialog();
        }

        private void NewEnemy_Click(object sender, RoutedEventArgs e)
        {
            string modName = txtBox_modName.Text;
            modName = modName.Replace(' ', '_');

            if (modName.Length != 0)
            {
                if (!chosenEnemyDirectory.Equals(""))
                {
                    LoadEnemyWindow(modName, chosenEnemyDirectory);
                }
                else
                {
                    string potentialPath = FileManager.ChooseDirectory();
                    if (!potentialPath.Equals(""))
                    {
                        LoadEnemyWindow(modName, potentialPath);
                    }
                }
            }
        }

        private void LoadEnemyWindow(string modName, string potentialPath)
        {
            if (createNewFolder) potentialPath = FileManager.CreateDirectory(modName, potentialPath);
            CreateEnemyWindow newCreateWindow = new CreateEnemyWindow();
            newCreateWindow.InitializeMod(modName, potentialPath);
            Close();
            newCreateWindow.ShowDialog();
        }

        private void NewMystery_Click(object sender, RoutedEventArgs e)
        {
            string modName = txtBox_modName.Text;
            modName = modName.Replace(' ', '_');

            if (modName.Length != 0)
            {
                if (!chosenMysteryDirectory.Equals(""))
                {
                    LoadMysteryWindow(modName, chosenMysteryDirectory);
                }
                else
                {
                    string potentialPath = FileManager.ChooseDirectory();
                    if (!potentialPath.Equals(""))
                    {
                        LoadMysteryWindow(modName, potentialPath);
                    }
                }
            }
        }

        private void LoadMysteryWindow(string modName, string potentialPath)
        {
            if (createNewFolder) potentialPath = FileManager.CreateDirectory(modName, potentialPath);
            CreateMysteryWindow newCreateWindow = new CreateMysteryWindow();
            newCreateWindow.InitializeMod(modName, potentialPath);
            Close();
            newCreateWindow.ShowDialog();
        }
    }
}
