   M S B A M L   `   `   `  ���� c  _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  @ <NOTAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:NOTAM.BehaviorNOTAM.Behavior     NOTAM.View.IntlNotamSearch   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     1Behaviorclr-namespace:NOTAM.Behavior     .��� $��4q�.   (System.Windows.Interactivity.Interaction    	Behaviors  0  *NOTAM.Behavior.ValidationExceptionBehavior   � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $	��150 ��� $	��150 ��� $	��150 ��� $	��150 ��� $��* � � RowDefinitions �� $
��Auto ��� $��30 ��� $
��Auto ��� $��30 ��� $��30 ��� $
��Auto ��� $
��Auto ��� $
��128* ��� $��20 � � 	Resources  �� DataType-  'System.Windows.Controls.ValidationError �� Path��%k   (��       *, +)�� "  .R��� $��Italic$�
���  ��$
��Left=�$	��0,1q����� $ ErrorContent�.+��� $��0��$��0��$
��FIR:��$��Right=� �� Target ��  �� ElementName$ FIRTxt���� -    ��Name$ firTxt��$��0��$��1��	 �� 
IsEditable	 . ���� $ 
FirOptions�
 �� Mode$
 OneTime=� �� SelectedItem �� $	 FIR� �� ValidatesOnDataErrors . �� $��0��$��3��$��Origin:��$��Right=� �� $ 	OriginTxt���� -    ��Name$ 	OriginTxt��$��0��$��4������ $ 
OriginItem� .  �� UpdateSourceTrigger$ PropertyChanged=� =� ErrorTemplate Z� �� $��1��$��0��$��Aerodom:��$��Right=� �� $ 
AerodomTxt���� -   $ aeroTxt��$��1��$��1��	 . $��22������ $ AerodomOptions�$
 Default=�$ PropertyChanged=� �� $ Aerodom� . �� $��1��$��3��$��Type:��$��Right=� �� $ TypeTxt���� -   $ TypeTxt��$��1��$��4������ $
 Type� . $ PropertyChanged=� Z� �� $��2��$��0��$��Year:��$��Right=� �� $ YearTxt���� -   $ YearTxt��$��2��$��1������ $
 Year� . $ PropertyChanged=� Z� �� $��2��$��3��$��QCode:��$��Right=� �� $ QcodeTxt���� -   $ QcodeTxt��$��2��$��4������ $ QCode� . $ PropertyChanged=� Z� �� $��3��$��0��$��From Number��$��Right=� �� $ FromTxt���� -   $ FromTxt��$��3��$��1������ $
 From� . $ PropertyChanged=� Z� �� $��3��$��3��$��
To Number:��$��Right=� �� $ ToTxt���� -   $ ToTxt��$��3��$��4������ $ To� . $ PropertyChanged=� Z� �� $��4��$��0��$��	From Date��$��Right=� �� $ FromTxt���� -	   $ FromDateTxt��$��4��$��1������ $ FromDate� . $ PropertyChanged=� Z� �� $��4��$��3��$��To Date:��$��Right=� �� $ 	ToDateTxt���� -
   $ 	ToDateTxt��$��4��$��4������ $ ToDate� . $ PropertyChanged=� Z� �� $��5��$��0��$��Generate Report��$
��Left=�$��0,2,1,2q�$��70��$
��Auto��$
��Auto������ $ 
RunCommand��� $��5��$��1��$��Valid Notams��$
��Left=�$��5,2,5,2q�$��70��$
��Auto��$
��Auto������ $ ValidNotamsCommand��� $��5��$��2��$��Save to File��$
��Left=�$��	10,2,54,2q�$��70��$	��120��$
��Auto��$��3������ $ SaveCommand��� $��5��$��3��$��Archive Notams��$
��Left=�$��0,2,54,2q�$��70��$
��Auto��$
��Auto��$��3������ $ DisplayArchivesCommand��� $��5��$��5��$��Print��$
��Left=�$��0,2,31,2q�$��70��$
��Auto��$
��Auto��$��2������ $ PrintCommand��� $��5��$��4��$��Blanc NOTAMS��$
��Left=�$��0,2,31,2q�$��70��$
��Auto��$
��Auto��$��2������ $ BlancCommand��� $��5��$��6��$��GAP Analysis��$
��Left=�$��0,2,31,2q�$��70��$
��Auto��$
��Auto��$��2������ $ 
GAPCommand��� -   $ 
PreviewTxt��$��6��$��0��$��6�� �� AcceptsReturn .  �� MaxLines$ 20��$ �� HorizontalScrollBarVisibility$
 Auto=�" �� VerticalScrollBarVisibility$
 Auto=����� $ ReportResult� . $ PropertyChanged=� Z� �� -   $ GAPTxt��$��6��$��6��$��1�� . $ 20��$
 Auto=�$
 Auto=����� $ ImportedListText� . $ PropertyChanged=� Z� � $��8��$��0��$��6�� � Minimum$ 0Q� � Maximum$	 100Q�$y�Hidden=� � Value �� $ ProgressVal�