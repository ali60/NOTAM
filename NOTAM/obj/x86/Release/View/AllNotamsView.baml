   M S B A M L   `   `   `  ���� R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel  R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data @ <NOTAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:NOTAM.View
NOTAM.View /clr-namespace:NOTAM.BehaviorNOTAM.Behavior    NOTAM.View.AllNotamsView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase      @local.clr-namespace:System.Data;assembly=System.Data      (clrclr-namespace:NOTAM.View      1Behaviorclr-namespace:NOTAM.Behavior        � 	Resources     brushWatermarkBackground �� Color  brushWatermarkForeground   BooleanToVisibilityConverter3  -NOTAM.Behavior.TextInputToVisibilityConverter " TextInputToVisibilityConverter  
NotamsData �� Source �� Path(  "System.Windows.Controls.GroupStyle  NotamGroupStyle   HeaderTemplate ��Name  Brush_HeaderBackground �� Padding  NotamItemStyle �� 
TargetType �� Property	 �� Value
 �� Mode �� Triggers ]� 
Conditions �� Property �� Value P� Property P� Value��%8  &	        &	      &	 &     &	 +     &	 0     &	 N     7  &	 �     �� $ White���� $ 	LightGray����   ��  �� $ 	AllNotams�   �� .R��� $	 txt��8��  $
��Bold"�
�������$��1q�$ 4,2,0,2q��� " ��.���  � ��$	 Stretch=���  �@��
IsSelected	 �� $ 
IsSelected�$
 TwoWay=� ]�  ��  �@��AlternationIndex$ 1����  �@��
IsSelected .  ��  �@J�IsMouseOver .  ./���  � ��	 �	#EEEEEEEEP�  �@J�IsMouseOver . . ���  �@�ToolTip	 �� $ ArchiveReason�.��]� .+��  � ColumnDefinitions �� $��35 ��� $��65 ��� $��30 ��� $��50 ��� $��50 ��� $��* ��� $��70 ��� $��70 ��� $��70 � � RowDefinitions �� $��40 ��� $��* �.+�� $��0��$��0��#��[  .+��� $	��Top=�$��0,0,0,10q�$
��Type��#��[ $
��Thin"�y���  �� ElementName$ txtTypeFilter��$ Text.IsEmpty� �� 	Converter# [ �� -   $��txtTypeFilter��$	��Top=�$��0,0,0,10q�
������  � ToolTip$
 Type������ $ 
TypeFilter�� $��0��$��1��#��[  .+��� $	��Top=�$��0,0,0,10q�$��Number��#��[ $
��Thin"�y��� $ txtNumberFilter��$ Text.IsEmpty�# [ �� -   $��txtNumberFilter��$	��Top=�$��0,0,0,10q�
������ $ Number������ $ NumberFilter�� $��0��$��2��#��[  .+��� $	��Top=�$��0,0,0,10q�$
��Year��#��[ $
��Thin"�y��� $ txtYearFilter��$ Text.IsEmpty�# [ �� -   $��txtYearFilter��$	��Top=�$��0,0,0,10q�
������ $
 Year������ $ 
YearFilter�� $��0��$��3��#��[  .+��� $	��Top=�$��0,0,0,10q�$��QCode��#��[ $
��Thin"�y��� $ txtQCodeFilter��$ Text.IsEmpty�# [ �� -   $��txtQCodeFilter��$	��Top=�$��0,0,0,10q�
������ $ QCode������ $ NotamCodeFilter�� $��0��$��4��#��[  .+��� $	��Top=�$��0,0,0,10q�$��FIR/Aero��#��[ $
��Thin"�y��� $ txtAeroFilter��$ Text.IsEmpty�# [ �� -   $��txtAeroFilter��$	��Top=�$��0,0,0,10q�
������ $ FIR/Aero������ $ NotamAeroFilter�� $��0��$��5��#��[  .+��� $	��Top=�$��0,0,0,10q�$��Item E��#��[ $
��Thin"�y��� $ txtItemE��$ Text.IsEmpty�# [ �� -   $��txtItemE��$	��Top=�$��0,0,0,10q�
������ $ Item E������ $ ItemEFilter�� $��0��$��6��#��[  .+��� $	��Top=�$��60��$
��Left=�$��0,0,0,10q�$��Refresh������ $ RefreshCommand�� $��0��$��7��#��[  .+��� $	��Top=�$
��Left=�$��0,0,0,10q�$��
Statistics������ $ StatisticsCommand�� $��0��$��8��#��[  .+��� $	��Top=�$
��Left=�$��0,0,0,10q�$��Report������ $ ReportCommand��� $��1��$��9�� �� AlternationCount$ 2�� � DataContext# [ #��[ ����   NOTAM.View.Commands!   ListViewDoubleClickCommand �� *HandleDoubleClick+ � ContextMenu �� .@�m� $��Force Delete�� m� Command �� *MenuRemoveCommand+ m� CommandParameter ��  �� RelativeSource ��  �� AncestorType" ��$" PlacementTarget.SelectedItem�m� $��Force Archive�� �� *MenuArchiveCommand+ ��  �� " ��$" PlacementTarget.SelectedItem� �� 
GroupStyle ��  �� ResourceKey$ NotamGroupStyle��  �� View  �� .P��� $
��Type��! �� DisplayMemberBinding! �� $
 Type��� $��Number��! �� $ NotamNum��� $
��Year��! �� $
 Year��� $��QCode��! �� $ 	NotamCode��� $��
Notam Type��! �� $ 	NotamType��� $��FIR/Aero��! �� $ FirAero��� $��Origin��! �� $ Origin��� $	��FIR��! �� $	 FIR��� $��ToDate��! �� $ ToDate��� $��Perm/Est��! �� $ PermEst��� " �� CellTemplate" �� .R��� #��[" �$��Details��$��0q�$��Center=����� $ DetailCommand�