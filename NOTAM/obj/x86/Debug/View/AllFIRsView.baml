   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel  5      @ <NOTAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:NOTAM.View
NOTAM.View 6S      NOTAM.View.AllFIRsView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      5      @d2http://schemas.microsoft.com/expression/blend/2008      5      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase      5      (clrclr-namespace:NOTAM.View      6S     � 	Resources  5         FIRsData �� Source �� Path �� SortDescriptions+   %System.ComponentModel.SortDescription   PropertyName   	Direction(  "System.Windows.Controls.GroupStyle  FIRGroupStyle   HeaderTemplate ��Name  Brush_HeaderBackground �� Padding	 �� Triggers
 �� Value �� Binding �� 
TargetName �� Property �� Value  FIRItemStyle �� 
TargetType �� Mode �� Triggers ]� 
Conditions �� Property �� Value��%�  &	        &	 �     7  &	 �    �� 5   
    61   �� $ AllFIRs� 5
        5      $
 Code��6&   $ 
Descending=�6:     5      $
 Name��6&   $ 	Ascending=�6:   5        5   
    5      �� 5      .R�5      �� $	 txt��5      8��  $
��Bold"�5      
�������$��1q�5      $ 4,2,0,2q�5      	 5      �� 5      $

 True��6D    6&   �� $
 Name�.a�5      �� $	 txt��6%    � ��$ 	Companies��6F   5      5       5"      �� 5$   
   " ��6%   .�5)      ��  � ��$ Stretch=�6;   �� 5.       �@��
IsSelected 6+   �� $ 
IsSelected�$ TwoWay=� 5/      ]� 50       51      �� 52       �@��AlternationIndex$ 1��6M   �� 53       �@��
IsSelected .  6:   �� 54       �@J�IsMouseOver .  6;   ./�56      ��  � �� �	#EEEEEEEE63   57      59      5:      .��5<      ]� .+�5>   
   ��  �� AlternationCount$ 2��5?       � DataContext# [  5@      #��[ 5A      ��5B      ��   NOTAM.View.Commands!   ListViewDoubleClickCommand 5C      �� *HandleDoubleClick+ �� 
GroupStyle 5E      �� 5F       �� ResourceKey$ FIRGroupStyle��5G       �� View 5K      �� 5L      .P�5M      �� $
��Code��5N       �� DisplayMemberBinding 5O      �� $
 Code��� 5Q      $
��Name��5R       5S      �� $
 Name��� 5U      $��Origin��5V       5W      �� $ Origin��� 5Y      $��AtcCode��5Z       5[      �� $ AtcCode��� 5]      $��
NorthLimit��5^       5_      �� $ 
NorthLimit��� 5b      $��
SouthLimit��5c       5d      �� $ 
SouthLimit��� 5g      $��	WestLimit��5h       5i      �� $ 	WestLimit��� 5l      $��	EastLimit��5m       5n      �� $ 	EastLimit�5q      5s      5t      5u      