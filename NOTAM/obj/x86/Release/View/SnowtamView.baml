   M S B A M L   `   `   `  ���� 	  NOTAM    NOTAM.View.SnowtamView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     $��13(�.��� $��4q�  � ColumnDefinitions  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto ��� $��* ��� $
��Auto ��� $��* ��� $
��Auto ��� $��* � � RowDefinitions �� $
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
��Auto ��� $
��Auto � � 	Resources  �� DataType-  'System.Windows.Controls.ValidationError �� Path��%q   (��       *, +)�� "  .R��� $��Italic$����	#FFFF0000$
��Left=�$	��0,1q����� $ ErrorContent�.+��� $��0��$��0��$��Filling��$
��Left=� �� Target ��  �� ElementName$ 
fillingTxt���� -   $��
fillingTxt��$��0��$��1��{�.  ���� $ Filling� �� ValidatesOnDataErrors .  �� UpdateSourceTrigger$ PropertyChanged=�	 =� ErrorTemplate	 Z� �� $��	Originat:��$��1��$
��Left=�$��1,0,0,0q�$��30��$	��Top=�$��2�� �� $ 	originTxt���� -   $��	originTxt��$��1��$��1��{�.  $��0,2,0,0q�$
��Left=�$
��Auto������ $ OriginOptions�
 �� Mode$
 OneTime=� �� SelectedItem �� $ Originat� . 	 Z� �� $��2��$��SW��$
��Left=� �� $ 
OpgroupTxt���� -   $��
OpgroupTxt��$��2��$��1�� �� 	MaxLength$ 2������ $ Opgroup� . $ PropertyChanged=�	 Z� �� $��1��$��2��$��Number��$
��Left=� �� $ numTxt���� -   $��numTxt��{�.  $��2��$��2������ $ 
SnowtamNum� . $ PropertyChanged=�	 Z� �� $��3��$��0��$��	Aerodrome��$
��Left=� �� $ aeroTxt���� -    ��Name$ aeroTxt��$��3��$��1�� �� 
IsEditable . ���� $ AerodomOptions�$
 Default=�$ PropertyChanged=� �� $ SelectedAerodom� . �� $��	Aerodrome��$
��Left=�$��1��$��3�� �� $ AerodomeTxt���� -   $��AerodomeTxt��$��2��$��3��$ 3�� �� 
IsReadOnly . ���� $ Aerodome� . $ PropertyChanged=�	 Z� �� $��1��$��4��$��	Date/Time��$
��Left=�$��30��$	��Top=� �� $ DateTimeTxt���� -   $��DateTimeTxt��$��2��$��4��$ 8������ $ 	DateTimeT� . $ PropertyChanged=�	 Z� �� $��4��$��0��$��B) Date/Time��$
��Left=� �� $ DateTimeBTxt���� -   $��DateTimeBTxt��$��0��$��5��$ 8������ $ 	DateTimeB� . $ PropertyChanged=�	 Z� �� $��4��$��1��$��	C) Runway��$
��Left=� �� $ 	RunwayTxt���� -	   $��	RunwayTxt��$��5��$��1��$ 3������ $ Runway� . $ PropertyChanged=�	 Z� �� $��4��$��2��$��D) RunwayLen��$
��Left=� �� $ RunwayLenTxt���� -
   $��RunwayLenTxt��$��5��$��2��$ 4������ $ 	RunwayLen� . $ PropertyChanged=�	 Z� �� $��4��$��3��$��E) Runway Width��$
��Left=� �� $ RunwayWidthTxt���� -   $��RunwayWidthTxt��$��5��$��3������ $ RunwayWidth� . $ PropertyChanged=�	 Z� �� $��4��$��4��$��F) Depositon��$
��Left=� �� $ DepositonTxt���� -   $��DepositonTxt��$��5��$��4������ $ 	Depositon� . $ PropertyChanged=�	 Z� �� $��4��$��5��$��G) Depth��$
��Left=� �� $ DepthTxt���� -   $��DepthTxt��$��5��$��5������ $ Depth� . $ PropertyChanged=�	 Z� �� $��6��$��0��$��H)Friction*��$
��Left=� �� $ FrictionTxt���� -   $��FrictionTxt��$��7��$��0������ $ Friction� . $ PropertyChanged=�	 Z� �� $��6��$��1��$��
J)SnowBank��$
��Left=� �� $ SnowBankTxt���� -   $��SnowBankTxt��$��7��$��1������ $ SnowBank� . $ PropertyChanged=�	 Z� �� $��6��$��2��$��K)RunwayLight��$
��Left=� �� $ RunAwayLightTxt���� -   $��RunAwayLightTxt��$��7��$��2������ $ RunAwayLight� . $ PropertyChanged=�	 Z� �� $��6��$��3��$��	L)Further��$
��Left=� �� $ 
FurtherTxt���� -   $��
FurtherTxt��$��7��$��3������ $ Further� . $ PropertyChanged=�	 Z� �� $��6��$��4��$��M)FurtherClearance��$
��Left=� �� $ FurtherClearanceTxt���� -   $��FurtherClearanceTxt��$��7��$��4������ $ FurtherClearance� . $ PropertyChanged=�	 Z� �� $��6��$��5��$��	N)Taxiway��$
��Left=� �� $ 
TaxiwayTxt���� -   $��
TaxiwayTxt��$��7��$��5������ $ Taxiway� . $ PropertyChanged=�	 Z� �� $��6��$��6��$��P)Taxiway Snowbank��$
��Left=� �� $ TaxiwaySnowbankTxt���� -   $��TaxiwaySnowbankTxt��$��7��$��6������ $ TaxiwaySnowbank� . $ PropertyChanged=�	 Z� �� $��8��$��0��$��B)��$
��Left=� �� $ DateTime2Txt���� -   $��DateTime2Txt��$ 8��$��9��$��0��$��1������ $ 
ObsrvDate2� . $ PropertyChanged=�	 Z� �� $��8��$��1��$��C)��$
��Left=� �� $ 
Runway2Txt���� -   $��
Runway2Txt��$��9��$��1��$��1������ $ Runway2� . $ PropertyChanged=�	 Z� �� $��8��$��2��$��D)��$
��Left=� �� $ ClearedRunwayLen2Txt���� -   $��ClearedRunwayLen2Txt��$��9��$��2��$��1������ $ ClearedRunwayLen2� . $ PropertyChanged=�	 Z� �� $��8��$��3��$��E)��$
��Left=� �� $ ClearedRunwayWidth2Txt���� -   $��ClearedRunwayWidth2Txt��$��9��$��3��$��1������ $ ClearedRunwayWidth2� . $ PropertyChanged=�	 Z� �� $��8��$��4��$��F)��$
��Left=� �� $ Depositon2Txt���� -   $��Depositon2Txt��$��9��$��4��$��1������ $ 
Depositon2� . $ PropertyChanged=�	 Z� �� $��8��$��5��$��G)��$
��Left=� �� $ MeanDepth2Txt���� -   $��MeanDepth2Txt��$��9��$��5��$��1������ $ 
MeanDepth2� . $ PropertyChanged=�	 Z� �� $��10��$��0��$��H)��$
��Left=� �� $ Friction2Txt���� -   $��Friction2Txt��$��11��$��0��$��1������ $ 	Friction2� . $ PropertyChanged=�	 Z� �� $��10��$��1��$��J)��$
��Left=� �� $ CriticalSnowbank2Txt���� -   $��CriticalSnowbank2Txt��$��11��$��1��$��1������ $ CriticalSnowbank2� . $ PropertyChanged=�	 Z� �� $��10��$��2��$��K)��$
��Left=� �� $ RunwayLight2Txt���� -   $��RunwayLight2Txt��$��11��$��2��$��1������ $ RunwayLight2�	 Z� �� $��10��$��3��$��L)��$
��Left=� �� $ FurtherClearance2Txt���� -   $��FurtherClearance2Txt��$��11��$��3��$��1������ $ FurtherClearance2� . $ PropertyChanged=�	 Z� �� $��10��$��4��$��M)��$
��Left=� �� $ FurtherClearanceexp2Txt���� -   $��FurtherClearanceexp2Txt��$��11��$��4��$��1������ $ FurtherClearanceexp2� . $ PropertyChanged=�	 Z� �� $��10��$��5��$��N)��$
��Left=� �� $ Taxiway2Txt���� -    $��Taxiway2Txt��$��11��$��5��$��1������ $ Taxiway2� . $ PropertyChanged=�	 Z� �� $��10��$��6��$��P)��$
��Left=� �� $ TaxiwaySnowbank2Txt���� -!   $��TaxiwaySnowbank2Txt��$��11��$��6��$��1������ $ TaxiwaySnowbank2� . $ PropertyChanged=�	 Z� �� $��12��$��0��$��B)��$
��Left=� �� $ DateTime3Txt���� -"   $��DateTime3Txt��$��13��$��0��$��1��$ 8������ $ 
ObsrvDate3� . $ PropertyChanged=�	 Z� �� $��12��$��1��$��C)��$
��Left=� �� $ 
Runway3Txt���� -#   $��
Runway3Txt��$��13��$��1��$��1������ $ Runway3� . $ PropertyChanged=�	 Z� �� $��12��$��2��$��D)��$
��Left=� �� $ ClearedRunwayLen3Txt���� -$   $��ClearedRunwayLen3Txt��$��13��$��2��$��1������ $ ClearedRunwayLen3� . $ PropertyChanged=�	 Z� �� $��12��$��3��$��E)��$
��Left=� �� $ ClearedRunwayWidth3Txt���� -%   $��ClearedRunwayWidth3Txt��$��13��$��3��$��1������ $ ClearedRunwayWidth3� . $ PropertyChanged=�	 Z� �� $��12��$��4��$��F)��$
��Left=� �� $ Depositon3Txt���� -&   $��Depositon3Txt��$��13��$��4��$��1������ $ 
Depositon3� . $ PropertyChanged=�	 Z� �� $��12��$��5��$��G)��$
��Left=� �� $ MeanDepth3Txt���� -'   $��MeanDepth3Txt��$��13��$��5��$��1������ $ 
MeanDepth3� . $ PropertyChanged=�	 Z� �� $��14��$��0��$��H)��$
��Left=� �� $ Friction3Txt���� -(   $��Friction3Txt��$��15��$��0��$��1������ $ 	Friction3� . $ PropertyChanged=�	 Z� �� $��14��$��1��$��J)��$
��Left=� �� $ CriticalSnowbank3Txt���� -)   $��CriticalSnowbank3Txt��$��15��$��1��$��1������ $ CriticalSnowbank3� . $ PropertyChanged=�	 Z� �� $��14��$��2��$��K)��$
��Left=� �� $ RunwayLight3Txt���� -*   $��RunwayLight3Txt��$��15��$��2��$��1������ $ RunwayLight3� . $ PropertyChanged=�	 Z� �� $��14��$��3��$��L)��$
��Left=� �� $ FurtherClearance3Txt���� -+   $��FurtherClearance3Txt��$��15��$��3��$��1������ $ FurtherClearance3� . $ PropertyChanged=�	 Z� �� $��14��$��4��$��M)��$
��Left=� �� $ FurtherClearanceexp3Txt���� -,   $��FurtherClearanceexp3Txt��$��15��$��4��$��1������ $ FurtherClearanceexp3� . $ PropertyChanged=�	 Z� �� $��14��$��5��$��N)��$
��Left=� �� $ Taxiway3Txt���� --   $��Taxiway3Txt��$��15��$��5��$��1������ $ Taxiway3� . $ PropertyChanged=�	 Z� �� $��14��$��6��$��P)��$
��Left=� �� $ TaxiwaySnowbank3Txt���� -.   $��TaxiwaySnowbank3Txt��$��15��$��6��$��1������ $ TaxiwaySnowbank3� . $ PropertyChanged=�	 Z� �� $��16��$��1��$��Apron��$
��Left=��� $��16��$��4��$
��Next��$
��Left=��� $��17��$��0��$��R)��$
��Left=� �� $ ApronTxt���� -/   $��ApronTxt��$��17��$��1��$��2������ $ Apron� . $ PropertyChanged=�	 Z� �� $��17��$��3��$��S)��$
��Left=� �� $ NextObsrvTxt���� -0   $��NextObsrvTxt��$��17��$��4��$��2������ $ 	NextObsrv� . $ PropertyChanged=�	 Z� �� $��19��$��T)��$
��Left=�$��2�� �� $ FreeTexttTxt���� -1   $��FreeTexttTxt��$��19��$��1��$��7�� �� AcceptsReturn . $
��Auto=� �� MaxLines$ 8��$��3��$��12,1,0,7q����� $ 	FreeTextt� . $ PropertyChanged=�	 Z� �� $��22��$��	_Localize��$
��Left=�$��60��$��4��$��3��$��	12,7,0,23q����� $ LocalizeCommand��� -2   $��22��$��8��$
��Next��$
��Left=�$��	0,10,0,26q�$��60��$��3��$��3������ $ SendCommand��� $��24��$��12��$
��Help��$
��Left=�$��	0,10,0,26q�$��60��$��3��$��3������ $ HelpCommand��� -3   $��ErrorMessageLable��$��25��$��2��$��4��$
��Left=�
���  ������ $ ErrorMessage�