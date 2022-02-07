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
using System.Diagnostics;

namespace Mod_the_Horror
{
    /// <summary>
    /// Interaction logic for CreateCharacterWindow.xaml
    /// </summary>
    public partial class CreateCharacterWindow : Window
    {
        private static bool _hasDirtyData;
        private string spriteIconPath = "";
        private string spriteChibiPath = "";
        private string spriteBackPath = "";
        private string spriteHousePath = "";
        private string spritePortraitPath = "";
        public CreateCharacterWindow()
        {
            InitializeComponent();
        }

        private void OnTextBoxChange(object sender, TextChangedEventArgs e)
        {
            _hasDirtyData = true;
        }

        private void OnNumberBoxChanged(object sender, TextChangedEventArgs e) {
            _hasDirtyData = true;
            TextBox numberBox = (TextBox)sender;
            int numberTyped = 0;
            Int32.TryParse(numberBox.Text, out numberTyped);

            if (numberTyped > 99) numberBox.Text = "99";
            else if (numberTyped < 0) numberBox.Text = "0";
            else numberBox.Text = $"{numberTyped}";
        }

        private void OnSaveClicked(object sender, RoutedEventArgs e) {
            Image? icon = (img_spriteIcon.Source == null) ? img_spriteIcon : null;
            ItoWriter.WriteCustomCharacter(txtBox_name.Text, txtBox_author.Text, txtBox_contact.Text,
                int.Parse(txtBox_strength.Text), int.Parse(txtBox_dexterity.Text), int.Parse(txtBox_perception.Text),
                int.Parse(txtBox_charisma.Text), int.Parse(txtBox_knowledge.Text), int.Parse(txtBox_luck.Text),
                txtBox_aName.Text, txtBox_menuTag.Text, txtBox_menuDesc.Text, txtBox_aPerkPack.Text, txtBox_bPerkPack.Text,
                txtBox_name.Text, txtBox_gender.Text, int.Parse(txtBox_age.Text),
                (img_spriteIcon.Source != null) ? img_spriteIcon : null, 
                (img_spriteBack.Source != null) ? img_spriteBack : null,
                (img_spriteHouse.Source != null) ? img_spriteHouse : null,
                (img_spriteChibi.Source != null) ? img_spriteChibi : null,
                (img_spritePortrait.Source != null) ? img_spritePortrait : null
                );
        }


        private static string ImportSprite()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "sprite_icon";
            dlg.DefaultExt = ".png";
            dlg.Filter = "Images|*.png;*.jpg*.jpeg";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                _hasDirtyData = true;
                return dlg.FileName;
            }
            return "";
        }
        private void UpdateImage(Image imgToChange, string imgPath)
        {
            //Change the image of sprite to highlight the change.
            Uri uriSource = new Uri(imgPath);
            imgToChange.Source = new BitmapImage(uriSource);
        }

        private void btn_spriteIcon_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = ImportSprite();
            if (!potentialPath.Equals(""))
            {
                spriteIconPath = potentialPath;
                UpdateImage(img_spriteIcon, spriteIconPath);
            }
        }

        private void btn_spriteBack_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = ImportSprite();
            if (!potentialPath.Equals(""))
            {
                spriteBackPath = potentialPath;
                UpdateImage(img_spriteBack, spriteBackPath);
            }

        }

        private void btn_spriteHouse_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = ImportSprite();
            if (!potentialPath.Equals(""))
            {
                spriteHousePath = potentialPath;
                UpdateImage(img_spriteHouse, spriteHousePath);
            }
        }

        private void btn_spritePortrait_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = ImportSprite();
            if (!potentialPath.Equals(""))
            {
                spritePortraitPath = potentialPath;
                UpdateImage(img_spritePortrait, spritePortraitPath);
            }
        }

        private void btn_spriteChibi_Click(object sender, RoutedEventArgs e)
        {
            string potentialPath = ImportSprite();
            if (!potentialPath.Equals(""))
            {
                spriteChibiPath = potentialPath;
                UpdateImage(img_spriteChibi, spriteChibiPath);
            }

        }
    }
}
