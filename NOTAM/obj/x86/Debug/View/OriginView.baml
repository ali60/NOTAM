   M S B A M L   `   `   `  ����	  NOTAM    NOTAM.View.OriginView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      $��Center=�5      $��Center=�5      $	��400��5      .��5      � $��4q�6     � ColumnDefinitions  5	   
   �� 5
      $
��Auto �6   �� 5      $��6 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6    � 	Resources 5   
    �� DataType-  'System.Windows.Controls.ValidationError �� Path��%�   (��       5      *, +)�� 6   "  6   .R�5      �� $��Italic$�5      
���  ��$��Right=�5       $	��0,1q�5!      ��5"      �� $ ErrorContent�5$      5%      .+�5(   
   �� $��0��5)      $��0��6   $��Code:��5*      $��Right=�5+       �� Target 5,      ��  �� ElementName$ codeTxt���� 5.   
   -   5         ��Name$ codeTxt��5/      $��0��50      $��2��6   ��51      �� $
 Code� �� ValidatesOnDataErrors . 	 �� UpdateSourceTrigger$	 PropertyChanged=�
 =� ErrorTemplate
 52      Z� �� 55   
   $��1��56      $��2��6   ��57      �� $ customerTypeCmb��$% (Validation.Errors).CurrentItem��� 5;   
   $��2��5<      $��0��6   $��Name:��5=      $��Right=�5>       5?      �� $ nameTxt���� 5A   
   -   5        $ nameTxt��5B      $��2��5C      $��2��6   ��5D      �� $
 Name� . $	 PropertyChanged=�
 5E      Z� �� 5G   
   $��3��5H      $��2��6   ��5I      �� $ nameTxt��$% (Validation.Errors).CurrentItem��� 5M   
   $��4��5N      $��0��6   $��_Serial��5O      $��Right=�5P       5Q      �� $ 	serialTxt���� 5S   
   -   5        $ 	serialTxt��5T      $��4��5U      $��2��6   ��5V      �� $ Serial� . $	 PropertyChanged=�
 5W      Z� �� 5Y   
   $��5��5Z      $��2��6   ��5[      �� $ 	serialTxt��$% (Validation.Errors).CurrentItem��� 5_   
   $��6��5`      $��0��6   $��_CenterType:��5a      $��Right=�5b       5c      �� $ 	centerTxt���� 5e   
   -   5        $ 	centerTxt��5f      $��6��5g      $��2��6   ��5h      �� $ 
CenterType� . $	 PropertyChanged=�
 5i      Z� �� 5k   
   $��7��5l      $��2��6   ��5m      �� $ 	centerTxt��$% (Validation.Errors).CurrentItem��� 5q   
   $��8��5r      $��2��6   $��_Save��5t      $��Right=�5u      $	��4,2q�5v      $��60��5w      ��5s      �� $ SaveCommand�5w      5y      5z      