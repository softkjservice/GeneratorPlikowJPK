   M S B A M L   `   `   `  ����D  @JPK_VAT_3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:JPK_VAT_3	JPK_VAT_3  5   	       JPK_VAT_3.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:JPK_VAT_3     5   	     � WindowState$  	Maximized=�5   	    � Title$$ Oprogramowanie Soft-KJ-Service��5	   	   $	��768��61   $
��1200��6>   .��5
      � .+�5   
   �� -   5         ��Name$ lab_Generator��5      $ ��Generator plików JPK_ VAT��6'   $
��Left=�6K   $��	47,22,0,0q�6f   $	��Top=�6y   $��36(�6�   $
��Bold"�6�   �� 5   
   -   5        $ lab_ZaOkres��5      $��	Za okres:��6%   $
��Left=�69   $��
10,132,0,0q�6T   $	��Top=�6h   $��22(�6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ PickerOd��5      $
��Left=�6'   $��217,139,0,0q�6B   $	��Top=�6W   �� 5   
   -   5        $ 
lab_OdDnia��5      $��	od dnia  ��6$   $
��Left=�68   $��147,141,0,0q�6S   $	��Top=�6h    J� RenderTransformOrigin$ 0.526,0.077&�6�   $��14(�6�   �� 5   
   -   5        $ 
lab_DoDnia��5      $��	do dnia  ��6$   $
��Left=�68   $��370,141,0,0q�6S   $	��Top=�6h   $ 0.526,0.077&�6�   $��14(�6�     5   
   -   5        $ PickerDo��5      $
��Left=�6'   $��440,139,0,0q�6B   $	��Top=�6W   $ 0.343,0.542&�6o   �� 5   
   -   5        $ lab_DataWytworzeniaJpk��5      $��Data wytworzenia JPK ��60   $��Right=�6P   $��0,132,150,0q�6l   $	��Top=�6�   $ 0.842,0.692&�6�   $��22(�6�     5   
   -   5        $ PickerDataWytworzenia��5      $��Right=�64   $��
0,139,10,0q�6P   $	��Top=�6d   $ -0.52,0.667&�6|   �� 5   
   -	   5        $ lab_df1��5      $��Kod formularza��6!   $
��Left=�6:   $��
10,176,0,0q�6U   $	��Top=�6i   �� 5   
   -
   5        $ lab_df2��5      $��Nazwa systemu��6!   $
��Left=�69   $��210,176,0,0q�6T   $	��Top=�6i   �� 5   
   -   5        $ lab_df3��5      $��Wersja schematu��6!   $
��Left=�6;   $��410,176,0,0q�6V   $	��Top=�6k   �� 5   
   -   5        $ lab_df4��5      $��Wariant formularz��6!   $
��Left=�6=   $��610,176,0,0q�6X   $	��Top=�6m   �� 5   
   -   5        $ lab_df5��5      $��Cel złożenia��6!   $
��Left=�68   $��810,176,0,0q�6S   $	��Top=�6h   �� 5   
   -   5         ��Name$ textBox_df10��5      $
��Left=�6(   $��23��6C   $��110,180,0,0q�6O    �� TextWrapping$
 Wrap=�6d   $��TextBox��6x   $	��Top=�6�   ���	#FFFFFFFF6�   �� 5   
   -   5        $ textBox_df20��5      $
��Left=�6(   $��23��6C   $��310,180,0,0q�6O   $
 Wrap=�6d   $��TextBox��6x   $	��Top=�6�   ���	#FFFFFFFF6�   �� 5   
   -   5        $ textBox_df30��5      $
��Left=�6(   $��23��6C   $��510,180,0,0q�6O   $
 Wrap=�6d   $��TextBox��6x   $	��Top=�6�   ���	#FFFFFFFF6�   �� 5   
   -   5        $ textBox_df40��5      $
��Left=�6(   $��23��6C   $��715,180,0,0q�6O   $
 Wrap=�6d   $��TextBox��6x   $	��Top=�6�   ���	#FFFFFFFF6�   �� 5   
   -   5         ��Name$ comboBox_cel��5      $
��Left=�6)   $��903,176,0,0q�6D   $	��Top=�6Y   $ 0.283,-0.091&�6q   .@�5      ��  �� 
IsSelected . $��0��6-   �� 5      $��1��6   �� 5      $��2��6   �� 5       $��3��6   �� 5!      $��4��6   �� 5"      $��5��6   �� 5#      $��6��6   �� 5$      $��7��6   �� 5%      $��8��6   �� 5&      $��9 ��6   5'      �� 5(   
   -   5        $ lab_WybierzFirme��5(      $��Wybierz firmę��6*   $
��Left=�6B   $��
10,231,0,0q�6]   $	��Top=�6q   $��36(�6�   $ 0.456,-0.088&�6�   �� 5)   
   -   5        	 ��Name$	 butAkceptujton��5)      $��Akceptuj��6)   $
��Left=�6<   $��
70,294,0,0q�6W   $	��Top=�6k   $��75��6�   6�   �� 5*   
   -   5        $	 
butZaznacz��5*      $��V��6%   $
��Left=�61   $��
10,294,0,0q�6L   $	��Top=�6`   $��25��6x   6�   �� 5+   
   -   5        $	 
butWyczysc��5+      $��-��6%   $
��Left=�61   $��
40,294,0,0q�6L   $	��Top=�6`   $��25��6x   6�   �� 5,   
   -   5        $	 butWidok��5,      $��	Widok baz��6#   $
��Left=�67   $��150,294,0,0q�6R   $	��Top=�6g   $��75��6   6�   �� 5-   
   -   5        $	 butJPK��5-      $��
Zobacz JPK��6!   $
��Left=�66   $��230,294,0,0q�6Q   $	��Top=�6f   $��75��6~   6�   �� 5.   
   -   5        $	 butDodajXML��5.      $��	Dodaj XML��6&   $
��Left=�6:   $��310,294,0,0q�6U   $	��Top=�6j   $��75��6�   $ 
0.147,-0.4&�6�   6�   �� 5/   
   -   5        $	 bt_ZapiszDoPliku��5/      $��Zapisz zmiany��6+   $��Right=�6C   $��
0,293,10,0q�6_   $	��Top=�6s   $��75��6�   $ 0.515,-0.534&�6�   &   System.Windows.Controls.DataGrid  50   
   -   5        
  Name$
 dg_firmy��50         AutoGenerateColumns .  $	��100��6B   $	��100��6R   $
��Left=�6a   $��
10,318,0,0q�6|   $	��Top=�6�      Columns 52      4  .System.Windows.Controls.DataGridCheckBoxColumn  53      ,  &System.Windows.Controls.DataGridColumn   Header$ Wybierz��6*   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   ��  �� Path$ Zaznacz�0  *System.Windows.Controls.DataGridTextColumn  54      $ Nazwa skrócona��6%    6=   �� $ KrotkaNazwa�  55      $ Pełna nazwa firmy��6%    6@   �� $ 
PelnaNazwa�  56      $	 NIP��6%    62   �� $	 NIP�  57      $ Email��6%    64   �� $ Email�  58      $ Katalog bazy danych��6%    6B   �� $ KatalogBazy�  59      $ Katalog plików JPK��6%    6A   �� $ 
KatalogJpk�  5:      $ Żródło danych��6%    6<   �� $ ZrodloDanych�5>      5A      5B      