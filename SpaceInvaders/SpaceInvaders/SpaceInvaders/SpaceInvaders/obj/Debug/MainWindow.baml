   M S B A M L   `   `   `  ����H  DSpaceInvaders, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:SpaceInvadersSpaceInvaders  5   	       SpaceInvaders.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:SpaceInvaders     5   	   -   5          � 
ResizeMode$  NoResize=�5   	    � Title$ Space Invaders��5	   	   $	��720��6    $
��1080��6-   .��5      � .+�5   
   o� $
��Left=�6   $��25��6*   $	��Top=�66   $
��1070��6N   $��
0,0,-0.4,0q�6[   .@�5      m� $��Gameplay��6   $��25��6)   $��90��65   .@�5      m� -   5         m�Name$ mnuGameplayStartStop��5      $��_Start/Stop��69   $
��Left=�6N   $��25��6i   $	��180��6u   6�   m� 5      -   5        $ mnuGameplayControls��5      $��	_Controls��68   $
��Left=�6K   $��25��6f   $	��180��6r   6~   m� 5      -   5        $ mnuGameplayPoints��5      $��_Points System��66   $
��Left=�6N   $��25��6i   $	��180��6u   6�   5      m� 5      $��About��6   $��25��6&   $��71��62   .@�5      m� -   5        $ mnuAboutInfo��5      $��_Info��61   $
��Left=�6@   $��25��6[   $	��140��6g   6s   5      5      �� 5   
   -   5         ��Name$ PlayArea��5      $	��660��6#   $
��1070��60   $��4,30,0,0q�6=   $	��Top=�6O   ��5      �� 5       �� ImageSource$ Resources/BG.png��6   5      �� 5   
   -   5         ��Name$
 menu��5      $��Center=�5      $��Center=�5       $	��250��5!      $	��350��5"      .+�5#      �� -   5         ��Name$ 	btn_start��5#      $��Start��5$      $��20q�5%      $��36(�5&      6#   �� 5'      -	   5        $ btn_highScores��5'      $��High Scores��5(      $��20q�5)      $��36(�5*      6#   �� 5+      -
   5        $ btn_exit��5+      $
��Exit��5,      $��20q�5-      $��36(�5.      6#   5/      50      51      