   M S B A M L   `   `   `  ���� 	  NOTAM    NOTAM.View.AerodomView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .��� $��4q�  � ColumnDefinitions  �� $
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
 Z� �� $��3��$��2������ $ nameTxt��$% (Validation.Errors).CurrentItem��� $��4��$��0��$��FIR : ��$��Right=� �� $ firCmb���� -    ��Name$ firCmb��$��4��$��2�� �� 
IsEditable . ���� $ 
FIROptions� �� Mode$ OneTime=� �� SelectedItem �� $	 FIR� . 
 Z� �� $��5��$��2������ $ firCmb��$% (Validation.Errors).CurrentItem��� $��6��$��0��$��Country:��$��Right=� �� $ 
countryCmb���� -   $ 
countryCmb��$��6��$��2�� . ���� $ CountryOptions�$ OneTime=� �� $ Country� . 
 Z� �� $��7��$��2������ $ 
countryCmb��$% (Validation.Errors).CurrentItem��� $��8��$��0��$��
_Latitude:��$��Right=� �� $ latitudeTxt���� -   $ latitudeTxt��$��8��$��2������ $ Latitude� . $	 PropertyChanged=�
 Z� �� $��9��$��2������ $ latitudeTxt��$% (Validation.Errors).CurrentItem��� $��10��$��0��$��_Direction:��$��Right=� �� $ directionTxt���� -   $ directionTxt��$��10��$��2������ $ 	Direction� . $	 PropertyChanged=�
 Z� �� $��11��$��2������ $ directionTxt��$% (Validation.Errors).CurrentItem��� $��12��$��0��$��_VOR:��$��Right=� �� $ vorTxt���� -   $ vorTxt��$��12��$��2������ $	 VOR� . $	 PropertyChanged=�
 Z� �� $��13��$��2������ $ vorTxt��$% (Validation.Errors).CurrentItem��� $��14��$��0��$��_I_L:��$��Right=� �� $ ilTxt���� -   $ ilTxt��$��14��$��2������ $ IL� . $	 PropertyChanged=�
 Z� �� $��15��$��2������ $ ilTxt��$% (Validation.Errors).CurrentItem��� $��16��$��2��$��_Save��$��Right=�$	��4,2q�$��60������ $ SaveCommand�