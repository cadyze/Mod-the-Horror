using Mod_the_Horror.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for InvestigationTurnEditor.xaml
    /// </summary>
    public partial class InvestigationTurnEditor : Window
    {
        public InvestigationTurn? investigationTurn = null;
        private string rootDirectory = "";
        public InvestigationTurnEditor()
        {
            InitializeComponent();
            UIManager.InitializeComboBox(comboBox_location, @"GameInformation\EnemyLocations.txt");
        }

        public void SetInvestigationTurn(InvestigationTurn turn, string rootDirectory) {
            investigationTurn = turn;
            lbl_num.Content = $"INVESTIGATION {investigationTurn.longProgressNum}";
            lbl_path.Content = investigationTurn.forcedEvent;
            txtBox_precedingText.Text = investigationTurn.precedingText.Replace('#', '\n');
            UIManager.UpdateComboBox(comboBox_location, investigationTurn.location);
            comboBox_location.Items.Add("ending");
            this.rootDirectory = rootDirectory;
        }

        private void ImportEventEnemy_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = FileManager.ChooseItoFile();
            if (!potentialPath.Equals("") && investigationTurn != null)
            {
                ModType loadedModType = ItoWriter.ReadItoType(potentialPath);
                switch (loadedModType)
                {
                    case ModType.EVENT:
                        goto case ModType.ENEMY;
                    case ModType.ENEMY:
                        string relativePath = System.IO.Path.GetRelativePath(rootDirectory, potentialPath);
                        investigationTurn.forcedEvent = relativePath;
                        lbl_path.Content = relativePath;
                        break;
                    default:
                        //Show error Message
                        break;
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (investigationTurn != null)
            {
                investigationTurn.precedingText = Regex.Replace(txtBox_precedingText.Text, @"\r\n?|\n", "#"); ;
                investigationTurn.location = (string)comboBox_location.SelectedItem;
            }
            base.OnClosing(e);
        }
    }
}
