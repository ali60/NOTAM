   M S B A M L   `   `   `  ����@  <NOTAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:NOTAM.BehaviorNOTAM.Behavior  5   K       NOTAM.View.SettingView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1Behaviorclr-namespace:NOTAM.Behavior     6K   .��5      � $��4q�6     � ColumnDefinitions  5   
   �� 5	      $
��Auto �6   �� 5
      $��6 �6   �� 5      $	��100 �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6    � 	Resources 5   
    �� DataType-  'System.Windows.Controls.ValidationError �� Path��%�   (��       5      *, +)�� 6   "  6   .R�5      �� $��Italic$�5      
���  ��$��Right=�5      $	��0,1q�5       ��5!      �� $ ErrorContent�5#      5%      .+�5(   
   �� $��0��5)      $��0��6   $��Number Of Print ��5*      $��Right=�5+       �� Target 5,      ��  �� ElementName$ userTxt���� 5.   
   -   5         ��Name$ userTxt��5/       �� CharacterCasing$ Normal=�6   $��0��50      $��2��6   $��50��6$   ��51      �� $ 
PrintCount�	 �� ValidatesOnDataErrors	 . 
 �� UpdateSourceTrigger$
 PropertyChanged=��� 5A   
   -   5         ��Name$ lblError��5A      $��5��5B      $��2��6   
���  ����5C      �� $ PasswordValidator��� 5F   
   -   5        $ 	lblError2��5F      $��5��5G      $��2��6   
���  ����5H      �� $ PasswordError�6.   &    NOTAM.Behavior.PasswordValidator  5K   
      Box1 5L      �� $ passBox��   Box2 5M      �� $ pass2Box��   ErrorMsg 5N      �� $ lblError���� 5S   
   $��1��5T      $��0��6   $��Support Laser Printer��5U      $��Right=�5V       5W      �� $ 
adminCheck���� 5Y   
   -   5         ��Name$ 
adminCheck��5Z      $��1��5[      $��2��6   $��0,6,0,24q�5]   +   $��3��6=    d� 	IsChecked 5\      �� $ IsLaser�	 . $
 PropertyChanged=� =� ErrorTemplate 5]      Z� 6=   �� 5^   
   $��2��5_      $��2��6   ��5`      �� $ 
adminCheck��$% (Validation.Errors).CurrentItem��� 5d   
   $��8��5e      $��2��6   $��_Save��5g      $��Right=�5h      $	��4,2q�5i      $��60��5j      ��5f      �� $ SaveCommand�5j      5l      5n      