   M S B A M L   `   `   `  ���� 	  NOTAM    NOTAM.View.NotamCodeView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .��� $��4q�  � ColumnDefinitions  �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* � � RowDefinitions �� $��30 ��� $
��Auto ��� $��30 ��� $
��Auto ��� $��30 ��� $
��Auto ��� $��30 ��� $
��Auto ��� $��40 ��� $
��Auto � � 	Resources  �� DataType-  'System.Windows.Controls.ValidationError �� Path��%k   (��       *, +)�� "  .R��� $��Italic$�
���  ��$
��Left=�$	��0,1q����� $ ErrorContent�.+��� $��0��$��0��$��Subject��$
��Left=� �� Target ��  �� ElementName$ 
subjectTxt���� -    ��Name$ 
subjectTxt��$��0��$��1������ $ Subject� �� ValidatesOnDataErrors . 	 �� UpdateSourceTrigger$	 PropertyChanged=�
 =� ErrorTemplate
 Z� �� $��0��$��2��$��Description��$
��Left=� �� $ subjectDescTxt���� -   $ subjectDescTxt�� �� CharacterCasing$ Normal=�$��0��$��3������ $ Subject_Desc� . $	 PropertyChanged=�
 Z� �� $��2��$��0��$��
Condition:��$
��Left=� �� $ conditionTxt���� -   $ conditionTxt��$��2��$��1������ $ 	Condition� . $	 PropertyChanged=�
 Z� �� $��2��$��2��$��Description��$
��Left=� �� $ conditionDescTxt���� -   $ conditionDescTxt��$ Normal=�$��2��$��3������ $ Condition_Desc� . $	 PropertyChanged=�
 Z� �� $��4��$��0��$��Scope:��$
��Left=� �� $ scopeTxt���� -   $ scopeTxt��$��4��$��1������ $ Scope� . $	 PropertyChanged=�
 Z� �� $��4��$��2��$��	Category:��$
��Left=� �� $ categoryTxt���� -   $ categoryTxt��$��4��$��3������ $ Category� . $	 PropertyChanged=�
 Z� �� $��6��$��0��$��Traffic:��$
��Left=� �� $ 
trafficTxt���� -   $ 
trafficTxt��$��6��$��1������ $ Traffic� . $	 PropertyChanged=�
 Z� �� $��6��$��2��$��Purpose:��$
��Left=� �� $ 
purposeTxt���� -   $ 
purposeTxt��$��6��$��3������ $ Purpose� . $	 PropertyChanged=�
 Z� �� $��8��$��3��$��_Save��$
��Left=�$	��4,2q�$��60������ $ SaveCommand�