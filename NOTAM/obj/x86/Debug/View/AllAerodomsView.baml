   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel  5      @ <NOTAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:NOTAM.View
NOTAM.View 6S       NOTAM.View.AllAerodomsView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      5      @d2http://schemas.microsoft.com/expression/blend/2008      5      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase      5      (clrclr-namespace:NOTAM.View      6S     � 	Resources  5         AerodomsData �� Source �� Path �� SortDescriptions+   %System.ComponentModel.SortDescription   PropertyName   	Direction(  "System.Windows.Controls.GroupStyle  AerodomGroupStyle   HeaderTemplate ��Name  Brush_HeaderBackground �� Padding	 �� Triggers
 �� Value �� Binding �� 
TargetName �� Property �� Value  AerodomItemStyle �� 
TargetType �� Mode �� Triggers ]� 
Conditions �� Property �� Value��%�  &	        &	 �     7  &	 �    �� 5	   
    65   �� $ AllAerodoms� 5        5      $
 Code��6&   $ 
Descending=�6:     5      $
 Name��6&   $ 	Ascending=�6:   5        5   
    5      �� 5      .R�5      �� $	 txt��5      8��  $
��Bold"�5      
�������$��1q�5      $ 4,2,0,2q�5      	 5      �� 5      $

 True��6D    6&   �� $
 Name�.a�5      �� $	 txt��6%    � ��$ 	Companies��6F   5      5!      5#      �� 5%   
   " ��6)   .�5*      ��  � ��$ Stretch=�6;   �� 5/       �@��
IsSelected 6+   �� $ 
IsSelected�$ TwoWay=� 50      ]� 51       52      �� 53       �@��AlternationIndex$ 1��6M   �� 54       �@��
IsSelected .  6:   �� 55       �@J�IsMouseOver .  6;   ./�57      ��  � �� �	#EEEEEEEE63   58      5:      5;      .��5=      ]� .+�5?   
   ��  �� AlternationCount$ 2��5@       � DataContext# [  5A      #��[ 5B      ��5C      ��   NOTAM.View.Commands!   ListViewDoubleClickCommand 5D      �� *HandleDoubleClick+ �� 
GroupStyle 5F      �� 5G       �� ResourceKey$ AerodomGroupStyle��5H       �� View 5K      �� 5L      .P�5M      �� $
��Code��5N       �� DisplayMemberBinding 5O      �� $
 Code��� 5Q      $
��Name��5R       5S      �� $
 Name��� 5U      $	��FIR��5V       5W      �� $	 FIR��� 5Y      $��Country��5Z       5[      �� $ Country��� 5]      $��Latitude��5^       5_      �� $ Latitude��� 5b      $��	Direction��5c       5d      �� $ 	Direction��� 5g      $	��VOR��5h       5i      �� $	 VOR��� 5l      $	��I_L��5m       5n      �� $ IL�5q      5s      5t      5u      