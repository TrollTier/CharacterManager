   M S B A M L   `   `   `  ����K  GCharacterManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:DarkSunProgramming.LanguagesDarkSunProgramming.Languages  5   	   @    :DarkSunProgramming.Windows.Dialogs.EditCharacterFileDialog   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=3.5.41019.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   :ext*clr-namespace:DarkSunProgramming.Languages     5   	     � Title$$  Datei-Informationen bearbeiten��5   	   $	��300��60   $	��500��6=    � Icon$6 0pack://application:,,,/Resources/Pencil-icon.png��5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�6   .��5      �  � ColumnDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6$   ��5      5  /DarkSunProgramming.Languages.TranslateExtension     TranslationKey$ 000011���� 5   
   $��1��6   $��0��6"   ��5      �� *
FileName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 5   
   $��0��6   $��1��6$   ��5        $ 000012���� 5   
   $��1��6   $��1��6"   	 �� AcceptsReturn	 . 
 �� 
AcceptsTab
 . ��5      �� *FileDescription+$ TwoWay=�$ PropertyChanged=��� 5   
    �� Orientation$ 
Horizontal=�6   $��0��5      $��2��6%   $��2��62   $��Right=�5      .+�5       �� -   5        $��cmdSave��5!      $��50��5       $��25��6    
������ 
������ .��5#      �� $1��+pack://application:,,,/Resources/save_1.png��6   5$      �� 5%      -   5        $��	cmdCancel��5&   '   $��50��5%      $��25��6    $��10,0,0,0q�5&      
������ 
������ .��5(      �� $6��0pack://application:,,,/Resources/icon_cancel.png��6   5)      5*      5+      5,      