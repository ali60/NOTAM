   M S B A M L   `   `   `  ���� R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel  @ <NOTAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:NOTAM.View
NOTAM.View    NOTAM.View.AllFIRsView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      @d2http://schemas.microsoft.com/expression/blend/2008      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase      (clrclr-namespace:NOTAM.View        � 	Resources     FIRsData �� Source �� Path �� SortDescriptions+   %System.ComponentModel.SortDescription   PropertyName   	Direction(  "System.Windows.Controls.GroupStyle  FIRGroupStyle   HeaderTemplate ��Name  Brush_HeaderBackground �� Padding	 �� Triggers
 �� Value �� Binding �� 
TargetName �� Property �� Value  FIRItemStyle �� 
TargetType �� Mode �� Triggers ]� 
Conditions �� Property �� Value��%   &	        &	 a     7  &	     ��  �� $ AllFIRs�   $
 Code��$ 
Descending=�  $
 Name��$ 	Ascending=�   �� .R��� $	 txt��8��  $
��Bold"�
�������$��1q�$ 4,2,0,2q�	 �� $

 True�� �� $
 Name�.a��� $	 txt�� � ��$ 	Companies���� " ��.���  � ��$ Stretch=���  �@��
IsSelected �� $ 
IsSelected�$ TwoWay=� ]�  ��  �@��AlternationIndex$ 1����  �@��
IsSelected .  ��  �@J�IsMouseOver .  ./���  � �� �	#EEEEEEEE.��]� .+���  �� AlternationCount$ 2�� � DataContext# [  #��[ ����   NOTAM.View.Commands!   ListViewDoubleClickCommand �� *HandleDoubleClick+ �� 
GroupStyle ��  �� ResourceKey$ FIRGroupStyle�� �� View �� .P��� $
��Code�� �� DisplayMemberBinding �� $
 Code��� $
��Name�� �� $
 Name��� $��Origin�� �� $ Origin��� $��AtcCode�� �� $ AtcCode��� $��
NorthLimit�� �� $ 
NorthLimit��� $��
SouthLimit�� �� $ 
SouthLimit��� $��	WestLimit�� �� $ 	WestLimit��� $��	EastLimit�� �� $ 	EastLimit�