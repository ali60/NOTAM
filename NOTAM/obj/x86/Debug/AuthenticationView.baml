   M S B A M L   `   `   `  ����@  <NOTAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:NOTAM.ViewModelNOTAM.ViewModel  5   @   clr-namespace:NOTAMNOTAM  6p   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 6�   /clr-namespace:NOTAM.BehaviorNOTAM.Behavior  6�       NOTAM.AuthenticationView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   3	ViewModelclr-namespace:NOTAM.ViewModel     6@   "vwclr-namespace:NOTAM     6p   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     6�   1Behaviorclr-namespace:NOTAM.Behavior     6�     � Title$  
Login Page��6  $	��300��6+  $
��300 ��67   � WindowStartupLocation$ CenterScreen=�5   	    � Icon$. (/NOTAM;component/Properties/Airplane.ico��6.    � 
ResizeMode$ NoResize=�6^    � WindowStyle$ ThreeDBorderWindow=�6t    � 	Resources 5          booleanToVisibilityConverter �� DataType-   'NOTAM.ViewModel.AuthenticationViewModel   NOTAM.AuthenticationView �� 
TargetType �� Property	 �� Value
 �� Triggers P� Property P� Value �� RelativeSource �� Path  MainHCCStyle  Brush_HeaderBackground �� CornerRadius �� Padding J� SnapsToDevicePixels �� 
TargetType  MainBorderStyle��%o  &	        (��      5      *, +)'�� E     'G� �     '�� �     &	 �    7  &	     �� 5   
   6'   �� 5   
   "  6   .R�5        5	      �� 5
   
   " ��6   .�5      ��  � ��$	 4q�5      5      �� 5   
   " G�6   .�5      ��  � ��$	 4q�5      5      �� 5   
   " ��6   .�5      ��  � ��$	 4q�5      
 5      P� 5       �@=�HasError . . �5      ��  �@�ToolTip	 5      ��  �� *Self+$) #(Validation.Errors)[0].ErrorContent�5      5      �� 5!   
   " ��6%   .�5"      ��  � ��	 5#      �� 5$      .R�5%      �� 8��  
�������$��1q�5(      $ 5��5)      $��4q�5*      $ 4q�5+       . ._�5.      �� $��14(�5/      $
��Bold"�50      
�������$��Center=�52      #��z��53      55      56      58      �� 59       � ��	 .  6*   �� 5:       � ��	 5;      �� 5@      " ��6&   .R�5A      ]� .+�5B      �� $	��Top=�5C      $��Header��5D      #��z��5E      �� 5G      $��Content��5H      #��z��5I      5K      5L      5N      5O      �� 5T   
   " ��6(   .�5U      ��  � ��
	 �����6+   �� 5V       � ��
	 �����6,   �� 5W       � ��$	 1q�60   �� 5X       �@��CornerRadius$	 5��6-   �� 5Y       �@J�SnapsToDevicePixels	 . 64   5Z      5]      .��5`      � .  (System.Windows.Interactivity.Interaction   	Behaviors 5b   
   0   *NOTAM.Behavior.ValidationExceptionBehavior  5c       5f   
   -  'System.Windows.Controls.ValidationError��%�   (��       5g      *, +)�� 6   "  6   .R�5h      �� $��Italic$�5i      
���  ��$��Right=�5k      $	��0,1q�5l      ��5m      �� $ ErrorContent�5o      5p       � ColumnDefinitions 5q   
   �� 5r      $
��Auto �6   �� 5s      $��* �6    � RowDefinitions 5u   
   �� 5v      $
��Auto �6   �� 5w      $
��Auto �6   �� 5x      $
��Auto �6   �� 5y      $
��Auto �6   �� 5z      $��* �6   .+�5|   
   �� $��0��67   $��0��6D   $��2��6T   $��Center=�5}      $
��Wrap=�6.   $
��Bold"�6B   $��2,2,2,2q�5~      ��5|      �� *AuthenticatedUser+5~      �� 5   
   $��	Username:��6   $��1��6%   $��0��62   y�6B   �� *	ShowLogin+ �� 	Converter# [  �� 5�   
   $��	Password:��6   $��2��6%   $��0��62   y�6B   �� *	ShowLogin+# [  �� 5�   
   $��1��6,   $��1��69   ��6   �� *
Username+y�6I   �� *	ShowLogin+# [  G� 5�   
   -   5        $��passwordBox��5�      $��2��6*   $��1��67   y�6G   �� *	Showlogin+# [  �� 5�   
    �� Orientation$ 
Horizontal=�6   $��3��6.   $��1��6;   .+�5�      �� $��Log in��6   $��Center=�5�      ��5�   &   �� *LoginCommand+ �� Mode$ OneTime=���5�      ��  �� ElementName$ passwordBox��5�      �� 5�      $��Log out��6   $��Center=�5�      $��2,0,0,0q�6*   ��5�   '   �� *LogoutCommand+$ OneTime=�y�5�      �� *IsAuthenticated+# [  5�   *   5�      �� 5�   
   $��4��6,   $��1��69   $��Center=�5�      $��Center=�6-   
���  ��$
��Wrap=�6Y   ��5�      �� *Status+5�   Y   5�      5�      