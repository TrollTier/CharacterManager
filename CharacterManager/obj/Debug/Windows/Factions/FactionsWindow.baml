   M S B A M L   `   `   `  ����K  GCharacterManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:DarkSunProgramming.LanguagesDarkSunProgramming.Languages  5   	   -    'CharacterManager.Windows.FactionsWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35` \RibbonControlsLibrary, Version=3.5.41019.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Mribbon@http://schemas.microsoft.com/winfx/2006/xaml/presentation/ribbon  5   	   5lang*clr-namespace:DarkSunProgramming.Languages  5   	     � Title$  FactionsWindow��5   	   $	��513��6    $	��746��6-    � Icon$3 -pack://application:,,,/Resources/factions.png��5   	   .��5      �  � RowDefinitions 5	   
   �� 5
      $��25 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   $��0��6>   $
��Auto��6K   
�������.+�5      �� ��6   �� *
AddCommand+.��5      �� $.��(pack://application:,,,/Resources/add.png��6   $��25��6J   5      �� 5      $��5,0,0,0q�67   ��6   �� *DeleteCommand+.��5      �� $3��-pack://application:,,,/Resources/delete_1.png��6   $��25��6O   5      �� 5      $��5,0,0,0q�65   ��6   �� *SaveCommand+.��5      �� $1��+pack://application:,,,/Resources/save_1.png��6   $��25��6M   5      5      � 5   
   $��0��6   $��1��6   $��10, 10, 10, 10q�6,    5      �� 5      $��* �6#   �� 5       $��3* �6#   .+�5#      ��  �� DisplayMemberPath$
 Name��5$      ��5#      �� *
Factions+ �� SelectedValue 5%      �� *SelectedObject+� 5'      $��0��6   $��1��6    $��10, 0, 0, 0q�60   {�5(      �� *
IsEditable+ 5)      �� 5*      $
��Auto �6'   �� 5+      $��* �6'   �� 5,      $	��100 �6'    5.      �� 5/      $
��Auto �6$   �� 50      $
��Auto �6$   �� 51      $��* �6$   .+�54      �� $��0��55      $��0��6)   ��54      5  /DarkSunProgramming.Languages.TranslateExtension     TranslationKey$ 000010��55   )   �� 56      $��0��57      $��1��6'    �� 
IsReadOnly . 
���������56      �� *SelectedObject.ID+	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=�58      �� 5:      $��1��5;      $��0��6)   ��5:        $ 000011��5;   )   �� 5<      $��1��5=      $��1��6'   ��5<      �� *SelectedObject.Name+$	 TwoWay=�$
 PropertyChanged=�5=   '   �� 5?      $��2��5@      $��0��6)   ��5?        $ 000012��5@   )   �� 5A      $��2��5B      $��1��6'    �� AcceptsReturn . ��5A      �� *SelectedObject.Description+$	 TwoWay=�$
 PropertyChanged=�5B   7   �� 5D      -   5        $��imgFactionImage��5F      $��0��5E      $��2��6%   $��2��65   ��5D      �� *FactionImage+$	 OneWay=�$
 PropertyChanged=�5F   /   5G      5H      5I      5J      