   M S B A M L   `   `   `  ���� c  _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  @ <NOTAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:NOTAM.BehaviorNOTAM.Behavior !   NOTAM.View.NotamSummaryView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     1Behaviorclr-namespace:NOTAM.Behavior     .��� $��4q�.   (System.Windows.Interactivity.Interaction    	Behaviors  0  *NOTAM.Behavior.ValidationExceptionBehavior   � ColumnDefinitions �� $
��Auto ��� $	��100 ��� $��* ��� $
��Auto ��� $	��100 ��� $��* � � RowDefinitions �� $
��Auto ��� $��30 ��� $
��Auto ��� $��30 ��� $
��206* � � 	Resources  �� DataType-  'System.Windows.Controls.ValidationError �� Path��%k   (��       *, +)�� "  .R��� $��Italic$�
���  ��$
��Left=�$	��0,1q����� $ ErrorContent�.+��� $��0��$��0��$��
From date:��$��Right=� �� Target ��  �� ElementName$ fromTxt���� -    ��Name$ fromTxt��$��0��$��1������ $ FromDate�	 �� ValidatesOnDataErrors	 . 
 �� UpdateSourceTrigger$
 PropertyChanged=� =� ErrorTemplate Z� �� $��0��$��3��$��To date:��$��Right=� �� $ toTxt���� -   $ toTxt��$��0��$��4������ $ ToDate�	 . $
 PropertyChanged=� Z� �� $��1��$��0��$��Notam type:��$��Right=� �� $ typeTxt���� -   $ typeTxt��$��1��$��1������ $ NType�	 . $
 PropertyChanged=� Z� �� $��1��$��3��$��List Of Valid NOTAMS��$��Right=�$��0,2,1,2q�$��70��$	��160��$
��Auto������ $ 
RunCommand��� -   $��button1��$��Create Check List��$��3��$��3��$
��Auto��$
��Left=�$��1,3,0,0q�$	��Top=�$	��160������ $ CheckListCommand�