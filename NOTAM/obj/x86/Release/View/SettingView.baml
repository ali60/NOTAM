   M S B A M L   `   `   `  ���� @  <NOTAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:NOTAM.BehaviorNOTAM.Behavior      NOTAM.View.SettingView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1Behaviorclr-namespace:NOTAM.Behavior     .��� $��4q�  � ColumnDefinitions  �� $
��Auto ��� $��6 ��� $	��100 � � RowDefinitions �� $
��Auto ��� $��30 ��� $
��Auto ��� $��30 ��� $
��Auto ��� $��30 ��� $
��Auto ��� $��30 ��� $
��Auto � � 	Resources  �� DataType-  'System.Windows.Controls.ValidationError �� Path��%l   (��       *, +)�� "  .R��� $��Italic$�
���  ��$��Right=�$	��0,1q����� $ ErrorContent�.+��� $��0��$��0��$��Number Of Print ��$��Right=� �� Target ��  �� ElementName$ userTxt���� -    ��Name$ userTxt�� �� CharacterCasing$ Normal=�$��0��$��2��$��50������ $ 
PrintCount�	 �� ValidatesOnDataErrors	 . 
 �� UpdateSourceTrigger$
 PropertyChanged=��� -    ��Name$ lblError��$��5��$��2��
���  ������ $ PasswordValidator��� -   $ 	lblError2��$��5��$��2��
���  ������ $ PasswordError�&    NOTAM.Behavior.PasswordValidator     Box1 �� $ passBox��   Box2 �� $ pass2Box��   ErrorMsg �� $ lblError���� $��1��$��0��$��Support Laser Printer��$��Right=� �� $ 
adminCheck���� -    ��Name$ 
adminCheck��$��1��$��2��$��0,6,0,24q�$��3�� d� 	IsChecked �� $ IsLaser�	 . $
 PropertyChanged=� =� ErrorTemplate Z� �� $��2��$��2������ $ 
adminCheck��$% (Validation.Errors).CurrentItem��� $��8��$��2��$��_Save��$��Right=�$	��4,2q�$��60������ $ SaveCommand�