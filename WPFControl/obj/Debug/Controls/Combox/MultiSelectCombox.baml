   M S B A M L   `   `   `  ����E  AWPFControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:WPFControl.Controls.ComboxWPFControl.Controls.Combox  5      2    ,WPFControl.Controls.Combox.MultiSelectCombox   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:WPFControl.Controls.Combox     5        � 	Resources  5	         ToggleButtonControlTemplate �� 
TargetType � ColumnDefinitions ��Name �� CornerRadius ��Name �� Padding �� Path �� RelativeSource	 �� Mode
 �� AncestorType �� ElementName F�Name J� RenderTransformOrigin'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard g� Duration �� 
TargetName �� TargetProperty	 W� To  ComboBoxItemStyle �� 
TargetType �� Property �� Value �� Padding J� SnapsToDevicePixels � UseLayoutRounding �� Triggers P� Property P� Value��%�  &	        &	 n    �� 5
   
   " d�6>   .R�5      �  5      �� 5      $��* �6'   �� 5      $��28 �6'   .+�5      �� $ Border��5      $��2��5      $ 2��5      
������ ���#1487EB$��1,1,1,1q�5      �� 5      $ 
BorderComp��5      $��0��5      $ 2��5      $��1q�5      
������ ���#1487EB$��0,0,0,0q�5      ._�5      �� $ 	txtResult��6    
������ $ 3q�5    9   ��5   4   �� $
 Text� �� $	 FindAncestor=�$
 UserControlL���5    E   �� *
Foreground+$ MultiSelectCombo��5!      5"      F� 5#      $ Arrow��5$      $ 0.5,0.5&�5#      $��1��5%      ���#1487EB$��Center=�5'      $��Center=�5(      "����        �@KL ��[ р��     	z�5*      V� 5+      .~�5,      �� �� 5-      �� 5.      T� 5/      50      52       53        54      CheckStates��. 55        Checked��. 56      �� .��57   "   W� $ 0:0:0.2D�62   $ Arrow��6E   $V P(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)�58   2   $	 180X�59   2   5:      5;        5<      	Unchecked��. 5=      �� .��5>   "   W� $ 0:0:0.2D�62   $ Arrow��6E   $V P(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)�5?   2   $ 0X�5@   2   5A      5B      5C      5E      5F      �� 5G   
   " ��6*   .�5H      ��  �@J�SnapsToDevicePixels . 64   �� 5I       � ��$ 5,5,0,5q�6(   �� 5J       � �� 6;   �� *HorizontalContentAlignment+ �� "
 ���� 5K       � �� 69   �� *VerticalContentAlignment+ �� "
 ���� 5L       � ��
 ���� 6+   �� 5N       � ��
 ���� 6,   �� 5O       � ��$ 0q�60   �� 5P       � �� 6)   �� *
FontSize+ �� "
 ���� 5Q       � �� 5R      �� 5S      " ��6&   .R�5T      �� $ Bd��6!   #��z��5U      #��z��5V      #��z��5W      # z��5X       .  . ._�5Z      �� #��z��6/   #��z��5[   +   # z 5\   +   5]       5^      P� 5_       �@J�IsMouseOver . . �5`   "   ��  � �� �#0C41726?   �� 5a   "    � �� �#0000006?   5b      5d      5f      5g      5h      .��5j      �� -   5         ��Name$ MultiSelectCombo��5k   	    .  � OverridesDefaultStyle . $
��Auto=�5n   	   $
��Auto=�5o   	   ��. $  �� IsSynchronizedWithCurrentItem$
  TrueY�5q   	   #��[ 5r   	   ��5s   
   �� 5t      .R�5u      �� -   5        
���������5u      �� *Title+! d� 	IsChecked! 5v      �� $ 
IsSelected�" �� Mode$" TwoWay=�
# � Tag# 5w      �� *FindAncestor+"
 ��5x      5y      ��5{   
   �� 5|      " ��6   .R�5}      � .+�5~      d� $ d�Name$$ ToggleButton��5      #��[  6/   $��2��5�      |�.  
�������% �� 	ClickMode$% Press=�5�      $
��Left=�6+   ! 5�   )   �� $ IsDropDownOpen�$" TwoWay=� �� *TemplatedParent+5�   +   � 5�      $��Popup��5�      $��Bottom=�5�      & � AllowsTransparency& . |�.  ' �� IsDropDownOpen#��z' 5�   ,   $��Slide=�5�      .��5�      � $��DropDown��5�       . ( � ActualWidth#��z( 5�      ) �� MaxDropDownHeight#��z) 5�      .+�5�      �� $ DropDownBorder��5�   !   $��1q�5�   !   ���#11253B���#1487EB$ 1��5�   7    .  . 6c   �� 5�      $��2q�6+    . * � DataContext* 6Q   �� .��5�   "   �� + K� IsItemsHost+ . -  'System.Windows.Input.KeyboardNavigation,   DirectionalNavigation$, 	Contained=�6@   5�      5�      5�      5�       5�      P� 5�       �@��HasItems .  . �5�      �� - �� 
TargetName$- DropDownBorder��6!    � ��$ 95��6R   5�      5�      5�      5�      