   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel  5      R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data 6S   @ <NOTAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:NOTAM.View
NOTAM.View 6�      NOTAM.View.AllSnowtamView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      5      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase      5      @local.clr-namespace:System.Data;assembly=System.Data      6S   (clrclr-namespace:NOTAM.View      6�   -   5          � 	Resources  5         
NotamsData �� Source �� Path(  "System.Windows.Controls.GroupStyle  NotamGroupStyle   HeaderTemplate ��Name  Brush_HeaderBackground �� Padding  NotamItemStyle �� 
TargetType �� Property �� Value	 �� Mode
 �� Triggers ]� 
Conditions �� Property �� Value P� Property P� Value��%  &	        &	 7     7  &	 �     �� 5   
    63   �� $ AllSnowtams�5        5
   
    5      �� 5      .R�5      �� $	 txt��5      8��  $
��Bold"�5      
�������$��1q�5      $ 4,2,0,2q�5      5      5      �� 5   
   " ��6'   .�5      ��  � ��$ Stretch=�6;   �� 5(       �@��
IsSelected 6+   �� $ 
IsSelected�$	 TwoWay=�
 5)      ]� 5*       5+      �� 5,       �@��AlternationIndex$ 1��6M   �� 5-       �@��
IsSelected .  6:   �� 5.       �@J�IsMouseOver .  6;   ./�50      ��  � �� �	#EEEEEEEE63   51      P� 52       �@J�IsMouseOver . . �53      ��  �@�ToolTip 68   �� $ ArchiveReason�54      57      58      .��5:      ]� .+�5;   
   �  � ColumnDefinitions 5<      �� 5=      $��50 �6#   �� 5>      $��50 �6#   �� 5?      $��50 �6#   �� 5@      $��* �6#   �� 5A      $��70 �6#    � RowDefinitions 5D      �� 5E      $��40 �6    �� 5F      $��* �6    .+�5H      � $��0��6   $��5��6!   .+�5I      �� $	��Top=�6   $
��Left=�6/   $��0,0,0,10q�6q   $��Report��6�   ��6J   �� $ ReportCommand�6�   5J      �� 5M      $��1��6   $��5��6$    �� AlternationCount$ 2��5N       � DataContext# [  5O      #��[ 5P      ��5Q      ��   NOTAM.View.Commands!   ListViewDoubleClickCommand 5R      �� *HandleDoubleClick+ �� 
GroupStyle 5T      �� 5U       �� ResourceKey$ NotamGroupStyle��5V       �� View 5Z      �� 5\      .P�5^      �� $��Number��5_       �� DisplayMemberBinding 5`      �� $ 
SnowtamNum��� 5b      $
��Aero��5c       5d      �� $ Aerodome��� 5f      $
��Date��5g       5h      �� $ 	DateTimeT��� 5l       �� CellTemplate 5m      �� 5n   "   .R�5o   &   �� #��[" �5p   )   $��Details��5q   %   $��0q�5r   %   $��Center=�5s   %   ��5t   %   �� $ DetailCommand�5u   #   5w      5{      5}      5~      5      5�      