   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel  5      R NSystem.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089>.clr-namespace:System.Data;assembly=System.DataSystem.Data 6S   @ <NOTAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:NOTAM.View
NOTAM.View 6�   /clr-namespace:NOTAM.BehaviorNOTAM.Behavior 6�   "   NOTAM.View.AllIntlNotamsView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      5      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase      5      @local.clr-namespace:System.Data;assembly=System.Data      6S   (clrclr-namespace:NOTAM.View      6�   1Behaviorclr-namespace:NOTAM.Behavior      6�     � 	Resources  5         brushWatermarkBackground �� Color  brushWatermarkForeground   BooleanToVisibilityConverter3  -NOTAM.Behavior.TextInputToVisibilityConverter " TextInputToVisibilityConverter  
NotamsData �� Source �� Path(  "System.Windows.Controls.GroupStyle  NotamGroupStyle   HeaderTemplate ��Name  Brush_HeaderBackground �� Padding  NotamItemStyle �� 
TargetType �� Property	 �� Value
 �� Mode �� Triggers ]� 
Conditions �� Property �� Value P� Property P� Value��%�  &	        &	      &	 B     &	 U     &	 h     &	 �     7  &	 H    �� 5   
   $ White��6;   �� 5   
   $ 	LightGray��6;   �� 5
   
   6'     5   
   62   �� 5   
    63   �� $ 	AllNotams�5        5   
    5      �� 5      .R�5      �� $	 txt��5      8��  $
��Bold"�5      
�������$��1q�5      $ 4,2,0,2q�5      5      5      �� 5   
   " ��6'   .�5#      ��  � ��$	 Stretch=�6;   �� 5-       �@��
IsSelected	 6+   �� $ 
IsSelected�$
 TwoWay=� 5.      ]� 5/       50      �� 51       �@��AlternationIndex$ 1��6M   �� 52       �@��
IsSelected .  6:   �� 53       �@J�IsMouseOver .  6;   ./�55      ��  � ��	 �	#EEEEEEEE63   56      P� 57       �@J�IsMouseOver . . �58      ��  �@�ToolTip	 68   �� $ ArchiveReason�59      5<      5=      .��5?      ]� .+�5@   
   �  � ColumnDefinitions 5A      �� 5B      $��35 �6#   �� 5C      $��65 �6#   �� 5D      $��30 �6#   �� 5E      $��50 �6#   �� 5F      $��50 �6#   �� 5G      $��* �6#   �� 5H      $��70 �6#   �� 5I      $��70 �6#   �� 5J      $��70 �6#    � RowDefinitions 5M      �� 5N      $��40 �6    �� 5O      $��* �6    .+�5X      � $��0��6   $��0��6!   #��[  61   .+�5Y      �� $	��Top=�6   $��0,0,0,10q�61   $
��Type��6D   #��[ 6Q   $
��Thin"�6�   y�5Z      ��  �� ElementName$ txtTypeFilter��$ Text.IsEmpty� �� 	Converter# [ �� 5[      -   5        $��txtTypeFilter��5[   Z   $	��Top=�6   $��0,0,0,10q�6/   
������  � ToolTip$
 Type��6�   ��6o   �� $ 
TypeFilter�6�   5\      � 5]      $��0��6   $��1��6!   #��[  61   .+�5^      �� $	��Top=�6   $��0,0,0,10q�61   $��Number��6D   #��[ 6S   $
��Thin"�6�   y�5_      �� $ txtNumberFilter��$ Text.IsEmpty�# [ �� 5`      -   5        $��txtNumberFilter��5`   Z   $	��Top=�6   $��0,0,0,10q�6/   
������ $ Number��6�   ��6r   �� $ NumberFilter�6�   5a      � 5b      $��0��6   $��2��6!   #��[  61   .+�5c      �� $	��Top=�6   $��0,0,0,10q�61   $
��Year��6D   #��[ 6Q   $
��Thin"�6�   y�5d      �� $ txtYearFilter��$ Text.IsEmpty�# [ �� 5e      -   5        $��txtYearFilter��5e   Z   $	��Top=�6   $��0,0,0,10q�6/   
������ $
 Year��6�   ��6o   �� $ 
YearFilter�6�   5f      � 5g      $��0��6   $��3��6!   #��[  61   .+�5h      �� $	��Top=�6   $��0,0,0,10q�61   $��QCode��6D   #��[ 6R   $
��Thin"�6�   y�5i      �� $ txtQCodeFilter��$ Text.IsEmpty�# [ �� 5j      -   5        $��txtQCodeFilter��5j   Z   $	��Top=�6   $��0,0,0,10q�6/   
������ $ QCode��6�   ��6p   �� $ NotamCodeFilter�6�   5k      � 5l      $��0��6   $��4��6!   #��[  61   .+�5m      �� $	��Top=�6   $��0,0,0,10q�61   $��FIR/Aero��6D   #��[ 6U   $
��Thin"�6�   y�5n      �� $ txtAeroFilter��$ Text.IsEmpty�# [ �� 5o      -   5        $��txtAeroFilter��5o   Z   $	��Top=�6   $��0,0,0,10q�6/   
������ $ FIR/Aero��6�   ��6o   �� $ NotamAeroFilter�6�   5p      � 5q      $��0��6   $��5��6!   #��[  61   .+�5r      �� $	��Top=�6   $��0,0,0,10q�61   $��Item E��6D   #��[ 6S   $
��Thin"�6�   y�5s      �� $ txtItemE��$ Text.IsEmpty�# [ �� 5t      -   5        $��txtItemE��5t   Z   $	��Top=�6   $��0,0,0,10q�6/   
������ $ Item E��6�   ��6j   �� $ ItemEFilter�6�   5u      � 5v      $��0��6   $��6��6!   #��[  61   .+�5w      �� $	��Top=�6   $��60��6.   $
��Left=�69   $��0,0,0,10q�6|   $��Refresh��6�   ��6T   �� $ RefreshCommand�6�   5x      � 5y      $��0��6   $��7��6!   #��[  61   .+�5z      �� $	��Top=�6   $
��Left=�6/   $��0,0,0,10q�6u   $��
Statistics��6�   ��6J   �� $ StatisticsCommand�6�   5{      � 5|      $��0��6   $��8��6!   #��[  61   .+�5}      �� $	��Top=�6   $
��Left=�6/   $��0,0,0,10q�6q   $��Report��6�   ��6J   �� $ ReportCommand�6�   5~      �� 5      $��1��6   $��9��6$    �� AlternationCount$ 2��5�       � DataContext# [ 5�      #��[ 5�      ��5�      ��   NOTAM.View.Commands!   ListViewDoubleClickCommand 5�      �� *HandleDoubleClick+ � ContextMenu 5�      �� 5�      .@�5�      m� $��Force Delete��6#    m� Command 5�      �� *MenuRemoveCommand+ m� CommandParameter 5�      ��  �� RelativeSource ��  �� AncestorType" ��$" PlacementTarget.SelectedItem�m� 5�      $��Force Archive��6#    5�      �� *MenuArchiveCommand+ 5�      ��  �� " ��$" PlacementTarget.SelectedItem�5�       �� 
GroupStyle 5�      �� 5�       �� ResourceKey$ NotamGroupStyle��5�        �� View  5�      �� 5�      .P�5�      �� $
��Type��5�      ! �� DisplayMemberBinding! 5�      �� $
 Type��� 5�      $��Number��5�      ! 5�      �� $ NotamNum��� 5�      $
��Year��5�      ! 5�      �� $
 Year��� 5�      $��QCode��5�      ! 5�      �� $ 	NotamCode��� 5�      $��
Notam Type��5�      ! 5�      �� $ 	NotamType��� 5�      $��FIR/Aero��5�      ! 5�      �� $ FirAero��� 5�      $��Origin��5�      ! 5�      �� $ Origin��� 5�      $	��FIR��5�      ! 5�      �� $	 FIR��� 5�      $��ToDate��5�      ! 5�      �� $ ToDate��� 5�      $��Perm/Est��5�      ! 5�      �� $ PermEst��� 5�      " �� CellTemplate" 5�      �� 5�   "   .R�5�   &   �� #��[" �5�   )   $��Details��5�   %   $��0q�5�   %   $��Center=�5�   %   ��5�   %   �� $ DetailCommand�5�   #   5�      5�      5�      5�      5�      5�      