   M S B A M L   `   `   `  ���� B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  M IFluent, Version=8.0.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546f/$clr-namespace:Fluent;assembly=FluentFluent     WpfApp1.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:WpfApp1     7Fluent$clr-namespace:Fluent;assembly=Fluent     -     � Title$  Laptop Shop��$��642.37��$��906.4�� � 	Resources '   !WpfApp1.ByteArrayToImageConverter   	converter��%   &	          .��]� ��. .+�  Fluent.Ribbon  -     Name$ _ribbon��$	��Top=�   Menu   Fluent.Backstage    Fluent.RibbonControl   Header$ My Shop��   Content.    Fluent.BackstageTabControl  .@�  Fluent.BackstageTabItem     Header$
 Info��.���� $*��$1760449 - Lê Nguyễn Hải Triều��  -   $
 Exit��   Tabs.   Fluent.RibbonTabItem     Header$ Master data��	   Groups.	   Fluent.RibbonGroupBox  $��Import��.@�	  Fluent.Button	  -   $��importexcel��
 	  Header$
 Excel�� 	  	LargeIcon �� $��/Images/Excel.png�� �� BitmapScalingMode$ HighQuality=�  $��Category��.@�	  -   $
 Add category�� �� $��/Images/plus.png��$ HighQuality=�	  -   $
 Edit category�� �� $��/Images/edit.png��$ HighQuality=�	  -   $
 Delete category�� �� $��/Images/delete.png��$ HighQuality=�  $��Product��.@�	  -   $
 Add product�� �� $��/Images/add.png��$ HighQuality=�	  -	   $
 Edit product�� �� $��/Images/test.png��$ HighQuality=�	  -
   $
 Delete product�� �� $��/Images/error.png��$ HighQuality=�  $
 Sale��.	   .@��� $��Center=� �� Orientation$ 
Horizontal=�$��40��$	��Top=�.+��� -   $��addPurchaseButton��$��80��$��30��$��
5, 0, 0, 0q�.���� $ 
Horizontal=�.+��� $��/Images/plus.png��$��20��$��20���� $	��Add��$��
7, 0, 0, 0q�$��Center=��� $��Từ ngày:��$��24��$��
5, 0, 5, 0q�(
  "System.Windows.Controls.DatePicker
  -   $��fromDatePicker�� 
  Text$  ��$	��120��$��30��&   System.Windows.Media.TextOptions   TextHintingMode$ Fixed=��� $��Đến ngày:��$��24��$��
5, 0, 5, 0q�
  -   $��toDatePicker��$	��120��$��30���� $ ��Trạng thái đơn hàng:��$��24��$��
5, 0, 5, 0q��� -   $��purchaseStatesComboBox��$	��100��$��30��$��
5, 0, 5, 0q����� .R��� ���� *Key+�� -    ��Name$ refreshButton��$��Refresh��$��80��$��30��$��100, 0, 5, 0q�  $ Report���� $��Bottom=� �� SelectedIndex ��  �� ElementName$ _ribbon�� �� Path$ SelectedTabIndex�����  �� 
TargetType" ��.���  �� Property � y� �� Value$ 	Collapsed=�.@��� .��]� .+��� $��Bottom=�$��30��.@�]� .+��� -    ��Name$ statusTextBlock��$��All is done��$	��400��� -    �Name$ loadingProgressBar��$��Right=�$	��100��$��10��]� .+��� $ 
Horizontal=�$��40��$	��Top=�.+��� -   $��categoriesComboBox��$	��300��$��30��$��
8, 0, 0, 0q����� .R��� .+���  �� Padding$ 3, 4, 0 , 0q�
����  �$
��Bold"����� *Name+�� .+��� $ HighQuality=�$��/Images/search.png��$��16��$��16�� �� Left$ 18��
 �� Top$ 12�� K� ZIndex$ 10���� -    ��Name$ keywordTextBox��$��30, 5, 0, 0q�$��30��$	��450��$��10, 5, 0 , 0q��� .+��� -   $��productsListView��$	��336��$	��Top=�$	��792������ .R��� .+��� ���� *product_name+�� $	��100��$	��100������ *	Thumbnail+ �� 	Converter# [  �� -   $ previousButton��$��Previous page��$ 53��$	 372��$��87���� -   $ 
nextButton��$��	Next page��$	 426��$	 372��$��76���� -   $��pagingComboBox��$	��120��$	 218��$	 372�� J� RenderTransformOrigin$ 	0.87,0.67&��� .��]� ��. .+��� -   $��purchaseStackPanel��$	��350��
���  ��$��Right=�$y�Visible=�.+�&   System.Windows.Controls.DataGrid  $	��300������ $ purchaseDataGrid��$ SelectedItem.Details�  -      Name$  purchaseDataGrid��!   CanUserSortColumns! . "   CanUserAddRows" .  #   AutoGenerateColumns# .  $   Columns$ 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn%   Header$% Khách hàng��&   CanUserSort& . '   CellTemplate' �� .R��� ���� *Customer_Name+  guestConverter# [   $% Số điện thoại��& . ' �� .R��� ���� *Tel+  $% Tổng tiền��& . ' �� .R��� ���� *Total+  $% Ngày tạo��' �� .R�
  ( 
  SelectedDate( �� *
Created_At+) �� Mode$) OneTime=�* � ContextMenu* �� .@�m� -   $
��Edit��+ m� Icon+ �� $��/Images/edit.png��$��16��$��16��m� -   $��Delete��+ �� $��/Images/delete.png��$��16��$��16��