   M S B A M L   `   `   `  ���� 	  NOTAM    NOTAM.View.FIRView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     $��Center=�$��Center=�$	��400��.��� $��4q�  � ColumnDefinitions  �� $
��Auto ��� $��6 ��� $��* � � RowDefinitions �� $
��Auto ��� $��30 ��� $
��Auto ��� $��30 ��� $
��Auto ��� $��30 ��� $
��Auto ��� $��30 ��� $
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
 Z� �� $��3��$��2������ $ nameTxt��$% (Validation.Errors).CurrentItem��� $��4��$��0��$��	Origin : ��$��Right=� �� $ 	originCmb���� -    ��Name$ 	originCmb��$��4��$��2�� �� 
IsEditable . ���� $ OriginOptions� �� Mode$ OneTime=� �� SelectedItem �� $ Origin� . 
 Z� �� $��5��$��2������ $ 	originCmb��$% (Validation.Errors).CurrentItem��� $��6��$��0��$��AtcCode:��$��Right=� �� $ atcTxt���� -   $ atcTxt��$��6��$��2������ $ AtcCode� . $	 PropertyChanged=�
 Z� �� $��7��$��2������ $ 	serialTxt��$% (Validation.Errors).CurrentItem��� $��8��$��0��$��_NotrhLimit:��$��Right=� �� $ northTxt���� -   $ northTxt��$��8��$��2������ $ 
NorthLimit� . $	 PropertyChanged=�
 Z� �� $��9��$��2������ $ northTxt��$% (Validation.Errors).CurrentItem��� $��10��$��0��$��_SouthLimit:��$��Right=� �� $ southTxt���� -   $ southTxt��$��10��$��2������ $ 
SouthLimit� . $	 PropertyChanged=�
 Z� �� $��11��$��2������ $ southTxt��$% (Validation.Errors).CurrentItem��� $��12��$��0��$��_WestLimit:��$��Right=� �� $ westTxt���� -   $ westTxt��$��12��$��2������ $ 	WestLimit� . $	 PropertyChanged=�
 Z� �� $��13��$��2������ $ westTxt��$% (Validation.Errors).CurrentItem��� $��14��$��0��$��_EastLimit:��$��Right=� �� $ eastTxt���� -   $ eastTxt��$��14��$��2������ $ 	EastLimit� . $	 PropertyChanged=�
 Z� �� $��15��$��2������ $ eastTxt��$% (Validation.Errors).CurrentItem��� $��16��$��2��$��_Save��$��Right=�$	��4,2q�$��60������ $ SaveCommand�