   M S B A M L   `   `   `  ���� 	  NOTAM    NOTAM.View.OriginView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     $��Center=�$��Center=�$	��400��.��� $��4q�  � ColumnDefinitions  �� $
��Auto ��� $��6 ��� $��* � � RowDefinitions �� $
��Auto ��� $��30 ��� $
��Auto ��� $��30 ��� $
��Auto ��� $��30 ��� $
��Auto ��� $��30 ��� $
��Auto � � 	Resources  �� DataType-  'System.Windows.Controls.ValidationError �� Path��%l   (��       *, +)�� "  .R��� $��Italic$�
���  ��$��Right=�$	��0,1q����� $ ErrorContent�.+��� $��0��$��0��$��Code:��$��Right=� �� Target ��  �� ElementName$ codeTxt���� -    ��Name$ codeTxt��$��0��$��2������ $
 Code� �� ValidatesOnDataErrors . 	 �� UpdateSourceTrigger$	 PropertyChanged=�
 =� ErrorTemplate
 Z� �� $��1��$��2������ $ customerTypeCmb��$% (Validation.Errors).CurrentItem��� $��2��$��0��$��Name:��$��Right=� �� $ nameTxt���� -   $ nameTxt��$��2��$��2������ $
 Name� . $	 PropertyChanged=�
 Z� �� $��3��$��2������ $ nameTxt��$% (Validation.Errors).CurrentItem��� $��4��$��0��$��_Serial��$��Right=� �� $ 	serialTxt���� -   $ 	serialTxt��$��4��$��2������ $ Serial� . $	 PropertyChanged=�
 Z� �� $��5��$��2������ $ 	serialTxt��$% (Validation.Errors).CurrentItem��� $��6��$��0��$��_CenterType:��$��Right=� �� $ 	centerTxt���� -   $ 	centerTxt��$��6��$��2������ $ 
CenterType� . $	 PropertyChanged=�
 Z� �� $��7��$��2������ $ 	centerTxt��$% (Validation.Errors).CurrentItem��� $��8��$��2��$��_Save��$��Right=�$	��4,2q�$��60������ $ SaveCommand�