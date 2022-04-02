using Mod_the_Horror.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for InvestigationTurnEditor.xaml
    /// </summary>
    public partial class InvestigationTurnEditor : Window
    {
        public InvestigationTurn? investigationTurn = null;
        public InvestigationTurnEditor()
        {
            InitializeComponent();
            UIManager.InitializeComboBox(comboBox_location, @"GameInformation\EnemyLocations.txt");
        }

        public void SetInvestigationTurn(InvestigationTurn turn) {
            investigationTurn = turn;
            lbl_num.Content = $"INVESTIGATION {investigationTurn.longProgressNum}";
            lbl_path.Content = investigationTurn.forcedEvent;
            txtBox_precedingText.Text = investigationTurn.precedingText;
            UIManager.UpdateComboBox(comboBox_location, investigationTurn.location);
            comboBox_location.Items.Add("ending");
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
                        investigationTurn.forcedEvent = potentialPath;
                        lbl_path.Content = potentialPath;
                        break;
                    case ModType.ENEMY:
                        investigationTurn.forcedEvent = potentialPath;
                        lbl_path.Content = potentialPath;
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
                investigationTurn.precedingText = txtBox_precedingText.Text;
                investigationTurn.location = (string)comboBox_location.SelectedItem;
            }
            base.OnClosing(e);
        }
    }
}
