   M S B A M L   `   `   `  ����E  AWPFControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:WPFControl.Themes.AnimationWPFControl.Themes.Animation  5      4    .WPFControl.Themes.Animation.MagicCircleControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:WPFControl.Themes.Animation     5        � 	Resources  5	         ShadowColor��%+   &	       ��5
   
   	#EEB8FFF9��6@   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� -   5        $��buttonHalfPanel��5      $��0��6,   $��1��6<   $��0��6P   $��1��6]   �� 5   
   -   5        $��magicCircleImage��5      $��2��6(   $��2��6<   $/��)/WPFControl;component/Style/Images/wj.png��6M    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      -   5        magicCircleDropShadowEffect��   Color# [  5   H      RenderingBias$ Performance=�6m      
BlurRadius$ 5Q�6�      ShadowDepth$ 0Q�6�   z�5      V� 5      .~�5      �� �� 5       �� 5!       �� CenterX 6&   �� *ActualWidth+	 �� ElementName$	 buttonHalfPanel��
 �� CenterY
 5"   &   �� *ActualWidth+$	 buttonHalfPanel��T� 5#      5$       � Triggers 5&      :� 5'       :� RoutedEvent$ FrameworkElement.Loaded��6   .[�5(      �� .��5)      �� .��5*      U�  g� RepeatBehavior$ Forever��6<    g� AutoReverse .  �� TargetProperty$ 
BlurRadius�6h    �� 
TargetName$! magicCircleDropShadowEffect��6�   .\�5+   "   9  3System.Windows.Media.Animation.EasingDoubleKeyFrame   O� KeyTime$ 0:0:2��67    O� Value$ 30Q�6G   5,   #   5-      U� 5/      $ Forever��6<   $V P(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)�6U   $ magicCircleImage��6�   .\�50   "     $ 0:0:20��67   $
 -360Q�6H   51   #   52      53      54      55      57      �� 5B   
   $
��1200��6   $	��300��6   .+�5C      ��  �� Orientation$ 
Horizontal=�6   
 �� Top$ 50��62   .+�5E      �� $/��)/WPFControl;component/Style/Images/wj.png��6   $	��100��6K   $	��100��6X   $
��Fill=�6d   $��10q�5F       J� Opacity$	 0.5Q�6!   61   �� 5G      $/��)/WPFControl;component/Style/Images/wj.png��6   $	��100��6K   $	��100��6X   $
��Fill=�6d   $��10q�5H      z�5I      u� 5J       u� Matrix$ 1 0 0 1 -80 30x�6*   6D   5L      �� 5O      $/��)/WPFControl;component/Style/Images/wj.png��6   $	��100��6K   $	��100��6X   $
��Fill=�6d   $��10q�5P      $	 0.5Q�6!   61   �� 5Q      $/��)/WPFControl;component/Style/Images/wj.png��6   $	��100��6K   $	��100��6X   $
��Fill=�6d   $��10q�5R      z�5S      u� 5U      $ 0 1 -1 0 30 0x�6*   6C   5W      �� 5Z      $/��)/WPFControl;component/Style/Images/wj.png��6   $	��100��6K   $	��100��6X   $
��Fill=�6d   $��10q�5[      $	 0.5Q�6!   61   �� 5\      $/��)/WPFControl;component/Style/Images/wj.png��6   $	��100��6K   $	��100��6X   $
��Fill=�6d   $��10q�5]      z�5^      u� 5`      $ 1.5 0 0 1.5 -80 0x�6*   6G   5b      �� 5e      $/��)/WPFControl;component/Style/Images/wj.png��6   $	��100��6K   $	��100��6X   $
��Fill=�6d   $��10q�5f      $	 0.5Q�6!   61   �� 5g      $/��)/WPFControl;component/Style/Images/wj.png��6   $	��100��6K   $	��100��6X   $
��Fill=�6d   $��10q�5h      z�5i      u� 5k      $ 1 0 1 1 -80 0x�6*   6C   5m      �� 5p      $/��)/WPFControl;component/Style/Images/wj.png��6   $	��100��6K   $	��100��6X   $
��Fill=�6d   $��10q�5q      $	 0.5Q�6!   61   �� 5r      $/��)/WPFControl;component/Style/Images/wj.png��6   $	��100��6K   $	��100��6X   $
��Fill=�6d   $��10q�5s      z�5t      u� 5w      $ 1.5 0 1 1.5 -120 0x�6*   6H   5y      5z      5{      5~      5      