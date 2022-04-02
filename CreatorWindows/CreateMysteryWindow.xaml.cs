using Mod_the_Horror.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.IO;

namespace Mod_the_Horror.CreatorWindows
{
    /// <summary>
    /// Interaction logic for CreateMysteryWindow.xaml
    /// </summary>
    public partial class CreateMysteryWindow : Window
    {
        private string modLocation = "";
        private string modName = "";
        private string iconPath = "";
        private string previewPath = "";
        private string backgroundPath = "";
        public CreateMysteryWindow()
        {
            InitializeComponent();

            string[] possibleTurns = { "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT" };
            foreach (string turn in possibleTurns) investigationTurns.Add(new InvestigationTurn(turn));
            InvestigationList.ItemsSource = InvestigationTurns;

            char[] possibleEndings = { 'A', 'B', 'C' };
            foreach (char possibleEnd in possibleEndings) endings.Add(new MysteryEnding(possibleEnd));
            EndingList.ItemsSource = Endings;
        }

        public void InitializeMod(string modName, string path) {
            modLocation = FileManager.CreateDirectory(modName, path);
            this.modName = $"{modName}.ito";
        }

        public void LoadMod(string path) {
            modLocation = System.IO.Path.GetDirectoryName(path);
            modName = System.IO.Path.GetFileName(path);
            ReadItoInformation(System.IO.File.ReadAllText(path));
        }

        void ReadItoInformation(string information) {
            Dictionary<string, string> sections = new Dictionary<string, string>();
            string informationRemaining = information;
            while (informationRemaining.Contains('[')){
                int startPos = informationRemaining.IndexOf('[');
                int headerEnd = informationRemaining.IndexOf(']');
                string headerTitle = informationRemaining.Substring(startPos + 1, headerEnd - startPos - 1);

                int endPos = informationRemaining.IndexOf('[', headerEnd);
                if (endPos == -1)
                {
                    sections.Add(headerTitle, informationRemaining);
                    informationRemaining = "";
                }
                else
                {
                    sections.Add(headerTitle, informationRemaining.Substring(startPos, endPos - startPos));
                    informationRemaining = informationRemaining.Substring(endPos);
                }

            }

            //MYSTERY HEADER
            if (sections.ContainsKey("mystery"))
            {
                string[] sectionInfo = sections["mystery"].Split('\n');
                foreach (string line in sectionInfo)
                {
                    if (line.Contains("name=")) txtBox_name.Text = ItoWriter.ExtractInfo(line);
                    if (line.Contains("author=")) txtBox_author.Text = ItoWriter.ExtractInfo(line);
                    if (line.Contains("art=")) UpdateIcon(ItoWriter.ExtractInfo(line));
                    if (line.Contains("description=")) txtBox_description.Text = ItoWriter.ExtractInfo(line);
                }
            }

            //INTRODUCTION HEADER
            if (sections.ContainsKey("intro"))
            {
                string[] sectionInfo = sections["intro"].Split('\n');
                foreach (string line in sectionInfo)
                {
                    if (line.Contains("art=")) UpdatePreviewArt(ItoWriter.ExtractInfo(line));
                    if (line.Contains("text_one=")) txtBox_intro1.Text = ItoWriter.ExtractInfo(line);
                    if (line.Contains("text_two=")) txtBox_intro2.Text = ItoWriter.ExtractInfo(line);
                    if (line.Contains("text_three=")) txtBox_intro3.Text = ItoWriter.ExtractInfo(line);
                }
            }

            //PROGRESS HEADER
            if (sections.ContainsKey("progress"))
            {
                string[] sectionInfo = sections["progress"].Split('\n');
                Trace.WriteLine("progress");
                foreach (string line in sectionInfo)
                {
                    if (line.Contains("location=")) txtBox_endLocation.Text = ItoWriter.ExtractInfo(line);
                    if (line.Contains("background=")) UpdateBackground(ItoWriter.ExtractInfo(line));
                    if (line.Contains("one_loc=")) investigationTurns[0].location = ItoWriter.ExtractInfo(line);
                    if (line.Contains("one_txt=")) investigationTurns[0].precedingText = ItoWriter.ExtractInfo(line);
                    if (line.Contains("one_frc=")) investigationTurns[0].forcedEvent = ItoWriter.ExtractInfo(line);
                    if (line.Contains("two_loc=")) investigationTurns[1].location = ItoWriter.ExtractInfo(line);
                    if (line.Contains("two_txt=")) investigationTurns[1].precedingText = ItoWriter.ExtractInfo(line);
                    if (line.Contains("two_frc=")) investigationTurns[1].forcedEvent = ItoWriter.ExtractInfo(line);
                    if (line.Contains("thr_loc=")) investigationTurns[2].location = ItoWriter.ExtractInfo(line);
                    if (line.Contains("thr_txt=")) investigationTurns[2].precedingText = ItoWriter.ExtractInfo(line);
                    if (line.Contains("thr_frc=")) investigationTurns[2].forcedEvent = ItoWriter.ExtractInfo(line);
                    if (line.Contains("fou_loc=")) investigationTurns[3].location = ItoWriter.ExtractInfo(line);
                    if (line.Contains("fou_txt=")) investigationTurns[3].precedingText = ItoWriter.ExtractInfo(line);
                    if (line.Contains("fou_frc=")) investigationTurns[3].forcedEvent = ItoWriter.ExtractInfo(line);
                    if (line.Contains("fiv_loc=")) investigationTurns[4].location = ItoWriter.ExtractInfo(line);
                    if (line.Contains("fiv_txt=")) investigationTurns[4].precedingText = ItoWriter.ExtractInfo(line);
                    if (line.Contains("fiv_frc=")) investigationTurns[4].forcedEvent = ItoWriter.ExtractInfo(line);
                    if (line.Contains("six_loc=")) investigationTurns[5].location = ItoWriter.ExtractInfo(line);
                    if (line.Contains("six_txt=")) investigationTurns[5].precedingText = ItoWriter.ExtractInfo(line);
                    if (line.Contains("six_frc=")) investigationTurns[5].forcedEvent = ItoWriter.ExtractInfo(line);
                    if (line.Contains("sev_loc=")) investigationTurns[6].location = ItoWriter.ExtractInfo(line);
                    if (line.Contains("sev_txt=")) investigationTurns[6].precedingText = ItoWriter.ExtractInfo(line);
                    if (line.Contains("sev_frc=")) investigationTurns[6].forcedEvent = ItoWriter.ExtractInfo(line);
                    if (line.Contains("eig_loc=")) investigationTurns[7].location = ItoWriter.ExtractInfo(line);
                    if (line.Contains("eig_txt=")) investigationTurns[7].precedingText = ItoWriter.ExtractInfo(line);
                    if (line.Contains("eig_frc=")) investigationTurns[7].forcedEvent = ItoWriter.ExtractInfo(line);
                }
            }

            //ENDING A
            if (sections.ContainsKey("ending_a"))
            {
                string[] sectionInfo = sections["ending_a"].Split('\n');
                Trace.WriteLine("a");
                foreach (string line in sectionInfo)
                {
                    if (line.Contains("end_title=")) endings[0].endingTitle = ItoWriter.ExtractInfo(line);
                    if (line.Contains("end_img="))
                    {
                        string extraction = ItoWriter.ExtractInfo(line);
                        if(!extraction.Equals("")) endings[0].pathToImage = System.IO.Path.Combine(modLocation, extraction);
                    }
                    if (line.Contains("end_txta=")) endings[0].pageA = ItoWriter.ExtractInfo(line);
                    if (line.Contains("end_txtb=")) endings[0].pageB = ItoWriter.ExtractInfo(line);
                    if (line.Contains("end_txtc=")) endings[0].pageC = ItoWriter.ExtractInfo(line);
                    if (line.Contains("end_txtd=")) endings[0].pageD = ItoWriter.ExtractInfo(line);
                }
            }

            //ENDING B
            if (sections.ContainsKey("ending_b"))
            {
                string[] sectionInfo = sections["ending_b"].Split('\n');
                Trace.WriteLine("b");
                foreach (string line in sectionInfo)
                {
                    Trace.WriteLine(line);
                    if (line.Contains("end_title=")) endings[1].endingTitle = ItoWriter.ExtractInfo(line);
                    if (line.Contains("end_img="))
                    {
                        string extraction = ItoWriter.ExtractInfo(line);
                        if (!extraction.Equals("")) endings[1].pathToImage = System.IO.Path.Combine(modLocation, extraction);
                    }
                    if (line.Contains("end_txta=")) endings[1].pageA = ItoWriter.ExtractInfo(line);
                    if (line.Contains("end_txtb=")) endings[1].pageB = ItoWriter.ExtractInfo(line);
                    if (line.Contains("end_txtc=")) endings[1].pageC = ItoWriter.ExtractInfo(line);
                    if (line.Contains("end_txtd=")) endings[1].pageD = ItoWriter.ExtractInfo(line);
                }
            }

            //ENDING C
            if (sections.ContainsKey("ending_c"))
            {
                string[] sectionInfo = sections["ending_c"].Split('\n');
                Trace.WriteLine("c");
                foreach (string line in sectionInfo)
                {
                    if (line.Contains("end_title=")) endings[2].endingTitle = ItoWriter.ExtractInfo(line);
                    if (line.Contains("end_img="))
                    {
                        string extraction = ItoWriter.ExtractInfo(line);
                        if (!extraction.Equals("")) endings[2].pathToImage = System.IO.Path.Combine(modLocation, extraction);
                    }
                    if (line.Contains("end_txta=")) endings[2].pageA = ItoWriter.ExtractInfo(line);
                    if (line.Contains("end_txtb=")) endings[2].pageB = ItoWriter.ExtractInfo(line);
                    if (line.Contains("end_txtc=")) endings[2].pageC = ItoWriter.ExtractInfo(line);
                    if (line.Contains("end_txtd=")) endings[2].pageD = ItoWriter.ExtractInfo(line);
                }
            }

            //OVERALL ENDING
            if (sections.ContainsKey("big_ending"))
            {
                string[] sectionInfo = sections["intro"].Split('\n');
                foreach (string line in sectionInfo)
                {
                    if (line.Contains("end_txt=")) txtBox_endSummary.Text = ItoWriter.ExtractInfo(line);
                }
            }
        }

        void UpdateIcon(string relativePath) 
        {
            if (!relativePath.Equals("")) iconPath = System.IO.Path.Combine(modLocation, relativePath);
            FileManager.UpdateImage(img_icon, iconPath);
        }

        void UpdatePreviewArt(string relativePath)
        {
            if (!relativePath.Equals("")) previewPath = System.IO.Path.Combine(modLocation, relativePath);
            FileManager.UpdateImage(img_mysteryPreview, previewPath);
        }

        void UpdateBackground(string relativePath)
        {
            if (!relativePath.Equals("")) backgroundPath = System.IO.Path.Combine(modLocation, relativePath);
            FileManager.UpdateImage(img_background, backgroundPath);
        }

        public ObservableCollection<InvestigationTurn> investigationTurns = new ObservableCollection<InvestigationTurn>();

        public ObservableCollection<InvestigationTurn> InvestigationTurns {
            get { return investigationTurns; }
        }

        public ObservableCollection<MysteryEnding> endings = new ObservableCollection<MysteryEnding>();

        public ObservableCollection<MysteryEnding> Endings
        {
            get {
                return endings;
            }
        }

        private void InvestigationTurn_Click(object sender, RoutedEventArgs e)
        {
            Button? buttonSender = sender as Button;
            if (buttonSender != null) {
                InvestigationTurn investigationTurn = (InvestigationTurn)buttonSender.DataContext;
                InvestigationTurnEditor editor = new InvestigationTurnEditor();
                editor.SetInvestigationTurn(investigationTurn);
                editor.ShowDialog();
            }
        }

        private void MysteryEnding_Click(object sender, RoutedEventArgs e)
        {
            Button? buttonSender = sender as Button;
            if (buttonSender != null)
            {
                MysteryEnding endData = (MysteryEnding)buttonSender.DataContext;
                Trace.WriteLine("FUFUFUFUUFUFUF " + endData.pathToImage);
                MysteryEndingEditor editor = new MysteryEndingEditor();
                editor.SetEndingData(endData);
                editor.ShowDialog();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e) {
            string[] intro = { txtBox_intro1.Text, txtBox_intro2.Text, txtBox_intro3.Text };

            string spriteDirectory = System.IO.Path.Combine(modLocation, "mystery_sprites");
            if (!Directory.Exists(spriteDirectory)){
                FileManager.CreateDirectory("mystery_sprites", modLocation);
            }

            iconPath = iconPath.Equals("") ? "" : System.IO.Path.Combine(spriteDirectory, System.IO.Path.GetFileName(iconPath));
            previewPath = previewPath.Equals("") ? "" : System.IO.Path.Combine(spriteDirectory, System.IO.Path.GetFileName(previewPath));
            backgroundPath = backgroundPath.Equals("") ? "" : System.IO.Path.Combine(spriteDirectory, System.IO.Path.GetFileName(backgroundPath));


            FileManager.SaveImage(img_icon, iconPath);
            FileManager.SaveImage(img_mysteryPreview, previewPath);
            FileManager.SaveImage(img_background, backgroundPath);

            //Manage images for all endings
            foreach (MysteryEnding ending in endings) {
                string originalEndSource = ending.pathToImage;
                ending.pathToImage = ending.pathToImage.Equals("") ? "" : System.IO.Path.Combine(spriteDirectory, System.IO.Path.GetFileName(ending.pathToImage));
                if(!ending.pathToImage.Equals("")) FileManager.SaveImage(originalEndSource, ending.pathToImage);
            }



            ItoWriter.WriteMystery(modLocation, modName, txtBox_name.Text, txtBox_author.Text, txtBox_description.Text,
                txtBox_endLocation.Text, txtBox_endSummary.Text, intro, Endings.ToArray(), InvestigationTurns.ToArray(),
                iconPath, previewPath, backgroundPath);
        }

        private void IconImport_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = FileManager.ImportSprite();
            if (!potentialPath.Equals(""))
            {
                iconPath = potentialPath;
                FileManager.UpdateImage(img_icon, iconPath);
            }
        }

        private void PreviewImport_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = FileManager.ImportSprite();
            if (!potentialPath.Equals(""))
            {
                previewPath = potentialPath;
                FileManager.UpdateImage(img_mysteryPreview, previewPath);
            }
        }

        private void BackgroundImport_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = FileManager.ImportSprite();
            if (!potentialPath.Equals(""))
            {
                backgroundPath = potentialPath;
                FileManager.UpdateImage(img_background, backgroundPath);
            }
        }
    }
}
