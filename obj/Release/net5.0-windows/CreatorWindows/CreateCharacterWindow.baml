   M S B A M L   `   `   `  ���� I  EMod the Horror, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Mod_the_HorrorMod_the_Horror  *    $Mod_the_Horror.CreateCharacterWindow   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:Mod_the_Horror       � Title$  CreateCharacterWindow��$	��720��$
��1280��
���   �.��� $��10q� � RowDefinitions �� $��60 ��� $��8* ��� $��6* �.+�� $��0�� � ColumnDefinitions �� $��* ��� $��* ��� $��* �� $��1�� �� $��2* ��� $��3* �.+�� $��0��$��10q� �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��2* � �� $��3* ��� $��4* �.+��� $��0��$��0��$��
Full Name:��   header2#��[  $
��Left=�$��10,0,0,0q�$��Center=�$
��Auto��$
��Auto���� $��1��$��0��$��	Nickname:��#��[  $
��Left=�$��10,0,0,0q�$��Center=�$
��Auto��$
��Auto���� $��2��$��0��$��Gender:��#��[  $
��Left=�$��10,0,0,0q�$��Center=�$
��Auto��$
��Auto���� $��3��$��0��$
��Age:��#��[  $
��Left=�$��10,0,0,0q�$��Center=�$
��Auto��$
��Auto���� $��4��$��0��$��Occupation:��#��[  $
��Left=�$��10,0,0,0q�$��Center=�$
��Auto��$
��Auto���� $��5��$��0��$��Description:��#��[  $
��Left=�$��10,0,0,0q�$	��Top=�$
��Auto��$
��Auto���� -    ��Name$ txtBox_fullName��$��0��$��1��  rightAlignedTxtBoxDesign#��[  �� MaxLines$ 
2147483633���� -   $ txtBox_name��$��1��$��1��#��[ �� -   $ txtBox_gender��$��2��$��1��#��[ �� -   $ 
txtBox_age��$��3��$��1�� 
 numBox#��[ �� -   $ txtBox_menuTag��$��4��$��1��#��[ �� -   $ txtBox_menuDesc��$��5��$��1��#��[ $��32(�$��Normal$� �� TextAlignment$ Center=�$
��Thin"� �� TextWrapping$
 Wrap=� �� AcceptsReturn$
 True��� $��2��$��10q� �� $��6* ��� $��6* �.+�� $��0�� �� $��* ��� $��* ��� $��* � �� $��4* ��� $��* �.+��� -    ��Name$ img_spriteHouse��$��0��$��0��  pixelArtImage#��[ �� -   $ img_spritePortrait��$��0��$��1��#��[ �� -	   $ img_spriteBack��$��0��$��2��#��[ �� -
   	 ��Name$	 btn_spriteHouse��$��1��$��0��$��Import House Image...��  importBtnDesign#��[ �� -   $	 btn_spritePortrait��$��1��$��1��$ ��Import Main Portrait [A]..��#��[ �� -   $	 btn_spriteBack��$��1��$��2��$!��Import Backside Portrait...��#��[ � $��1�� �� $��* ��� $��* ��� $��* � �� $��4* ��� $��* �.+��� -   $ img_spriteIcon��$��0��$��0��#��[ �� -   $ img_spritePortraitB��$��0��$��1��#��[ �� -   $ img_spriteChibi��$��0��$��2��#��[ �� -   $	 btn_spriteIcon��$��1��$��0��$��Import Character Icon...��#��[ �� -   $	 btn_spriteChibi��$��1��$��2��$&�� Import Character Chibi Sprite...��#��[ �� -   $	 btn_spritePortraitB��$��1��$��1��$"��Import Outfit Portait [B]...��#��[ � $��2�� �� $��* ��� $��* � �� $��2* ��� $��6* �.+��� $��0��  header1#��[ $��Gameplay Stats��$��Center=�$��Center=��� $��1��#��[ $��Perks & Other Information��$��Center=�$��Center=�� $��1�� �� $��2* ��� $��* ��� $��2* ��� $��* � �� $��* ��� $��* ��� $��* �.+��� $��0��$��0��$��	Strength:��#��[  $
��Left=�$��10,0,0,0q�$��Center=�$
��Auto��$
��Auto���� $��1��$��0��$��
Dexterity:��#��[  $
��Left=�$��10,0,0,0q�$��Center=�$
��Auto��$
��Auto���� $��2��$��0��$��Perception:��#��[  $
��Left=�$��10,0,0,0q�$��Center=�$
��Auto��$
��Auto���� -   $ txtBox_strength��$��0��$��1��#��[ �� -   $ txtBox_dexterity��$��1��$��1��#��[ �� -   $ txtBox_perception��$��2��$��1��#��[ �� $��0��$��2��$��
Knowledge:��#��[  $
��Left=�$��10,0,0,0q�$��Center=�$
��Auto��$
��Auto���� $��1��$��2��$��	Charisma:��#��[  $
��Left=�$��10,0,0,0q�$��Center=�$
��Auto��$
��Auto���� $��2��$��2��$��Luck:��#��[  $
��Left=�$��10,0,0,0q�$��Center=�$
��Auto��$
��Auto���� -   $ txtBox_knowledge��$��0��$��3��#��[ �� -   $ txtBox_charisma��$��1��$��3��#��[ �� -   $ txtBox_luck��$��2��$��3��#��[ � $��1��$��1��.+�� $��5q� �� $��* ��� $��* � �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* �.+��� -   $ txtBox_aName��  smallerTextBox#��[ $��1��$��0���� -   $ txtBox_bName��#��[ $��1��$��2���� -   $ txtBox_author��#��[ $��1��$��4���� -   $ txtBox_contact��#��[ $��1��$��5���� -   
 ��Name$
 comboBox_aPerkPack��  comboBoxDark#��[ $��1��$��1���� -   $
 comboBox_bPerkPack��#��[ $��1��$��3���� $��[A] Outfit Name: ��  header3#��[ $��0���� $��A-Set Perks: ��#��[ $��1���� $��[B] Outfit Name: ��#��[ $��2���� $��B-Set Perks: ��#��[ $��3���� $��Author: ��#��[ $��4���� $��Contact Information: ��#��[ $��5���  �� $��2* ��� $��* ��� $��* �.+��� $��CUSTOM CHARACTER CREATOR��#��[ $
��Left=�$��0���� -   $
��SAVE��$��2��#��[ $��64(�