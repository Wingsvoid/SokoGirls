   M S B A M L   `   `   `  ����?  ;Soko, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:SokoSoko  5   	       Soko.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:Soko     5   	   -   5          �Name$  
GameScreen��5   	    � Title$ 	SokoGirls��5   	   $	��768��6   $
��1024��6(    � WindowState$ 	Maximized=�65    � Icon$ other/labyrinth.ico��6j    � 
ResizeMode$ NoResize=�6�    � WindowStyle$
 None=�6�   .��5	      �� -   5         ��Name$ scene��5	      $��
0,0,-346,0q�6   $	��100��60   $	��100��6?   ��5
   
   �� 5      -   5        bgBrush�� �� ImageSource$ tiles/floor_stop2.png��5   *    j� TileMode$
 Tile=�6N   	 j� ViewportUnits$	 Absolute=�6^   
 j� Viewport$
 0, 0, 128, 128�6x   .+�5   
   �� -   5         ��Name$
 rule��5      $��Rules��6    �� Left$
 1226��6/   
 �� Top$	 701��6B   $	��134��6S   $��57��6_   $��24(�6~   ���	#FFBA3300   	GameStyle#���   6�   ��6�   Z� ��5      �� 5      $ 
Button.png��6   5      �� 5   
   -   5        $
 exit��5      $�� ��6   $��32��6*   $
 1324��66   $��46��6I   ��6T   Z� ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value 5      �� 5      $ /other/exit.png��6)   5      �� 5       � �� 5      �� 5       �� 
TargetType" ��6.   .R�5   "   ��  ��Name$ border��6)   #��z��69   #��z��6e   #��z��6�    J� SnapsToDevicePixels . ._�5   &   ��  ��Name$ contentPresenter��67   |�.  #��z��6c   #��z��6�   ��. # z 6�   #��z��6   5   #    �� Triggers 5    "   P� 5!   &    P� Property �@J�IsMouseOver P� Value . 5#   '   P� 5$   &    � �� . . �5%   *   ��  �� 
TargetName$ border��6G    � �� 5&   .   �� 5'   2   $ /other/exitpress.png��6=   5)   +   5*   '   5-      5/      50      53      54      56      