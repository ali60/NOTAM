   M S B A M L   `   `   `  ����@  <NOTAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:NOTAM.BehaviorNOTAM.Behavior  5   K       NOTAM.View.UserView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1Behaviorclr-namespace:NOTAM.Behavior     6K   .��5      � $��4q�6     � ColumnDefinitions  5   
   �� 5	      $
��Auto �6   �� 5
      $��6 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6   �� 5      $��30 �6   �� 5      $
��Auto �6    � 	Resources 5   
    �� DataType-  'System.Windows.Controls.ValidationError �� Path��%�   (��       5      *, +)�� 6   "  6   .R�5      �� $��Italic$�5      
���  ��$��Right=�5      $	��0,1q�5       ��5!      �� $ ErrorContent�5#      5%      .+�5(   
   �� $��0��5)      $��0��6   $��	Username:��5*      $��Right=�5+       �� Target 5,      ��  �� ElementName$ userTxt���� 5.   
   -   5         ��Name$ userTxt��5/       �� CharacterCasing$ Normal=�6   $��0��50      $��2��6   ��51      �� $ Username�	 �� ValidatesOnDataErrors	 . 
 �� UpdateSourceTrigger$
 PropertyChanged=��� 56   
   $��2��57      $��0��6   $��	Password:��58      $��Right=�59       5:      �� $ passBox��G� 5=   
   -   5         G�Name$ passBox��5=      $��2��6'   $��2��64   �� 5I   
   $��4��5J      $��0��6   $��Repeat Password��5K      $��Right=�5L       5M      �� $ pass2Box��G� 5P   
   -   5        $ pass2Box��5P      $��4��6(   $��2��65   �� 5S   
   -   5         ��Name$ lblError��5S      $��5��5T      $��2��6   
���  ����5U      �� $ PasswordValidator��� 5X   
   -   5        $ 	lblError2��5X      $��5��5Y      $��2��6   
���  ����5Z      �� $ PasswordError�6.   &    NOTAM.Behavior.PasswordValidator  5]   
      Box1 5^      �� $ passBox��   Box2 5_      �� $ pass2Box��   ErrorMsg 5`      �� $ lblError���� 5e   
   $��6��5f      $��0��6   $��IsAdmin:��5g      $��Right=�5h       5i      �� $ 
adminCheck���� 5k   
   -   5         ��Name$ 
adminCheck��5l      $��6��5m      $��2��6    d� 	IsChecked 5n      �� $ IsAdmin�	 . $
 PropertyChanged=� =� ErrorTemplate 5o      Z� �� 5q   
   $��7��5r      $��2��6   ��5s      �� $ 
adminCheck��$% (Validation.Errors).CurrentItem��� 5w   
   $��8��5x      $��2��6   $��_Save��5{      $��Right=�5|      $	��4,2q�5}      $��60��5~      ��5y      �� $ SaveCommand���5z      �� $ passBox��5~      5�      5�      