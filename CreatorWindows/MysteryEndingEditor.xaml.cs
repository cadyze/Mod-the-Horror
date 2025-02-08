using Mod_the_Horror.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    /// Interaction logic for MysteryEndingEditor.xaml
    /// </summary>
    public partial class MysteryEndingEditor : Window
    {
        MysteryEnding? endingData = null;

        public MysteryEndingEditor()
        {
            InitializeComponent();
        }

        public void SetEndingData(MysteryEnding endingData) {
            this.endingData = endingData;
            lbl_ending.Content = $"ENDING {endingData.EndingLetter}";
            txtBox_endingTitle.Text = endingData.endingTitle;
            txtBox_pageA.Text = endingData.pageA.Replace('#', '\n');
            txtBox_pageB.Text = endingData.pageB.Replace('#', '\n');
            txtBox_pageC.Text = endingData.pageC.Replace('#', '\n');
            txtBox_pageD.Text = endingData.pageD.Replace('#', '\n');
            FileManager.UpdateImage(img_ending, endingData.pathToImage);
        }

        private void ImportEndArt_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = FileManager.ImportSprite();
            if (!potentialPath.Equals("") && endingData != null) {
                endingData.pathToImage = potentialPath;
                FileManager.UpdateImage(img_ending, endingData.pathToImage);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (endingData != null) {
                endingData.pageA = Regex.Replace(txtBox_pageA.Text, @"\r\n?|\n", "#");
                endingData.pageB = Regex.Replace(txtBox_pageB.Text, @"\r\n?|\n", "#");
                endingData.pageC = Regex.Replace(txtBox_pageC.Text, @"\r\n?|\n", "#");
                endingData.pageD = Regex.Replace(txtBox_pageD.Text, @"\r\n?|\n", "#");
                endingData.endingTitle = txtBox_endingTitle.Text;
            }
            base.OnClosing(e);
        }
        private void CapLock_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            if (txtBox != null && txtBox.Text != null) txtBox.Text = txtBox.Text.ToString().ToUpper();
        }
    }
}
