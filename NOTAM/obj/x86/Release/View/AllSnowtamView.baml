   M S B A M L   `   `   `  ���� R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel  R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data @ <NOTAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:NOTAM.View
NOTAM.View    NOTAM.View.AllSnowtamView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase      @local.clr-namespace:System.Data;assembly=System.Data      (clrclr-namespace:NOTAM.View      -     � 	Resources     
NotamsData �� Source �� Path(  "System.Windows.Controls.GroupStyle  NotamGroupStyle   HeaderTemplate ��Name  Brush_HeaderBackground �� Padding  NotamItemStyle �� 
TargetType �� Property �� Value	 �� Mode
 �� Triggers ]� 
Conditions �� Property �� Value P� Property P� Value��%�  &	        &	       7  &	 q     ��  �� $ AllSnowtams�   �� .R��� $	 txt��8��  $
��Bold"�
�������$��1q�$ 4,2,0,2q��� " ��.���  � ��$ Stretch=���  �@��
IsSelected �� $ 
IsSelected�$	 TwoWay=�
 ]�  ��  �@��AlternationIndex$ 1����  �@��
IsSelected .  ��  �@J�IsMouseOver .  ./���  � �� �	#EEEEEEEEP�  �@J�IsMouseOver . . ���  �@�ToolTip �� $ ArchiveReason�.��]� .+��  � ColumnDefinitions �� $��50 ��� $��50 ��� $��50 ��� $��* ��� $��70 � � RowDefinitions �� $��40 ��� $��* �.+�� $��0��$��5��.+��� $	��Top=�$
��Left=�$��0,0,0,10q�$��Report������ $ ReportCommand��� $��1��$��5�� �� AlternationCount$ 2�� � DataContext# [  #��[ ����   NOTAM.View.Commands!   ListViewDoubleClickCommand �� *HandleDoubleClick+ �� 
GroupStyle ��  �� ResourceKey$ NotamGroupStyle�� �� View �� .P��� $��Number�� �� DisplayMemberBinding �� $ 
SnowtamNum��� $
��Aero�� �� $ Aerodome��� $
��Date�� �� $ 	DateTimeT���  �� CellTemplate �� .R��� #��[" �$��Details��$��0q�$��Center=����� $ DetailCommand�