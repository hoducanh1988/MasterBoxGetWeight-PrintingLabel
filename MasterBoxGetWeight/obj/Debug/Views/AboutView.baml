   M S B A M L   `   `   `  ����[  WMasterBoxGetWeight&PrintingLabel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:MasterBoxGetWeight.ViewsMasterBoxGetWeight.Views  5      (    "MasterBoxGetWeight.Views.AboutView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:MasterBoxGetWeight.Views     5         color_background#��[  5   7   .��5
      �� $��5,5,5,5q�6   $
��Auto=�6$   $
��Auto=�6G   .��5   
   �� $��0,0,5,0q�6   .+�5      ��   fontSizeHeader#��[ 6   $��SemiBold"�6?   $��Lịch sử sửa đổi��6U   6q   &   System.Windows.Controls.DataGrid  5          AutoGenerateColumns  .  
�������   CanUserAddRows .     CanUserDeleteRows .     CanUserResizeColumns .     CanUserResizeRows .     CanUserSortColumns .     CanUserReorderColumns .  "   VerticalScrollBarVisibility$
 Auto=�5   �   $   HorizontalScrollBarVisibility$
 Auto=�6�   	   GridLinesVisibility$
	 None=�5      
   HeadersVisibility$
 Column=�5      $��0,0,0,0q�5         SelectionUnit$ FullRow=�5         SelectionMode$ Extended=�67   ��5   3   �� *Abouts+   	CellStyle 5      �� 5       �� 
TargetType*  $System.Windows.Controls.DataGridCell"  6   .�5      ��  �� Property � �� �� Value$ 0,0,1,1q�6<   �� 5       � �� �	#FFB3B3B368   �� 5       � ��  fontSizeNormal# [ 65   �� 5       � ��$ Normal"�67   �� 5       � ��$
 Auto��63   �� 5       � ��$ Center=�6E   5         ColumnHeaderStyle 5"      �� 5#      =  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .�5$      ��  � ��# [ 65   �� 5%       � ��$ 40��63   �� 5&       � ��$ SemiBold"�67   �� 5'       � �� �	#FFB3B3B368   �� 5(       � ��$ 0,0,0,1q�6<   �� 5)       � ��$ 3,0,0,0q�64   5*         Columns 5-      0  *System.Windows.Controls.DataGridTextColumn  5.      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *ID+6L     5/      $ Version��6)   $
 Auto 6V    6:   �� *	Version+6V     50      $ Content��6)   $
 Auto 6V    6:   �� *	Content+6V     51      $
 Date��6)   $
 Auto 6P    67   �� *Date+6P     52      $ 
ChangeType��6)   $
 Auto 6\    6=   �� *
ChangeType+6\     53      $ Person��6)   $
 Auto 6T    69   �� *Person+6T   55      �� 57      6   �� 58      #��[ 6   $��SemiBold"�6?   $"��Thông tin về phần mềm��6U   6w   �� 59      6   �� 5:      #��[ 6   .��6?   /-Bản quyền năm 2021 của VNPT Technology6g   �� 5;      #��[ 6   .��6?   PNPhần mềm được phát triển bởi nhóm hỗ trợ sản xuất - HEC.6y   5<      5=      5?      