   M S B A M L   `   `   `  ����	  NOTAM    NOTAM.View.AerodomView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      � $��4q�6     � ColumnDefinitions  5   
   �� 5	      $
��Auto �6   �� 5
      $��6 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6    � 	Resources 5"   
    �� DataType-  'System.Windows.Controls.ValidationError �� Path��%�   (��       5#      *, +)�� 6   "  6   .R�5$      �� $��Italic$�5%      
���  ��$��Right=�5'      $	��0,1q�5(      ��5)      �� $ ErrorContent�5+      5,      .+�5/   
   �� $��0��50      $��0��6   $��Code:��51      $��Right=�52       �� Target 53      ��  �� ElementName$ codeTxt���� 55   
   -   5         ��Name$ codeTxt��56      $��0��57      $��2��6   ��58      �� $
 Code� �� ValidatesOnDataErrors . 	 �� UpdateSourceTrigger$	 PropertyChanged=�
 =� ErrorTemplate
 59      Z� �� 5<   
   $��1��5=      $��2��6   ��5>      �� $ customerTypeCmb��$% (Validation.Errors).CurrentItem��� 5B   
   $��2��5C      $��0��6   $��Name:��5D      $��Right=�5E       5F      �� $ nameTxt���� 5H   
   -   5        $ nameTxt��5I      $��2��5J      $��2��6   ��5K      �� $
 Name� . $	 PropertyChanged=�
 5L      Z� �� 5N   
   $��3��5O      $��2��6   ��5P      �� $ nameTxt��$% (Validation.Errors).CurrentItem��� 5T   
   $��4��5U      $��0��6   $��FIR : ��5V      $��Right=�5W       5X      �� $ firCmb���� 5Z   
   -   5         ��Name$ firCmb��5[      $��4��5\      $��2��6    �� 
IsEditable . ��5]      �� $ 
FIROptions� �� Mode$ OneTime=� �� SelectedItem 5^      �� $	 FIR� . 
 5_      Z� 6+   �� 5`   
   $��5��5a      $��2��6   ��5b      �� $ firCmb��$% (Validation.Errors).CurrentItem��� 5f   
   $��6��5g      $��0��6   $��Country:��5h      $��Right=�5i       5j      �� $ 
countryCmb���� 5m   
   -   5        $ 
countryCmb��5n      $��6��5o      $��2��6    . ��5p      �� $ CountryOptions�$ OneTime=� 5q      �� $ Country� . 
 5r      Z� 6+   �� 5s   
   $��7��5t      $��2��6   ��5u      �� $ 
countryCmb��$% (Validation.Errors).CurrentItem��� 5y   
   $��8��5z      $��0��6   $��
_Latitude:��5{      $��Right=�5|       5}      �� $ latitudeTxt���� 5   
   -   5        $ latitudeTxt��5�      $��8��5�      $��2��6   ��5�      �� $ Latitude� . $	 PropertyChanged=�
 5�      Z� �� 5�   
   $��9��5�      $��2��6   ��5�      �� $ latitudeTxt��$% (Validation.Errors).CurrentItem��� 5�   
   $��10��5�      $��0��6   $��_Direction:��5�      $��Right=�5�       5�      �� $ directionTxt���� 5�   
   -   5        $ directionTxt��5�      $��10��5�      $��2��6   ��5�      �� $ 	Direction� . $	 PropertyChanged=�
 5�      Z� �� 5�   
   $��11��5�      $��2��6   ��5�      �� $ directionTxt��$% (Validation.Errors).CurrentItem��� 5�   
   $��12��5�      $��0��6   $��_VOR:��5�      $��Right=�5�       5�      �� $ vorTxt���� 5�   
   -   5        $ vorTxt��5�      $��12��5�      $��2��6   ��5�      �� $	 VOR� . $	 PropertyChanged=�
 5�      Z� �� 5�   
   $��13��5�      $��2��6   ��5�      �� $ vorTxt��$% (Validation.Errors).CurrentItem��� 5�   
   $��14��5�      $��0��6   $��_I_L:��5�      $��Right=�5�       5�      �� $ ilTxt���� 5�   
   -   5        $ ilTxt��5�      $��14��5�      $��2��6   ��5�      �� $ IL� . $	 PropertyChanged=�
 5�      Z� �� 5�   
   $��15��5�      $��2��6   ��5�      �� $ ilTxt��$% (Validation.Errors).CurrentItem��� 5�   
   $��16��5�      $��2��6   $��_Save��5�      $��Right=�5�      $	��4,2q�5�      $��60��5�      ��5�      �� $ SaveCommand�5�      5�      5�      