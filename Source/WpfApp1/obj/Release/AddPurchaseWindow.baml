   M S B A M L   `   `   `  ���� B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1      WpfApp1.AddPurchaseWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     'localclr-namespace:WpfApp1       � Title$  AddPurchaseWindow��$	��450��$	��800��.��]� ��. .+��� $��50��$��Bottom=� �� Orientation$ 
Horizontal=�
�������$��RightToLeft=�.+��� $��80��$��35��$��Hủy�� �� IsCancel . $��10, 0, 10, 0q��� -   $��addPurchaseButton��$��80��$��35��$��Thêm�� �� 	IsDefault . $��0, 0, 10, 0q��� .+��� $(��"Số điện thoại khách hàng:�� �� Left$ 10��
 �� Top$ 35���� -    ��Name$ customerTelTextBox��$	��123��$��35��$ 10��$ 56���� $!��Họ và tên khách hàng:��$	 161��$ 35���� -   $ customerNameTextBox��$	��214��$��35��$	 161��$ 56���� $��Tổng tiền: ��$	 231��$	 343���� -    ��Name$ totalTextBlock��$��0�� �� TextAlignment$ Right=�$
��Bold"�
����  �	 �� Right$		 368��$	 343��
 J� RenderTransformOrigin$
 0.463,-0.196&�$	 308��$��67���� -    ��Name$ selectedProductsListView��$	��363��$	��216��$ 12��$	 111������ .R��� .+��� ���� *Product_Name+�� ���� *
Quantity+�� ���� *
Unit_Price+�� 
����  �$
��Bold"����� *
SubTotal+�� -   $��selectButton��$��Chọn��$��80��$��35��$	 596��$ 10���� -   $ productsListView��$	��310��$	��271��$	 472��$ 56������ .R��� .+��� ���� *Name+�� ���� *Price+