   M S B A M L   `   `   `  ���� J  FAnti-Plagiarism, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Anti_PlagiarismAnti_Plagiarism       Anti_Plagiarism.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     /localclr-namespace:Anti_Plagiarism     -     �Name$  frmAntiPlagiarizer�� � Title$ Anti-Plagiarizer��$	��350��$	��525�� � 
ResizeMode$ NoResize=�$y�Visible=� � WindowStyle$
 None=� � AllowsTransparency . $��Center=�$��Center=� � WindowStartupLocation$ CenterScreen=�.��� -    �Name$ grdMain��.+��� -    ��Name$ btnClose��$��X��$��Right=�$	��Top=�$��50��$��25��$��14(�$��Center=�
��������� -   $ btnMinimize��$��-��$��Right=�$��0,0,50,0q�$	��Top=�$��25��$��14(�$��25���� -    ��Name$ lblTitle��$"��Enter Word/ Phrase/ Sentence��$��Center=�$��0q�$	��Top=�$��14(��� -   	 ��Name$	 imgIcon��$
��Left=�$��30��$	��Top=�$��30��$)��#Resources/anti-plagiarism 30x30.jpg���� -   
 ��Name$
 txtInput��$
��Left=�$	��306��$��	10,34,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��505�� �� 	IsEnabled . " �� VerticalScrollBarVisibility$
 Auto=� �� AcceptsReturn .  �� 
AcceptsTab . � -   $ grdSettings��$��Center=�$	��125��$��217,0,213,0q�$��Bottom=�$	��100�� J� Opacity$ 0Q�.+��� -	   $ 	btnChange��$��Change��$��Center=�$��Bottom=�$	��100��$��25��$��14(�$��Center=�
�������$��0q����	#FF0050EF�� -
    ��Name$ sldPercentage��$��Center=�$��0,0,0,50q�$��Bottom=�$	��100��$��25�� �� TickPlacement$ BottomRight=� � Maximum$	 100Q� �� TickFrequency$ 10Q� � Minimum$ 1Q� � Value$ 50Q��� -   $ lblPercentage��$
��50 %��$��Center=�$��0,0,0,75q�$��Bottom=�$��14(�$	��100��$��25��$��Center=�$��Center=�� -    �Name$ pbChange��$��Center=�$��25��$��0,0,0,25q�$��Bottom=�$	��100�����	#FF0050EF���	#FF1BA1E2$	 100Q���Z� �� -   $ 
btnGrammar��$��
GrammarIze��$��Center=�$��Bottom=�$	��100��$��25��$��14(�$��Center=�
����������	#FF0050EF$��	0,0,0,100q��� -   $��scrlGrammar��$
��Left=�$	��100��$��	0,250,0,0q�$	��Top=�$	��212��$
��Auto=�$ 0Q�.���� -   $��	spGrammar��$	��100��$	��212��