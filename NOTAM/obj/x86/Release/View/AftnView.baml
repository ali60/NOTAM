   M S B A M L   `   `   `  ���� 	  NOTAM    NOTAM.View.AftnView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .��� $��4q�  � ColumnDefinitions  �� $
��Auto ��� $��6 ��� $
��Auto ��� $��20 � � RowDefinitions �� $
��Auto ��� $��30 ��� $
��Auto ��� $��30 ��� $
��Auto ��� $��30 ��� $
��Auto ��� $��30 � � 	Resources  �� DataType-  'System.Windows.Controls.ValidationError �� Path��%l   (��       *, +)�� "  .R��� $��Italic$�
���  ��$��Right=�$	��0,1q����� $ ErrorContent�.+��� $��0��$��0��$��Series:��$��Right=� �� Target ��  �� ElementName$ 	seriesTxt���� -    ��Name$ 	seriesTxt��$��0��$��2������ $ Series� �� ValidatesOnDataErrors . 	 �� UpdateSourceTrigger$	 PropertyChanged=�
 =� ErrorTemplate
 Z� �� $��0��$��2������ $ 	seriesTxt��$% (Validation.Errors).CurrentItem��� $��2��$��0��$
��Aftn��$��Right=� �� $ aftnTxt���� -   $ aftnTxt��$��2��$��2������ $ NewAftn� . $	 PropertyChanged=�
 Z� �� $��2��$��3��$��+������ $ 
AddCommand��� $��4��$��0��$��
Aftn List:��$��Right=� �� $ listTxt���� $��4��$��2������ *
AftnList+���� .R���  �� Orientation$ Vertical=�.+��� $��Center=����� $ .� . $	 PropertyChanged=�
 Z� ���� .R�A�  A� Columns$ 5���� $��3��$��2������ $ nameTxt��$% (Validation.Errors).CurrentItem��� $��16��$��2��$��_Save��$��Right=�$	��4,2q�$��60������ $ SaveCommand�