using Mod_the_Horror.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Mod_the_Horror.OtherWindows
{
    /// <summary>
    /// Interaction logic for CreateProject.xaml
    /// </summary>
    public partial class CreateProject : Window
    {
        Mod? currentModSelected;
        string projectDirectory = "";

        public void InitializeModList(string rootDirectory) {
            projectDirectory = rootDirectory;
            List<string> directories = Directory.GetDirectories(projectDirectory).ToList<string>();
            directories.Add(rootDirectory);

            foreach (string directory in directories) {
                foreach (string mod in Directory.GetFiles(directory, "*.ito"))
                {
                    ModType modType = ItoWriter.ReadItoType(mod);
                    if (modType != ModType.ERROR) mods.Add(new Mod(mod, modType));
                }
            }
        }

        public void InitializeProject(string path) {
            projectDirectory = path;
        }


        public CreateProject()
        {
            InitializeComponent();
            ModList.ItemsSource = Mods;
        }

        public ObservableCollection<Mod> mods = new ObservableCollection<Mod>();
        public ObservableCollection<Mod> Mods { get { return mods; } }

        private void ModPreviewClick(object sender, RoutedEventArgs e)
        {
            Button? buttonSender = sender as Button;
            if (buttonSender != null)
            {
                Mod mod = (Mod)buttonSender.DataContext;
                lbl_description.Content = mod.previewDescription;
                lbl_name.Content = mod.previewName;
                currentModSelected = mod;

                grid_preview.Visibility = Visibility.Visible;
            }
        }

        private void NewMod_Click(object sender, RoutedEventArgs e)
        {
            CreatorWindows.CreateNewMod createNewModWindow = new CreatorWindows.CreateNewMod();
            createNewModWindow.ChosenDirectory = projectDirectory;
            createNewModWindow.ShowDialog();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (currentModSelected != null)
            {
                switch (currentModSelected.modType)
                {
                    case ModType.CHARACTER:
                        CreateCharacterWindow characterWindow = new CreateCharacterWindow();
                        characterWindow.LoadMod(currentModSelected.pathToMod);
                        characterWindow.ShowDialog();
                        break;
                    case ModType.ENEMY:
                        CreateEnemyWindow enemyWindow = new CreateEnemyWindow();
                        enemyWindow.LoadMod(currentModSelected.pathToMod);
                        enemyWindow.ShowDialog();
                        break;
                    case ModType.EVENT:
                        CreateEventWindow eventWindow = new CreateEventWindow();
                        eventWindow.LoadMod(currentModSelected.pathToMod);
                        eventWindow.ShowDialog();
                        break;
                    case ModType.MYSTERY:

                        //To be impemented
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
