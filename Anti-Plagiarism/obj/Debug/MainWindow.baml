   M S B A M L   `   `   `  ����J  FAnti-Plagiarism, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Anti_PlagiarismAnti_Plagiarism  5   	   C&clr-namespace:Anti_Plagiarism.ControlsAnti_Plagiarism.Controls  65        Anti_Plagiarism.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   /localclr-namespace:Anti_Plagiarism     5   	   ;Controls&clr-namespace:Anti_Plagiarism.Controls     65   -   5          �Name$  frmAntiPlagiarizer��5   m    � Title$ Anti-Plagiarizer��5   	   $	��350��6"   $	��525��6/    � 
ResizeMode$ NoResize=�6;   $y�Visible=�6Q    � WindowStyle$
 None=�6f    � AllowsTransparency . $��Center=�6�   $��Center=�6�    � WindowStartupLocation$ CenterScreen=�6�   .��5      � -   5         �Name$ grdMain��5      .+�5   
   �� -   5         ��Name$ lblTitle��5      $"��Enter Word/ Phrase/ Sentence��6"   $��Center=�6I   $��0q�6f   $	��Top=�6q   $��14(�6�   �� 5   
   -   5         ��Name$ imgIcon��5      $
��Left=�6!   $��30��6<   $	��Top=�6H   $��30��6`   $)��#Resources/anti-plagiarism 30x30.jpg��6k   � 5	   
   -   5        $ grdSettings��5	      $��Center=�6$   $	��100��6A   $��217,0,213,0q�6N   $��Bottom=�6c   $	��100��6~   	 J� Opacity$	 0Q�6�   .+�5
      �� -   5        $ lblPercentage��5
      $
��50 %��6+   $��Center=�6:   $��0,0,0,50q�6W   $��Bottom=�6i   $��14(�6�   $	��100��6�   $��25��6�   $��Center=�6�   $��Center=�6�   )   #Anti_Plagiarism.Controls.FlowButton  5      -   5        
  Name$
 
btnGrammar��5   "   )   #Anti_Plagiarism.Controls.FlowButton   MouseUp$ btnGrammar_MouseUp��6�   $��Center=�66   $	��Top=�6S   $	��100��6k      Text$ Check Grammar��6w   $��0q�6�     5      -   5        $
 	btnChange��5   "   $ btnChange_MouseUp��6�   $��Center=�65   $��0q�6R   $��Bottom=�6]   $	��100��6x   $ Change Text��6�   6�   .   (Anti_Plagiarism.Controls.FlowProgressBar  5      -	   5          Name$ pbChange��5   '   $��Center=�69   $��0,50,0,0q�6V   $	��Top=�6h   $	��100��6�   5      �� 5   
   -
   5         ��Name$ scrlGrammar��5      $
��Left=�6,   $	��100��6G   $��	0,250,0,0q�6T   $	��Top=�6g   $	��212��6   $
��Auto=�6�   $	 0Q�6�   .��5      �� -   5         ��Name$ 	spGrammar��5      $	��100��6-   $	��212��6:   5        5   
   -   5        $
 btnClose��5      $ btnClose_MouseUp��6�   $��Right=�60   $��0q�6L   $	��Top=�6W   $ X��6o   $��50��6x   6�     5   
   -   5        $
 btnMinimize��5      $ btnMinimize_MouseUp��6�   $��Right=�63   $��0,0,50,0q�6O   $	��Top=�6a   $��25��6y   $ -��6�   6�   �� 5   
   -   5         ��Name$ txtInput��5      $
��Left=�6(   $	��306��6C   $��	10,30,0,0q�6P   $	��Top=�6c   $	��505��6{    �� 
AcceptsTab .  �� 	IsEnabled . 5      5      5      