   M S B A M L   `   `   `  ����	  NOTAM    NOTAM.View.AftnView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      � $��4q�6     � ColumnDefinitions  5   
   �� 5      $
��Auto �6   �� 5	      $��6 �6   �� 5
      $
��Auto �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6    � 	Resources 5   
    �� DataType-  'System.Windows.Controls.ValidationError �� Path��%�   (��       5      *, +)�� 6   "  6   .R�5      �� $��Italic$�5      
���  ��$��Right=�5      $	��0,1q�5      ��5       �� $ ErrorContent�5"      5#      .+�5&   
   �� $��0��5'      $��0��6   $��Series:��5(      $��Right=�5)       �� Target 5*      ��  �� ElementName$ 	seriesTxt���� 5,   
   -   5         ��Name$ 	seriesTxt��5-      $��0��5.      $��2��6   ��5/      �� $ Series� �� ValidatesOnDataErrors . 	 �� UpdateSourceTrigger$	 PropertyChanged=�
 =� ErrorTemplate
 50      Z� �� 53   
   $��0��54      $��2��6   ��55      �� $ 	seriesTxt��$% (Validation.Errors).CurrentItem��� 59   
   $��2��5:      $��0��6   $
��Aftn��5;      $��Right=�5<       5=      �� $ aftnTxt���� 5?   
   -   5        $ aftnTxt��5@      $��2��5A      $��2��6   ��5B      �� $ NewAftn� . $	 PropertyChanged=�
 5C      Z� �� 5E   
   $��2��5F      $��3��6   $��+��5H      ��5G      �� $ 
AddCommand�5H      �� 5N   
   $��4��5O      $��0��6   $��
Aftn List:��5P      $��Right=�5Q       5R      �� $ listTxt���� 5U   
   $��4��68   $��2��6E   ��6   �� *
AftnList+��5V      �� 5W      .R�5Y      ��  �� Orientation$ Vertical=�6%   .+�5Z      �� $��Center=�5[   +   ��5Z   '   �� $ .� . $	 PropertyChanged=�
 5[      Z� 6+   5]      5_      ��5a      �� 5b      .R�5c      A�  A� Columns$ 5��6"   5d      5f      �� 5h   
   $��3��5i      $��2��6   ��5j      �� $ nameTxt��$% (Validation.Errors).CurrentItem��� 5p   
   $��16��5q      $��2��6   $��_Save��5s      $��Right=�5t      $	��4,2q�5u      $��60��5v      ��5r      �� $ SaveCommand�5v      5x      5y      