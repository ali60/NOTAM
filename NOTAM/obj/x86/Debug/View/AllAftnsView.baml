   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel  5   K   @ <NOTAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:NOTAM.View
NOTAM.View 6�      NOTAM.View.AllAftnsView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      5      @d2http://schemas.microsoft.com/expression/blend/2008      5      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase      6K   (clrclr-namespace:NOTAM.View      6�     � 	Resources  5         	AftnsData �� Source �� Path �� SortDescriptions+   %System.ComponentModel.SortDescription   PropertyName   	Direction(  "System.Windows.Controls.GroupStyle  AftnGroupStyle   HeaderTemplate ��Name  Brush_HeaderBackground �� Padding	 �� Triggers
 �� Value �� Binding �� 
TargetName �� Property �� Value  AftnItemStyle �� 
TargetType �� Mode �� Triggers ]� 
Conditions �� Property �� Value��%W  &	        &	 w     7  &	 �    �� 5   
    62   �� $ AllAftns� 5
        5      $ Series��6&   $ 
Descending=�6<   5        5   
    5      �� 5      .R�5      �� $	 txt��5      8��  $
��Bold"�5      
�������$��1q�5      $ 4,2,0,2q�5      	 5      �� 5      $

 True��6D    6&   �� $
 Name�.a�5      �� $	 txt��6%    � ��$ 	Companies��6F   5      5      5!      �� 5#   
   " ��6&   .�5(      ��  � ��$ Stretch=�6;   �� 5-       �@��
IsSelected 6+   �� $ 
IsSelected�$ TwoWay=� 5.      ]� 5/       50      �� 51       �@��AlternationIndex$ 1��6M   �� 52       �@��
IsSelected .  6:   �� 53       �@J�IsMouseOver .  6;   ./�55      ��  � �� �	#EEEEEEEE63   56      58      59      .��5;      ]� .+�5=   
   ��  �� AlternationCount$ 2��5>       � DataContext# [  5?      #��[ 5@      ��5A      ��   NOTAM.View.Commands!   ListViewDoubleClickCommand 5B      �� *HandleDoubleClick+ �� 
GroupStyle 5C      �� 5D       �� ResourceKey$ AftnGroupStyle��5E       �� View 5I      �� 5J      .P�5K      �� $��Series��5L       �� DisplayMemberBinding 5M      �� $ Series��� 5O      $��AftnList��5P       5Q      �� $ AftnList�5U      5W      5X      5Y      