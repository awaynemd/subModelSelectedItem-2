   M S B A M L   `   `   `  ����  DataGrid2.View    DataGrid2.View.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ SubModelSelectedItem��5   	   $	��350��5   	   $	��500��5	   	   .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��dataGrid��5         AlternatingRowBackground
 ��ح� �� AlternationCount$ 2��5   
      AutoGenerateColumns$ False��6!   ��5   
   �� *Rows+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Time��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *	RowName+4  .System.Windows.Controls.DataGridTemplateColumn  5      $
 0:00��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$	 354 67   	   
IsReadOnly$
	 True��6C   
   CellTemplate
 5      �� 5      .R�5      �� $��Center=�6o   ��6'   �� *	Columns+ �� SelectedItem 6G   �� *SelectedEntity+��5   "   �� 5   &   .R�5   *   ��  �� Orientation$ 
Horizontal=�65   .+�5   .   �� ��68   �� *
CellName+�� 5   .   ��68   �� *SelectedLabel+5   +   5   '   5!      5"      5$      5'      5(      5*      