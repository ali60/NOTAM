   M S B A M L   `   `   `  ����@  <NOTAM, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:NOTAM.ViewModelNOTAM.ViewModel  5      'clr-namespace:NOTAM.View
NOTAM.View  5      /clr-namespace:NOTAM.BehaviorNOTAM.Behavior  6'   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� DataType)    #NOTAM.ViewModel.AllOriginsViewModel   NOTAM.View.AllOriginsView%   NOTAM.ViewModel.OriginViewModel   NOTAM.View.OriginView+   %NOTAM.ViewModel.NotamSummaryViewModel!   NOTAM.View.NotamSummaryView&    NOTAM.ViewModel.AllFIRsViewModel   NOTAM.View.AllFIRsView"   NOTAM.ViewModel.FIRViewModel	   NOTAM.View.FIRView$
   NOTAM.ViewModel.NotamViewModel   NOTAM.View.NotamView*   $NOTAM.ViewModel.NotamDetailViewModel    NOTAM.View.NotamDetailView.   (NOTAM.ViewModel.IntlNotamDetailViewModel$   NOTAM.View.IntlNotamDetailView,   &NOTAM.ViewModel.SnowtamDetailViewModel"   NOTAM.View.SnowtamDetailView(   "NOTAM.ViewModel.AllNotamsViewModel   NOTAM.View.AllNotamsView,   &NOTAM.ViewModel.AllIntlNotamsViewModel"   NOTAM.View.AllIntlNotamsView(   "NOTAM.ViewModel.IntlNotamViewModel   NOTAM.View.IntlNotamView*   $NOTAM.ViewModel.AllSnowtamsViewModel   NOTAM.View.AllSnowtamView&    NOTAM.ViewModel.AerodomViewModel   NOTAM.View.AerodomView*   $NOTAM.ViewModel.AllAerodomsViewModel    NOTAM.View.AllAerodomsView(   "NOTAM.ViewModel.NotamCodeViewModel   NOTAM.View.NotamCodeView,    &NOTAM.ViewModel.AllNotamCodesViewModel"!   NOTAM.View.AllNotamCodesView("   "NOTAM.ViewModel.NotamTextViewModel#   NOTAM.View.NotamTextView&$    NOTAM.ViewModel.SnowtamViewModel%   NOTAM.View.SnowtamView&&    NOTAM.ViewModel.SettingViewModel'   NOTAM.View.SettingView#(   NOTAM.ViewModel.UserViewModel)   NOTAM.View.UserView'*   !NOTAM.ViewModel.AllUsersViewModel+   NOTAM.View.AllUsersView',   !NOTAM.ViewModel.AllAftnsViewModel-   NOTAM.View.AllAftnsView#.   NOTAM.ViewModel.AftnViewModel/   NOTAM.View.AftnView �� 
TargetType �� Property �� Value �� Triggers P� Property P� Value �� RelativeSource �� Path&0    NOTAM.Behavior.PasswordValidator%1   NOTAM.Behavior.ComboBoxBehavior   MainHCCStyle  Brush_HeaderBackground	 �� CornerRadius
 �� Padding J� SnapsToDevicePixels �� 
TargetType  MainBorderStyle  CommandsTemplate �� Command  ClosableTabItemTemplate � Cursor  WorkspacesTemplate$ �� IsSynchronizedWithCurrentItem*2   $NOTAM.ViewModel.NotamSearchViewModel 3   NOTAM.View.NotamSearchView/4   )NOTAM.ViewModel.IntlNotamsSearchViewModel 5   NOTAM.View.IntlNotamSearch�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      $vmclr-namespace:NOTAM.ViewModel  5      vwclr-namespace:NOTAM.View  5      )Behaviorclr-namespace:NOTAM.Behavior  6'   %�  (��       5      *,  +)(�� 2     5      *, +)(�� d     5      *, +)(�� �     5#      *, +)(�� �     5+      *, +)(�� �     5/      *,
 +)(�� ,    53      *, +)(�� ^    57      *, +)(�� �    5;      *, +)(�� �    5?      *, +)(�� �    5C      *, +)(�� &    5F      *, +)(�� X    5J      *, +)(�� �    5N      *, +)(�� �    5R      *, +)(�� �    5V      *, +)(��      5Z      *,  +)(�� R    5^      *," +)(�� �    5b      *,$ +)(�� �    5f      *,& +)(�� �    5j      *,( +)(��     5n      *,* +)(�� L    5r      *,, +)(�� ~    5v      *,. +)'�� �    '�� !    '0  t    '�� @    '	� �    &	      7  &	 W
    &	 P    &	 ^    &	 	    7  (�� �    5     *,2 +)(�� �    5      *,4 +)�� 5      "    6   .R�5   
     5      �� 5      "   6   .R�5   
     5      �� 5      "   6   .R�5   
     5      �� 5#      "   6   .R�5$   
     5%      �� 5+      "   6   .R�5,   
   	  5-      �� 5/      "  
 6   .R�50   
     51      �� 53      "   6   .R�54   
     55      �� 57      "   6   .R�58   
     59      �� 5;      "   6   .R�5<   
     5=      �� 5?      "   6   .R�5@   
     5A      �� 5C      "   6   .R�5D   
     5E      �� 5F      "   6   .R�5G   
     5H      �� 5J      "   6   .R�5K   
     5L      �� 5N      "   6   .R�5O   
     5P      �� 5R      "   6   .R�5S   
     5T      �� 5V      "   6   .R�5W   
     5X      �� 5Z      "    6   .R�5[   
   !  5\      �� 5^      "  " 6   .R�5_   
   #  5`      �� 5b      "  $ 6   .R�5c   
   %  5d      �� 5f      "  & 6   .R�5g   
   '  5h      �� 5j      "  ( 6   .R�5k   
   )  5l      �� 5n      "  * 6   .R�5o   
   +  5p      �� 5r      "  , 6   .R�5s   
   -  5t      �� 5v      "  . 6   .R�5w   
   /  5x      �� 5~      " ��6   .�5   
   ��  � ��$ 4q�5�      �� 5�   
    � ��$
 Bold"�6'   5�      �� 5�      " ��6   .�5�   
   ��  � ��$ 4q�5�      �� 5�   
    � ��$
 Bold"�6'   �� 5�   
    �@��CharacterCasing$ Upper=�6,    5�   
   P� 5�       �@=�HasError . . �5�      ��  �@�ToolTip 5�      ��  �� *Self+$) #(Validation.Errors)[0].ErrorContent�5�      5�      �� 5�      " 0 6    5�   
   P� 5�       �@=�HasError . . �5�      ��  �@�ToolTip 5�      ��  �� *Self+$) #(Validation.Errors)[0].ErrorContent�5�      5�      �� 5�      " ��6   .�5�   
   ��  � ��$
 Bold"�6'   �� 5�   
    �@1 CharacterCasing$ Upper=�6F   5�      �� 5�      " 	�6   .�5�   
   ��  � ��$
 Bold"�6'   5�      �� 5�      " ��6!   .�5�   
   ��  � �� 5�      �� 5�      .R�5�      �� 8��  
�������$��1q�5�      $	 5��5�      $��4q�5�      $
 4q�5�       . ._�5�      �� $��14(�5�      $
��Bold"�5�      
�������$��Center=�5�      #��z��5�      5�      5�      5�      �� 5�   
    � �� .  6&   �� 5�   
    � �� 5�      �� 5�      " ��6"   .R�5�      ]� .+�5�      �� $	��Top=�5�      $��Header��5�      #��z��5�      �� 5�      $��Content��5�      #��z��5�      5�      5�      5�      5�      �� 5�      " ��6$   .�5�   
   ��  � ��
 �����6'   �� 5�   
    � ��
 �����6(   �� 5�   
    � ��$ 1q�6,   �� 5�   
    �@��CornerRadius$ 5��6)   �� 5�   
    �@J�SnapsToDevicePixels . 60   5�      �� 5�      .R�5�   
   �� ��.  $	��6,2q�6A   ��6)   �� ��5�      �� 5�      .R�5�      �� $	��2,6q�6    �� 5�       6   �� $ Command�.�69   .�5�      �� ��6   �� $ DisplayName�5�      5�      5�      5�      5�      �� 5�      .R�5�   
   ]� $	��120��6   .+�5�      �� $��X��5�   	   $
 Hand��5�   	   $��Right=�5�   	   |�.  $��Courier)�5�   	   $��9(�5   	   $
��Bold"�5  	   $��0,1,0,0q�5  	   $��0q�5  	   $��Bottom=�5  	   $��16��5  	   $��16��6   ��5�   	   �� $ CloseCommand�5     �� 5     $��Center=�5	  	   ��5  	   �� $ DisplayName�5	  	   5     5     �� 5     .R�5  
   �� ���#f0f0f0$
 TrueY�5     8��  $��4q�5     $��15(�5     ��5     �� 5     5     �� 5     "  2 6   .R�5  
   3  5     �� 5      "  4 6   .R�5!  
   5  5"     5%     