   M S B A M L   `   `   `  ���� 	  NOTAM     NOTAM.View.NotamDetailView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       � 	Resources     AftnItemStyle �� 
TargetType �� Property �� Value  AftnData �� Source �� Path  SelectedAftnData  ManualAftns��%�   &	        &	 (     &	 H     &	 j     �� " ��.���  � ��$ Stretch=���  �� $ AftnOptions���  �� $ SelectedAftns���  �� $ ManualAftns�.��� $��4q� � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto ��� $��* ��� $	��100 � � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �   �� DataType-  'System.Windows.Controls.ValidationError��%k   (��       *, +)�� "  .R��� $��Italic$�
���  ��$
��Left=�$	��0,1q����� $ ErrorContent�.+��� $��0��$��0��$��
Notam Text��$
��Left=�	 �� Target	 �� 
 �� ElementName$
 notamTxt���� -    ��Name$ notamTxt��$��0��$��1��$��3��$
��Auto=� �� MaxLines$ 10�� �� AcceptsReturn .  �� 
IsReadOnly . ���� $ 	NotamText� �� ValidatesOnDataErrors .  �� UpdateSourceTrigger$ PropertyChanged=� =� ErrorTemplate Z� �� $��0��$��4��$��Print��$	��4,2q�$
��Left=�$��60��$��40������ $ PrintCommand��� $��1��$��0��$��Aftn Message��$
��Left=� d� 	IsChecked �� $ 	IsAftnMsg� . $ PropertyChanged=��� $��1��$��2��$��Remark��$
��Left=� �� $ IsRemark� . $ PropertyChanged=��� $��1��$��3��$��Initial:��$
��Auto��$
��Left=�$��0,1,0,0q�$��3��	 �� $
 userTxt���� -   $ userTxt��$��1��$��50��$��3��$
��Left=�$��49,0,0,0q����� $
 User� . $ PropertyChanged=��� $��1��$��4��$��_Save��$
��Left=�$	��4,2q�$��60������ $ SaveCommand��� -   $ descriptionAFTNTxt��$��2��$��0��$��2��$ 10��" �� VerticalScrollBarVisibility$
 Auto=� �� TextWrapping$
 Wrap=� . ���� $ DescriptionAFTN� . $ PropertyChanged=� Z� {��� $ 	IsAftnMsg��� -   $ descriptionRemarkTxt��$��2��$��2��$��2��$
 Auto=�$ 10��$
 Wrap=� . ���� $ DescriptionRemark� . $ PropertyChanged=� Z� {��� $ IsRemark��� $��3��$��0��$��File:��$
��Left=�	 �� $
 fileTxt���� -   $ fileTxt��$��3��$��1������ $ FilePath� . $ PropertyChanged=� Z� �� $��3��$��2��$��Browse��$��0,2,20,2q�$
��Left=����� $ OpenFileCommand��� $��4��$��2��$��Send Default��$��0,2,20,2q�$
��Left=����� $ SendDefaultCommand��� $��Individual Series��$��5��$��0��$��2��.��� $��20q� �� $��* ��� $��20 ��� $��* ��� $��* � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �.+��� $��0��$��0��$
��AFTN���� $��1��$��0������ $ 
ManualAftn��� $��1��$��1��$��>������ $ AddManualAftnCommand��� -   $��2��$��1��$��<������ $ RemoveManualAftnCommand��� $��1��$��2��$��3���� -   $��aftnManualListView��$��1��$��2��$��3�� �� SelectionMode$ Multiple=� � DataContext# [ #��[  ����  �� SelectedItem �� $ RemovedManualAftn��� $��Distribution Series��$��5��$��2��$��2��.��� $��20q� �� $��* ��� $��20 ��� $��* ��� $��* � �� $
��Auto ��� $��20 ��� $	��100 �.+��� $��0��$��0��$��Destination List���� -   $��aftnListView��$��1��$��0��$��2��# [ #��[   �� SelectedValue �� $ SelectedAftn� �� Mode$ TwoWay=�����  �� View �� .P���  �� DisplayMemberBinding �� $ Series��� -	   $��1��$��1��$��>������ $ AddAftnCommand��� -
   $��2��$��1��$��<��$	��20 ������ $ RemoveAftnCommand��� $��0��$��2��$��
Dest. AFTN���� -   $��aftnSelectedListView��$��1��$��2��$��2��$ Multiple=�# [ #��[   �� $ RemovedAftn�����  �� .P���  �� $ Series��� $��10��$��3��$��_Send��$
��Left=�$	��4,2q�$��60������ $ SendCommand��� -   $ TxtCountPrint��$��3��$
��Left=�$��24��$��49,0,0,0q�$��3��$
 Wrap=�$	��Top=�$��50������ $ TxtCountPrint��� -    ��Name$ lblPrintCount��$��count print:��$��2��$
��Left=�$��70,0,0,0q�$��3��$	��Top=�$��2��