   M S B A M L   `   `   `  ����E  AWPFControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:WPFControl.ControlsWPFControl.Controls  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   TextBoxBorder  �� EndPoint � MappingMode �� 
StartPoint � Color � Offset  TextBox_MouseOver  TextBox_Focused  TextBox_DisabledBorder �� Color  TextBox_DisabledBackground  DefaultWatermarkTemplate  WatermarkTextBox �� 
TargetType*    $WPFControl.Controls.WatermarkTextBox �� Property �� Value	 �� 
TargetType
 ��Name �� CornerRadius ��Name J� SnapsToDevicePixels ��Name    	Watermark J� IsHitTestVisible �� RelativeSource �� TargetNullValue J� Opacity �� Triggers ]� 
Conditions �� Property �� Value �� 
TargetName P� Property P� Value �� BasedOn�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      +local!clr-namespace:WPFControl.Controls  5      %  &	        &	 �     &	 �    &	 m    &	 �    &	 �    &	     7   7  7  7  7  '   �
    7  �� 5      $
  0,20&�60   $ Absolute=�6@   $	 0,0&�6W   .��5   
   � $ #ABADB3��6   $
 0.05Q�6'   � 5   
   $ #E2E3EA��6   $
 0.07Q�6'   � 5   
   $ #E3E9EF��6   $ 1Q�6'   5      �� 5
      $	  0,1&�64   $	 0,0&�6C   .��5   
   � $ #5794BF��6   $
 0.05Q�6'   � 5   
   $ #B7D5EA��6   $
 0.07Q�6'   � 5   
   $ #C7E2F1��6   $ 1Q�6'   5      �� 5      $	  0,1&�62   $	 0,0&�6A   .��5   
   � $ #3D7BAD��6   $
 0.05Q�6'   � 5   
   $ #A4C9E3��6   $
 0.07Q�6'   � 5   
   $ #B7D9ED��6   $ 1Q�6'   5      �� 5      $ #ADB2B5��65   �� 5      $ #F4F4F4��69   �� 5      .R�5   
   �� ���#0587C0$��14(�6B   |�.  ��6   �� 6P   5      �� 5      "   6%   .�5   
   ��  � �� �#FEFEFF6'   �� 5   
    � �� �	#3618B8FF6'   �� 5    
    � ��8   6(   �� 5!   
    � ��$ 0q�6,   �� 5"   
    � ��$ 	12,12,3,3q�6$   �� 5#   
    �@J�	AllowDrop . 6&   �� 5$   
    �@�FocusVisualStyle 6-   Z� �� 5%   
    �@��PanningMode$ VerticalFirst=�65   �� 5&   
    �@��IsFlicksEnabled .  63   �� 5'   
    �@  WatermarkTemplate8  6.   �� 5(   
    � �� 5)      �� 5*      "	   6"   .R�5+      � .+�5-      �� $
 Border��6!   #��z��5.      #��z��5/      $ 1��50      #��z��51      ._�52      � .+�53   "   �� $ PART_ContentHost��6/   # z 54   '   �� 55   "   $ PART_WatermarkHost��63   #��z 56   +   #��z��58   +   #��z��59   +    .  #��z��5;   +   $y�	Collapsed=�5<   +   ��57   +   �� *WatermarkTemplate+ �� *TemplatedParent+8  5<   +   5=      5>      �� 5?      $
 MouseOverVisual��6!   $ 0Q�6:   #��z��6F   8�� $ 1��6�   �� 5@      $
 FocusVisual��6!   $ 0Q�66   #��z��6B   8�� $ 1��6�   5B       5C      ]� 5D       5E      �� 5F   "    �@  KeepWatermarkOnGotFocus .  6O   �� 5G   "    �@J�	IsFocused .  6A   �� 5H   "    � ��$  ��6<   /�5J      �� 5K   "   $ PART_WatermarkHost��6?    � y�$ Visible=�6_   5M      ]� 5N       5O      �� 5P   "    �@  KeepWatermarkOnGotFocus . 5Q   $   �� 5R   "    � ��$  ��5S   $   /�5U      �� 5V   "   $ PART_WatermarkHost��5W   !    � y�$ Visible=�5X   !   5Z      P� 5[       �@J�IsMouseOver . . �5\      �� $ MouseOverVisual��6%    �@J�Opacity$ 1Q�6U   5]      P� 5^       �@J�	IsFocused . . �5_      �� $ FocusVisual��6%    �@J�Opacity$ 1Q�6Q   5`      5e      5g      5h      �� 5j      "   6   8   69   5k      