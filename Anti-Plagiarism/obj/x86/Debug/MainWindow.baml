   M S B A M L   `   `   `  ����J  FAnti-Plagiarism, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Anti_PlagiarismAnti_Plagiarism  5   	        Anti_Plagiarism.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   /localclr-namespace:Anti_Plagiarism     5   	   -   5          �Name$  frmAntiPlagiarizer��5   5    � Title$ Anti-Plagiarizer��5   	   $	��350��6"   $	��525��6/    � 
ResizeMode$ NoResize=�6;   $y�Visible=�6Q    � WindowStyle$
 None=�6f    � AllowsTransparency . $��Center=�6�   $��Center=�6�    � WindowStartupLocation$ CenterScreen=�6�   .��5      � -   5         �Name$ grdMain��5      .+�5   
   �� -   5         ��Name$ btnClose��5      $��X��6#   $��Right=�6/   $	��Top=�6K   $��50��6c   $��25��6n   $��14(�6z   $��Center=�6�   
�������6�   �� 5   
   -   5        $ btnMinimize��5      $��-��6&   $��Right=�62   $��0,0,50,0q�6N   $	��Top=�6`   $��25��6x   $��14(�6�   $��25��6�   �� 5	   
   -   5         ��Name$ lblTitle��5	      $"��Enter Word/ Phrase/ Sentence��6"   $��Center=�6I   $��0q�6f   $	��Top=�6q   $��14(�6�   �� 5
   
   -   5        	 ��Name$	 imgIcon��5
      $
��Left=�6!   $��30��6<   $	��Top=�6H   $��30��6`   $)��#Resources/anti-plagiarism 30x30.jpg��6k   �� 5   
   -   5        
 ��Name$
 txtInput��5      $
��Left=�6$   $	��306��6?   $��	10,34,0,0q�6L    �� TextWrapping$
 Wrap=�6_   $	��Top=�6s   $	��505��6�    �� 	IsEnabled . " �� VerticalScrollBarVisibility$
 Auto=�6�    �� AcceptsReturn .  �� 
AcceptsTab . 6%  � 5   
   -   5        $ grdSettings��5      $��Center=�6$   $	��125��6A   $��217,0,213,0q�6N   $��Bottom=�6c   $	��100��6~    J� Opacity$ 0Q�6�   .+�5      �� -	   5        $ 	btnChange��5      $��Change��6(   $��Center=�69   $��Bottom=�6V   $	��100��6q   $��25��6}   $��14(�6�   $��Center=�6�   
�������$��0q�6�   ���	#FF0050EF6�   �� 5      -
   5         ��Name$ sldPercentage��5      $��Center=�6,   $��0,0,0,50q�6I   $��Bottom=�6[   $	��100��6v   $��25��6�    �� TickPlacement$ BottomRight=�6�    � Maximum$	 100Q�6�    �� TickFrequency$ 10Q�6�    � Minimum$ 1Q�6�    � Value$ 50Q�6  �� 5      -   5        $ lblPercentage��5      $
��50 %��6+   $��Center=�6:   $��0,0,0,75q�6W   $��Bottom=�6i   $��14(�6�   $	��100��6�   $��25��6�   $��Center=�6�   $��Center=�6�   � 5      -   5         �Name$ pbChange��5      $��Center=�6,   $��25��6I   $��0,0,0,25q�6U   $��Bottom=�6g   $	��100��6�   ���	#FF0050EF���	#FF1BA1E2$	 100Q�6�   ��6�   Z� 6�   �� 5      -   5        $ 
btnGrammar��5      $��
GrammarIze��6)   $��Center=�6>   $��Bottom=�6[   $	��100��6v   $��25��6�   $��14(�6�   $��Center=�6�   
����������	#FF0050EF$��	0,0,0,100q�6  5      �� 5   
   -   5        $��scrlGrammar��5      $
��Left=�6*   $	��100��6E   $��	0,250,0,0q�6R   $	��Top=�6e   $	��212��6}   $
��Auto=�6�   $ 0Q�6�   .��5      �� -   5        $��	spGrammar��5      $	��100��6+   $	��212��68   5      5      5      