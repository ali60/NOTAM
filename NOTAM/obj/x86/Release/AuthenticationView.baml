   M S B A M L   `   `   `  ���� @  <NOTAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:NOTAM.ViewModelNOTAM.ViewModel  clr-namespace:NOTAMNOTAM  c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity /clr-namespace:NOTAM.BehaviorNOTAM.Behavior      NOTAM.AuthenticationView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     3	ViewModelclr-namespace:NOTAM.ViewModel     "vwclr-namespace:NOTAM     ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     1Behaviorclr-namespace:NOTAM.Behavior       � Title$  
Login Page��$	��300��$
��300 �� � WindowStartupLocation$ CenterScreen=� � Icon$. (/NOTAM;component/Properties/Airplane.ico�� � 
ResizeMode$ NoResize=� � WindowStyle$ ThreeDBorderWindow=� � 	Resources     booleanToVisibilityConverter �� DataType-   'NOTAM.ViewModel.AuthenticationViewModel   NOTAM.AuthenticationView �� 
TargetType �� Property	 �� Value
 �� Triggers P� Property P� Value �� RelativeSource �� Path  MainHCCStyle  Brush_HeaderBackground �� CornerRadius �� Padding J� SnapsToDevicePixels �� 
TargetType  MainBorderStyle��%�  &	        (��      *, +)'��      'G� 9     '�� [     &	 �     7  &	     �� �� "  .R�  �� " ��.���  � ��$	 4q��� " G�.���  � ��$	 4q��� " ��.���  � ��$	 4q�
 P�  �@=�HasError . . ���  �@�ToolTip	 ��  �� *Self+$) #(Validation.Errors)[0].ErrorContent��� " ��.���  � ��	 �� .R��� 8��  
�������$��1q�$ 5��$��4q�$ 4q� . ._��� $��14(�$
��Bold"�
�������$��Center=�#��z����  � ��	 .  ��  � ��	 �� " ��.R�]� .+��� $	��Top=�$��Header��#��z���� $��Content��#��z���� " ��.���  � ��
	 �������  � ��
	 �������  � ��$	 1q���  �@��CornerRadius$	 5����  �@J�SnapsToDevicePixels	 . .��� .  (System.Windows.Interactivity.Interaction   	Behaviors 0   *NOTAM.Behavior.ValidationExceptionBehavior   -  'System.Windows.Controls.ValidationError��%l   (��       *, +)�� "  .R��� $��Italic$�
���  ��$��Right=�$	��0,1q����� $ ErrorContent� � ColumnDefinitions �� $
��Auto ��� $��* � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �.+��� $��0��$��0��$��2��$��Center=�$
��Wrap=�$
��Bold"�$��2,2,2,2q����� *AuthenticatedUser+�� $��	Username:��$��1��$��0��y��� *	ShowLogin+ �� 	Converter# [  �� $��	Password:��$��2��$��0��y��� *	ShowLogin+# [  �� $��1��$��1������ *
Username+y��� *	ShowLogin+# [  G� -   $��passwordBox��$��2��$��1��y��� *	Showlogin+# [  ��  �� Orientation$ 
Horizontal=�$��3��$��1��.+��� $��Log in��$��Center=����� *LoginCommand+ �� Mode$ OneTime=�����  �� ElementName$ passwordBox���� $��Log out��$��Center=�$��2,0,0,0q����� *LogoutCommand+$ OneTime=�y��� *IsAuthenticated+# [  �� $��4��$��1��$��Center=�$��Center=�
���  ��$
��Wrap=����� *Status+