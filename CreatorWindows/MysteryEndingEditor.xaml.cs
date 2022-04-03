using Mod_the_Horror.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            txtBox_pageA.Text = endingData.pageA;
            txtBox_pageB.Text = endingData.pageB;
            txtBox_pageC.Text = endingData.pageC;
            txtBox_pageD.Text = endingData.pageD;
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
                endingData.pageA = txtBox_pageA.Text;
                endingData.pageB = txtBox_pageB.Text;
                endingData.pageC = txtBox_pageC.Text;
                endingData.pageD = txtBox_pageD.Text;
                endingData.endingTitle = txtBox_endingTitle.Text;
            }
            base.OnClosing(e);
        }
    }
}
