   M S B A M L   `   `   `  ����J  Flagerus-maximus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:lagerus_maximuslagerus_maximus  5   	       lagerus_maximus.AboutView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:lagerus_maximus     5   	   -   5          � Title$  About��5   	   $	��200��6   $	��350��6$    � 
ResizeMode$ NoResize=�60    � WindowStartupLocation$ CenterOwner=�6F    � Icon 6�   �� *	AboutIcon+ � 	Resources 5
      $   lagerus_maximus.AboutViewModel   	ViewModel��%   &	          5   
   64   5       � DataContext 5      �� 5   
    �� ResourceKey$ 	ViewModel��6   63   .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   6)   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      6%   �� 5      6%   .+�5      � $��1��6   .+�5!      �� $��Center=�6   .+�5#      �� ��6    �� *ApplicationName+�� 5$      ��6    �� *	Version+�� 5%      ��6    �� *MadeBy+5'      5)      5,      � 5.   
   $��1��6   .+�5/      �� 	 J� IsManipulationEnabled$	 False��65   $��10q�6S   $��3q�6_   
 �� 
IsReadOnly$

 True��6k   $��50��6}   ��6   �� *CopyrightText+6�   50      �� 52   
   $��2��6   $��20��6   $��75��6*   $��Right=�6V   $��10q�6r   $��OK��6~   ��65   �� *CloseCommand+6�   53      54      