   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel  5   K   	 NOTAM   NOTAM.View.AllUsersView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      5      @d2http://schemas.microsoft.com/expression/blend/2008      5      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase      6K     � 	Resources  5         	UsersData �� Source �� Path �� SortDescriptions+   %System.ComponentModel.SortDescription   PropertyName   	Direction(  "System.Windows.Controls.GroupStyle  UserGroupStyle   HeaderTemplate ��Name  Brush_HeaderBackground �� Padding	 �� Triggers
 �� Value �� Binding �� 
TargetName �� Property �� Value  UserItemStyle �� 
TargetType �� Mode �� Triggers ]� 
Conditions �� Property �� Value��%�  &	        &	 �     7  &	 �    �� 5   
    62   �� $ AllUsers� 5
        5      $ IsAdmin��6&   $ 
Descending=�6=     5      $ Username��6&   $ 	Ascending=�6>   5        5   
    5      �� 5      .R�5      �� $	 txt��5      8��  $
��Bold"�5      
�������$��1q�5      $ 4,2,0,2q�5      	 5      �� 5      $

 True��6D    6&   �� $
 Name�.a�5      �� $	 txt��6%    � ��$ 	Companies��6F   5      5       5"      �� 5$   
   " ��6&   .�5)      ��  � ��$ Stretch=�6;   �� 5.       �@��
IsSelected 6+   �� $ 
IsSelected�$ TwoWay=� 5/      ]� 50       51      �� 52       �@��AlternationIndex$ 1��6M   �� 53       �@��
IsSelected .  6:   �� 54       �@J�IsMouseOver .  6;   ./�56      ��  � �� �	#EEEEEEEE63   57      59      5:      .��5<      ]� .+�5>   
   ��  �� AlternationCount$ 2��5?       � DataContext# [  5@      #��[ 5A      ��5B      ��  �� 
GroupStyle 5D      �� 5E       �� ResourceKey$ UserGroupStyle��5F       �� View 5J      �� 5K      .P�5L      �� $��Username��5M       �� DisplayMemberBinding 5N      �� $ Username��� 5P      $��IsAdmin��5Q       5R      �� $ IsAdmin��� 5T       �� CellTemplate 5U      �� 5V      .R�5W   "   �� #��[" �5X   )   $��Delete��5Y   %   $��0q�5Z   %   $��Center=�5[   %   ��5\   %   �� $ DeleteCommand�5]      5_      5`      5b      5c      5d      