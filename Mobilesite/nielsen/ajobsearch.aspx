<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajobsearch.aspx.cs" Inherits="ajobsearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
	<head>
		<meta charset="utf-8">
		<meta name="viewport" content="width=device-width,initial-scale=1">
		<title>nielsen</title>
		<link rel="stylesheet" href="../themes/Bootstrap.css">
		<link rel="stylesheet" href="http://code.jquery.com/mobile/1.4.0/jquery.mobile.structure-1.4.0.min.css" />
		<link rel="stylesheet" href="../themes/jquery.mobile.icons.min.css" />
		<script src="http://code.jquery.com/jquery-1.8.2.min.js" type="text/javascript"></script>
		<script src="http://code.jquery.com/mobile/1.4.0/jquery.mobile-1.4.0.min.js" type="text/javascript"></script>
	</head>
	<body>
		<div data-role="page" data-theme="e" style="z-index:99999;">
			<div data-role="header" data-position="inline" data-theme="e">
                <img src="../images/nielsen/nielsen.jpg" height="73px"/>
			</div>			
  
		    <div data-role="content" data-theme="a">				
            <h2></h2>
            

                <form id="Form2" action="" method="post" class="label-top" runat="server">

                <div >
				     <label for="location">地点</label>
                     <asp:DropDownList ID="location" runat="server">
                         <asp:ListItem Selected="True"></asp:ListItem>
                     </asp:DropDownList>
					</div>
                <div>
			         <label for="department">部门</label>
                     <asp:DropDownList ID="department" runat="server">
                         <asp:ListItem Selected="True"></asp:ListItem>
                     </asp:DropDownList>
			    </div>
                <div class="ui-grid-a">			        
                    <div class="ui-block-a">
				    <asp:Button ID="submit" class=" ui-btn ui-btn-a ui-shadow ui-corner-all"  runat="server" Text="搜索" onclick="submit_Click"></asp:Button>
                    </div>
                    <div class="ui-block-b">
                    <asp:Button ID="backBtn" class=" ui-btn ui-btn-a ui-shadow ui-corner-all" 
                            runat="server" Text="返回" data-theme="a" onclick="backBtn_Click"></asp:Button>
                    </div>                    
                </div>

            </form>     
           
            </div>
            
        </div>

         

	</body>
</html>