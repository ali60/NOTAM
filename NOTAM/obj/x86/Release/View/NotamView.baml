   M S B A M L   `   `   `  ���� c  _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  @ <NOTAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:NOTAM.BehaviorNOTAM.Behavior X TXceed.Wpf.Toolkit, Version=2.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4P:clr-namespace:Xceed.Wpf.Toolkit;assembly=Xceed.Wpf.ToolkitXceed.Wpf.Toolkit M ISystem, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089/$clr-namespace:System;assembly=SystemSystem    NOTAM.View.NotamView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     1Behaviorclr-namespace:NOTAM.Behavior     Kxctk:clr-namespace:Xceed.Wpf.Toolkit;assembly=Xceed.Wpf.Toolkit     4sys$clr-namespace:System;assembly=System     .��� $��4q�  $� IsVirtualizing  .  $� VirtualizationMode$ 	Recycling=�.   (System.Windows.Interactivity.Interaction   	Behaviors 0  *NOTAM.Behavior.ValidationExceptionBehavior   � ColumnDefinitions �� $
��Auto ��� $��90 ��� $��* ��� $��90 ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � � RowDefinitions �� $
��Auto ��� $��30 ��� $
��Auto ��� $��30 ��� $
��Auto ��� $��30 ��� $
��Auto ��� $
��Auto ��� $��30 ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��30 ��� $
��Auto ��� $��30 ��� $
��Auto ��� $��30 ��� $
��Auto � � 	Resources  �� DataType-  'System.Windows.Controls.ValidationError �� Path   SimpleTemplate  ExtendedTemplate �� Orientation  NotamCodeTemplate	 ��Name
 �� Triggers �� Binding �� RelativeSource �� Mode �� AncestorType �� Value �� 
TargetName �� Value �� Property��%  (��       *, +)&	  [     &	 �     &	 J    7  7   �� "  .R��� $��Italic$�
���  ��$
��Left=�$	��0,1q����� $ ErrorContent��� .R��� .+��� ���� *DisplayCaption+�� .R��� $ 
Horizontal=�.+��� ���� *	Subject+�� $��|���� ���� *	Condition+�� $��|���� ���� *Subject_Desc+�� $��|���� ���� *Condition_Desc+�� .R��� $	 
theControl��|�.  8��  
 ��  ��  �� $ FindAncestor=�" ��$ 
IsSelected� Z� .a��� $ 
theControl��8   � ��.+��� $��0��$��0��$��Filling��$
��Left=� �� Target ��  �� ElementName$ 
fillingTxt���� -    ��Name$ 
fillingTxt��$��1��$��0��{��� $ IsFreeStyle����� $ Filling� �� ValidatesOnDataErrors .  �� UpdateSourceTrigger$ PropertyChanged=��� $��2��$��0��$��	Originat:��$
��Left=� �� $ 	originTxt���� -    ��Name$ 	originTxt��$��3��$��0������ $ OriginOptions� �� Mode$ OneTime=� �� SelectedItem �� $ Origin� . {��� $ IsFreeStyle��� $��2��$��1��$��Type*��$
��Left=� �� $ typeTxt���� -   $ typeTxt��$��3��$��1�� �� 
IsEditable . {��� $ IsFirAdEnabled����� $ TypeOptions�$ OneTime=� �� $
 Type� . �� $��2��$��2��$��
Notam Num*��$
��Left=� �� $ numTxt���� -   $ numTxt��$��3��$��2��{��� $ IsFreeStyle����� $ NotamNum� . $ PropertyChanged=��� $��2��$��3��$��Year*��$
��Left=� �� $ yearTxt���� -   $ yearTxt��$��3��$��3������ $
 Year� . $ PropertyChanged=��� $��2��$��4��$��
Notam Type��$
��Left=�$��30��$	��Top=� �� $ nTypeTxt���� -   $ nTypeTxt��$��3��$��4������ $ 	NotamType� . $ PropertyChanged=�{��� $ IsFreeStyle��� $��2��$��5��$��Ref Type��$
��Left=�y��� *RefVisibility+ �� $ 
refTypeTxt���� -   $ 
refTypeTxt��$��3��$��5��$��50��$
��Left=�y��� *RefVisibility+���� $ RefType� . $ PropertyChanged=��� $��2��$��6��$��Ref Num��$
��Left=�y��� *RefVisibility+ �� $ 	refNumTxt���� -   $ 	refNumTxt��$��3��$��6��y��� *RefVisibility+���� $ RefNum� . $ PropertyChanged=��� $��2��$��7��$��Ref Year��$
��Left=�y��� *RefVisibility+ �� $ 
refYearTxt���� -	   $ 
refYearTxt��$��3��$��7��$��70��$
��Left=�y��� *RefVisibility+���� $ RefYear� . $ PropertyChanged=��� $��4��$��0��$	��FIR��$
��Left=� �� $ firTxt���� -
   $ firTxt��$��5��$��0�� . ���� $ 
FirOptions�$ OneTime=� �� $	 FIR� . {��� $ IsFirEnabled��� $��4��$��1��$��QCode��$
��Left=� �� $ qCodeTxt���� -   $ qCodeTxt��$��5��$��1��#��[ ���� $ NotamCodeOptions�$ Default=� �� *IsNotamCodeEditable+ �� *	NotamCode+$ TwoWay=����� .R�$� �� $��4��$��2��$��Traffic��$
��Left=� �� $ 
trafficTxt���� -   $ 
trafficTxt��$��5��$��2������ $ Traffic� . $ PropertyChanged=�{��� $ IsFreeStyle��� $��4��$��3��$��Purpose��$
��Left=� �� $ 
purposeTxt���� -   $ 
purposeTxt��$��5��$��3������ $ Purpose� . $ PropertyChanged=�{��� $ IsFreeStyle��� $��4��$��4��$��Scope��$
��Left=� �� $ scopeTxt���� -   $ scopeTxt��$��5��$��4������ $ Scope� . $ PropertyChanged=�{��� $ IsFreeStyle��� $��4��$��5��$��Lower��$
��Left=� �� $ lowerTxt���� -   $ lowerTxt��$��5��$��5������ $ 
LowerLimit� . $ PropertyChanged=��� $��4��$��6��$��Upper��$
��Left=� �� $ upperTxt���� -   $ upperTxt��$��5��$��6������ $ 
UpperLimit� . $ PropertyChanged=��� $��4��$��7��$��Latitude��$
��Left=� �� $ latTxt���� -   $ latTxt��$��5��$��7�� �� 	MaxLength$ 5������ $ Latitude� . $ PropertyChanged=��� $��4��$��8��$��
Longtitude��$
��Left=� �� $ longTxt���� -   $ longTxt��$��5��$��8��$ 6������ $ 
Longtitude� . $ PropertyChanged=��� $��4��$��9��$��Radius��$
��Left=� �� $ 	radiusTxt���� -   $ 	radiusTxt��$��5��$��9��$ 3������ $ Radius� . $ PropertyChanged=��� $��6��$��0��$��FIR/AD*��$
��Left=� �� $ aeroTxt���� -   $ aeroTxt��$��7��$��0�� .  �� 
IsReadOnly . ���� $ AerodomOptions�$ Default=�$ PropertyChanged=� �� $ Aerodom� . {��� $ IsFirAdEnabled��� $��6��$��1��$��2��$��B)From Date��$��Center=� �� $ fromDateTxt���� -   $ fromDateTxt��$��7��$��1��$	��100��$��2��$	��150��$��Center=�$��30������ $ FromDate� . $ PropertyChanged=��� $��6��$��3��$��	C)To Date��$��2��$��Center=� �� $ 	toDateTxt���� -   $ 	toDateTxt��$	��100��$	��150��$��2��$��Center=�$��7��$��3��{��� $ 	IsEnabled����� $ ToDate� . $ PropertyChanged=��� $��6��$��5��$��EST/PERM��$
��Left=� �� $ permTxt���� -   $ permTxt��$��7��$��5�� . ���� $ PermOptions�$ OneTime=� �� $ PermEst� . �� $��9��$��0��$��D)��$��Right=� �� $ dFreeTxt���� -   $��dFreeTxt��$��9��$��1��$��8�� �� 	IsEnabled .  �� AcceptsReturn .   �� MinLines$  2��! �� MaxLines$! 4��"" �� VerticalScrollBarVisibility$
" Auto=�$	 200������ $ 	DFreeText� . $ PropertyChanged=��� $��11��$��0��$��E)��$��Right=� �� $ eFreeTxt���� -   $ eFreeTxt��# �� TextWrapping$
# Wrap=� . $��11��$��1��$��8��$
��Auto=�$! 7��$  2�� . ���� $ 	EFreeText� . $ PropertyChanged=����� $ 	ETxtColor�.��$ �� CustomDictionaries$ @�-)pack://application:,,,/DictionaryTest.lex?�.��(  "Xceed.Wpf.Toolkit.MagnifierManager%   	Magnifier% !  Xceed.Wpf.Toolkit.Magnifier  -   $��MagnifiyingGlass��$y�Hidden=�&   Radius$	& 200Q�'   
ZoomFactor$' .6Q�
��� ���$��2q�.���� -   $��11��$��Use Magnifier��$��9���� $��13��$��0��$��F)��$��Right=� �� $ fFreeTxt���� -   $ fFreeTxt�� . $��13��$��1��$��4�� . $! 4��$
" Auto=����� $ 	FFreeText� . $ PropertyChanged=��� $��13��$��5��$��G)��$��Right=� �� $ gFreeTxt���� -   $ gFreeTxt�� . $��13��$��6��$��4�� . $! 4��$
" Auto=����� $ 	GFreeText� . $ PropertyChanged=��� $��15��$��7��$��	_Localize��$
��Left=�$	��4,2q�$��60������ $ LocalizeCommand��� $��15��$��8��$
��Left=�$	��4,2q�$��60������ $ HoldCommand����� $ HoldCaption�