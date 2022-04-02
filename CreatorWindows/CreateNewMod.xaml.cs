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
        private string chosenDirectory = "";
        public CreateNewMod()
        {
            InitializeComponent();
        }

        public string ChosenDirectory {
            get { return chosenDirectory; }
            set { chosenDirectory = value; }
        }

        private void NewChar_Click(object sender, RoutedEventArgs e)
        {
            string modName = txtBox_modName.Text;
            modName = modName.Replace(' ', '_');

            if (modName.Length != 0)
            {
                if (!chosenDirectory.Equals(""))
                {
                    CreateCharacterWindow newCreateWindow = new CreateCharacterWindow();
                    newCreateWindow.InitializeMod(modName, chosenDirectory);
                    Close();
                    newCreateWindow.ShowDialog();
                }
                else
                {
                    string potentialPath = FileManager.ChooseDirectory();
                    if (!potentialPath.Equals(""))
                    {
                        CreateCharacterWindow newCreateWindow = new CreateCharacterWindow();
                        newCreateWindow.InitializeMod(modName, potentialPath);
                        Close();
                        newCreateWindow.ShowDialog();
                    }
                }
            }
        }
        private void NewEvent_Click(object sender, RoutedEventArgs e)
        {
            string modName = txtBox_modName.Text;
            modName = modName.Replace(' ', '_');

            if (modName.Length != 0)
            {
                if (!chosenDirectory.Equals(""))
                {
                    CreateEventWindow newCreateWindow = new CreateEventWindow();
                    newCreateWindow.InitializeMod(modName, chosenDirectory);
                    Close();
                    newCreateWindow.ShowDialog();
                }
                else
                {
                    string potentialPath = FileManager.ChooseDirectory();
                    if (!potentialPath.Equals(""))
                    {
                        CreateEventWindow newCreateWindow = new CreateEventWindow();
                        newCreateWindow.InitializeMod(modName, potentialPath);
                        Close();
                        newCreateWindow.ShowDialog();
                    }
                }
            }
        }
        private void NewEnemy_Click(object sender, RoutedEventArgs e)
        {
            string modName = txtBox_modName.Text;
            modName = modName.Replace(' ', '_');

            if (modName.Length != 0)
            {
                if (!chosenDirectory.Equals(""))
                {
                    CreateEnemyWindow newCreateWindow = new CreateEnemyWindow();
                    newCreateWindow.InitializeMod(modName, chosenDirectory);
                    Close();
                    newCreateWindow.ShowDialog();
                }
                else
                {
                    string potentialPath = FileManager.ChooseDirectory();
                    if (!potentialPath.Equals(""))
                    {
                        CreateEnemyWindow newCreateWindow = new CreateEnemyWindow();
                        newCreateWindow.InitializeMod(modName, potentialPath);
                        Close();
                        newCreateWindow.ShowDialog();
                    }
                }
            }
        }
        private void NewMystery_Click(object sender, RoutedEventArgs e)
        {
            string modName = txtBox_modName.Text;
            modName = modName.Replace(' ', '_');

            if (modName.Length != 0)
            {
                if (!chosenDirectory.Equals(""))
                {
                    CreateMysteryWindow newCreateWindow = new CreateMysteryWindow();
                    newCreateWindow.InitializeMod(modName, chosenDirectory);
                    Close();
                    newCreateWindow.ShowDialog();
                }
                else
                {
                    string potentialPath = FileManager.ChooseDirectory();
                    if (!potentialPath.Equals(""))
                    {
                        CreateMysteryWindow newCreateWindow = new CreateMysteryWindow();
                        newCreateWindow.InitializeMod(modName, potentialPath);
                        Close();
                        newCreateWindow.ShowDialog();
                    }
                }
            }
        }
    }
}
