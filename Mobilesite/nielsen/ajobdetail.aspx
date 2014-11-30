<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajobdetail.aspx.cs" Inherits="ajobdetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
	<head>
		<meta charset="utf-8">
		<meta name="viewport" content="width=device-width,initial-scale=1">
		<title>nielsen</title>
		<link rel="stylesheet" href="../themes/Bootstrap.css">
        <link href="themes/jquery.mobile.structure-1.4.0.min.css" rel="stylesheet" type="text/css" />
		<link rel="stylesheet" href="../themes/jquery.mobile.icons.min.css" />
        <script src="Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
        <script src="Scripts/jquery.mobile-1.4.0.min.js" type="text/javascript"></script>
	</head>
	<body>
		<div data-role="page" data-theme="e" style="z-index:99999;">
			<div data-role="header" data-position="inline" data-theme="e">
        <img src="../images/nielsen/nielsen.jpg" height="73px"/>
			</div>			
  
		    <div data-role="content" data-theme="a">				
            <h2 id="jobTitle" runat="server">Job Title</h2>
            <br />
            <h4 id="jobLocation" runat="server">地点:</h4>
            <h4 id="jobVertical" runat="server">部门:</h4>
            <p id="jobDes" runat="server"></p>
             <div data-position="inline" data-role="content" role="banner" class="ui-content" style="background:#fff;border:0">
            <form runat="server">
                <fieldset class="ui-grid-a">			        
                    <div class="ui-block-a">
                    <asp:Button ID="applyBtn" class=" ui-btn ui-btn-a ui-shadow ui-corner-all" runat="server" Text="申请职位" data-theme="a" onclick="applyBtn_Click" />
                    </div> 
                    <div class="ui-block-b">
                    <asp:Button ID="backBtn" class=" ui-btn ui-btn-a ui-shadow ui-corner-all" style="margin-top: 8px; margin-bottom: 8px; padding-bottom: 11px; padding-top: 11px; height: 21px;"
                            runat="server" Text="返回" data-theme="a" onclick="backBtn_Click" />
                    </div>                    
                </fieldset>
            </form>     
           
            </div>
            
        </div>

         

	</body>
</html>
