   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5   K   @ <NOTAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:NOTAM.BehaviorNOTAM.Behavior 6�   !   NOTAM.View.NotamSummaryView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     6K   1Behaviorclr-namespace:NOTAM.Behavior     6�   .��5      � $��4q�6   .   (System.Windows.Interactivity.Interaction    	Behaviors  5	   
   0  *NOTAM.Behavior.ValidationExceptionBehavior  5
       � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $	��100 �6   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $	��100 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��206* �6    � 	Resources 5   
    �� DataType-  'System.Windows.Controls.ValidationError �� Path��%�   (��       5      *, +)�� 6   "  6   .R�5      �� $��Italic$�5       
���  ��$
��Left=�5"      $	��0,1q�5#      ��5$      �� $ ErrorContent�5&      5'      .+�5+   
   �� $��0��5,      $��0��6   $��
From date:��5-      $��Right=�5.       �� Target 5/      ��  �� ElementName$ fromTxt���� 51   
   -   5         ��Name$ fromTxt��52      $��0��53      $��1��6   ��54      �� $ FromDate�	 �� ValidatesOnDataErrors	 . 
 �� UpdateSourceTrigger$
 PropertyChanged=� =� ErrorTemplate 55      Z� �� 58   
   $��0��59      $��3��6   $��To date:��5:      $��Right=�5;       5<      �� $ toTxt���� 5>   
   -   5        $ toTxt��5?      $��0��5@      $��4��6   ��5A      �� $ ToDate�	 . $
 PropertyChanged=� 5B      Z� �� 5F   
   $��1��5G      $��0��6   $��Notam type:��5H      $��Right=�5I       5J      �� $ typeTxt���� 5L   
   -   5        $ typeTxt��5M      $��1��5N      $��1��6   ��5O      �� $ NType�	 . $
 PropertyChanged=� 5P      Z� �� 5S   
   $��1��5T      $��3��6   $��List Of Valid NOTAMS��5V      $��Right=�5W      $��0,2,1,2q�5X      $��70��5Y      $	��160��6   $
��Auto��6!   ��5U      �� $ 
RunCommand�5Y   !   �� 5Z   
   -   5        $��button1��5Z   �   $��Create Check List��6   $��3��6-   $��3��6=   $
��Auto��6J   $
��Left=�6X   $��1,3,0,0q�6s   $	��Top=�6�   $	��160��5[      ��6   �� $ CheckListCommand�5\      5]      